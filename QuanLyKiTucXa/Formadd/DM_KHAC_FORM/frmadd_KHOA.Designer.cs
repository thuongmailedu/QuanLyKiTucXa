namespace QuanLyKiTucXa.Formadd.DM_KHAC_FORM
{
    partial class frmadd_KHOA
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
            this.panelMenu = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTENNV = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMANV = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvDMPhong = new System.Windows.Forms.DataGridView();
            this.btnadd_KHOA = new Guna.UI2.WinForms.Guna2CircleButton();
            this.MAKHOA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENKHOA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btndelete_KHOA = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnedit_KHOA = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.BorderColor = System.Drawing.Color.Black;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(76)))), ((int)(((byte)(170)))));
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(0);
            this.panelMenu.MaximumSize = new System.Drawing.Size(1646, 100);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(911, 21);
            this.panelMenu.TabIndex = 61;
            // 
            // btnLuu
            // 
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(766, 502);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 45);
            this.btnLuu.TabIndex = 72;
            this.btnLuu.Text = "Lưu";
            // 
            // btnHuy
            // 
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(652, 502);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(89, 45);
            this.btnHuy.TabIndex = 73;
            this.btnHuy.Text = "Hủy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(63, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 70;
            this.label4.Text = "Mã khoa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(501, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 71;
            this.label2.Text = "Tên khoa";
            // 
            // txtTENNV
            // 
            this.txtTENNV.BorderColor = System.Drawing.Color.DarkGray;
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
            this.txtTENNV.Location = new System.Drawing.Point(628, 56);
            this.txtTENNV.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.txtTENNV.Name = "txtTENNV";
            this.txtTENNV.PlaceholderText = "";
            this.txtTENNV.SelectedText = "";
            this.txtTENNV.Size = new System.Drawing.Size(223, 48);
            this.txtTENNV.TabIndex = 64;
            // 
            // txtMANV
            // 
            this.txtMANV.BorderColor = System.Drawing.Color.DarkGray;
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
            this.txtMANV.Location = new System.Drawing.Point(203, 56);
            this.txtMANV.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.txtMANV.Name = "txtMANV";
            this.txtMANV.PlaceholderText = "";
            this.txtMANV.SelectedText = "";
            this.txtMANV.Size = new System.Drawing.Size(231, 48);
            this.txtMANV.TabIndex = 65;
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
            this.dgvDMPhong.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.MAKHOA,
            this.TENKHOA});
            this.dgvDMPhong.EnableHeadersVisualStyles = false;
            this.dgvDMPhong.Location = new System.Drawing.Point(66, 167);
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
            this.dgvDMPhong.Size = new System.Drawing.Size(784, 289);
            this.dgvDMPhong.TabIndex = 74;
            // 
            // btnadd_KHOA
            // 
            this.btnadd_KHOA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnadd_KHOA.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.create;
            this.btnadd_KHOA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnadd_KHOA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_KHOA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnadd_KHOA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnadd_KHOA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnadd_KHOA.FillColor = System.Drawing.Color.Transparent;
            this.btnadd_KHOA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnadd_KHOA.ForeColor = System.Drawing.Color.White;
            this.btnadd_KHOA.Location = new System.Drawing.Point(820, 120);
            this.btnadd_KHOA.Name = "btnadd_KHOA";
            this.btnadd_KHOA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnadd_KHOA.Size = new System.Drawing.Size(30, 32);
            this.btnadd_KHOA.TabIndex = 75;
            // 
            // MAKHOA
            // 
            this.MAKHOA.DataPropertyName = "MAKHOA";
            this.MAKHOA.HeaderText = "Mã khoa";
            this.MAKHOA.MinimumWidth = 8;
            this.MAKHOA.Name = "MAKHOA";
            // 
            // TENKHOA
            // 
            this.TENKHOA.DataPropertyName = "TENKHOA";
            this.TENKHOA.HeaderText = "Tên khoa";
            this.TENKHOA.MinimumWidth = 8;
            this.TENKHOA.Name = "TENKHOA";
            // 
            // btndelete_KHOA
            // 
            this.btndelete_KHOA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btndelete_KHOA.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.del;
            this.btndelete_KHOA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndelete_KHOA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_KHOA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btndelete_KHOA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btndelete_KHOA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btndelete_KHOA.FillColor = System.Drawing.Color.Transparent;
            this.btndelete_KHOA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btndelete_KHOA.ForeColor = System.Drawing.Color.White;
            this.btndelete_KHOA.Location = new System.Drawing.Point(742, 120);
            this.btndelete_KHOA.Name = "btndelete_KHOA";
            this.btndelete_KHOA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btndelete_KHOA.Size = new System.Drawing.Size(35, 32);
            this.btndelete_KHOA.TabIndex = 77;
            // 
            // btnedit_KHOA
            // 
            this.btnedit_KHOA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnedit_KHOA.BackgroundImage = global::QuanLyKiTucXa.Properties.Resources.edit;
            this.btnedit_KHOA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnedit_KHOA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_KHOA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnedit_KHOA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnedit_KHOA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnedit_KHOA.FillColor = System.Drawing.Color.Transparent;
            this.btnedit_KHOA.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnedit_KHOA.ForeColor = System.Drawing.Color.White;
            this.btnedit_KHOA.Location = new System.Drawing.Point(783, 120);
            this.btnedit_KHOA.Name = "btnedit_KHOA";
            this.btnedit_KHOA.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnedit_KHOA.Size = new System.Drawing.Size(31, 32);
            this.btnedit_KHOA.TabIndex = 76;
            // 
            // frmadd_KHOA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 595);
            this.Controls.Add(this.btndelete_KHOA);
            this.Controls.Add(this.btnedit_KHOA);
            this.Controls.Add(this.btnadd_KHOA);
            this.Controls.Add(this.dgvDMPhong);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTENNV);
            this.Controls.Add(this.txtMANV);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmadd_KHOA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmadd_KHOA";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMenu;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtTENNV;
        private Guna.UI2.WinForms.Guna2TextBox txtMANV;
        private System.Windows.Forms.DataGridView dgvDMPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAKHOA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENKHOA;
        private Guna.UI2.WinForms.Guna2CircleButton btnadd_KHOA;
        private Guna.UI2.WinForms.Guna2CircleButton btndelete_KHOA;
        private Guna.UI2.WinForms.Guna2CircleButton btnedit_KHOA;
    }
}