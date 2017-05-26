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
    public partial class frmSinhVien : Form
    {
        private Stack<Command> _commands;
        private DataTable lop;

        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            _commands = new Stack<Command>();
            if (DBAccess.nhom == "Truong")
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                comboBox2.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                dateTimePicker1.Enabled = false;
                tableLayoutPanel2.Visible = true;
                LoadChiNhanhToCombo();
                comboBox1.Text = DBAccess.chiNhanh;
            }
            btnReload.PerformClick();
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
        }

        private void LoadChiNhanhToCombo()
        {
            foreach (Connection cnn in DBAccess.CnnList)
            {
                if (cnn.Name != "Trường")
                    comboBox1.Items.Add(cnn.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void AddLopToCombo()
        {
            lop = DBAccess.ExecuteQuery("SP_LayLopDonGian");
            if (lop == null)
            {
                MessageBox.Show("Lỗi CDSL! Không thể lấy danh sách các lớp");
                return;
            }
            else if (lop.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có lớp nào. Vui lòng nhập thêm lớp trước khi nhập sinh viên.");
                return;
            }

            comboBox2.Items.Clear();
            foreach (DataRow s in lop.Rows)
            {
                comboBox2.Items.Add(s[1]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Today;

            textBox1.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox2.SelectedIndex = 0;
            textBox2.Enabled = true;
            comboBox2.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private int Execute(string _operator, DSinhVien _operand, DSinhVien oldstate)
        {
            Command command = new SinhVienCommand(_operator, _operand, oldstate);
            int code = command.Execute();
            _commands.Push(command);
            btnUndo.Enabled = true;
            return code;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa.Enabled = false;
            DataRow red = gridView1.GetFocusedDataRow();
            DSinhVien KHTrongBang = new DSinhVien
            {
                MaSV = red["Mã sinh viên"].ToString(),
                Ho = red["Họ"].ToString(),
                Ten = red["Tên"].ToString(),
                NgaySinh = DateTime.Parse(red["Ngày sinh"].ToString()),
                DiaChi = red["Địa chỉ"].ToString(),
                MaLop = lop.Select(string.Format("tenlop ='{0}'", red["Tên lớp"].ToString()))[0][0].ToString()
            };

            int code = Execute("delete", KHTrongBang, null);
            if (code == 0)
            {
                //MessageBox.Show("Xoá sinh viên thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá sinh viên thất bại.");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("Mã sinh viên không được bỏ trống");
                errors += "\r\n+ Mã sinh viên bị bỏ trống";
                noError++;
            }
            if (textBox2.Text.Trim() == "")
            {
                //MessageBox.Show("Họ không được bỏ trống");
                errors += "\r\n+ Họ bị bỏ trống";
                noError++;
            }
            if (textBox3.Text.Trim() == "")
            {
                //MessageBox.Show("Tên không được bỏ trống");
                errors += "\r\n+ Tên bị bỏ trống";
                noError++;
            }
            if (comboBox2.Text.Trim() == "")
            {
                //MessageBox.Show("Tên lớp không được bỏ trống. Vui lòng nhập thêm lớp hoặc kiểm tra lại CSDL");
                errors += "\r\n+ Tên lớp không được bỏ trống.\r\nVui lòng nhập thêm lớp hoặc kiểm tra lại CSDL";
                noError++;
            }
            if (dateTimePicker1.Value.Date > DateTime.Today.AddYears(18))
            {
                ///MessageBox.Show("Chỉ được đăng ký lịch thi sau thời điểm hiện tại");
                errors += "\r\n+ Ngày sinh không hợp lệ";
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
                DSinhVien KHTrongForm = new DSinhVien
                {
                    MaSV = textBox1.Text.Trim().ToUpper(),
                    Ho = textBox2.Text.Trim().ToUpper(),
                    Ten = textBox3.Text.Trim().ToUpper(),
                    NgaySinh = dateTimePicker1.Value,
                    DiaChi = textBox4.Text.Trim(),
                    MaLop = lop.Select(string.Format("tenlop ='{0}'", comboBox2.Text.Trim()))[0][0].ToString()
                };

                int code = Execute("insert", KHTrongForm, null);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Tạo sinh viên thành công");
                }
                else
                {
                    MessageBox.Show("Tạo sinh viên thất bại");
                }
                textBox1.Enabled = false;
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                DSinhVien KHTrongBang = new DSinhVien
                {
                    MaSV = red["Mã sinh viên"].ToString(),
                    Ho = red["Họ"].ToString(),
                    Ten = red["Tên"].ToString(),
                    NgaySinh = DateTime.Parse(red["Ngày sinh"].ToString()),
                    DiaChi = red["Địa chỉ"].ToString(),
                    MaLop = lop.Select(string.Format("tenlop ='{0}'", red["Tên lớp"].ToString()))[0][0].ToString()
                };
                DSinhVien KHTrongForm = new DSinhVien
                {
                    MaSV = textBox1.Text.Trim().ToUpper(),
                    Ho = textBox2.Text.Trim().ToUpper(),
                    Ten = textBox3.Text.Trim().ToUpper(),
                    NgaySinh = dateTimePicker1.Value,
                    DiaChi = textBox4.Text.Trim(),
                    MaLop = lop.Select(string.Format("tenlop ='{0}'", comboBox2.Text.Trim()))[0][0].ToString()
                };

                int code = Execute("update", KHTrongForm, KHTrongBang);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Lưu sinh viên thành công");
                }
                else
                {
                    MessageBox.Show("Lưu sinh viên thất bại");
                }
            }
            btnLuu.Enabled = true;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable lop = DBAccess.ExecuteQuery("SP_LaySinhVien");
            gridControl1.DataSource = lop;
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                comboBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = false;
            }
            else
            {
                FocusedRowChanged();
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                comboBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
            }
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
            AddLopToCombo();
            FocusedRowChanged();
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
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                comboBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
            else
            {
                textBox1.Text = gridView1.GetFocusedRowCellValue("Mã sinh viên").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("Họ").ToString();
                textBox3.Text = gridView1.GetFocusedRowCellValue("Tên").ToString();
                textBox4.Text = gridView1.GetFocusedRowCellValue("Địa chỉ").ToString();
                comboBox2.Text = gridView1.GetFocusedRowCellValue("Tên lớp").ToString();
                dateTimePicker1.Value = DateTime.Parse(gridView1.GetFocusedRowCellValue("Ngày sinh").ToString());
            }
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void FocusedRowChanged()
        {
            if (gridView1.RowCount > 0)
            {
                DataRow red = gridView1.GetFocusedDataRow();
                textBox1.Text = red["Mã sinh viên"].ToString();
                textBox2.Text = red["Họ"].ToString();
                textBox3.Text = red["Tên"].ToString();
                textBox4.Text = red["Địa chỉ"].ToString();
                comboBox2.Text = red["Tên lớp"].ToString();
                dateTimePicker1.Value = DateTime.Parse(red["Ngày sinh"].ToString());
            }
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
            textBox1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection cnn = DBAccess.CnnList[comboBox1.SelectedIndex];
            DBAccess.dataSource = cnn.DataSource;
            DBAccess.initCatalog = cnn.InitCatalog;
            btnReload.PerformClick();
        }
    }
}
