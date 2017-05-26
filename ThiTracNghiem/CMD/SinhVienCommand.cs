using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class SinhVienCommand : Command
    {
        private string _operator;
        private DSinhVien _operand;
        private DSinhVien oldstate;

        public SinhVienCommand(string _operator, DSinhVien _operand, DSinhVien oldstate)
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

        public int Operation(string _operator, DSinhVien _operand, DSinhVien oldstate)
        {
            int code=0;
            switch (_operator)
            {
                case "insert":
                    code = CreateSinhVien(_operand);
                    break;
                case "update":
                    code = UpdateSinhVien(_operand);
                    break;
                case "delete":
                    code = RemoveSinhVien(_operand);
                    break;
                case "unupdate":
                    code = UpdateSinhVien(oldstate);
                    break;
            }
            return code;
        }

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
