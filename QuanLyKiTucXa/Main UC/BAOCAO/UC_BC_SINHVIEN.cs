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
                dtpTHANG.Format = DateTimePickerFormat.Custom;
                dtpTHANG.CustomFormat = "MM/yyyy";
                dtpTHANG.Value = DateTime.Now;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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
                MessageBox.Show("Lỗi load nhà: " + ex.Message);
            }
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue == null) return;

            string maNha = comNHA.SelectedValue.ToString();

            if (maNha == "ALL")
            {
                comPHONG.DataSource = null;
                comPHONG.Items.Clear();
                comPHONG.Items.Add("--- Tất cả ---");
                comPHONG.SelectedIndex = 0;
            }
            else
            {
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
                MessageBox.Show("Lỗi load phòng: " + ex.Message);
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

                int thang = dtpTHANG.Value.Month;
                int nam = dtpTHANG.Value.Year;
                string tenNV = GetTenNhanVien(UserSession.TenDangNhap);

                DataTable dtBaoCao = GetDataBaoCao(maNha, maPhong, thang, nam);

                if (dtBaoCao.Rows.Count == 0)
                {
                    MessageBox.Show($"Không có dữ liệu tháng {thang:00}/{nam}!", "Thông báo");
                    return;
                }

                HienThiBaoCao(dtBaoCao, maNha, maPhong, thang, nam, tenNV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi");
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
            catch { return ""; }
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
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyKiTucXa.ReportsSystem.Reports.rptSINHVIEN.rdlc";
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
                MessageBox.Show("Lỗi:\n" + ex.Message + "\n\n" + ex.StackTrace, "Lỗi");
            }
        }
    }
}