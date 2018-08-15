using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class LopDAO
    {

        public int CreateLop(DLop lop)
        {
            string[] name = { "@malop", "@tenlop", "@makh" };
            object[] param = { lop.MaLop, lop.TenLop, lop.MaKhoa };
            return DBAccess.ExecuteNonQuery("SP_Themlop", name, param, 3);
        }

        public int UpdateLop(DLop lop)
        {
            string[] name = { "@malop", "@tenlop", "@makh" };
            object[] param = { lop.MaLop, lop.TenLop, lop.MaKhoa };
            return DBAccess.ExecuteNonQuery("SP_Sualop", name, param, 3);
        }

        public int RemoveLop(DLop lop)
        {
            string[] name = { "@malop" };
            object[] param = { lop.MaLop };
            return DBAccess.ExecuteNonQuery("SP_Xoalop", name, param, 1);
        }
    }
}
