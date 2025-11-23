using QuanLyKiTucXa.Formadd;
using QuanLyKiTucXa.Formadd.HSSV;
using QuanLyKiTucXa.Ribbons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class frmLayout : Form
    {
        CultureInfo viVN = new CultureInfo("vi-VN");
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        public frmLayout()
        {
            InitializeComponent();
            lblTime.Text = System.DateTime.Now.ToString("dddd dd/MM/yyyy HH:mm:ss", viVN);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnHome.Checked = true;
            addUserControl(new UC_TrangChu_Ribbon());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToString("dddd dd/MM/yyyy HH:mm:ss", viVN);
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_HOSOSINHVIEN_Ribbon());
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_QLHD_Ribbon());
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_QLPHONG_Ribbon());
        }
       
        private void btnHome_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_TrangChu_Ribbon());
        }

        private void btn_qldv_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_QLDV_Ribbon());
            //testadddata form = new testadddata(); // Tạo instance
            //form.ShowDialog();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_BAOCAO_Ribbon());
        }
    }
}
