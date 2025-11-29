namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    partial class UC_NHACC
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
            this.dgvDM_NHACC = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_NHACC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEN_NHACC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIACHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GHICHU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btndelete_NHACC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit_NHACC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnadd_NHACC = new Guna.UI2.WinForms.Guna2CircleButton();
            this.lblSinhVien = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDM_NHACC)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDM_NHACC
            // 
            this.dgvDM_NHACC.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDM_NHACC.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDM_NHACC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDM_NHACC.BackgroundColor = System.Drawing.Color.White;
            this.dgvDM_NHACC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDM_NHACC.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDM_NHACC.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDM_NHACC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDM_NHACC.ColumnHeadersHeight = 45;
            this.dgvDM_NHACC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MA_NHACC,
            this.TEN_NHACC,
            this.SDT,
            this.DIACHI,
            this.GHICHU});
            this.dgvDM_NHACC.EnableHeadersVisualStyles = false;
            this.dgvDM_NHACC.Location = new System.Drawing.Point(66, 96);
            this.dgvDM_NHACC.Name = "dgvDM_NHACC";
            this.dgvDM_NHACC.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDM_NHACC.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDM_NHACC.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDM_NHACC.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDM_NHACC.RowTemplate.Height = 45;
            this.dgvDM_NHACC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDM_NHACC.Size = new System.Drawing.Size(1331, 660);
            this.dgvDM_NHACC.TabIndex = 18;
            // 
            // STT
            // 
            this.STT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.Width = 60;
            // 
            // MA_NHACC
            // 
            this.MA_NHACC.DataPropertyName = "MA_NHACC";
            this.MA_NHACC.HeaderText = "Mã nhà cung cấp";
            this.MA_NHACC.MinimumWidth = 8;
            this.MA_NHACC.Name = "MA_NHACC";
            // 
            // TEN_NHACC
            // 
            this.TEN_NHACC.DataPropertyName = "TEN_NHACC";
            this.TEN_NHACC.HeaderText = "Tên nhà cung cấp";
            this.TEN_NHACC.MinimumWidth = 8;
            this.TEN_NHACC.Name = "TEN_NHACC";
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "SDT";
            this.SDT.HeaderText = "Số điện thoại";
            this.SDT.MinimumWidth = 8;
            this.SDT.Name = "SDT";
            // 
            // DIACHI
            // 
            this.DIACHI.DataPropertyName = "DIACHI";
            this.DIACHI.HeaderText = "Địa chỉ";
            this.DIACHI.MinimumWidth = 8;
            this.DIACHI.Name = "DIACHI";
            // 
            // GHICHU
            // 
            this.GHICHU.DataPropertyName = "GHICHU";
            this.GHICHU.HeaderText = "Ghi chú";
            this.GHICHU.MinimumWidth = 6;
            this.GHICHU.Name = "GHICHU";
            // 
            // btndelete_NHACC
            // 
            this.btndelete_NHACC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.del;
            this.btndelete_NHACC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndelete_NHACC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_NHACC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_NHACC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btndelete_NHACC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btndelete_NHACC.FillColor = System.Drawing.Color.Transparent;
            this.btndelete_NHACC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btndelete_NHACC.ForeColor = System.Drawing.Color.White;
            this.btndelete_NHACC.Location = new System.Drawing.Point(1362, 58);
            this.btndelete_NHACC.Name = "btndelete_NHACC";
            this.btndelete_NHACC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btndelete_NHACC.Size = new System.Drawing.Size(35, 32);
            this.btndelete_NHACC.TabIndex = 21;
            this.btndelete_NHACC.Click += new System.EventHandler(this.btndelete_NHACC_Click);
            // 
            // btnedit_NHACC
            // 
            this.btnedit_NHACC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.edit;
            this.btnedit_NHACC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnedit_NHACC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_NHACC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_NHACC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit_NHACC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit_NHACC.FillColor = System.Drawing.Color.Transparent;
            this.btnedit_NHACC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit_NHACC.ForeColor = System.Drawing.Color.White;
            this.btnedit_NHACC.Location = new System.Drawing.Point(1325, 58);
            this.btnedit_NHACC.Name = "btnedit_NHACC";
            this.btnedit_NHACC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit_NHACC.Size = new System.Drawing.Size(31, 32);
            this.btnedit_NHACC.TabIndex = 20;
            this.btnedit_NHACC.Click += new System.EventHandler(this.btnedit_NHACC_Click);
            // 
            // btnadd_NHACC
            // 
            this.btnadd_NHACC.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnadd_NHACC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadd_NHACC.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_NHACC.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_NHACC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnadd_NHACC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnadd_NHACC.FillColor = System.Drawing.Color.Transparent;
            this.btnadd_NHACC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnadd_NHACC.ForeColor = System.Drawing.Color.White;
            this.btnadd_NHACC.Location = new System.Drawing.Point(1280, 58);
            this.btnadd_NHACC.Name = "btnadd_NHACC";
            this.btnadd_NHACC.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd_NHACC.Size = new System.Drawing.Size(30, 32);
            this.btnadd_NHACC.TabIndex = 19;
            this.btnadd_NHACC.Click += new System.EventHandler(this.btnadd_NHACC_Click);
            // 
            // lblSinhVien
            // 
            this.lblSinhVien.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSinhVien.AutoSize = true;
            this.lblSinhVien.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSinhVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            this.lblSinhVien.Location = new System.Drawing.Point(491, 14);
            this.lblSinhVien.Name = "lblSinhVien";
            this.lblSinhVien.Size = new System.Drawing.Size(476, 46);
            this.lblSinhVien.TabIndex = 23;
            this.lblSinhVien.Text = "DANH MỤC NHÀ CUNG CẤP";
            // 
            // UC_NHACC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSinhVien);
            this.Controls.Add(this.btndelete_NHACC);
            this.Controls.Add(this.btnedit_NHACC);
            this.Controls.Add(this.btnadd_NHACC);
            this.Controls.Add(this.dgvDM_NHACC);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_NHACC";
            this.Size = new System.Drawing.Size(1474, 846);
            this.Load += new System.EventHandler(this.UC_NHACC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDM_NHACC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDM_NHACC;
        private Guna.UI2.WinForms.Guna2CircleButton btndelete_NHACC;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit_NHACC;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd_NHACC;
        private System.Windows.Forms.Label lblSinhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_NHACC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEN_NHACC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIACHI;
        private System.Windows.Forms.DataGridViewTextBoxColumn GHICHU;
    }
}
