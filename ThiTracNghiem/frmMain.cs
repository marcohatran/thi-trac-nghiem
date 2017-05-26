using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadSettings();
            frmDangNhap f = new frmDangNhap() { MdiParent = this, Text = "Đăng nhập" };
            f.Show();
        }

        private void LoadFileSettings()
        {
            string line;
            using (StreamReader file = new StreamReader(@"data.csv"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    Connection cnn = new Connection()
                    {
                        Name = fields[0],
                        InitCatalog = fields[1],
                        DataSource = fields[2]
                    };
                    DBAccess.CnnList.Add(cnn);
                }
            }
        }

        private void LoadSettings()
        {
            DataTable chiNhanh = DBAccess.ExecuteQuery("SP_LayDSCoSo");
            if (chiNhanh == null)
            {
                LoadFileSettings();
                return;
            }
            else if (chiNhanh.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có cơ sở nào. ");
                return;
            }
            using (StreamWriter file = new StreamWriter(@"data.csv"))
            {
                foreach (DataRow s in chiNhanh.Rows)
                {
                    Connection cnn = new Connection()
                    {
                        InitCatalog = s["subscriber_db"].ToString(),
                        DataSource = s["subscriber"].ToString(),
                        Name = s["NAME"].ToString()
                    };
                    DBAccess.CnnList.Add(cnn);
                    file.WriteLine(string.Format("{0},{1},{2}", cnn.Name, cnn.InitCatalog, cnn.DataSource));
                }
            }
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDangNhap));
            if (frm != null) frm.Activate();
            else
            {
                frmDangNhap f = new frmDangNhap() { MdiParent = this, Text = "Đăng nhập" };
                f.Show();
            }
        }

        public void DangNhap()
        {
            btnDangNhap.Enabled = false;
            btnDangXuat.Enabled = true;
        }

        public void HienThiQuanLyTab()
        {
            pageQuanLy.Visible = true;
            pageGiaoVien.Visible = true;
            pageSinhVien.Visible = true;
            btnThi.Enabled = false;
            ribbonControl1.SelectedPage = ribbonPage2;
            if (DBAccess.nhom == "nhan vien")
            {
                btnDoiMKNV.Enabled = true;
                btnThemTK.Enabled = true;
            }
        }

        public void HienThiGiaoVienTab()
        {
            pageGiaoVien.Visible = true;
            pageSinhVien.Visible = true;
            btnThi.Enabled = false;
            ribbonControl1.SelectedPage = ribbonPage3;
            btnDoiMKNV.Enabled = true;
        }

        public void HienThiSinhVienTab()
        {
            pageSinhVien.Visible = true;
            btnThi.Enabled = true;
            ribbonControl1.SelectedPage = ribbonPage4;
        }

        public void HienThiStatus()
        {
            lblID.Text = DBAccess.id;
            lblHoTen.Text = DBAccess.hoTen;
            lblMaDonVi.Text = DBAccess.donVi;
            lblDonVi.Text = DBAccess.tenDonVi;
            lblNhom.Text = DBAccess.nhom;
        }

        private void btnDangXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pageQuanLy.Visible = false;
            pageGiaoVien.Visible = false;
            pageSinhVien.Visible = false;

            DBAccess.hoTen = "";
            DBAccess.chiNhanh = "";
            DBAccess.donVi = "";
            DBAccess.tenDonVi = "";
            DBAccess.id = "";
            DBAccess.nhom = "";

            HienThiStatus();

            btnDangNhap.Enabled = true;
            btnDangXuat.Enabled = false;
            btnDoiMKNV.Enabled = false;

            foreach (Form f in this.MdiChildren)
                f.Close();

            btnDangNhap.PerformClick();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void btnMonHoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmMonHoc));
            if (frm != null) frm.Activate();
            else
            {
                frmMonHoc f = new frmMonHoc() { MdiParent = this, Text = "Quản lý môn học" };
                f.Show();
            }
        }

        private void btnKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmKhoa));
            if (frm != null) frm.Activate();
            else
            {
                frmKhoa f = new frmKhoa() { MdiParent = this, Text = "Quản lý khoa" };
                f.Show();
            }
        }

        private void btnLop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmLop));
            if (frm != null) frm.Activate();
            else
            {
                frmLop f = new frmLop() { MdiParent = this, Text = "Quản lý lớp" };
                f.Show();
            }
        }

        private void btnGiaoVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmGiaoVien));
            if (frm != null) frm.Activate();
            else
            {
                frmGiaoVien f = new frmGiaoVien() { MdiParent = this, Text = "Quản lý giáo viên" };
                f.Show();
            }
        }

        private void btnSinhVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmSinhVien));
            if (frm != null) frm.Activate();
            else
            {
                frmSinhVien f = new frmSinhVien() { MdiParent = this, Text = "Quản lý sinh viên" };
                f.Show();
            }
        }

        private void btnDe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDe));
            if (frm != null) frm.Activate();
            else
            {
                frmDe f = new frmDe() { MdiParent = this, Text = "Quản lý đề thi" };
                f.Show();
            }
        }

        private void btnDKThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDKLichThi));
            if (frm != null) frm.Activate();
            else
            {
                frmDKLichThi f = new frmDKLichThi() { MdiParent = this, Text = "Đăng ký lịch thi" };
                f.Show();
            }
        }

        private void btnThiThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmThiThu));
            if (frm != null) frm.Activate();
            else
            {
                frmThiThu f = new frmThiThu() { MdiParent = this, Text = "Thi thử" };
                f.Show();
            }
        }

        private void btnBangDiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmBangDiem));
            if (frm != null) frm.Activate();
            else
            {
                frmBangDiem f = new frmBangDiem() { MdiParent = this, Text = "Xem bảng điểm" };
                f.Show();
            }
        }

        private void btnThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmThi));
            if (frm != null) frm.Activate();
            else
            {
                frmThi f = new frmThi() { MdiParent = this, Text = "Thi" };
                f.Show();
            }
        }

        private void btnBaiThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmBaiThi));
            if (frm != null) frm.Activate();
            else
            {
                frmBaiThi f = new frmBaiThi() { MdiParent = this, Text = "Xem bài thi" };
                f.Show();
            }
        }

        private void btnDoiMKNV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDoiMK));
            if (frm != null) frm.Activate();
            else
            {
                frmDoiMK f = new frmDoiMK() { MdiParent = this, Text = "Đổi mật khẩu" };
                f.Show();
            }
        }

        private void btnThemTK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmTaoTK));
            if (frm != null) frm.Activate();
            else
            {
                frmTaoTK f = new frmTaoTK() { MdiParent = this, Text = "Thêm tài khoản" };
                f.Show();
            }
        }
    }
}
