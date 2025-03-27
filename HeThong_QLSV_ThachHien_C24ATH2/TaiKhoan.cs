using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
{
    class TaiKhoan : NguoiDung
    {
        public string Role { get; set; }

        public TaiKhoan(string username, string password, string role)
            : base(username, password)
        {
            Role = role;
        }

        // Ghi đè phương thức đăng nhập
        public override void DangNhap()
        {
            Console.WriteLine($"{Username} ({Role}) đã đăng nhập thành công.");
        }
    }
}
