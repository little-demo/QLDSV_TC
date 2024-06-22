using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            HienThiMenu();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType().Equals(ftype))
                {
                    return f;
                }
            }
            return null;
        }

        public void HienThiMenu()
        {
            MANV.Text = "Mã GV = " + Program.username;
            HOTEN.Text = "Họ tên = " + Program.mHoten;
            NHOM.Text = "Nhóm = " + Program.mGroup;
            //Phân quyền
            rb_DanhMuc.Visible = rib_BaoCao.Visible = true;

            //tiếp tục if trên ProgramGroup để bật tắt menu quyền hạn tương ứng
            if (Program.mGroup.Equals("SV"))
            {
                btnDangKi.Enabled = true;
                btnDiem.Enabled = btnHocPhi.Enabled = btnLopHoc.Enabled = btnLopTC.Enabled = btnMonHoc.Enabled = btnSinhVien.Enabled = false;
                btnDssvLTC.Enabled = btnDsLTC.Enabled = btnBangDiemMon.Enabled = btnDsHocPhi.Enabled = barButtonItem8.Enabled = false;
                barButtonItem10.Enabled = false;
            }
            if (Program.mGroup.Equals("PKT"))
            {
                btnHocPhi.Enabled = true;
                btnDiem.Enabled = btnLopHoc.Enabled = btnLopTC.Enabled = btnMonHoc.Enabled = btnSinhVien.Enabled = btnDangKi.Enabled = false;
                btnDssvLTC.Enabled = btnDsLTC.Enabled = btnBangDiemMon.Enabled = btnPhieuDiemSV.Enabled = barButtonItem8.Enabled = false;
            }
            if (Program.mGroup.Equals("PGV") || Program.mGroup.Equals("KHOA"))
            {
                btnDangKi.Enabled = btnHocPhi.Enabled = false;
                btnDsHocPhi.Enabled = false;
            }
        }

        private void btnDangNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDangNhap));
            if (frm != null) frm.Activate();
            else
            {
                frmDangNhap loginForm = new frmDangNhap();
                loginForm.MdiParent = this;
                loginForm.Show();
            }
        }

        private void btnLopHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formLopHoc));
            if (frm != null) frm.Activate();
            else
            {
                formLopHoc f = new formLopHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnMonHoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formMonHoc));
            if (frm != null) frm.Activate();
            else
            {
                formMonHoc f = new formMonHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDangKi_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formDangKyLTC));
            if (frm != null) frm.Activate();
            else
            {
                formDangKyLTC f = new formDangKyLTC();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDiem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formNhapDiemSV));
            if (frm != null) frm.Activate();
            else
            {
                formNhapDiemSV f = new formNhapDiemSV();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDsLTC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formInDSLTC));
            if (frm != null) frm.Activate();
            else
            {
                formInDSLTC f = new formInDSLTC();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDssvLTC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formInSVDKLTC));
            if (frm != null) frm.Activate();
            else
            {
                formInSVDKLTC f = new formInSVDKLTC();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnPhieuDiemSV_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formInDiemSV));
            if (frm != null) frm.Activate();
            else
            {
                formInDiemSV f = new formInDiemSV();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDsHocPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formInDSDongHP));
            if (frm != null) frm.Activate();
            else
            {
                formInDSDongHP f = new formInDSDongHP();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formInBDTK));
            if (frm != null) frm.Activate();
            else
            {
                formInBDTK f = new formInBDTK();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnSinhVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formSinhVien));
            if (frm != null) frm.Activate();
            else
            {
                formSinhVien f = new formSinhVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnLopTC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formLopTinChi));
            if (frm != null) frm.Activate();
            else
            {
                formLopTinChi f = new formLopTinChi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnHocPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formHocPhi));
            if (frm != null) frm.Activate();
            else
            {
                formHocPhi f = new formHocPhi();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnBangDiemMon_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formInBDMH));
            if (frm != null) frm.Activate();
            else
            {
                formInBDMH f = new formInBDMH();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(formTaoLogin));
            if (frm != null) frm.Activate();
            else
            {
                formTaoLogin f = new formTaoLogin();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
            Form frmDangNhap = new frmDangNhap();

            this.Close();
        }
    }
}