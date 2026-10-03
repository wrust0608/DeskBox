# DeskBox Vietnamese — Phase P1 Proposal

**Document ID:** `P0_NEXT_PHASE_PROPOSAL`
**Date:** 2026-10-03
**Auditor / Author:** Implementation Engineer
**Status:** PROPOSED (Phase P1 is LOCKED awaiting External Review)

---

## 1. Context & Purpose

Phase P0 (Discovery & Baseline Audit) has established the complete technical baseline:
- Canonical resource key count is verified at **2,677 keys**.
- The localization architecture uses embedded JSON dictionaries with static source-generated contexts for Native AOT, PRI resources for MSIX packaging, and Inno Setup custom messages for the installer.
- The environment has been audited, and the baseline test blocker (.NET SDK version mismatch in `global.json`) has been identified.

This proposal outlines the technical plan for **Phase P1 (Localization Architecture & Design)** once authorized by the Project Manager / External Reviewer.

---

## 2. Proposed Phase P1 Scope & Workstreams

Phase P1 is a design, specification, and contract phase. **No production translation or code changes will be committed in P1.**

### Workstream 1: Terminology Standard & Glossary Specification
- Formalize the Vietnamese UI glossary based on `docs/project-management/04_TRANSLATION_STANDARD.md` and Microsoft Style Guide conventions for Windows 11.
- Standardize key technical and product concepts:
  - "Widget" -> "Widget" / "Tiện ích"
  - "Quick Capture" -> "Ghi chú nhanh"
  - "Todo" -> "Việc cần làm"
  - "Glance" -> "Xem nhanh"
  - "Desktop Organization" -> "Tổ chức màn hình nền"
  - "Managed Storage" -> "Thư mục lưu trữ quản lý"
  - "Capsule Mode" -> "Chế độ con nhộng" (Capsule)
  - "Folder Mapping" -> "Ánh xạ thư mục"
  - "Recycle Bin" -> "Thùng rác"
  - "Taskbar / System Tray" -> "Khay hệ thống"
- Define tone: Professional, modern, active, direct, natural Vietnamese without awkward machine-translated syntax.

### Workstream 2: Technical Architecture Specification for `vi-VN`
- Produce a detailed architectural specification document detailing exact code diffs required across the 14 identified files in P2.
- Define the exact implementation pattern for `WeatherCodeMapper.GetDescriptionVi(code)`.
- Specify the Inno Setup `.isl` Vietnamese translation structure and custom messages.

### Workstream 3: Text Expansion & Layout Risk Mitigation
- Identify WinUI 3 views with narrow fixed-width containers (e.g. Tray menu flyouts, Segmented controls, Card action buttons).
- Establish text length guidelines for Vietnamese translations (e.g. max characters for button labels and menu items) to avoid ellipsis clipping.

### Workstream 4: Automated Parity & Contract Test Design
- Design the automated test cases to be added to `tests/DeskBox.Tests/LocalizationResourceContractTests.cs` and `tests/DeskBox.Tests/LocalizationServiceLanguageTests.cs`.
- Formalize CI validation criteria requiring 100% key parity, non-empty values, and 100% placeholder token parity.

### Workstream 5: .NET SDK Resolution Plan
- Follow the Project Manager's decision on the `global.json` vs SDK `10.0.401` resolution so that the baseline build and tests can be unblocked prior to P2 implementation.

---

## 3. Proposed Phase P1 Deliverables

1. `docs/vi-VN/P1_TERMINOLOGY_GLOSSARY.md` (Standardized glossary of core terms and phrases)
2. `docs/vi-VN/P1_LOCALIZATION_SPECIFICATION.md` (Technical implementation blueprint for P2)
3. `docs/vi-VN/P1_UI_TEXT_GUIDELINES.md` (Length limits and text expansion mitigation rules)
4. `docs/vi-VN/P1_TEST_SPECIFICATION.md` (Automated and manual QA criteria for `vi-VN`)
5. `docs/project-management/STATUS.md` (Updated upon Gate-1 review)

---

## 4. Gate-1 Entry & Exit Criteria

### Entry Criteria:
- [x] Phase P0 Complete with all evidence reports on GitHub.
- [ ] Project Manager approves Gate-0 and authorizes opening Phase P1.
- [ ] Reviewer provides decisions on `.NET SDK` resolution and glossary preferences.

### Exit Criteria (to unlock P2):
- Comprehensive Vietnamese glossary approved by Project Manager.
- Technical architecture specification for `vi-VN` reviewed and verified.
- Unblock strategy for test suite approved.
- Project Manager approves Gate-1 evidence on GitHub.

---

## 5. Lock Status

> [!IMPORTANT]
> **PHASE P1 IS CURRENTLY LOCKED.**
> The Implementation Engineer will not execute any P1 activities until Gate-0 approval is formally granted by the Project Manager / External Reviewer.
