using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC
{
    public partial class reportBDTK : DevExpress.XtraReports.UI.XtraReport
    {
        public reportBDTK()
        {
            InitializeComponent();
        }
        public reportBDTK(string malop)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = malop;
        }
    }
}
