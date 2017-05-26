using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class frmTaoTK : Form
    {
        public frmTaoTK()
        {
            InitializeComponent();
        }

        private void btnDoi_Click(object sender, EventArgs e)
        {
            string[] name = { "@login", "@password", "@coso" };
            object[] param = { textBox3.Text.Trim().ToUpper(), textBox2.Text, DBAccess.donVi };
            if (textBox2.Text.Trim() == "" )
            {
                MessageBox.Show("Mật khẩu phải chứa kí tự hoặc số.");
                return;
            }
            else if (textBox2.Text != textBox1.Text)
            {
                MessageBox.Show("Mật khẩu ở 2 ô phải giống nhau ");
                return;
            }
            int code;
            if (comboBox1.Text=="Cơ sở")
             code = DBAccess.ExecuteNonQuery("SP_ThemTaiKhoanCoSo", name, param, 3);
            else 
                code= DBAccess.ExecuteNonQuery("SP_ThemTaiKhoanGiaoVien", name, param, 3);
            if (code == 0)
            {
                MessageBox.Show("Thêm tài khoản thành công");
                this.Close();
            }
            else
                MessageBox.Show("Thêm tài khoản thất bại");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
