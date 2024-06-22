using DevExpress.XtraWaitForm;
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
    public partial class frmDangNhap : Form
    {
        private SqlConnection conn_publisher = new SqlConnection();
        private bool isSinhVien = false;

        private void LayDSPM(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();
            Program.bds_dspm.DataSource = dt;
            cbxBoPhan.DataSource = Program.bds_dspm;
            cbxBoPhan.DisplayMember = "TENKHOA"; cbxBoPhan.ValueMember = "TENSERVER";
        }
        private int KetNoi_CSDLGOC()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = Program.connstr_publisher;
                conn_publisher.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối về cơ sở dữ liệu gốc.\nBạn xem lại Tên server của Publisher, và tên của CSDL trong chuỗi kết nối.\n");
                return 0;
            }
        }
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            if (KetNoi_CSDLGOC() == 0) return;
            LayDSPM("SELECT * FROM V_DS_PHANMANH");
            cbxBoPhan.SelectedIndex = 1; cbxBoPhan.SelectedIndex = 0;
        }


        private void cbxBoPhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Program.servername = cbxBoPhan.SelectedValue.ToString();
            }
            catch(Exception) { }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (isSinhVien == false)
            {
                if (txtTaiKhoan.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
                {
                    MessageBox.Show("Tài khoản và mật khẩu không được trống", "", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (txtTaiKhoan.Text.Trim() == "")
                {
                    MessageBox.Show("Tài khoản không được trống", "", MessageBoxButtons.OK);
                    return;
                }
            }

            if (isSinhVien == true)
            {
                Program.mlogin = "SVKN";
                Program.password = "123456";
                if (Program.KetNoi() == 0) return;
            }
            else
            {
                Program.mlogin = txtTaiKhoan.Text;
                Program.password = txtMatKhau.Text;
                if (Program.KetNoi() == 0) return;
            }
            

            Program.mKhoa = cbxBoPhan.SelectedIndex;
            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;
            String strLenh = "EXEC SP_Lay_Thong_Tin_GV_Tu_Login '" + Program.mlogin + "'";
            Program.myReader = Program.ExecSqlDataReader(strLenh);

            if (Program.myReader == null) return;
            Program.myReader.Read(); //chỉ đọc 1 dòng (nếu có nhiều thì dùng vòng lặp)

            Program.mGroup = Program.myReader.GetString(2);

            if (isSinhVien == false)
            {
                Program.mHoten = Program.myReader.GetString(1);
                Program.username = Program.myReader.GetString(0);
            }

            Program.myReader.Close();

            //Program.username = Program.myReader.GetString(0);
            

            //Program.mHoten = Program.myReader.GetString(1);
            //Program.mGroup = Program.myReader.GetString(2);
            

            //Nếu là sinh viên:
            if (isSinhVien == true)
            {
                string strlenh1 = "EXEC [dbo].[SP_LayThongTinSinhVien_DangNhap] '" + txtTaiKhoan.Text + "', '" + txtMatKhau.Text + "'";
                SqlDataReader reader = Program.ExecSqlDataReader(strlenh1);

                if (reader.HasRows == false && isSinhVien == true)
                {
                    MessageBox.Show("Đăng nhập thất bại! \nMã sinh viên không tồn tại");
                    return;
                }

                reader.Read();
                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu.\n Bạn xem lại username, password", "", MessageBoxButtons.OK);
                    return;
                }

                if (isSinhVien == true)
                {
                    try
                    {
                        Program.username = reader.GetString(0);
                        Program.mHoten = reader.GetString(1);
                    }
                    catch (Exception) { }
                }
                reader.Close();
            }
            

            
            Program.conn.Close();

            MessageBox.Show("Đăng nhập thành công !!!");
            Form f = new frmMain();
            f.ShowDialog();
            
            Program.frmChinh.HienThiMenu();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
            Program.frmChinh.Close();
        }

        private void checkBoxIsSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            isSinhVien = !isSinhVien;
        }

    }
}
