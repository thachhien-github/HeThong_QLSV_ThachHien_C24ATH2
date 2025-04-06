using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
{
    class TaiKhoan
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public TaiKhoan(string maTaiKhoan, string matKhau, string vaiTro)
        {
            Username = maTaiKhoan;
            Password = matKhau;
            Role = vaiTro;
        }
    }
}
