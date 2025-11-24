using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    public partial class frm_HD_TONGHOP : Form
    {
        public frm_HD_TONGHOP()
        {
            InitializeComponent();
        }

        private void frm_HD_TONGHOP_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
