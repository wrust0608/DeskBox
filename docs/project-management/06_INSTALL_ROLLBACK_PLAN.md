# 06 — Installation & Rollback Plan

## Installation principle
No installation trial until P0–P5 pass.

## Before packaging
Confirm:
- machine architecture
- current upstream package identity/versioning
- direct installer vs Store/MSIX behavior
- updater behavior
- whether a locally-built Preview would overwrite, coexist with, or be overwritten by official DeskBox

## Before installation
Inventory:
- currently installed DeskBox version
- running process path
- `%LocalAppData%` data used by DeskBox
- configured managed storage
- startup registration state

Create a timestamped backup manifest containing:
- source paths
- backup paths
- hashes for critical settings/config files when practical
- installer/version identity

Do not delete managed storage.

## Preview safety defaults
For first installed trial:
- startup OFF
- no destructive Organize Desktop action
- no purge user data
- use disposable file test folder
- avoid auto-update replacing the local Preview; only change updater settings by supported application mechanisms

## Rollback path
1. Stop Preview instance.
2. Uninstall/remove only the Preview identity using its supported mechanism.
3. Reinstall previous official version if required.
4. Restore backed-up application data only if compatibility requires it.
5. Verify managed storage/file locations still exist and match manifest.
6. Launch previous version and validate settings/data.

## Rollback acceptance
- user files lost: 0
- managed storage deleted: 0
- prior version launchable: YES
- prior settings/data accessible: YES or explicitly documented migration issue
