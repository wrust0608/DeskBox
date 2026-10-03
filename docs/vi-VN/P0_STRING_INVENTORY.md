# DeskBox Vietnamese — P0 Hard-Coded String Inventory

**Document ID:** `P0_STRING_INVENTORY`
**Date:** 2026-10-03
**Auditor:** Implementation Engineer
**Audit Tool:** `tools/localization-audit/scan-hardcoded-strings.ps1`
**Data Artifact:** `tools/localization-audit/hardcoded_strings_inventory.json`
**Status:** COMPLETE (P0 Discovery & Audit)

---

## 1. Classification Scheme

All scanned literals and candidate strings were audited against five governance categories:

- **`LOC_REQUIRED`**: Strings that are visible to end-users during normal execution and should be localized.
- **`DEBUG_ONLY`**: Strings appearing only in debug logs, trace messages, or test smoke fixtures.
- **`TECHNICAL`**: Technical constants, file extensions, regex patterns, formatting tokens, API parameters, or invariant identifiers.
- **`PROPER_NOUN`**: Brand names, product names ("DeskBox", "Windows", "GitHub", "Explorer") or standard proper nouns.
- **`REVIEW_REQUIRED`**: Borderline or architectural strings requiring Project Manager / Reviewer guidance before modification.

---

## 2. Hard-Coded String Inventory Findings

### 2.1 XAML User-Facing Surface Scan

| File | Line | Type | Value | Classification | Assessment & Recommendation |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `src/DeskBox/Views/ContentWidgetWindow.xaml` | 11 | `Title` | `"DeskBox Content Widget"` | `PROPER_NOUN` | Window title bar text for content widget. "DeskBox" is proper noun. Subtitle can remain or be localized in P2. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 11 | `Title` | `"DeskBox Onboarding"` | `PROPER_NOUN` | Window title for onboarding setup. Proper noun. |
| `src/DeskBox/Views/QuickCaptureWidgetWindow.xaml` | 13 | `Title` | `"DeskBox Quick Capture"` | `PROPER_NOUN` | Window title for Quick Capture. Proper noun. |
| `src/DeskBox/Views/SettingsWindow.xaml` | 17 | `Title` | `"DeskBox Settings"` | `PROPER_NOUN` | Window title for Settings. Proper noun. |
| `src/DeskBox/Views/SettingsWindow.xaml` | 1460 | `PlaceholderText` | `".psd .ai .fig"` | `TECHNICAL` | Example list of file extensions for file type filtering. Language-neutral; keep as is. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 171 | `Text` | `"📁"` | `LOC_REQUIRED` (Visual Icon) | Emoji character used as folder icon in onboarding illustration. Keep as is. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 186 | `Text` | `"📄"` | `LOC_REQUIRED` (Visual Icon) | Emoji character used as document icon. Keep as is. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 272 | `Text` | `"🗂️"` | `LOC_REQUIRED` (Visual Icon) | Emoji character used as card box icon. Keep as is. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 276 | `Text` | `"🗃️"` | `LOC_REQUIRED` (Visual Icon) | Emoji character used as file box icon. Keep as is. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 280 | `Text` | `"📂"` | `LOC_REQUIRED` (Visual Icon) | Emoji character used as open folder icon. Keep as is. |
| `src/DeskBox/Views/OnboardingWindow.xaml` | 285 | `Text` | `"⚙️"` | `LOC_REQUIRED` (Visual Icon) | Emoji character used as gear icon. Keep as is. |

*Note: All textual user-facing labels in `SettingsWindow.xaml`, `ContentWidgetWindow.xaml`, `QuickCaptureWidgetWindow.xaml`, `DesktopOrganizationWindow.xaml`, and `OnboardingWindow.xaml` use `Localized.Key`, `Localized.HeaderKey`, or `Localized.DescriptionKey`.*

---

### 2.2 C# Code Behind & Service Scan

