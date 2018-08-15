using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ThiTracNghiem.CMD;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem
{
    public partial class frmDe : Form
    {
        private DeDAO deDAO;
        private DataTable monHoc;
        private DataTable giaoVien;
        private bool isThem;
        public frmDe()
        {
            InitializeComponent();
        }

        private void frmDe_Load(object sender, EventArgs e)
        {
            btnReload.PerformClick();
            deDAO = new DeDAO();
            switch (DBAccess.nhom)
            {
                case "giao vien":
                    comboBox4.Items.Add(DBAccess.hoTen);
                    comboBox4.SelectedIndex = 0;
                    comboBox4.Enabled = false;
                    break;
                case "Truong":
                    btnThem.Enabled = false;
                    btnXoa.Enabled = false;
                    /*comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox5.Enabled = false;*/
                    break;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            isThem = true;
        }

        private void AddMonHocToCombo()
        {
            monHoc = DBAccess.ExecuteQuery("select mamh, tenmh from monhoc", cmdType: CommandType.Text);
            if (monHoc == null)
            {
                MessageBox.Show("Lỗi CDSL! Không thể lấy danh sách các môn học");
                return;
            }
            else if (monHoc.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có môn học nào. Vui lòng nhập thêm môn học trước khi nhập đề.");
                return;
            }

            comboBox1.Items.Clear();
            foreach (DataRow s in monHoc.Rows)
            {
                comboBox1.Items.Add(s[1]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void AddGiaoVienToCombo()
        {
            giaoVien = DBAccess.ExecuteQuery("select magv, concat(ho,' ',ten) as hoten from giaovien", cmdType: CommandType.Text);
            if (giaoVien == null)
            {
                MessageBox.Show("Lỗi CDSL! Không thể lấy danh sách giáo viên");
                return;
            }
            else if (giaoVien.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có giảng viên nào. Vui lòng nhập thêm giảng viên trước khi nhập đề.");
                return;
            }

            comboBox4.Items.Clear();
            foreach (DataRow s in giaoVien.Rows)
            {
                comboBox4.Items.Add(s[1]);
            }
            comboBox4.SelectedIndex = 0;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa.Enabled = false;
            DataRow red = gridView1.GetFocusedDataRow();
            string maGVTrongBang;
            if (DBAccess.nhom == "giao vien")
            {
                maGVTrongBang = DBAccess.id;
            }
            else
            {
                maGVTrongBang = giaoVien.Select(string.Format("hoten ='{0}'", red["Giáo viên"].ToString()))[0][0].ToString();
            }
            DDe de = new DDe
            {
                CauHoi = Int32.Parse(red["Câu hỏi"].ToString()),
                NoiDung = red["Nội dung"].ToString(),
                A = red["A"].ToString(),
                B = red["B"].ToString(),
                C = red["C"].ToString(),
                D = red["D"].ToString(),
                MaMonHoc = monHoc.Select(string.Format("tenmh ='{0}'", red["Môn học"].ToString()))[0][0].ToString(),
                TrinhDo = red["Trình độ"].ToString()[0],
                DapAn = red["Đáp án"].ToString()[0],
                MaGV = maGVTrongBang
            };

            int code = deDAO.RemoveDe(de);
            if (code == 0)
            {
                //MessageBox.Show("Xoá câu hỏi thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá câu hỏi thất bại.");
            btnXoa.Enabled = true;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                errors += "\r\n+ Nội dung câu hỏi bị bỏ trống";
                noError++;
            }
            if (textBox2.Text.Trim() == "")
            {
                errors += "\r\n+ Đáp án A bị bỏ trống";
                noError++;
            }
            if (textBox3.Text.Trim() == "")
            {
                errors += "\r\n+ Đáp án B bị bỏ trống";
                noError++;
            }
            if (textBox4.Text.Trim() == "")
            {
                errors += "\r\n+ Đáp án C bị bỏ trống";
                noError++;
            }
            if (textBox5.Text.Trim() == "")
            {
                errors += "\r\n+ Đáp án D bị bỏ trống";
                noError++;
            }
            if (comboBox1.Text.Trim() == "")
            {
                errors += "\r\n+ Tên môn học bị bỏ trống.\r\nVui lòng nhập thêm môn học hoặc kiểm tra lại CSDL";
                noError++;
            }
            if (noError > 0)
            {
                MessageBox.Show(errors);
                btnLuu.Enabled = true;
                return;
            }

            if (isThem)
            {
                string maGVTrongForm;
                if (DBAccess.nhom == "giao vien")
                {
                    maGVTrongForm = DBAccess.id;
                }
                else
                {
                    maGVTrongForm = giaoVien.Select(string.Format("hoten ='{0}'", comboBox4.Text))[0][0].ToString();
                }
                DDe de = new DDe
                {
                    NoiDung = textBox1.Text.Trim(),
                    A = textBox2.Text.Trim(),
                    B = textBox3.Text.Trim(),
                    C = textBox4.Text.Trim(),
                    D = textBox5.Text.Trim(),
                    MaMonHoc = monHoc.Select(string.Format("tenmh ='{0}'", comboBox1.Text.Trim()))[0][0].ToString(),
                    TrinhDo = comboBox2.Text[0],
                    DapAn = comboBox3.Text[0],
                    MaGV = maGVTrongForm
                };
                int code = deDAO.CreateDe(de);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    isThem = false;
                }
                else
                {
                    MessageBox.Show("Thêm câu hoi thất bại");
                }
                btnLuu.Enabled = true;
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                string maGVTrongBang;
                string maGVTrongForm;
                if (DBAccess.nhom == "giao vien")
                {
                    maGVTrongBang = DBAccess.id;
                }
                else
                {
                    maGVTrongBang = giaoVien.Select(string.Format("hoten ='{0}'", red["Giáo viên"].ToString()))[0][0].ToString();
                }
                if (DBAccess.nhom == "giao vien")
                {
                    maGVTrongForm = DBAccess.id;
                }
                else
                {
                    maGVTrongForm = giaoVien.Select(string.Format("hoten ='{0}'", comboBox4.Text))[0][0].ToString();
                }
                DDe de = new DDe
                {
                    CauHoi = Int32.Parse(red["Câu hỏi"].ToString()),
                    NoiDung = textBox1.Text.Trim(),
                    A = textBox2.Text.Trim(),
                    B = textBox3.Text.Trim(),
                    C = textBox4.Text.Trim(),
                    D = textBox5.Text.Trim(),
                    MaMonHoc = monHoc.Select(string.Format("tenmh ='{0}'", comboBox1.Text))[0][0].ToString(),
                    TrinhDo = comboBox2.Text[0],
                    DapAn = comboBox3.Text[0],
                    MaGV = maGVTrongForm
                };
                int code = deDAO.UpdateDe(de);
                if (code == 0)
                {
                    btnReload.PerformClick();
                    //MessageBox.Show("Lưu câu hỏi thành công");
                }
                else
                {
                    MessageBox.Show("Lưu câu hỏi thất bại");
                }
            }
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DBAccess.nhom == "giao vien")
            {
                string[] name = { "@magv" };
                object[] param = { DBAccess.id };
                DataTable monHoc = DBAccess.ExecuteQuery("SP_LayDe", name, param, 1);
                gridControl1.DataSource = monHoc;
            }
            else
            {
                DataTable monHoc = DBAccess.ExecuteQuery("SP_LayDe");
                gridControl1.DataSource = monHoc;
                AddGiaoVienToCombo();
            }
            AddMonHocToCombo();
            if (gridView1.RowCount == 0)
            {
                comboBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = false;
            }
            else
            {
                FocusedRowChanged();
                comboBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = true;
            }
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnHuyBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
            else
            {
                /*textBox1.Text = gridView1.GetFocusedRowCellValue("Nội dung").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("A").ToString();
                textBox3.Text = gridView1.GetFocusedRowCellValue("B").ToString();
                textBox4.Text = gridView1.GetFocusedRowCellValue("C").ToString();
                textBox5.Text = gridView1.GetFocusedRowCellValue("D").ToString();
                comboBox1.Text = gridView1.GetFocusedRowCellValue("Môn học").ToString();
                comboBox2.Text = gridView1.GetFocusedRowCellValue("Trình độ").ToString();
                comboBox3.Text = gridView1.GetFocusedRowCellValue("Đáp án").ToString();
                comboBox4.Text = gridView1.GetFocusedRowCellValue("Giảng viên").ToString();*/
                FocusedRowChanged();
            }
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void FocusedRowChanged()
        {
            if (gridView1.RowCount > 0)
            {
                DataRow red = gridView1.GetFocusedDataRow();
                textBox1.Text = red["Nội dung"].ToString();
                textBox2.Text = red["A"].ToString();
                textBox3.Text = red["B"].ToString();
                textBox4.Text = red["C"].ToString();
                textBox5.Text = red["D"].ToString();
                comboBox1.Text = red["Môn học"].ToString();
                comboBox2.Text = red["Trình độ"].ToString();
                comboBox3.Text = red["Đáp án"].ToString();
                comboBox4.Text = red["Giáo viên"].ToString();
            }
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }
    }
}
