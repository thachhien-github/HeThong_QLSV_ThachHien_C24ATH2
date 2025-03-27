using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
{
    class SinhVien : NguoiDung
    {
        public string MSSV { get; set; }
        public string HoTen { get; set; }
        public double DiemTKDH { get; set; }  // Điểm môn Thiết kế Đồ họa
        public double DiemLTHDT { get; set; } // Điểm môn Lập trình Hướng đối tượng
        public double DiemLTWin { get; set; } // Điểm môn Lập trình Windows

        public SinhVien(string mssv, string hoten)
        : base(mssv, mssv)  // Dùng MSSV làm cả username và password cho sinh viên
        {
            MSSV = mssv;
            HoTen = hoten;
        }

        public double DiemTrungBinh()
        {
            return (DiemTKDH + DiemLTHDT + DiemLTWin) / 3;
        }

        public string XepLoai()
        {
            double dtb = DiemTrungBinh();
            if (dtb >= 8.5) return "Giỏi";
            if (dtb >= 6.5) return "Khá";
            if (dtb >= 5) return "Trung Bình";
            return "Yếu";
        }

        public override void DangNhap()
        {
            Console.WriteLine($"Sinh viên {HoTen} ({MSSV}) đã đăng nhập.");
        }
    }
}
