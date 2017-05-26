using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem.PL
{
    public partial class ucCBThi2 : UserControl
    {
        private frmThi frmThi;
        public ucCBThi2(frmThi frmThi)
        {
            InitializeComponent();
            this.frmThi = frmThi;
        }

        private void ucCBThi2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            btnTim.PerformClick();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string[] name = { "@ngaythi", "@masv", "@malop" };
            object[] param = { dateTimePicker1.Value.Date, DBAccess.id, DBAccess.donVi };
            DataTable lichThi = DBAccess.ExecuteQuery("SP_LayLichThi", name, param, 3);
            gridControl1.DataSource = lichThi;
            FocusedRowChanged();
            if (dateTimePicker1.Value.Date < DateTime.Today)
            {
                btnThi.Enabled = false;
            }

        }

        private void btnThi_Click(object sender, EventArgs e)
        {
            DataRow red = gridView1.GetFocusedDataRow();
            UCThi ucThi = new UCThi(red["Mã môn học"].ToString(), red["Trình độ"].ToString()[0],
                Int32.Parse(red["Thời gian"].ToString()), Int32.Parse(red["Số câu thi"].ToString()),
                Int32.Parse(red["Lần"].ToString()), dateTimePicker1.Value.Date)
            {
                Dock = DockStyle.Fill
            };
            frmThi.ReplaceControl(ucThi);
        }

        private void FocusedRowChanged()
        {
            if (gridView1.RowCount == 0)
            {
                btnThi.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                textBox1.Text = string.Format
                    ("Mã giáo viên:{0}\r\nMã môn học:{1}\r\nTên môn học:{2}\r\nMã lớp:{3}\r\nTrình độ:{4}" +
                    "\r\nNgày thi:{5}\r\nLần:{6}\r\nSố câu thi:{7}\r\nThời gian:{8} phút",
                    red["Mã giáo viên"], red["Mã môn học"], red["Tên môn học"], red["Mã lớp"], red["Trình độ"],
                    String.Format("{0:dd-MM-yyyy}", red["Ngày thi"]), red["Lần"], red["Số câu thi"], red["Thời gian"]);
                btnThi.Enabled = true;
            }
        }
    }
}
