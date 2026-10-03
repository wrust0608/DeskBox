# 04 — Professional Vietnamese Localization Standard

**Status:** APPROVED & BINDING
**Core Priority:** **TỰ NHIÊN > DỄ HIỂU > NGẮN GỌN > NHẤT QUÁN > DỊCH ĐỦ**
**Goal:** Tạo cảm giác như DeskBox có giao diện tiếng Việt chính thức được một product team Việt Nam thiết kế. Không đặt mục tiêu “dịch mọi từ tiếng Anh sang tiếng Việt”.

---

## 1. Triết lý dịch: Vietnamese-first, English-when-better

Áp dụng nguyên tắc **Vietnamese-first, English-when-better**.

Có thể giữ nguyên hoặc mix tiếng Anh khi:
- Thuật ngữ tiếng Anh quen thuộc hơn với người dùng Windows/IT tại Việt Nam.
- Bản dịch tiếng Việt quá dài và nặng nề, gây quá tải thị giác hoặc vỡ layout.
- Bản dịch khiến người dùng khó nhận ra chức năng thực tế.
- Tên công nghệ, thư viện, giao thức.
- Tên sản phẩm, thương hiệu.
- Thuật ngữ phổ biến trong cộng đồng Windows.
- UI có không gian hiển thị hạn chế.

> [!IMPORTANT]
> Mục tiêu dự án không phải là đạt `0 English strings`. Mục tiêu bắt buộc là đạt **`0 awkward strings`** (không có câu dịch gượng gạo, văn dịch máy).

---

## 2. Tuyệt đối không dịch Word-by-Word

Không dịch từng từ độc lập tách rời ngữ cảnh. Luôn xác định:
- Control này dùng làm gì?
- Hành động xảy ra sau khi click là gì?
- Đối tượng được thao tác là ai / cái gì?
- Câu xuất hiện ở đâu (button, menu item, card title, card description, toast, error dialog, tooltip)?
- Không gian UI có giới hạn không?

### Ví dụ đối chiếu:
- `Organize Desktop`:
  - ❌ *Tránh:* `Tổ chức màn hình nền` (dịch theo nghĩa đen hành chính)
  - ✅ *Tự nhiên:* `Sắp xếp Desktop` hoặc `Sắp xếp màn hình nền` (chọn theo ngữ cảnh UI).
- `Resource Saver`:
  - ❌ *Tránh:* `Trình tiết kiệm tài nguyên`
  - ✅ *Tốt hơn:* `Tiết kiệm tài nguyên`
- `Quick Capture`:
  - ❌ *Tránh:* `Chụp nhanh` (dịch sai semantic; đây là tính năng ghi chú / lưu nhanh nội dung)
  - ✅ *Tốt hơn:* `Quick Capture` hoặc `Ghi chú nhanh` (xác định chuẩn theo ngữ cảnh UI).

---

## 3. Mix English có chủ đích

Không coi việc giữ từ tiếng Anh là lỗi localization.

### Thuật ngữ giữ nguyên:
- DeskBox, Widget, WebDAV, OneDrive, GitHub, Windows, Mica, Acrylic, WinUI, .NET, URL, CPU, RAM, GPU, Hotkey.

### Thuật ngữ tùy ngữ cảnh UI có thể giữ English:
- Desktop, Widget, Stack, Layout, Preview, Quick Capture, Drag & Drop, Shortcut.

### Quy tắc hòa trộn:
- Nếu người dùng Việt Nam phổ thông hiểu từ English nhanh hơn bản dịch tiếng Việt thì ưu tiên English.
  - Ví dụ: Dùng `Widget trên Desktop` thay vì `Tiện ích trên màn hình nền`.
  - Dùng `Reset layout` thay vì câu tiếng Việt quá dài nếu control nhỏ.
- **Không lạm dụng nửa mùa:** Tránh cấu trúc lủng củng như `Tạo new Widget vào Desktop`. Việc mix phải có chủ ý, tự nhiên và nhất quán.

---

## 4. Giọng điệu & Hành văn (Tone of Voice)

DeskBox sử dụng giọng điệu:
- Hiện đại
- Thân thiện
- Điềm tĩnh
- Ngắn gọn
- Không hành chính, không quan liêu
- Không máy móc (tránh văn dịch từ Google Translate)
- Không quá thân mật, suồng sã

### Từ ngữ cấm kỵ:
- Không dùng: `Quý khách`, `Vui lòng tiến hành...`, `Bạn có chắc chắn rằng bạn muốn tiến hành...`, `Thao tác này đã được thực hiện thành công`.

### Ưu tiên hành văn trực tiếp:
- `Xóa tệp này?`
- `Không thể mở thư mục.`
- `Đã sao lưu.`
- `Khôi phục hoàn tất.`
- `Thử lại`

