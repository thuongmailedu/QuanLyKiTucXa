using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    partial class UC_HOADON_DV
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btncancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnupdate = new Guna.UI2.WinForms.Guna2Button();
            this.btnfillter = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnDelete = new Guna.UI2.WinForms.Guna2CircleButton();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_HD_TONGHOP = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_PHONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENHD_TH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THOIGIAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_DIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIENDIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_NUOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIENNUOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIENINTERNET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TONGTIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TINHTRANGTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comTRANGTHAI = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comPHONG = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comNHA = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpTHOIGIAN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HD_TONGHOP)).BeginInit();
            this.SuspendLayout();
            // 
            // btncancel
            // 
            this.btncancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btncancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btncancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btncancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btncancel.FillColor = System.Drawing.Color.White;
            this.btncancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Image = global::QuanLyKiTucXa.Properties.Resources.cancel;
            this.btncancel.Location = new System.Drawing.Point(1394, 105);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(46, 40);
            this.btncancel.TabIndex = 62;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnupdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnupdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnupdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnupdate.FillColor = System.Drawing.Color.White;
            this.btnupdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnupdate.ForeColor = System.Drawing.Color.White;
            this.btnupdate.Image = global::QuanLyKiTucXa.Properties.Resources.circle_ok;
            this.btnupdate.Location = new System.Drawing.Point(1457, 109);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(40, 36);
            this.btnupdate.TabIndex = 61;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
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
            this.btnfillter.Location = new System.Drawing.Point(1199, 99);
            this.btnfillter.Name = "btnfillter";
            this.btnfillter.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnfillter.Size = new System.Drawing.Size(35, 40);
            this.btnfillter.TabIndex = 57;
            this.btnfillter.Click += new System.EventHandler(this.btnfillter_Click);
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
            this.btnDelete.Location = new System.Drawing.Point(1332, 107);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnDelete.Size = new System.Drawing.Size(35, 32);
            this.btnDelete.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(579, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(328, 41);
            this.label8.TabIndex = 59;
            this.label8.Text = "HÓA ĐƠN TỔNG HỢP";
            // 
            // dgv_HD_TONGHOP
            // 
            this.dgv_HD_TONGHOP.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_HD_TONGHOP.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_HD_TONGHOP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_HD_TONGHOP.BackgroundColor = System.Drawing.Color.White;
            this.dgv_HD_TONGHOP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_HD_TONGHOP.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_HD_TONGHOP.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HD_TONGHOP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgv_HD_TONGHOP.ColumnHeadersHeight = 45;
            this.dgv_HD_TONGHOP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MANHA,
            this.MA_PHONG,
            this.TENHD_TH,
            this.THOIGIAN,
            this.MAHD_DIEN,
            this.TIENDIEN,
            this.MAHD_NUOC,
            this.TIENNUOC,
            this.MAHD_INT,
            this.TIENINTERNET,
            this.TONGTIEN,
            this.TINHTRANGTT});
            this.dgv_HD_TONGHOP.EnableHeadersVisualStyles = false;
            this.dgv_HD_TONGHOP.Location = new System.Drawing.Point(16, 172);
            this.dgv_HD_TONGHOP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_HD_TONGHOP.Name = "dgv_HD_TONGHOP";
            this.dgv_HD_TONGHOP.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HD_TONGHOP.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgv_HD_TONGHOP.RowHeadersWidth = 62;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_HD_TONGHOP.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgv_HD_TONGHOP.RowTemplate.Height = 45;
            this.dgv_HD_TONGHOP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_HD_TONGHOP.Size = new System.Drawing.Size(1490, 602);
            this.dgv_HD_TONGHOP.TabIndex = 58;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 8;
            this.STT.Name = "STT";
            this.STT.Width = 60;
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
            // TENHD_TH
            // 
            this.TENHD_TH.DataPropertyName = "TENHD_TH";
            this.TENHD_TH.HeaderText = "Tên hóa đơn";
            this.TENHD_TH.MinimumWidth = 8;
            this.TENHD_TH.Name = "TENHD_TH";
            this.TENHD_TH.Width = 108;
            // 
            // THOIGIAN
            // 
            this.THOIGIAN.DataPropertyName = "THOIGIAN";
            this.THOIGIAN.HeaderText = "Thời gian";
            this.THOIGIAN.MinimumWidth = 8;
            this.THOIGIAN.Name = "THOIGIAN";
            this.THOIGIAN.Width = 90;
            // 
            // MAHD_DIEN
            // 
            this.MAHD_DIEN.DataPropertyName = "MAHD_DIEN";
            this.MAHD_DIEN.HeaderText = "Mã HD Điện";
            this.MAHD_DIEN.MinimumWidth = 8;
            this.MAHD_DIEN.Name = "MAHD_DIEN";
            this.MAHD_DIEN.Width = 108;
            // 
            // TIENDIEN
            // 
            this.TIENDIEN.DataPropertyName = "TIENDIEN";
            this.TIENDIEN.HeaderText = "Tiền điện";
            this.TIENDIEN.MinimumWidth = 6;
            this.TIENDIEN.Name = "TIENDIEN";
            this.TIENDIEN.Width = 90;
            // 
            // MAHD_NUOC
            // 
            this.MAHD_NUOC.DataPropertyName = "MAHD_NUOC";
            this.MAHD_NUOC.HeaderText = "Mã HD Nước";
            this.MAHD_NUOC.MinimumWidth = 6;
            this.MAHD_NUOC.Name = "MAHD_NUOC";
            this.MAHD_NUOC.Width = 113;
            // 
            // TIENNUOC
            // 
            this.TIENNUOC.DataPropertyName = "TIENNUOC";
            this.TIENNUOC.HeaderText = "Tiền nước";
            this.TIENNUOC.MinimumWidth = 6;
            this.TIENNUOC.Name = "TIENNUOC";
            this.TIENNUOC.Width = 93;
            // 
            // MAHD_INT
            // 
            this.MAHD_INT.DataPropertyName = "MAHD_INT";
            this.MAHD_INT.HeaderText = "Mã HD Internet";
            this.MAHD_INT.MinimumWidth = 6;
            this.MAHD_INT.Name = "MAHD_INT";
            this.MAHD_INT.Width = 126;
            // 
            // TIENINTERNET
            // 
            this.TIENINTERNET.DataPropertyName = "TIENINTERNET";
            this.TIENINTERNET.HeaderText = "Tiền internet";
            this.TIENINTERNET.MinimumWidth = 6;
            this.TIENINTERNET.Name = "TIENINTERNET";
            this.TIENINTERNET.Width = 109;
            // 
            // TONGTIEN
            // 
            this.TONGTIEN.DataPropertyName = "TONGTIEN";
            this.TONGTIEN.HeaderText = "Tổng tiền";
            this.TONGTIEN.MinimumWidth = 6;
            this.TONGTIEN.Name = "TONGTIEN";
            this.TONGTIEN.Width = 91;
            // 
            // TINHTRANGTT
            // 
            this.TINHTRANGTT.DataPropertyName = "TINHTRANGTT";
            this.TINHTRANGTT.HeaderText = "Trạng thái";
            this.TINHTRANGTT.MinimumWidth = 6;
            this.TINHTRANGTT.Name = "TINHTRANGTT";
            this.TINHTRANGTT.Width = 94;
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
            this.comTRANGTHAI.Location = new System.Drawing.Point(757, 100);
            this.comTRANGTHAI.Name = "comTRANGTHAI";
            this.comTRANGTHAI.Size = new System.Drawing.Size(157, 51);
            this.comTRANGTHAI.TabIndex = 51;
            this.comTRANGTHAI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.comPHONG.Location = new System.Drawing.Point(523, 100);
            this.comPHONG.Name = "comPHONG";
            this.comPHONG.Size = new System.Drawing.Size(157, 51);
            this.comPHONG.TabIndex = 52;
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
            this.comNHA.Location = new System.Drawing.Point(320, 100);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(157, 51);
            this.comNHA.TabIndex = 53;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.dtpTHOIGIAN.Location = new System.Drawing.Point(959, 105);
            this.dtpTHOIGIAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHOIGIAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHOIGIAN.Name = "dtpTHOIGIAN";
            this.dtpTHOIGIAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHOIGIAN.Size = new System.Drawing.Size(190, 45);
            this.dtpTHOIGIAN.TabIndex = 63;
            this.dtpTHOIGIAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // UC_HOADON_DV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpTHOIGIAN);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnupdate);
            this.Controls.Add(this.btnfillter);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgv_HD_TONGHOP);
            this.Controls.Add(this.comTRANGTHAI);
            this.Controls.Add(this.comPHONG);
            this.Controls.Add(this.comNHA);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_HOADON_DV";
            this.Size = new System.Drawing.Size(1580, 998);
            this.Load += new System.EventHandler(this.UC_HOADON_DV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HD_TONGHOP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btncancel;
        private Guna.UI2.WinForms.Guna2Button btnupdate;
        private Guna.UI2.WinForms.Guna2CircleButton btnfillter;
        private Guna.UI2.WinForms.Guna2CircleButton btnDelete;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_HD_TONGHOP;
        private Guna.UI2.WinForms.Guna2ComboBox comTRANGTHAI;
        private Guna.UI2.WinForms.Guna2ComboBox comPHONG;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_PHONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENHD_TH;
        private System.Windows.Forms.DataGridViewTextBoxColumn THOIGIAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_DIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIENDIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_NUOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIENNUOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_INT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIENINTERNET;
        private System.Windows.Forms.DataGridViewTextBoxColumn TONGTIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TINHTRANGTT;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHOIGIAN;
    }
}
