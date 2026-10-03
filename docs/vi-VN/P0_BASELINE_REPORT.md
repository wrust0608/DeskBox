# DeskBox Vietnamese — P0 Discovery & Baseline Audit Report

**Document ID:** `P0_BASELINE_REPORT`
**Date:** 2026-10-03
**Auditor:** Implementation Engineer
**Governance Phase:** `P0 — DISCOVERY & BASELINE AUDIT`
**Phase Status:** `P0_COMPLETE_AWAITING_EXTERNAL_REVIEW`

---

## 1. Executive Summary

This report documents the completion of **Phase P0 (Discovery & Baseline Audit)** for the **DeskBox Vietnamese** (`vi-VN`) localization project.

In strict adherence to project governance:
- **Zero production source code** has been modified.
- **Zero translations** have been introduced.
- **No installer** has been built or run.
- **No real desktop files, registry keys, or user settings** have been mutated.
- The repository was established via a clean GitHub fork under user ownership (`wrust0608/DeskBox`) with explicit tracking to official upstream (`Tianyu199509/DeskBox`).
- All 11 project governance and control documents have been successfully imported into `docs/project-management/`.
- All environment toolchains, localization architectures, resource keys, and potential hardcoded strings were comprehensively measured and audited.

---

## 2. Git & Repository Provenance

| Parameter | Value | Verification Command / Output |
| :--- | :--- | :--- |
| **Local Repository Path** | `E:\desktop_gui` | Working directory |
| **Origin Remote (User Fork)** | `https://github.com/wrust0608/DeskBox.git` | `git remote -v` |
| **Upstream Remote (Official)** | `https://github.com/Tianyu199509/DeskBox.git` | `git remote -v` |
| **Active Working Branch** | `feat/vi-vn-localization` | `git branch --show-current` |
| **Tracking Upstream Branch** | `upstream/main` | `git status -sb` |
| **Baseline Upstream Commit** | `22f231299a64225312864667e6407b166e07a313` | `git rev-parse upstream/main` |
| **Current HEAD Commit** | `22f231299a64225312864667e6407b166e07a313` | `git rev-parse HEAD` |
| **Latest Upstream Tag / Release** | `v1.5.5` (DeskBox 1.5.5, 2026-09-22) | `git describe --tags --abbrev=0` / GitHub API |
| **Git Working Tree State** | Clean (untracked documentation & audit tools only) | `git status --short` |
| **Git Identity Preserved** | `wrust0608 <tieensn57@gmail.com>` | `git config user.name` & `user.email` |
| **Git Hook Attribution Policy** | Enforced via `.githooks/commit-msg` | `git config core.hooksPath .githooks` |

---

## 3. Environment & Toolchain State

| Component | Detected Version / Status | Impact / Notes |
| :--- | :--- | :--- |
| **Operating System** | Windows 11 Home Single Language (Build 26200, Version 10.0.26200, 64-bit) | Supported modern Win11 host |
| **Processor Architecture** | `X64` | Target platform for local preview |
| **.NET SDK Requirement** | `10.0.303` (pinned in `global.json`, `latestPatch`) | Defines repository build contract |
| **Installed .NET SDK** | `10.0.401` (`C:\Users\nvhoa\AppData\Local\Microsoft\dotnet\sdk`) | Feature band 400 (see Blocker below) |
| **Rust Toolchain** | `1.96.0-x86_64-pc-windows-msvc` (`rustc 1.96.0`, `cargo 1.96.0`) | Fully aligned with `rust-toolchain.toml` |
| **Windows App SDK** | Runtime 2.4.0.0 and 2.5.1.0 installed (`Microsoft.WindowsAppRuntime.2`) | Satisfies runtime requirement |
| **Inno Setup (`ISCC.exe`)** | Not installed in PATH | Required in P6 for installer compilation |
| **Visual Studio / Build Tools** | Standalone MSBuild not installed | Build handled via `dotnet` CLI |

---

## 4. Local Installation Discovery (Read-Only)

A non-invasive inspection of the host system was conducted to determine whether DeskBox is currently installed or running:

- **Running DeskBox Processes:** `None` (`Get-Process *DeskBox*` returned zero instances).
- **`%LocalAppData%\DeskBox` Directory:** `Does Not Exist` (`Test-Path` returned `False`).
- **`%LocalAppData%\DeskBox-Recovery` Directory:** `Does Not Exist` (`Test-Path` returned `False`).
- **Registry Uninstall Keys:** `None` (Inspected `HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall` and `HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall`; no DeskBox entry found).
- **Registry App Key:** `None` (`HKCU:\Software\DeskBox` does not exist).

**Conclusion:** The machine is completely clean of any prior DeskBox installation. There is zero risk of existing application data corruption or upgrade conflicts during baseline audit.

---

## 5. Baseline Test Suite Status

### Result: `BASELINE TEST BLOCKED`

