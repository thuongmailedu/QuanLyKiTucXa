namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    partial class frm_addHD_INT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpTHOIGIAN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_add_HD_INT = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_PHONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONGIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SO_SV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TONGTIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THOIGIAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TINHTRANGTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Lưu = new Guna.UI2.WinForms.Guna2Button();
            this.btn_Huy = new Guna.UI2.WinForms.Guna2Button();
            this.comNHA = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTaoHD = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_add_HD_INT)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTHOIGIAN
            // 
            this.dtpTHOIGIAN.BackColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.BorderColor = System.Drawing.Color.Silver;
            this.dtpTHOIGIAN.BorderThickness = 1;
            this.dtpTHOIGIAN.Checked = true;
            this.dtpTHOIGIAN.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.CustomFormat = "MM/yyyy";
            this.dtpTHOIGIAN.FillColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.FocusedColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.dtpTHOIGIAN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTHOIGIAN.Location = new System.Drawing.Point(683, 77);
            this.dtpTHOIGIAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHOIGIAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHOIGIAN.Name = "dtpTHOIGIAN";
            this.dtpTHOIGIAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHOIGIAN.Size = new System.Drawing.Size(190, 45);
            this.dtpTHOIGIAN.TabIndex = 26;
            this.dtpTHOIGIAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(585, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 25;
            this.label3.Text = "Thời gian";
            // 
            // dgv_add_HD_INT
            // 
            this.dgv_add_HD_INT.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_add_HD_INT.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_add_HD_INT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_add_HD_INT.BackgroundColor = System.Drawing.Color.White;
            this.dgv_add_HD_INT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_add_HD_INT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_add_HD_INT.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_add_HD_INT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_add_HD_INT.ColumnHeadersHeight = 45;
            this.dgv_add_HD_INT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MAHD_INT,
            this.TENHD,
            this.MANHA,
            this.MA_PHONG,
            this.DONGIA,
            this.SO_SV,
            this.TONGTIEN,
            this.THOIGIAN,
            this.TINHTRANGTT});
            this.dgv_add_HD_INT.EnableHeadersVisualStyles = false;
            this.dgv_add_HD_INT.Location = new System.Drawing.Point(46, 144);
            this.dgv_add_HD_INT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_add_HD_INT.Name = "dgv_add_HD_INT";
            this.dgv_add_HD_INT.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_add_HD_INT.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_add_HD_INT.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_add_HD_INT.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_add_HD_INT.RowTemplate.Height = 45;
            this.dgv_add_HD_INT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_add_HD_INT.Size = new System.Drawing.Size(1008, 540);
            this.dgv_add_HD_INT.TabIndex = 27;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 8;
            this.STT.Name = "STT";
            this.STT.Width = 60;
            // 
            // MAHD_INT
            // 
            this.MAHD_INT.DataPropertyName = "MAHD_INT";
            this.MAHD_INT.HeaderText = "Mã hóa đơn";
            this.MAHD_INT.MinimumWidth = 8;
            this.MAHD_INT.Name = "MAHD_INT";
            this.MAHD_INT.Width = 107;
            // 
            // TENHD
            // 
            this.TENHD.DataPropertyName = "TENHD";
            this.TENHD.HeaderText = "Tên hóa đơn";
            this.TENHD.MinimumWidth = 8;
            this.TENHD.Name = "TENHD";
            this.TENHD.Width = 108;
            // 
            // MANHA
            // 
            this.MANHA.DataPropertyName = "MANHA";
            this.MANHA.HeaderText = "Mã nhà";
            this.MANHA.MinimumWidth = 6;
            this.MANHA.Name = "MANHA";
            this.MANHA.Width = 79;
            // 
            // MA_PHONG
            // 
            this.MA_PHONG.DataPropertyName = "MA_PHONG";
            this.MA_PHONG.HeaderText = "Mã phòng";
            this.MA_PHONG.MinimumWidth = 6;
            this.MA_PHONG.Name = "MA_PHONG";
            this.MA_PHONG.Width = 96;
            // 
            // DONGIA
            // 
            this.DONGIA.DataPropertyName = "DONGIA";
            this.DONGIA.HeaderText = "Đơn giá";
            this.DONGIA.MinimumWidth = 8;
            this.DONGIA.Name = "DONGIA";
            this.DONGIA.Width = 82;
            // 
            // SO_SV
            // 
            this.SO_SV.DataPropertyName = "SO_SV";
            this.SO_SV.HeaderText = "Số sinh viên";
            this.SO_SV.MinimumWidth = 6;
            this.SO_SV.Name = "SO_SV";
            this.SO_SV.Width = 105;
            // 
            // TONGTIEN
            // 
            this.TONGTIEN.DataPropertyName = "TONGTIEN";
            this.TONGTIEN.HeaderText = "Tổng tiền";
            this.TONGTIEN.MinimumWidth = 6;
            this.TONGTIEN.Name = "TONGTIEN";
            this.TONGTIEN.Width = 91;
            // 
            // THOIGIAN
            // 
            this.THOIGIAN.DataPropertyName = "THOIGIAN";
            this.THOIGIAN.HeaderText = "Thời gian";
            this.THOIGIAN.MinimumWidth = 8;
            this.THOIGIAN.Name = "THOIGIAN";
            this.THOIGIAN.Width = 90;
            // 
            // TINHTRANGTT
            // 
            this.TINHTRANGTT.DataPropertyName = "TINHTRANGTT";
            this.TINHTRANGTT.HeaderText = "Trạng thái";
            this.TINHTRANGTT.MinimumWidth = 6;
            this.TINHTRANGTT.Name = "TINHTRANGTT";
            this.TINHTRANGTT.Width = 94;
            // 
            // btn_Lưu
            // 
            this.btn_Lưu.BorderRadius = 3;
            this.btn_Lưu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Lưu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Lưu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Lưu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Lưu.FillColor = System.Drawing.Color.Navy;
            this.btn_Lưu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Lưu.ForeColor = System.Drawing.Color.White;
            this.btn_Lưu.Location = new System.Drawing.Point(939, 710);
            this.btn_Lưu.Name = "btn_Lưu";
            this.btn_Lưu.Size = new System.Drawing.Size(115, 45);
            this.btn_Lưu.TabIndex = 29;
            this.btn_Lưu.Text = "Lưu";
            // 
            // btn_Huy
            // 
            this.btn_Huy.BorderRadius = 3;
            this.btn_Huy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Huy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Huy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Huy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Huy.FillColor = System.Drawing.Color.Navy;
            this.btn_Huy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Huy.ForeColor = System.Drawing.Color.White;
            this.btn_Huy.Location = new System.Drawing.Point(778, 710);
            this.btn_Huy.Name = "btn_Huy";
            this.btn_Huy.Size = new System.Drawing.Size(118, 45);
            this.btn_Huy.TabIndex = 29;
            this.btn_Huy.Text = "Hủy";
            this.btn_Huy.Click += new System.EventHandler(this.btn_Huy_Click);
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
            this.comNHA.Location = new System.Drawing.Point(365, 71);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(157, 51);
            this.comNHA.TabIndex = 30;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(39, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 38);
            this.label1.TabIndex = 25;
            this.label1.Text = "Thêm mới hóa đơn Internet";
            // 
            // btnTaoHD
            // 
            this.btnTaoHD.BorderRadius = 3;
            this.btnTaoHD.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoHD.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoHD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoHD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoHD.FillColor = System.Drawing.Color.Navy;
            this.btnTaoHD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoHD.ForeColor = System.Drawing.Color.White;
            this.btnTaoHD.Location = new System.Drawing.Point(936, 77);
            this.btnTaoHD.Name = "btnTaoHD";
            this.btnTaoHD.Size = new System.Drawing.Size(118, 45);
            this.btnTaoHD.TabIndex = 29;
            this.btnTaoHD.Text = "Tạo hóa đơn";
            this.btnTaoHD.Click += new System.EventHandler(this.btnTaoHD_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(311, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 23);
            this.label2.TabIndex = 25;
            this.label2.Text = "Nhà";
            // 
            // frm_addHD_INT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 791);
            this.Controls.Add(this.comNHA);
            this.Controls.Add(this.btnTaoHD);
            this.Controls.Add(this.btn_Huy);
            this.Controls.Add(this.btn_Lưu);
            this.Controls.Add(this.dgv_add_HD_INT);
            this.Controls.Add(this.dtpTHOIGIAN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_addHD_INT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin hóa đơn internet";
            this.Load += new System.EventHandler(this.frm_addHD_INT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_add_HD_INT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHOIGIAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_add_HD_INT;
        private Guna.UI2.WinForms.Guna2Button btn_Lưu;
        private Guna.UI2.WinForms.Guna2Button btn_Huy;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_INT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_PHONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONGIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn SO_SV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TONGTIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn THOIGIAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TINHTRANGTT;
        private Guna.UI2.WinForms.Guna2Button btnTaoHD;
        private System.Windows.Forms.Label label2;
    }
}