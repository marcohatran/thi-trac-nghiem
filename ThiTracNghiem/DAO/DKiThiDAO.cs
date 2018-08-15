using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class DKiThiDAO
    {

        public int CreateDangKy(DGVDangKy dangKy)
        {
            string[] name = { "@mamh", "@malop", "@lan", "@magv", "@trinhdo", "@ngaythi", "@socauthi", "@thoigian" };
            object[] param = { dangKy.MaMonHoc, dangKy.MaLop, dangKy.Lan, dangKy.MaGV, dangKy.TrinhDo,
                                    dangKy.NgayThi, dangKy.SoCauThi, dangKy.ThoiGian };
            return DBAccess.ExecuteNonQuery("SP_ThemDKThi", name, param, 8);
        }

        public int UpdateDangKy(DGVDangKy dangKy)
        {
            string[] name = { "@mamh", "@malop", "@lan", "@magv", "@trinhdo", "@ngaythi", "@socauthi", "@thoigian" };
            object[] param = { dangKy.MaMonHoc, dangKy.MaLop, dangKy.Lan, dangKy.MaGV, dangKy.TrinhDo,
                                    dangKy.NgayThi, dangKy.SoCauThi, dangKy.ThoiGian };
            return DBAccess.ExecuteNonQuery("SP_SuaLichThi", name, param, 8);
        }

        public int RemoveDangKy(DGVDangKy dangKy)
        {
            string[] name = { "@mamh", "@malop", "@lan" };
            object[] param = { dangKy.MaMonHoc, dangKy.MaLop, dangKy.Lan };
            return DBAccess.ExecuteNonQuery("SP_XoaLichThi", name, param, 3);
        }
    }
}
