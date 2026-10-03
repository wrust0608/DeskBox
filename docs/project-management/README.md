# DeskBox Vietnamese — Project Control Pack

**Status:** PLANNING / PRE-FLIGHT ONLY
**Rule:** No code modification, translation implementation, installer build, or installation is authorized until Gate P0 is reviewed and approved.

## Goal
Create a professional Vietnamese localization (`vi-VN`) for DeskBox, validate it against upstream architecture and tests, then produce and safely test a local Vietnamese Preview build on Windows with complete rollback evidence.

## Verified upstream baseline (2026-10-03)
- Upstream: `https://github.com/Tianyu199509/DeskBox`
- Latest release observed: `v1.5.5` (2026-09-22)
- Platforms: Windows 10/11, x64 and ARM64
- Stack: C#, WinUI 3, .NET 10 Native AOT, Windows App SDK 2.4, Rust native Shell layer
- Current selectable languages: 12
- License: GPL-3.0-only
- Upstream workflow is governed by `AGENTS.md`
- Canonical Debug executable: `src/DeskBox/bin/Debug/net10.0-windows10.0.22621.0/DeskBox.exe`
- x64 tests must not start with AnyCPU; upstream requires `-p:Platform=x64`

## Project documents
1. `00_PROJECT_CHARTER.md` — scope, objectives, non-goals, governance
2. `01_MASTER_PLAN.md` — phased execution plan
3. `02_GATE_MATRIX.md` — hard stop/go criteria
4. `03_AGENT_BOOTSTRAP_PROMPT.md` — first prompt to give the agent
5. `04_TRANSLATION_STANDARD.md` — Vietnamese terminology and style rules
6. `05_QA_TEST_PLAN.md` — localization, regression and UI test strategy
7. `06_INSTALL_ROLLBACK_PLAN.md` — safe local trial and rollback design
8. `07_REPORT_TEMPLATE.md` — mandatory agent handoff report
9. `08_DECISION_LOG.md` — decisions that require reviewer approval
10. `STATUS.md` — single source of truth for current project state

## Operating model
The agent works in controlled stages. It must stop at every gated milestone and return evidence. The reviewer decides whether the next stage is authorized.

The first agent run is deliberately limited to **P0: discovery and baseline audit**. It is not allowed to edit source files, create `vi-VN`, build an installer, or install DeskBox.
