using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class formDangKyLTC : DevExpress.XtraEditors.XtraForm
    {
        private BindingSource bdsSinhVien = new BindingSource();
        private BindingSource bdsLopTinchi = new BindingSource();
        private BindingSource bdsDSLTC_HUY = new BindingSource();
        public formDangKyLTC()
        {
            InitializeComponent();
        }

        private void btnTimSV_Click(object sender, EventArgs e)
        {
            if (txbMaSv.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txbMaSv.Focus();
                return;
            }
            if (txbMaSv.Text != Program.username)
            {
                MessageBox.Show("Bạn không phải là tài khoản sinh viên này!", "", MessageBoxButtons.OK);
                txbMaSv.Focus();
                return;
            }
            txbMaSVDK.Text = txbMaSv.Text;
            string cmd = "EXEC dbo.SP_getInfoSVDKI '" + txbMaSv.Text + "'";
            string cmd1 = "EXEC dbo.SP_ListLTC_SVDuocHuyDK '" + txbMaSv.Text + "'";
            DataTable tableSV = Program.ExecSqlDataTable(cmd);
            DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);

            this.bdsSinhVien.DataSource = tableSV;
            this.bdsDSLTC_HUY.DataSource = tableDSLTC_HUY;
            this.gridSV.DataSource = this.bdsSinhVien;
            this.gridHuyLTC.DataSource = this.bdsDSLTC_HUY;
        }

        private void formDangKyLTC_Load(object sender, EventArgs e)
        {
            txbMaSv.Text = Program.username;
            loadcbNienkhoa();
            cbxNienKhoa.SelectedIndex = 0;
        }

        void loadcbNienkhoa()
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.GetAllNienKhoa";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsNienKhoa = new BindingSource();
            bdsNienKhoa.DataSource = dt;
            cbxNienKhoa.DataSource = bdsNienKhoa;
            cbxNienKhoa.DisplayMember = "NIENKHOA";
            cbxNienKhoa.ValueMember = "NIENKHOA";
        }
        void loadcbHocKi(string nienkhoa)
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.GetAllHocKy '" + nienkhoa + "'";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsHocKi = new BindingSource();
            bdsHocKi.DataSource = dt;
            cbxHocKi.DataSource = bdsHocKi;
            cbxHocKi.DisplayMember = "HOCKY";
            cbxHocKi.ValueMember = "HOCKY";
        }

        private void cbxNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcbHocKi(cbxNienKhoa.Text);
            cbxHocKi.SelectedIndex = 0;
        }

        private void btnTimNKHK_Click(object sender, EventArgs e)
        {
            string cmd = "EXEC [dbo].[SP_InDanhSachLopTinChi] '" + cbxNienKhoa.Text + "', '" + cbxHocKi.Text + "'";
            DataTable tableLopTC = Program.ExecSqlDataTable(cmd);
            this.bdsLopTinchi.DataSource = tableLopTC;
            this.gridLTC.DataSource = this.bdsLopTinchi;
        }

        private void gridLTC_MouseClick(object sender, MouseEventArgs e)
        {
            if (bdsLopTinchi.Count > 0)
            {
                txbMaLTCDK.Text = ((DataRowView)bdsLopTinchi[bdsLopTinchi.Position])["MALTC"].ToString();
            }
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if (txbMaSVDK.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txbMaSVDK.Focus();
                return;
            }
            if (txbMaLTCDK.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp tín chỉ không được thiếu!", "", MessageBoxButtons.OK);
                txbMaLTCDK.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng kí lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string cmd = "EXEC [dbo].[SP_XuLy_DKLTC] '" + txbMaSVDK.Text + "' , '" + txbMaLTCDK.Text + "', " + 1;
                if (Program.ExecSqlNonQuery(cmd) == 0)
                {
                    MessageBox.Show("Đăng kí thành công!");
                    string cmd1 = "EXEC [dbo].[SP_ListLTC_SVDuocHuyDK] '" + txbMaSv.Text + "'";
                    DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);
                    this.bdsDSLTC_HUY.DataSource = tableDSLTC_HUY;
                    this.gridHuyLTC.DataSource = this.bdsDSLTC_HUY;
                }
                else
                {
                    MessageBox.Show("Đăng kí thất bại");
                }
            }
            else
            {
                return;
            }
        }

        private void btnHuyDK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txbMaSVDK.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được thiếu!", "", MessageBoxButtons.OK);
                txbMaSVDK.Focus();
                return;
            }
            if (bdsDSLTC_HUY.Position < 0)
            {
                MessageBox.Show("Bạn chưa chọn lớp tín chỉ để hủy");
                gridHuyLTC.Focus();
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy đăng kí lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string maltc = "";
                if (((DataRowView)bdsDSLTC_HUY[bdsDSLTC_HUY.Position])["MALTC"] != null)
                {
                    maltc = ((DataRowView)bdsDSLTC_HUY[bdsDSLTC_HUY.Position])["MALTC"].ToString();
                }

                string cmd = "EXEC [dbo].[SP_XuLy_DKLTC] '" + txbMaSVDK.Text + "' , '" + maltc + "', " + 2;
                if (Program.ExecSqlNonQuery(cmd) == 0)
                {
                    MessageBox.Show("Hủy đăng kí thành công!");
                    string cmd1 = "EXEC [dbo].[SP_ListLTC_SVDuocHuyDK] '" + txbMaSv.Text + "'";
                    DataTable tableDSLTC_HUY = Program.ExecSqlDataTable(cmd1);
                    this.bdsDSLTC_HUY.DataSource = tableDSLTC_HUY;
                    this.gridHuyLTC.DataSource = this.bdsDSLTC_HUY;
                }
                else
                {
                    MessageBox.Show("Hủy đăng kí thất bại");
                }
            }
            else
            {
                return;
            }
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txbMaLTCDK.Text = "";
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}