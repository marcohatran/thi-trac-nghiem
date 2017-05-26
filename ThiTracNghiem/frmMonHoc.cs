using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ThiTracNghiem.CMD;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem
{
    public partial class frmMonHoc : Form
    {
        private Stack<Command> _commands;
        public frmMonHoc()
        {
            InitializeComponent();
        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            btnReload.PerformClick();
            _commands = new Stack<Command>();
            if (DBAccess.nhom == "Truong")
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            textBox2.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private int Execute(string _operator, DMonHoc _operand, DMonHoc oldstate)
        {
            Command command = new MonHocCommand(_operator, _operand, oldstate);
            int code = command.Execute();
            _commands.Push(command);
            btnUndo.Enabled = true;
            return code;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa.Enabled = false;
            DataRow red = gridView1.GetFocusedDataRow();
            DMonHoc MHTrongBang = new DMonHoc { MaMH = red["Mã môn học"].ToString(), TenMH = red["Tên môn học"].ToString() };

            int code = Execute("delete", MHTrongBang, null);
            if (code == 0)
            {
                //MessageBox.Show("Xoá thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá thất bại.");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("Mã môn học không được để trống");
                errors += "\r\n+ Mã môn học bị bỏ trống";
                noError++;
            }
            if (textBox2.Text.Trim() == "")
            {
                //MessageBox.Show("Tên môn học không được để trống");
                errors += "\r\n+ Tên môn học bị bỏ trống";
                noError++;
            }
            if (noError > 0)
            {
                MessageBox.Show(errors);
                btnLuu.Enabled = true;
                return;
            }

            if (textBox1.Enabled)
            {
                DMonHoc MHTrongForm = new DMonHoc { MaMH = textBox1.Text.Trim().ToUpper(), TenMH = textBox2.Text.Trim() };
                int code = Execute("insert", MHTrongForm, null);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Tạo môn học thành công");
                }
                else
                {
                    MessageBox.Show("Tạo môn học thất bại");
                }
                textBox1.Enabled = false;
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                DMonHoc MHTrongBang = new DMonHoc { MaMH = red["Mã môn học"].ToString(), TenMH = red["Tên môn học"].ToString() };
                DMonHoc MHTrongForm = new DMonHoc { MaMH = textBox1.Text.Trim().ToUpper(), TenMH = textBox2.Text.Trim() };
                int code = Execute("update", MHTrongForm, MHTrongBang);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Lưu môn học thành công");
                }
                else
                {
                    MessageBox.Show("Lưu môn học thất bại");
                }
            }
            btnLuu.Enabled = true;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable monHoc = DBAccess.ExecuteQuery("SP_LayMonHoc");
            gridControl1.DataSource = monHoc;
            if (gridView1.RowCount == 0)
            {
                btnXoa.Enabled = false;
                if (DBAccess.nhom != "Truong")
                    textBox2.Enabled = false;
            }
            else
            {
                FocusedRowChanged();
                textBox2.Enabled = true;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = true;
            }
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_commands.Count > 0)
            {
                Command command = _commands.Pop();
                int code = command.UnExecute();
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Phục hồi thành công");
                }
                else
                    MessageBox.Show("Phục hồi thất bại.");
                if (_commands.Count == 0)
                    btnUndo.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private void FocusedRowChanged()
        {
            DataRow red = gridView1.GetFocusedDataRow();
            textBox1.Text = red["Mã môn học"].ToString();
            textBox2.Text = red["Tên môn học"].ToString();
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
            textBox1.Enabled = false;
        }

        private void btnHuyBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
            }
            else
            {
                textBox1.Text = gridView1.GetFocusedRowCellValue("Mã môn học").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("Tên môn học").ToString();
            }
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }
    }
}
