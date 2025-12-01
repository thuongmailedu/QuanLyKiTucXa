namespace QuanLyKiTucXa.Main_UC.BAOCAO
{
    partial class UC_BC_PHONG
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
            this.comNHA = new Guna.UI2.WinForms.Guna2ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label8 = new System.Windows.Forms.Label();
            this.comTINHTRANG = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.btnXemBaoCao.Location = new System.Drawing.Point(1465, 92);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnXemBaoCao.Size = new System.Drawing.Size(31, 34);
            this.btnXemBaoCao.TabIndex = 53;
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
            this.dtpTHANG.Location = new System.Drawing.Point(914, 75);
            this.dtpTHANG.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHANG.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHANG.Name = "dtpTHANG";
            this.dtpTHANG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHANG.Size = new System.Drawing.Size(186, 51);
            this.dtpTHANG.TabIndex = 52;
            this.dtpTHANG.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
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
            this.comNHA.Location = new System.Drawing.Point(567, 75);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(188, 51);
            this.comNHA.TabIndex = 48;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(72, 149);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1424, 619);
            this.reportViewer1.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(554, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(464, 41);
            this.label8.TabIndex = 45;
            this.label8.Text = "BÁO CÁO TÌNH TRẠNG PHÒNG";
            // 
            // comTINHTRANG
            // 
            this.comTINHTRANG.BackColor = System.Drawing.Color.Transparent;
            this.comTINHTRANG.BorderColor = System.Drawing.Color.Gray;
            this.comTINHTRANG.BorderRadius = 12;
            this.comTINHTRANG.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comTINHTRANG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTINHTRANG.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comTINHTRANG.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comTINHTRANG.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comTINHTRANG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comTINHTRANG.ItemHeight = 45;
            this.comTINHTRANG.Location = new System.Drawing.Point(1262, 75);
            this.comTINHTRANG.Name = "comTINHTRANG";
            this.comTINHTRANG.Size = new System.Drawing.Size(175, 51);
            this.comTINHTRANG.TabIndex = 54;
            this.comTINHTRANG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(500, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 28);
            this.label2.TabIndex = 56;
            this.label2.Text = "Nhà";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(805, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 28);
            this.label1.TabIndex = 56;
            this.label1.Text = "Thời gian";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(1131, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 28);
            this.label3.TabIndex = 56;
            this.label3.Text = "Tình trạng";
            // 
            // UC_BC_PHONG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comTINHTRANG);
            this.Controls.Add(this.btnXemBaoCao);
            this.Controls.Add(this.dtpTHANG);
            this.Controls.Add(this.comNHA);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_BC_PHONG";
            this.Size = new System.Drawing.Size(1601, 853);
            this.Load += new System.EventHandler(this.UC_BC_PHONG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton btnXemBaoCao;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHANG;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ComboBox comTINHTRANG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
