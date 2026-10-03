# DeskBox Vietnamese — P0 Environment Baseline Report

**Document ID:** `P0_ENVIRONMENT`
**Date:** 2026-10-03
**Auditor:** Implementation Engineer
**Workspace:** `E:\desktop_gui`
**Status:** COMPLETE (P0 Discovery & Audit)

---

## 1. Operating System & Host Hardware

| Parameter | Value | Audit Command / Evidence |
| :--- | :--- | :--- |
| **OS Caption** | Microsoft Windows 11 Home Single Language | `Get-CimInstance Win32_OperatingSystem \| Select-Object Caption` |
| **OS Version** | 10.0.26200 | `Get-CimInstance Win32_OperatingSystem \| Select-Object Version` |
| **OS Build** | 26200 | `Get-CimInstance Win32_OperatingSystem \| Select-Object BuildNumber` |
| **OS Architecture** | 64-bit (`X64`) | `[System.Runtime.InteropServices.RuntimeInformation]::OSArchitecture` |
| **Process Architecture** | `X64` | `[System.Runtime.InteropServices.RuntimeInformation]::ProcessArchitecture` |
| **64-bit OS / Process** | True / True | `[System.Environment]::Is64BitOperatingSystem` / `Is64BitProcess` |

---

## 2. Developer Toolchain & Runtimes

### 2.1 .NET SDK & Runtime Status

| Component | Status | Details / Path |
| :--- | :--- | :--- |
| **Active `global.json` Requirement** | **10.0.303** (`latestPatch`, no prerelease) | Root `global.json` |
| **Installed .NET SDKs** | **10.0.401** | `C:\Users\nvhoa\AppData\Local\Microsoft\dotnet\sdk\10.0.401` |
| **Machine .NET Directory** | `C:\Program Files\dotnet` | Runtimes only (`Microsoft.NETCore.App 10.0.11`, `Microsoft.WindowsDesktop.App 10.0.11`); No SDK |
| **User .NET Directory (`DOTNET_ROOT`)** | `C:\Users\nvhoa\AppData\Local\Microsoft\dotnet` | Host 10.0.12, SDK 10.0.401, Runtimes: `Microsoft.AspNetCore.App 10.0.12`, `Microsoft.NETCore.App 10.0.12`, `Microsoft.WindowsDesktop.App 10.0.12` |
| **.NET SDK Resolution Impact** | **VERSION MISMATCH (BLOCKED)** | `global.json` specifies `10.0.303` with `rollForward: latestPatch`. Because `10.0.401` belongs to feature band `400` (not `3xx`), the CLI rejects executing SDK commands without updating `global.json` or installing SDK `10.0.303`. |

### 2.2 Rust Toolchain

| Component | Version / Status | Evidence |
| :--- | :--- | :--- |
| **Target Pinned by Repo** | 1.96.0 (`rust-toolchain.toml`) | `channel = "1.96.0"` |
| **rustc** | `rustc 1.96.0 (ac68faa20 2026-05-25)` | `rustc --version` |
| **cargo** | `cargo 1.96.0 (30a34c682 2026-05-25)` | `cargo --version` |
| **Active Toolchain** | `1.96.0-x86_64-pc-windows-msvc` | Auto-synced via rustup |

### 2.3 Git & GitHub CLI

| Tool | Version | Authentication Status |
| :--- | :--- | :--- |
| **Git** | `2.55.0.windows.5` | System installed in `C:\Program Files\Git` |
| **GitHub CLI (`gh`)** | `2.100.0 (2026-09-03)` | Authenticated as `wrust0608` (scopes: `repo`, `workflow`, `read:org`, `gist`) |
| **Git Identity** | `wrust0608 <tieensn57@gmail.com>` | Configured in `C:\Users\nvhoa\.gitconfig` |
| **Git Credential Helper** | `!'C:\Program Files\GitHub CLI\gh.exe' auth git-credential` | Verified active |

### 2.4 Build Tools, Compilers & Packaging

| Tool | Status | Details |
| :--- | :--- | :--- |
| **Visual Studio / Build Tools** | Not registered via `vswhere.exe` | `vswhere.exe` returns empty array `[]` |
| **Standalone MSBuild** | Not in PATH | Available only via `dotnet build` |
| **Inno Setup (`ISCC.exe`)** | Not installed in PATH or standard Program Files | Required for installer builds in Phase P6 |
| **Windows App SDK Runtime** | Installed | `Microsoft.WindowsAppRuntime.2` version `2.4.0.0` and `2.5.1.0` (X64) confirmed via `Get-AppxPackage` |

---

## 3. Environment Assessment Summary

1. **Host Readiness for Localization (P0 - P3):**
   - Environment is fully capable of string auditing, file inspection, JSON parsing, parity testing, and git operations.
2. **Blockers for Automated Build / Test:**
   - `global.json` restricts SDK resolution to `10.0.303` (patch-level rollforward). The host has SDK `10.0.401`.
   - As per P0 governance ("Không cài dependency mới trong P0", "Không chỉnh source để làm tests PASS"), baseline testing is classified as `BASELINE TEST BLOCKED` until the Project Manager authorizes either:
     - Option A: Installing .NET SDK `10.0.303`; or
     - Option B: Updating `global.json` rollForward policy (e.g. `minor` or pinning `10.0.401`).
3. **Blockers for Installer Compilation (P6):**
   - Inno Setup 6 (`ISCC.exe`) is not installed on the host. Required only in Phase P6.
