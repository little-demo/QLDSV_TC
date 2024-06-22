using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class formInHocPhiLop : DevExpress.XtraEditors.XtraForm
    {
        private String tenkhoa = "";
        public formInHocPhiLop()
        {
            InitializeComponent();
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
            cbxHocKy.DataSource = bdsHocKi;
            cbxHocKy.DisplayMember = "HOCKY";
            cbxHocKy.ValueMember = "HOCKY";
        }

        private void formInHocPhiLop_Load(object sender, EventArgs e)
        {
            loadcbNienkhoa();
            cbxNienKhoa.SelectedIndex = 0;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txbMaLop.Text.Length > 10)
            {
                MessageBox.Show("Mã lớp không hợp lệ.", "Thông Báo", MessageBoxButtons.OK);
                txbMaLop.Focus();
            }
            else
            {

                try
                {
                    string cmd = "exec SP3_GETKHOA_BYLOP '" + txbMaLop.Text.Trim() + "'";
                    DataTable dt = Program.ExecSqlDataTable(cmd);
                    tenkhoa = dt.Rows[0][0].ToString();
                    //Report_HocPhiTheoLop rpt = new Report_HocPhiTheoLop(txbMaLop.Text, cbxNienKhoa.Text, int.Parse(cbxHocKy.Text));
                    //rpt.lbKHOA.Text = "KHOA : " + tenkhoa.ToUpper();
                    //rpt.lbMALOP.Text = "Mã Lớp: " + tbMaLop.Text.ToUpper();
                    //ReportPrintTool print = new ReportPrintTool(rpt);
                    //print.ShowPreviewDialog();

                }
                catch
                {
                    MessageBox.Show("Thông tin lớp học không hợp lệ.", "Thông Báo", MessageBoxButtons.OK);
                    //tbMaLop.Focus();
                }
            }

        }
    }
}