namespace ThiTracNghiem
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnDangXuat = new DevExpress.XtraBars.BarButtonItem();
            this.btnThoat = new DevExpress.XtraBars.BarButtonItem();
            this.btnDangNhap = new DevExpress.XtraBars.BarButtonItem();
            this.btnMonHoc = new DevExpress.XtraBars.BarButtonItem();
            this.btnKhoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnLop = new DevExpress.XtraBars.BarButtonItem();
            this.btnGiaoVien = new DevExpress.XtraBars.BarButtonItem();
            this.btnSinhVien = new DevExpress.XtraBars.BarButtonItem();
            this.btnDe = new DevExpress.XtraBars.BarButtonItem();
            this.btnDKThi = new DevExpress.XtraBars.BarButtonItem();
            this.btnThiThu = new DevExpress.XtraBars.BarButtonItem();
            this.btnBangDiem = new DevExpress.XtraBars.BarButtonItem();
            this.btnThi = new DevExpress.XtraBars.BarButtonItem();
            this.btnBaiThi = new DevExpress.XtraBars.BarButtonItem();
            this.txtID = new DevExpress.XtraBars.BarStaticItem();
            this.txtHoTen = new DevExpress.XtraBars.BarStaticItem();
            this.txtNhom = new DevExpress.XtraBars.BarStaticItem();
            this.txtDonVi = new DevExpress.XtraBars.BarStaticItem();
            this.btnDoiMKNV = new DevExpress.XtraBars.BarButtonItem();
            this.btnThemTK = new DevExpress.XtraBars.BarButtonItem();
            this.pageQuanLy = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageGiaoVien = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageSinhVien = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pageHeThong = new DevExpress.XtraBars.Ribbon.RibbonPageCategory();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblID = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHoTen = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNhom = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMaDonVi = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDonVi = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnDangXuat,
            this.btnThoat,
            this.btnDangNhap,
            this.btnMonHoc,
            this.btnKhoa,
            this.btnLop,
            this.btnGiaoVien,
            this.btnSinhVien,
            this.btnDe,
            this.btnDKThi,
            this.btnThiThu,
            this.btnBangDiem,
            this.btnThi,
            this.btnBaiThi,
            this.txtID,
            this.txtHoTen,
            this.txtNhom,
            this.txtDonVi,
            this.btnDoiMKNV,
            this.btnThemTK});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 25;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageCategories.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageCategory[] {
            this.pageQuanLy,
            this.pageGiaoVien,
            this.pageSinhVien,
            this.pageHeThong});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.MacOffice;
            this.ribbonControl1.Size = new System.Drawing.Size(652, 128);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Caption = "Đăng xuất";
            this.btnDangXuat.Enabled = false;
            this.btnDangXuat.Id = 3;
            this.btnDangXuat.ImageUri.Uri = "DeleteDataSource";
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangXuat_ItemClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Caption = "Thoát";
            this.btnThoat.Id = 4;
            this.btnThoat.ImageUri.Uri = "Delete";
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThoat_ItemClick);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Caption = "Đăng nhập";
            this.btnDangNhap.CloseSubMenuOnClick = false;
            this.btnDangNhap.Id = 5;
            this.btnDangNhap.ImageUri.Uri = "AddNewDataSource";
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangNhap_ItemClick);
            // 
            // btnMonHoc
            // 
            this.btnMonHoc.Caption = "Quản lý môn học";
            this.btnMonHoc.Id = 6;
            this.btnMonHoc.ImageUri.Uri = "EditWrapPoints";
            this.btnMonHoc.Name = "btnMonHoc";
            this.btnMonHoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMonHoc_ItemClick);
            // 
            // btnKhoa
            // 
            this.btnKhoa.Caption = "Quản lý khoa";
            this.btnKhoa.Id = 7;
            this.btnKhoa.ImageUri.Uri = "Home";
            this.btnKhoa.Name = "btnKhoa";
            this.btnKhoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnKhoa_ItemClick);
            // 
            // btnLop
            // 
            this.btnLop.Caption = "Quản lý lớp";
            this.btnLop.Id = 8;
            this.btnLop.ImageUri.Uri = "NavigationBar";
            this.btnLop.Name = "btnLop";
            this.btnLop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLop_ItemClick);
            // 
            // btnGiaoVien
            // 
            this.btnGiaoVien.Caption = "Quản lý giáo viên";
            this.btnGiaoVien.Id = 9;
            this.btnGiaoVien.ImageUri.Uri = "Show";
            this.btnGiaoVien.Name = "btnGiaoVien";
            this.btnGiaoVien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGiaoVien_ItemClick);
            // 
            // btnSinhVien
            // 
            this.btnSinhVien.Caption = "Quản lý sinh viên";
            this.btnSinhVien.Id = 10;
            this.btnSinhVien.ImageUri.Uri = "EditDataSource";
            this.btnSinhVien.Name = "btnSinhVien";
            this.btnSinhVien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSinhVien_ItemClick);
            // 
            // btnDe
            // 
            this.btnDe.Caption = "Quản lý đề thi";
            this.btnDe.Id = 11;
            this.btnDe.ImageUri.Uri = "WrapText";
            this.btnDe.Name = "btnDe";
            this.btnDe.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDe_ItemClick);
            // 
            // btnDKThi
            // 
            this.btnDKThi.Caption = "Quản lý lịch thi";
            this.btnDKThi.Id = 12;
            this.btnDKThi.ImageUri.Uri = "MonthView";
            this.btnDKThi.Name = "btnDKThi";
            this.btnDKThi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDKThi_ItemClick);
            // 
            // btnThiThu
            // 
            this.btnThiThu.Caption = "Thi thử";
            this.btnThiThu.Id = 13;
            this.btnThiThu.ImageUri.Uri = "SplitAppointment";
            this.btnThiThu.Name = "btnThiThu";
            this.btnThiThu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThiThu_ItemClick);
            // 
            // btnBangDiem
            // 
            this.btnBangDiem.Caption = "Bảng điểm";
            this.btnBangDiem.Id = 14;
            this.btnBangDiem.ImageUri.Uri = "Chart";
            this.btnBangDiem.Name = "btnBangDiem";
            this.btnBangDiem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBangDiem_ItemClick);
            // 
            // btnThi
            // 
            this.btnThi.Caption = "Thi";
            this.btnThi.Id = 15;
            this.btnThi.ImageUri.Uri = "EditWrapPoints";
            this.btnThi.Name = "btnThi";
            this.btnThi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThi_ItemClick);
            // 
            // btnBaiThi
            // 
            this.btnBaiThi.Caption = "Xem bài thi";
            this.btnBaiThi.Id = 16;
            this.btnBaiThi.ImageUri.Uri = "InLineWithText";
            this.btnBaiThi.Name = "btnBaiThi";
            this.btnBaiThi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBaiThi_ItemClick);
            // 
            // txtID
            // 
            this.txtID.Caption = "Tài khoản";
            this.txtID.Id = 17;
            this.txtID.Name = "txtID";
            this.txtID.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Caption = "Họ tên";
            this.txtHoTen.Id = 19;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // txtNhom
            // 
            this.txtNhom.Caption = "Nhóm";
            this.txtNhom.Id = 20;
            this.txtNhom.Name = "txtNhom";
            this.txtNhom.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Caption = "Đơn vị";
            this.txtDonVi.Id = 21;
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btnDoiMKNV
            // 
            this.btnDoiMKNV.Caption = "Đổi mật khẩu";
            this.btnDoiMKNV.Enabled = false;
            this.btnDoiMKNV.Id = 22;
            this.btnDoiMKNV.ImageUri.Uri = "Customization";
            this.btnDoiMKNV.Name = "btnDoiMKNV";
            this.btnDoiMKNV.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDoiMKNV_ItemClick);
            // 
            // btnThemTK
            // 
            this.btnThemTK.Caption = "Thêm tài khoản cơ sở";
            this.btnThemTK.Enabled = false;
            this.btnThemTK.Id = 24;
            this.btnThemTK.ImageUri.Uri = "AddNewDataSource";
            this.btnThemTK.Name = "btnThemTK";
            this.btnThemTK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThemTK_ItemClick);
            // 
            // pageQuanLy
            // 
            this.pageQuanLy.Name = "pageQuanLy";
            this.pageQuanLy.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage2});
            this.pageQuanLy.Visible = false;
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4,
            this.ribbonPageGroup6,
            this.ribbonPageGroup7,
            this.ribbonPageGroup10,
            this.ribbonPageGroup5});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Quản lý";
            this.ribbonPage2.Visible = false;
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.btnMonHoc);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.ShowCaptionButton = false;
            this.ribbonPageGroup4.Text = "Môn học";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.btnKhoa);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnLop);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.ShowCaptionButton = false;
            this.ribbonPageGroup6.Text = "Khoa, Lớp";
            // 
            // ribbonPageGroup7
            // 
            this.ribbonPageGroup7.ItemLinks.Add(this.btnGiaoVien);
            this.ribbonPageGroup7.Name = "ribbonPageGroup7";
            this.ribbonPageGroup7.ShowCaptionButton = false;
            this.ribbonPageGroup7.Text = "Giáo viên";
            // 
            // ribbonPageGroup10
            // 
            this.ribbonPageGroup10.ItemLinks.Add(this.btnSinhVien);
            this.ribbonPageGroup10.Name = "ribbonPageGroup10";
            this.ribbonPageGroup10.ShowCaptionButton = false;
            this.ribbonPageGroup10.Text = "Sinh viên";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.btnThemTK);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.ShowCaptionButton = false;
            this.ribbonPageGroup5.Text = "Tài khoản cơ sở";
            // 
            // pageGiaoVien
            // 
            this.pageGiaoVien.Name = "pageGiaoVien";
            this.pageGiaoVien.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage3});
            this.pageGiaoVien.Visible = false;
            // 
            // ribbonPage3
            // 
            this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3,
            this.ribbonPageGroup8,
            this.ribbonPageGroup9});
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "Giáo viên";
            this.ribbonPage3.Visible = false;
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnDe);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.ShowCaptionButton = false;
            this.ribbonPageGroup3.Text = "Đề thi";
            // 
            // ribbonPageGroup8
            // 
            this.ribbonPageGroup8.ItemLinks.Add(this.btnDKThi);
            this.ribbonPageGroup8.ItemLinks.Add(this.btnThiThu);
            this.ribbonPageGroup8.Name = "ribbonPageGroup8";
            this.ribbonPageGroup8.ShowCaptionButton = false;
            this.ribbonPageGroup8.Text = "Thi";
            // 
            // ribbonPageGroup9
            // 
            this.ribbonPageGroup9.ItemLinks.Add(this.btnBangDiem);
            this.ribbonPageGroup9.Name = "ribbonPageGroup9";
            this.ribbonPageGroup9.ShowCaptionButton = false;
            this.ribbonPageGroup9.Text = "Bảng điểm";
            // 
            // pageSinhVien
            // 
            this.pageSinhVien.Name = "pageSinhVien";
            this.pageSinhVien.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage4});
            this.pageSinhVien.Visible = false;
            // 
            // ribbonPage4
            // 
            this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPage4.Name = "ribbonPage4";
            this.ribbonPage4.Text = "Sinh Viên";
            this.ribbonPage4.Visible = false;
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnThi);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnBaiThi);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "Thi";
            // 
            // pageHeThong
            // 
            this.pageHeThong.Name = "pageHeThong";
            this.pageHeThong.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Hệ thống";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowMinimize = false;
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDangNhap);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDangXuat);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDoiMKNV);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnThoat);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Tài khoản";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblID,
            this.lblHoTen,
            this.lblNhom,
            this.lblMaDonVi,
            this.lblDonVi});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(652, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblID
            // 
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 17);
            // 
            // lblHoTen
            // 
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(0, 17);
            // 
            // lblNhom
            // 
            this.lblNhom.Name = "lblNhom";
            this.lblNhom.Size = new System.Drawing.Size(0, 17);
            // 
            // lblMaDonVi
            // 
            this.lblMaDonVi.Name = "lblMaDonVi";
            this.lblMaDonVi.Size = new System.Drawing.Size(0, 17);
            // 
            // lblDonVi
            // 
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(0, 17);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 404);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "Chương trình quản lý thi trắc nghiệm";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnDangXuat;
        private DevExpress.XtraBars.BarButtonItem btnThoat;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageQuanLy;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageGiaoVien;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageSinhVien;
        private DevExpress.XtraBars.Ribbon.RibbonPageCategory pageHeThong;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnDangNhap;
        private DevExpress.XtraBars.BarButtonItem btnMonHoc;
        private DevExpress.XtraBars.BarButtonItem btnKhoa;
        private DevExpress.XtraBars.BarButtonItem btnLop;
        private DevExpress.XtraBars.BarButtonItem btnGiaoVien;
        private DevExpress.XtraBars.BarButtonItem btnSinhVien;
        private DevExpress.XtraBars.BarButtonItem btnDe;
        private DevExpress.XtraBars.BarButtonItem btnDKThi;
        private DevExpress.XtraBars.BarButtonItem btnThiThu;
        private DevExpress.XtraBars.BarButtonItem btnBangDiem;
        private DevExpress.XtraBars.BarButtonItem btnThi;
        private DevExpress.XtraBars.BarButtonItem btnBaiThi;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup7;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup10;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup8;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup9;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarStaticItem txtID;
        private DevExpress.XtraBars.BarStaticItem txtHoTen;
        private DevExpress.XtraBars.BarStaticItem txtNhom;
        private DevExpress.XtraBars.BarStaticItem txtDonVi;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblID;
        private System.Windows.Forms.ToolStripStatusLabel lblHoTen;
        private System.Windows.Forms.ToolStripStatusLabel lblDonVi;
        private System.Windows.Forms.ToolStripStatusLabel lblMaDonVi;
        private System.Windows.Forms.ToolStripStatusLabel lblNhom;
        private DevExpress.XtraBars.BarButtonItem btnDoiMKNV;
        private DevExpress.XtraBars.BarButtonItem btnThemTK;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
    }
}