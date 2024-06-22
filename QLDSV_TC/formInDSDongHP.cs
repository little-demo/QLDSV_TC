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
    public partial class formInDSDongHP : DevExpress.XtraEditors.XtraForm
    {
        private String tenkhoa = "";
        public formInDSDongHP()
        {
            InitializeComponent();
        }

        private void formInDSDongHP_Load(object sender, EventArgs e)
        {
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
                try
                {
                DataTable dt = new DataTable();
                string cmd = "exec SP3_GETKHOA_BYLOP '" + tbMaLop.Text.Trim() + "'";
                dt = Program.ExecSqlDataTable(cmd);

                Console.WriteLine("dữ liệu dt:" + dt.Rows[0][0].ToString());

                tenkhoa = dt.Rows[0][0].ToString();
                
                reportHPLopcs rpt = new reportHPLopcs(tbMaLop.Text, cbNIENKHOA.Text, int.Parse(cbHOCKY.Text));
                    rpt.lbKhoa.Text = tenkhoa.ToUpper();
                    rpt.lbMaLop.Text = tbMaLop.Text.ToUpper();
                Console.WriteLine(rpt.lbKhoa.Text, rpt.lbMaLop.Text);
                    ReportPrintTool print = new ReportPrintTool(rpt);
                Console.WriteLine("in thanh cong");
                    print.ShowPreviewDialog();

                }
                catch
                {
                    MessageBox.Show("Thông tin lớp học không hợp lệ.", "Thông Báo", MessageBoxButtons.OK);
                    tbMaLop.Focus();
                }

            }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}