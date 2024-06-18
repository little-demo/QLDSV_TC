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
    public partial class formNhapDiemSV : DevExpress.XtraEditors.XtraForm
    {
        private BindingSource bds_Diem = new BindingSource();

        public formNhapDiemSV()
        {
            InitializeComponent();
        }


        private void formNhapDiemSV_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            this.mONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.mONHOCTableAdapter.Fill(this.DS.MONHOC);

            // Lấy hai dòng đầu tiên từ bds_dspm
            var firstTwoRows = Program.bds_dspm.Cast<DataRowView>().Take(2).ToList();

            cbxKhoa.DataSource = firstTwoRows;
            //cbxKhoa.DataSource = Program.bds_dspm;
            cbxKhoa.DisplayMember = "TENKHOA";
            cbxKhoa.ValueMember = "TENSERVER";
            cbxKhoa.SelectedIndex = Program.mKhoa;

            cbxMonHoc.DataSource = bds_MonHoc;
            cbxMonHoc.DisplayMember = "TENMH";
            cbxMonHoc.ValueMember = "TENMH";

            if (Program.mGroup == "KHOA")
            {
                cbxKhoa.Enabled = false;
            }
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
        void loadNhom(string nienkhoa, string hocki)
        {
            DataTable dt = new DataTable();
            string cmd = "EXEC dbo.GetAllNhom '" + nienkhoa + "', " + hocki;
            dt = Program.ExecSqlDataTable(cmd);

            BindingSource bdsNhom = new BindingSource();
            bdsNhom.DataSource = dt;
            cbxNhom.DataSource = bdsNhom;
            cbxNhom.DisplayMember = "NHOM";
            cbxNhom.ValueMember = "NHOM";
        }
        void loadBDMH()
        {
            string cmd = "EXEC [dbo].[SP_BDMH] '" + cbxNienKhoa.Text + "', " + cbxHocKi.Text + ", " + cbxNhom.Text + ", N'" + cbxMonHoc.SelectedValue.ToString() + "'";
            DataTable diemTable = Program.ExecSqlDataTable(cmd);
            this.bds_Diem.DataSource = diemTable;
            this.gridDiem.DataSource = this.bds_Diem;
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
                loadcbNienkhoa();
                cbxNienKhoa.SelectedIndex = 0;
            }
        }

        private void cbxNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcbHocKi(cbxNienKhoa.Text);
            cbxHocKi.SelectedIndex = 0;
        }

        private void cbxHocKi_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadNhom(cbxNienKhoa.Text, cbxHocKi.Text);
            cbxNhom.SelectedIndex = 0;
        }

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            loadBDMH();
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            BindingSource bdsTemp = (BindingSource)this.gridDiem.DataSource;
            if (bdsTemp == null)
            {
                MessageBox.Show("Chưa có thông tin để ghi điểm!", "", MessageBoxButtons.OK);
                return;
            }

            bdsTemp.EndEdit();
            SqlConnection conn = new SqlConnection(Program.connstr);
            // bắt đầu transaction
            SqlTransaction tran;

            conn.Open();
            tran = conn.BeginTransaction();
            try
            {


                for (int i = 0; i < bdsTemp.Count; i++)
                {

                    SqlCommand cmd = new SqlCommand("SP_Nhap_Diem", conn);
                    cmd.Connection = conn;
                    cmd.Transaction = tran;



                    cmd.CommandType = CommandType.StoredProcedure;
                    string masv = ((DataRowView)bdsTemp[i])["MASV"].ToString();
                    cmd.Parameters.Add(new SqlParameter("@MSSV", masv));
                    cmd.Parameters.Add(new SqlParameter("@MALTC", ((DataRowView)bdsTemp[i])["MALC"].ToString()));
                    float diemcc = 0;
                    float diemgk = 0;
                    float diemck = 0;
                    if (((DataRowView)bdsTemp[i])["DIEM_CC"].ToString() != "")
                    {
                        diemcc = float.Parse(((DataRowView)bdsTemp[i])["DIEM_CC"].ToString());
                    }
                    if (((DataRowView)bdsTemp[i])["DIEM_GK"].ToString() != "")
                    {
                        diemgk = float.Parse(((DataRowView)bdsTemp[i])["DIEM_GK"].ToString());
                    }
                    if (((DataRowView)bdsTemp[i])["DIEM_CK"].ToString() != "")
                    {
                        diemck = float.Parse(((DataRowView)bdsTemp[i])["DIEM_CK"].ToString());
                    }
                    if (diemcc < 0 || diemcc > 10 || diemck < 0 || diemck > 10 || diemgk < 0 || diemgk > 10)
                    {
                        tran.Rollback();
                        XtraMessageBox.Show("Điểm số chỉ được nhập từ 0 đến 10! Xin vui lòng nhập lại");
                        conn.Close();
                        loadBDMH();
                        return;
                    }
                    cmd.Parameters.Add(new SqlParameter("@DIEMCC", diemcc));
                    cmd.Parameters.Add(new SqlParameter("@DIEMGK", diemgk));
                    cmd.Parameters.Add(new SqlParameter("@DIEMCK", diemck));
                    cmd.ExecuteNonQuery();


                }


                tran.Commit();
            }
            catch (SqlException sqlex)
            {
                try
                {

                    tran.Rollback();
                    XtraMessageBox.Show("Lỗi ghi toàn bộ điểm vào Database. Bạn hãy xem lại ! " + sqlex.Message, "", MessageBoxButtons.OK);
                    loadBDMH();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
                conn.Close();
                return;
            }
            finally
            {
                conn.Close();
            }
            XtraMessageBox.Show("Thao tác thành công!", "", MessageBoxButtons.OK);
            string cmd1 = "EXEC [dbo].[SP_BDMH] '" + cbxNienKhoa.Text + "', " + cbxHocKi.Text + ", " + cbxNhom.Text + ", N'" + cbxMonHoc.SelectedValue.ToString() + "'";
            DataTable diemTable = Program.ExecSqlDataTable(cmd1);
            this.bds_Diem.DataSource = diemTable;
            this.gridDiem.DataSource = this.bds_Diem;
            return;
        }
    }
}