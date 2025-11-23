using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyKiTucXa.Main_UC.BAOCAO
{
    public partial class UC_BC_SINHVIEN : UserControl
    {
        string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_BC_SINHVIEN()
        {
            InitializeComponent();
        }

        private void UC_BC_SINHVIEN_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboNha();
                dtpTHOIGIAN.Format = DateTimePickerFormat.Custom;
                dtpTHOIGIAN.CustomFormat = "MM/yyyy";
                dtpTHOIGIAN.Value = DateTime.Now;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboNha()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT MANHA, MANHA + ' - ' + LOAIPHONG + ' (' + GIOITINH + ')' AS TENNHA 
                                     FROM NHA ORDER BY MANHA";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow rowAll = dt.NewRow();
                    rowAll["MANHA"] = "ALL";
                    rowAll["TENNHA"] = "--- Tất cả ---";
                    dt.Rows.InsertAt(rowAll, 0);

                    comNHA.DataSource = dt;
                    comNHA.DisplayMember = "TENNHA";
                    comNHA.ValueMember = "MANHA";
                    comNHA.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhà: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue == null) return;

            string maNha = comNHA.SelectedValue.ToString();

            if (maNha == "ALL")
            {
                // Chọn tất cả -> không load phòng
                comPHONG.DataSource = null;
                comPHONG.Items.Clear();
                comPHONG.Items.Add("--- Tất cả ---");
                comPHONG.SelectedIndex = 0;
            }
            else
            {
                // Chọn nhà cụ thể -> load danh sách phòng
                LoadComboPhong(maNha);
            }
        }

        private void LoadComboPhong(string maNha)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT MA_PHONG FROM PHONG WHERE MANHA = @MANHA ORDER BY MA_PHONG";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MANHA", maNha);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow rowAll = dt.NewRow();
                    rowAll["MA_PHONG"] = "ALL";
                    dt.Rows.InsertAt(rowAll, 0);

                    comPHONG.DataSource = dt;
                    comPHONG.DisplayMember = "MA_PHONG";
                    comPHONG.ValueMember = "MA_PHONG";
                    comPHONG.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string maNha = (comNHA.SelectedValue != null && comNHA.SelectedValue.ToString() != "ALL")
                               ? comNHA.SelectedValue.ToString() : null;

                string maPhong = null;
                if (comPHONG.SelectedValue != null && comPHONG.SelectedValue.ToString() != "ALL")
                    maPhong = comPHONG.SelectedValue.ToString();

                int thang = dtpTHOIGIAN.Value.Month;
                int nam = dtpTHOIGIAN.Value.Year;
                string tenNV = GetTenNhanVien(UserSession.TenDangNhap);

                DataTable dtBaoCao = GetDataBaoCao(maNha, maPhong, thang, nam);

                if (dtBaoCao.Rows.Count == 0)
                {
                    MessageBox.Show($"Không có sinh viên cư trú trong tháng {thang:00}/{nam}!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                HienThiBaoCao(dtBaoCao, maNha, maPhong, thang, nam, tenNV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    return result != null ? result.ToString() : "Quản trị viên";
                }
            }
            catch { return "Quản trị viên"; }
        }

        private DataTable GetDataBaoCao(string maNha, string maPhong, int thang, int nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoSinhVienTheoThang", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MANHA", (object)maNha ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MA_PHONG", (object)maPhong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@THANG", thang);
                cmd.Parameters.AddWithValue("@NAM", nam);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void HienThiBaoCao(DataTable data, string maNha, string maPhong, int thang, int nam, string tenNV)
        {
            try
            {
                // ✅ SỬA ĐƯỜNG DẪN CHO ĐÚNG VỚI CẤU TRÚC THƯ MỤC
               
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyKitucXa.ReportsSystem.Reports.rptSINHVIEN.rdlc";

                //reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("DataSet_DS_SINHVIEN", data);
                reportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("prMANHA", string.IsNullOrEmpty(maNha) ? "Tất cả" : maNha),
                    new ReportParameter("prMA_PHONG", string.IsNullOrEmpty(maPhong) ? "Tất cả" : maPhong),
                    new ReportParameter("prTHANG", $"Tháng {thang:00}/{nam}"),
                    new ReportParameter("prTENNV", string.IsNullOrEmpty(tenNV) ? "" : tenNV)
                };

                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}