using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class formLopTinChi : DevExpress.XtraEditors.XtraForm
    {
        private SqlConnection conn_publisher = new SqlConnection();
        int vitri = 0;
        string macn = "";
        public formLopTinChi()
        {
            InitializeComponent();
            
        }


        private void formLopTinChi_Load(object sender, EventArgs e)
        {
            
            DS.EnforceConstraints = false;

            this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTINCHITableAdapter.Fill(this.DS.LOPTINCHI);

            this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
            this.DANGKYTableAdapter.Fill(this.DS.DANGKY);


            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS.MONHOC);

            this.GIANGVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.GIANGVIENTableAdapter.Fill(this.DS.GIANGVIEN);

            macn = ((DataRowView)bds_LopTinChi[0])["MAKHOA"].ToString();
            cbxKhoa.DataSource = Program.bds_dspm;
            cbxKhoa.DisplayMember = "TENKHOA";
            cbxKhoa.ValueMember = "TENSERVER";
            cbxKhoa.SelectedIndex = Program.mKhoa;



            cbxMonHoc.DataSource = bds_MonHoc;
            cbxMonHoc.DisplayMember = "TENMH";
            cbxMonHoc.ValueMember = "MAMH";

            cbxGiangVien.DataSource = bds_GiangVien;
            cbxGiangVien.DisplayMember = "TEN";
            cbxGiangVien.ValueMember = "MAGV";

            if (Program.mGroup == "KHOA")
            {
                cbxKhoa.Enabled = false;
            }
            //txbMaMH.Text = cbxMonHoc.ValueMember.ToString();
            //txbMaGV.Text = cbxGiangVien.ValueMember.ToString();


            panelControl3.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void cbxKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxKhoa.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cbxKhoa.SelectedValue.ToString();
            if (cbxKhoa.SelectedIndex != Program.mKhoa)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {
                this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTINCHITableAdapter.Fill(this.DS.LOPTINCHI);
                this.GIANGVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.GIANGVIENTableAdapter.Fill(this.DS.GIANGVIEN);
                this.DANGKYTableAdapter.Connection.ConnectionString = Program.connstr;
                this.DANGKYTableAdapter.Fill(this.DS.DANGKY); 
                this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
                this.MONHOCTableAdapter.Fill(this.DS.MONHOC);
                if (bds_LopTinChi.Count > 0)
                {
                    macn = ((DataRowView)bds_LopTinChi[0])["MAKHOA"].ToString();
                  
                }
                
            }
        }

        private void txbMaMH_EditValueChanged(object sender, EventArgs e)
        {
            if (cbxMonHoc.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(txbMaMH.Text) && cbxMonHoc.Items.Contains(txbMaMH.Text))
                {
                    cbxMonHoc.SelectedValue = txbMaMH.Text;
                }
            }
        }

        private void txbMaGV_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbMaGV.Text) && cbxGiangVien.Items.Contains(txbMaGV.Text))
            {
                cbxGiangVien.SelectedValue = txbMaGV.Text;
            }
        }

        private void cbxMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMonHoc.SelectedValue != null)
            {
                txbMaMH.Text = cbxMonHoc.SelectedValue.ToString();
            }
        }

        private void cbxGiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxGiangVien.SelectedValue != null)
            {
                txbMaGV.Text = cbxGiangVien.SelectedValue.ToString();
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bds_LopTinChi.Position;
            panelControl3.Enabled = true;
            bds_LopTinChi.AddNew();
            txbMaKhoa.Text = macn;
            cbxGiangVien.SelectedIndex = 0;
            cbxMonHoc.SelectedIndex = 0;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            lOPTINCHIGridControl.Enabled = false;
            cbxKhoa.Enabled = false;
            txbMaKhoa.Enabled = false;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bds_LopTinChi.Position;
            panelControl3.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            lOPTINCHIGridControl.Enabled = false;
            cbxKhoa.Enabled = false;
            txbMaKhoa.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string maloptc = "";
            if (bds_DangKy.Count > 0)
            {
                MessageBox.Show("Không thể xóa lớp này vì đã có sinh viên", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa lớp này ?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    maloptc = ((DataRowView)bds_LopTinChi[bds_LopTinChi.Position])["MALTC"].ToString();
                    bds_LopTinChi.RemoveCurrent();
                    this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTINCHITableAdapter.Update(this.DS.LOPTINCHI);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp : " + ex.Message, "", MessageBoxButtons.OK);
                    this.LOPTINCHITableAdapter.Fill(this.DS.LOPTINCHI);
                    bds_LopTinChi.Position = bds_LopTinChi.Find("MALTC", maloptc);
                    return;
                }
            }
            if (bds_LopTinChi.Count == 0) btnXoa.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnPhucHoi.Enabled = true;

            if (numberHocKy.Value == 0)
            {
                MessageBox.Show("Học kì không được thiếu!", "", MessageBoxButtons.OK);
                numberHocKy.Focus();
                return;
            }
            if (numberSVTT.Value == 0)
            {
                MessageBox.Show("Số sinh viên tối thiểu không được thiếu!", "", MessageBoxButtons.OK);
                numberSVTT.Focus();
                return;
            }
            if (numberNhom.Value == 0)
            {
                MessageBox.Show("Nhóm không được thiếu!", "", MessageBoxButtons.OK);
                numberNhom.Focus();
                return;
            }
            if (txbMaKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Mã khoa không được thiếu!", "", MessageBoxButtons.OK);
                txbMaKhoa.Focus();
                return;
            }
            if (txbMaMH.Text.Trim() == "")
            {
                MessageBox.Show("Mã môn học không được thiếu!", "", MessageBoxButtons.OK);
                txbMaMH.Focus();
                return;
            }
            if (txbMaGV.Text.Trim() == "")
            {
                MessageBox.Show("Mã giảng viên không được thiếu!", "", MessageBoxButtons.OK);
                txbMaGV.Focus();
                return;
            }
            if (txbNienKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Niên khóa không được thiếu!", "", MessageBoxButtons.OK);
                txbNienKhoa.Focus();
                return;
            }
            try
            {
                bds_LopTinChi.EndEdit();
                bds_LopTinChi.ResetCurrentItem();
                this.LOPTINCHITableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTINCHITableAdapter.Update(this.DS.LOPTINCHI);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi lớp tín chỉ: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
            lOPTINCHIGridControl.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            panelControl3.Enabled = false;
            cbxKhoa.Enabled = true;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bds_LopTinChi.CancelEdit();
            if (btnThem.Enabled == false) bds_LopTinChi.Position = vitri;
            lOPTINCHIGridControl.Enabled = true;

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            cbxKhoa.Enabled = true;
            panelControl3.Enabled = false;
            //formLopTinChi_Load(sender, e);

            // load lại cả form...


            if (vitri > 0)
            {
                bds_LopTinChi.Position = vitri;
            }
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.LOPTINCHITableAdapter.Fill(this.DS.LOPTINCHI);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bds_LopTinChi.CancelEdit();
            lOPTINCHIGridControl.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLamMoi.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            panelControl3.Enabled = false;
            cbxKhoa.Enabled = true;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        
    }
}