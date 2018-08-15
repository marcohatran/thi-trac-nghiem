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
    public partial class frmKhoa : Form
    {
        private KhoaDAO khoaDAO;
        public frmKhoa()
        {
            InitializeComponent();
        }

        private void frmKhoa_Load(object sender, EventArgs e)
        {
            btnReload.PerformClick();
            khoaDAO = new KhoaDAO();
            if (DBAccess.nhom == "Truong")
            {
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
                textBox2.Enabled = false;
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

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Enabled = true;
            textBox2.Text = "";
            textBox2.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa.Enabled = false;
            DataRow red = gridView1.GetFocusedDataRow();
            DKhoa khoa = new DKhoa
            {
                MaKhoa = red["Mã khoa"].ToString(),
                TenKhoa = red["Tên khoa"].ToString(),
                MaCS = DBAccess.donVi
            };

            int code = khoaDAO.RemoveKhoa(khoa);
            if (code == 0)
            {
                //MessageBox.Show("Xoá khoa thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá khoa thất bại.");
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("Mã khoa không được bỏ trống");
                errors += "\r\n+ Mã khoa bị bỏ trống";
                noError++;
            }
            if (textBox2.Text.Trim() == "")
            {
                //MessageBox.Show("Tên khoa không được bỏ trống");
                errors += "\r\n+ Tên khoa bị bỏ trống";
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
                DKhoa khoa = new DKhoa
                {
                    MaKhoa = textBox1.Text.Trim().ToUpper(),
                    TenKhoa = textBox2.Text.Trim(),
                    MaCS = DBAccess.donVi
                };

                int code = khoaDAO.CreateKhoa(khoa);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Tạo khoa thành công");
                }
                else
                {
                    MessageBox.Show("Tạo khoa thất bại");
                }
                textBox1.Enabled = false;
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                DKhoa khoa = new DKhoa
                {
                    MaKhoa = textBox1.Text.Trim().ToUpper(),
                    TenKhoa = textBox2.Text.Trim(),
                    MaCS = DBAccess.donVi
                };


                int code = khoaDAO.UpdateKhoa(khoa);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Lưu khoa thành công");
                }
                else
                {
                    MessageBox.Show("Lưu khoa thất bại");
                }
            }
            btnLuu.Enabled = true;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable monHoc = DBAccess.ExecuteQuery("SP_LayKhoa");
            gridControl1.DataSource = monHoc;
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = false;
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

        private void btnHuyBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
            if (gridView1.RowCount == 0)
            {
                textBox2.Enabled = false;
            }
            else
            {
                textBox1.Text = gridView1.GetFocusedRowCellValue("Mã khoa").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("Tên khoa").ToString();
            }
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
            textBox1.Text = red["Mã khoa"].ToString();
            textBox2.Text = red["Tên khoa"].ToString();
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