---

## 5. Quy tắc nút bấm (Button Labels)

Button ưu tiên **động từ ngắn gọn**.

| English | ❌ Tránh | ✅ Chuẩn |
| :--- | :--- | :--- |
| `Save` | Tiến hành lưu | **Lưu** |
| `Cancel` | Thực hiện hủy bỏ | **Hủy** |
| `Delete` | Tiến hành xóa | **Xóa** |
| `Retry` | Thực hiện thử lại | **Thử lại** |
| `Restore` | Tiến hành khôi phục | **Khôi phục** |
| `Apply` | Áp dụng thay đổi | **Áp dụng** |

---

## 6. Quy tắc tiêu đề & Menu (Titles & Menus)

Title và menu cần ngắn, không dùng câu hoàn chỉnh:
- `Backup and Restore` → `Sao lưu & khôi phục`
- `Appearance Settings` → `Giao diện` (không cần `Cài đặt về giao diện`)
- `Startup Behavior` → `Khởi động` (nếu ngữ cảnh nằm trong Cài đặt)

---

## 7. Quy tắc phần mô tả (Descriptions)

Phần mô tả được phép tự nhiên hóa, không cần bám cấu trúc ngữ pháp tiếng Anh:
- *English:* `Automatically start DeskBox when you sign in to Windows.`
- ❌ *Không cần:* `Tự động khởi động DeskBox khi bạn đăng nhập vào Windows.`
- ✅ *Tự nhiên, gọn:* `Mở DeskBox khi đăng nhập Windows.`

---

## 8. Quy tắc thông báo lỗi (Error Messages)

Thông báo lỗi phải trả lời được ít nhất một trong hai câu hỏi:
1. Chuyện gì đã xảy ra?
2. Người dùng cần làm gì tiếp theo?

- ❌ *Bad English:* `An error occurred while attempting to perform the operation.`
- ❌ *Bad Vietnamese:* `Đã xảy ra lỗi khi cố gắng thực hiện thao tác.`
- ✅ *Tốt:* `Không thể di chuyển tệp.`
- ✅ *Có giải pháp:* `Không thể di chuyển tệp. Kiểm tra quyền truy cập rồi thử lại.`
- Tuyệt đối không dịch stack trace hoặc exception message kỹ thuật của hệ thống.

---

## 9. Hộp thoại xác nhận (Confirmations)

Không dùng câu dài rườm rà:
- ❌ *Tránh:* `Bạn có chắc chắn muốn xóa mục đã chọn không?`
- ✅ *Nếu rõ ngữ cảnh:* `Xóa mục này?` hoặc `Xóa 5 mục đã chọn?`
- *Subtext giải thích:* `Các mục sẽ được chuyển vào Thùng rác.`
- *Nút bấm:* `Xóa` / `Hủy`

---

## 10. Tránh từ ngữ Hán-Việt quá hàn lâm

Tránh các từ ngữ hàn lâm nếu có lựa chọn thông dụng hơn:
- `khởi tạo` → `Tạo`
- `thực thi` → `Chạy`
- `truy xuất` → `Mở` / `Tìm`
- `định danh` → `Tên` / `ID`
- `ánh xạ` → `Liên kết`
- `thực thể` → `Mục`
- `cấu hình hóa` → `Cài đặt`

Ví dụ: `Initialize widget` không dùng `Khởi tạo tiện ích`, chỉ cần `Tạo Widget`.

---

## 11. Xử lý thuật ngữ đặc thù DeskBox

- **`Capsule`:** Không dịch là `Viên nang`. Tùy ngữ cảnh dùng `Thu gọn`, `Dạng thu gọn`, hoặc giữ `Capsule` nếu là tên chế độ riêng.
- **`Stack`:** Không mặc định `Ngăn xếp` (thuật ngữ lập trình). Trong UI quản lý tệp, dùng `Nhóm`, `Xếp chồng`, hoặc giữ `Stack`.
- **`Mapping`:** Không mặc định `Ánh xạ`. Với người dùng phổ thông, dùng `Liên kết thư mục`.
- **`Tray`:** Không dùng từ `Khay` đơn độc. Dùng `Khay hệ thống` hoặc `System tray`.

---

## 12. Dịch theo ngữ cảnh (Context-Aware)

Một English key không nhất thiết luôn có một bản dịch duy nhất.
Ví dụ từ `Open`:
- Button action: → `Mở`
- Trạng thái: → `Đang mở`
- Tính từ: → nghĩa khác tùy ngữ cảnh.

