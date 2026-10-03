# DeskBox Vietnamese — Installer & Local Preview Report

## 1. Executive Summary

- **Project:** DeskBox Vietnamese
- **Status:** `VI_PREVIEW_INSTALLED_READY_FOR_USE`
- **Execution Date:** 2026-10-03
- **Platform:** Windows 11 x64 (OS Build 26200)
- **Application Version:** 1.5.5 (FileVersion 1.5.5.0)
- **Installer Scope:** Current-User (`/CURRENTUSER`)

---

## 2. Git & Source Integrity

- **Starting Source HEAD:** `c902a389136933617bdae85641504c7670e1d2d5`
- **Installer Source Commit:** `0b2e854edd12fddbc62a64722f469e1df70a0846` (`feat(installer): add professional Vietnamese localization`)
- **Fix & Runtime Commit:** `c6dbed3a8c3d0dfa0bc0928e08d66df21b0235ad` (`fix(tray): safeguard ContextFlyout assignment against OS 26200 E_NOTIMPL`)
- **Remote Branch:** `feat/vi-vn-localization` (pushed to `origin/feat/vi-vn-localization`)

---

## 3. Installer Localization & Parity

- **Inno Language File:** `installer/Languages/Vietnamese.isl` (UTF-8 with BOM, `LanguageID=$042a`, `LanguageCodePage=0`, `LanguageName=Tiếng Việt`)
- **Installer Configuration:**
  - `installer/DeskBox.iss` (x64): Added `Name: "vietnamese"; MessagesFile: "Languages\Vietnamese.isl"` and `ActiveLanguage = 'vietnamese' -> 'vi-VN'`
  - `installer/DeskBox.arm64.iss` (ARM64): Added `Name: "vietnamese"; MessagesFile: "Languages\Vietnamese.isl"` and `ActiveLanguage = 'vietnamese' -> 'vi-VN'`
- **Custom Messages Coverage:**
  - `installer/DeskBox.NewLanguageCustomMessages.iss`: 24 / 24 keys localized
  - `installer/DeskBox.UninstallCustomMessages.iss`: 2 / 2 keys localized
  - `installer/DeskBox.DependencyCustomMessages.iss`: 7 / 7 keys localized
- **Custom Messages Audit:**
  - Total Custom Messages: 33
  - Missing Keys: 0
  - Extra Keys: 0
  - Placeholder Mismatch: 0

---

## 4. Test Verification

- **Environment:** .NET SDK 10.0.303 x64
- **Installer Contract Tests (`InstallerUninstallContractTests`):**
  - Total: 22
  - Passed: 22
  - Failed: 0
- **Localization Contract Tests (`Localization*`):**
  - Total: 81
  - Passed: 81
  - Failed: 0
- **Full Regression Test Suite:**
  - Total: 4,634
  - Passed: 4,631
  - Failed: 3 (KB-001, KB-002, KB-003 — known baseline exceptions accepted per D-008)
  - Skipped: 0
  - New Regressions: 0

---

## 5. Native AOT Retail Publish

- **Build Script:** `scripts/publish-aot-retail.ps1 -Platform x64 -DotNetPath C:\Users\nvhoa\AppData\Local\Microsoft\dotnet\dotnet.exe`
- **Publish Status:** SUCCESS (Exit code 0)
- **Platform / Architecture:** x64 / `win-x64`
- **Native AOT:** Enabled (`PublishAot=true`)
- **Key Artifacts Verified:**
  - `DeskBox.exe` (PE x64)
  - `DeskBox.Updater.exe` (PE x64)
  - `DeskBox.ThumbnailProxy.exe` (PE x64)
  - `deskbox_native.dll` (PE x64)
  - `EverythingSdk.dll` (PE x64)
  - `DeskBox.pri` (present)
  - `DeskBox.InstallManifest.txt` (present)
  - Forbidden CoreCLR payload: Absent
  - Debug / smoke harness: Excluded in Retail

---

## 6. Installer Artifact Audit

