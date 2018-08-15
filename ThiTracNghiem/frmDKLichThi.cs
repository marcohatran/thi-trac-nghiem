using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThiTracNghiem.CMD;
using ThiTracNghiem.DTO;

namespace ThiTracNghiem
{
    public partial class frmDKLichThi : Form
    {
        private DKiThiDAO dKiThiDAO;
        private DataTable lop;
        private DataTable monHoc;
        private DataTable giaoVien;

        public frmDKLichThi()
        {
            InitializeComponent();
        }

        private void frmDKLichThi_Load(object sender, EventArgs e)
        {
            btnReload.PerformClick();
            dKiThiDAO = new DKiThiDAO();
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            switch (DBAccess.nhom)
            {
                case "giao vien":
                    comboBox2.Items.Add(DBAccess.hoTen);
                    comboBox2.SelectedIndex = 0;
                    comboBox2.Enabled = false;
                    break;
                case "Truong":
                    btnThem.Enabled = false;
                    btnXoa.Enabled = false;
                    break;
            }
        }

        private void AddLopToCombo()
        {
            lop = DBAccess.ExecuteQuery("SP_LayLopDonGian");
            if (lop == null)
            {
                MessageBox.Show("Lỗi CDSL! Không thể lấy danh sách các lớp");
                return;
            }
            else if (lop.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có lớp nào. Vui lòng nhập thêm lớp trước khi đăng ký lịch thi.");
                return;
            }

            comboBox3.Items.Clear();
            foreach (DataRow s in lop.Rows)
            {
                comboBox3.Items.Add(s[1]);
            }
            comboBox3.SelectedIndex = 0;
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

            comboBox2.Items.Clear();
            foreach (DataRow s in giaoVien.Rows)
            {
                comboBox2.Items.Add(s[1]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void AddMonHocToCombo()
        {
            monHoc = DBAccess.ExecuteQuery("SP_LayMonHoc");
            if (monHoc == null)
            {
                MessageBox.Show("Lỗi CDSL! Không thể lấy danh sách các môn học");
                return;
            }
            else if (monHoc.Rows.Count == 0)
            {
                MessageBox.Show("Trong CSDL không có môn học nào. Vui lòng nhập thêm môn học trước khi đăng ký lịch thi.");
                return;
            }

            comboBox1.Items.Clear();
            foreach (DataRow s in monHoc.Rows)
            {
                comboBox1.Items.Add(s[1]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            dateTimePicker1.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
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
            DGVDangKy dkThi = new DGVDangKy
            {
                TrinhDo = red["Trình độ"].ToString()[0],
                ThoiGian = Int32.Parse(red["Thời gian"].ToString()),
                SoCauThi = Int32.Parse(red["Số câu"].ToString()),
                NgayThi = DateTime.Parse(red["Ngày thi"].ToString()),
                Lan = Int32.Parse(red["Lần"].ToString()),
                MaLop = lop.Select(string.Format("tenlop ='{0}'", red["Tên lớp"].ToString()))[0][0].ToString(),
                MaMonHoc = monHoc.Select(string.Format("[Tên môn học] ='{0}'", red["Môn học"].ToString()))[0][0].ToString(),
                MaGV = maGVTrongBang
            };

            int code = dKiThiDAO.RemoveDangKy(dkThi);
            if (code == 0)
            {
                //MessageBox.Show("Xoá đăng ký thi thành công");
                btnReload.PerformClick();
            }
            else
                MessageBox.Show("Xoá thất bại.");
            btnXoa.Enabled = true;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnLuu.Enabled = false;
            int noError = 0;
            string errors = "Nội dung bạn nhập có 1 số lỗi sau. Vui lòng sửa trước khi lưu.";
            if (textBox1.Text.Trim() == "")
            {
                errors += "\r\n+ Thời gian không được bỏ trống";
                noError++;
            }
            else
            {
                int test;
                if (!int.TryParse(textBox1.Text.Trim(), out test))
                {
                    errors += "\r\n+ Nội dung ô thời gian không phải là số";
                    noError++;
                }
                else if (test < 15 || test > 60)
                {
                    errors += "\r\n+ Thời gian phải từ 15 đến 60 phút";
                    noError++;
                }
            }
            if (textBox2.Text.Trim() == "")
            {
                errors += "\r\n+ Số câu bị bỏ trống";
                noError++;
            }
            else
            {
                int test;
                if (!int.TryParse(textBox1.Text.Trim(), out test))
                {
                    errors += "\r\n+ Nội dung ô số câu thi không phải là số";
                    noError++;
                }
                else if (test < 10 || test > 100)
                {
                    errors += "\r\n+ Số câu thi phải từ 10 đến 100 câu";
                    noError++;
                }

            }
            if (comboBox1.Text.Trim() == "")
            {
                errors += "\r\n+ Tên môn học bị bỏ trống.\r\nVui lòng nhập thêm môn học hoặc kiểm tra lại CSDL";
                noError++;
            }
            if (comboBox3.Text.Trim() == "")
            {
                errors += "\r\n+ Tên lớp bị bỏ trống.\r\nVui lòng nhập thêm lớp hoặc kiểm tra lại CSDL";
                noError++;
            }
            if (dateTimePicker1.Value.Date <= DateTime.Today)
            {
                errors += "\r\n+ Chỉ được đăng ký lịch thi sau thời điểm hiện tại";
                noError++;
            }
            if (noError > 0)
            {
                MessageBox.Show(errors);
                btnLuu.Enabled = true;
                return;
            }


            if (comboBox1.Enabled)
            {
                string maGVTrongForm;
                if (DBAccess.nhom == "giao vien")
                {
                    maGVTrongForm = DBAccess.id;
                }
                else
                {
                    maGVTrongForm = giaoVien.Select(string.Format("hoten ='{0}'", comboBox2.Text))[0][0].ToString();
                }
                DGVDangKy dkThi = new DGVDangKy
                {
                    TrinhDo = comboBox4.Text[0],
                    ThoiGian = Int32.Parse(textBox1.Text.Trim()),
                    SoCauThi = Int32.Parse(textBox2.Text.Trim()),
                    NgayThi = dateTimePicker1.Value,
                    Lan = Int32.Parse(comboBox5.Text.Trim()),
                    MaLop = lop.Select(string.Format("tenlop ='{0}'", comboBox3.Text.Trim()))[0][0].ToString(),
                    MaMonHoc = monHoc.Select(string.Format("[Tên môn học] ='{0}'", comboBox1.Text.Trim()))[0][0].ToString(),
                    MaGV = maGVTrongForm
                };
                int code = dKiThiDAO.CreateDangKy(dkThi);
                if (code == 0)
                {
                    btnReload.PerformClick();
                }
                else if (code == 2)
                {
                    MessageBox.Show("Số câu hỏi trong ngân hàng đề thi ít hơn số câu hỏi thi dự kiến.");
                }
                else if (code == 3)
                {
                    MessageBox.Show("Môn học này chưa đăng ký thi lần 1.");
                }
                else if (code == 4)
                {
                    MessageBox.Show("Lần thi 2 phải đăng ký sau lần 1.");
                }
                else
                {
                    MessageBox.Show("Đăng ký lịch thi thất bại");
                }
            }
            else
            {
                DataRow red = gridView1.GetFocusedDataRow();
                string maGVTrongBang, maGVTrongForm;
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
                    maGVTrongForm = giaoVien.Select(string.Format("hoten ='{0}'", comboBox2.Text))[0][0].ToString();
                }
                DGVDangKy dkThi = new DGVDangKy
                {
                    TrinhDo = comboBox4.Text[0],
                    ThoiGian = Int32.Parse(textBox1.Text.Trim()),
                    SoCauThi = Int32.Parse(textBox2.Text.Trim()),
                    NgayThi = dateTimePicker1.Value,
                    Lan = Int32.Parse(comboBox5.Text.Trim()),
                    MaLop = lop.Select(string.Format("tenlop ='{0}'", comboBox3.Text.Trim()))[0][0].ToString(),
                    MaMonHoc = monHoc.Select(string.Format("[Tên môn học] ='{0}'", comboBox1.Text.Trim()))[0][0].ToString(),
                    MaGV = maGVTrongForm
                };

                int code = dKiThiDAO.UpdateDangKy(dkThi);
                if (code == 0)
                {
                    btnReload.PerformClick();
                }
                else if (code == 2)
                {
                    MessageBox.Show("Số câu hỏi trong ngân hàng đề thi ít hơn số câu hỏi thi dự kiến.");
                }
                else if (code == 3)
                {
                    MessageBox.Show("Lần thi 2 phải đăng ký sau lần 1.");
                }
                else
                {
                    MessageBox.Show("Cập nhật lịch thi thất bại");
                }
                btnReload.PerformClick();
            }
            btnLuu.Enabled = true;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DBAccess.nhom == "giao vien")
            {
                string[] name = { "@magv" };
                object[] param = { DBAccess.id };
                DataTable lop = DBAccess.ExecuteQuery("SP_LayLichThiGiangVien", name, param, 1);
                gridControl1.DataSource = lop;
            }
            else
            {
                DataTable lop = DBAccess.ExecuteQuery("SP_LayLichThiGiangVien");
                gridControl1.DataSource = lop;
                AddGiaoVienToCombo();
            }
            AddLopToCombo();
            AddMonHocToCombo();

            if (gridView1.RowCount == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                comboBox4.Enabled = false;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                comboBox4.Enabled = true;
                if (DBAccess.nhom != "Truong")
                    btnXoa.Enabled = true;
                FocusedRowChanged();
            }
            comboBox1.Enabled = false;
            comboBox3.Enabled = false;
            comboBox5.Enabled = false;

            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;

        }

        private void btnHuyBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                comboBox4.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                comboBox4.Enabled = true;

                comboBox4.Text = gridView1.GetFocusedRowCellValue("Trình độ").ToString();
                textBox1.Text = gridView1.GetFocusedRowCellValue("Thời gian").ToString();
                textBox2.Text = gridView1.GetFocusedRowCellValue("Số câu").ToString();
                dateTimePicker1.Value = DateTime.Parse(gridView1.GetFocusedRowCellValue("Ngày thi").ToString());
                comboBox5.Text = gridView1.GetFocusedRowCellValue("Lần").ToString();
                comboBox3.Text = gridView1.GetFocusedRowCellValue("Tên lớp").ToString();
                comboBox1.Text = gridView1.GetFocusedRowCellValue("Môn học").ToString();
            }

            comboBox1.Enabled = false;
            comboBox3.Enabled = false;
            comboBox5.Enabled = false;
            btnHuyBo.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void FocusedRowChanged()
        {
            if (gridView1.RowCount > 0)
            {
                DataRow red = gridView1.GetFocusedDataRow();
                comboBox4.Text = red["Trình độ"].ToString();
                textBox1.Text = red["Thời gian"].ToString();
                textBox2.Text = red["Số câu"].ToString();
                dateTimePicker1.Value = DateTime.Parse(red["Ngày thi"].ToString());
                comboBox5.Text = red["Lần"].ToString();
                comboBox3.Text = red["Tên lớp"].ToString();
                comboBox1.Text = red["Môn học"].ToString();
            }
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged();
            comboBox1.Enabled = false;
            comboBox3.Enabled = false;
            comboBox5.Enabled = false;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
        }
    }
}
