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
    public partial class UC_QLDV_Ribbon : UserControl
    {
        public UC_QLDV_Ribbon()
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

        private void UC_QLDV_Ribbon_Load(object sender, EventArgs e)
        {
            btnDMDV.Checked = true;
            addUserControl(new UC_DANHMUC_DV());
        }

        private void btnDMDV_Click(object sender, EventArgs e)
        {
            
            addUserControl( new UC_DANHMUC_DV());
        }

        private void btnHDDV_Click(object sender, EventArgs e)
        {
            UC_HOADON_DV uc = new UC_HOADON_DV();
            addUserControl(uc);
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHD_INT_Click(object sender, EventArgs e)
        {
            UC_HD_INT uc = new UC_HD_INT();
            addUserControl(uc);
        }

        private void btnHD_DIEN_Click(object sender, EventArgs e)
        {
            UC_HD_DIEN uc = new UC_HD_DIEN();
            addUserControl(uc);
        }

        private void btnHD_NUOC_Click(object sender, EventArgs e)
        {
            UC_HD_NUOC uc = new UC_HD_NUOC();
            addUserControl(uc);
        }
    }
}
