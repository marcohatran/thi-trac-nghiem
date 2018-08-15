using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{

    class KhoaDAO
    {

        public int CreateKhoa(DKhoa khoa)
        {
            string[] name = { "@makh", "@tenkh", "@macs" };
            object[] param = { khoa.MaKhoa, khoa.TenKhoa, khoa.MaCS };
            return DBAccess.ExecuteNonQuery("SP_ThemKhoa", name, param, 3);
        }

        public int UpdateKhoa(DKhoa khoa)
        {
            string[] name = { "@makh", "@tenkh", "@macs" };
            object[] param = { khoa.MaKhoa, khoa.TenKhoa, khoa.MaCS };
            return DBAccess.ExecuteNonQuery("SP_SuaKhoa", name, param, 3);
        }

        public int RemoveKhoa(DKhoa khoa)
        {
            string[] name = { "@makh" };
            object[] param = { khoa.MaKhoa };
            return DBAccess.ExecuteNonQuery("SP_XoaKhoa", name, param, 1);
        }
    }
}
