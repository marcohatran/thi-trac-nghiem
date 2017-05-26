using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiTracNghiem.CMD;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem
{
    public partial class frmLop : Form
    {
        private Stack<Command> _commands;
        private DataTable khoa;

        public frmLop()
        {
            InitializeComponent();
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            btnReload.PerformClick();
            _commands = new Stack<Command>();
            if (DBAccess.nhom == "Truong")
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                textBox2.Enabled = false;
                comboBox2.Enabled = false;
                tableLayoutPanel1.Visible = true;
                LoadChiNhanhToCombo();
                comboBox2.Text = DBAccess.chiNhanh;
            }
        }

        private void LoadChiNhanhToCombo()
        {
            foreach (Connection cnn in DBAccess.CnnList)
            {
                if (cnn.Name != "Trường")
                    comboBox2.Items.Add(cnn.Name);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void AddKhoaToCombo()
        {
            khoa = DBAccess.ExecuteQuery("SP_LayKhoa");
            if (khoa == null)
            {
                MessageBox.Show("Lỗi CDSL! Không thể lấy danh sách các khoa");
                return;
            }
            else if (khoa.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có khoa nào. Vui lòng nhập thêm khoa trước khi nhập lớp.");
                return;
            }
            comboBox3.Items.Clear();
            foreach (DataRow s in khoa.Rows)
            {
                comboBox3.Items.Add(s[1]);
            }
            comboBox3.SelectedIndex = 0;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            comboBox3.SelectedIndex = 0;
            textBox2.Enabled = true;
            comboBox3.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private int Execute(string _operator, DLop _operand, DLop oldstate)
        {
            Command command = new LopCommand(_operator, _operand, oldstate);
            int code = command.Execute();
            _commands.Push(command);
            btnUndo.Enabled = true;
            return code;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa.Enabled = false;
            DataRow red = gridView1.GetFocusedDataRow();
            DLop LopTrongBang = new DLop
            {
                MaLop = red["Mã lớp"].ToString(),
                TenLop = red["Tên lớp"].ToString(),
                MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", red["Tên khoa"].ToString()))[0][0].ToString()
            };
            int code = Execute("delete", LopTrongBang, null);
            if (code == 0)
            {
                //MessageBox.Show("Xoá lớp thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá lớp thất bại");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("Mã lớp không được bỏ trống");
                errors += "\r\n+ Mã lớp bị bỏ trống";
                noError++;
            }
            if (textBox2.Text.Trim() == "")
            {
                //MessageBox.Show("Tên lớp không được bỏ trống");
                errors += "\r\n+ Tên lớp bị bỏ trống";
                noError++;
            }
            if (comboBox3.Text.Trim() == "")
            {
                //MessageBox.Show("Tên khoa không được bỏ trống. Vui lòng nhập thêm khoa hoặc kiểm tra lại CSDL");
                errors += "\r\n+ Tên khoa bị bỏ trống. Vui lòng nhập thêm khoa hoặc kiểm tra lại CSDL";
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
                DataRow red = gridView1.GetFocusedDataRow();
                string maLopTrongForm = khoa.Select(string.Format("[Tên khoa] ='{0}'", comboBox3.Text))[0][0].ToString();
                DLop LopTrongForm = new DLop
                {
                    MaLop = textBox1.Text.Trim(),
                    TenLop = textBox2.Text.Trim(),
                    MaKhoa = maLopTrongForm
                };

                int code = Execute("insert", LopTrongForm, null);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    // MessageBox.Show("Tạo lớp thành công");
                }
                else
                {
                    MessageBox.Show("Tạo lớp thất bại");
                }
                textBox1.Enabled = false;
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                DLop LopTrongBang = new DLop
                {
                    MaLop = red["Mã lớp"].ToString(),
                    TenLop = red["Tên lớp"].ToString(),
                    MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", red["Tên khoa"].ToString()))[0][0].ToString()
                };
                DLop LopTrongForm = new DLop
                {
                    MaLop = textBox1.Text.Trim(),
                    TenLop = textBox2.Text.Trim(),
                    MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", comboBox3.Text))[0][0].ToString()
                };

                int code = Execute("update", LopTrongForm, LopTrongBang);
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
            DataTable lop = DBAccess.ExecuteQuery("SP_LayLop");
            gridControl1.DataSource = lop;
            AddKhoaToCombo();
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                comboBox2.Enabled = false;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = false;
            }
            else
            {
                FocusedRowChanged();
                textBox2.Enabled = true;
                comboBox2.Enabled = true;
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

        private void btnHuyBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                comboBox2.Enabled = false;
            }
            else
            {
                textBox1.Text = gridView1.GetFocusedRowCellValue("Mã lớp").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("Tên lớp").ToString();
                comboBox3.Text = gridView1.GetFocusedRowCellValue("Tên khoa").ToString();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection cnn = DBAccess.CnnList[comboBox2.SelectedIndex];
            DBAccess.dataSource = cnn.DataSource;
            DBAccess.initCatalog = cnn.InitCatalog;
            btnReload.PerformClick();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
            textBox1.Enabled = false;
        }

        private void FocusedRowChanged()
        {
            DataRow red = gridView1.GetFocusedDataRow();
            textBox1.Text = red["Mã lớp"].ToString();
            textBox2.Text = red["Tên lớp"].ToString();
            comboBox3.Text = red["Tên khoa"].ToString();
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }
    }
}
