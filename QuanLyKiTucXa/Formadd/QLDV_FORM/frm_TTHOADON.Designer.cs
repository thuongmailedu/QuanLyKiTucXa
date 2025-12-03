namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    partial class frm_TTHOADON
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_addHD_DIEN = new System.Windows.Forms.DataGridView();
            this.dtpTHOIGIAN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.lblformtype = new System.Windows.Forms.Label();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_DIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHISOCU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHISOMOI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SODIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONGIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TONGTIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THOIGIAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAYGHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TINHTRANGTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMAHD_DIEN = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2TextBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.guna2TextBox4 = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_addHD_DIEN)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_addHD_DIEN
            // 
            this.dgv_addHD_DIEN.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_addHD_DIEN.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_addHD_DIEN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_addHD_DIEN.BackgroundColor = System.Drawing.Color.White;
            this.dgv_addHD_DIEN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_addHD_DIEN.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_addHD_DIEN.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_addHD_DIEN.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_addHD_DIEN.ColumnHeadersHeight = 45;
            this.dgv_addHD_DIEN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MAHD_DIEN,
            this.TENHD,
            this.CHISOCU,
            this.CHISOMOI,
            this.SODIEN,
            this.DONGIA,
            this.TONGTIEN,
            this.THOIGIAN,
            this.NGAYGHI,
            this.TINHTRANGTT});
            this.dgv_addHD_DIEN.EnableHeadersVisualStyles = false;
            this.dgv_addHD_DIEN.Location = new System.Drawing.Point(37, 119);
            this.dgv_addHD_DIEN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_addHD_DIEN.Name = "dgv_addHD_DIEN";
            this.dgv_addHD_DIEN.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_addHD_DIEN.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_addHD_DIEN.RowHeadersWidth = 62;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_addHD_DIEN.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_addHD_DIEN.RowTemplate.Height = 45;
            this.dgv_addHD_DIEN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_addHD_DIEN.Size = new System.Drawing.Size(1077, 233);
            this.dgv_addHD_DIEN.TabIndex = 41;
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
            this.dtpTHOIGIAN.Location = new System.Drawing.Point(900, 53);
            this.dtpTHOIGIAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHOIGIAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHOIGIAN.Name = "dtpTHOIGIAN";
            this.dtpTHOIGIAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHOIGIAN.Size = new System.Drawing.Size(205, 45);
            this.dtpTHOIGIAN.TabIndex = 42;
            this.dtpTHOIGIAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(44, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 20);
            this.label8.TabIndex = 43;
            this.label8.Text = "Nhà";
            // 
            // lblformtype
            // 
            this.lblformtype.AutoSize = true;
            this.lblformtype.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblformtype.ForeColor = System.Drawing.Color.Navy;
            this.lblformtype.Location = new System.Drawing.Point(30, 9);
            this.lblformtype.Name = "lblformtype";
            this.lblformtype.Size = new System.Drawing.Size(390, 38);
            this.lblformtype.TabIndex = 44;
            this.lblformtype.Text = "Cập nhật trạng thái hóa đơn";
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 8;
            this.STT.Name = "STT";
            this.STT.Width = 60;
            // 
            // MAHD_DIEN
            // 
            this.MAHD_DIEN.DataPropertyName = "MAHD_DIEN";
            this.MAHD_DIEN.HeaderText = "Mã hóa đơn";
            this.MAHD_DIEN.MinimumWidth = 8;
            this.MAHD_DIEN.Name = "MAHD_DIEN";
            this.MAHD_DIEN.Width = 107;
            // 
            // TENHD
            // 
            this.TENHD.DataPropertyName = "TENHD";
            this.TENHD.HeaderText = "Tên hóa đơn";
            this.TENHD.MinimumWidth = 8;
            this.TENHD.Name = "TENHD";
            this.TENHD.Width = 108;
            // 
            // CHISOCU
            // 
            this.CHISOCU.DataPropertyName = "CHISOCU";
            this.CHISOCU.HeaderText = "Chỉ số cũ";
            this.CHISOCU.MinimumWidth = 6;
            this.CHISOCU.Name = "CHISOCU";
            this.CHISOCU.Width = 74;
            // 
            // CHISOMOI
            // 
            this.CHISOMOI.DataPropertyName = "CHISOMOI";
            this.CHISOMOI.HeaderText = "Chỉ số mới";
            this.CHISOMOI.MinimumWidth = 6;
            this.CHISOMOI.Name = "CHISOMOI";
            this.CHISOMOI.Width = 98;
            // 
            // SODIEN
            // 
            this.SODIEN.DataPropertyName = "SODIEN";
            this.SODIEN.HeaderText = "Lượng tiêu thụ";
            this.SODIEN.MinimumWidth = 6;
            this.SODIEN.Name = "SODIEN";
            this.SODIEN.Width = 102;
            // 
            // DONGIA
            // 
            this.DONGIA.DataPropertyName = "DONGIA";
            this.DONGIA.HeaderText = "Đơn giá";
            this.DONGIA.MinimumWidth = 8;
            this.DONGIA.Name = "DONGIA";
            this.DONGIA.Width = 82;
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
            // NGAYGHI
            // 
            this.NGAYGHI.DataPropertyName = "NGAYGHI";
            this.NGAYGHI.HeaderText = "Ngày ghi";
            this.NGAYGHI.MinimumWidth = 6;
            this.NGAYGHI.Name = "NGAYGHI";
            this.NGAYGHI.Width = 89;
            // 
            // TINHTRANGTT
            // 
            this.TINHTRANGTT.DataPropertyName = "TINHTRANGTT";
            this.TINHTRANGTT.HeaderText = "Trạng thái";
            this.TINHTRANGTT.MinimumWidth = 6;
            this.TINHTRANGTT.Name = "TINHTRANGTT";
            this.TINHTRANGTT.Width = 94;
            // 
            // txtMAHD_DIEN
            // 
            this.txtMAHD_DIEN.BorderColor = System.Drawing.Color.DarkGray;
            this.txtMAHD_DIEN.BorderRadius = 5;
            this.txtMAHD_DIEN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMAHD_DIEN.DefaultText = "";
            this.txtMAHD_DIEN.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMAHD_DIEN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMAHD_DIEN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMAHD_DIEN.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMAHD_DIEN.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMAHD_DIEN.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMAHD_DIEN.ForeColor = System.Drawing.Color.Black;
            this.txtMAHD_DIEN.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMAHD_DIEN.Location = new System.Drawing.Point(101, 53);
            this.txtMAHD_DIEN.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMAHD_DIEN.Name = "txtMAHD_DIEN";
            this.txtMAHD_DIEN.PlaceholderText = "";
            this.txtMAHD_DIEN.SelectedText = "";
            this.txtMAHD_DIEN.Size = new System.Drawing.Size(187, 45);
            this.txtMAHD_DIEN.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(447, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 43;
            this.label1.Text = "Phòng";
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TextBox1.BorderRadius = 5;
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2TextBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox1.Location = new System.Drawing.Point(504, 53);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PlaceholderText = "";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(187, 45);
            this.guna2TextBox1.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(801, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Thời gian";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "Mã sinh viên thanh toán";
            // 
            // guna2TextBox2
            // 
            this.guna2TextBox2.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TextBox2.BorderRadius = 5;
            this.guna2TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox2.DefaultText = "";
            this.guna2TextBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2TextBox2.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox2.Location = new System.Drawing.Point(233, 396);
            this.guna2TextBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.guna2TextBox2.Name = "guna2TextBox2";
            this.guna2TextBox2.PlaceholderText = "";
            this.guna2TextBox2.SelectedText = "";
            this.guna2TextBox2.Size = new System.Drawing.Size(187, 45);
            this.guna2TextBox2.TabIndex = 45;
            // 
            // guna2DateTimePicker1
            // 
            this.guna2DateTimePicker1.BackColor = System.Drawing.Color.White;
            this.guna2DateTimePicker1.BorderColor = System.Drawing.Color.Silver;
            this.guna2DateTimePicker1.BorderRadius = 5;
            this.guna2DateTimePicker1.BorderThickness = 1;
            this.guna2DateTimePicker1.Checked = true;
            this.guna2DateTimePicker1.CheckedState.FillColor = System.Drawing.Color.White;
            this.guna2DateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.guna2DateTimePicker1.FillColor = System.Drawing.Color.White;
            this.guna2DateTimePicker1.FocusedColor = System.Drawing.Color.White;
            this.guna2DateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.guna2DateTimePicker1.Location = new System.Drawing.Point(767, 396);
            this.guna2DateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            this.guna2DateTimePicker1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.guna2DateTimePicker1.Size = new System.Drawing.Size(205, 45);
            this.guna2DateTimePicker1.TabIndex = 42;
            this.guna2DateTimePicker1.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(561, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Ngày thanh toán";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 478);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 20);
            this.label5.TabIndex = 43;
            this.label5.Text = "Tên sinh viên thanh toán";
            // 
            // guna2TextBox3
            // 
            this.guna2TextBox3.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TextBox3.BorderRadius = 5;
            this.guna2TextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox3.DefaultText = "";
            this.guna2TextBox3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2TextBox3.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox3.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox3.Location = new System.Drawing.Point(233, 466);
            this.guna2TextBox3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.guna2TextBox3.Name = "guna2TextBox3";
            this.guna2TextBox3.PlaceholderText = "";
            this.guna2TextBox3.SelectedText = "";
            this.guna2TextBox3.Size = new System.Drawing.Size(187, 45);
            this.guna2TextBox3.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(561, 478);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 25);
            this.label6.TabIndex = 43;
            this.label6.Text = "Người nhận";
            // 
            // guna2TextBox4
            // 
            this.guna2TextBox4.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2TextBox4.BorderRadius = 5;
            this.guna2TextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox4.DefaultText = "";
            this.guna2TextBox4.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox4.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox4.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2TextBox4.ForeColor = System.Drawing.Color.Black;
            this.guna2TextBox4.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox4.Location = new System.Drawing.Point(767, 466);
            this.guna2TextBox4.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.guna2TextBox4.Name = "guna2TextBox4";
            this.guna2TextBox4.PlaceholderText = "";
            this.guna2TextBox4.SelectedText = "";
            this.guna2TextBox4.Size = new System.Drawing.Size(205, 45);
            this.guna2TextBox4.TabIndex = 45;
            // 
            // frm_TTHOADON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 536);
            this.Controls.Add(this.guna2TextBox1);
            this.Controls.Add(this.guna2TextBox4);
            this.Controls.Add(this.guna2TextBox3);
            this.Controls.Add(this.guna2TextBox2);
            this.Controls.Add(this.txtMAHD_DIEN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblformtype);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.guna2DateTimePicker1);
            this.Controls.Add(this.dtpTHOIGIAN);
            this.Controls.Add(this.dgv_addHD_DIEN);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_TTHOADON";
            this.Text = "frm_TTHOADON";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_addHD_DIEN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_addHD_DIEN;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHOIGIAN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblformtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_DIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHISOCU;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHISOMOI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SODIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONGIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TONGTIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn THOIGIAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAYGHI;
        private System.Windows.Forms.DataGridViewTextBoxColumn TINHTRANGTT;
        private Guna.UI2.WinForms.Guna2TextBox txtMAHD_DIEN;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox3;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox4;
    }
}