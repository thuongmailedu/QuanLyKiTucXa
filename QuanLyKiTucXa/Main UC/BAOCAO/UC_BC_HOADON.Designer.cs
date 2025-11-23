namespace QuanLyKiTucXa.Main_UC.BAOCAO
{
    partial class UC_BC_HOADON
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
            this.dtpTHANG = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.comPHONG = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comNHA = new Guna.UI2.WinForms.Guna2ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label8 = new System.Windows.Forms.Label();
            this.comTRANGTHAI = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            this.btnXemBaoCao.Location = new System.Drawing.Point(1522, 131);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnXemBaoCao.Size = new System.Drawing.Size(31, 34);
            this.btnXemBaoCao.TabIndex = 64;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // dtpTHANG
            // 
            this.dtpTHANG.BackColor = System.Drawing.Color.White;
            this.dtpTHANG.BorderColor = System.Drawing.Color.Silver;
            this.dtpTHANG.BorderThickness = 1;
            this.dtpTHANG.Checked = true;
            this.dtpTHANG.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpTHANG.CustomFormat = "MM/yyyy";
            this.dtpTHANG.FillColor = System.Drawing.Color.White;
            this.dtpTHANG.FocusedColor = System.Drawing.Color.White;
            this.dtpTHANG.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.dtpTHANG.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTHANG.Location = new System.Drawing.Point(1271, 89);
            this.dtpTHANG.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHANG.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHANG.Name = "dtpTHANG";
            this.dtpTHANG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHANG.Size = new System.Drawing.Size(210, 51);
            this.dtpTHANG.TabIndex = 63;
            this.dtpTHANG.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(565, 106);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(42, 18);
            this.guna2HtmlLabel3.TabIndex = 59;
            this.guna2HtmlLabel3.Text = "Phòng";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(1185, 102);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(44, 22);
            this.guna2HtmlLabel2.TabIndex = 61;
            this.guna2HtmlLabel2.Text = "Tháng";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(238, 106);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(28, 18);
            this.guna2HtmlLabel1.TabIndex = 58;
            this.guna2HtmlLabel1.Text = "Nhà";
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
            this.comPHONG.Location = new System.Drawing.Point(637, 89);
            this.comPHONG.Name = "comPHONG";
            this.comPHONG.Size = new System.Drawing.Size(189, 51);
            this.comPHONG.TabIndex = 56;
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
            this.comNHA.Location = new System.Drawing.Point(295, 89);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(189, 51);
            this.comNHA.TabIndex = 57;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comNHA.SelectedIndexChanged += new System.EventHandler(this.comNHA_SelectedIndexChanged);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(39, 171);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1514, 634);
            this.reportViewer1.TabIndex = 55;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(471, 13);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(490, 41);
            this.label8.TabIndex = 54;
            this.label8.Text = "BÁO CÁO DANH SÁCH HÓA ĐƠN";
            // 
            // comTRANGTHAI
            // 
            this.comTRANGTHAI.BackColor = System.Drawing.Color.Transparent;
            this.comTRANGTHAI.BorderColor = System.Drawing.Color.Gray;
            this.comTRANGTHAI.BorderRadius = 12;
            this.comTRANGTHAI.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comTRANGTHAI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTRANGTHAI.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comTRANGTHAI.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comTRANGTHAI.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comTRANGTHAI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comTRANGTHAI.ItemHeight = 45;
            this.comTRANGTHAI.Items.AddRange(new object[] {
            "Đã thanh toán",
            "Chưa thanh toán"});
            this.comTRANGTHAI.Location = new System.Drawing.Point(987, 89);
            this.comTRANGTHAI.Name = "comTRANGTHAI";
            this.comTRANGTHAI.Size = new System.Drawing.Size(157, 51);
            this.comTRANGTHAI.TabIndex = 65;
            this.comTRANGTHAI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(891, 106);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(70, 22);
            this.guna2HtmlLabel4.TabIndex = 59;
            this.guna2HtmlLabel4.Text = "Trạng thái";
            // 
            // UC_BC_HOADON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comTRANGTHAI);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.dtpTHANG);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.comPHONG);
            this.Controls.Add(this.comNHA);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_BC_HOADON";
            this.Size = new System.Drawing.Size(1593, 867);
            this.Load += new System.EventHandler(this.UC_BC_HOADON_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton btnXemBaoCao;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHANG;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ComboBox comPHONG;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ComboBox comTRANGTHAI;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
    }
}
