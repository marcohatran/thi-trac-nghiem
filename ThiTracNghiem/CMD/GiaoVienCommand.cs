using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class GiaoVienCommand : Command
    {
        private string _operator;
        private DGiaoVien _operand;
        private DGiaoVien oldstate;

        public GiaoVienCommand(string _operator, DGiaoVien _operand, DGiaoVien oldstate)
        {
            this._operator = _operator;
            this._operand = _operand;
            this.oldstate = oldstate;
        }

        public override int Execute()
        {
            return Operation(_operator, _operand, oldstate);
        }

        public override int UnExecute()
        {
            return Operation(Undo(_operator), _operand, oldstate);
        }

        public string Undo(string _operator)
        {
            switch (_operator)
            {
                case "insert": return "delete";
                case "update": return "unupdate";
                case "delete": return "insert";
                default:
                    throw new ArgumentException("_operator");
            }
        }

        public int Operation(string _operator, DGiaoVien _operand, DGiaoVien oldstate)
        {
            int code = 0;
            switch (_operator)
            {
                case "insert":
                    code = CreateGiaoVien(_operand);
                    break;
                case "update":
                    code = UpdateGiaoVien(_operand);
                    break;
                case "delete":
                    code = RemoveGiaoVien(_operand);
                    break;
                case "unupdate":
                    code = UpdateGiaoVien(oldstate);
                    break;
                case "undelete":
                    code = RestoreGiaoVien(oldstate);
                    break;
            }
            return code;
        }

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
