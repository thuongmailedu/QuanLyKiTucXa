using QuanLyKiTucXa.Main_UC.QLHD;
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
    public partial class UC_QLHD_Ribbon : UserControl
    {

        public UC_QLHD_Ribbon()
        {
            InitializeComponent();
        }
        private void UC_QLHD_Ribbon_Load(object sender, EventArgs e)
        {
            btnThuePhong.Checked = true;
            addUserControl(new UC_ThuePhong());
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void btnThuePhong_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_ThuePhong());
        }

        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_TraPhong());
        }

        private void btnDATPHONG_Click(object sender, EventArgs e)
        {
            addUserControl(new UC_DANHMUCPHONG());
        }
    }
}
