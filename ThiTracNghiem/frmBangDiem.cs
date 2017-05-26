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
    public partial class frmBangDiem : Form
    {
        private DataTable lop;
        private DataTable monHoc;
        public frmBangDiem()
        {
            InitializeComponent();
        }

        private void frmBangDiem_Load(object sender, EventArgs e)
        {
            AddLopToCombo();
            AddMonHocToCombo();
            comboBox3.SelectedIndex = 0;
        }

        private void AddLopToCombo()
        {
            lop = DBAccess.ExecuteQuery("SP_LayLopDonGian");
            comboBox1.Items.Clear();
            foreach (DataRow s in lop.Rows)
            {
                comboBox1.Items.Add(s[1]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void AddMonHocToCombo()
        {
            monHoc = DBAccess.ExecuteQuery("SP_LayMonHoc");
            comboBox2.Items.Clear();
            foreach (DataRow s in monHoc.Rows)
            {
                comboBox2.Items.Add(s[1]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Report.BangDiem obj = new Report.BangDiem();
            string maLop = lop.Select(string.Format("tenlop ='{0}'", comboBox1.Text.Trim()))[0][0].ToString();
            string maMH = monHoc.Select(string.Format("[Tên môn học] ='{0}'", comboBox2.Text.Trim()))[0][0].ToString();
            int lan = Convert.ToInt32(comboBox3.Text);
            string[] name = { "@mamh", "@malop", "@lan" };
            object[] param = { maMH, maLop, lan };
            DataTable bangDiem = DBAccess.ExecuteQuery("SP_LayBangDiem", name, param, 3);

            obj.SetDataSource(bangDiem);
            obj.SetParameterValue("@malop", maLop);
            obj.SetParameterValue("@mamh", maMH);
            obj.SetParameterValue("@lan", lan);
            obj.SetParameterValue("tengv", DBAccess.hoTen);
            crptView.ReportSource = obj;
        }

    }
}
