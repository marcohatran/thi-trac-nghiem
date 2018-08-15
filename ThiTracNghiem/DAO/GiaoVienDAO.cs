using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class GiaoVienDAO
    {

        public int CreateGiaoVien(DGiaoVien giaoVien)
        {
            string[] name = { "@magv", "@ho", "@ten", "@hocvi", "@diachi", "@makh" };
            object[] param = { giaoVien.MaGV, giaoVien.Ho, giaoVien.Ten, giaoVien.HocVi, giaoVien.DiaChi, giaoVien.MaKhoa };
            return DBAccess.ExecuteNonQuery("SP_ThemGiaoVien", name, param, 6);
        }

        public int UpdateGiaoVien(DGiaoVien giaoVien)
        {
            string[] name = { "@magv", "@ho", "@ten", "@hocvi", "@diachi", "@makh" };
            object[] param = { giaoVien.MaGV, giaoVien.Ho, giaoVien.Ten, giaoVien.HocVi, giaoVien.DiaChi, giaoVien.MaKhoa };
            return DBAccess.ExecuteNonQuery("SP_SuaGiaoVien", name, param, 6);
        }

        public int RemoveGiaoVien(DGiaoVien giaoVien)
        {
            string[] name = { "@magv" };
            object[] param = { giaoVien.MaGV };
            return DBAccess.ExecuteNonQuery("SP_XoaGiaoVien", name, param, 1);
        }

        public int RestoreGiaoVien(DGiaoVien giaoVien)
        {
            string[] name = { "@magv", "@ho", "@ten", "@hocvi", "@diachi", "@makh", "@password" };
            object[] param = { giaoVien.MaGV, giaoVien.Ho, giaoVien.Ten, giaoVien.HocVi, giaoVien.DiaChi, giaoVien.MaKhoa,giaoVien.MatKhau };
            return DBAccess.ExecuteNonQuery("SP_PhucHoiGiaoVien", name, param, 7);
        }
    }
}
