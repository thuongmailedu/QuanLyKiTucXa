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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnUpdate = new Guna.UI2.WinForms.Guna2Button();
            this.btnfillter = new Guna.UI2.WinForms.Guna2CircleButton();
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
            this.btnXuatHD = new Guna.UI2.WinForms.Guna2CircleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExport = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HD_TONGHOP)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 5;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Image = global::QuanLyKiTucXa.Properties.Resources.Button___Hủy_xác_nhận2;
            this.btnCancel.ImageSize = new System.Drawing.Size(40, 40);
            this.btnCancel.Location = new System.Drawing.Point(1292, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(46, 40);
            this.btnCancel.TabIndex = 62;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BorderRadius = 5;
            this.btnUpdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUpdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUpdate.FillColor = System.Drawing.Color.White;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = global::QuanLyKiTucXa.Properties.Resources.circle_ok;
            this.btnUpdate.ImageSize = new System.Drawing.Size(25, 25);
            this.btnUpdate.Location = new System.Drawing.Point(1355, 80);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(41, 40);
            this.btnUpdate.TabIndex = 61;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
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
            this.btnfillter.Location = new System.Drawing.Point(1232, 80);
            this.btnfillter.Name = "btnfillter";
            this.btnfillter.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnfillter.Size = new System.Drawing.Size(35, 40);
            this.btnfillter.TabIndex = 57;
            this.btnfillter.Click += new System.EventHandler(this.btnfillter_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(586, 10);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(328, 41);
            this.label8.TabIndex = 59;
            this.label8.Text = "HÓA ĐƠN TỔNG HỢP";
            // 
            // dgv_HD_TONGHOP
            // 
            this.dgv_HD_TONGHOP.AllowUserToResizeRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_HD_TONGHOP.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgv_HD_TONGHOP.BackgroundColor = System.Drawing.Color.White;
            this.dgv_HD_TONGHOP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_HD_TONGHOP.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HD_TONGHOP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
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
            this.dgv_HD_TONGHOP.Location = new System.Drawing.Point(33, 128);
            this.dgv_HD_TONGHOP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_HD_TONGHOP.Name = "dgv_HD_TONGHOP";
            this.dgv_HD_TONGHOP.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HD_TONGHOP.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.dgv_HD_TONGHOP.RowHeadersWidth = 62;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_HD_TONGHOP.RowsDefaultCellStyle = dataGridViewCellStyle27;
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
            this.MANHA.Width = 110;
            // 
            // MA_PHONG
            // 
            this.MA_PHONG.DataPropertyName = "MA_PHONG";
            this.MA_PHONG.HeaderText = "Mã phòng";
            this.MA_PHONG.MinimumWidth = 6;
            this.MA_PHONG.Name = "MA_PHONG";
            this.MA_PHONG.Width = 120;
            // 
            // TENHD_TH
            // 
            this.TENHD_TH.DataPropertyName = "TENHD_TH";
            this.TENHD_TH.HeaderText = "Tên hóa đơn";
            this.TENHD_TH.MinimumWidth = 8;
            this.TENHD_TH.Name = "TENHD_TH";
            this.TENHD_TH.Width = 140;
            // 
            // THOIGIAN
            // 
            this.THOIGIAN.DataPropertyName = "THOIGIAN";
            dataGridViewCellStyle21.Format = "MM/yyyy";
            this.THOIGIAN.DefaultCellStyle = dataGridViewCellStyle21;
            this.THOIGIAN.HeaderText = "Thời gian";
            this.THOIGIAN.MinimumWidth = 8;
            this.THOIGIAN.Name = "THOIGIAN";
            this.THOIGIAN.Width = 113;
            // 
            // MAHD_DIEN
            // 
            this.MAHD_DIEN.DataPropertyName = "MAHD_DIEN";
            this.MAHD_DIEN.HeaderText = "Mã HD Điện";
            this.MAHD_DIEN.MinimumWidth = 8;
            this.MAHD_DIEN.Name = "MAHD_DIEN";
            this.MAHD_DIEN.Width = 150;
            // 
            // TIENDIEN
            // 
            this.TIENDIEN.DataPropertyName = "TIENDIEN";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TIENDIEN.DefaultCellStyle = dataGridViewCellStyle22;
            this.TIENDIEN.HeaderText = "Tiền điện";
            this.TIENDIEN.MinimumWidth = 6;
            this.TIENDIEN.Name = "TIENDIEN";
            this.TIENDIEN.Width = 140;
            // 
            // MAHD_NUOC
            // 
            this.MAHD_NUOC.DataPropertyName = "MAHD_NUOC";
            this.MAHD_NUOC.HeaderText = "Mã HD Nước";
            this.MAHD_NUOC.MinimumWidth = 6;
            this.MAHD_NUOC.Name = "MAHD_NUOC";
            this.MAHD_NUOC.Width = 150;
            // 
            // TIENNUOC
            // 
            this.TIENNUOC.DataPropertyName = "TIENNUOC";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TIENNUOC.DefaultCellStyle = dataGridViewCellStyle23;
            this.TIENNUOC.HeaderText = "Tiền nước";
            this.TIENNUOC.MinimumWidth = 6;
            this.TIENNUOC.Name = "TIENNUOC";
            this.TIENNUOC.Width = 140;
            // 
            // MAHD_INT
            // 
            this.MAHD_INT.DataPropertyName = "MAHD_INT";
            this.MAHD_INT.HeaderText = "Mã HD Internet";
            this.MAHD_INT.MinimumWidth = 6;
            this.MAHD_INT.Name = "MAHD_INT";
            this.MAHD_INT.Width = 150;
            // 
            // TIENINTERNET
            // 
            this.TIENINTERNET.DataPropertyName = "TIENINTERNET";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TIENINTERNET.DefaultCellStyle = dataGridViewCellStyle24;
            this.TIENINTERNET.HeaderText = "Tiền internet";
            this.TIENINTERNET.MinimumWidth = 6;
            this.TIENINTERNET.Name = "TIENINTERNET";
            this.TIENINTERNET.Width = 140;
            // 
            // TONGTIEN
            // 
            this.TONGTIEN.DataPropertyName = "TONGTIEN";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TONGTIEN.DefaultCellStyle = dataGridViewCellStyle25;
            this.TONGTIEN.HeaderText = "Tổng tiền";
            this.TONGTIEN.MinimumWidth = 6;
            this.TONGTIEN.Name = "TONGTIEN";
            this.TONGTIEN.Width = 140;
            // 
            // TINHTRANGTT
            // 
            this.TINHTRANGTT.DataPropertyName = "TINHTRANGTT";
            this.TINHTRANGTT.HeaderText = "Trạng thái";
            this.TINHTRANGTT.MinimumWidth = 6;
            this.TINHTRANGTT.Name = "TINHTRANGTT";
            this.TINHTRANGTT.Width = 140;
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
            this.comTRANGTHAI.Location = new System.Drawing.Point(723, 69);
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
            this.comPHONG.Location = new System.Drawing.Point(384, 69);
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
            this.comNHA.Location = new System.Drawing.Point(107, 69);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(157, 51);
            this.comNHA.TabIndex = 53;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpTHOIGIAN
            // 
            this.dtpTHOIGIAN.BackColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.BorderColor = System.Drawing.Color.Silver;
            this.dtpTHOIGIAN.BorderRadius = 5;
            this.dtpTHOIGIAN.BorderThickness = 1;
            this.dtpTHOIGIAN.Checked = true;
            this.dtpTHOIGIAN.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.CustomFormat = "MM/yyyy";
            this.dtpTHOIGIAN.FillColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.FocusedColor = System.Drawing.Color.White;
            this.dtpTHOIGIAN.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.dtpTHOIGIAN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTHOIGIAN.Location = new System.Drawing.Point(1027, 75);
            this.dtpTHOIGIAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHOIGIAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHOIGIAN.Name = "dtpTHOIGIAN";
            this.dtpTHOIGIAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHOIGIAN.Size = new System.Drawing.Size(190, 45);
            this.dtpTHOIGIAN.TabIndex = 63;
            this.dtpTHOIGIAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // btnXuatHD
            // 
            this.btnXuatHD.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnXuatHD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnXuatHD.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatHD.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatHD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuatHD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuatHD.FillColor = System.Drawing.Color.Transparent;
            this.btnXuatHD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXuatHD.ForeColor = System.Drawing.Color.White;
            this.btnXuatHD.Location = new System.Drawing.Point(1414, 86);
            this.btnXuatHD.Name = "btnXuatHD";
            this.btnXuatHD.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnXuatHD.Size = new System.Drawing.Size(31, 34);
            this.btnXuatHD.TabIndex = 64;
            this.btnXuatHD.Click += new System.EventHandler(this.btnXuatHD_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(30, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 28);
            this.label1.TabIndex = 65;
            this.label1.Text = "Nhà";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(294, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 28);
            this.label2.TabIndex = 65;
            this.label2.Text = "Phòng";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(594, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 28);
            this.label3.TabIndex = 65;
            this.label3.Text = "Trạng thái";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(909, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 28);
            this.label4.TabIndex = 65;
            this.label4.Text = "Thời gian";
            // 
            // btnExport
            // 
            this.btnExport.BorderRadius = 5;
            this.btnExport.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExport.FillColor = System.Drawing.Color.Navy;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(1458, 80);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(65, 45);
            this.btnExport.TabIndex = 68;
            this.btnExport.Text = "Ex";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // UC_HOADON_DV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnXuatHD);
            this.Controls.Add(this.dtpTHOIGIAN);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnfillter);
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

        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2CircleButton btnfillter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgv_HD_TONGHOP;
        private Guna.UI2.WinForms.Guna2ComboBox comTRANGTHAI;
        private Guna.UI2.WinForms.Guna2ComboBox comPHONG;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHOIGIAN;
        private Guna.UI2.WinForms.Guna2CircleButton btnXuatHD;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridViewTextBoxColumn STT;
        private DataGridViewTextBoxColumn MANHA;
        private DataGridViewTextBoxColumn MA_PHONG;
        private DataGridViewTextBoxColumn TENHD_TH;
        private DataGridViewTextBoxColumn THOIGIAN;
        private DataGridViewTextBoxColumn MAHD_DIEN;
        private DataGridViewTextBoxColumn TIENDIEN;
        private DataGridViewTextBoxColumn MAHD_NUOC;
        private DataGridViewTextBoxColumn TIENNUOC;
        private DataGridViewTextBoxColumn MAHD_INT;
        private DataGridViewTextBoxColumn TIENINTERNET;
        private DataGridViewTextBoxColumn TONGTIEN;
        private DataGridViewTextBoxColumn TINHTRANGTT;
        private Guna.UI2.WinForms.Guna2Button btnExport;
    }
}