- **Compiler:** Inno Setup 6.4.1 (`C:\Users\nvhoa\AppData\Local\Programs\Inno Setup 6\ISCC.exe`)
- **Flags:** `/DDeskBoxNativeAot=1 /DDeskBoxBundledRuntime=1 /DMyAppReleaseDir=...`
- **Installer Path:** `E:\desktop_gui\.artifacts\direct-installer\DeskBox_Setup_1.5.5_x64.exe`
- **File Size:** 44,624,926 bytes (~42.5 MB)
- **SHA-256:** `2DB547EC43E1E265688F15D9593944B109FA2188D4519CB36BB228E4B6028A8F`
- **Authenticode Status:** `NotSigned` (Unsigned local development preview build)
- **Product Version:** 1.5.5
- **AppId:** `{5E052824-3456-427E-9759-3BCAE078A1D3}`

---

## 7. Pre-Install Safety & Backup

- **Existing Process Stop:** All Debug `DeskBox.exe` stopped cleanly.
- **Managed User Files:** Completely untouched (no deletion, no reorganization, no desktop modification).
- **AppData Backup Path:** `C:\Users\nvhoa\AppData\Local\DeskBox-Vi-Preview-Backup\20261003-154931`
- **Directories Backed Up:**
  - `%LOCALAPPDATA%\DeskBox`
  - `%LOCALAPPDATA%\DeskBox-Recovery`
- **Backup Status:** Success (True)

---

## 8. Installation Execution

- **Command Line:**
  ```powershell
  DeskBox_Setup_1.5.5_x64.exe /CURRENTUSER /LANG=vietnamese /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
  ```
- **Exit Code:** 0 (SUCCESS)
- **Install Scope:** Current-User (`current-user`)
- **Installed Directory:** `C:\Users\nvhoa\AppData\Local\Programs\DeskBox`
- **Desktop Shortcut:** Not created (`desktopicon` unchecked)
- **Startup:** Disabled (no task, no run key, no startup shortcut)

---

## 9. Post-Install Verification & Smoke Test

- **Actual Installed Executable:** `C:\Users\nvhoa\AppData\Local\Programs\DeskBox\DeskBox.exe`
- **Process ID & Path:** PID 7764 (initial), PID 28684 (relaunch), verified running from `C:\Users\nvhoa\AppData\Local\Programs\DeskBox\DeskBox.exe`
- **Registry Key Verification:**
  - `HKCU\Software\DeskBox\DirectInstall\InstallLocation` = `C:\Users\nvhoa\AppData\Local\Programs\DeskBox`
  - `HKCU\Software\DeskBox\DirectInstall\InstallVersion` = `1.5.5`
  - `HKCU\Software\DeskBox\DirectInstall\InstallScope` = `current-user`
  - `HKCU\Software\DeskBox\InstallLanguage` = `vi-VN`
- **Settings Persistence:**
  - `%LOCALAPPDATA%\DeskBox\data\settings.json`: `"language": "vi-VN"` persisted automatically from installer `InstallLanguage`.
- **Relaunch Verification:** App cleanly stopped and relaunched; retained `vi-VN` language setting and started successfully (`OnLaunched completed successfully`).
- **Surface Coverage Status:**
  - **Tray Surface:** Initialized (handles OS 26200 `SetShownInSwitchers` gracefully without crash)
  - **Settings:** Configured with `vi-VN`
  - **Quick Capture:** Operational
  - **Glance:** Operational
  - **Capsule:** Operational
  - **File Stacks:** Operational
  - **Backup / Restore:** Operational
  - **Search:** Operational
  - **About:** Displays Version 1.5.5 (x64)

---

## 10. Rollback Readiness

- **Uninstaller Path:** `C:\Users\nvhoa\AppData\Local\Programs\DeskBox\unins000.exe`
- **Install Location:** `C:\Users\nvhoa\AppData\Local\Programs\DeskBox`
- **AppData Backup Path:** `C:\Users\nvhoa\AppData\Local\DeskBox-Vi-Preview-Backup\20261003-154931`
- **Rollback Readiness Status:** READY

---

## 11. Unresolved Issues

- **None:** 0 blocking issues. Known baseline exceptions KB-001, KB-002, KB-003 remain isolated to upstream test expectations as approved in D-008.
