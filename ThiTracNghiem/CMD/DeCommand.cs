using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class DeCommand : Command
    {
        private string _operator;
        private DDe _operand;
        private DDe oldstate;

        public DeCommand(string _operator, DDe _operand, DDe oldstate)
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

        public int Operation(string _operator, DDe _operand, DDe oldstate)
        {
            int code = 0;
            switch (_operator)
            {
                case "insert":
                    code = CreateDe(_operand);
                    break;
                case "update":
                    code = UpdateDe(_operand);
                    break;
                case "delete":
                    code = RemoveDe(_operand);
                    break;
                case "unupdate":
                    code = UpdateDe(oldstate);
                    break;
            }
            return code;
        }

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
