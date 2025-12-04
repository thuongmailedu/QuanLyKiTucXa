using System;
using QuanLyKiTucXa.Main_UC.QLDV;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    public partial class frm_TTHOADON : Form
    {
        // Properties nhận từ form gọi
        public string MaNha { get; set; }
        public string MaPhong { get; set; }
        public DateTime ThoiGian { get; set; }
        public decimal TongTien { get; set; }
        public bool IsViewOnly { get; set; } = false; // Chế độ xem (đã thanh toán)

        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True"; // Thay bằng connection string của bạn
                                                                                                                                       // Properties nhận từ form gọi
        
        public frm_TTHOADON()
        {
            InitializeComponent();
        }

        private void frm_TTHOADON_Load(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị thông tin cơ bản
                txtMANHA.Text = MaNha;
                txtMA_PHONG.Text = MaPhong;
                dtpTHOIGIAN.Value = ThoiGian;
                txtTONGTHANHTOAN.Text = TongTien.ToString("N0") + " VNĐ";

                // ✅ Lấy thông tin nhân viên từ UserSession (ĐÚNG CẤU TRÚC CỦA BẠN)
                txtMANV.Text = UserSession.TenDangNhap;  // TenDangNhap chính là MANV
                LayThongTinNhanVien(UserSession.TenDangNhap);

                // Load chi tiết hóa đơn
                LoadChiTietHoaDon();

                // Kiểm tra đã thanh toán chưa
                if (KiemTraDaThanhToan())
                {
                    // Đã thanh toán - Load thông tin và khóa form
                    LoadThongTinThanhToan();
                    IsViewOnly = true;
                    KhoaForm();
                    this.Text = "XEM THÔNG TIN THANH TOÁN";
                }
                else
                {
                    // Chưa thanh toán - cho phép nhập
                    dtpNGAYTHANHTOAN.Value = DateTime.Now;
                    comHINHTHUC_TT.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load form: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietHoaDon()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ChiTietHoaDonTheoPhong", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MA_PHONG", MaPhong);
                    cmd.Parameters.AddWithValue("@THANG", ThoiGian.Month);
                    cmd.Parameters.AddWithValue("@NAM", ThoiGian.Year);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvCHITIET_HD.DataSource = dt;

                    // Format columns
                    //if (dgvCHITIET_HD.Columns.Contains("LOAI_HOADON"))
                    //    dgvCHITIET_HD.Columns["LOAI_HOADON"].HeaderText = "Loại HĐ";
                    //if (dgvCHITIET_HD.Columns.Contains("TENHD"))
                    //    dgvCHITIET_HD.Columns["TENHD"].HeaderText = "Tên hóa đơn";
                    //if (dgvCHITIET_HD.Columns.Contains("CHISOCU"))
                    //    dgvCHITIET_HD.Columns["CHISOCU"].HeaderText = "Chỉ số cũ";
                    //if (dgvCHITIET_HD.Columns.Contains("CHISOMOI"))
                    //    dgvCHITIET_HD.Columns["CHISOMOI"].HeaderText = "Chỉ số mới";
                    //if (dgvCHITIET_HD.Columns.Contains("SOLUONG_TIEUDUNG"))
                    //    dgvCHITIET_HD.Columns["SOLUONG_TIEUDUNG"].HeaderText = "Số lượng";
                    //if (dgvCHITIET_HD.Columns.Contains("DONGIA"))
                    //{
                    //    dgvCHITIET_HD.Columns["DONGIA"].HeaderText = "Đơn giá";
                    //    dgvCHITIET_HD.Columns["DONGIA"].DefaultCellStyle.Format = "N0";
                    //}
                    //if (dgvCHITIET_HD.Columns.Contains("DONVI"))
                    //    dgvCHITIET_HD.Columns["DONVI"].HeaderText = "Đơn vị";
                    //if (dgvCHITIET_HD.Columns.Contains("TONGTIEN"))
                    //{
                    //    dgvCHITIET_HD.Columns["TONGTIEN"].HeaderText = "Thành tiền";
                    //    dgvCHITIET_HD.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
                    //}
                    //if (dgvCHITIET_HD.Columns.Contains("TINHTRANGTT"))
                    //    dgvCHITIET_HD.Columns["TINHTRANGTT"].HeaderText = "Trạng thái";

                    // Ẩn các cột không cần thiết
                    string[] hiddenColumns = { "LOAI_HOADON", "MA_PHONG", "MADV", "THOIGIAN", "TINHTRANGTT" };
                    foreach (string col in hiddenColumns)
                    {
                        if (dgvCHITIET_HD.Columns.Contains(col))
                            dgvCHITIET_HD.Columns[col].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load chi tiết hóa đơn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraDaThanhToan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT COUNT(*) FROM THANHTOAN 
                                    WHERE MA_PHONG = @MA_PHONG 
                                      AND YEAR(THOIGIAN) = @NAM 
                                      AND MONTH(THOIGIAN) = @THANG";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MA_PHONG", MaPhong);
                    cmd.Parameters.AddWithValue("@NAM", ThoiGian.Year);
                    cmd.Parameters.AddWithValue("@THANG", ThoiGian.Month);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra thanh toán: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void LoadThongTinThanhToan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_LayThongTinThanhToan", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MA_PHONG", MaPhong);
                    cmd.Parameters.AddWithValue("@THANG", ThoiGian.Month);
                    cmd.Parameters.AddWithValue("@NAM", ThoiGian.Year);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtMASV.Text = reader["MASV"].ToString();
                        txtTENSV.Text = reader["TENSV"].ToString();
                        txtMANV.Text = reader["MANV"].ToString();
                        txtTENNV.Text = reader["TENNV"].ToString();
                        dtpNGAYTHANHTOAN.Value = Convert.ToDateTime(reader["NGAYTHANHTOAN"]);
                        comHINHTHUC_TT.Text = reader["HINHTHUC_TT"].ToString();
                       // txtGHICHU.Text = reader["GHICHU"].ToString();

                        decimal tongTien = Convert.ToDecimal(reader["TONGTIEN"]);
                        txtTONGTHANHTOAN.Text = tongTien.ToString("N0") + " VNĐ";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông tin thanh toán: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoaForm()
        {
            txtMASV.ReadOnly = true;
            dtpNGAYTHANHTOAN.Enabled = false;
            comHINHTHUC_TT.Enabled = false;
           // txtGHICHU.ReadOnly = true;
            btnLuu.Visible = false;
        }

        private void txtMASV_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMASV.Text))
            {
                LayThongTinSinhVien(txtMASV.Text.Trim());
            }
        }

        private void LayThongTinSinhVien(string maSV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TENSV FROM SINHVIEN WHERE MASV = @MASV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MASV", maSV);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtTENSV.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy sinh viên có mã: {maSV}",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTENSV.Text = "";
                        txtMASV.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin sinh viên: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LayThongTinNhanVien(string maNV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TENNV FROM NHANVIEN WHERE MANV = @MANV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MANV", maNV);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtTENNV.Text = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin nhân viên: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(txtMASV.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên thanh toán!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMASV.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTENSV.Text))
                {
                    MessageBox.Show("Mã sinh viên không hợp lệ!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMASV.Focus();
                    return;
                }

                if (comHINHTHUC_TT.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn hình thức thanh toán!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comHINHTHUC_TT.Focus();
                    return;
                }

                // Confirm
                DialogResult confirm = MessageBox.Show(
                    $"Xác nhận thanh toán hóa đơn:\n\n" +
                    $"Phòng: {MaPhong}\n" +
                    $"Tháng: {ThoiGian:MM/yyyy}\n" +
                    $"Tổng tiền: {txtTONGTHANHTOAN.Text}\n" +
                    $"Sinh viên: {txtTENSV.Text}\n" +
                    $"Hình thức: {comHINHTHUC_TT.Text}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.No) return;

                // Lưu thanh toán
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CapNhatTrangThaiHoaDon", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MA_PHONG", MaPhong);
                    cmd.Parameters.AddWithValue("@THANG", ThoiGian.Month);
                    cmd.Parameters.AddWithValue("@NAM", ThoiGian.Year);
                    cmd.Parameters.AddWithValue("@TINHTRANGTT", "Đã thanh toán");
                    cmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MANV", txtMANV.Text.Trim());
                    cmd.Parameters.AddWithValue("@NGAYTHANHTOAN", dtpNGAYTHANHTOAN.Value);
                    cmd.Parameters.AddWithValue("@HINHTHUC_TT", comHINHTHUC_TT.Text);
                   // cmd.Parameters.AddWithValue("@GHICHU", txtGHICHU.Text.Trim());

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int success = Convert.ToInt32(reader["Success"]);
                        if (success == 1)
                        {
                            MessageBox.Show("Thanh toán thành công!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            string errorMsg = reader["ErrorMessage"].ToString();
                            MessageBox.Show($"Lỗi: {errorMsg}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu thanh toán: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Class UserSession(nếu chưa có)
    //public static class UserSession
    //{
    //    public static string MANV { get; set; }
    //    public static string TENNV { get; set; }
    //    public static string QUYEN { get; set; }
    //}
}