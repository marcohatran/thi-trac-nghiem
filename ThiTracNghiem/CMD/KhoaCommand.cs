using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{

    class KhoaCommand : Command
    {
        private string _operator;
        private DKhoa _operand;
        private DKhoa oldstate;

        public DKhoa getOperand()
        {
            return _operand;
        }

        public KhoaCommand(string _operator, DKhoa _operand, DKhoa oldstate)
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
            switch(_operator)
            {
                case "insert": return "delete";
                case "update": return "unupdate";
                case "delete": return "insert";
                default:
                    throw new ArgumentException("_operator");
            }
        }

        public int Operation(string _operator, DKhoa _operand, DKhoa oldstate)
        {
            int code = 0;
            switch (_operator)
            {
                case "insert":
                    code = CreateKhoa(_operand);
                    break;
                case "update":
                    code = UpdateKhoa(_operand);
                    break;
                case "delete":
                    code = RemoveKhoa(_operand);
                    break;
                case "unupdate":
                    code = UpdateKhoa(oldstate);
                    break;
            }
            return code;
        }

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
