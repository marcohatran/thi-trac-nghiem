using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiTracNghiem.PL;

namespace ThiTracNghiem
{
    public partial class frmThiThu : Form
    {
        public frmThiThu()
        {
            InitializeComponent();
        }

        private void frmThiThu_Load(object sender, EventArgs e)
        {
            UCCBiThiThu ucChuanBi = new UCCBiThiThu(this) { Dock=DockStyle.Fill };
            this.Controls.Add(ucChuanBi);
        }

        public void ReplaceControl(UserControl uc)
        {
            this.Controls.Clear();
            this.Controls.Add(uc);
        }
    }
}
