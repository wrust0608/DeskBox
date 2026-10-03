# DeskBox Vietnamese — P0 Localization Architecture Audit

**Document ID:** `P0_LOCALIZATION_ARCHITECTURE`
**Date:** 2026-10-03
**Auditor:** Implementation Engineer
**Status:** COMPLETE (P0 Discovery & Audit)

---

## 1. Overview of Localization Architecture

DeskBox utilizes a multi-layered localization architecture designed for high performance, dynamic in-memory switching without restart, and compatibility with .NET 10 Native AOT trimming:

```text
+-------------------------------------------------------------------------+
|                              DeskBox UI                                 |
|  +--------------------+   +-----------------------+   +---------------+ |
|  |     XAML Views     |   |   ViewModels/Services |   |   Tray Icon   | |
|  | (Localized.Key etc)|   |  (LocalizationService)|   | (App.Tray.cs) | |
|  +---------+----------+   +-----------+-----------+   +-------+-------+ |
+------------|--------------------------|-----------------------|---------+
             |                          |                       |
             v                          v                       v
+-------------------------------------------------------------------------+
|                  LocalizationService & Localized Extension              |
|  - Language Setting: SettingsService.Settings.Language                  |
|  - Dynamic Refresh: RaiseLanguageChanged() -> Localized.RefreshAll()    |
|  - In-Memory Fallback: Active Locale -> en-US -> zh-CN -> Raw Key       |
|  - Deserializer: System.Text.Json Source Generator (Native AOT)         |
+------------------------------------+------------------------------------+
                                     |
                                     v
+-------------------------------------------------------------------------+
|                     Embedded String Resources                           |
|  - src/DeskBox/Strings/{locale}.json (2,677 keys per locale)            |
|  - Loaded via Assembly.GetManifestResourceStream()                      |
+-------------------------------------------------------------------------+
                                     |
    +--------------------------------+-------------------------------+
    |                                                                |
    v                                                                v
+-----------------------------+                    +-----------------------------+
|    MSIX Package Resources   |                    |    Inno Setup Installer     |
| - src/DeskBox/Resources/    |                    | - installer/Languages/*.isl |
|   {locale}/Resources.resw   |                    | - CustomMessages.iss        |
| - AppDisplayName / Desc     |                    | - Writes InstallLanguage    |
+-----------------------------+                    +-----------------------------+
```

---

## 2. Resource Locations and Formats

### 2.1 Primary Application Strings (`Strings/*.json`)
- **Directory:** `src/DeskBox/Strings/`
- **Format:** Key-value JSON dictionaries (`Dictionary<string, string>`).
- **Embedding:** Registered in `src/DeskBox/DeskBox.csproj` as `<EmbeddedResource Include="Strings\*.json" />`.
- **Loading:** Loaded on-demand into memory via `Assembly.GetManifestResourceStream("DeskBox.Strings.{locale}.json")` and cached in static dictionary instances.
- **Serialization:** Deserialized with `System.Text.Json` using `LocalizationJsonContext` (`JsonSourceGenerationMode.Metadata`) for complete Native AOT compatibility.

### 2.2 Windows Package & Shell PRI Resources (`Resources/*/*.resw`)
- **Directory:** `src/DeskBox/Resources/{locale}/Resources.resw`
- **Format:** XML-based `.resw` resource files.
- **Keys:** Exactly 2 keys per locale: `AppDisplayName` and `AppDescription`.
- **Purpose:** Referenced in `src/DeskBox/Package.appxmanifest` (`ms-resource:AppDisplayName`, `ms-resource:AppDescription`) for Windows Shell, Start Menu, and App Packages.

### 2.3 Weather Interpretation Strings (`WeatherCodeMapper.cs`)
- **File:** `src/DeskBox/Helpers/WeatherCodeMapper.cs`
- **Format:** Hard-coded switch statements per language mapping WMO weather codes (0-99) to localized condition descriptions (e.g. `GetDescriptionZh`, `GetDescriptionEn`, `GetDescriptionJa`, etc.).
- **Method:** `WeatherCodeMapper.GetDescription(int code, string locale)`.

