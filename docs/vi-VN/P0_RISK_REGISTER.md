# DeskBox Vietnamese — P0 Risk Register

**Document ID:** `P0_RISK_REGISTER`
**Date:** 2026-10-03
**Auditor:** Implementation Engineer
**Status:** COMPLETE (P0 Discovery & Audit)

---

## 1. Risk Evaluation Matrix

This risk register evaluates all critical operational, architectural, and data risks associated with the DeskBox Vietnamese localization and testing lifecycle.

---

### Risk 01: Real Desktop Mutation Risk
- **Description:** DeskBox features an "Organize Desktop" command and widget file-mapping operations that can move, rearrange, or group desktop shortcuts and files. If executed inadvertently during preview testing on a developer/user workstation, real user desktop files could be altered or moved into widgets.
- **Severity:** **CRITICAL**
- **Likelihood:** Low (prevented by strict governance)
- **Evidence:** `DesktopOrganizationWindow.xaml.cs`, `WidgetManager.cs`, and `AGENTS.md` describe file grouping operations that move files between Desktop and widgets.
- **Mitigation Proposal:**
  - Strictly forbid executing "Organize Desktop" in P0 through P4.
  - In P5 (Functional Sandbox QA), test only inside a sandbox directory or isolated test desktop environment using generated dummy files.
  - Never run organize desktop on actual user folders.

---

### Risk 02: Managed Storage Data Mutation Risk
- **Description:** DeskBox stores managed files under a dedicated storage directory (e.g. `D:\DeskBox\username` or `%UserProfile%\DeskBox`). During uninstallation, if the user or a test script selects "Purge User Data", unintended deletion could occur.
- **Severity:** **CRITICAL**
- **Likelihood:** Low
- **Evidence:** `installer/DeskBox.iss` lines 126-133 confirm explicit dialogs asking whether to keep or remove application data and confirming that managed storage files are preserved.
- **Mitigation Proposal:**
  - Enforce `06_INSTALL_ROLLBACK_PLAN.md` protocols.
  - In Phase P7/P8, verify uninstaller behavior only with dedicated mock storage directories.
  - Back up any existing mock storage before uninstall trials.

---

### Risk 03: File-Operation Risk (SHFileOperation & Recycle Bin Recovery)
- **Description:** File Widget menu actions ("Move Back to Desktop", "Delete", "Restore from Recycle Bin") use native Win32/Rust shell calls (`SHFileOperationW`, `deskbox_native.dll`).
- **Severity:** **HIGH**
- **Likelihood:** Medium
- **Evidence:** `src/DeskBox/DeskBox.csproj` targets `ValidateDeskBoxNativeAotConfiguration` document exact Recycle Bin queries and `SHFileOperationW` bindings.
- **Mitigation Proposal:**
  - In automated and runtime tests, only operate against temporary fixture folders created under test directories (`tests/DeskBox.Tests/TestFixtures/`).

---

### Risk 04: Localization Regression Risk
- **Description:** Introducing `vi-VN` resources could accidentally introduce syntax errors, missing keys, or malformed JSON that breaks `LocalizationService` or fails unit contract tests.
- **Severity:** **HIGH**
- **Likelihood:** Medium
- **Evidence:** `tests/DeskBox.Tests/LocalizationResourceContractTests.cs` strictly enforces 100% key parity with `en-US` and fails if any key is missing or empty.
- **Mitigation Proposal:**
  - Enforce automated validation with `tools/localization-audit/audit-localization.ps1` and unit tests before any PR or merge.
  - 100% of the 2,677 canonical keys must be present and validated.

---

### Risk 05: Placeholder Corruption Risk
- **Description:** Vietnamese translations might drop, alter, or reorder format placeholders like `{0}`, `{1}`, or escaped tokens (`\n`, `%n` in Inno Setup), leading to runtime `FormatException` crashes during `string.Format`.
- **Severity:** **HIGH**
- **Likelihood:** Medium
- **Evidence:** `LocalizationResourceContractTests.JsonLocales_HaveIdenticalKeysValuesAndPlaceholders` asserts that placeholder token indexes match `en-US` exactly.
- **Mitigation Proposal:**
  - Integrate placeholder regex verification into automated pre-commit and CI test suites.
  - Flag any key where placeholder index set diverges from `en-US`.

---

