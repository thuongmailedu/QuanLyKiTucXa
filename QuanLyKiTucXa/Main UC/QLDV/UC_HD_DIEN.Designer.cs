namespace QuanLyKiTucXa.Main_UC.QLDV
{
    partial class UC_HD_DIEN
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
            this.dtpTHOIGIAN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_HD_DIEN = new System.Windows.Forms.DataGridView();
            this.comPHONG = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comNHA = new Guna.UI2.WinForms.Guna2ComboBox();
            this.comTRANGTHAI = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnfillter = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnDelete = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnadd = new Guna.UI2.WinForms.Guna2CircleButton();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MADV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_DIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MANHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MA_PHONG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHISOCU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHISOMOI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SODIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONGIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TONGTIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THOIGIAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAYGHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TINHTRANGTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HD_DIEN)).BeginInit();
            this.SuspendLayout();
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
            this.dtpTHOIGIAN.Location = new System.Drawing.Point(1152, 98);
            this.dtpTHOIGIAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHOIGIAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHOIGIAN.Name = "dtpTHOIGIAN";
            this.dtpTHOIGIAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHOIGIAN.Size = new System.Drawing.Size(190, 45);
            this.dtpTHOIGIAN.TabIndex = 38;
            this.dtpTHOIGIAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(615, 26);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(318, 41);
            this.label8.TabIndex = 37;
            this.label8.Text = "HÓA ĐƠN TIỀN ĐIỆN";
            // 
            // dgv_HD_DIEN
            // 
            this.dgv_HD_DIEN.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_HD_DIEN.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_HD_DIEN.BackgroundColor = System.Drawing.Color.White;
            this.dgv_HD_DIEN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_HD_DIEN.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HD_DIEN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_HD_DIEN.ColumnHeadersHeight = 45;
            this.dgv_HD_DIEN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MADV,
            this.MAHD_DIEN,
            this.TENHD,
            this.MANHA,
            this.MA_PHONG,
            this.CHISOCU,
            this.CHISOMOI,
            this.SODIEN,
            this.DONGIA,
            this.TONGTIEN,
            this.THOIGIAN,
            this.NGAYGHI,
            this.TINHTRANGTT});
            this.dgv_HD_DIEN.EnableHeadersVisualStyles = false;
            this.dgv_HD_DIEN.Location = new System.Drawing.Point(54, 159);
            this.dgv_HD_DIEN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_HD_DIEN.Name = "dgv_HD_DIEN";
            this.dgv_HD_DIEN.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_HD_DIEN.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_HD_DIEN.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_HD_DIEN.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_HD_DIEN.RowTemplate.Height = 45;
            this.dgv_HD_DIEN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_HD_DIEN.Size = new System.Drawing.Size(1490, 602);
            this.dgv_HD_DIEN.TabIndex = 36;
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
            this.comPHONG.Location = new System.Drawing.Point(552, 94);
            this.comPHONG.Name = "comPHONG";
            this.comPHONG.Size = new System.Drawing.Size(157, 51);
            this.comPHONG.TabIndex = 30;
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
            this.comNHA.Location = new System.Drawing.Point(284, 92);
            this.comNHA.Name = "comNHA";
            this.comNHA.Size = new System.Drawing.Size(157, 51);
            this.comNHA.TabIndex = 31;
            this.comNHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.comTRANGTHAI.Location = new System.Drawing.Point(853, 94);
            this.comTRANGTHAI.Name = "comTRANGTHAI";
            this.comTRANGTHAI.Size = new System.Drawing.Size(157, 51);
            this.comTRANGTHAI.TabIndex = 30;
            this.comTRANGTHAI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(226, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 28);
            this.label2.TabIndex = 67;
            this.label2.Text = "Nhà";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(473, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 28);
            this.label1.TabIndex = 67;
            this.label1.Text = "Phòng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(738, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 28);
            this.label3.TabIndex = 67;
            this.label3.Text = "Trạng thái";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(1043, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 28);
            this.label4.TabIndex = 67;
            this.label4.Text = "Thời gian";
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
            this.btnfillter.Location = new System.Drawing.Point(1376, 99);
            this.btnfillter.Name = "btnfillter";
            this.btnfillter.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnfillter.Size = new System.Drawing.Size(35, 40);
            this.btnfillter.TabIndex = 35;
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
            this.btnDelete.Location = new System.Drawing.Point(1428, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnDelete.Size = new System.Drawing.Size(35, 32);
            this.btnDelete.TabIndex = 34;
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
            this.btnedit.Location = new System.Drawing.Point(1472, 103);
            this.btnedit.Name = "btnedit";
            this.btnedit.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit.Size = new System.Drawing.Size(31, 32);
            this.btnedit.TabIndex = 33;
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
            this.btnadd.Location = new System.Drawing.Point(1514, 103);
            this.btnadd.Name = "btnadd";
            this.btnadd.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd.Size = new System.Drawing.Size(30, 32);
            this.btnadd.TabIndex = 32;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 8;
            this.STT.Name = "STT";
            this.STT.Width = 50;
            // 
            // MADV
            // 
            this.MADV.DataPropertyName = "MADV";
            this.MADV.HeaderText = "Mã dịch vụ";
            this.MADV.MinimumWidth = 6;
            this.MADV.Name = "MADV";
            this.MADV.Width = 102;
            // 
            // MAHD_DIEN
            // 
            this.MAHD_DIEN.DataPropertyName = "MAHD_DIEN";
            this.MAHD_DIEN.HeaderText = "Mã hóa đơn";
            this.MAHD_DIEN.MinimumWidth = 8;
            this.MAHD_DIEN.Name = "MAHD_DIEN";
            this.MAHD_DIEN.Width = 150;
            // 
            // TENHD
            // 
            this.TENHD.DataPropertyName = "TENHD";
            this.TENHD.HeaderText = "Tên hóa đơn";
            this.TENHD.MinimumWidth = 8;
            this.TENHD.Name = "TENHD";
            this.TENHD.Width = 150;
            // 
            // MANHA
            // 
            this.MANHA.DataPropertyName = "MANHA";
            this.MANHA.HeaderText = "Mã nhà";
            this.MANHA.MinimumWidth = 6;
            this.MANHA.Name = "MANHA";
            this.MANHA.Width = 80;
            // 
            // MA_PHONG
            // 
            this.MA_PHONG.DataPropertyName = "MA_PHONG";
            this.MA_PHONG.HeaderText = "Mã phòng";
            this.MA_PHONG.MinimumWidth = 6;
            this.MA_PHONG.Name = "MA_PHONG";
            this.MA_PHONG.Width = 110;
            // 
            // CHISOCU
            // 
            this.CHISOCU.DataPropertyName = "CHISOCU";
            this.CHISOCU.HeaderText = "Chỉ số cũ";
            this.CHISOCU.MinimumWidth = 6;
            this.CHISOCU.Name = "CHISOCU";
            this.CHISOCU.Width = 102;
            // 
            // CHISOMOI
            // 
            this.CHISOMOI.DataPropertyName = "CHISOMOI";
            this.CHISOMOI.HeaderText = "Chỉ số mới";
            this.CHISOMOI.MinimumWidth = 6;
            this.CHISOMOI.Name = "CHISOMOI";
            this.CHISOMOI.Width = 102;
            // 
            // SODIEN
            // 
            this.SODIEN.DataPropertyName = "SODIEN";
            this.SODIEN.HeaderText = "Số điện tiêu thụ";
            this.SODIEN.MinimumWidth = 6;
            this.SODIEN.Name = "SODIEN";
            this.SODIEN.Width = 130;
            // 
            // DONGIA
            // 
            this.DONGIA.DataPropertyName = "DONGIA";
            this.DONGIA.HeaderText = "Đơn giá";
            this.DONGIA.MinimumWidth = 8;
            this.DONGIA.Name = "DONGIA";
            this.DONGIA.Width = 102;
            // 
            // TONGTIEN
            // 
            this.TONGTIEN.DataPropertyName = "TONGTIEN";
            this.TONGTIEN.HeaderText = "Tổng tiền";
            this.TONGTIEN.MinimumWidth = 6;
            this.TONGTIEN.Name = "TONGTIEN";
            this.TONGTIEN.Width = 101;
            // 
            // THOIGIAN
            // 
            this.THOIGIAN.DataPropertyName = "THOIGIAN";
            this.THOIGIAN.HeaderText = "Thời gian";
            this.THOIGIAN.MinimumWidth = 8;
            this.THOIGIAN.Name = "THOIGIAN";
            this.THOIGIAN.Width = 102;
            // 
            // NGAYGHI
            // 
            this.NGAYGHI.DataPropertyName = "NGAYGHI";
            this.NGAYGHI.HeaderText = "Ngày ghi";
            this.NGAYGHI.MinimumWidth = 6;
            this.NGAYGHI.Name = "NGAYGHI";
            this.NGAYGHI.Width = 102;
            // 
            // TINHTRANGTT
            // 
            this.TINHTRANGTT.DataPropertyName = "TINHTRANGTT";
            this.TINHTRANGTT.HeaderText = "Trạng thái";
            this.TINHTRANGTT.MinimumWidth = 6;
            this.TINHTRANGTT.Name = "TINHTRANGTT";
            this.TINHTRANGTT.Width = 150;
            // 
            // UC_HD_DIEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpTHOIGIAN);
            this.Controls.Add(this.btnfillter);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnedit);
            this.Controls.Add(this.btnadd);
            this.Controls.Add(this.dgv_HD_DIEN);
            this.Controls.Add(this.comTRANGTHAI);
            this.Controls.Add(this.comPHONG);
            this.Controls.Add(this.comNHA);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_HD_DIEN";
            this.Size = new System.Drawing.Size(1602, 1024);
            this.Load += new System.EventHandler(this.UC_HD_DIEN_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_HD_DIEN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHOIGIAN;
        private Guna.UI2.WinForms.Guna2CircleButton btnfillter;
        private Guna.UI2.WinForms.Guna2CircleButton btnDelete;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd;
        private System.Windows.Forms.DataGridView dgv_HD_DIEN;
        private Guna.UI2.WinForms.Guna2ComboBox comPHONG;
        private Guna.UI2.WinForms.Guna2ComboBox comNHA;
        private Guna.UI2.WinForms.Guna2ComboBox comTRANGTHAI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MADV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_DIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn MANHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn MA_PHONG;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHISOCU;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHISOMOI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SODIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONGIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TONGTIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn THOIGIAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAYGHI;
        private System.Windows.Forms.DataGridViewTextBoxColumn TINHTRANGTT;
    }
}
