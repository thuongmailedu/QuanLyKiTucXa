using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyKiTucXa.Main_UC.BAOCAO
{
    public partial class UC_BC_PHONG : UserControl
    {
        string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_BC_PHONG()
        {
            InitializeComponent();
        }

        private void UC_BC_PHONG_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboNha();
                LoadComboTinhTrang();

                //dtpTHANG.Format = DateTimePickerFormat.Custom;
                //dtpTHANG.CustomFormat = "MM/yyyy";
                //dtpTHANG.ShowUpDown = true;
                dtpTHANG.Value = DateTime.Now;

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboNha()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT MANHA, 
                                     MANHA + ' - ' + LOAIPHONG + ' (' + GIOITINH + ')' AS TENNHA 
                                     FROM NHA 
                                     ORDER BY MANHA";

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
                MessageBox.Show("Lỗi load nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboTinhTrang()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("VALUE");
                dt.Columns.Add("DISPLAY");

                dt.Rows.Add("ALL", "--- Tất cả ---");
                dt.Rows.Add("Còn trống", "Còn trống");
                dt.Rows.Add("Đã hết chỗ", "Đã hết chỗ");

                comTINHTRANG.DataSource = dt;
                comTINHTRANG.DisplayMember = "DISPLAY";
                comTINHTRANG.ValueMember = "VALUE";
                comTINHTRANG.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load tình trạng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string maNha = (comNHA.SelectedValue != null && comNHA.SelectedValue.ToString() != "ALL")
                               ? comNHA.SelectedValue.ToString() : null;

                string tinhTrang = null;
                if (comTINHTRANG.SelectedValue != null && comTINHTRANG.SelectedValue.ToString() != "ALL")
                    tinhTrang = comTINHTRANG.SelectedValue.ToString();

                int thang = dtpTHANG.Value.Month;
                int nam = dtpTHANG.Value.Year;
                string tenNV = GetTenNhanVien(UserSession.TenDangNhap);

                DataTable dtBaoCao = GetDataBaoCao(maNha, thang, nam, tinhTrang);

                if (dtBaoCao.Rows.Count == 0)
                {
                    MessageBox.Show($"Không có dữ liệu phòng trong tháng {thang:00}/{nam}!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                HienThiBaoCao(dtBaoCao, maNha, thang, nam, tinhTrang, tenNV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private DataTable GetDataBaoCao(string maNha, int thang, int nam, string tinhTrang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoTinhTrangPhong", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MANHA", (object)maNha ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@THANG", thang);
                cmd.Parameters.AddWithValue("@NAM", nam);
                cmd.Parameters.AddWithValue("@TINHTRANG", (object)tinhTrang ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        private void HienThiBaoCao(DataTable data, string maNha, int thang, int nam, string tinhTrang, string tenNV)
        {
            try
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyKiTucXa.ReportsSystem.Reports.rpt_TT_PHONG.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("DataSet_TT_PHONG", data);
                reportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("prMANHA", string.IsNullOrEmpty(maNha) ? "Tất cả" : maNha),
                    new ReportParameter("prTHANG", $"Tháng {thang:00}/{nam}"),
                    new ReportParameter("prTINHTRANG", string.IsNullOrEmpty(tinhTrang) ? "Tất cả" : tinhTrang),
                    new ReportParameter("prTENNV", string.IsNullOrEmpty(tenNV) ? "" : tenNV)
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
    }
}