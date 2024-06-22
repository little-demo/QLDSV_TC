namespace QLDSV_TC
{
    partial class formInDSDongHP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nIENKHOALabel = new System.Windows.Forms.Label();
            this.mALOPLabel = new System.Windows.Forms.Label();
            this.hOCKYLabel = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMaLop = new DevExpress.XtraEditors.TextEdit();
            this.cbNIENKHOA = new DevExpress.XtraEditors.TextEdit();
            this.cbHOCKY = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaLop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNIENKHOA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHOCKY.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // nIENKHOALabel
            // 
            this.nIENKHOALabel.AutoSize = true;
            this.nIENKHOALabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nIENKHOALabel.Location = new System.Drawing.Point(144, 155);
            this.nIENKHOALabel.Name = "nIENKHOALabel";
            this.nIENKHOALabel.Size = new System.Drawing.Size(75, 17);
            this.nIENKHOALabel.TabIndex = 39;
            this.nIENKHOALabel.Text = "Niên Khóa:";
            // 
            // mALOPLabel
            // 
            this.mALOPLabel.AutoSize = true;
            this.mALOPLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mALOPLabel.Location = new System.Drawing.Point(144, 110);
            this.mALOPLabel.Name = "mALOPLabel";
            this.mALOPLabel.Size = new System.Drawing.Size(59, 17);
            this.mALOPLabel.TabIndex = 40;
            this.mALOPLabel.Text = "Mã Lớp:";
            // 
            // hOCKYLabel
            // 
            this.hOCKYLabel.AutoSize = true;
            this.hOCKYLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hOCKYLabel.Location = new System.Drawing.Point(424, 155);
            this.hOCKYLabel.Name = "hOCKYLabel";
            this.hOCKYLabel.Size = new System.Drawing.Size(58, 17);
            this.hOCKYLabel.TabIndex = 41;
            this.hOCKYLabel.Text = "Học Kỳ:";
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(887, 54);
            this.panelControl1.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 25);
            this.label2.TabIndex = 37;
            this.label2.Text = "Nhập Thông Tin Lớp Học:";
            // 
            // tbMaLop
            // 
            this.tbMaLop.Location = new System.Drawing.Point(241, 107);
            this.tbMaLop.Name = "tbMaLop";
            this.tbMaLop.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMaLop.Properties.Appearance.Options.UseFont = true;
            this.tbMaLop.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbMaLop.Properties.Mask.EditMask = "[A-Z 0-9 -]{0,10}";
            this.tbMaLop.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.tbMaLop.Properties.Mask.ShowPlaceHolders = false;
            this.tbMaLop.Size = new System.Drawing.Size(400, 24);
            this.tbMaLop.TabIndex = 41;
            // 
            // cbNIENKHOA
            // 
            this.cbNIENKHOA.Location = new System.Drawing.Point(241, 153);
            this.cbNIENKHOA.Name = "cbNIENKHOA";
            this.cbNIENKHOA.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNIENKHOA.Properties.Appearance.Options.UseFont = true;
            this.cbNIENKHOA.Properties.Mask.EditMask = "[0-9 -]{9}";
            this.cbNIENKHOA.Properties.Mask.IgnoreMaskBlank = false;
            this.cbNIENKHOA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cbNIENKHOA.Properties.Mask.ShowPlaceHolders = false;
            this.cbNIENKHOA.Size = new System.Drawing.Size(175, 24);
            this.cbNIENKHOA.TabIndex = 43;
            // 
            // cbHOCKY
            // 
            this.cbHOCKY.Location = new System.Drawing.Point(487, 153);
            this.cbHOCKY.Name = "cbHOCKY";
            this.cbHOCKY.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHOCKY.Properties.Appearance.Options.UseFont = true;
            this.cbHOCKY.Properties.Mask.EditMask = "[1-4]";
            this.cbHOCKY.Properties.Mask.IgnoreMaskBlank = false;
            this.cbHOCKY.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cbHOCKY.Properties.Mask.ShowPlaceHolders = false;
            this.cbHOCKY.Size = new System.Drawing.Size(154, 24);
            this.cbHOCKY.TabIndex = 44;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.BackColor = System.Drawing.Color.Turquoise;
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.simpleButton4.Appearance.Options.UseBackColor = true;
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Appearance.Options.UseForeColor = true;
            this.simpleButton4.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.simpleButton4.AppearanceDisabled.Options.UseBackColor = true;
            this.simpleButton4.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.simpleButton4.AppearancePressed.Options.UseBackColor = true;
            this.simpleButton4.Location = new System.Drawing.Point(335, 218);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(81, 34);
            this.simpleButton4.TabIndex = 55;
            this.simpleButton4.Text = "In";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.BackColor = System.Drawing.Color.Turquoise;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseBackColor = true;
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.simpleButton2.AppearancePressed.Options.UseBackColor = true;
            this.simpleButton2.Location = new System.Drawing.Point(487, 218);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(81, 34);
            this.simpleButton2.TabIndex = 54;
            this.simpleButton2.Text = "Thoát";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // formInDSDongHP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 470);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.cbHOCKY);
            this.Controls.Add(this.cbNIENKHOA);
            this.Controls.Add(this.hOCKYLabel);
            this.Controls.Add(this.mALOPLabel);
            this.Controls.Add(this.tbMaLop);
            this.Controls.Add(this.nIENKHOALabel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.label2);
            this.Name = "formInDSDongHP";
            this.Text = "In Học Phí";
            this.Load += new System.EventHandler(this.formInDSDongHP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaLop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNIENKHOA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHOCKY.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label2;
        private QLDSV_TC.DS qLDSV_TCDataSet1;
        private DevExpress.XtraEditors.TextEdit tbMaLop;
        private DevExpress.XtraEditors.TextEdit cbNIENKHOA;
        private DevExpress.XtraEditors.TextEdit cbHOCKY;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Label nIENKHOALabel;
        private System.Windows.Forms.Label mALOPLabel;
        private System.Windows.Forms.Label hOCKYLabel;
    }
}