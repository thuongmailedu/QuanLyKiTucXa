namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    partial class UC_DM_LOP_KHOA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDMPhong = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANHA_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GIOITINH_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOAIPHONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GIAPHONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOIDA_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btndelete_NHA = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit_NHA = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnadd_NHA = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDMPhong
            // 
            this.dgvDMPhong.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDMPhong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDMPhong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvDMPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDMPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvDMPhong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDMPhong.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDMPhong.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDMPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDMPhong.ColumnHeadersHeight = 45;
            this.dgvDMPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MANHA_2,
            this.GIOITINH_2,
            this.LOAIPHONG,
            this.GIAPHONG,
            this.TOIDA_2});
            this.dgvDMPhong.EnableHeadersVisualStyles = false;
            this.dgvDMPhong.Location = new System.Drawing.Point(433, 203);
            this.dgvDMPhong.Name = "dgvDMPhong";
            this.dgvDMPhong.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDMPhong.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDMPhong.RowHeadersWidth = 62;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDMPhong.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDMPhong.RowTemplate.Height = 45;
            this.dgvDMPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDMPhong.Size = new System.Drawing.Size(579, 621);
            this.dgvDMPhong.TabIndex = 18;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            // 
            // MANHA_2
            // 
            this.MANHA_2.DataPropertyName = "MANHA";
            this.MANHA_2.HeaderText = "Mã nhà";
            this.MANHA_2.MinimumWidth = 8;
            this.MANHA_2.Name = "MANHA_2";
            // 
            // GIOITINH_2
            // 
            this.GIOITINH_2.DataPropertyName = "GIOITINH";
            this.GIOITINH_2.HeaderText = "Giới tính";
            this.GIOITINH_2.MinimumWidth = 8;
            this.GIOITINH_2.Name = "GIOITINH_2";
            // 
            // LOAIPHONG
            // 
            this.LOAIPHONG.DataPropertyName = "LOAIPHONG";
            this.LOAIPHONG.HeaderText = "Loại phòng";
            this.LOAIPHONG.MinimumWidth = 8;
            this.LOAIPHONG.Name = "LOAIPHONG";
            // 
            // GIAPHONG
            // 
            this.GIAPHONG.DataPropertyName = "GIAPHONG";
            this.GIAPHONG.HeaderText = "Giá phòng";
            this.GIAPHONG.MinimumWidth = 8;
            this.GIAPHONG.Name = "GIAPHONG";
            // 
            // TOIDA_2
            // 
            this.TOIDA_2.DataPropertyName = "TOIDA";
            this.TOIDA_2.HeaderText = "Tối đa";
            this.TOIDA_2.MinimumWidth = 6;
            this.TOIDA_2.Name = "TOIDA_2";
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
            this.btndelete_NHA.Location = new System.Drawing.Point(612, 99);
            this.btndelete_NHA.Name = "btndelete_NHA";
            this.btndelete_NHA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btndelete_NHA.Size = new System.Drawing.Size(35, 32);
            this.btndelete_NHA.TabIndex = 21;
            // 
            // btnedit_NHA
            // 
            this.btnedit_NHA.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.edit;
            this.btnedit_NHA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnedit_NHA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_NHA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_NHA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit_NHA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit_NHA.FillColor = System.Drawing.Color.Transparent;
            this.btnedit_NHA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit_NHA.ForeColor = System.Drawing.Color.White;
            this.btnedit_NHA.Location = new System.Drawing.Point(575, 99);
            this.btnedit_NHA.Name = "btnedit_NHA";
            this.btnedit_NHA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit_NHA.Size = new System.Drawing.Size(31, 32);
            this.btnedit_NHA.TabIndex = 20;
            // 
            // btnadd_NHA
            // 
            this.btnadd_NHA.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnadd_NHA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadd_NHA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_NHA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_NHA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnadd_NHA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnadd_NHA.FillColor = System.Drawing.Color.Transparent;
            this.btnadd_NHA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnadd_NHA.ForeColor = System.Drawing.Color.White;
            this.btnadd_NHA.Location = new System.Drawing.Point(530, 99);
            this.btnadd_NHA.Name = "btnadd_NHA";
            this.btnadd_NHA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd_NHA.Size = new System.Drawing.Size(30, 32);
            this.btnadd_NHA.TabIndex = 19;
            // 
            // UC_DM_LOP_KHOA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btndelete_NHA);
            this.Controls.Add(this.btnedit_NHA);
            this.Controls.Add(this.btnadd_NHA);
            this.Controls.Add(this.dgvDMPhong);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_DM_LOP_KHOA";
            this.Size = new System.Drawing.Size(1527, 878);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMPhong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDMPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANHA_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIOITINH_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOAIPHONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIAPHONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOIDA_2;
        private Guna.UI2.WinForms.Guna2CircleButton btndelete_NHA;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit_NHA;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd_NHA;
    }
}
