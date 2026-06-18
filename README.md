# NetMail

NetMail là một ứng dụng WinForms hỗ trợ gửi email theo nhiều định dạng, kết hợp đăng nhập/đăng ký tài khoản, quản lý người dùng và gửi email hàng loạt với đính kèm file.

## Tính năng chính

- Đăng nhập / đăng ký tài khoản
- Xác minh email bằng mã xác minh
- Gửi email cho nhiều người nhận (To, Cc, Bcc)
- Hỗ trợ gửi kèm file đính kèm
- Nhập danh sách email từ file Excel
- Kiểm tra địa chỉ email trước khi gửi
- Lịch trình gửi email (scheduler)
- Quản trị viên có thể quản lý người dùng và đặt lại mật khẩu
- Lưu lịch sử gửi email

## Công nghệ sử dụng

- C# / WinForms
- .NET Framework 4.7.2
- SQLite để lưu trữ tài khoản người dùng
- EPPlus để đọc file Excel
- SMTP để gửi email

## Cấu trúc thư mục chính

- `Program.cs` - điểm khởi chạy ứng dụng
- `AppConfig.cs` - cấu hình SMTP và thông tin mặc định
- `DatabaseHelper.cs` - xử lý database SQLite
- `EmailHelper.cs` - gửi email xác minh
- `HashHelper.cs` - mã hóa và kiểm tra mật khẩu
- `forms/` - các form giao diện chính:
  - `LoginForm.cs` - đăng nhập / đăng ký
  - `User.cs` - giao diện người dùng gửi email
  - `AdminForm.cs` - giao diện quản trị

## Yêu cầu hệ thống

- Visual Studio 2019 hoặc mới hơn
- .NET Framework 4.7.2
- Kết nối Internet để gửi email qua SMTP

## Hướng dẫn cài đặt

1. Mở file `NetMail.sln`
2. Restore NuGet packages
3. Build solution
4. Chạy project bằng Visual Studio

## Cách sử dụng

### 1. Đăng nhập
- Mặc định có tài khoản admin:
  - Email: `admin@netmail.com`
  - Mật khẩu: `Admin123`

### 2. Gửi email
- Nhập SMTP Server, Port, tài khoản email và mật khẩu
- Điền người nhận ở các ô To / Cc / Bcc
- Nhập tiêu đề và nội dung email
- Chọn file đính kèm nếu cần
- Nhấn Send

### 3. Nhập danh sách email từ Excel
- Chọn file `.xlsx` hoặc `.xls`
- Chọn mục đích nhập vào To / Cc / Bcc

### 4. Lịch trình gửi
- Chọn thời gian gửi và khoảng thời gian lặp
- Nhấn Start Schedule để kích hoạt

## Lưu ý quan trọng

- Thông tin SMTP và tài khoản mặc định đang được định nghĩa trực tiếp trong mã nguồn, nên trước khi dùng cho môi trường thực tế cần cập nhật lại cho phù hợp.
- Khuyến nghị không để mật khẩu và thông tin nhạy cảm trong code khi triển khai sản phẩm chính thức.

## Ghi chú phát triển

Dự án này phù hợp để học cách:
- xây dựng ứng dụng WinForms
- tích hợp SQLite
- gửi email qua SMTP
- xử lý danh sách email và file đính kèm
- thiết kế luồng xác thực người dùng đơn giản
