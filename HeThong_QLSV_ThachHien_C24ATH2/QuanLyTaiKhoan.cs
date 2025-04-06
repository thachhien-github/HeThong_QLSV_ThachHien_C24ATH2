using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
{
    class QuanLyTaiKhoan
    {
        private List<TaiKhoan> danhSachTaiKhoan = new List<TaiKhoan>();
        private List<string> lichSuDangNhap = new List<string>();
        private List<string> nhatKyHoatDong = new List<string>();

        public QuanLyTaiKhoan()
        {
            // Thêm tài khoản Admin mặc định
            danhSachTaiKhoan.Add(new TaiKhoan("admin", "admin123", "Admin"));
        }

        public void ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            // Kiểm tra xem tài khoản đã tồn tại chưa
            if (danhSachTaiKhoan.Any(tk => tk.Username == taiKhoan.Username))
            {
                Console.WriteLine("Tài khoản đã tồn tại. Vui lòng chọn tên tài khoản khác.");
                return;
            }

            danhSachTaiKhoan.Add(taiKhoan);
            GhiNhanHoatDong($"Đã thêm tài khoản: {taiKhoan.Username} ({taiKhoan.Role})");
        }

        public void XoaTaiKhoan(string Username)
        {
            TaiKhoan taiKhoan = danhSachTaiKhoan.Find(tk => tk.Username == Username);

            if (taiKhoan != null)
            {
                // Hiển thị thông báo xác nhận xóa tài khoản
                Console.WriteLine($"Bạn có chắc chắn muốn xóa tài khoản '{Username}' không? (y/n): ");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "y")
                {
                    danhSachTaiKhoan.Remove(taiKhoan);
                    GhiNhanHoatDong($"Đã xóa tài khoản: {Username}");
                    Console.WriteLine("✅ Đã xóa tài khoản thành công.\n");
                }
                else
                {
                    Console.WriteLine("❌ Đã hủy việc xóa tài khoản.\n");
                }
            }
            else
            {
                Console.WriteLine("❌ Không tìm thấy tài khoản này.\n");
            }
        }

        public TaiKhoan TimKiemTaiKhoan(string Username)
        {
            return danhSachTaiKhoan.Find(tk => tk.Username == Username);
        }

        public void XemDanhSachTaiKhoan()
        {
            Console.WriteLine("\n=== DANH SÁCH TÀI KHOẢN ===");
            if (danhSachTaiKhoan.Count == 0)
            {
                Console.WriteLine("Chưa có tài khoản nào.");
            }
            else
            {
                Console.WriteLine("============================================================");
                Console.WriteLine($"| {"Username",-20} | {"Password",-20} | {"Role",-10} |");
                Console.WriteLine("------------------------------------------------------------");

                foreach (var tk in danhSachTaiKhoan)
                {
                    Console.WriteLine($"| {tk.Username,-20} | {tk.Password,-20} | {tk.Role,-10} |");
                    Console.WriteLine("------------------------------------------------------------");
                }
            }
        }

        public void GhiNhanDangNhap(string username)
        {
            string log = $"[{DateTime.Now}] Tài khoản '{username}' đã đăng nhập.";
            lichSuDangNhap.Add(log);
        }

        public void XemLichSuDangNhap()
        {
            Console.WriteLine("\n=== LỊCH SỬ ĐĂNG NHẬP ===");
            Console.WriteLine();
            if (lichSuDangNhap.Count == 0)
            {
                Console.WriteLine("Chưa có lịch sử đăng nhập nào.");
            }
            else
            {
                foreach (var log in lichSuDangNhap)
                {
                    Console.WriteLine(log);
                }
            }
        }

        public void GhiNhanHoatDong(string noiDung)
        {
            string log = $"[{DateTime.Now}] {noiDung}";
            nhatKyHoatDong.Add(log);
        }

        public void XemNhatKyHoatDong()
        {
            Console.WriteLine("\n=== NHẬT KÝ HOẠT ĐỘNG ===");
            Console.WriteLine();
            if (nhatKyHoatDong.Count == 0)
            {
                Console.WriteLine("Chưa có hoạt động nào.");
            }
            else
            {
                foreach (var log in nhatKyHoatDong)
                {
                    Console.WriteLine(log);
                }
            }
        }

        // Đăng nhập tài khoản và ghi nhận lịch sử đăng nhập
        public bool DangNhapTaiKhoan(string username, string password)
        {
            TaiKhoan taiKhoan = danhSachTaiKhoan.Find(tk =>
                tk.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (taiKhoan != null && taiKhoan.Password == password)
            {
                Console.WriteLine($"✅ Đăng nhập thành công với tài khoản {username}.\n");
                GhiNhanDangNhap(username); // Ghi nhận lịch sử đăng nhập
                return true;
            }
            else
            {
                Console.WriteLine("❌ Đăng nhập thất bại! Tài khoản hoặc mật khẩu không đúng.\n");
                return false;
            }
        }
    }
}
