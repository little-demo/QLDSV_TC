using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV_TC
{
    public partial class reportHPLopcs : DevExpress.XtraReports.UI.XtraReport
    {
        public reportHPLopcs()
        {
            InitializeComponent();
        }
        public reportHPLopcs(string malop, string nienkhoa, int hocky)
        {
            InitializeComponent();
            
            Console.WriteLine("Server: " + Program.connstr);
            if (this.sqlDataSource1 != null)
            {
                if (this.sqlDataSource1.Connection != null)
                {
                    this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
                }
                else
                {
                    // Xử lý trường hợp this.sqlDataSource1.Connection là null
                    MessageBox.Show("Connection của sqlDataSource1 chưa được khởi tạo.");
                }
            }

            this.sqlDataSource1.Queries[0].Parameters[0].Value = malop;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = nienkhoa;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = hocky;
            this.sqlDataSource1.Fill();

        }
    }
}
