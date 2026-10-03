# 03 — Agent Bootstrap Prompt

You are the implementation agent for the **DeskBox Vietnamese** project.

Your authorization in this run is **P0 — Discovery & Baseline Audit ONLY**.

## Core rule
Do **not** implement Vietnamese yet. Do **not** modify application source, localization resources, installer files, project files, or tests. Do **not** build an installer. Do **not** install/uninstall DeskBox. Do **not** reorganize the user's real Desktop.

The goal of this run is to produce an exact baseline and an implementation-ready localization audit for reviewer approval.

## Upstream
Repository: `https://github.com/Tianyu199509/DeskBox`

Treat the repository's current `AGENTS.md` as mandatory. Read it before any build/test/restart action.

## Step 1 — Identify workspace state
Report:
- repository absolute path
- `git remote -v`
- current branch
- `git rev-parse HEAD`
- `git status --short`
- latest relevant tag/release visible locally
- whether this is a clean clone, fork, or modified working tree

Do not discard or overwrite existing changes.

## Step 2 — Read project governance and architecture
Read at minimum:
- `AGENTS.md`
- `README.md`
- `README.zh-CN.md`
- `CONTRIBUTING.md`
- `global.json`
- `Directory.Build.props`
- solution/project files
- localization-related source/resources
- localization-related tests
- updater project
- installer scripts
- build/release scripts
- relevant docs under `docs/architecture` and `docs/releases`

## Step 3 — Environment baseline
Capture:
- Windows edition/version/build
- OS architecture
- CPU architecture
- installed .NET SDKs, highlighting the SDK selected by `global.json`
- Rust toolchain
- Git version
- Visual Studio / Build Tools availability if required
- Inno Setup availability if relevant to later packaging

Do not install missing dependencies in P0. Only report them.

## Step 4 — Localization architecture audit
Find and report exact paths for:
- all locale resource files
- canonical/fallback locale
- locale registry / language selector
- culture persistence
- runtime resource loading
- installer language resources
- updater strings
- notifications/toasts
- error dialogs
- onboarding
- tray UI

For every locale:
- count keys
- compare key sets
- compare formatting placeholders/tokens
- identify malformed or exceptional entries

Do not assume historical key counts. Count the current checkout.

## Step 5 — Hard-coded user-facing string audit
Search C#, XAML, Rust/native, updater and installer code for user-facing strings that bypass the localization layer.

Classify findings:
- A — should be localized for `vi-VN`
- B — debug/log/diagnostic only
- C — proper noun/technical term that should remain unchanged
- D — uncertain; reviewer decision required

Provide file path + line/reference + sample string for each relevant finding.

## Step 6 — Establish future test/build commands
From current `AGENTS.md` and repository scripts, identify the exact commands that should be used later for:
- restore
- x64 tests on an x64 machine OR ARM64 tests on ARM64
- Debug build/restart workflow
- Release publish
- installer build

Do not invent commands when repository instructions already exist.

## Step 7 — Inspect current installed state without changing it
Determine whether DeskBox is currently installed/running.

If found, record only:
- process path
- installed version/package identity if safely observable
- likely app-data locations
- whether managed storage appears configured

Do not stop an installed production instance unless needed merely to inspect it, and do not modify its settings/data during P0.

## Step 8 — Create P0 reports only
Create:
- `docs/vi-VN/BASELINE.md`
- `docs/vi-VN/LOCALIZATION_AUDIT.md`

If creating these two documentation files would itself violate the user's requirement of zero tracked changes during audit, write them outside the repo in a temporary audit folder and report that location. Do not touch source code.

`BASELINE.md` must include:
- repo path
- remotes
- branch
- SHA
- working-tree state
- environment/toolchain
- target architecture
- installed DeskBox state
- missing prerequisites

`LOCALIZATION_AUDIT.md` must include:
- localization architecture
- exact resource paths
- current languages
- canonical/fallback locale
- per-locale key counts
- key-set parity result
- placeholder parity result
- hard-coded user-facing findings
- installer/updater localization mechanism
- proposed exact files that would need modification in P2
- risks and open questions

## Hard stop
At the end of P0, STOP.

Do not create `vi-VN`.
Do not translate strings.
Do not change code.
Do not run release packaging.
Do not install anything.

Return a concise handoff using this exact structure:

P0 STATUS: PASS / BLOCKED
REPO PATH:
BRANCH:
HEAD SHA:
WORKTREE CLEAN: YES/NO
TARGET ARCH:
CURRENT LANGUAGES:
CANONICAL LOCALE:
KEY COUNT PER LOCALE:
KEY PARITY:
PLACEHOLDER PARITY:
HARDCODED UI FINDINGS:
MISSING PREREQUISITES:
INSTALLED DESKBOX DETECTED: YES/NO
PROPOSED FILES FOR P2:
P0 REPORT PATHS:
BLOCKERS / REVIEW DECISIONS:

Wait for reviewer approval before any P1/P2 work.
