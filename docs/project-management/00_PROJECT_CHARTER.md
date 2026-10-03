# 00 — Project Charter

## Project name
**DeskBox Vietnamese**

## Mission
Produce a high-quality Vietnamese localization for DeskBox that feels native to Windows, preserves upstream behavior, passes localization and regression gates, and can be safely trialed on the user's machine with deterministic rollback.

## Success definition
The project is successful only when all of the following are true:
- `vi-VN` is implemented through DeskBox's existing localization architecture.
- Resource-key coverage is 100% against the canonical locale.
- Formatting-placeholder parity is 100%.
- No unintended Chinese/English UI remains except approved proper nouns, technical names, or documented fallback.
- Upstream regression tests pass for the target architecture.
- Vietnamese UI is visually verified for clipping, encoding, width, light/dark mode and common scaling values.
- File-operation smoke tests use disposable test data only.
- A Preview build can be installed without destroying or mixing user data.
- A written rollback path is validated.
- The agent produces auditable evidence at every gate.

## Primary deliverables
- Vietnamese locale resources
- Language registration and selection support
- Vietnamese README / user-facing localization notes where appropriate
- Localization contract tests or extensions to existing tests
- QA evidence
- Local Preview installer/build artifact if packaging gate is approved
- Installation and rollback report

## Non-goals for Version 1
- Redesigning DeskBox UI
- Replacing upstream localization framework
- Refactoring unrelated application code
- Adding new desktop features
- Rebranding DeskBox
- Publishing to Microsoft Store
- Submitting a PR upstream without explicit instruction
- Enabling startup, cloud backup, or auto-update by default during the trial
- Running destructive file-organization tests on the user's real Desktop

## Governance
Roles:
- **User:** product owner and final approval authority
- **Reviewer:** reviews evidence, approves gates, controls scope
- **Agent:** executes only the currently authorized phase

Rules:
1. No phase may silently expand scope.
2. No destructive cleanup of upstream artifacts without approval.
3. No use of the user's real Desktop as test data during development/QA.
4. No security bypass for unsigned local builds.
5. Every important conclusion must be backed by file paths, commands, test results, or screenshots.
6. If upstream instructions conflict with this plan, stop and report the conflict rather than improvising.

## Branch/workspace convention
Proposed local workspace: `DeskBox-Vietnamese`
Proposed branch: `feat/vi-vn-localization`

The agent must first verify whether it is working from a clean clone, an existing fork, or an already-modified repository before creating or switching branches.
