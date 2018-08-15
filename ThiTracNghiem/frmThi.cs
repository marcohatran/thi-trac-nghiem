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
    public partial class frmThi : Form
    {
        public frmThi()
        {
            InitializeComponent();
        }

        public void ReplaceControl(UserControl uc)
        {
            this.Controls.Clear();
            this.Controls.Add(uc);
        }

        private void frmThi_Load(object sender, EventArgs e)
        {
            UCCBThi ucChuanBi = new UCCBThi(this) { Dock = DockStyle.Fill };
            this.Controls.Add(ucChuanBi);
        }
    }
}
