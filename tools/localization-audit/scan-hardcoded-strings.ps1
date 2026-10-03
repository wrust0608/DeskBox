# tools/localization-audit/scan-hardcoded-strings.ps1
# Scans C# and XAML files for potential hardcoded user-facing strings in DeskBox
[CmdletBinding()]
param(
    [string]$RepoRoot = "E:\desktop_gui"
)

$ErrorActionPreference = "Stop"
Write-Host "=== Scanning for Hardcoded User-Facing Strings ===" -ForegroundColor Cyan

$srcDir = Join-Path $RepoRoot "src\DeskBox"
$results = [System.Collections.Generic.List[PSCustomObject]]::new()

# 1. Scan XAML files for literal text attributes that lack localization bindings
$xamlFiles = Get-ChildItem -Path $srcDir -Recurse -Filter "*.xaml"
$attrRegex = [regex]'(Text|Content|Header|Title|PlaceholderText|ToolTip)\s*=\s*"([^"{}\r\n]+)"'

foreach ($file in $xamlFiles) {
    $lines = Get-Content -LiteralPath $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        # Ignore comments or styles/templates
        if ($line -match '^\s*<!--' -or $line -match 'xmlns:') { continue }

        $matches = $attrRegex.Matches($line)
        foreach ($m in $matches) {
            $attrName = $m.Groups[1].Value
            $val = $m.Groups[2].Value.Trim()

            # Filter out non-user-facing strings or trivial symbols
            if ($val.Length -le 1 -or $val -match '^[0-9]+(\.[0-9]+)?$' -or $val -match '^([A-Za-z0-9_.]+)$' -or $val -match '^ms-appx:' -or $val -match '^#') {
                continue
            }

            # Check if this control has Localized.Key or similar on the same element or line
            $isLocalized = $line -match 'Localized\.(Key|HeaderKey|DescriptionKey|ToolTipKey)'

            $classification = "REVIEW_REQUIRED"
            if ($isLocalized) {
                # It might be a design-time fallback or sample text
                $classification = "DEBUG_ONLY"
            } elseif ($val -match 'DeskBox|Windows|Explorer|GitHub') {
                $classification = "PROPER_NOUN"
            } elseif ($val -match '^(\w+\.)+\w+$') {
                $classification = "TECHNICAL"
            } else {
                $classification = "LOC_REQUIRED"
            }

            $results.Add([PSCustomObject]@{
                File = $file.FullName.Substring($RepoRoot.Length + 1)
                Line = $i + 1
                Type = "XAML:$attrName"
                String = $val
                Classification = $classification
            })
        }
    }
}

# 2. Scan C# files for ContentDialog, AppNotification, Tray, or WeatherMapper literals
$csFiles = Get-ChildItem -Path $srcDir -Recurse -Filter "*.cs"

foreach ($file in $csFiles) {
    # Skip test or obj/bin
    if ($file.FullName -match '\\obj\\' -or $file.FullName -match '\\bin\\') { continue }
    $lines = Get-Content -LiteralPath $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]

        # Check ContentDialog literal assignments
        if ($line -match 'Title\s*=\s*"([^"]+)"' -and $line -notmatch 'Localized') {
            $val = $matches[1].Trim()
            if ($val.Length -gt 2 -and $val -notmatch '^DeskBox$') {
                $results.Add([PSCustomObject]@{
                    File = $file.FullName.Substring($RepoRoot.Length + 1)
                    Line = $i + 1
                    Type = "C#:DialogTitle"
                    String = $val
                    Classification = "REVIEW_REQUIRED"
                })
            }
        }

        # Check Toast / Notification
        if ($line -match 'AddText\s*\(\s*"([^"]+)"' -and $line -notmatch '_localizationService') {
            $val = $matches[1].Trim()
            if ($val.Length -gt 2) {
                $results.Add([PSCustomObject]@{
                    File = $file.FullName.Substring($RepoRoot.Length + 1)
                    Line = $i + 1
                    Type = "C#:NotificationText"
                    String = $val
                    Classification = "REVIEW_REQUIRED"
                })
            }
        }
    }
}

Write-Host "Total findings: $($results.Count)"
$byClass = $results | Group-Object Classification
foreach ($g in $byClass) {
    Write-Host "  - $($g.Name): $($g.Count)"
}

# Export to CSV / JSON for report generation
$outJson = Join-Path $RepoRoot "tools\localization-audit\hardcoded_strings_inventory.json"
$results | ConvertTo-Json -Depth 3 | Set-Content -LiteralPath $outJson -Encoding UTF8
Write-Host "Exported inventory to $outJson" -ForegroundColor Green
