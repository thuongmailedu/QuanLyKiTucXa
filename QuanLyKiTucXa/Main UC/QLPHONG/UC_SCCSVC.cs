using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLPHONG
{
    public partial class UC_SCCSVC : UserControl
    {
        public UC_SCCSVC()
        {
            InitializeComponent();
        }

        private void UC_SCCSVC_Load(object sender, EventArgs e)
        {
            dgvSCCSVC.ColumnHeadersDefaultCellStyle.Font = new Font(dgvSCCSVC.Font, FontStyle.Bold);

        }

        private void btn_SCCSVC_Click(object sender, EventArgs e)
        {
            frm_SCCSVC form = new frm_SCCSVC(); // Tạo instance
            form.ShowDialog();
        }

        private void btnfillter_Click(object sender, EventArgs e)
        {

        }

        private void btnedit_Click(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

        }
    }
}
