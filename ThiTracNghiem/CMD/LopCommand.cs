using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class LopCommand : Command
    {
        private string _operator;
        private DLop _operand;
        private DLop oldstate;

        public DLop getOperand()
        {
            return _operand;
        }

        public LopCommand(string _operator, DLop _operand, DLop oldstate)
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

        public int Operation(string _operator, DLop _operand, DLop oldstate)
        {
            int code = 0;
            switch (_operator)
            {
                case "insert":
                    code = CreateLop(_operand);
                    break;
                case "update":
                    code = UpdateLop(_operand);
                    break;
                case "delete":
                    code = RemoveLop(_operand);
                    break;
                case "unupdate":
                    code = UpdateLop(oldstate);
                    break;
            }
            return code;
        }

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
