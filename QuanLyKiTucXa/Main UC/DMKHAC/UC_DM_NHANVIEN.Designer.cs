namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    partial class UC_DM_NHANVIEN
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
            this.dgvDMPhong = new System.Windows.Forms.DataGridView();
            this.lblSinhVien = new System.Windows.Forms.Label();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btndelete_NHA = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit_NHANVIEN = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnadd_NHANVIEN = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDMPhong
            // 
            this.dgvDMPhong.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDMPhong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDMPhong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvDMPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDMPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvDMPhong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDMPhong.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDMPhong.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDMPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDMPhong.ColumnHeadersHeight = 45;
            this.dgvDMPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MANHA,
            this.MANV,
            this.TENNV});
            this.dgvDMPhong.EnableHeadersVisualStyles = false;
            this.dgvDMPhong.Location = new System.Drawing.Point(65, 109);
            this.dgvDMPhong.Name = "dgvDMPhong";
            this.dgvDMPhong.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDMPhong.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDMPhong.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDMPhong.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDMPhong.RowTemplate.Height = 45;
            this.dgvDMPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDMPhong.Size = new System.Drawing.Size(1438, 653);
            this.dgvDMPhong.TabIndex = 18;
            // 
            // lblSinhVien
            // 
            this.lblSinhVien.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSinhVien.AutoSize = true;
            this.lblSinhVien.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSinhVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            this.lblSinhVien.Location = new System.Drawing.Point(466, 23);
            this.lblSinhVien.Name = "lblSinhVien";
            this.lblSinhVien.Size = new System.Drawing.Size(620, 46);
            this.lblSinhVien.TabIndex = 19;
            this.lblSinhVien.Text = "DANH MỤC NHÂN VIÊN _TÀI KHOẢN";
            // 
            // STT
            // 
            this.STT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STT.DataPropertyName = "STT";
            this.STT.FillWeight = 130.5728F;
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.Width = 60;
            // 
            // MANHA
            // 
            this.MANHA.DataPropertyName = "MANHA";
            this.MANHA.FillWeight = 119.2565F;
            this.MANHA.HeaderText = "Nhà";
            this.MANHA.MinimumWidth = 6;
            this.MANHA.Name = "MANHA";
            // 
            // MANV
            // 
            this.MANV.DataPropertyName = "MANV";
            this.MANV.FillWeight = 119.2565F;
            this.MANV.HeaderText = "Mã nhân viên";
            this.MANV.MinimumWidth = 8;
            this.MANV.Name = "MANV";
            // 
            // TENNV
            // 
            this.TENNV.DataPropertyName = "TENNV";
            this.TENNV.FillWeight = 119.2565F;
            this.TENNV.HeaderText = "Tên nhân viên";
            this.TENNV.MinimumWidth = 8;
            this.TENNV.Name = "TENNV";
            // 
            // btndelete_NHA
            // 
            this.btndelete_NHA.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.del;
            this.btndelete_NHA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndelete_NHA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_NHA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_NHA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btndelete_NHA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btndelete_NHA.FillColor = System.Drawing.Color.Transparent;
            this.btndelete_NHA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btndelete_NHA.ForeColor = System.Drawing.Color.White;
            this.btndelete_NHA.Location = new System.Drawing.Point(1468, 71);
            this.btndelete_NHA.Name = "btndelete_NHA";
            this.btndelete_NHA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btndelete_NHA.Size = new System.Drawing.Size(35, 32);
            this.btndelete_NHA.TabIndex = 22;
            // 
            // btnedit_NHANVIEN
            // 
            this.btnedit_NHANVIEN.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.edit;
            this.btnedit_NHANVIEN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnedit_NHANVIEN.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_NHANVIEN.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_NHANVIEN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit_NHANVIEN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit_NHANVIEN.FillColor = System.Drawing.Color.Transparent;
            this.btnedit_NHANVIEN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit_NHANVIEN.ForeColor = System.Drawing.Color.White;
            this.btnedit_NHANVIEN.Location = new System.Drawing.Point(1431, 71);
            this.btnedit_NHANVIEN.Name = "btnedit_NHANVIEN";
            this.btnedit_NHANVIEN.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit_NHANVIEN.Size = new System.Drawing.Size(31, 32);
            this.btnedit_NHANVIEN.TabIndex = 21;
            // 
            // btnadd_NHANVIEN
            // 
            this.btnadd_NHANVIEN.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnadd_NHANVIEN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadd_NHANVIEN.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_NHANVIEN.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_NHANVIEN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnadd_NHANVIEN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnadd_NHANVIEN.FillColor = System.Drawing.Color.Transparent;
            this.btnadd_NHANVIEN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnadd_NHANVIEN.ForeColor = System.Drawing.Color.White;
            this.btnadd_NHANVIEN.Location = new System.Drawing.Point(1383, 71);
            this.btnadd_NHANVIEN.Name = "btnadd_NHANVIEN";
            this.btnadd_NHANVIEN.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd_NHANVIEN.Size = new System.Drawing.Size(30, 32);
            this.btnadd_NHANVIEN.TabIndex = 20;
            // 
            // UC_DM_NHANVIEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btndelete_NHA);
            this.Controls.Add(this.btnedit_NHANVIEN);
            this.Controls.Add(this.btnadd_NHANVIEN);
            this.Controls.Add(this.lblSinhVien);
            this.Controls.Add(this.dgvDMPhong);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_DM_NHANVIEN";
            this.Size = new System.Drawing.Size(1588, 870);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDMPhong;
        private System.Windows.Forms.Label lblSinhVien;
        private Guna.UI2.WinForms.Guna2CircleButton btndelete_NHA;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit_NHANVIEN;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd_NHANVIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENNV;
    }
}
