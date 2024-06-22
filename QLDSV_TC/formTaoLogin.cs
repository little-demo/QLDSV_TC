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
    public partial class formTaoLogin : DevExpress.XtraEditors.XtraForm
    {
        public formTaoLogin()
        {
            InitializeComponent();
        }


        private void formTaoLogin_Load(object sender, EventArgs e)
        {
            loadGVcombobox();


            if (Program.mGroup.Equals("KHOA"))
            {
                rdKhoa.Checked = true;
                rdPGV.Enabled = rdPKT.Enabled = false;
            }
            if (Program.mGroup.Equals("PGV"))
            {
                rdPKT.Enabled = false;
            }
            if (Program.mGroup.Equals("PKT"))
            {
                rdPKT.Checked = true;
                rdKhoa.Enabled = rdPGV.Enabled = false;
            }
        }

        void loadGVcombobox()
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.SP_LayDSGV";
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsgv = new BindingSource();
            bdsgv.DataSource = dt;
            cbxGiangVien.DataSource = bdsgv;
            cbxGiangVien.DisplayMember = "MAGV";
            cbxGiangVien.ValueMember = "MAGV";
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            if (txbTenDN.Text.Trim() == "")
            {
                MessageBox.Show("Tên đăng nhập không được thiếu!", "", MessageBoxButtons.OK);
                txbTenDN.Focus();
                return;
            }
            if (txbMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Mật khẩu không được thiếu!", "", MessageBoxButtons.OK);
                txbMatKhau.Focus();
                return;
            }
            if (txbXacNhanMK.Text.Trim() == "")
            {
                MessageBox.Show("Xác nhận mật khẩu không được thiếu!", "", MessageBoxButtons.OK);
                txbXacNhanMK.Focus();
                return;
            }
            if (txbXacNhanMK.Text != txbMatKhau.Text)
            {
                MessageBox.Show("Xác nhận mật khẩu không trùng!", "", MessageBoxButtons.OK);
                txbXacNhanMK.Focus();
                return;
            }
            if (rdKhoa.Checked == false && rdPGV.Checked == false && rdPKT.Checked == false)
            {
                MessageBox.Show("Nhóm quyền không được thiếu!", "", MessageBoxButtons.OK);
                rdKhoa.Focus();
                return;
            }
            string login = txbTenDN.Text;
            string matkhau = txbMatKhau.Text;
            string user = cbxGiangVien.SelectedValue.ToString();
            string role = "";
            if (rdKhoa.Checked) role = "KHOA";
            if (rdPGV.Checked) role = "PGV";
            if (rdPKT.Checked) role = "PKT";

            String subLenh = " EXEC    @return_value = [dbo].[SP_TAOLOGIN] " +

                           " @LGNAME = N'" + login + "', " +
                           " @PASS = N'" + matkhau + "', " +
                           " @USERNAME = N'" + user + "', " +
                           " @ROLE = N'" + role + "' ";

            /* if (role == "PKT" && Program.severname == ((DataRowView)Program.bds_dspm[0])["TENSERVER"].ToString())
             {
                 // site 1 ---> sử dụng LINK2
                 subLenh = " EXEC    @return_value = LINK2.QLDSV.[dbo].[SP_TAOLOGIN] " +

                             " @LGNAME = N'" + login + "', " +
                             " @PASS = N'" + matkhau + "', " +
                             " @USERNAME = N'" + user + "', " +
                             " @ROLE = N'" + role + "' ";
             }
             else if (role == "PKT" && Program.severname == ((DataRowView)Program.bds_dspm[1])["TENSERVER"].ToString())

             {
                 subLenh = " EXEC    @return_value = LINK2.QLDSV.[dbo].[SP_TAOLOGIN] " +

                            " @LGNAME = N'" + login + "', " +
                            " @PASS = N'" + matkhau + "', " +
                            " @USERNAME = N'" + user + "', " +
                            " @ROLE = N'" + role + "' ";
             }*/
            string strLenh = " DECLARE @return_value int " + subLenh +
                    " SELECT  'Return Value' = @return_value ";

            int resultCheckLogin = Program.CheckDataHelper(strLenh);

            if (resultCheckLogin == -1)
            {
                XtraMessageBox.Show("Lỗi kết nối với database. Mời ban xem lại !", "", MessageBoxButtons.OK);
                this.Close();
            }
            if (resultCheckLogin == 1)
            {
                XtraMessageBox.Show("Login bị trùng . Mời bạn nhập login khác !", "", MessageBoxButtons.OK);

            }
            else if (resultCheckLogin == 2)
            {
                XtraMessageBox.Show("Giảng viên đã có tài khoản rồi !", "", MessageBoxButtons.OK);


            }
            else if (resultCheckLogin == 3)
            {
                XtraMessageBox.Show("Lỗi thực thi trong cơ sơ dữ liệu !", "", MessageBoxButtons.OK);
            }
            else if (resultCheckLogin == 0)
            {
                XtraMessageBox.Show("Tạo tài khoản thành công !", "", MessageBoxButtons.OK);

            }

            return;
        }
    }
}