### Risk 06: UI Clipping & Text Expansion Risk
- **Description:** Vietnamese text typically expands by 15% to 35% compared to English or Chinese due to diacritics, multi-word phrasing, and tone marks. This could cause button labels, settings headers, card descriptions, or widget titles to clip or overflow.
- **Severity:** **MEDIUM**
- **Likelihood:** **HIGH**
- **Evidence:** Many WinUI 3 cards and buttons have fixed or constrained widths (e.g. `TrayMenuItemWidth`, segmented controls, widget titles).
- **Mitigation Proposal:**
  - In Phase P1, establish concise translation standards (`04_TRANSLATION_STANDARD.md`).
  - In Phase P4 (Runtime QA), perform systematic visual audits across all settings cards, widget titles, and tray flyouts.
  - Where necessary, propose XAML `TextTrimming="CharacterEllipsis"` or responsive container adjustments in the Decision Log.

---

### Risk 07: Installer Collision Risk
- **Description:** Installing a preview or custom build over an existing installation could cause version mismatch or directory conflicts.
- **Severity:** **MEDIUM**
- **Likelihood:** Low
- **Evidence:** `DeskBox.iss` enforces `MultipleInstallationsTitle` and `UpgradeDirectoryMismatch` checks using registry entries under `HKCU\Software\DeskBox\DirectInstall`.
- **Mitigation Proposal:**
  - P0 audit confirms DeskBox is **NOT installed** on the host.
  - For future trial phases (P6/P7), specify an isolated custom test directory and verify registry state before and after trials.

---

### Risk 08: Auto-Updater Overwriting Custom Build Risk
- **Description:** If DeskBox's internal auto-updater (`DeskBox.Updater`) detects an upstream release on GitHub, it might prompt or download an official upstream installer (e.g. `v1.5.6`), overwriting the Vietnamese preview build.
- **Severity:** **HIGH**
- **Likelihood:** Medium
- **Evidence:** `src/DeskBox/Services/UpdateService.cs` queries GitHub Releases API.
- **Mitigation Proposal:**
  - In preview builds, disable automatic update checks in settings (`AutoCheckUpdates = false`) or stub the update endpoint during local trials.

---

### Risk 09: Unsigned Installer & SmartScreen Warning Risk
- **Description:** Community/preview installers will not have Tianyu Zhu's official code-signing certificate, triggering Windows SmartScreen warnings ("Windows protected your PC").
- **Severity:** **LOW**
- **Likelihood:** **HIGH**
- **Evidence:** Upstream releases are signed with developer certificates; local builds are unsigned by default.
- **Mitigation Proposal:**
  - Document the expected SmartScreen prompt in user instructions and release notes. Provide SHA-256 checksums for integrity verification.

---

### Risk 10: Application Data Migration & Snapshot Risk
- **Description:** First launch writes settings, widget layouts, and recovery snapshots to `%LocalAppData%\DeskBox`. If formats diverge, rollback could leave corrupted config files.
- **Severity:** **MEDIUM**
- **Likelihood:** Low
- **Evidence:** `SettingsService.cs` contains automatic migration routines and debounced snapshotting.
- **Mitigation Proposal:**
  - All preview test phases must preserve pre-test state and maintain an atomic backup of `%LocalAppData%\DeskBox` before launching test builds.

---

### Risk 11: Native AOT Build Issues
- **Description:** .NET 10 Native AOT publishes require reflection-free JSON serializers, trim-safe COM wrappers, and matching CRT libraries. Missing AOT source generation attributes could cause runtime crashes.
- **Severity:** **HIGH**
- **Likelihood:** Low
- **Evidence:** `LocalizationService.cs` specifically implements `LocalizationJsonContext` (`JsonSerializable(typeof(Dictionary<string, string>))`).
- **Mitigation Proposal:**
  - Strict adherence to upstream Native AOT patterns: keep all string deserialization within the source-generated `LocalizationJsonContext`.

---

### Risk 12: Windows App SDK Dependency Floor
- **Description:** DeskBox requires Windows App Runtime 2.4. On machines lacking runtime 2.4, the app will fail to start.
- **Severity:** **MEDIUM**
- **Likelihood:** Low on this host (verified installed)
- **Evidence:** P0 environment audit verified `Microsoft.WindowsAppRuntime.2` (version 2.4.0.0 and 2.5.1.0) is present on the host.
- **Mitigation Proposal:**
  - For standalone installers, bundle the private runtime components as done in official upstream Full installers (`DeskBoxBundledRuntime=1`).

---

### Risk 13: Upstream Divergence Risk
- **Description:** If upstream commits new features or modifies `en-US.json` while DeskBox Vietnamese is in development, merge conflicts or key parity failures could arise.
- **Severity:** **MEDIUM**
- **Likelihood:** Medium
- **Evidence:** Upstream repository `Tianyu199509/DeskBox` is actively maintained (v1.5.5 released recently).
- **Mitigation Proposal:**
  - Lock baseline SHA (`22f231299a64225312864667e6407b166e07a313`) for current localization milestone.
  - Sync upstream changes only at deliberate, gated sync points after P8 handoff.
