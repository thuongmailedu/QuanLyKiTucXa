using QuanLyKiTucXa.Main_UC.QLPHONG;
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
    public partial class UC_QLPHONG_Ribbon : UserControl
    {
        public UC_QLPHONG_Ribbon()
        {
            InitializeComponent();
        }

        private void UC_QLPHONG_Ribbon_Load(object sender, EventArgs e)
        {
            btnDMCSVC.Checked = true;
            //addUserControl(new UC_DANHMUCPHONG());
            addUserControl(new UC_DANHMUCCSVC());
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnDMPhong_Click(object sender, EventArgs e)
        {
            //addUserControl(new UC_DANHMUCPHONG());
        }

        private void btnDMCSVC_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_DANHMUCCSVC());
        }

        private void btnSCCSVC_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_SCCSVC());
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
