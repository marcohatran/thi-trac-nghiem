using System;
using System.Collections.Generic;

using System.Windows.Forms;
using ThiTracNghiem.DTO;
using System.Data;

namespace ThiTracNghiem.PL
{
    public partial class UCThiThu : DevExpress.XtraEditors.XtraUserControl
    {
        private int minute;
        private int second;

        private int thoiGian;
        private string maMH;
        private char trinhDo;
        private int soCau;

        private int cauHoi;

        private int soCauDung;

        private List<DBaiThi> baiThi;

        public UCThiThu(string maMH, char trinhDo, int thoiGian, int soCau)
        {
            InitializeComponent();
            this.maMH = maMH;
            this.trinhDo = trinhDo;
            this.thoiGian = thoiGian;
            this.soCau = soCau;
            baiThi = new List<DBaiThi>();
        }

        private void UCThiThu_Load(object sender, EventArgs e)
        {
            minute = 0;
            second = 0;
            soCauDung = 0;
            cauHoi = 0;
            LayDe();
            ThemSoCau();
            LoadCauHoi(cauHoi);
            textBox1.Width = textBox1.Parent.Width;
        }

        private List<DBaiThi> LayDeThi(string maMH, char trinhDo, int socau)
        {
            List<DBaiThi> deThi = new List<DBaiThi>();
            string[] name = { "@mamh", "@trinhdo", "@socau"};
            object[] param = { maMH, trinhDo, socau };
            DataTable dt = DBAccess.ExecuteQuery("SP_LayDeThi", name, param, 3);
            foreach (DataRow dr in dt.Rows)
            {
                DBaiThi db = new DBaiThi
                {
                    NoiDung = dr[0].ToString(),
                    A = dr[1].ToString(),
                    B = dr[2].ToString(),
                    C = dr[3].ToString(),
                    D = dr[4].ToString(),
                    DapAn = dr[5].ToString()[0]
                };
                deThi.Add(db);
            }
            return deThi;
        }


        private void LayDe()
        {
            List<DBaiThi> dt = LayDeThi(this.maMH, this.trinhDo, this.soCau);
            foreach (DBaiThi bt in dt)
            {
                List<char> dapAn = new List<char> { 'A', 'B', 'C', 'D' };
                string[] dapAnTron = new string[4];
                char da = 'A';
                Random rnd = new Random();
                for (int i = 0; i < 4; i++)
                {
                    char d = dapAn[rnd.Next(dapAn.Count)];
                    dapAn.Remove(d);
                    switch (d)
                    {
                        case 'A':
                            dapAnTron[i] = bt.A;
                            if (bt.DapAn == 'A')
                                da += (char)i;
                            break;
                        case 'B':
                            dapAnTron[i] = bt.B;
                            if (bt.DapAn == 'B')
                                da += (char)i;
                            break;
                        case 'C':
                            dapAnTron[i] = bt.C;
                            if (bt.DapAn == 'C')
                                da += (char)i;
                            break;
                        case 'D':
                            dapAnTron[i] = bt.D;
                            if (bt.DapAn == 'D')
                                da += (char)i;
                            break;
                    }
                }
                DBaiThi ndt = new DBaiThi
                {
                    NoiDung = bt.NoiDung,
                    DapAn = da,
                    A = dapAnTron[0],
                    B = dapAnTron[1],
                    C = dapAnTron[2],
                    D = dapAnTron[3]
                };
                baiThi.Add(ndt);
            }
        }

        private void ThemSoCau()
        {
            for (int i = 1; i <= baiThi.Count; i++)
                comboCauHoi.Items.Add(i);
            comboCauHoi.SelectedIndex = 0;
        }

        private void LoadCauHoi(int i)
        {
            textBox1.Text = baiThi[i].NoiDung;
            lblA.Text = baiThi[i].A;
            lblB.Text = baiThi[i].B;
            lblC.Text = baiThi[i].C;
            lblD.Text = baiThi[i].D;
            label5.Text = baiThi[i].DapAn.ToString();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            switch (baiThi[i].Chon)
            {
                case 'A':
                    radioButton1.Checked = true;
                    break;
                case 'B':
                    radioButton2.Checked = true;
                    break;
                case 'C':
                    radioButton3.Checked = true;
                    break;
                case 'D':
                    radioButton4.Checked = true;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second++;
            if (second == 60)
            {
                second = 0;
                minute++;
            }
            if (minute == thoiGian)
            {
                timer1.Enabled = false;
                btnNopBai.Enabled = false;
                float diem = (float)soCauDung / soCau * 10;
                MessageBox.Show("Số điểm của bạn: " + diem.ToString("0.00"));
            }
            else
            {
                label3.Text = String.Format("{0:00}:{1:00}", minute, second);
            }
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            LoadCauHoi(--cauHoi);
            btnSau.Enabled = true;
            if (cauHoi == 0)
                btnTruoc.Enabled = false;
            comboCauHoi.SelectedIndex = cauHoi;
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            LoadCauHoi(cauHoi++);
            btnTruoc.Enabled = true;
            if (cauHoi == soCau - 1)
                btnSau.Enabled = false;
            comboCauHoi.SelectedIndex = cauHoi;
        }

        private void comboCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cauHoi = comboCauHoi.SelectedIndex;
            LoadCauHoi(cauHoi);
            if (cauHoi == 0)
                btnTruoc.Enabled = false;
            else
                btnTruoc.Enabled = true;
            if (cauHoi == soCau - 1)
                btnSau.Enabled = false;
            else
                btnSau.Enabled = true;
        }

        private void btnNopBai_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnNopBai.Enabled = false;
            float diem = (float)soCauDung / soCau * 10;
            MessageBox.Show("Số điểm của bạn: " + diem.ToString("0.00"));
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (baiThi[cauHoi].DapAn == 'A' && baiThi[cauHoi].Chon != 'A')
                soCauDung++;
            if (baiThi[cauHoi].DapAn != 'A' && baiThi[cauHoi].Chon == baiThi[cauHoi].DapAn)
                soCauDung--;
            baiThi[cauHoi].Chon = 'A';
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (baiThi[cauHoi].DapAn == 'B' && baiThi[cauHoi].Chon != 'B')
                soCauDung++;
            if (baiThi[cauHoi].DapAn != 'B' && baiThi[cauHoi].Chon == baiThi[cauHoi].DapAn)
                soCauDung--;
            baiThi[cauHoi].Chon = 'B';
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (baiThi[cauHoi].DapAn == 'C' && baiThi[cauHoi].Chon != 'C')
                soCauDung++;
            if (baiThi[cauHoi].DapAn != 'C' && baiThi[cauHoi].Chon == baiThi[cauHoi].DapAn)
                soCauDung--;
            baiThi[cauHoi].Chon = 'C';
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (baiThi[cauHoi].DapAn == 'D' && baiThi[cauHoi].Chon != 'D')
                soCauDung++;
            if (baiThi[cauHoi].DapAn != 'D' && baiThi[cauHoi].Chon == baiThi[cauHoi].DapAn)
                soCauDung--;
            baiThi[cauHoi].Chon = 'D';
        }
    }
}
