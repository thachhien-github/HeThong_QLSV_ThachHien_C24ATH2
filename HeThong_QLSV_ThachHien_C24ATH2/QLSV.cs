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

        // Thêm sinh viên vào danh sách
        public void ThemSV(SinhVien sv)
        {
            if (TimKiem(sv.MSSV) != null)
            {
                Console.WriteLine("❌ MSSV đã tồn tại. Vui lòng nhập lại!\n");
                return;
            }

            danhsachSV.Add(sv);
            quanLyTaiKhoan.ThemTaiKhoan(new TaiKhoan(sv.MSSV, sv.MSSV, "SinhVien"));
            Console.WriteLine("✅ Thêm sinh viên thành công!\n");
        }

        // Xóa sinh viên theo MSSV
        public void XoaSV(string mssv)
        {
            SinhVien sv = TimKiem(mssv);
            if (sv != null)
            {
                // Hiển thị thông báo xác nhận xóa tài khoản và sinh viên
                Console.WriteLine($"Bạn có chắc chắn muốn xóa sinh viên có MSSV {mssv} và tài khoản liên quan không? (y/n): ");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "y")
                {
                    // Xóa tài khoản và sinh viên
                    quanLyTaiKhoan.XoaTaiKhoan(mssv);  // Xóa tài khoản
                    danhsachSV.Remove(sv);  // Xóa sinh viên khỏi danh sách
                    Console.WriteLine("✅ Đã xóa sinh viên và tài khoản thành công.\n");
                }
                else
                {
                    Console.WriteLine("❌ Đã hủy việc xóa sinh viên và tài khoản.\n");
                }
            }
            else
            {
                Console.WriteLine("❌ Không tìm thấy sinh viên có MSSV này.\n");
            }
        }

        // Tìm sinh viên theo MSSV
        public SinhVien TimKiem(string mssv)
        {
            return danhsachSV.Find(sv => sv.MSSV == mssv);
        }

        // Cập nhật điểm cho sinh viên
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
                    Console.WriteLine("✅ Cập nhật điểm thành công!\n");
                }
                else
                {
                    Console.WriteLine("❌ Điểm phải nằm trong khoảng 0 - 10.\n");
                }
            }
            else
            {
                Console.WriteLine("❌ Không tìm thấy sinh viên cần cập nhật.\n");
            }
        }

        // Sắp xếp danh sách sinh viên giảm dần theo điểm trung bình
        public void SapXep()
        {
            danhsachSV.Sort((sv1, sv2) => sv2.DiemTrungBinh().CompareTo(sv1.DiemTrungBinh()));
            Console.WriteLine("📊 Danh sách đã được sắp xếp giảm dần theo điểm trung bình.\n");
        }

        // Xuất danh sách sinh viên
        public void XuatDanhSach()
        {
            if (danhsachSV.Count == 0)
            {
                Console.WriteLine("📭 Danh sách sinh viên trống.\n");
                return;
            }

            Console.WriteLine("=== DANH SÁCH SINH VIÊN ===");
            Console.WriteLine("=============================================================");
            Console.WriteLine("|    MSSV    |        Họ Tên        |  ĐTB   |   Xếp Loại   |");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var sv in danhsachSV)
            {
                string mssv = sv.MSSV.PadRight(10).Substring(0, 10);
                string hoTen = sv.HoTen.PadRight(20).Substring(0, 20);
                string diemTB = sv.DiemTrungBinh().ToString("F2").PadRight(6);
                string xepLoai = sv.XepLoai().PadRight(12).Substring(0, 12);

                Console.WriteLine($"| {mssv} | {hoTen} | {diemTB} | {xepLoai} |");
            }

            Console.WriteLine("=============================================================\n");
        }

        // Kiểm tra điểm hợp lệ
        private bool KiemTraDiem(double diem)
        {
            return diem >= 0 && diem <= 10;
        }

        // Trả về danh sách sinh viên
        public List<SinhVien> GetDanhSachSV()
        {
            return danhsachSV;
        }
    }
}
