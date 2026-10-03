# 05 — QA Test Plan

## A. Static localization tests
- locale resource parses successfully
- `vi-VN` key set equals canonical locale
- no duplicate keys
- placeholder/token multiset equals source for every key
- locale is registered exactly once
- fallback remains intentional
- resource encoding valid

## B. Regression tests
Use the commands mandated by current upstream `AGENTS.md` for the actual machine architecture. On x64, upstream currently requires the test project to run with `-p:Platform=x64` rather than starting with AnyCPU.

Capture:
- command
- commit SHA
- total
- passed
- failed
- skipped
- duration
- failing test names if any

## C. Runtime UI matrix
Surfaces:
- first-run/onboarding
- main widgets
- context menus
- settings navigation and pages
- dialogs
- tray menu
- search
- Todo
- Quick Capture
- backup/restore UI
- updater/update prompts
- errors/confirmations

Visual checks:
- diacritics
- clipping
- ellipsis
- wrapping
- tooltip fit
- button width
- dialog width/height
- alignment
- light/dark
- 100% / 125% / 150% scaling where feasible

## D. Disposable-file functional tests
Create a dedicated test directory, never the user's real Desktop.

Suggested fixtures:
- `Báo cáo thử nghiệm.txt`
- `Tài liệu dự án`
- `Ảnh màn hình.png`
- `Dữ liệu_Đặng_Thị_Hồng.zip`
- long Vietnamese filename
- nested folders
- shortcut `.lnk`
- Unicode file/folder names

Operations:
- create/map widget
- copy/move
- rename
- delete/recycle
- drag in/out
- stack/group
- search
- reopen/restart persistence

## E. Evidence requirements
- screenshot set (no video required)
- test log path
- build log path
- process executable path during Debug QA
- issue list with severity: BLOCKER / MAJOR / MINOR / COSMETIC
