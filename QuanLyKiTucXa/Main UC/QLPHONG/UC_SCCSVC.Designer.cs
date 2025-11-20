namespace QuanLyKiTucXa.Main_UC.QLPHONG
{
    partial class UC_SCCSVC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblSinhVien = new System.Windows.Forms.Label();
            this.dgvSCCSVC = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnfillter_SCCSVC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btndelete_SCCSVC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit_SCCSVC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnadd_SCCSVC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.txtSearch_SCCSVC = new Guna.UI2.WinForms.Guna2TextBox();
            this.comfillter_SCCSVC = new Guna.UI2.WinForms.Guna2ComboBox();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_SCCSVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_PHONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_CSVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_CSVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_NHACC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAY_YEUCAU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAY_HOANTHANH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANGTHAI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHITIET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSCCSVC)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSinhVien
            // 
            this.lblSinhVien.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSinhVien.AutoSize = true;
            this.lblSinhVien.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSinhVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            this.lblSinhVien.Location = new System.Drawing.Point(643, 12);
            this.lblSinhVien.Name = "lblSinhVien";
            this.lblSinhVien.Size = new System.Drawing.Size(483, 46);
            this.lblSinhVien.TabIndex = 16;
            this.lblSinhVien.Text = "SỬA CHỮA CƠ SỞ VẬT CHẤT";
            // 
            // dgvSCCSVC
            // 
            this.dgvSCCSVC.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvSCCSVC.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSCCSVC.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvSCCSVC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSCCSVC.BackgroundColor = System.Drawing.Color.White;
            this.dgvSCCSVC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSCCSVC.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSCCSVC.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSCCSVC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSCCSVC.ColumnHeadersHeight = 45;
            this.dgvSCCSVC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MA_SCCSVC,
            this.MANHA,
            this.MA_PHONG,
            this.MA_CSVC,
            this.TEN_CSVC,
            this.TEN_NHACC,
            this.NGAY_YEUCAU,
            this.NGAY_HOANTHANH,
            this.TRANGTHAI,
            this.CHITIET});
            this.dgvSCCSVC.EnableHeadersVisualStyles = false;
            this.dgvSCCSVC.Location = new System.Drawing.Point(82, 138);
            this.dgvSCCSVC.Name = "dgvSCCSVC";
            this.dgvSCCSVC.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSCCSVC.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSCCSVC.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSCCSVC.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSCCSVC.RowTemplate.Height = 45;
            this.dgvSCCSVC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSCCSVC.Size = new System.Drawing.Size(1482, 646);
            this.dgvSCCSVC.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Controls.Add(this.btnfillter_SCCSVC);
            this.panel2.Controls.Add(this.btndelete_SCCSVC);
            this.panel2.Controls.Add(this.btnedit_SCCSVC);
            this.panel2.Controls.Add(this.btnadd_SCCSVC);
            this.panel2.Controls.Add(this.txtSearch_SCCSVC);
            this.panel2.Controls.Add(this.comfillter_SCCSVC);
            this.panel2.Location = new System.Drawing.Point(82, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1482, 71);
            this.panel2.TabIndex = 18;
            // 
            // btnfillter_SCCSVC
            // 
            this.btnfillter_SCCSVC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.filter;
            this.btnfillter_SCCSVC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnfillter_SCCSVC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnfillter_SCCSVC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnfillter_SCCSVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnfillter_SCCSVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnfillter_SCCSVC.FillColor = System.Drawing.Color.Transparent;
            this.btnfillter_SCCSVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnfillter_SCCSVC.ForeColor = System.Drawing.Color.White;
            this.btnfillter_SCCSVC.Location = new System.Drawing.Point(1288, 15);
            this.btnfillter_SCCSVC.Name = "btnfillter_SCCSVC";
            this.btnfillter_SCCSVC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnfillter_SCCSVC.Size = new System.Drawing.Size(35, 40);
            this.btnfillter_SCCSVC.TabIndex = 14;
            // 
            // btndelete_SCCSVC
            // 
            this.btndelete_SCCSVC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.del;
            this.btndelete_SCCSVC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndelete_SCCSVC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_SCCSVC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_SCCSVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btndelete_SCCSVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btndelete_SCCSVC.FillColor = System.Drawing.Color.Transparent;
            this.btndelete_SCCSVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btndelete_SCCSVC.ForeColor = System.Drawing.Color.White;
            this.btndelete_SCCSVC.Location = new System.Drawing.Point(1421, 19);
            this.btndelete_SCCSVC.Name = "btndelete_SCCSVC";
            this.btndelete_SCCSVC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btndelete_SCCSVC.Size = new System.Drawing.Size(35, 32);
            this.btndelete_SCCSVC.TabIndex = 13;
            // 
            // btnedit_SCCSVC
            // 
            this.btnedit_SCCSVC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.edit;
            this.btnedit_SCCSVC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnedit_SCCSVC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_SCCSVC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_SCCSVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit_SCCSVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit_SCCSVC.FillColor = System.Drawing.Color.Transparent;
            this.btnedit_SCCSVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit_SCCSVC.ForeColor = System.Drawing.Color.White;
            this.btnedit_SCCSVC.Location = new System.Drawing.Point(1384, 19);
            this.btnedit_SCCSVC.Name = "btnedit_SCCSVC";
            this.btnedit_SCCSVC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit_SCCSVC.Size = new System.Drawing.Size(31, 32);
            this.btnedit_SCCSVC.TabIndex = 12;
            // 
            // btnadd_SCCSVC
            // 
            this.btnadd_SCCSVC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnadd_SCCSVC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadd_SCCSVC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_SCCSVC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_SCCSVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnadd_SCCSVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnadd_SCCSVC.FillColor = System.Drawing.Color.Transparent;
            this.btnadd_SCCSVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnadd_SCCSVC.ForeColor = System.Drawing.Color.White;
            this.btnadd_SCCSVC.Location = new System.Drawing.Point(1339, 19);
            this.btnadd_SCCSVC.Name = "btnadd_SCCSVC";
            this.btnadd_SCCSVC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd_SCCSVC.Size = new System.Drawing.Size(30, 32);
            this.btnadd_SCCSVC.TabIndex = 11;
            this.btnadd_SCCSVC.Click += new System.EventHandler(this.btn_SCCSVC_Click);
            // 
            // txtSearch_SCCSVC
            // 
            this.txtSearch_SCCSVC.BorderColor = System.Drawing.Color.Gray;
            this.txtSearch_SCCSVC.BorderRadius = 12;
            this.txtSearch_SCCSVC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch_SCCSVC.DefaultText = "";
            this.txtSearch_SCCSVC.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch_SCCSVC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch_SCCSVC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch_SCCSVC.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch_SCCSVC.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch_SCCSVC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch_SCCSVC.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearch_SCCSVC.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch_SCCSVC.IconLeftOffset = new System.Drawing.Point(20, 0);
            this.txtSearch_SCCSVC.IconLeftSize = new System.Drawing.Size(25, 25);
            this.txtSearch_SCCSVC.Location = new System.Drawing.Point(177, 11);
            this.txtSearch_SCCSVC.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch_SCCSVC.Name = "txtSearch_SCCSVC";
            this.txtSearch_SCCSVC.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtSearch_SCCSVC.PlaceholderText = "Nhập để tìm kiếm";
            this.txtSearch_SCCSVC.SelectedText = "";
            this.txtSearch_SCCSVC.Size = new System.Drawing.Size(968, 51);
            this.txtSearch_SCCSVC.TabIndex = 10;
            this.txtSearch_SCCSVC.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // comfillter_SCCSVC
            // 
            this.comfillter_SCCSVC.BackColor = System.Drawing.Color.Transparent;
            this.comfillter_SCCSVC.BorderColor = System.Drawing.Color.Gray;
            this.comfillter_SCCSVC.BorderRadius = 12;
            this.comfillter_SCCSVC.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comfillter_SCCSVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comfillter_SCCSVC.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comfillter_SCCSVC.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comfillter_SCCSVC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comfillter_SCCSVC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comfillter_SCCSVC.ItemHeight = 45;
            this.comfillter_SCCSVC.Items.AddRange(new object[] {
            "Mã nhà",
            "Mã phòng"});
            this.comfillter_SCCSVC.Location = new System.Drawing.Point(3, 11);
            this.comfillter_SCCSVC.Name = "comfillter_SCCSVC";
            this.comfillter_SCCSVC.Size = new System.Drawing.Size(157, 51);
            this.comfillter_SCCSVC.StartIndex = 0;
            this.comfillter_SCCSVC.TabIndex = 0;
            this.comfillter_SCCSVC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 8;
            this.STT.Name = "STT";
            // 
            // MA_SCCSVC
            // 
            this.MA_SCCSVC.DataPropertyName = "MA_SCCSVC";
            this.MA_SCCSVC.HeaderText = "Mã SCCSVC";
            this.MA_SCCSVC.MinimumWidth = 8;
            this.MA_SCCSVC.Name = "MA_SCCSVC";
            // 
            // MANHA
            // 
            this.MANHA.DataPropertyName = "MANHA";
            this.MANHA.HeaderText = "Mã nhà";
            this.MANHA.MinimumWidth = 6;
            this.MANHA.Name = "MANHA";
            // 
            // MA_PHONG
            // 
            this.MA_PHONG.DataPropertyName = "MA_PHONG";
            this.MA_PHONG.HeaderText = "Mã phòng";
            this.MA_PHONG.MinimumWidth = 8;
            this.MA_PHONG.Name = "MA_PHONG";
            // 
            // MA_CSVC
            // 
            this.MA_CSVC.DataPropertyName = "MA_CSVC";
            this.MA_CSVC.HeaderText = "Mã CSVC";
            this.MA_CSVC.MinimumWidth = 8;
            this.MA_CSVC.Name = "MA_CSVC";
            // 
            // TEN_CSVC
            // 
            this.TEN_CSVC.DataPropertyName = "TEN_CSVC";
            this.TEN_CSVC.HeaderText = "Tên CSVC";
            this.TEN_CSVC.MinimumWidth = 8;
            this.TEN_CSVC.Name = "TEN_CSVC";
            // 
            // TEN_NHACC
            // 
            this.TEN_NHACC.DataPropertyName = "TEN_NHACC";
            this.TEN_NHACC.HeaderText = "Nhà cung cấp";
            this.TEN_NHACC.MinimumWidth = 8;
            this.TEN_NHACC.Name = "TEN_NHACC";
            // 
            // NGAY_YEUCAU
            // 
            this.NGAY_YEUCAU.DataPropertyName = "NGAY_YEUCAU";
            this.NGAY_YEUCAU.HeaderText = "Ngày yêu cầu";
            this.NGAY_YEUCAU.MinimumWidth = 6;
            this.NGAY_YEUCAU.Name = "NGAY_YEUCAU";
            // 
            // NGAY_HOANTHANH
            // 
            this.NGAY_HOANTHANH.DataPropertyName = "NGAY_HOANTHANH";
            this.NGAY_HOANTHANH.HeaderText = "Ngày hoàn thành";
            this.NGAY_HOANTHANH.MinimumWidth = 6;
            this.NGAY_HOANTHANH.Name = "NGAY_HOANTHANH";
            // 
            // TRANGTHAI
            // 
            this.TRANGTHAI.DataPropertyName = "TRANGTHAI";
            this.TRANGTHAI.HeaderText = "Trạng thái";
            this.TRANGTHAI.MinimumWidth = 6;
            this.TRANGTHAI.Name = "TRANGTHAI";
            // 
            // CHITIET
            // 
            this.CHITIET.DataPropertyName = "CHITIET";
            this.CHITIET.HeaderText = "Chi tiết";
            this.CHITIET.MinimumWidth = 6;
            this.CHITIET.Name = "CHITIET";
            // 
            // UC_SCCSVC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvSCCSVC);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblSinhVien);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_SCCSVC";
            this.Size = new System.Drawing.Size(1646, 1053);
            this.Load += new System.EventHandler(this.UC_SCCSVC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSCCSVC)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSinhVien;
        private System.Windows.Forms.DataGridView dgvSCCSVC;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2CircleButton btnfillter_SCCSVC;
        private Guna.UI2.WinForms.Guna2CircleButton btndelete_SCCSVC;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit_SCCSVC;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd_SCCSVC;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch_SCCSVC;
        private Guna.UI2.WinForms.Guna2ComboBox comfillter_SCCSVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_SCCSVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_PHONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_CSVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEN_CSVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEN_NHACC;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAY_YEUCAU;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAY_HOANTHANH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANGTHAI;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHITIET;
    }
}
