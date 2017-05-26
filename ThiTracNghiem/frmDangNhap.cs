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

namespace ThiTracNghiem
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            LoadChiNhanhToCombo();
        }

        private void LoadChiNhanhToCombo()
        {
            foreach (Connection cnn in DBAccess.CnnList)
            {
                comboBox1.Items.Add(cnn.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private bool DangNhapSV(string username)
        {
            DBAccess.username = ConnectionSettings.Default.sinhvien;
            DBAccess.password = ConnectionSettings.Default.svpwd;
            DBAccess.Refresh();
            string[] name = { "@masv" };
            object[] param = { username };
            SqlDataReader reader = DBAccess.ExecSqlDataReader("SP_DangNhapSV", name, param, 1);
            if (reader == null)
                return false;
            reader.Read();
            if (reader.HasRows)
            {
                DBAccess.hoTen = reader["Ho ten"].ToString();
                DBAccess.nhom = "Sinh viên";
                DBAccess.id = username;
                DBAccess.donVi = reader["malop"].ToString();
                DBAccess.tenDonVi = reader["tenlop"].ToString();
                DBAccess.chiNhanh = comboBox1.Text.Trim();
                Program.frmChinh.HienThiStatus();
                Program.frmChinh.HienThiSinhVienTab();
                Program.frmChinh.DangNhap();
                DBAccess.Close();
                reader.Close();
                this.Close();
            }
            else
            {
                DBAccess.Close();
                reader.Close();
                return false;
            }
            return true;
        }

        private bool DangNhapCS(string username, string password)
        {
            DBAccess.username = username;
            DBAccess.password = password;
            DBAccess.Refresh();
            string[] name = { "@login" };
            object[] param = { username };
            SqlDataReader reader = DBAccess.ExecSqlDataReader("SP_DangNhap", name, param, 1);
            if (reader == null)
                return false;
            reader.Read();
            if (reader.HasRows)
            {
                DBAccess.hoTen = reader["Ho ten"].ToString();
                DBAccess.nhom = reader["Role"].ToString();
                DBAccess.id = username;
                DBAccess.donVi = reader["Don vi"].ToString();
                DBAccess.tenDonVi = reader["Ten Don vi"].ToString();
                DBAccess.chiNhanh = comboBox1.Text.Trim();
                Program.frmChinh.HienThiStatus();
                Program.frmChinh.DangNhap();
                reader.Close();
                if (DBAccess.nhom == "giao vien")
                {
                    Program.frmChinh.HienThiGiaoVienTab();
                }
                else
                {
                    Program.frmChinh.HienThiQuanLyTab();
                }
                DBAccess.Close();
                reader.Close();
                this.Close();
                return true;
            }
            DBAccess.Close();
            return false;
        }

        private bool DangNhapTruong(string username, string password)
        {
            DBAccess.dataSource = ConnectionSettings.Default.DefaultDatasource;
            DBAccess.initCatalog = ConnectionSettings.Default.DefaultCatalog;
            DBAccess.username = username;
            DBAccess.password = password;
            DBAccess.Refresh();

            DBAccess.Close();

            string[] name = { "@login" };
            object[] param = { username };
            SqlDataReader reader = DBAccess.ExecSqlDataReader("SP_DangNhapTruong", name, param, 1);
            if (reader == null)
                return false;
            reader.Read();
            if (reader.HasRows)
            {
                Connection cnn = DBAccess.CnnList[comboBox1.SelectedIndex];
                DBAccess.dataSource = cnn.DataSource;
                DBAccess.username = ConnectionSettings.Default.support;
                DBAccess.password = ConnectionSettings.Default.sppwd;

                DBAccess.nhom = "Truong";
                DBAccess.id = username;
                DBAccess.chiNhanh = comboBox1.Text.Trim();
                Program.frmChinh.HienThiStatus();
                Program.frmChinh.HienThiQuanLyTab();
                Program.frmChinh.DangNhap();
                reader.Close();
                DBAccess.Close();
                this.Close();
                return true;
            }
            DBAccess.Close();
            return false;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Tên tài khoản không được để trống");
                textBox1.Focus();
                return;
            }

            Connection cnn = DBAccess.CnnList[comboBox1.SelectedIndex];
            DBAccess.dataSource = cnn.DataSource;
            DBAccess.initCatalog = cnn.InitCatalog;

            string username = textBox1.Text.Trim().ToUpper();
            string password = textBox2.Text.Trim();

            if (password == "")
            {
                if (!DangNhapSV(username))
                    MessageBox.Show("Sai mã sinh viên");
            }
            else if (!DangNhapCS(username, password))
                if (!DangNhapTruong(username, password))
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
            textBox2.Text = "";
        }

    }
}
