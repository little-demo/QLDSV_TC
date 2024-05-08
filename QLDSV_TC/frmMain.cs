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
            PhanQuyen();
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
            if (Program.mGroup == "PGV")
            {
                rib_BaoCao.Visible = false;
            } else
            {

            }
        }

        public void PhanQuyen()
        {
            
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

    }
}