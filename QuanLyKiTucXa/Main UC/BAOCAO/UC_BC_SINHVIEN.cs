using QuanLyKiTucXa.Main_UC.BAOCAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyKiTucXa.UserControls
{
    public partial class UC_BC_SINHVIEN : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_BC_SINHVIEN()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadComboNha();
                dtpTHOIGIAN.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message, "Lỗi",
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

                    comNHA.DataSource = dt;
                    comNHA.DisplayMember = "TENNHA";
                    comNHA.ValueMember = "MANHA";
                    comNHA.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message);
            }
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedIndex >= 0)
            {
                LoadComboPhong(comNHA.SelectedValue.ToString());
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

                    // Thêm dòng "Tất cả" vào đầu
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
                MessageBox.Show("Lỗi khi load danh sách phòng: " + ex.Message);
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đầu vào
                if (comNHA.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn nhà!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comNHA.Focus();
                    return;
                }

                // Lấy thông tin
                string maNha = comNHA.SelectedValue.ToString();
                string maPhong = (comPHONG.SelectedValue.ToString() == "ALL") ? null : comPHONG.SelectedValue.ToString();
                int thang = dtpTHOIGIAN.Value.Month;
                int nam = dtpTHOIGIAN.Value.Year;
                string tenNV = GetTenNhanVien(UserSession.TenDangNhap);

                // Load dữ liệu
                DataTable dtBaoCao = GetDataBaoCao(maNha, maPhong, thang, nam);

                if (dtBaoCao.Rows.Count == 0)
                {
                    MessageBox.Show($"Không có sinh viên nào đang cư trú trong tháng {thang}/{nam}!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Hiển thị báo cáo
                HienThiBaoCao(dtBaoCao, maNha, maPhong, thang, nam, tenNV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem báo cáo: " + ex.Message, "Lỗi",
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

        private DataTable GetDataBaoCao(string maNha, string maPhong, int thang, int nam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BaoCaoSinhVienTheoThang", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MANHA", maNha);
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
                reportViewer1.LocalReport.ReportPath = "Reports/rptSINHVIEN.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Gán DataSource với tên DataSet giống trong RDLC
                ReportDataSource rds = new ReportDataSource("DataSet_DS_SINHVIEN", data);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Thiết lập các tham số
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("prMANHA", maNha),
                    new ReportParameter("prMA_PHONG", string.IsNullOrEmpty(maPhong) ? "Tất cả" : maPhong),
                    new ReportParameter("prTHANG", $"Tháng {thang:00}/{nam}"),
                    new ReportParameter("prTENNV", tenNV)
                };

                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UC_BC_SINHVIEN_Load(object sender, EventArgs e)
        {

        }
    }
}