**Command Attempted:**
```powershell
dotnet test .\tests\DeskBox.Tests\DeskBox.Tests.csproj --no-restore --verbosity:minimal -p:Platform=x64
```

**Diagnostic Error Output:**
```text
The command could not be loaded, possibly because:
  * You intended to execute a .NET application:
      The application 'test' does not exist or is not a managed .dll or .exe.
  * You intended to execute a .NET SDK command:
      A compatible .NET SDK was not found.

Requested SDK version: 10.0.303
global.json file: E:\desktop_gui\global.json

Installed SDKs:
10.0.401 [C:\Users\nvhoa\AppData\Local\Microsoft\dotnet\sdk]
```

**Root Cause Analysis:**
1. The repository root contains `global.json` pinning .NET SDK `10.0.303` with `"rollForward": "latestPatch"`.
2. The host has .NET SDK `10.0.401` installed under `C:\Users\nvhoa\AppData\Local\Microsoft\dotnet\sdk`.
3. Under .NET SDK rollforward rules, `latestPatch` allows rollforward within the same feature band (`10.0.3xx`), but strictly forbids rolling forward to feature band `400` (`10.0.401`).
4. In compliance with P0 governance ("Không cài dependency mới trong P0", "Không chỉnh source để làm tests PASS"), no SDK installation or modification of `global.json` was performed.

**Resolution Proposals for External Reviewer:**
- **Proposal A (Recommended):** Project Manager authorizes installing .NET SDK `10.0.303` on the host to match the upstream contract exactly.
- **Proposal B:** Project Manager authorizes modifying `global.json` to allow `rollForward: "minor"` or pinning `10.0.401`.

---

## 6. Localization Baseline & Parity Audit

Using the custom automated tool `tools/localization-audit/audit-localization.ps1`, the localization layer was audited directly from source:

- **Resource Architecture:**
  - In-memory embedded JSON dictionaries (`src/DeskBox/Strings/*.json`).
  - Windows Package PRI XML resources (`src/DeskBox/Resources/*/*.resw`).
  - Native Inno Setup language files (`installer/Languages/*.isl`).
  - Hardcoded WMO weather code descriptions in `src/DeskBox/Helpers/WeatherCodeMapper.cs`.
- **Shipped Locales:** 12 locales (`en-US`, `zh-CN`, `zh-TW`, `ja-JP`, `de-DE`, `pt-BR`, `hi-IN`, `es-ES`, `fr-FR`, `ar-SA`, `bn-BD`, `ru-RU`).
- **Canonical Key Count:** Exactly **2,677 keys** per locale.
- **Key-Set Parity:** **100% PASS** across all 12 locales (no missing or extraneous keys).
- **Placeholder Parity:** **100% PASS** across all 12 locales (all `{0}`, `{1}`, etc. match `en-US` indexes).
- **PRI Package Keys:** Exactly 2 keys (`AppDisplayName`, `AppDescription`) in each of the 12 locales.
- **Hardcoded String Audit:**
  - Audited via `tools/localization-audit/scan-hardcoded-strings.ps1`.
  - Found 18 items across XAML, C#, and resources.
  - Zero unlocalized user-facing UI labels remaining in production views.
  - Identified `WeatherCodeMapper.cs` as an architectural requirement needing `GetDescriptionVi(code)` in P2.

---

## 7. Deliverables & Documentation Created

All P0 governance and technical documentation files have been generated:

1. `docs/vi-VN/P0_BASELINE_REPORT.md` (This document)
2. `docs/vi-VN/P0_LOCALIZATION_ARCHITECTURE.md` (Detailed architecture, mechanics, and P2 touchpoints)
3. `docs/vi-VN/P0_STRING_INVENTORY.md` (Classification and inventory of all audited strings)
4. `docs/vi-VN/P0_ENVIRONMENT.md` (Host environment and toolchain report)
5. `docs/vi-VN/P0_RISK_REGISTER.md` (13 evaluated project risks with mitigation strategies)
6. `docs/vi-VN/P0_NEXT_PHASE_PROPOSAL.md` (P1 architecture & design proposal)
7. `docs/vi-VN/EXTERNAL_REVIEW_PACKET.md` (Reviewer decision packet)
8. `docs/project-management/STATUS.md` (Updated to `P0_COMPLETE_AWAITING_EXTERNAL_REVIEW`)
9. `tools/localization-audit/audit-localization.ps1` (Reusable resource and parity audit tool)
10. `tools/localization-audit/scan-hardcoded-strings.ps1` (Reusable string scanner)
11. `tools/localization-audit/hardcoded_strings_inventory.json` (Audit data artifact)

---

## 8. Governance Sign-off & Gate Status

- **Current State:** `P0_COMPLETE_AWAITING_EXTERNAL_REVIEW`
- **Gate Status:** `GATE-0 LOCKED`
- **Next Authorized Action:** STOP and await External Reviewer decision.
