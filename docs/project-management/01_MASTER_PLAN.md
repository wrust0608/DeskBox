# 01 — Master Plan

## P0 — Discovery & Baseline Audit
**Purpose:** understand the exact current upstream state before changing anything.

Tasks:
- Identify repository path, remote(s), branch, HEAD SHA, working-tree state.
- Read `README.md`, `README.zh-CN.md`, `AGENTS.md`, `CONTRIBUTING.md`, `global.json`, build props, project files, installer scripts and localization-related tests.
- Inventory environment: Windows build, architecture, .NET SDK, Rust, Git, build tools, Inno Setup where relevant.
- Locate canonical localization files and language registry.
- Count keys per locale and placeholder patterns.
- Find user-facing hard-coded strings in C#, XAML, Rust/native, updater and installer.
- Record baseline build/test commands mandated by upstream.
- Detect whether DeskBox is currently installed and whether its local data already exists; do not modify it.

Outputs:
- `docs/vi-VN/BASELINE.md`
- `docs/vi-VN/LOCALIZATION_AUDIT.md`
- machine-readable key-count/placeholder audit if feasible
- short terminal summary

**Hard stop:** Return evidence for review. No source modifications.

---

## P1 — Localization Design
**Purpose:** design before translating.

Tasks:
- Choose canonical semantic source locale (normally English, verified from architecture).
- Create `VI_GLOSSARY.md`.
- Classify terms into: translate / retain brand / retain technical / context-dependent.
- Define punctuation, capitalization, button/label brevity and error-message style.
- Decide handling for terms such as Widget, Stack, Capsule, Glance, Quick Capture, Organize Desktop.
- Map every user-facing surface to resource keys.

Outputs:
- glossary
- translation style guide
- UI surface coverage matrix

**Gate:** terminology and surface map approved.

---

## P2 — `vi-VN` Implementation
**Purpose:** add Vietnamese strictly through existing architecture.

Tasks:
- Add locale resources.
- Register `vi-VN` / `Tiếng Việt`.
- Preserve all placeholders and formatting tokens.
- Localize installer/updater strings only if supported by the same architecture/pattern.
- Add/update Vietnamese README where appropriate.
- Avoid unrelated code changes.

Outputs:
- localized resources
- minimal supporting code/config changes
- change manifest

**Gate:** static localization contracts pass.

---

## P3 — Automated Validation
**Purpose:** prove the translation is structurally correct.

Required checks:
- key-set equality
- placeholder equality
- duplicate/malformed resource detection
- JSON/XML/resource parse validation as applicable
- language registry presence
- installer locale presence where applicable
- no known unintended source-language leaks in localized surfaces

Run upstream target-architecture tests exactly as repository instructions require.

**Gate:** zero localization contract failures and zero new regression failures.

---

## P4 — Debug Runtime QA
**Purpose:** verify real UI without installing a release package.

Tasks:
- Stop only repository-owned DeskBox process if required.
- Build the affected project.
- Launch the canonical Debug executable specified by upstream.
- Verify process path.
- Select Vietnamese and verify persistence.
- Test light/dark and common scaling values where feasible.
- Inspect clipping, truncation, mnemonics, tooltips, tray menu, dialogs and error states.
- Use a disposable test-data folder only.

Evidence:
- screenshots
- exact executable path
- UI issue list

**Gate:** no blocking UI/localization defects.

---

## P5 — Functional Sandbox QA
**Purpose:** ensure localization changes did not break file workflows.

Use only disposable files in a dedicated test directory.

Test:
- widget creation
- mapped folder
- copy/move/rename/delete
- drag/drop
- stack/group behavior
- search
- Todo / Quick Capture
- backup/restore only with test data if included in scope
- Unicode Vietnamese filenames

**Gate:** no data-loss behavior; no regression attributable to localization.

---

## P6 — Release/Installer Build
**Purpose:** produce a reproducible local Preview artifact.

Preconditions:
- P0–P5 approved
- target architecture confirmed
- release pipeline understood

Tasks:
- use upstream release/publish conventions
- generate artifact for machine architecture
- generate SHA-256
- record source SHA and build configuration
- do not claim signing if unsigned

**Gate:** package/install audit passes.

---

## P7 — Safe Local Installation Trial
**Purpose:** test the Vietnamese Preview on the user's machine without risking existing data.

Before installation:
- detect existing DeskBox installation
- inventory app data and managed storage
- create backup manifest
- decide coexistence vs in-place upgrade based on actual package identity/update behavior
- disable auto-update during Preview only if there is a supported app setting or safe documented mechanism; do not weaken Windows security

After installation:
- verify installed executable/package identity
- launch
- choose Vietnamese
- restart and verify persistence
- run non-destructive smoke tests

**Gate:** installed Preview stable and user data intact.

---

## P8 — Rollback Verification & Handoff
**Purpose:** prove recoverability and close the project cleanly.

Tasks:
- document uninstallation/restoration steps
- verify app data backup can be restored if needed
- confirm managed files are untouched
- provide final change summary and unresolved issues

Final outputs:
- `QA_REPORT.md`
- `INSTALL_REPORT.md`
- `ROLLBACK.md`
- `CHANGESET_SUMMARY.md`
- final status summary