### 2.4 Installer Strings (`installer/Languages/` & `.iss`)
- **Directory:** `installer/Languages/`
- **Format:** Inno Setup language files (`.isl` and Unicode `.islu`).
- **Custom Messages:**
  - `installer/DeskBox.iss` (Base languages: English, Simplified Chinese, Japanese, German, Brazilian Portuguese)
  - `installer/DeskBox.NewLanguageCustomMessages.iss` (Extended languages: Hindi, Spanish, French, Arabic, Bengali, Russian)
  - `installer/DeskBox.HindiBengaliMessages.iss`
  - `installer/DeskBox.UninstallCustomMessages.iss`
  - `installer/DeskBox.DependencyCustomMessages.iss`

---

## 3. Localization Mechanics & Runtime Behavior

### 3.1 Canonical & Fallback Chain
- **Canonical Master:** `en-US` (English) and `zh-CN` (Simplified Chinese).
  - Upstream unit tests enforce that every supported locale must have exact key parity with `en-US`.
- **Fallback Chain in `LocalizationService.T(key)`:**
  1. Active culture table (`CurrentCultureName`)
  2. If key missing: `en-US` table
  3. If key missing: `zh-CN` table
  4. If key missing: raw `key` string returned as safety fallback.

### 3.2 Language Registry & Available Options
Configured in:
1. `src/DeskBox/Services/SettingsService.cs`:
   - Language constants (`LanguageSystem`, `LanguageChinese`, `LanguageEnglish`, etc.).
   - Normalization & validation in `NormalizeSettings()`.
2. `src/DeskBox/Services/LocalizationService.cs`:
   - `AvailableLanguageSettings` list.
   - `GetLanguageDisplayName(string language)`.
   - `ApiLanguageCode` (two-letter code used for weather/geocoding API requests).
3. `src/DeskBox/ViewModels/SettingsViewModel.FeatureOptions.cs`:
   - `AvailableLanguages` array.
   - `AvailableLanguageDisplayNames` array.

### 3.3 Dynamic Culture Switching
- When the user selects a language in Settings:
  1. `LocalizationService.SetLanguage(lang)` updates `SettingsService.Settings.Language`.
  2. `SettingsService.SaveDebounced()` asynchronously persists settings to `%LocalAppData%\DeskBox\data\settings.json`.
  3. `RaiseLanguageChanged()` fires the `LanguageChanged` event.
  4. `Localized.RefreshAll(this)` iterates all tracked UI elements across all open windows and updates text dynamically.
  5. `App.Tray.cs` re-evaluates all tray menu text.
  6. **No application restart is required.**

### 3.4 Startup Language Resolution
When DeskBox starts:
1. If user previously chose a language in settings, that setting is honored.
2. If language is `System` (default):
   - First checks Windows Registry: `HKCU\Software\DeskBox\InstallLanguage` (written by the Inno Setup installer at install time).
   - If registry value absent or unrecognized: checks `CultureInfo.CurrentUICulture.Name` (matches prefix or culture name).
   - If still unmatched: falls back to `LanguageEnglish` (`en-US`).

### 3.5 Tray & Notifications
- **Tray Context Menu (`src/DeskBox/App.Tray.cs`):**
  - All tray items (`OrganizeDesktop`, `NewFolderMapping`, `AddFeatureWidget`, `Settings`, `OpenManagedStorage`, `UpdateAvailable`, `Exit`) load localized text via `_localizationService.T(...)`.
  - Subscribes to `LanguageChanged` to update text dynamically.
- **Native Notifications (`src/DeskBox/Services/NativeAppNotificationService.cs`):**
  - Toast notifications receive pre-localized title and message strings from callers.
  - Callers (e.g. `TodoWidgetViewModel`) use `_localizationService.Format(...)`.

### 3.6 Updater (`src/DeskBox.Updater`)
- A lightweight headless launcher (`Program.cs`) that coordinates the Inno Setup installer and app restart.
- Has no user-facing UI or dialogs; all strings are diagnostic log messages written to `%LocalAppData%\DeskBox\DeskBox.Updater.log`.

### 3.7 Native / Rust Layer (`native/`)
- Native modules (`deskbox-native`, `deskbox-thumbnail-proxy`, `deskbox-audio-session-fixture`) provide low-level Win32/COM shell boundaries (shortcuts, volume, quick access, recycle bin).
- Pure binary ABI; contains no localized user-facing strings.

---

## 4. Current Locale Inventory & Metric Verification

All metrics below were computed directly from current repository source files using `tools/localization-audit/audit-localization.ps1`:

