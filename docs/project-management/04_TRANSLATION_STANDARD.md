# 04 — Vietnamese Translation Standard

## Principles
1. Translate meaning, not word order.
2. Prefer terminology familiar to Vietnamese Windows users.
3. Keep labels short enough for WinUI controls.
4. Preserve product names, APIs, protocols and third-party brands.
5. Keep the same concept translated the same way everywhere.
6. Error messages should state what happened and, when appropriate, what the user can do next.
7. Avoid overly formal administrative Vietnamese.
8. Never alter formatting placeholders or markup tokens.

## Initial terminology candidates
These are **provisional** until P1 verifies actual UI context.

| English | Preferred Vietnamese | Notes |
|---|---|---|
| Settings | Cài đặt | Standard Windows wording |
| Desktop | Màn hình nền | |
| Search | Tìm kiếm | |
| Startup | Khởi động cùng Windows | Use shorter variant where space is limited |
| System tray | Khay hệ thống | |
| Recycle Bin | Thùng rác | |
| Backup | Sao lưu | |
| Restore | Khôi phục | |
| Todo | Việc cần làm | |
| Quick Capture | Ghi nhanh | Verify feature semantics |
| Organize Desktop | Sắp xếp màn hình nền | |
| Folder mapping | Liên kết thư mục | Verify distinction from shortcut |
| Managed storage | Vùng lưu trữ DeskBox | |
| Resource saver | Tiết kiệm tài nguyên | |
| Widget | Tiện ích | Review width/context |
| Stack | Nhóm xếp chồng | Context-dependent; may need shorter label |
| Capsule | Dạng thu gọn | Do not translate literally |

## Keep unchanged unless context demands otherwise
DeskBox, Windows, WinUI, .NET, Mica, Acrylic, WebDAV, OneDrive, GitHub, Everything, QuickLook.

## UI length rule
For compact controls, translation should generally not exceed the source label by more than ~35% unless the control dynamically sizes. Any exception must be checked visually.

## Vietnamese typography
- UTF-8 end-to-end.
- Use proper Vietnamese diacritics; no ASCII fallback.
- Avoid unnecessary Title Case.
- Use sentence-style capitalization for descriptions and messages.
- Preserve variables and file paths verbatim.