| File | Line | Type | Value | Classification | Assessment & Recommendation |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `src/DeskBox/App.AotTodoNotificationLifecycleSmoke.cs` | 221 | `DialogTitle` | `"DeskBox Todo AOT 验证（单项）"` | `DEBUG_ONLY` | Smoke test harness for AOT validation; only compiled with `DESKBOX_AOT_SMOKE_HARNESS`. Do not modify. |
| `src/DeskBox/App.AotTodoNotificationLifecycleSmoke.cs` | 223 | `DialogTitle` | `"DeskBox Todo AOT 验证（聚合）"` | `DEBUG_ONLY` | Smoke test harness for AOT validation. Do not modify. |
| `src/DeskBox/App.AotTodoStepsPersistenceSmoke.cs` | 10 | `DialogTitle` | `"AOT Todo steps task"` | `DEBUG_ONLY` | Smoke test harness for AOT validation. Do not modify. |
| `src/DeskBox/Controls/WidgetContents/TodoWidgetContent.AotPersistenceSmoke.cs` | 9 | `DialogTitle` | `"AOT Todo initial task"` | `DEBUG_ONLY` | Smoke test fixture. Do not modify. |
| `src/DeskBox/Controls/WidgetContents/TodoWidgetContent.AotPersistenceSmoke.cs` | 10 | `DialogTitle` | `"AOT Todo persisted edited title"` | `DEBUG_ONLY` | Smoke test fixture. Do not modify. |
| `src/DeskBox/Controls/WidgetContents/TodoWidgetContent.AotStepsPersistenceSmoke.cs` | 11 | `DialogTitle` | `"AOT Todo steps task"` | `DEBUG_ONLY` | Smoke test fixture. Do not modify. |

---

### 2.3 Weather Description Table in `WeatherCodeMapper.cs`

**Status:** `LOC_REQUIRED` (Architectural exception to JSON localization).

`src/DeskBox/Helpers/WeatherCodeMapper.cs` contains hard-coded switch statements for every language:
- `GetDescriptionZh(int code)`
- `GetDescriptionZhTw(int code)`
- `GetDescriptionEn(int code)`
- `GetDescriptionJa(int code)`
- `GetDescriptionDe(int code)`
- `GetDescriptionPt(int code)`
- `GetDescriptionHi(int code)`
- `GetDescriptionEs(int code)`
- `GetDescriptionFr(int code)`
- `GetDescriptionAr(int code)`
- `GetDescriptionBn(int code)`
- `GetDescriptionRu(int code)`

**Action for P2:**
Add a new method `GetDescriptionVi(int code)` containing Vietnamese weather condition translations corresponding to the 28 standard WMO weather interpretation codes, and route `"vi-VN" => GetDescriptionVi(code)` in `GetDescription(int code, string locale)`.

---

### 2.4 Predefined City Database (`src/DeskBox/Assets/Cities/cities.json`)

**Status:** `TECHNICAL` / `REVIEW_REQUIRED`.

- `cities.json` is an embedded JSON resource containing ~4,950 lines of city records.
- Schema per city:
  - `zh`: Chinese name
  - `en`: English name
  - `pinyin`: Pinyin romanization
  - `lat` / `lon`: Coordinates
  - `country_zh` / `country_en`
  - `admin1_zh` / `admin1_en`
- All other 10 non-Chinese languages (`ja`, `de`, `pt`, `hi`, `es`, `fr`, `ar`, `bn`, `ru`) query against the English and Chinese fields, falling back to dynamic search via Open-Meteo Geocoding API.
- **Recommendation:** No schema alteration to `cities.json` is required for `vi-VN`; Vietnamese users will search cities via the existing Open-Meteo online geocoding API or English names, matching the behavior of German, French, Spanish, etc.

---

### 2.5 Inno Setup Custom Messages (`installer/*.iss`)

**Status:** `LOC_REQUIRED`.

The installer contains custom dialog messages for:
- Detecting multiple DeskBox installations
- Directory mismatch warnings during upgrades
- Retention choices for application data during uninstallation (`AppDataChoiceTitle`, `KeepAppDataButton`, etc.)
- Managed storage warning confirming files will not be deleted
- Runtime dependency downloading and installation progress

**Action for P2:**
Provide accurate Vietnamese translations for all custom message keys in `installer/DeskBox.NewLanguageCustomMessages.iss` (or `DeskBox.iss`), prefixed with `vietnamese.`.

---

## 3. Inventory Summary

| Classification | Count | Description / Scope |
| :--- | :--- | :--- |
| **`LOC_REQUIRED`** | **7** | 6 emoji visual icons in XAML (no change needed) + Weather conditions in `WeatherCodeMapper.cs` & Installer custom messages |
| **`DEBUG_ONLY`** | **6** | All in AOT smoke test harnesses (`App.Aot*.cs`, `TodoWidgetContent.Aot*.cs`) |
| **`TECHNICAL`** | **1** | File extensions (`.psd .ai .fig`) in Settings filter placeholder |
| **`PROPER_NOUN`** | **4** | Window title literals ("DeskBox Content Widget", "DeskBox Settings", etc.) |
| **`REVIEW_REQUIRED`** | **0** | No ambiguous production user-facing strings remaining outside the localization layer |
| **Total Items Audited** | **18** | Complete inventory established |
