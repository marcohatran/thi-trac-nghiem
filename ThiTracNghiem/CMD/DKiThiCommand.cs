using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class DKiThiCommand : Command
    {
        private string _operator;
        private DGVDangKy _operand;
        private DGVDangKy oldstate;

        public DGVDangKy getOperand()
        {
            return _operand;
        }

        public DKiThiCommand(string _operator, DGVDangKy _operand, DGVDangKy oldstate)
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

        public int Operation(string _operator, DGVDangKy _operand, DGVDangKy oldstate)
        {
            int code = 0;
            switch (_operator)
            {
                case "insert":
                    code = CreateDangKy(_operand);
                    break;
                case "update":
                    code = UpdateDangKy(_operand);
                    break;
                case "delete":
                    code = RemoveDangKy(_operand);
                    break;
                case "unupdate":
                    code = UpdateDangKy(oldstate);
                    break;
            }
            return code;
        }

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
