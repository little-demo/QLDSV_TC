using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class formInBDTK : DevExpress.XtraEditors.XtraForm
    {
        public formInBDTK()
        {
            InitializeComponent();
        }

        private void formInBDTK_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.LOP' table. You can move, or remove it, as needed.

            try
            {
                Console.WriteLine("Starting to fill LOP table.");
                this.lOPTableAdapter.Fill(this.DS.LOP);
                Console.WriteLine("Successfully filled LOP table.");
            }
            catch (System.Data.ConstraintException ex)
            {
                Console.WriteLine("Constraint Exception: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Exception: " + ex.Message);
            }

            cbxLop.DataSource = bdsLop;
            cbxLop.DisplayMember = "MALOP";
            cbxLop.ValueMember = "TENLOP";


            cbxKhoa.DataSource = Program.bds_dspm;
            cbxKhoa.DisplayMember = "TENKHOA";
            cbxKhoa.ValueMember = "TENSERVER";
            cbxKhoa.SelectedIndex = Program.mKhoa;
            if (Program.mGroup == "KHOA")
            {
                cbxKhoa.Enabled = false;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string malop = cbxLop.Text;
            string cmd = "SELECT TENKHOA,KHOAHOC FROM dbo.LOP,dbo.KHOA WHERE MALOP = '" + malop + "' AND KHOA.MAKHOA = LOP.MAKHOA";
            SqlDataReader reader = Program.ExecSqlDataReader(cmd);
            reader.Read();
            string tenkhoa = reader.GetString(0);
            string khoahoc = reader.GetString(1);
            reader.Close();
            reportBDTK rpt = new reportBDTK(malop);
            rpt.khoa.Text = tenkhoa;
            rpt.khoaHoc.Text = khoahoc;
            rpt.lop.Text = malop;

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
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
                this.lOPTableAdapter.Connection.ConnectionString = Program.connstr;
                this.lOPTableAdapter.Fill(this.DS.LOP);
            }
        }
    }
}