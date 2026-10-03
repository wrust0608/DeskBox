# 08 — Decision Log

| ID | Decision | Status | Owner | Evidence / rationale |
|---|---|---|---|---|
| D-001 | Base from current upstream `main` vs fixed `v1.5.5` tag | PROPOSED: `main` (SHA `22f231299a64225312864667e6407b166e07a313`) | Reviewer | P0 verified upstream `main` commit matches `v1.5.5` tag commit exactly (`22f231299a64225312864667e6407b166e07a313`). Zero divergence. |
| D-002 | Canonical translation source locale | PROPOSED: `en-US` with `zh-CN` reference | Reviewer | P0 confirmed `en-US` and `zh-CN` both have 2,677 keys. Upstream unit tests (`LocalizationResourceContractTests`) enforce exact key parity with `en-US`. |
| D-003 | Final Vietnamese wording for Widget / Stack / Capsule / Glance / Quick Capture | RESOLVED | Reviewer | Approved binding standard in `docs/project-management/04_TRANSLATION_STANDARD.md`: Vietnamese-first, English-when-better, Widget preserved, 3-pass translation. |
| D-004 | Local Preview coexistence vs in-place install | OPEN | Reviewer | Host has no DeskBox installed. For P7 trial, recommend standalone directory and disabled auto-updater. |
| D-005 | Installer localization scope | PROPOSED: `installer/Languages/Vietnamese.isl` + `DeskBox.NewLanguageCustomMessages.iss` | Reviewer | Architecture mirrors Hindi, French, Spanish in upstream Inno Setup scripts. |
| D-006 | Whether to maintain a long-lived fork or a patch branch | OPEN | User | Awaiting post-preview decision. |
| D-007 | .NET SDK version resolution (`10.0.303` vs `10.0.401`) | OPEN (BLOCKER) | Reviewer | `global.json` requires `10.0.303` (`latestPatch`). Host has `10.0.401`. Need decision: (A) install SDK `10.0.303` or (B) update `global.json` rollForward to `minor`. |
