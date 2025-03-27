using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeThong_QLSV_ThachHien_C24ATH2
{
    class QLSV
    {
        private List<SinhVien> danhsachSV = new List<SinhVien>();
        private QuanLyTaiKhoan quanLyTaiKhoan;

        public QLSV(QuanLyTaiKhoan qltk)
        {
            this.quanLyTaiKhoan = qltk;
        }


        public void ThemSV(SinhVien sv)
        {
            danhsachSV.Add(sv);
            quanLyTaiKhoan.ThemTaiKhoan(new TaiKhoan(sv.MSSV, sv.MSSV, "SV"));
            Console.WriteLine("Thêm sinh viên thành công!\n");
        }

        public void XoaSV(string mssv)
        {
            SinhVien sv = TimKiem(mssv);
            if (sv != null)
            {
                danhsachSV.Remove(sv);
                quanLyTaiKhoan.XoaTaiKhoan(mssv);
                Console.WriteLine("Xóa thành công!\n");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!\n");
            }
        }

        public SinhVien TimKiem(string mssv)
        {
            for (int i = 0; i < danhsachSV.Count; i++)
            {
                if (danhsachSV[i].MSSV == mssv)
                {
                    return danhsachSV[i]; // Trả về sinh viên nếu tìm thấy
                }
            }
            return null; // Trả về null nếu không tìm thấy
        }

        public void CapNhatSV(string mssv, double diemtkdh, double diemlthdt, double diemltwin)
        {
            SinhVien sv = TimKiem(mssv);
            if (sv != null)
            {
                if (KiemTraDiem(diemtkdh) && KiemTraDiem(diemlthdt) && KiemTraDiem(diemltwin))
                {
                    sv.DiemTKDH = diemtkdh;
                    sv.DiemLTHDT = diemlthdt;
                    sv.DiemLTWin = diemltwin;
                    Console.WriteLine("Cập nhật thành công!\n");
                }
                else
                {
                    Console.WriteLine("Điểm phải nằm trong khoảng 0 - 10!\n");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên!\n");
            }
        }

        public void SapXep()
        {
            for (int i = 0; i < danhsachSV.Count - 1; i++)
            {
                for (int j = 0; j < danhsachSV.Count - i - 1; j++)
                {
                    if (danhsachSV[j].DiemTrungBinh() < danhsachSV[j + 1].DiemTrungBinh())
                    {
                        // Hoán đổi vị trí hai sinh viên nếu điểm trung bình nhỏ hơn
                        SinhVien temp = danhsachSV[j];
                        danhsachSV[j] = danhsachSV[j + 1];
                        danhsachSV[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("Đã sắp xếp danh sách giảm dần theo điểm trung bình!\n");
        }

        public void XuatDanhSach()
        {
            Console.WriteLine("===DANH SÁCH SINH VIÊN===");
            Console.WriteLine("=============================================================");
            Console.WriteLine("|    MSSV    |        Họ Tên        |  ĐTB   |   Xếp Loại   |");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var sv in danhsachSV)
            {
                string mssv = sv.MSSV.PadRight(10).Substring(0, 10);         // MSSV: 10 ký tự
                string hoTen = sv.HoTen.PadRight(20).Substring(0, 20);       // Họ Tên: 20 ký tự
                string diemTB = sv.DiemTrungBinh().ToString("F2").PadRight(6); // Điểm TB: 6 ký tự
                string xepLoai = sv.XepLoai().PadRight(12).Substring(0, 12); // Xếp Loại: 12 ký tự

                Console.WriteLine($"| {mssv} | {hoTen} | {diemTB} | {xepLoai} |");
            }

            Console.WriteLine("=============================================================");
        }

        private bool KiemTraDiem(double diem)
        {
            return diem >= 0 && diem <= 10;
        }
        public List<SinhVien> GetDanhSachSV() //trả về giá trị danh sách sinh viên để MSSV kiểm tra trùng lặp 
        {
            return danhsachSV;
        }
    }
}
