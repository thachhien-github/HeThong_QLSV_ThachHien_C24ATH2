# 🎓 HỆ THỐNG QUẢN LÝ SINH VIÊN

> **Tác giả:** Thạch Hiền  
> **Lớp:** C24A.TH2  
> **Giảng viên hướng dẫn:** Tăng Quốc Cường  
> **Trường:** Cao đẳng Giao thông Vận tải TP.HCM  

## 👋 Giới thiệu

Chào mừng bạn đến với dự án **Hệ thống Quản lý Sinh viên** được xây dựng bằng ngôn ngữ lập trình **C#** trên nền tảng **.NET Console**.  
Mục tiêu chính là quản lý thông tin sinh viên, hỗ trợ giảng viên trong việc nhập điểm, cập nhật, phân quyền và bảo mật dữ liệu hiệu quả.

---

## 🚀 Chức năng chính

### 🔐 Đăng nhập và phân quyền
- **Admin:** Quản lý tài khoản giảng viên và xem nhật ký hoạt động.
- **Giảng viên:** Thêm / xóa / sửa / xem sinh viên và điểm số.
- **Sinh viên:** Chỉ xem điểm cá nhân.

### 🧑‍🏫 Quản lý Sinh viên (Giảng viên)
- Nhập thông tin: MSSV, Họ tên, Điểm các môn.
- Chuẩn hóa tên sinh viên.
- Tính điểm trung bình, xếp loại.
- Sắp xếp danh sách sinh viên.
- Tìm kiếm và cập nhật điểm theo MSSV.

### 🎓 Sinh viên
- Đăng nhập bằng MSSV.
- Xem điểm trung bình và từng môn học.

---

## 💻 Công nghệ sử dụng

| Thành phần | Công cụ |
|------------|---------|
| Ngôn ngữ | C# |
| IDE | Visual Studio |
| Thư viện | `System.Collections.Generic`, `System.Text.RegularExpressions`, `System.Globalization` |

---

## 🛠 Hướng dẫn sử dụng hệ thống

### 1. Màn hình đăng nhập
- **1** → Đăng nhập (Nhập username + password)
- **0** → Thoát chương trình

### 2. Menu chức năng sau đăng nhập
- **Admin:** Tài khoản mặc định: `admin` / `admin123`
- **Giảng viên:** Được tạo bởi Admin
- **Sinh viên:** Được tạo tự động khi giảng viên thêm sinh viên

### 3. Quy tắc nhập liệu
- **MSSV:** Gồm 10 chữ số, ví dụ: `2421160052`
- **Họ tên:** Không chứa số, ký tự đặc biệt
- **Điểm:** Nhập từ `0 - 10`, hỗ trợ `.` hoặc `,` làm dấu thập phân

---

## 🌱 Hướng phát triển tương lai
- Lưu dữ liệu vào database thay vì bộ nhớ tạm
- Tăng bảo mật tài khoản Admin
- Phát triển giao diện đồ họa (GUI)
- Thống kê kết quả học tập
- Tăng cường tương tác giữa giảng viên và sinh viên

---

## 📸 Hình ảnh minh họa

> Giao diện đăng nhập  
![login](./images/login.png)

> Menu Admin  
![admin](./images/admin_menu.png)

> Menu Giảng viên  
![teacher](./images/teacher_menu.png)

> Xem điểm sinh viên  
![student](./images/student_view.png)

---

## 🔗 Dự án GitHub

- 🔗 [Link repo dự án](https://github.com/thachhien-github/HeThong_QLSV_ThachHien_C24ATH2)
- 👤 [Profile GitHub](https://github.com/thachhien-github)

---

## 🌐Socials
[![Facebook](https://img.shields.io/badge/Facebook-%231877F2.svg?logo=Facebook&logoColor=white)](https://facebook.com/https://www.facebook.com/Hon.Ty.739326) [![Instagram](https://img.shields.io/badge/Instagram-%23E4405F.svg?logo=Instagram&logoColor=white)](https://instagram.com/https://www.instagram.com/hon.ty.739326) [![TikTok](https://img.shields.io/badge/TikTok-%23000000.svg?logo=TikTok&logoColor=white)](https://tiktok.com/@https://www.tiktok.com/@conbebin?_t=ZS-8vKqIibbEyV&_r=1) 
