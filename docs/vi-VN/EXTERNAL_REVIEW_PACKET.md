# DeskBox Vietnamese — External Review Packet

## Repository
- URL: https://github.com/wrust0608/DeskBox.git
- branch: feat/vi-vn-localization
- baseline: 22f231299a64225312864667e6407b166e07a313
- current HEAD: 22f231299a64225312864667e6407b166e07a313

## P0 Status
- status: P0_COMPLETE_AWAITING_EXTERNAL_REVIEW
- source modified: NO
- production files modified: NO
- installation modified: NO

## Environment
- OS: Windows 11 Home Single Language (Version 10.0.26200, Build 26200, 64-bit)
- architecture: X64
- .NET: Host 10.0.12, Installed SDK 10.0.401, global.json pinned 10.0.303 (mismatch blocking CLI)
- Rust: 1.96.0-x86_64-pc-windows-msvc (rustc 1.96.0, cargo 1.96.0)

## Localization
- current locales: 12 (en-US, zh-CN, zh-TW, ja-JP, de-DE, pt-BR, hi-IN, es-ES, fr-FR, ar-SA, bn-BD, ru-RU)
- canonical locale: en-US (and zh-CN)
- canonical key count: 2,677 keys
- parity status: 100% PASS (all 12 locales have identical 2,677 keys and matching {0}, {1} placeholders)
- hardcoded LOC_REQUIRED count: 7 (6 emoji visual icons in XAML + Weather condition table in WeatherCodeMapper.cs)

## Baseline Tests
- command: dotnet test .\tests\DeskBox.Tests\DeskBox.Tests.csproj --no-restore --verbosity:minimal -p:Platform=x64
- total: 0
- passed: 0
- failed: 0
- skipped: 0
- blocked reason: .NET SDK version mismatch. global.json pins SDK 10.0.303 with rollForward: latestPatch. Host has SDK 10.0.401 installed (feature band 400). As per P0 governance ("Không cài dependency mới trong P0", "Không chỉnh source để làm tests PASS"), execution is blocked pending reviewer decision.

## Main Risks
1. Real Desktop Mutation Risk (CRITICAL): "Organize Desktop" can move real desktop files. Mitigation: strictly forbidden in P0-P4; test only in mock sandbox in P5.
2. Managed Storage Mutation Risk (CRITICAL): Uninstall options could prompt to purge data. Mitigation: verify uninstaller strictly with mock storage directories in P7/P8.
3. Localization Parity & Placeholder Corruption Risk (HIGH): Missing keys or corrupted tokens like {0} break builds or crash string.Format. Mitigation: automated parity validation script and unit contract tests enforcing 100% parity.
4. UI Text Expansion & Clipping Risk (HIGH): Vietnamese text expands by 15-35%, risking clipping on fixed-width cards or tray flyouts. Mitigation: concise translation guidelines in P1 and visual UI testing in P4.
5. Auto-Updater Overwriting Custom Build Risk (HIGH): Internal updater querying GitHub Releases could overwrite preview build. Mitigation: disable auto-updates or stub endpoint in preview test builds.

## Proposed P1
Phase P1 (Localization Architecture & Design) will formalize:
- The Vietnamese translation glossary and terminology standards based on Windows 11 WinUI 3 conventions.
- The detailed architectural diff specification for the 14 identified files requiring modification in P2.
- UI text length guidelines to mitigate clipping on compact controls.
- The automated test specification for CI validation.
Phase P1 is LOCKED and will not begin until Project Manager approval.

## Decisions Needed From Reviewer
1. **.NET SDK Resolution for Baseline Build/Test:**
   - Option A: Install .NET SDK `10.0.303` on the host to match upstream `global.json` exactly.
   - Option B: Authorize updating `global.json` to allow `rollForward: "minor"` (or pin `10.0.401`).
2. **Glossary Terminology Guidance:**
   - Confirm terminology preferences for core product terms: "Widget" (keep as "Widget" or "Tiện ích"), "Quick Capture" ("Ghi chú nhanh"), "Capsule Mode" ("Chế độ con nhộng / Capsule"), "Glance" ("Xem nhanh").
3. **Phase Gate Approval:**
   - Confirm whether Phase P0 evidence is accepted and authorize opening Phase P1 (LOCKED -> OPEN).
