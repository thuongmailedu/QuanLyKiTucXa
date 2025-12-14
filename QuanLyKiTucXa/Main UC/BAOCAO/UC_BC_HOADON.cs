using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyKiTucXa.Main_UC.BAOCAO
{
    public partial class UC_BC_HOADON : UserControl
    {
        string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_BC_HOADON()
        {
            InitializeComponent();
        }

        private void UC_BC_HOADON_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách nhà
                LoadComboNha();

                // Load danh sách trạng thái
                LoadComboTrangThai();

                // Thiết lập DateTimePicker - chỉ hiển thị tháng/năm
                //dtpTHANG.Format = DateTimePickerFormat.Custom;
                //dtpTHANG.CustomFormat = "MM/yyyy";
                //dtpTHANG.ShowUpDown = true; // Dùng nút lên/xuống thay vì calendar
                dtpTHANG.Value = DateTime.Now;

                // Load ReportViewer
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

                    // Thêm "Tất cả"
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

        private void LoadComboTrangThai()
        {
            try
            {
                // Tạo danh sách trạng thái
                DataTable dt = new DataTable();
                dt.Columns.Add("VALUE");
                dt.Columns.Add("DISPLAY");

                dt.Rows.Add("ALL", "--- Tất cả ---");
                dt.Rows.Add("Chưa thanh toán", "Chưa thanh toán");
                dt.Rows.Add("Đã thanh toán", "Đã thanh toán");

                comTRANGTHAI.DataSource = dt;
                comTRANGTHAI.DisplayMember = "DISPLAY";
                comTRANGTHAI.ValueMember = "VALUE";
                comTRANGTHAI.SelectedIndex = 0; // Mặc định chọn "Tất cả"
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load trạng thái: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ⚡ EVENT 1: Khi chọn nhà -> Load phòng
        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue == null) return;

            string maNha = comNHA.SelectedValue.ToString();

            if (maNha == "ALL")
            {
                // Chọn "Tất cả" -> Reset combo phòng
                comPHONG.DataSource = null;
                comPHONG.Items.Clear();
                comPHONG.Items.Add("--- Tất cả ---");
                comPHONG.SelectedIndex = 0;
            }
            else
            {
                // Chọn nhà cụ thể -> Load danh sách phòng
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

                    // Thêm "Tất cả"
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
                MessageBox.Show("Lỗi load phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ⚡ EVENT 2: Nút Xem báo cáo
        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tham số
                string maNha = (comNHA.SelectedValue != null && comNHA.SelectedValue.ToString() != "ALL")
                               ? comNHA.SelectedValue.ToString() : null;

                string maPhong = null;
                if (comPHONG.SelectedValue != null && comPHONG.SelectedValue.ToString() != "ALL")
                    maPhong = comPHONG.SelectedValue.ToString();

                string trangThai = null;
                if (comTRANGTHAI.SelectedValue != null && comTRANGTHAI.SelectedValue.ToString() != "ALL")
                    trangThai = comTRANGTHAI.SelectedValue.ToString();

                int thang = dtpTHANG.Value.Month;
                int nam = dtpTHANG.Value.Year;
                string tenNV = GetTenNhanVien(UserSession.TenDangNhap);

                // Load dữ liệu
                DataTable dtBaoCao = GetDataBaoCao(maNha, maPhong, thang, nam, trangThai);

                if (dtBaoCao.Rows.Count == 0)
                {
                    MessageBox.Show($"Không có hóa đơn nào trong tháng {thang:00}/{nam}!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Hiển thị báo cáo
                HienThiBaoCao(dtBaoCao, maNha, maPhong, thang, nam, trangThai, tenNV);
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

        private DataTable GetDataBaoCao(string maNha, string maPhong, int thang, int nam, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LayHoaDonTongHop", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Truyền tham số
                cmd.Parameters.AddWithValue("@THANG", thang);
                cmd.Parameters.AddWithValue("@NAM", nam);
                cmd.Parameters.AddWithValue("@MANHA", (object)maNha ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MA_PHONG", (object)maPhong ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TINHTRANGTT", (object)trangThai ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        private void HienThiBaoCao(DataTable data, string maNha, string maPhong, int thang, int nam, string trangThai, string tenNV)
        {
            try
            {
                // Sử dụng Embedded Resource
                reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyKiTucXa.ReportsSystem.Reports.rptHDTONGHOP.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Gán DataSource
                ReportDataSource rds = new ReportDataSource("DataSet_HDTONGHOP", data);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Thiết lập Parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("prMANHA", string.IsNullOrEmpty(maNha) ? "Tất cả" : maNha),
                    new ReportParameter("prMA_PHONG", string.IsNullOrEmpty(maPhong) ? "Tất cả" : maPhong),
                    new ReportParameter("prTHANG", $"Tháng {thang:00}/{nam}"),
                    new ReportParameter("prTENNV", string.IsNullOrEmpty(tenNV) ? "" : tenNV),
                    new ReportParameter("prTRANGTHAI", string.IsNullOrEmpty(trangThai) ? "Tất cả" : trangThai)
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