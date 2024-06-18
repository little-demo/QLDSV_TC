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
    public partial class formInDSLTC : DevExpress.XtraEditors.XtraForm
    {
        public formInDSLTC()
        {
            InitializeComponent();
        }

        private void formInDSLTC_Load(object sender, EventArgs e)
        {
            cbxKhoa.DataSource = Program.bds_dspm;
            cbxKhoa.DisplayMember = "TENKHOA";
            cbxKhoa.ValueMember = "TENSERVER";
            cbxKhoa.SelectedIndex = Program.mKhoa;
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

        private void cbxNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcbHocKi(cbxNienKhoa.Text);
            cbxHocKi.SelectedIndex = 0;
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

        private void btnIn_Click(object sender, EventArgs e)
        {
            string nienkhoa = cbxNienKhoa.Text;
            int hocky = Int32.Parse(cbxHocKi.Text);
            string khoa = cbxKhoa.Text;
            reportDSLTC rpt = new reportDSLTC(nienkhoa, hocky);
            
            rpt.hocKy.Text = hocky.ToString();
            rpt.nienKhoa.Text = nienkhoa;
            rpt.khoa.Text = khoa;
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }
    }
}