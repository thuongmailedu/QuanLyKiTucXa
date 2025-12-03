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
        private Microsoft.Reporting.WinForms.ReportParameter[] reportParameters;

        public frm_IN_HOPDONG()
        {
            InitializeComponent();
        }

        // Method để nhận parameters từ UC_ThuePhong
        public void SetReportParameters(Microsoft.Reporting.WinForms.ReportParameter[] parameters)
        {
            this.reportParameters = parameters;
        }

        private void frm_IN_HOPDONG_Load(object sender, EventArgs e)
        {
            try
            {
                // Set parameters vào report
                if (reportParameters != null)
                {
                    this.reportViewer1.LocalReport.SetParameters(reportParameters);
                }

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