| Locale Code | Language Name | Native Display Name | `.json` Keys | Key Parity vs `en-US` | Placeholder Parity | `.resw` Keys | `.isl` Installer File |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **`en-US`** | English | English | **2,677** | Canonical (100%) | Canonical (100%) | 2 | `English.isl` |
| **`zh-CN`** | Simplified Chinese | 简体中文 | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `ChineseSimplified.isl` |
| **`zh-TW`** | Traditional Chinese | 繁體中文 | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `ChineseTraditional.isl` |
| **`ja-JP`** | Japanese | 日本語 | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `Japanese.isl` |
| **`de-DE`** | German | Deutsch | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `German.isl` |
| **`pt-BR`** | Portuguese (Brazil) | Português (Brasil) | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `BrazilianPortuguese.isl` |
| **`hi-IN`** | Hindi | हिन्दी | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `Hindi.islu` |
| **`es-ES`** | Spanish | Español | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `Spanish.isl` |
| **`fr-FR`** | French | Français | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `French.isl` |
| **`ar-SA`** | Arabic | العربية | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `Arabic.isl` |
| **`bn-BD`** | Bengali | বাংলা | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `Bengali.islu` |
| **`ru-RU`** | Russian | Русский | **2,677** | PASS (Exact Match) | PASS (Exact Match) | 2 | `Russian.isl` |

### Audit Conclusions:
1. **Total Shipped Locales:** 12 locales.
2. **Canonical Key Count:** Exactly **2,677** keys.
3. **Key-Set Parity:** 100% PASS across all 12 locales (no missing keys, no extraneous keys).
4. **Placeholder Parity:** 100% PASS across all 12 locales (all `{0}`, `{1}`, etc. match `en-US` indexes).
5. **Packaged PRI Parity:** Exactly 2 keys (`AppDisplayName`, `AppDescription`) across all 12 locales.

---

## 5. Scope of Changes Required in Phase P2 (`vi-VN`)

When implementation authorization is granted for Phase P2, the following exact files will need modification or addition:

| Component | Target File | Nature of Change |
| :--- | :--- | :--- |
| **Strings Resource** | `src/DeskBox/Strings/vi-VN.json` | **New file:** 2,677 translated keys matching `en-US.json` key-set and placeholders |
| **PRI Package Resource** | `src/DeskBox/Resources/vi-VN/Resources.resw` | **New file:** `AppDisplayName` and `AppDescription` in Vietnamese |
| **Project Definition** | `src/DeskBox/DeskBox.csproj` | Add `<EmbeddedResource Include="Strings\vi-VN.json" />` |
| **Package Manifest** | `src/DeskBox/Package.appxmanifest` | Add `<Resource Language="vi-VN" />` to `<Resources>` |
| **Settings Constants** | `src/DeskBox/Services/SettingsService.cs` | Add `LanguageVietnamese = "vi-VN"` constant & normalize check |
| **Localization Service** | `src/DeskBox/Services/LocalizationService.cs` | Add constant, display name ("Tiếng Việt"), API code ("vi"), table loading, and culture resolution |
| **Settings ViewModel** | `src/DeskBox/ViewModels/SettingsViewModel.FeatureOptions.cs` | Add `LanguageVietnamese` to `AvailableLanguages` |
| **Weather Description** | `src/DeskBox/Helpers/WeatherCodeMapper.cs` | Add `GetDescriptionVi(int code)` method and switch branch in `GetDescription` |
| **Inno Setup Script** | `installer/DeskBox.iss` & `DeskBox.arm64.iss` | Add Vietnamese language declaration and custom messages |
| **Inno Setup Lang Pack** | `installer/Languages/Vietnamese.isl` | **New file:** Inno Setup Vietnamese language definition |
| **Inno Setup Messages** | `installer/DeskBox.NewLanguageCustomMessages.iss` | Add `vietnamese.*` custom messages |
| **Unit Contract Tests** | `tests/DeskBox.Tests/LocalizationResourceContractTests.cs` | Add `"vi-VN"` to `SupportedLocales` array |
| **Unit Language Tests** | `tests/DeskBox.Tests/LocalizationServiceLanguageTests.cs` | Add `"vi-VN"` to test data tables |
| **Weather Unit Tests** | `tests/DeskBox.Tests/WeatherCodeMapperLocalizationTests.cs` | Add `[InlineData("vi-VN", ...)]` test case |
