# tools/localization-audit/audit-localization.ps1
# DeskBox Localization & Resource Audit Tool
[CmdletBinding()]
param(
    [string]$RepoRoot = "E:\desktop_gui"
)

$ErrorActionPreference = "Stop"

Write-Host "=== DeskBox Localization Audit ===" -ForegroundColor Cyan

$stringsDir = Join-Path $RepoRoot "src\DeskBox\Strings"
$jsonFiles = Get-ChildItem -Path $stringsDir -Filter "*.json" | Sort-Object Name

Write-Host "`n1. JSON Resource Files in $stringsDir" -ForegroundColor Yellow

$localeData = @{}
$keyCounts = @{}
$placeholderRegex = [regex]'\{(\d+)(?:[^{}]*)\}'

foreach ($file in $jsonFiles) {
    $locale = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
    $content = [System.IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::UTF8)
    $dict = [System.Text.Json.JsonSerializer]::Deserialize($content, [System.Type]::GetType("System.Collections.Generic.Dictionary``2[System.String,System.String]"))
    $localeData[$locale] = $dict
    $keyCounts[$locale] = $dict.Count
    Write-Host "  - Locale: $($locale.PadRight(8)) Keys: $($dict.Count)"
}

# Compare keys against en-US and zh-CN
$canonicalLocale = "en-US"
$canonicalKeys = [System.Collections.Generic.HashSet[string]]::new([string[]]$localeData[$canonicalLocale].Keys)

Write-Host "`n2. Key Parity Check against $canonicalLocale ($($canonicalKeys.Count) keys)" -ForegroundColor Yellow

$keyParityAllPass = $true
foreach ($locale in $localeData.Keys | Sort-Object) {
    $currentKeys = [System.Collections.Generic.HashSet[string]]::new([string[]]$localeData[$locale].Keys)

    $missing = [System.Collections.Generic.HashSet[string]]::new($canonicalKeys)
    $missing.ExceptWith($currentKeys)

    $extra = [System.Collections.Generic.HashSet[string]]::new($currentKeys)
    $extra.ExceptWith($canonicalKeys)

    if ($missing.Count -eq 0 -and $extra.Count -eq 0) {
        Write-Host "  [PASS] $locale has exact key parity with $canonicalLocale ($($currentKeys.Count) keys)" -ForegroundColor Green
    } else {
        $keyParityAllPass = $false
        Write-Host "  [FAIL] $locale mismatch: Missing=$($missing.Count), Extra=$($extra.Count)" -ForegroundColor Red
        if ($missing.Count -gt 0) {
            Write-Host "    Missing keys: $($missing -join ', ')" -ForegroundColor DarkRed
        }
        if ($extra.Count -gt 0) {
            Write-Host "    Extra keys: $($extra -join ', ')" -ForegroundColor DarkRed
        }
    }
}

Write-Host "`n3. Placeholder Parity Check against $canonicalLocale" -ForegroundColor Yellow

$placeholderIssues = @()
foreach ($key in ($canonicalKeys | Sort-Object)) {
    $enVal = $localeData[$canonicalLocale][$key]
    $enMatches = $placeholderRegex.Matches($enVal) | ForEach-Object { $_.Groups[1].Value } | Sort-Object -Unique
    $enTokens = $enMatches -join ","

    foreach ($locale in $localeData.Keys) {
        if ($locale -eq $canonicalLocale) { continue }
        $locVal = $localeData[$locale][$key]
        if ($null -eq $locVal) { continue }

        $locMatches = $placeholderRegex.Matches($locVal) | ForEach-Object { $_.Groups[1].Value } | Sort-Object -Unique
        $locTokens = $locMatches -join ","

        if ($enTokens -ne $locTokens) {
            $placeholderIssues += [PSCustomObject]@{
                Locale = $locale
                Key = $key
                Expected = $enTokens
                Actual = $locTokens
                EnglishText = $enVal
                LocalizedText = $locVal
            }
        }
    }
}

if ($placeholderIssues.Count -eq 0) {
    Write-Host "  [PASS] All placeholders match across all $($localeData.Count) locales!" -ForegroundColor Green
} else {
    Write-Host "  [FAIL] Found $($placeholderIssues.Count) placeholder mismatches:" -ForegroundColor Red
    $placeholderIssues | Format-Table Locale, Key, Expected, Actual -AutoSize
}

Write-Host "`n4. Resources.resw PRI Files Audit" -ForegroundColor Yellow
$resourcesDir = Join-Path $RepoRoot "src\DeskBox\Resources"
$reswDirs = Get-ChildItem -Path $resourcesDir -Directory | Sort-Object Name

foreach ($dir in $reswDirs) {
    $reswPath = Join-Path $dir.FullName "Resources.resw"
    if (Test-Path $reswPath) {
        [xml]$xml = Get-Content -LiteralPath $reswPath -Raw -Encoding UTF8
        $dataNodes = $xml.root.data
        $names = ($dataNodes | ForEach-Object { $_.name }) -join ", "
        Write-Host "  - Locale: $($dir.Name.PadRight(8)) Keys: $($dataNodes.Count) ($names)"
    } else {
        Write-Host "  - Locale: $($dir.Name.PadRight(8)) MISSING Resources.resw!" -ForegroundColor Red
    }
}

Write-Host "`n5. Installer Language Files Audit" -ForegroundColor Yellow
$installerLangDir = Join-Path $RepoRoot "installer\Languages"
$islFiles = Get-ChildItem -Path $installerLangDir -Filter "*.isl*" | Sort-Object Name
foreach ($isl in $islFiles) {
    Write-Host "  - Installer Lang: $($isl.Name) ($($isl.Length) bytes)"
}
