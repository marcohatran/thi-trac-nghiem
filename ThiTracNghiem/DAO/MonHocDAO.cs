using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class MonHocDAO
    {

        public int CreateMonHoc(DMonHoc monHoc)
        {
            string[] name = { "@mamh", "@tenmh" };
            object[] param = { monHoc.MaMH, monHoc.TenMH };
            return DBAccess.ExecuteNonQuery("SP_ThemMonHoc", name, param, 2);
        }

        public int UpdateMonHoc(DMonHoc monHoc)
        {
            string[] name = { "@mamh", "@tenmh" };
            object[] param = { monHoc.MaMH, monHoc.TenMH };
            return DBAccess.ExecuteNonQuery("SP_SuaMonHoc", name, param, 2);
        }

        public int RemoveMonHoc(DMonHoc monHoc)
        {
            string[] name = { "@mamh" };
            object[] param = { monHoc.MaMH };
            return DBAccess.ExecuteNonQuery("SP_XoaMonHoc", name, param, 1);
        }
    }
}
