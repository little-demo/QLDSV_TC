﻿using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLDSV_TC
{
    public partial class reportDiemSV : DevExpress.XtraReports.UI.XtraReport
    {
        public reportDiemSV()
        {
            InitializeComponent();
        }
        public reportDiemSV(string msv, int type)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = msv;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = type;
        }
    }
}
