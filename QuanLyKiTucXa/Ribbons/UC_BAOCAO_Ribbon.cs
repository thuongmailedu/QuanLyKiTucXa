using QuanLyKiTucXa.Main_UC.QLPHONG;
using QuanLyKiTucXa.Main_UC.BAOCAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Ribbons
{
    public partial class UC_BAOCAO_Ribbon : UserControl
    {
        public UC_BAOCAO_Ribbon()
        {
            InitializeComponent();
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void UC_BAOCAO_Ribbon_Load(object sender, EventArgs e)
        {
            btnBC_SINHVIEN.Checked = true;
            addUserControl(new UC_BC_SINHVIEN());
        }

        private void btnBC_SINHVIEN_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_BC_SINHVIEN());
        }

        private void btnBC_PHONG_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_BC_PHONG());
        }

        private void btnBC_HOPDONG_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_BC_HOPDONG());
        }

        private void btnBC_HOADON_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_BC_HOADON());
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
