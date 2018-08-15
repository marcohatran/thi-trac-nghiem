using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class SinhVienDAO
    {

        public int CreateSinhVien(DSinhVien sv)
        {
            string[] name = { "@masv", "@ho", "@ten", "@ngaysinh", "@diachi", "@malop" };
            object[] param = { sv.MaSV, sv.Ho, sv.Ten, String.Format("{0:yyyy-MM-dd}", sv.NgaySinh), sv.DiaChi, sv.MaLop };
            return DBAccess.ExecuteNonQuery("SP_ThemSinhVien", name, param, 6);
        }

        public int UpdateSinhVien(DSinhVien sv)
        {
            string[] name = { "@masv", "@ho", "@ten", "@ngaysinh", "@diachi", "@malop" };
            object[] param = { sv.MaSV, sv.Ho, sv.Ten, String.Format("{0:yyyy-MM-dd}", sv.NgaySinh), sv.DiaChi, sv.MaLop };
            return DBAccess.ExecuteNonQuery("SP_SuaSinhVien", name, param, 6);
        }

        public int RemoveSinhVien(DSinhVien sv)
        {
            string[] name = { "@masv" };
            object[] param = { sv.MaSV };
            return DBAccess.ExecuteNonQuery("SP_XoaSinhVien", name, param, 1);
        }
    }
}
