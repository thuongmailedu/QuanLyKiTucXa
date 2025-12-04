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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_CHITIET_HD = new System.Windows.Forms.DataGridView();
            this.dtpTHOIGIAN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.lblformtype = new System.Windows.Forms.Label();
            this.txtMANHA = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMA_PHONG = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMASV = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNGAYTHANHTOAN = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTENSV = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMANV = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAHD_DIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHISOCU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHISOMOI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SODIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONGIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DONVI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TONGTIEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THOIGIAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGAYGHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTENNV = new Guna.UI2.WinForms.Guna2TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTONGTHANHTOAN = new Guna.UI2.WinForms.Guna2TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comHINHTHUC_TT = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CHITIET_HD)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_CHITIET_HD
            // 
            this.dgv_CHITIET_HD.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_CHITIET_HD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_CHITIET_HD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_CHITIET_HD.BackgroundColor = System.Drawing.Color.White;
            this.dgv_CHITIET_HD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_CHITIET_HD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_CHITIET_HD.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CHITIET_HD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_CHITIET_HD.ColumnHeadersHeight = 45;
            this.dgv_CHITIET_HD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MAHD_DIEN,
            this.TENHD,
            this.CHISOCU,
            this.CHISOMOI,
            this.SODIEN,
            this.DONGIA,
            this.DONVI,
            this.TONGTIEN,
            this.THOIGIAN,
            this.NGAYGHI});
            this.dgv_CHITIET_HD.EnableHeadersVisualStyles = false;
            this.dgv_CHITIET_HD.Location = new System.Drawing.Point(37, 137);
            this.dgv_CHITIET_HD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_CHITIET_HD.Name = "dgv_CHITIET_HD";
            this.dgv_CHITIET_HD.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CHITIET_HD.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_CHITIET_HD.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_CHITIET_HD.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_CHITIET_HD.RowTemplate.Height = 45;
            this.dgv_CHITIET_HD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_CHITIET_HD.Size = new System.Drawing.Size(1098, 233);
            this.dgv_CHITIET_HD.TabIndex = 41;
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
            this.dtpTHOIGIAN.Location = new System.Drawing.Point(880, 64);
            this.dtpTHOIGIAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTHOIGIAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTHOIGIAN.Name = "dtpTHOIGIAN";
            this.dtpTHOIGIAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpTHOIGIAN.Size = new System.Drawing.Size(255, 45);
            this.dtpTHOIGIAN.TabIndex = 42;
            this.dtpTHOIGIAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(44, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 20);
            this.label8.TabIndex = 43;
            this.label8.Text = "Nhà";
            // 
            // lblformtype
            // 
            this.lblformtype.AutoSize = true;
            this.lblformtype.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblformtype.ForeColor = System.Drawing.Color.Navy;
            this.lblformtype.Location = new System.Drawing.Point(31, 9);
            this.lblformtype.Name = "lblformtype";
            this.lblformtype.Size = new System.Drawing.Size(316, 31);
            this.lblformtype.TabIndex = 44;
            this.lblformtype.Text = "Cập nhật trạng thái hóa đơn";
            // 
            // txtMANHA
            // 
            this.txtMANHA.BorderColor = System.Drawing.Color.DarkGray;
            this.txtMANHA.BorderRadius = 5;
            this.txtMANHA.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMANHA.DefaultText = "";
            this.txtMANHA.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMANHA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMANHA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMANHA.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMANHA.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMANHA.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMANHA.ForeColor = System.Drawing.Color.Black;
            this.txtMANHA.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMANHA.Location = new System.Drawing.Point(101, 64);
            this.txtMANHA.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMANHA.Name = "txtMANHA";
            this.txtMANHA.PlaceholderText = "";
            this.txtMANHA.SelectedText = "";
            this.txtMANHA.Size = new System.Drawing.Size(256, 45);
            this.txtMANHA.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(447, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "Phòng";
            // 
            // txtMA_PHONG
            // 
            this.txtMA_PHONG.BorderColor = System.Drawing.Color.DarkGray;
            this.txtMA_PHONG.BorderRadius = 5;
            this.txtMA_PHONG.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMA_PHONG.DefaultText = "";
            this.txtMA_PHONG.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMA_PHONG.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMA_PHONG.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMA_PHONG.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMA_PHONG.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMA_PHONG.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMA_PHONG.ForeColor = System.Drawing.Color.Black;
            this.txtMA_PHONG.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMA_PHONG.Location = new System.Drawing.Point(504, 64);
            this.txtMA_PHONG.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMA_PHONG.Name = "txtMA_PHONG";
            this.txtMA_PHONG.PlaceholderText = "";
            this.txtMA_PHONG.SelectedText = "";
            this.txtMA_PHONG.Size = new System.Drawing.Size(252, 45);
            this.txtMA_PHONG.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(781, 75);
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
            // txtMASV
            // 
            this.txtMASV.BorderColor = System.Drawing.Color.DarkGray;
            this.txtMASV.BorderRadius = 5;
            this.txtMASV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMASV.DefaultText = "";
            this.txtMASV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMASV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMASV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMASV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMASV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMASV.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMASV.ForeColor = System.Drawing.Color.Black;
            this.txtMASV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMASV.Location = new System.Drawing.Point(233, 396);
            this.txtMASV.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMASV.Name = "txtMASV";
            this.txtMASV.PlaceholderText = "";
            this.txtMASV.SelectedText = "";
            this.txtMASV.Size = new System.Drawing.Size(268, 45);
            this.txtMASV.TabIndex = 45;
            // 
            // dtpNGAYTHANHTOAN
            // 
            this.dtpNGAYTHANHTOAN.BackColor = System.Drawing.Color.White;
            this.dtpNGAYTHANHTOAN.BorderColor = System.Drawing.Color.Silver;
            this.dtpNGAYTHANHTOAN.BorderRadius = 5;
            this.dtpNGAYTHANHTOAN.BorderThickness = 1;
            this.dtpNGAYTHANHTOAN.Checked = true;
            this.dtpNGAYTHANHTOAN.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpNGAYTHANHTOAN.CustomFormat = "dd/MM/yyyy";
            this.dtpNGAYTHANHTOAN.FillColor = System.Drawing.Color.White;
            this.dtpNGAYTHANHTOAN.FocusedColor = System.Drawing.Color.White;
            this.dtpNGAYTHANHTOAN.Font = new System.Drawing.Font("Segoe UI", 9.857143F);
            this.dtpNGAYTHANHTOAN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNGAYTHANHTOAN.Location = new System.Drawing.Point(867, 551);
            this.dtpNGAYTHANHTOAN.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNGAYTHANHTOAN.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNGAYTHANHTOAN.Name = "dtpNGAYTHANHTOAN";
            this.dtpNGAYTHANHTOAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpNGAYTHANHTOAN.Size = new System.Drawing.Size(268, 45);
            this.dtpNGAYTHANHTOAN.TabIndex = 42;
            this.dtpNGAYTHANHTOAN.Value = new System.DateTime(2025, 10, 25, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(661, 562);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Ngày thanh toán";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(664, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 20);
            this.label5.TabIndex = 43;
            this.label5.Text = "Tên sinh viên thanh toán";
            // 
            // txtTENSV
            // 
            this.txtTENSV.BorderColor = System.Drawing.Color.DarkGray;
            this.txtTENSV.BorderRadius = 5;
            this.txtTENSV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTENSV.DefaultText = "";
            this.txtTENSV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTENSV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTENSV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTENSV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTENSV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTENSV.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTENSV.ForeColor = System.Drawing.Color.Black;
            this.txtTENSV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTENSV.Location = new System.Drawing.Point(867, 396);
            this.txtTENSV.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtTENSV.Name = "txtTENSV";
            this.txtTENSV.PlaceholderText = "";
            this.txtTENSV.SelectedText = "";
            this.txtTENSV.Size = new System.Drawing.Size(268, 45);
            this.txtTENSV.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 483);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 20);
            this.label6.TabIndex = 43;
            this.label6.Text = "Mã người nhận";
            // 
            // txtMANV
            // 
            this.txtMANV.BorderColor = System.Drawing.Color.DarkGray;
            this.txtMANV.BorderRadius = 5;
            this.txtMANV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMANV.DefaultText = "";
            this.txtMANV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMANV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMANV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMANV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMANV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMANV.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMANV.ForeColor = System.Drawing.Color.Black;
            this.txtMANV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMANV.Location = new System.Drawing.Point(233, 473);
            this.txtMANV.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMANV.Name = "txtMANV";
            this.txtMANV.PlaceholderText = "";
            this.txtMANV.SelectedText = "";
            this.txtMANV.Size = new System.Drawing.Size(268, 45);
            this.txtMANV.TabIndex = 45;
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 3;
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.FillColor = System.Drawing.Color.Navy;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(1031, 680);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(104, 45);
            this.btnLuu.TabIndex = 46;
            this.btnLuu.Text = "Lưu";
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 3;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.Navy;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(904, 680);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(104, 45);
            this.btnHuy.TabIndex = 47;
            this.btnHuy.Text = "Hủy";
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
            // DONVI
            // 
            this.DONVI.DataPropertyName = "DONVI";
            this.DONVI.HeaderText = "Đơn vị";
            this.DONVI.MinimumWidth = 6;
            this.DONVI.Name = "DONVI";
            this.DONVI.Width = 73;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(664, 485);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 20);
            this.label7.TabIndex = 43;
            this.label7.Text = "Tên người nhận";
            // 
            // txtTENNV
            // 
            this.txtTENNV.BorderColor = System.Drawing.Color.DarkGray;
            this.txtTENNV.BorderRadius = 5;
            this.txtTENNV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTENNV.DefaultText = "";
            this.txtTENNV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTENNV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTENNV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTENNV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTENNV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTENNV.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTENNV.ForeColor = System.Drawing.Color.Black;
            this.txtTENNV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTENNV.Location = new System.Drawing.Point(867, 473);
            this.txtTENNV.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtTENNV.Name = "txtTENNV";
            this.txtTENNV.PlaceholderText = "";
            this.txtTENNV.SelectedText = "";
            this.txtTENNV.Size = new System.Drawing.Size(268, 45);
            this.txtTENNV.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 562);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 20);
            this.label9.TabIndex = 43;
            this.label9.Text = "Tổng thanh toán";
            // 
            // txtTONGTHANHTOAN
            // 
            this.txtTONGTHANHTOAN.BorderColor = System.Drawing.Color.DarkGray;
            this.txtTONGTHANHTOAN.BorderRadius = 5;
            this.txtTONGTHANHTOAN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTONGTHANHTOAN.DefaultText = "";
            this.txtTONGTHANHTOAN.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTONGTHANHTOAN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTONGTHANHTOAN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTONGTHANHTOAN.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTONGTHANHTOAN.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTONGTHANHTOAN.Font = new System.Drawing.Font("Segoe UI", 9.857143F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTONGTHANHTOAN.ForeColor = System.Drawing.Color.Black;
            this.txtTONGTHANHTOAN.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTONGTHANHTOAN.Location = new System.Drawing.Point(233, 552);
            this.txtTONGTHANHTOAN.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtTONGTHANHTOAN.Name = "txtTONGTHANHTOAN";
            this.txtTONGTHANHTOAN.PlaceholderText = "";
            this.txtTONGTHANHTOAN.SelectedText = "";
            this.txtTONGTHANHTOAN.Size = new System.Drawing.Size(268, 45);
            this.txtTONGTHANHTOAN.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(33, 638);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 20);
            this.label10.TabIndex = 43;
            this.label10.Text = "Hình thức thanh toán";
            // 
            // comHINHTHUC_TT
            // 
            this.comHINHTHUC_TT.BackColor = System.Drawing.Color.Transparent;
            this.comHINHTHUC_TT.BorderColor = System.Drawing.Color.Gray;
            this.comHINHTHUC_TT.BorderRadius = 12;
            this.comHINHTHUC_TT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comHINHTHUC_TT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comHINHTHUC_TT.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comHINHTHUC_TT.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comHINHTHUC_TT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comHINHTHUC_TT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.comHINHTHUC_TT.ItemHeight = 45;
            this.comHINHTHUC_TT.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản ngân hàng"});
            this.comHINHTHUC_TT.Location = new System.Drawing.Point(233, 627);
            this.comHINHTHUC_TT.Name = "comHINHTHUC_TT";
            this.comHINHTHUC_TT.Size = new System.Drawing.Size(268, 51);
            this.comHINHTHUC_TT.TabIndex = 54;
            this.comHINHTHUC_TT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frm_TTHOADON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 769);
            this.Controls.Add(this.comHINHTHUC_TT);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtMA_PHONG);
            this.Controls.Add(this.txtTONGTHANHTOAN);
            this.Controls.Add(this.txtMANV);
            this.Controls.Add(this.txtTENNV);
            this.Controls.Add(this.txtTENSV);
            this.Controls.Add(this.txtMASV);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMANHA);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblformtype);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpNGAYTHANHTOAN);
            this.Controls.Add(this.dtpTHOIGIAN);
            this.Controls.Add(this.dgv_CHITIET_HD);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_TTHOADON";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CHITIET_HD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_CHITIET_HD;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTHOIGIAN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblformtype;
        private Guna.UI2.WinForms.Guna2TextBox txtMANHA;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtMA_PHONG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtMASV;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNGAYTHANHTOAN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox txtTENSV;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox txtMANV;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAHD_DIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHISOCU;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHISOMOI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SODIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONGIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DONVI;
        private System.Windows.Forms.DataGridViewTextBoxColumn TONGTIEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn THOIGIAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGAYGHI;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox txtTENNV;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2TextBox txtTONGTHANHTOAN;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2ComboBox comHINHTHUC_TT;
    }
}