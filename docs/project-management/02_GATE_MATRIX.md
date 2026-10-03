# 02 — Gate Matrix

| Gate | Must be true | If not true |
|---|---|---|
| P0 Baseline | Repo/environment/localization architecture fully inventoried | Stop; report missing facts |
| P1 Design | Glossary + surface map approved | Do not translate |
| P2 Static | 100% keys + 100% placeholder parity | Fix before tests |
| P3 Regression | No new failing tests | Stop; isolate failure |
| P4 UI | Vietnamese renders correctly; no blocking clipping/encoding | Fix UI/resources |
| P5 Functional | Disposable file workflows behave correctly | Stop; no installer |
| P6 Package | Correct architecture; reproducible build; hash recorded | Do not install |
| P7 Install | Backup exists; install identity understood; smoke test passes | Roll back |
| P8 Closeout | Rollback documented/verified and reports complete | Project remains OPEN |

## Absolute blockers
- Missing resource keys
- Placeholder mismatch
- Encoding corruption
- Build failure caused by project changes
- New regression failure
- Crash on locale selection
- File operation acting on real Desktop during development QA
- Unclear package identity or unsafe upgrade path
- Any requirement to disable SmartScreen/Defender/security policy to proceed

## Decision authority
The agent may gather evidence and recommend next steps. It may not self-approve a gate that explicitly requires reviewer approval.
