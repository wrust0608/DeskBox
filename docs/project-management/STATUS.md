# Project Status

**Project:** DeskBox Vietnamese
**Current state:** `VI_PREVIEW_INSTALLED_READY_FOR_USE`
**Execution model:** FAST TRACK (Approved per D-008)
**Code modification authorized:** YES (vi-VN infrastructure, installer localization & OS 26200 runtime fix)
**Translation authorized:** FULL TRANSLATION (2,677 keys translated, audited, and polished)
**Installer build authorized:** YES (Completed & Installed)
**Local installation authorized:** YES (Verified in Current-User mode)

## Fast Track Milestones
- [x] **SDK Unblock:** COMPLETE (.NET SDK 10.0.303 side-by-side)
- [x] **Baseline Verification:** COMPLETE (4,618 / 4,621 PASS; 3 Known Exceptions accepted per D-008)
- [x] **vi-VN Infrastructure:** COMPLETE (Settings, LocalizationService, ViewModel, WeatherCodeMapper, csproj, appxmanifest, resw)
- [x] **Resource Parity:** COMPLETE (2,677 keys, 0 missing, 0 extra, 0 placeholder mismatches)
- [x] **Translation Calibration:** COMPLETE (Real resource keys traceability fixed in `docs/vi-VN/TRANSLATION_CALIBRATION.md`)
- [x] **Full 2,677-Key Translation:** COMPLETE (Domain-by-domain natural translation, 0 missing, 0 empty)
- [x] **Localization Tests:** COMPLETE (81 / 81 PASS)
- [x] **Full Regression Suite:** COMPLETE (4,631 / 4,634 PASS; exactly 0 new regressions matching baseline)
- [x] **Debug Runtime Verification:** COMPLETE (Canonical Debug exe verified on Windows 11 x64, vi-VN loaded)
- [x] **Targeted Language Polish:** COMPLETE (Snapshot terminology, feature widgets, media, clipboard, verify, mapping, cleartext warnings polished)
- [x] **Scope Isolation:** COMPLETE (`App.Tray.cs` safeguarded against OS 26200 E_NOTIMPL)
- [x] **Installer vi-VN Localization:** COMPLETE (`Vietnamese.isl` + 33 custom messages + mapping `vietnamese` -> `vi-VN`)
- [x] **Installer Contract Tests:** COMPLETE (22 / 22 PASS)
- [x] **Native AOT Retail x64 Publish:** COMPLETE (All retail artifacts present, CoreCLR excluded)
- [x] **Direct Inno Installer Build:** COMPLETE (`DeskBox_Setup_1.5.5_x64.exe` compiled cleanly)
- [x] **Safe Local Installation:** COMPLETE (Installed via `/CURRENTUSER /LANG=vietnamese`, exit code 0)
- [x] **Post-Install Verification:** COMPLETE (Installed exe executed, `InstallLanguage = vi-VN`, settings persisted, relaunch verified)

## Active Deliverables
- `installer/Languages/Vietnamese.isl`
- `installer/DeskBox.iss`
- `installer/DeskBox.arm64.iss`
- `installer/DeskBox.NewLanguageCustomMessages.iss`
- `installer/DeskBox.UninstallCustomMessages.iss`
- `installer/DeskBox.DependencyCustomMessages.iss`
- `tests/DeskBox.Tests/InstallerUninstallContractTests.cs`
- `src/DeskBox/App.Tray.cs`
- `docs/vi-VN/INSTALL_REPORT.md`
- `docs/project-management/STATUS.md`

## Next Action Required
DeskBox Vietnamese local preview installed and ready for end-user operation.
