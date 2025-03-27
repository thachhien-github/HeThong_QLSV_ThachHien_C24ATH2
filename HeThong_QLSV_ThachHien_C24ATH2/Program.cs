using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
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

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            QuanLyTaiKhoan qltk = new QuanLyTaiKhoan();
            QLSV qlsv = new QLSV(qltk);

            while (true)
            {
                TaiKhoan nguoiDung = null;
                while (nguoiDung == null)
                {
                    Console.WriteLine("\n    HỆ THỐNG QUẢN LÝ SINH VIÊN");
                    Console.WriteLine("\n=========== ĐĂNG NHẬP ===========");
                    Console.WriteLine("| 1. Đăng nhập                  |");
                    Console.WriteLine("| 2. Đăng ký tài khoản          |");
                    Console.WriteLine("| 0. Thoát                      |");
                    Console.WriteLine("=================================");
                    Console.WriteLine("*Chú ý: nếu là sinh viên vui lòng đăng nhập với tên đăng nhập và mật khẩu là MSSV!");
                    Console.Write("Chọn chức năng: ");
                    string chon = Console.ReadLine();

                    switch (chon)
                    {
                        case "1":
                            nguoiDung = qltk.DangNhap();
                            break;
                        case "2":
                            qltk.DangKy();
                            break;
                        case "0":
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn lại.");
                            break;
                    }
                }

                if (nguoiDung.Role == "GV")
                {
                    MenuGiangVien(qlsv, qltk);
                }
                else if (nguoiDung.Role == "SV")
                {
                    MenuSinhVien(qlsv, nguoiDung.Username);
                    nguoiDung = null; // Reset lại để quay về màn hình đăng nhập
                }
            }
        }

        #region MenuGiangVien
        static void MenuGiangVien(QLSV qlsv, QuanLyTaiKhoan qltk)
        {
            while (true)
            {
                Console.WriteLine("\n      HỆ THỐNG QUẢN LÝ SINH VIÊN");
                Console.WriteLine("\n========== CHỨC NĂNG QLSV ===========");
                Console.WriteLine("| 1. Thêm sinh viên                 |");
                Console.WriteLine("| 2. Xóa sinh viên                  |");
                Console.WriteLine("| 3. Tìm kiếm sinh viên             |");
                Console.WriteLine("| 4. Cập nhật sinh viên             |");
                Console.WriteLine("| 5. Sắp xếp giảm dần theo điểm TB  |");
                Console.WriteLine("| 6. Hiển thị danh sách sinh viên   |");
                Console.WriteLine("| 7. Quản lý tài khoản              |");
                Console.WriteLine("| 0. Đăng xuất                      |");
                Console.WriteLine("=====================================");
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
                        qlsv.ThemSV(new SinhVien(mssv, hoten)
                        {
                            DiemTKDH = diemTKDH,
                            DiemLTHDT = diemLTHDT,
                            DiemLTWin = diemLTWin
                        });

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

                    case "7":
                        Console.Clear();
                        qltk.XemDanhSachTaiKhoan();
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
            }
        }
        #endregion

        #region MenuSinhVien
        static void MenuSinhVien(QLSV qlsv, string username)
        {
            while (true)
            {
                Console.WriteLine("\n===== TRA CỨU ĐIỂM HỌC PHẦN =====");
                Console.WriteLine("| 1. Xem điểm trung bình        |");
                Console.WriteLine("| 2. Xem điểm học phần          |");
                Console.WriteLine("| 0. Đăng xuất                  |");
                Console.WriteLine("=================================");
                Console.Write("Chọn chức năng: ");

                string chon = Console.ReadLine();
                SinhVien sv = qlsv.TimKiem(username);
                switch (chon)
                {
                    case "1":
                        if (sv != null)
                        {
                            Console.Clear();
                            Console.WriteLine("==========================================");
                            Console.WriteLine("|     Họ Tên       |  ĐTB   |  Xếp Loại  |");
                            Console.WriteLine("------------------------------------------");
                            Console.WriteLine($"| {sv.HoTen.PadRight(16).Substring(0, 16)} |{sv.DiemTrungBinh(),6:F2}  | {sv.XepLoai().PadRight(10).Substring(0, 10)} |");
                            Console.WriteLine("==========================================");
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
                            Console.WriteLine("=================================================================");
                            Console.WriteLine("|    MSSV     |       Họ Tên       |  TKĐH  |  LTHĐT  |  LTWin  |");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Console.WriteLine($"| {sv.MSSV.PadRight(11).Substring(0, 11)} " +
                                $"| {sv.HoTen.PadRight(18).Substring(0, 18)} " +
                                $"|{sv.DiemTKDH,6:F2}  " +
                                $"| {sv.DiemLTHDT,6:F2}  " +
                                $"| {sv.DiemLTWin,6:F2}  |");
                            Console.WriteLine("=================================================================");
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
                        return; // Thoát khỏi menu, quay lại màn hình đăng nhập

                    default:
                        Console.Clear();
                        Console.WriteLine("Lỗi! Vui lòng nhập số từ 0 đến 2.");
                        break;
                }
            }
        }
        #endregion
    }
}