Nếu ngữ nghĩa mơ hồ, bắt buộc kiểm tra vị trí sử dụng trong code C# và file XAML trước khi chốt bản dịch.

---

## 13. Kiểm soát độ dài UI (UI Length Budget)

Bản dịch tiếng Việt thường dài hơn tiếng Anh từ 15% đến 35%.
- Ưu tiên rút gọn câu mà không mất nghĩa:
  - `Automatically check for updates` → `Tự động kiểm tra cập nhật` (thay vì `Tự động kiểm tra xem có bản cập nhật mới hay không`).
- Nếu bản dịch gây tràn viền (clipping) hoặc vỡ layout: **xem xét rút gọn wording trước**, không vội can thiệp sửa layout XAML.

---

## 14. Bảng Glossary lõi (Core Consistency Glossary)

| Thuật ngữ gốc | Bản dịch ưu tiên | Ghi chú & Ngữ cảnh |
| :--- | :--- | :--- |
| **Settings** | Cài đặt | Chuẩn Windows |
| **Search** | Tìm kiếm | |
| **Backup** | Sao lưu | |
| **Restore** | Khôi phục | |
| **Delete** | Xóa | |
| **Rename** | Đổi tên | |
| **Appearance** | Giao diện | |
| **Update** | Cập nhật | |
| **Startup** | Khởi động cùng Windows | Rút thành `Khởi động` khi không gian hẹp |
| **Recycle Bin** | Thùng rác | Chuẩn Windows |
| **Folder** | Thư mục | |
| **File** | Tệp | Chuẩn Windows 11 tiếng Việt |
| **Shortcut** | Shortcut / Lối tắt | Tùy ngữ cảnh |
| **Widget** | Widget | Giữ nguyên theo chuẩn product hiện đại |
| **Desktop** | Desktop / Màn hình nền | Tùy ngữ cảnh UI |
| **Quick Capture** | Quick Capture / Ghi chú nhanh | Tùy vị trí title/button |
| **Capsule** | Thu gọn / Capsule | Chế độ thu gọn widget |
| **Stack** | Xếp chồng / Nhóm | Chế độ nhóm các widget |
| **Folder mapping** | Liên kết thư mục | Tạo widget từ thư mục |

---

## 15. Quy trình dịch 3 vòng (Three-Pass Translation Workflow)

Mọi resource tiếng Việt của DeskBox bắt buộc đi qua 3 pass:

```text
[PASS 1: SEMANTIC]
- Dịch toàn bộ resource theo đúng nghĩa
- Đảm bảo 100% key parity (đủ 2,677 keys)
- Bảo toàn nguyên vẹn tất cả placeholder ({0}, {1}...)
        |
        v
[PASS 2: NATURAL VIETNAMESE]
- Đọc lại dưới góc nhìn người dùng Việt Nam
- Loại bỏ văn dịch, câu bị động, câu dài, Hán-Việt rườm rà
- Tự hỏi: "Nếu không nhìn tiếng Anh, câu này có tự nhiên trong app Việt không?"
        |
        v
[PASS 3: UI CONTEXT & RUNTIME QA]
- Chạy DeskBox và kiểm tra trực tiếp trên giao diện thật
- Phát hiện và xử lý clipping, text tràn, menu quá dài
- PASS 3 có quyền tinh chỉnh wording của PASS 1 và PASS 2
```

---

## 16. Phân loại chất lượng đánh giá nội bộ

- **`A — Natural`**: Hoàn toàn tự nhiên, chuẩn phong cách Việt Nam, đạt yêu cầu.
- **`B — Acceptable`**: Dễ hiểu, đúng nghĩa, chấp nhận được.
- **`C — Awkward`**: Gượng gạo, có mùi văn dịch máy → **Bắt buộc viết lại**.
- **`D — Wrong / Misleading`**: Sai nghĩa, hiểu nhầm chức năng → **Cấm xuất hiện**.

---

## 17. Bộ tiêu chí nghiệm thu chuỗi (7-Step Acceptance Checklist)

Trước khi chấp nhận mỗi chuỗi tiếng Việt, kiểm tra qua 7 câu hỏi:
1. Có đúng nghĩa không?
2. Người bình thường đọc có hiểu ngay không?
3. Có giống văn dịch không?
4. Có thể viết ngắn hơn được không?
5. Từ tiếng Anh giữ lại có dễ hiểu hơn không?
6. Có nhất quán với toàn bộ ứng dụng không?
7. Đặt vào UI thực tế có vừa vặn và tự nhiên không?

> **Mục tiêu tối thượng:** Người dùng không nhận ra đây là một bản dịch. Họ chỉ cảm thấy DeskBox có giao diện tiếng Việt cực kỳ tự nhiên, hiện đại và chuẩn mực.
