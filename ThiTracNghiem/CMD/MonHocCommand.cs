using System;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem.CMD
{
    class MonHocCommand : Command
    {
        private string _operator;
        private DMonHoc _operand;
        private DMonHoc oldstate;

        public DMonHoc getOperand()
        {
            return _operand;
        }

        public MonHocCommand(string _operator, DMonHoc _operand, DMonHoc oldstate)
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

        public int Operation(string _operator, DMonHoc _operand, DMonHoc oldstate)
        {
            int code=0;
            switch (_operator)
            {
                case "insert":
                    code = CreateMonHoc(_operand);
                    break;
                case "update":
                    code = UpdateMonHoc(_operand);
                    break;
                case "delete":
                    code = RemoveMonHoc(_operand);
                    break;
                case "unupdate":
                    code = UpdateMonHoc(oldstate);
                    break;
            }
            return code;
        }

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
