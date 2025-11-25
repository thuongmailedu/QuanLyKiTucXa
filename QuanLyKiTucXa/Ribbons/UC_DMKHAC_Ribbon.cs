using QuanLyKiTucXa.Main_UC.DMKHAC;
using QuanLyKiTucXa.Main_UC.QLDV;
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
    public partial class UC_DMKHAC_Ribbon : UserControl
    {
        public UC_DMKHAC_Ribbon()
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
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_DMKHAC_Ribbon_Load(object sender, EventArgs e)
        {
            btnDM_NHANVIEN.Checked = true;
            addUserControl(new UC_DM_NHANVIEN());
        }

        private void btnDM_NHANVIEN_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_DM_NHANVIEN());
        }

        private void btnDM_LOP_KHOA_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_DM_LOP_KHOA());
        }

        private void btnDM_NHACC_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_NHACC());
        }
    }
}
