# 08 — Decision Log

| ID | Decision | Status | Owner | Evidence / rationale |
|---|---|---|---|---|
| D-001 | Base from current upstream `main` vs fixed `v1.5.5` tag | APPROVED | PM | Use upstream `main` SHA `22f231299a64225312864667e6407b166e07a313` (matches `v1.5.5` tag). No rebase/update this pass. |
| D-002 | Canonical translation source locale | APPROVED | PM | Primary: `en-US` (resource contract/key parity). Secondary reference: `zh-CN`. Inspect C#/XAML for semantic context. |
| D-003 | Final Vietnamese wording for Widget / Stack / Capsule / Glance / Quick Capture | RESOLVED | PM | Binding standard in `04_TRANSLATION_STANDARD.md`: Quick Capture & Glance preserved as feature names, Capsule preserved, file stacks natural, folder mapping natural. |
| D-004 | Local Preview coexistence vs in-place install | OPEN | PM | Host has no DeskBox installed. Post-calibration trial. |
| D-005 | Installer localization scope | DEFERRED | PM | Deferred past calibration phase; focus on application runtime locale infrastructure first. |
| D-006 | Whether to maintain a long-lived fork or a patch branch | OPEN | User | Awaiting post-preview decision. |
| D-007 | .NET SDK version resolution (`10.0.303` vs `10.0.401`) | RESOLVED (Option A) | PM | Installed .NET SDK `10.0.303` side-by-side at `C:\Users\nvhoa\AppData\Local\Microsoft\dotnet` without modifying `global.json` or `rollForward`. |
| D-008 | Accept three pre-existing baseline exceptions and use no-new-regression policy | APPROVED | PM | Baseline: 4,618 passed / 3 known exceptions (KB-001 async file race, KB-002 DPI/environment-sensitive, KB-003 Defender/AMSI). Localization baseline: 76/76 PASS. No production fixes for unrelated upstream issues; enforce no-new-regression policy. |
