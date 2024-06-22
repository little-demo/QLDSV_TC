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
    public partial class formHocPhi : DevExpress.XtraEditors.XtraForm
    {
        BindingSource bdsHocPhi = new BindingSource();
        BindingSource bdsCTHP = new BindingSource();

        int vitri = 0;
        int vitri1 = 0;
        public formHocPhi()
        {
            InitializeComponent();
        }
        void loadHP()
        {
            string cmd1 = "EXEC [dbo].[SP_GetInfoSV_HP] '" + txbMaSV.Text + "'";
            SqlDataReader reader = Program.ExecSqlDataReader(cmd1);
            if (reader.HasRows == false)
            {
                MessageBox.Show("Mã sinh viên không tồn tại");
                reader.Close();
                return;
            }
            reader.Read();
            txbHoTen.Text = reader.GetString(0);
            txbMaLop.Text = reader.GetString(1);
            reader.Close();
            Program.conn.Close();


            string cmd2 = "EXEC dbo.SP_GetDSHP_SV '" + txbMaSV.Text + "'";
            DataTable tableHocPhi = Program.ExecSqlDataTable(cmd2);
            this.bdsHocPhi.DataSource = tableHocPhi;
            this.gridHocPhi.DataSource = this.bdsHocPhi;
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txbMaSV.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được bỏ trống");
                txbMaSV.Focus();
                return;
            }
            loadHP();
            btnThem1.Enabled = true;
        }

        private void btnThem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri1 = bdsHocPhi.Position;
            bdsHocPhi.AddNew();
            btnThem1.Enabled = btnLamMoi1.Enabled = false;
            btnGhi1.Enabled = btnPhucHoi1.Enabled = true;
        }

        private void btnGhi1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txbMaSV.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã sinh viên");
                    txbMaSV.Focus();
                    return;
                }
                if (float.Parse(((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCPHI"].ToString()) <= 0)
                {
                    MessageBox.Show("Số tiền không được nhỏ hơn 0đ");
                    return;
                }
                if (((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString() == "")
                {
                    MessageBox.Show("Niên khóa chưa nhập!");
                    return;
                }
                if (((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString() == "")
                {
                    MessageBox.Show("Học kỳ chưa nhập!");
                    return;
                }
                if (((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCPHI"].ToString() == "")
                {
                    MessageBox.Show("Học phí chưa nhập!");
                    return;
                }
                if (float.Parse(((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString()) <= 0)
                {
                    MessageBox.Show("Học kì không được nhỏ hơn 1");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string msv = txbMaSV.Text;
            string nienkhoa = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString();
            string hocki = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString();
            string hocphi = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCPHI"].ToString();
            bdsHocPhi.EndEdit();
            bdsHocPhi.ResetCurrentItem();
            SqlConnection conn = new SqlConnection(Program.connstr);
            // bắt đầu transaction
            SqlTransaction tran;

            conn.Open();
            tran = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("TAO_THONGTINHOCPHI", conn);
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MASV", msv));
                cmd.Parameters.Add(new SqlParameter("@NienKhoa", nienkhoa));
                cmd.Parameters.Add(new SqlParameter("@HocKy", hocki));
                cmd.Parameters.Add(new SqlParameter("@HocPhi", hocphi));
                cmd.ExecuteNonQuery();
                tran.Commit();
                MessageBox.Show("Thêm học phí thành công!");
                loadHP();


            }
            catch (SqlException sqlex)
            {
                try
                {

                    tran.Rollback();
                    XtraMessageBox.Show("Lỗi ghi học phí vào Database. Bạn hãy xem lại ! " + sqlex.Message, "", MessageBoxButtons.OK);

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
            btnThem1.Enabled = btnLamMoi1.Enabled = true;
            btnGhi1.Enabled = btnPhucHoi1.Enabled = false;
        }

        private void btnPhucHoi1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsHocPhi.CancelEdit();
            btnThem1.Enabled = btnLamMoi1.Enabled = true;
            btnGhi1.Enabled = btnPhucHoi1.Enabled = false;
            //formHocPhi_Load(sender, e);
            if (vitri > 0)
            {
                bdsCTHP.Position = vitri1;
            }
        }

        private void formHocPhi_Load(object sender, EventArgs e)
        {
            btnThem1.Enabled = btnGhi1.Enabled = btnPhucHoi1.Enabled = false;
        }

        private void btnLamMoi1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadHP();
        }

       
        private void gridHocPhi_MouseClick(object sender, MouseEventArgs e)
        {
            if (bdsHocPhi.Count > 0)
            {
                string nienkhoa = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString();
                string hocki = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString();
                string msv = txbMaSV.Text;

                string cmd = "EXEC dbo.SP_GetCTHP_SV '" + msv + "', '" + nienkhoa + "', '" + hocki + "'";
                DataTable tableCTHP = Program.ExecSqlDataTable(cmd);
                this.bdsCTHP.DataSource = tableCTHP;
                this.gridCTHP.DataSource = this.bdsCTHP;
            }
        }

        private void btnThemCTHP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsCTHP.Position;
            bdsCTHP.AddNew();

            btnThemCTHP.Enabled = false;
            btnGhiCTHP.Enabled = btnPhucHoiCTHP.Enabled = true;
        }

        private void btnGhiCTHP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString() == "")
                {
                    MessageBox.Show("Số tiền không được bỏ trống");
                    return;
                }
                if (float.Parse(((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString()) <= 0)
                {
                    MessageBox.Show("Số tiền không được nhỏ hơn 0đ");
                    return;
                }

                if (float.Parse(((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString()) > float.Parse(((DataRowView)bdsHocPhi[bdsHocPhi.Position])["SOTIENCANDONG"].ToString()))
                {
                    MessageBox.Show("Số tiền đóng không được lớn hơn số tiền cần đóng!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            string nienkhoa = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["NIENKHOA"].ToString();
            string hocki = ((DataRowView)bdsHocPhi[bdsHocPhi.Position])["HOCKY"].ToString();
            string msv = txbMaSV.Text;
            string sotiendong = "";
            if (bdsCTHP.Position >= 0 && bdsCTHP.Position < bdsCTHP.Count)
            {
                 sotiendong = ((DataRowView)bdsCTHP[bdsCTHP.Position])["SOTIENDONG"].ToString();
                // Tiếp tục xử lý với sotiendong
            }
            else
            {
                // Xử lý trường hợp không có hàng nào hoặc vị trí không hợp lệ
                Console.WriteLine("Vị trí hiện tại của bdsCTHP không hợp lệ hoặc không có hàng nào.");
            }

            bdsCTHP.EndEdit();
            bdsCTHP.ResetCurrentItem();
            SqlConnection conn = new SqlConnection(Program.connstr);
            // bắt đầu transaction
            SqlTransaction tran;

            conn.Open();
            tran = conn.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("SV_DONGTIEN", conn);
                cmd.Connection = conn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MASV", msv));
                cmd.Parameters.Add(new SqlParameter("@NienKhoa", nienkhoa));
                cmd.Parameters.Add(new SqlParameter("@HocKy", hocki));
                cmd.Parameters.Add(new SqlParameter("@SoTienDong", sotiendong));
                cmd.ExecuteNonQuery();





                tran.Commit();
                MessageBox.Show("Thêm chi tiết học phí thành công!");
                loadHP();


            }
            catch (SqlException sqlex)
            {
                try
                {

                    tran.Rollback();
                    XtraMessageBox.Show("Lỗi ghi chi tiết học phí vào Database. Bạn hãy xem lại ! " + sqlex.Message, "", MessageBoxButtons.OK);

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
            btnThemCTHP.Enabled = true;
            btnGhiCTHP.Enabled = btnPhucHoiCTHP.Enabled = false;
        }

        private void btnPhucHoiCTHP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsCTHP.CancelEdit();
            //formHocPhi_Load(sender, e);
            btnThemCTHP.Enabled = true;
            btnGhiCTHP.Enabled = btnPhucHoiCTHP.Enabled = false;
            if (vitri > 0)
            {
                bdsCTHP.Position = vitri;
            }
        }

        private void btnThoat2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnHuy1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bdsHocPhi.CancelEdit();
            bdsCTHP.CancelEdit();
            btnThem1.Enabled = btnLamMoi1.Enabled = true;
            btnGhi1.Enabled = btnPhucHoi1.Enabled = false;

            btnThemCTHP.Enabled = true;
            btnGhiCTHP.Enabled = btnPhucHoiCTHP.Enabled = false;
        }
    }
}