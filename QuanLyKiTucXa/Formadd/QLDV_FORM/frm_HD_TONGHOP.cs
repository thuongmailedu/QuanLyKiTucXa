using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyKiTucXa.Formadd.QLDV_FORM   
{
    public partial class frm_HD_TONGHOP : Form
    {
        string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        private string _maNha;
        private string _maPhong;
        private int _thang;
        private int _nam;
        private string _ttThanhToan;

        // Constructor nhận tham số
        public frm_HD_TONGHOP(string maNha, string maPhong, int thang, int nam, string ttThanhToan)
        {
            InitializeComponent();

            _maNha = maNha;
            _maPhong = maPhong;
            _thang = thang;
            _nam = nam;
            _ttThanhToan = ttThanhToan;
        }

        private void frm_HD_TONGHOP_Load(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu chi tiết hóa đơn
                DataTable dtHoaDon = GetChiTietHoaDon(_maPhong, _thang, _nam);

                if (dtHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                // Lấy tên nhân viên
                string tenNV = GetTenNhanVien(UserSession.TenDangNhap);

                // Hiển thị báo cáo
                HienThiBaoCao(dtHoaDon, tenNV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetChiTietHoaDon(string maPhong, int thang, int nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ChiTietHoaDonTheoPhong", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                cmd.Parameters.AddWithValue("@THANG", thang);
                cmd.Parameters.AddWithValue("@NAM", nam);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        private string GetTenNhanVien(string tenDN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TENNV FROM NHANVIEN WHERE MANV = @TENDN";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TENDN", tenDN);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
            catch
            {
                return "";
            }
        }

        private void HienThiBaoCao(DataTable data, string tenNV)
        {
            try
            {
                // Sử dụng Embedded Resource
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyKiTucXa.ReportsSystem.Reports.rpt_HD_CHITIET.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Gán DataSource
                ReportDataSource rds = new ReportDataSource("DataSet_CHITIET_HOADON", data);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Thiết lập Parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("prMANHA", _maNha ?? ""),
                    new ReportParameter("prMA_PHONG", _maPhong ?? ""),
                    new ReportParameter("prTHOIGIAN", $"Tháng {_thang:00}/{_nam}"),
                    new ReportParameter("prTENNV", string.IsNullOrEmpty(tenNV) ? "" : tenNV),
                    new ReportParameter("prTTTHANHTOAN", _ttThanhToan ?? "")
                };

                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo:\n" + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút In
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.PrintDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Xuất Excel
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType, encoding, extension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    "Excel", null, out mimeType, out encoding,
                    out extension, out streamIds, out warnings);

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xls";
                saveDialog.FilterIndex = 0;
                saveDialog.FileName = $"HoaDon_{_maPhong}_{_thang:00}_{_nam}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Đóng
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}