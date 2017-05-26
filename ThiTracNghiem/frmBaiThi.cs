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
    public partial class frmBaiThi : Form
    {
        private DataTable monHoc;
        private DataTable sinhVien;
        public frmBaiThi()
        {
            InitializeComponent();
        }

        private void frmBaiThi_Load(object sender, EventArgs e)
        {
            AddMonHocToCombo();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            if (DBAccess.nhom == "Sinh viên")
            {
                comboBox1.Items.Add(DBAccess.hoTen);
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                AddSinhVienToCombo();
                comboBox1.SelectedIndex = 0;
            }
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

        private void AddSinhVienToCombo()
        {
            sinhVien = DBAccess.ExecuteQuery("select masv, concat(ho,' ',ten) as hoten from sinhvien",cmdType:CommandType.Text);
            comboBox1.Items.Clear();
            foreach (DataRow s in sinhVien.Rows)
            {
                comboBox1.Items.Add(s[1]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Report.BaiThi obj = new Report.BaiThi();
            string maMH = monHoc.Select(string.Format("[Tên môn học] ='{0}'", comboBox2.Text.Trim()))[0][0].ToString();
            string maSV, hoten;
            if (DBAccess.nhom == "Sinh viên")
            {
                maSV = DBAccess.id;
                hoten = DBAccess.hoTen;
            }
            else
            {
                maSV = sinhVien.Select(string.Format("hoten ='{0}'", comboBox1.Text.Trim()))[0][0].ToString();
                hoten = comboBox1.Text.Trim();
            }

            int lan = Convert.ToInt32(comboBox3.Text);
            string[] name = { "@masv", "@mamh", "@lan" };
            object[] param = { maSV, maMH, lan };
            DataTable bangDiem = DBAccess.ExecuteQuery("SP_LayBaiThi", name, param, 3);

            obj.SetDataSource(bangDiem);
            obj.SetParameterValue("@masv", maSV);
            obj.SetParameterValue("ma sv", maSV);
            obj.SetParameterValue("ten", hoten);
            obj.SetParameterValue("tenmh", comboBox2.Text.Trim());
            obj.SetParameterValue("@mamh", maMH);
            obj.SetParameterValue("@lan", lan);
            crptView.ReportSource = obj;
        }

    }
}
