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

        public TaiKhoan DangNhap()
        {
            Console.Write("Tên đăng nhập: ");
            string username = Console.ReadLine();
            Console.Write("Mật khẩu: ");
            string password = Console.ReadLine();

            foreach (TaiKhoan tk in danhSachTaiKhoan)
            {
                if (tk.Username == username && tk.Password == password)
                {
                    Console.Clear();
                    tk.DangNhap();
                    return tk;
                }
            }
            Console.Clear();
            Console.WriteLine("\nSai tên đăng nhập hoặc mật khẩu!");
            return null;
        }

        public void DangKy()
        {
            string username;
            do
            {
                Console.Write("Nhập tên đăng nhập: ");
                username = Console.ReadLine();

                if (!KiemTraTenHopLe(username))
                {
                    Console.WriteLine("Tên đăng nhập không chứa khoảng trắng và ký tự đặc biệt!");
                }
                else if (KiemTraTaiKhoanTonTai(username))
                {
                    Console.WriteLine("Tên đăng nhập đã tồn tại!");
                }
                else
                {
                    break;
                }
            } while (true);

            string password;
            do
            {
                Console.Write("Nhập mật khẩu: ");
                password = Console.ReadLine();

                if (password.Length < 8)
                {
                    Console.WriteLine("Mật khẩu chứa ít nhất 8 ký tự.");
                }
            } while (password.Length < 8);

            string role;
            do
            {
                Console.Write("Chọn vai trò (GV/SV): ");
                role = Console.ReadLine().ToUpper();

                if (role != "GV" && role != "SV")
                {
                    Console.WriteLine("Vai trò không hợp lệ! Chỉ nhận GV hoặc SV.");
                }
                else
                {
                    break;
                }

            } while (true);

            if (role == "GV")
            {
                Console.Write("Nhập mã xác nhận dành cho giảng viên: ");
                string maXacNhan = NhapMaXacNhan();

                if (maXacNhan != "GV123")
                {
                    Console.Clear();
                    Console.WriteLine("Mã xác nhận không đúng! Đăng ký thất bại.");
                    return;
                }
            }

            danhSachTaiKhoan.Add(new TaiKhoan(username, password, role));
            Console.Clear();
            Console.WriteLine("Đăng ký thành công!");
        }

        // Kiểm tra tên đăng nhập hợp lệ (không chứa khoảng trắng & ký tự đặc biệt)
        private bool KiemTraTenHopLe(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c)) // Không phải chữ hoặc số
                {
                    return false;
                }
            }
            return true;
        }

        // Kiểm tra tài khoản đã tồn tại chưa
        private bool KiemTraTaiKhoanTonTai(string username)
        {
            foreach (TaiKhoan tk in danhSachTaiKhoan)
            {
                if (tk.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        // Nhập mã xác nhận ẩn dạng '*'
        private string NhapMaXacNhan()
        {
            string maXacNhan = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && maXacNhan.Length > 0)
                {
                    maXacNhan = maXacNhan.Substring(0, maXacNhan.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    maXacNhan += key.KeyChar;
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return maXacNhan;
        }

        public void ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            // Kiểm tra xem tài khoản đã tồn tại chưa
            foreach (TaiKhoan tk in danhSachTaiKhoan)
            {
                if (tk.Username == taiKhoan.Username)
                {
                    return; // Nếu tồn tại, thoát phương thức mà không thêm
                }
            }

            // Nếu không tìm thấy, thêm tài khoản mới
            danhSachTaiKhoan.Add(taiKhoan);
        }

        public void XoaTaiKhoan(string username)
        {
            // Duyệt danh sách để tìm tài khoản cần xóa
            foreach (TaiKhoan tk in danhSachTaiKhoan)
            {
                if (tk.Username == username)
                {
                    danhSachTaiKhoan.Remove(tk);
                    return; // Xóa xong thì thoát phương thức
                }
            }
        }
        public void XemDanhSachTaiKhoan()
        {
            Console.WriteLine("\n=== DANH SÁCH TÀI KHOẢN ===");
            Console.WriteLine("==================================================");
            Console.WriteLine($"| {"Username",-15} | {"Password",-15} | {"Role",-10} |");
            Console.WriteLine("--------------------------------------------------");

            foreach (var tk in danhSachTaiKhoan)
            {
                Console.WriteLine($"| {tk.Username,-15} | {tk.Password,-15} | {tk.Role,-10} |");
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}
