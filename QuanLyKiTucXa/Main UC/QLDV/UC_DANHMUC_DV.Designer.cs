namespace QuanLyKiTucXa.Main_UC.QLDV
{
    partial class UC_DANHMUC_DV
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
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_DICHVU = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MADV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONGIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONVI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TUNGAY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DENNGAY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_NHACC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANGTHAI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnfillter = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnDelete = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnadd = new Guna.UI2.WinForms.Guna2CircleButton();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.comGIATRI = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DICHVU)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(627, 11);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(318, 41);
            this.label8.TabIndex = 18;
            this.label8.Text = "DANH MỤC DỊCH VỤ";
            // 
            // dgv_DICHVU
            // 
            this.dgv_DICHVU.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_DICHVU.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_DICHVU.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DICHVU.BackgroundColor = System.Drawing.Color.White;
            this.dgv_DICHVU.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_DICHVU.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DICHVU.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_DICHVU.ColumnHeadersHeight = 45;
            this.dgv_DICHVU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MADV,
            this.TENDV,
            this.DONGIA,
            this.DONVI,
            this.TUNGAY,
            this.DENNGAY,
            this.TEN_NHACC,
            this.TRANGTHAI});
            this.dgv_DICHVU.EnableHeadersVisualStyles = false;
            this.dgv_DICHVU.Location = new System.Drawing.Point(49, 139);
            this.dgv_DICHVU.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_DICHVU.Name = "dgv_DICHVU";
            this.dgv_DICHVU.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DICHVU.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_DICHVU.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_DICHVU.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_DICHVU.RowTemplate.Height = 45;
            this.dgv_DICHVU.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DICHVU.Size = new System.Drawing.Size(1490, 676);
            this.dgv_DICHVU.TabIndex = 19;
            // 
            // STT
            // 
            this.STT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 8;
            this.STT.Name = "STT";
            this.STT.Width = 60;
            // 
            // MADV
            // 
            this.MADV.DataPropertyName = "MADV";
            this.MADV.HeaderText = "Mã dịch vụ";
            this.MADV.MinimumWidth = 8;
            this.MADV.Name = "MADV";
            // 
            // TENDV
            // 
            this.TENDV.DataPropertyName = "TENDV";
            this.TENDV.HeaderText = "Tên dịch vụ";
            this.TENDV.MinimumWidth = 8;
            this.TENDV.Name = "TENDV";
            // 
            // DONGIA
            // 
            this.DONGIA.DataPropertyName = "DONGIA";
            this.DONGIA.HeaderText = "Đơn giá";
            this.DONGIA.MinimumWidth = 8;
            this.DONGIA.Name = "DONGIA";
            // 
            // DONVI
            // 
            this.DONVI.DataPropertyName = "DONVI";
            this.DONVI.HeaderText = "Đơn vị";
            this.DONVI.MinimumWidth = 8;
            this.DONVI.Name = "DONVI";
            // 
            // TUNGAY
            // 
            this.TUNGAY.DataPropertyName = "TUNGAY";
            this.TUNGAY.HeaderText = "Từ ngày";
            this.TUNGAY.MinimumWidth = 8;
            this.TUNGAY.Name = "TUNGAY";
            // 
            // DENNGAY
            // 
            this.DENNGAY.DataPropertyName = "DENNGAY";
            this.DENNGAY.HeaderText = "Đến ngày";
            this.DENNGAY.MinimumWidth = 6;
            this.DENNGAY.Name = "DENNGAY";
            // 
            // TEN_NHACC
            // 
            this.TEN_NHACC.DataPropertyName = "TEN_NHACC";
            this.TEN_NHACC.HeaderText = "Nhà cung cấp";
            this.TEN_NHACC.MinimumWidth = 6;
            this.TEN_NHACC.Name = "TEN_NHACC";
            // 
            // TRANGTHAI
            // 
            this.TRANGTHAI.DataPropertyName = "TRANGTHAI";
            this.TRANGTHAI.HeaderText = "Trạng thái";
            this.TRANGTHAI.MinimumWidth = 6;
            this.TRANGTHAI.Name = "TRANGTHAI";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnfillter);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnedit);
            this.panel1.Controls.Add(this.btnadd);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.comGIATRI);
            this.panel1.Location = new System.Drawing.Point(46, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1493, 71);
            this.panel1.TabIndex = 26;
            // 
            // btnfillter
            // 
            this.btnfillter.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.filter;
            this.btnfillter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnfillter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnfillter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnfillter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnfillter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnfillter.FillColor = System.Drawing.Color.Transparent;
            this.btnfillter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnfillter.ForeColor = System.Drawing.Color.White;
            this.btnfillter.Location = new System.Drawing.Point(1310, 14);
            this.btnfillter.Name = "btnfillter";
            this.btnfillter.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnfillter.Size = new System.Drawing.Size(35, 40);
            this.btnfillter.TabIndex = 14;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.del;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDelete.FillColor = System.Drawing.Color.Transparent;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(1364, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnDelete.Size = new System.Drawing.Size(35, 32);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnedit
            // 
            this.btnedit.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.edit;
            this.btnedit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnedit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit.FillColor = System.Drawing.Color.Transparent;
            this.btnedit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit.ForeColor = System.Drawing.Color.White;
            this.btnedit.Location = new System.Drawing.Point(1416, 22);
            this.btnedit.Name = "btnedit";
            this.btnedit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit.Size = new System.Drawing.Size(31, 32);
            this.btnedit.TabIndex = 12;
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // btnadd
            // 
            this.btnadd.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnadd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnadd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnadd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnadd.FillColor = System.Drawing.Color.Transparent;
            this.btnadd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnadd.ForeColor = System.Drawing.Color.White;
            this.btnadd.Location = new System.Drawing.Point(1463, 22);
            this.btnadd.Name = "btnadd";
            this.btnadd.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd.Size = new System.Drawing.Size(30, 32);
            this.btnadd.TabIndex = 11;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderColor = System.Drawing.Color.Gray;
            this.txtSearch.BorderRadius = 12;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.IconLeftOffset = new System.Drawing.Point(20, 0);
            this.txtSearch.IconLeftSize = new System.Drawing.Size(25, 25);
            this.txtSearch.Location = new System.Drawing.Point(177, 11);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.PlaceholderText = "Nhập để tìm kiếm";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(1065, 51);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // comGIATRI
            // 
            this.comGIATRI.BackColor = System.Drawing.Color.Transparent;
            this.comGIATRI.BorderColor = System.Drawing.Color.Gray;
            this.comGIATRI.BorderRadius = 12;
            this.comGIATRI.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comGIATRI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comGIATRI.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comGIATRI.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comGIATRI.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comGIATRI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comGIATRI.ItemHeight = 45;
            this.comGIATRI.Items.AddRange(new object[] {
            "TENDV"});
            this.comGIATRI.Location = new System.Drawing.Point(3, 11);
            this.comGIATRI.Name = "comGIATRI";
            this.comGIATRI.Size = new System.Drawing.Size(157, 51);
            this.comGIATRI.StartIndex = 0;
            this.comGIATRI.TabIndex = 0;
            this.comGIATRI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comGIATRI.SelectedIndexChanged += new System.EventHandler(this.comGIATRI_SelectedIndexChanged);
            // 
            // UC_DANHMUC_DV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_DICHVU);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_DANHMUC_DV";
            this.Size = new System.Drawing.Size(1580, 849);
            this.Load += new System.EventHandler(this.UC_DANHMUC_DV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DICHVU)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_DICHVU;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2CircleButton btnfillter;
        private Guna.UI2.WinForms.Guna2CircleButton btnDelete;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2ComboBox comGIATRI;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MADV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONGIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONVI;
        private System.Windows.Forms.DataGridViewTextBoxColumn TUNGAY;
        private System.Windows.Forms.DataGridViewTextBoxColumn DENNGAY;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEN_NHACC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANGTHAI;
    }
}
