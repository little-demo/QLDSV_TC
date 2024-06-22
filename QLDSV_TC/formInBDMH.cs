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
    public partial class formInBDMH : DevExpress.XtraEditors.XtraForm
    {
        public formInBDMH()
        {
            InitializeComponent();
        }

        private void formInBDMH_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.MONHOC' table. You can move, or remove it, as needed.
            this.MONHOCTableAdapter.Fill(this.DS.MONHOC);
            this.MONHOCTableAdapter.Connection.ConnectionString = Program.connstr;
            this.MONHOCTableAdapter.Fill(this.DS.MONHOC);
            cbxKhoa.DataSource = Program.bds_dspm;
            cbxKhoa.DisplayMember = "TENKHOA";
            cbxKhoa.ValueMember = "TENSERVER";
            cbxKhoa.SelectedIndex = Program.mKhoa;
            if (Program.mGroup == "KHOA")
            {
                cbxKhoa.Enabled = false;
            }
            cbxMonHoc.DataSource = bdsMonHoc;
            cbxMonHoc.DisplayMember = "TENMH";
            cbxMonHoc.ValueMember = "TENMH";
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
            cbxHocKy.DataSource = bdsHocKi;
            cbxHocKy.DisplayMember = "HOCKY";
            cbxHocKy.ValueMember = "HOCKY";
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
            if (cbxHocKy.Items.Count > 0)
            {
                cbxHocKy.SelectedIndex = 0;
            }
            
        }

        private void cbxHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadNhom(cbxNienKhoa.Text, cbxHocKy.Text);
            cbxNhom.SelectedIndex = 0;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string nienkhoa = cbxNienKhoa.Text;
            int hocky = Int32.Parse(cbxHocKy.Text);
            int nhom = Int32.Parse(cbxNhom.Text);
            string monhoc = cbxMonHoc.SelectedValue.ToString();
            string khoa = cbxKhoa.Text;
            reportBDMH rpt = new reportBDMH(nienkhoa, hocky, nhom, monhoc);
            rpt.monHoc.Text = monhoc;
            rpt.hocKy.Text = hocky.ToString();
            rpt.nhom.Text = nhom.ToString();
            rpt.nienKhoa.Text = nienkhoa;
            rpt.khoa.Text = khoa;
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}