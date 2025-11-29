namespace QuanLyKiTucXa.Main_UC.BAOCAO
{
    partial class UC_BC_HOPDONG
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnXemBaoCao = new Guna.UI2.WinForms.Guna2CircleButton();
            this.dtpTU = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.comPHONG = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comNHA = new Guna.UI2.WinForms.Guna2ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDEN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnXemBaoCao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnXemBaoCao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemBaoCao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemBaoCao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemBaoCao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemBaoCao.FillColor = System.Drawing.Color.Transparent;
            this.btnXemBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXemBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao.Location = new System.Drawing.Point(1573, 101);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnXemBaoCao.Size = new System.Drawing.Size(31, 34);
            this.btnXemBaoCao.TabIndex = 53;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // dtpTU
            // 
            this.dtpTU.BackColor = System.Drawing.Color.White;
            this.dtpTU.BorderColor = System.Drawing.Color.Silver;
            this.dtpTU.BorderRadius = 5;
            this.dtpTU.BorderThickness = 1;
            this.dtpTU.Checked = true;
            this.dtpTU.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpTU.CustomFormat = "dd/MM/yyyy";
            this.dtpTU.FillColor = System.Drawing.Color.White;
            this.dtpTU.FocusedColor = System.Drawing.Color.White;
            this.dtpTU.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.dtpTU.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTU.Location = new System.Drawing.Point(944, 84);
            this.dtpTU.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTU.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTU.Name = "dtpTU";
            this.dtpTU.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTU.Size = new System.Drawing.Size(210, 51);
            this.dtpTU.TabIndex = 52;
            this.dtpTU.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // comPHONG
            // 
            this.comPHONG.BackColor = System.Drawing.Color.Transparent;
            this.comPHONG.BorderColor = System.Drawing.Color.Gray;
            this.comPHONG.BorderRadius = 12;
            this.comPHONG.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comPHONG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPHONG.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comPHONG.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comPHONG.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comPHONG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comPHONG.ItemHeight = 45;
            this.comPHONG.Location = new System.Drawing.Point(589, 84);
            this.comPHONG.Name = "comPHONG";
            this.comPHONG.Size = new System.Drawing.Size(189, 51);
            this.comPHONG.TabIndex = 47;
            this.comPHONG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comNHA
            // 
            this.comNHA.BackColor = System.Drawing.Color.Transparent;
            this.comNHA.BorderColor = System.Drawing.Color.Gray;
            this.comNHA.BorderRadius = 12;
            this.comNHA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comNHA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comNHA.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comNHA.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comNHA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comNHA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comNHA.ItemHeight = 45;
            this.comNHA.Location = new System.Drawing.Point(254, 84);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(189, 51);
            this.comNHA.TabIndex = 48;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comNHA.SelectedIndexChanged += new System.EventHandler(this.comNHA_SelectedIndexChanged);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(44, 150);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1560, 597);
            this.reportViewer1.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(478, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(676, 41);
            this.label8.TabIndex = 45;
            this.label8.Text = "BÁO CÁO DANH SÁCH HỢP ĐỒNG PHÁT SINH";
            // 
            // dtpDEN
            // 
            this.dtpDEN.BackColor = System.Drawing.Color.White;
            this.dtpDEN.BorderColor = System.Drawing.Color.Silver;
            this.dtpDEN.BorderRadius = 5;
            this.dtpDEN.BorderThickness = 1;
            this.dtpDEN.Checked = true;
            this.dtpDEN.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpDEN.CustomFormat = "dd/MM/yyyy";
            this.dtpDEN.FillColor = System.Drawing.Color.White;
            this.dtpDEN.FocusedColor = System.Drawing.Color.White;
            this.dtpDEN.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.dtpDEN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDEN.Location = new System.Drawing.Point(1324, 84);
            this.dtpDEN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDEN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDEN.Name = "dtpDEN";
            this.dtpDEN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpDEN.Size = new System.Drawing.Size(210, 51);
            this.dtpDEN.TabIndex = 52;
            this.dtpDEN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(178, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 28);
            this.label2.TabIndex = 57;
            this.label2.Text = "Nhà";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(492, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 28);
            this.label1.TabIndex = 57;
            this.label1.Text = "Phòng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(835, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 28);
            this.label3.TabIndex = 57;
            this.label3.Text = "Từ ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(1215, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 28);
            this.label4.TabIndex = 57;
            this.label4.Text = "Đến ngày";
            // 
            // UC_BC_HOPDONG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.dtpDEN);
            this.Controls.Add(this.dtpTU);
            this.Controls.Add(this.comPHONG);
            this.Controls.Add(this.comNHA);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_BC_HOPDONG";
            this.Size = new System.Drawing.Size(1651, 824);
            this.Load += new System.EventHandler(this.UC_BC_HOPDONG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton btnXemBaoCao;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTU;
        private Guna.UI2.WinForms.Guna2ComboBox comPHONG;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDEN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
