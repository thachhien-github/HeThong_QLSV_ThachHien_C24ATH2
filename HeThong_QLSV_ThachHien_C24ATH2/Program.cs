using HeThong_QLSV_ThachHien_C24ATH2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HeThong_quanLyTaiKhoan_ThachHien_C24ATH2
{
    class Program 
    {
        #region Phương thức chuẩn hóa họ tên
        static string ChuanHoaHoTen(string hoten)
        {
            if (string.IsNullOrWhiteSpace(hoten)) return string.Empty;

            // Xóa khoảng trắng dư thừa, chuyển thành chữ thường
            hoten = Regex.Replace(hoten.Trim(), @"\s+", " ").ToLower();

            // Chuyển đổi chữ cái đầu mỗi từ thành chữ hoa (hỗ trợ tiếng Việt)
            TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
            hoten = textInfo.ToTitleCase(hoten);

            return hoten;
        }
        #endregion

        #region Phương thức nhập MSSV
        static string NhapMSSV(List<SinhVien> danhSachSV) // Kiểm tra nhập MSSV
        {
            int namHienTai = DateTime.Now.Year;
            int namToiDa = namHienTai - 2000;

            while (true)
            {
                Console.Write("MSSV: ");
                string mssv = Console.ReadLine();

                // Kiểm tra MSSV có đúng 10 chữ số không
                if (string.IsNullOrWhiteSpace(mssv) || mssv.Length != 10 || !long.TryParse(mssv, out _))
                {
                    Console.WriteLine("MSSV phải có đúng 10 chữ số!");
                    continue;
                }

                // Lấy 2 ký tự đầu và kiểm tra khóa học hợp lệ
                if (!int.TryParse(mssv.Substring(0, 2), out int khoaHoc) || khoaHoc < 0 || khoaHoc > namToiDa)
                {
                    Console.WriteLine($"Hai số đầu của MSSV phải nằm trong khoảng 00 - {namToiDa} (tương ứng từ năm 2000 đến {namHienTai})!");
                    continue;
                }

                // Tính năm tốt nghiệp
                int namNhapHoc = 2000 + khoaHoc;
                int namTotNghiep = namNhapHoc + 5; // Hạn đào tạo tối đa 5 năm

                // Kiểm tra nếu đã quá hạn đào tạo
                if (namHienTai > namTotNghiep)
                {
                    Console.WriteLine($"Sinh viên khóa {namNhapHoc} đã quá hạn đào tạo! (Hạn đào tạo: {namTotNghiep})");
                    continue;
                }

                // Kiểm tra trùng MSSV trong danh sách
                if (danhSachSV.Any(sv => sv.MSSV == mssv))
                {
                    Console.WriteLine("MSSV đã tồn tại! Vui lòng nhập MSSV khác.");
                    continue;
                }

                // Nếu tất cả đều hợp lệ, trả về MSSV
                return mssv;
            }
        }
        #endregion

        #region Phương thức nhập Họ Tên
        static string NhapHoTen() //kiểm tra lỗi nhập Họ tên
        {
            while (true)
            {
                Console.Write("Họ tên: ");
                string hoten = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(hoten) || hoten.Any(char.IsDigit) || Regex.IsMatch(hoten, @"[^a-zA-ZÀ-Ỹà-ỹ\s]"))
                {
                    Console.WriteLine("Họ tên không được chứa số, ký tự đặc biệt hoặc bỏ trống! Vui lòng nhập lại.");
                    continue;
                }

                return hoten;
            }
        }
        #endregion

        #region Phương thức nhập điểm
        static double NhapDiem(string nhap) //kiểm tra lỗi nhập điểm
        {
            double diem;
            while (true)
            {
                Console.Write(nhap);
                string input = Console.ReadLine();

                // Kiểm tra nếu input là số và nằm trong khoảng hợp lệ
                if (double.TryParse(input, out diem) && diem >= 0 && diem <= 10)
                {
                    return diem;
                }

                // Hiển thị lỗi nếu nhập sai
                Console.WriteLine("Lỗi: Vui lòng nhập một số từ 0 đến 10.");
            }
        }
        #endregion

        #region Ẩn mật khẩu
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
        #endregion

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            QuanLyTaiKhoan qltk = new QuanLyTaiKhoan();
            QLSV qlsv = new QLSV(qltk);

            while (true)
            {
                Console.WriteLine("\n╔═════════════════════════════════════════╗");
                Console.WriteLine("║       HỆ THỐNG QUẢN LÝ SINH VIÊN        ║");
                Console.WriteLine("╠════════════ >> ĐĂNG NHẬP << ════════════╣");
                Console.WriteLine("║  1. Đăng nhập                           ║");
                Console.WriteLine("║  0. Thoát                               ║");
                Console.WriteLine("╚═════════════════════════════════════════╝");
                Console.WriteLine("* Chú ý: Nếu là sinh viên, vui lòng đăng nhập với tên đăng nhập và mật khẩu là MSSV!");
                Console.Write("Chọn chức năng: ");
                string chon = Console.ReadLine();

                if (chon == "0") return;

                if (chon == "1")
                {
                    Console.Write("Tên tài khoản: ");
                    string username = Console.ReadLine();
                    Console.Write("Mật khẩu: ");
                    string password = ReadPassword();

                    TaiKhoan tk = qltk.TimKiemTaiKhoan(username);
                    if (tk != null && tk.Password == password)
                    {
                        qltk.DangNhapTaiKhoan(tk.Username, tk.Password);
                        switch (tk.Role)
                        {
                            case "Admin": MenuAdmin(qltk); break;
                            case "GiangVien": MenuGiangVien(qlsv, qltk); break;
                            case "SinhVien": MenuSinhVien(qlsv, qltk, username); break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Sai tên đăng nhập hoặc mật khẩu!");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Lựa chọn không hợp lệ");
                }
            }
        }
        #region MenuAdmin
        static void MenuAdmin(QuanLyTaiKhoan quanLyTaiKhoan)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔═════════════════════════════════════════════════╗");
                Console.WriteLine("║           HỆ THỐNG QUẢN LÝ SINH VIÊN            ║");
                Console.WriteLine("╠══════════════════ >> ADMIN << ══════════════════╣");
                Console.WriteLine("║       === CHỨC NĂNG QUẢN LÝ TÀI KHOẢN ===       ║");
                Console.WriteLine("║  1. Xem danh sách tài khoản                     ║");
                Console.WriteLine("║  2. Xem lịch sử đăng nhập                       ║");
                Console.WriteLine("║  3. Xem nhật ký hoạt động                       ║");
                Console.WriteLine("║  4. Thêm tài khoản Giảng viên                   ║");
                Console.WriteLine("║  5. Xóa tài khoản                               ║");
                Console.WriteLine("║  0. Đăng xuất                                   ║");
                Console.WriteLine("╚═════════════════════════════════════════════════╝");
                Console.Write("Chọn chức năng: ");
                string chon = Console.ReadLine();

                switch (chon)
                {
                    case "1":
                        Console.Clear();
                        quanLyTaiKhoan.XemDanhSachTaiKhoan();
                        break;

                    case "2":
                        Console.Clear();
                        quanLyTaiKhoan.XemLichSuDangNhap();
                        break;

                    case "3":
                        Console.Clear();
                        quanLyTaiKhoan.XemNhatKyHoatDong();
                        break;

                    case "4":
                        // Thêm tài khoản Giảng viên
                        Console.Clear();
                        Console.WriteLine("\n===== THÊM TÀI KHOẢN GIẢNG VIÊN =====");
                        Console.Write("Nhập tên tài khoản (username): ");
                        string username = Console.ReadLine();
                        Console.Write("Nhập mật khẩu: ");
                        string password = Console.ReadLine();
                        string role = "GiangVien";  // Mặc định vai trò là Giảng viên
                        TaiKhoan newAccount = new TaiKhoan(username, password, role);
                        quanLyTaiKhoan.ThemTaiKhoan(newAccount);
                        Console.WriteLine("Tài khoản giảng viên đã được thêm thành công!");
                        break;

                    case "5":
                        // Xóa tài khoản
                        Console.Clear();
                        Console.WriteLine("\n===== XÓA TÀI KHOẢN =====");
                        Console.Write("Nhập tên tài khoản cần xóa: ");
                        string usernameToDelete = Console.ReadLine();
                        quanLyTaiKhoan.XoaTaiKhoan(usernameToDelete);
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("Đăng xuất thành công! Quay lại màn hình đăng nhập...");
                        Thread.Sleep(1000); // Hiển thị thông báo trước khi quay lại đăng nhập
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Lựa chọn không hợp lệ");
                        break;
                }

                // Thêm để dừng console và kiểm tra kết quả
                Console.WriteLine("\nNhấn bất kỳ phím nào để quay lại menu chính...");
                Console.ReadKey();
            }
        }
        #endregion

        #region MenuGiangVien
        static void MenuGiangVien(QLSV qlsv, QuanLyTaiKhoan quanLyTaiKhoan)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔═════════════════════════════════════════════════╗");
                Console.WriteLine("║           HỆ THỐNG QUẢN LÝ SINH VIÊN            ║");
                Console.WriteLine("╠═══════════════ >> GIẢNG VIÊN << ════════════════╣");
                Console.WriteLine("║       === CHỨC NĂNG QUẢN LÝ SINH VIÊN ===       ║");
                Console.WriteLine("║  1. Thêm sinh viên                              ║");
                Console.WriteLine("║  2. Xóa sinh viên                               ║");
                Console.WriteLine("║  3. Tìm kiếm sinh viên                          ║");
                Console.WriteLine("║  4. Cập nhật sinh viên                          ║");
                Console.WriteLine("║  5. Sắp xếp giảm dần theo điểm trung bình       ║");
                Console.WriteLine("║  6. Hiển thị danh sách sinh viên                ║");
                Console.WriteLine("║  0. Đăng xuất                                   ║");
                Console.WriteLine("╚═════════════════════════════════════════════════╝");
                Console.Write("Chọn chức năng: ");
                string chon = Console.ReadLine();

                switch (chon)
                {
                    case "1":
                        Console.WriteLine("Mời nhập!");
                        string mssv = NhapMSSV(qlsv.GetDanhSachSV());
                        string hoten = NhapHoTen(); hoten = ChuanHoaHoTen(hoten);
                        double diemTKDH = NhapDiem("Điểm TKĐH: ");
                        double diemLTHDT = NhapDiem("Điểm LTHĐT: ");
                        double diemLTWin = NhapDiem("Điểm LTWin: ");
                        Console.Clear();

                        // Tạo sinh viên mới
                        SinhVien sv = new SinhVien(mssv, hoten)
                        {
                            DiemTKDH = diemTKDH,
                            DiemLTHDT = diemLTHDT,
                            DiemLTWin = diemLTWin
                        };

                        // Thêm sinh viên vào danh sách
                        qlsv.ThemSV(sv);

                        Console.WriteLine("=================================================================");
                        Console.WriteLine("|    MSSV     |       Họ Tên       |  TKĐH  |  LTHĐT  |  LTWin  |");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Console.WriteLine($"| {mssv.PadRight(11).Substring(0, 11)} " +
                            $"| {hoten.PadRight(18).Substring(0, 18)} " +
                            $"|{diemTKDH,6:F2}  " +
                            $"| {diemLTHDT,6:F2}  " +
                            $"| {diemLTWin,6:F2}  |");
                        Console.WriteLine("=================================================================");
                        break;

                    case "2":
                        Console.Clear();
                        Console.Write("Nhập MSSV cần xóa: ");
                        string maxoa = Console.ReadLine();
                        qlsv.XoaSV(maxoa);
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("Nhập MSSV cần tìm: ");
                        string matimkiem = Console.ReadLine();
                        SinhVien svtimkiem = qlsv.TimKiem(matimkiem);

                        if (svtimkiem != null)
                        {
                            Console.WriteLine("KẾT QUẢ TÌM KIẾM:");
                            Console.WriteLine("=================================================================");
                            Console.WriteLine("|    MSSV     |       Họ Tên       |  TKĐH  |  LTHĐT  |  LTWin  |");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Console.WriteLine($"| {svtimkiem.MSSV.PadRight(11).Substring(0, 11)} " +
                                $"| {svtimkiem.HoTen.PadRight(18).Substring(0, 18)} " +
                                $"|{svtimkiem.DiemTKDH,6:F2}  " +
                                $"| {svtimkiem.DiemLTHDT,6:F2}  " +
                                $"| {svtimkiem.DiemLTWin,6:F2}  |");
                            Console.WriteLine("=================================================================");
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sinh viên!");
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.Write("Nhập MSSV cần cập nhật: ");
                        string macapnhat = Console.ReadLine();

                        // Tìm sinh viên trước khi cập nhật
                        SinhVien svcapnhat = qlsv.TimKiem(macapnhat);
                        if (svcapnhat == null)
                        {
                            Console.WriteLine("Không tìm thấy sinh viên!\n");
                            break;
                        }

                        // Nếu tìm thấy, nhập điểm mới
                        Console.WriteLine($"Họ tên: {svcapnhat.HoTen}");
                        double diemTKDHmoi = NhapDiem("Nhập điểm Thiết kế Đồ họa: ");
                        double diemLTHDTmoi = NhapDiem("Nhập điểm Lập trình Hướng đối tượng: ");
                        double diemLTWinmoi = NhapDiem("Nhập điểm Lập trình Windows: ");

                        // Cập nhật thông tin
                        qlsv.CapNhatSV(macapnhat, diemTKDHmoi, diemLTHDTmoi, diemLTWinmoi);

                        Console.Clear();
                        Console.WriteLine("CẬP NHẬT THÀNH CÔNG!");
                        Console.WriteLine("=================================================================");
                        Console.WriteLine("|    MSSV     |       Họ Tên       |  TKĐH  |  LTHĐT  |  LTWin  |");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Console.WriteLine($"| {svcapnhat.MSSV.PadRight(11).Substring(0, 11)} " +
                            $"| {svcapnhat.HoTen.PadRight(18).Substring(0, 18)} " +
                            $"|{diemTKDHmoi,6:F2}  " +
                            $"| {diemLTHDTmoi,6:F2}  " +
                            $"| {diemLTWinmoi,6:F2}  |");
                        Console.WriteLine("=================================================================");
                        break;

                    case "5":
                        Console.Clear();
                        qlsv.SapXep();
                        qlsv.XuatDanhSach();
                        break;

                    case "6":
                        Console.Clear();
                        qlsv.XuatDanhSach();
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("Đăng xuất thành công! Quay lại màn hình đăng nhập...");
                        Thread.Sleep(1000); // Hiển thị thông báo trước khi quay lại đăng nhập
                        return; // Thoát khỏi menu, quay lại màn hình đăng nhập

                    default:
                        Console.Clear();
                        Console.WriteLine("Lỗi! Vui lòng nhập số từ 0 đến 6.");
                        break;
                }
                // Thêm để dừng console và kiểm tra kết quả
                Console.WriteLine("\nNhấn bất kỳ phím nào để quay lại menu chính...");
                Console.ReadKey();
            }
        }
        #endregion

        #region MenuSinhVien
        static void MenuSinhVien(QLSV qlsv,QuanLyTaiKhoan quanLyTaiKhoan, string username)
        {
            SinhVien sv = qlsv.TimKiem(username);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔═════════════════════════════════════════════╗");
                Console.WriteLine("║         HỆ THỐNG QUẢN LÝ SINH VIÊN          ║");
                Console.WriteLine("╠═════════════ >> SINH VIÊN << ═══════════════╣");
                Console.WriteLine("║       === CHỨC NĂNG TRA CỨU ĐIỂM ===        ║");
                Console.WriteLine("║  1. Xem điểm trung bình                     ║");
                Console.WriteLine("║  2. Xem điểm học phần                       ║");
                Console.WriteLine("║  0. Đăng xuất                               ║");
                Console.WriteLine("╚═════════════════════════════════════════════╝");
                Console.Write("Chọn chức năng: ");

                string chon = Console.ReadLine();

                switch (chon)
                {
                    case "1":
                        if (sv != null)
                        {
                            Console.Clear();
                            Console.WriteLine("=========================================================");
                            Console.WriteLine("|       Họ Tên         |   ĐTB   |     Xếp Loại         |");
                            Console.WriteLine("---------------------------------------------------------");
                            Console.WriteLine($"| {sv.HoTen.PadRight(20).Substring(0, 20)} " +
                                              $"|  {sv.DiemTrungBinh().ToString("F2").PadRight(5)}  " +
                                              $"| {sv.XepLoai().PadRight(20).Substring(0, 20)} |");
                            Console.WriteLine("=========================================================");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Không tìm thấy sinh viên!");
                        }
                        break;

                    case "2":
                        if (sv != null)
                        {
                            Console.Clear();
                            Console.WriteLine("=============================================================");
                            Console.WriteLine("|    MSSV    |        Họ Tên        | TKĐH  | LTHĐT | LTWin |");
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine($"| {sv.MSSV.PadRight(10)} | {sv.HoTen.PadRight(20).Substring(0, 20)} " +
                                              $"| {sv.DiemTKDH,5:F2} | {sv.DiemLTHDT,5:F2} | {sv.DiemLTWin,5:F2} |");
                            Console.WriteLine("=============================================================");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Không tìm thấy sinh viên!");
                        }
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("Đăng xuất thành công! Quay lại màn hình đăng nhập...");
                        Thread.Sleep(1000); // Hiển thị thông báo trước khi quay lại đăng nhập
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Lỗi! Vui lòng nhập số từ 0 đến 2.");
                        break;
                }
                // Thêm để dừng console và kiểm tra kết quả
                Console.WriteLine("\nNhấn bất kỳ phím nào để quay lại menu chính...");
                Console.ReadKey();
            }
        }
        #endregion
    }
}
