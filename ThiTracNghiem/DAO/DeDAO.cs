using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class DeDAO
    {

        public int CreateDe(DDe de)
        {
            string[] name = { "@mamh", "@trinhdo", "@noidung", "@a", "@b", "@c", "@d", "@dapan", "@magv"};
            object[] param = { de.MaMonHoc, de.TrinhDo, de.NoiDung, de.A, de.B, de.C, de.D, de.DapAn, de.MaGV };
            return DBAccess.ExecuteNonQuery("SP_ThemDe", name, param, 9);
        }

        public int UpdateDe(DDe de)
        {
            string[] name = { "@cauhoi", "@mamh", "@trinhdo", "@noidung", "@a", "@b", "@c", "@d", "@dapan", "@magv" };
            object[] param = { de.CauHoi, de.MaMonHoc, de.TrinhDo, de.NoiDung, de.A, de.B, de.C, de.D, de.DapAn, de.MaGV };
            return DBAccess.ExecuteNonQuery("SP_SuaDe", name, param, 10);
        }

        public int RemoveDe(DDe de)
        {
            string[] name = { "@cauhoi" };
            object[] param = { de.CauHoi };
            return DBAccess.ExecuteNonQuery("SP_XoaDe", name, param, 1);
        }
    }
}
