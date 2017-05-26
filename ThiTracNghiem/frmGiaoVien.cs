using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiTracNghiem.CMD;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem
{
    public partial class frmGiaoVien : Form
    {
        private Stack<Command> _commands;
        private DataTable khoa;

        public frmGiaoVien()
        {
            InitializeComponent();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            btnReload.PerformClick();
            _commands = new Stack<Command>();
            if (DBAccess.nhom == "Truong")
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                comboBox2.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox5.Enabled = false;
                comboBox3.Enabled = false;
                tableLayoutPanel2.Visible = true;
                LoadChiNhanhToCombo();
                comboBox1.Text = DBAccess.chiNhanh;
            }
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
                MessageBox.Show("Trong CSDL không có khoa nào. Vui lòng nhập thêm khoa trước khi nhập giáo viên.");
                return;
            }

            comboBox2.Items.Clear();
            foreach (DataRow s in khoa.Rows)
            {
                comboBox2.Items.Add(s[1]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox5.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private int Execute(string _operator, DGiaoVien _operand, DGiaoVien oldstate)
        {
            Command command = new GiaoVienCommand(_operator, _operand, oldstate);
            int code = command.Execute();
            _commands.Push(command);
            btnUndo.Enabled = true;
            return code;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa.Enabled = false;
            DataRow red = gridView1.GetFocusedDataRow();
            DGiaoVien GVTrongBang = new DGiaoVien
            {
                MaGV = red["Mã giáo viên"].ToString(),
                Ho = red["Họ"].ToString(),
                Ten = red["Tên"].ToString(),
                HocVi = red["Học vị"].ToString(),
                DiaChi = red["Địa chỉ"].ToString(),
                MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", red["Tên khoa"].ToString()))[0][0].ToString()
            };

            int code = Execute("delete", GVTrongBang, null);
            if (code == 0)
            {
                //MessageBox.Show("Xoá giáo viên thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá giáo viên thất bại.");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("Mã giáo viên không được bỏ trống");
                errors += "\r\n+ Mã giáo viên bị bỏ trống";
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
                DGiaoVien GVTrongForm = new DGiaoVien
                {
                    MaGV = textBox1.Text.Trim().ToUpper(),
                    Ho = textBox2.Text.Trim().ToUpper(),
                    Ten = textBox3.Text.Trim().ToUpper(),
                    HocVi = comboBox3.Text.Trim(),
                    DiaChi = textBox5.Text.Trim(),
                    MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", comboBox2.Text.Trim()))[0][0].ToString()
                };

                int code = Execute("insert", GVTrongForm, null);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Tạo giáo viên thành công");
                }
                else
                {
                    MessageBox.Show("Tạo giáo viên thất bại");
                }
                textBox1.Enabled = false;
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                DGiaoVien GVTrongBang = new DGiaoVien
                {
                    MaGV = red["Mã giáo viên"].ToString(),
                    Ho = red["Họ"].ToString(),
                    Ten = red["Tên"].ToString(),
                    HocVi = red["Học vị"].ToString(),
                    DiaChi = red["Địa chỉ"].ToString(),
                    MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", red["Tên khoa"].ToString()))[0][0].ToString()
                };
                DGiaoVien GVTrongForm = new DGiaoVien
                {
                    MaGV = textBox1.Text.Trim().ToUpper(),
                    Ho = textBox2.Text.Trim().ToUpper(),
                    Ten = textBox3.Text.Trim().ToUpper(),
                    HocVi = comboBox3.Text.Trim(),
                    DiaChi = textBox5.Text.Trim(),
                    MaKhoa = khoa.Select(string.Format("[Tên khoa] ='{0}'", comboBox2.Text.Trim()))[0][0].ToString()
                };

                int code = Execute("update", GVTrongForm, GVTrongBang);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Cập nhật thông tin giáo viên thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin giáo viên khoa thất bại");
                }
            }
            btnLuu.Enabled = true;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable lop = DBAccess.ExecuteQuery("SP_LayGiaoVien");
            gridControl1.DataSource = lop;
            AddKhoaToCombo();
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox5.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
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
                textBox5.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
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

            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox5.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
            else
            {
                textBox1.Text = gridView1.GetFocusedRowCellValue("Mã giáo viên").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("Họ").ToString();
                textBox3.Text = gridView1.GetFocusedRowCellValue("Tên").ToString();
                comboBox3.Text = gridView1.GetFocusedRowCellValue("Học vị").ToString();
                textBox5.Text = gridView1.GetFocusedRowCellValue("Địa chỉ").ToString();
                comboBox2.Text = gridView1.GetFocusedRowCellValue("Tên khoa").ToString();
            }
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connection cnn = DBAccess.CnnList[comboBox1.SelectedIndex];
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
            textBox1.Text = red["Mã giáo viên"].ToString();
            textBox2.Text = red["Họ"].ToString();
            textBox3.Text = red["Tên"].ToString();
            comboBox3.Text = red["Học vị"].ToString();
            textBox5.Text = red["Địa chỉ"].ToString();
            comboBox2.Text = red["Tên khoa"].ToString(); ;
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
