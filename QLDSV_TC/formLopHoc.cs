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
    public partial class formLopHoc : DevExpress.XtraEditors.XtraForm
    {
        int vitri = 0;
        string macn = "";
        private string _flagOptionLop;
        private string _oldMaLop = "";
        private string _oldTenLop = "";

        public formLopHoc()
        {
            InitializeComponent();
        }
        
        private void lOPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bds_LopHoc.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void formLopHoc_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;

            this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
            this.LOPTableAdapter.Fill(this.DS.LOP);

            this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.SINHVIENTableAdapter.Fill(this.DS.SINHVIEN);

            macn = ((DataRowView)bds_LopHoc[0])["MAKHOA"].ToString();

            // Lấy hai dòng đầu tiên từ bds_dspm
            var firstTwoRows = Program.bds_dspm.Cast<DataRowView>().Take(2).ToList();

            //cbxKhoa.DataSource = Program.bds_dspm;
            cbxKhoa.DataSource = firstTwoRows;
            cbxKhoa.DisplayMember = "TENKHOA";
            cbxKhoa.ValueMember = "TENSERVER";
            cbxKhoa.SelectedIndex = Program.mKhoa;
            if (Program.mGroup == "KHOA")
            {
                cbxKhoa.Enabled = false;
            }
        }

        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
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
                this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.LOPTableAdapter.Fill(this.DS.LOP);
                this.SINHVIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.SINHVIENTableAdapter.Fill(this.DS.SINHVIEN);
                macn = ((DataRowView)bds_LopHoc[0])["MAKHOA"].ToString();
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bds_LopHoc.Position;
            _flagOptionLop = "ADD";

            panelControl2.Enabled = false;
            bds_LopHoc.AddNew();
            txbMaKhoa.Text = macn;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            lOPGridControl.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (validatorLopHoc() == true)
            {
                try
                {
                    bds_LopHoc.EndEdit();
                    bds_LopHoc.ResetCurrentItem();
                    this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTableAdapter.Update(this.DS.LOP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi ghi lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                    return;
                }
                lOPGridControl.Enabled = true;
                btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
                btnGhi.Enabled = btnPhucHoi.Enabled = false;
                panelControl2.Enabled = true;
            }
            else
            {
                return;
            }

        }

        private bool validatorLopHoc()
        {
            if (txbMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được thiếu!", "", MessageBoxButtons.OK);
                txbMaLop.Focus();
                return false;
            }
            if (txbTenLop.Text.Trim() == "")
            {
                MessageBox.Show("Tên lớp không được thiếu!", "", MessageBoxButtons.OK);
                txbTenLop.Focus();
                return false;
            }
            if (txbKhoaHoc.Text.Trim() == "")
            {
                MessageBox.Show("Khóa học không được thiếu!", "", MessageBoxButtons.OK);
                txbKhoaHoc.Focus();
                return false;
            }
            if (txbMaKhoa.Text.Trim() == "")
            {
                MessageBox.Show("Mã khoa không được thiếu!", "", MessageBoxButtons.OK);
                txbMaKhoa.Focus();
                return false;
            }
            if (_flagOptionLop == "ADD")
            {
                //TODO: Check mã lớp có tồn tại chưa
                string query1 = "DECLARE  @return_value int \n"
                            + "EXEC  @return_value = SP_CHECKID \n"
                            + "@Code = N'" + txbMaLop.Text + "',@Type = N'MALOP' \n"
                            + "SELECT  'Return Value' = @return_value ";

                int resultMa = Program.CheckDataHelper(query1);
                if (resultMa == -1)
                {
                    XtraMessageBox.Show("Lỗi kết nối với database. Mời ban xem lại !", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultMa == 1)
                {
                    XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa hiên tại !", "", MessageBoxButtons.OK);

                    return false;
                }
                if (resultMa == 2)
                {
                    XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);

                    return false;
                }

                // TODO : Check tên lớp có tồn tại chưa
                string query2 = "DECLARE @return_value int \n"
                               + "EXEC @return_value = SP_CHECKNAME \n"
                               + "@Name = N'" + txbTenLop.Text + "', @Type = N'TENLOP' \n"
                               + "SELECT 'Return Value' = @return_value ";
                int resultTen = Program.CheckDataHelper(query2);
                if (resultTen == -1)
                {
                    XtraMessageBox.Show("Lỗi kết nối với Database. Mời bạn xem lại !", "", MessageBoxButtons.OK);
                    this.Close();
                }
                if (resultTen == 1)
                {
                    XtraMessageBox.Show("Tên lớp đã có rồi !", "", MessageBoxButtons.OK);

                    return false;
                }
                if (resultTen == 2)
                {
                    XtraMessageBox.Show("Tên lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (_flagOptionLop == "UPDATE")
            {
                if (!this.txbMaLop.Text.Trim().ToString().Equals(_oldMaLop))// Nếu mã lớp thay đổi so với ban đầu
                {
                    //TODO: Check mã lớp có tồn tại chưa
                    string query1 = "DECLARE  @return_value int \n"
                                + "EXEC  @return_value = SP_CHECKID \n"
                                + "@Code = N'" + txbMaLop.Text + "',@Type = N'MALOP' \n"
                                + "SELECT  'Return Value' = @return_value ";

                    int resultMa = Program.CheckDataHelper(query1);
                    if (resultMa == -1)
                    {
                        XtraMessageBox.Show("Lỗi kết nối với database. Mời ban xem lại !", "", MessageBoxButtons.OK);
                        this.Close();
                    }
                    if (resultMa == 1)
                    {
                        XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa hiên tại !", "", MessageBoxButtons.OK);
                        return false;
                    }
                    if (resultMa == 2)
                    {
                        XtraMessageBox.Show("Mã lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);
                        return false;
                    }
                }
                if (!this.txbTenLop.Text.Trim().ToString().Equals(_oldTenLop))
                {
                    // TODO : Check tên lớp có tồn tại chưa
                    string query2 = "DECLARE @return_value int \n"
                                   + "EXEC @return_value = SP_CHECKNAME \n"
                                   + "@Name = N'" + txbTenLop.Text + "', @Type = N'TENLOP' \n"
                                   + "SELECT 'Return Value' = @return_value ";
                    int resultTen = Program.CheckDataHelper(query2);
                    if (resultTen == -1)
                    {
                        XtraMessageBox.Show("Lỗi kết nối với Database. Mời bạn xem lại !", "", MessageBoxButtons.OK);
                        this.Close();
                    }
                    if (resultTen == 1)
                    {
                        XtraMessageBox.Show("Tên lớp đã có rồi !", "", MessageBoxButtons.OK);
                        return false;
                    }
                    if (resultTen == 2)
                    {
                        XtraMessageBox.Show("Tên lớp đã tồn tại ở Khoa khác !", "", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bds_LopHoc.Position;
            _flagOptionLop = "UPDATE";
            _oldMaLop = this.txbMaLop.Text.Trim();
            _oldTenLop = this.txbTenLop.Text.Trim();
            txbMaLop.Enabled = false;
            panelControl2.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnPhucHoi.Enabled = false;
            btnGhi.Enabled = true;
            lOPGridControl.Enabled = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string malop = "";
            if (bds_SinhVien.Count > 0)
            {
                MessageBox.Show("Không thể xóa lớp học này vì đã có sinh viên", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xóa lớp học này ?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    malop = ((DataRowView)bds_LopHoc[bds_LopHoc.Position])["MALOP"].ToString();
                    bds_LopHoc.RemoveCurrent();
                    this.LOPTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.LOPTableAdapter.Update(this.DS.LOP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp học: " + ex.Message, "", MessageBoxButtons.OK);
                    this.LOPTableAdapter.Fill(this.DS.LOP);
                    bds_LopHoc.Position = bds_LopHoc.Find("MALOP", malop);
                    return;
                }
            }
            if (bds_LopHoc.Count == 0) btnXoa.Enabled = false; 
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bds_LopHoc.CancelEdit();
            if (btnThem.Enabled == false) bds_LopHoc.Position = vitri;
            lOPGridControl.Enabled = true;
            panelControl2.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
            formLopHoc_Load(sender, e);

            if (vitri > 0)
            {
                bds_LopHoc.Position = vitri;
            }
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.LOPTableAdapter.Fill(this.DS.LOP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panelControl2.Enabled = true;
            lOPGridControl.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnGhi.Enabled = true;
        }
    }
}