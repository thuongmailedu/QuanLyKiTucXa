using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLHD_FORM
{
    public partial class frm_IN_HOPDONG : Form
    {
        public frm_IN_HOPDONG()
        {
            InitializeComponent();
        }

        private void frm_IN_HOPDONG_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            
        }
    }
}
