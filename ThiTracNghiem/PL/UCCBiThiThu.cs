using System;
using System.Data;
using System.Windows.Forms;

namespace ThiTracNghiem.PL
{
    public partial class UCCBiThiThu : UserControl
    {
        private DataTable monHoc;
        private frmThiThu frmThiThu;
        public UCCBiThiThu(frmThiThu frmThiThu)
        {
            InitializeComponent();
            this.frmThiThu = frmThiThu;
        }

        private void btnThi_Click(object sender, EventArgs e)
        {
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                //MessageBox.Show("Thời gian không được bỏ trống");
                errors += "\r\n+ Thời gian bị bỏ trống";
                noError++;
            }
            else
            {
                int test;
                if (!int.TryParse(textBox1.Text.Trim(), out test))
                {
                    //MessageBox.Show("Bạn phải nhập số vào ô thời gian");
                    errors += "\r\n+ Nội dung ô thời gian không phải là số";
                    noError++;
                }
                else if (test < 15 || test > 60)
                {
                    //MessageBox.Show("Thời gian phải từ 15 đến 60 phút");
                    errors += "\r\n+ Thời gian phải từ 15 đến 60 phút";
                    noError++;
                }
            }
            if (textBox2.Text.Trim() == "")
            {
                //MessageBox.Show("Số câu không được bỏ trống");
                errors += "\r\n+ Số câu bị bỏ trống";
                noError++;
            }
            else
            {
                int test;
                if (!int.TryParse(textBox1.Text.Trim(), out test))
                {
                    //MessageBox.Show("Bạn phải nhập số vào ô số câu");
                    errors += "\r\n+ Nội dung ô số câu không phải là số";
                    noError++;
                }
                else if (test < 10 || test > 100)
                {
                    //MessageBox.Show("Số câu thi phải từ 10 đến 100 câu");
                    errors += "\r\n+ Số câu thi phải từ 10 đến 100 câu";
                    noError++;
                }
            }
            if (noError > 0)
            {
                MessageBox.Show(errors);
                return;
            }

            int index = comboBox1.SelectedIndex;
            string maMH = monHoc.Rows[index][0].ToString();
            int thoiGian = Convert.ToInt32(textBox1.Text.Trim());
            int soCau = Convert.ToInt32(textBox2.Text.Trim());
            char trinhDo = comboBox2.Text[0];
            UCThiThu ucThiThu = new UCThiThu(maMH,trinhDo,thoiGian,soCau) { Dock = DockStyle.Fill };
            frmThiThu.ReplaceControl(ucThiThu);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            textBox1.Text = "0";
            textBox2.Text = "0";
        }

        private void UCCBiThiThu_Load(object sender, EventArgs e)
        {
            monHoc = DBAccess.ExecuteQuery("SP_LayMonHoc");
            foreach (DataRow s in monHoc.Rows)
            {
                comboBox1.Items.Add(s[1].ToString());
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
    }
}
