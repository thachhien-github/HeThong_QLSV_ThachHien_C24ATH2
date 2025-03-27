using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
{
    abstract class NguoiDung
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public NguoiDung(string username, string password)
        {
            Username = username;
            Password = password;
        }

        // Phương thức ảo để đăng nhập
        public virtual void DangNhap()
        {
            Console.WriteLine($"{Username} đang đăng nhập...");
        }
    }
}
