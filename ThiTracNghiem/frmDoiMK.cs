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
    public partial class frmDoiMK : Form
    {
        public frmDoiMK()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void btnDoi_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Mật khẩu phải chứa kí tự hoặc số. Không chứa khoảng trắng.");
                return;
            }
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Mật khẩu ở 2 ô phải giống nhau");
                return;
            }
            string[] name = { "@newpassword", "@oldpassword", "@login" };
            object[] param = { textBox1.Text, textBox3.Text, DBAccess.id };
            int code = DBAccess.ExecuteNonQuery("SP_DoiMatKhau", name, param, 3);
            if (code == 0)
            {
                MessageBox.Show("Đổi mật khẩu thành công");
                this.Close();
            }
            else
                MessageBox.Show("Đổi mật khẩu thất bại");
        }
    }
}
