using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLHD_FORM
{
    public partial class frm_addHopDong : Form
    {
        SqlConnection conn = new SqlConnection();
        string constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private HopDongValidator validator;
        private bool isFromUCDanhMucPhong = false;
        private string maNhanVien = "";

        public frm_addHopDong()
        {
            InitializeComponent();

            // Khởi tạo connection string TRƯỚC KHI khởi tạo validator
            conn.ConnectionString = constr;
            validator = new HopDongValidator(constr);
        }

        private void frm_addHopDong_Load(object sender, EventArgs e)
        {
            try
            {
                // Mở connection
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                // Tự động điền tên hợp đồng
                txtTENHD.Text = "Hợp đồng đăng ký phòng";

                // Load thông tin nhân viên
                LoadThongTinNhanVien();

                // Đăng ký event cho txtMASV
                txtMASV.Leave += txtMASV_Leave;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinNhanVien()
        {
            try
            {
                if (string.IsNullOrEmpty(UserSession.TenDangNhap))
                {
                    MessageBox.Show("Không tìm thấy thông tin đăng nhập! Vui lòng đăng nhập lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                maNhanVien = UserSession.TenDangNhap;

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT TENNV FROM NHANVIEN WHERE MANV = @MANV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MANV", maNhanVien);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtTENNV.Text = result.ToString();
                        txtTENNV.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy nhân viên với mã: {maNhanVien}",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị tên sinh viên và giới tính khi nhập MASV
        /// </summary>
        private void txtMASV_Leave(object sender, EventArgs e)
        {
            string maSV = txtMASV.Text.Trim();

            if (string.IsNullOrEmpty(maSV))
            {
                txtTENSV.Clear();
                comGIOITINH.SelectedIndex = -1;
                return;
            }

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                // Lấy cả TENSV và GIOITINH
                string query = "SELECT TENSV, GIOITINH FROM SINHVIEN WHERE MASV = @MASV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MASV", maSV);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Hiển thị tên sinh viên
                            txtTENSV.Text = reader["TENSV"].ToString();

                            // Hiển thị giới tính trong ComboBox
                            string gioiTinh = reader["GIOITINH"].ToString().Trim();

                            // Tìm và chọn item trong ComboBox
                            int index = comGIOITINH.FindStringExact(gioiTinh);
                            if (index >= 0)
                            {
                                comGIOITINH.SelectedIndex = index;
                            }
                            else
                            {
                                // Nếu không tìm thấy, thêm vào ComboBox
                                comGIOITINH.Items.Add(gioiTinh);
                                comGIOITINH.SelectedItem = gioiTinh;
                            }

                            // Disable ComboBox để không cho sửa
                            comGIOITINH.Enabled = false;
                        }
                        else
                        {
                            txtTENSV.Clear();
                            comGIOITINH.SelectedIndex = -1;
                            comGIOITINH.Enabled = true;

                            MessageBox.Show($"Sinh viên với mã {maSV} chưa tồn tại trong hệ thống!",
                                "Không tìm thấy sinh viên",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            txtMASV.Focus();
                            txtMASV.SelectAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm sinh viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetThongTinPhong(string maPhong, string maNha, string loaiPhong,
            decimal giaPhong, DateTime tuNgay, DateTime denNgay, int thoiHan, decimal tongTien)
        {
            isFromUCDanhMucPhong = true;

            txtMA_PHONG.Text = maPhong;
            txtMANHA.Text = maNha;
            txtLOAIPHONG.Text = loaiPhong;
            txtGIAPHONG.Text = giaPhong.ToString("N0");

            dateTUNGAY.Value = tuNgay;
            dateDENNGAY.Value = denNgay;
            txtTHOIHAN.Text = thoiHan.ToString();
            txtTONGTIEN.Text = tongTien.ToString("N0");
            dateNGAYKY.Value = DateTime.Now;

            // Tạo mã hợp đồng
            txtMAHD.Text = GenerateMaHD();

            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtLOAIPHONG.ReadOnly = true;
            txtGIAPHONG.ReadOnly = true;
            txtTHOIHAN.ReadOnly = true;
            txtTONGTIEN.ReadOnly = true;
            dateTUNGAY.Enabled = false;
            dateDENNGAY.Enabled = true;

            dateDENNGAY.ValueChanged += DateChanged;
        }

        public void SetThongTinPhongTuUCThuePhong(string maPhong, string maNha,
            string loaiPhong, string gioiTinh, decimal giaPhong)
        {
            isFromUCDanhMucPhong = false;

            txtMA_PHONG.Text = maPhong;
            txtMANHA.Text = maNha;
            txtLOAIPHONG.Text = loaiPhong;
            txtGIAPHONG.Text = giaPhong.ToString("N0");

            dateTUNGAY.Value = DateTime.Now;
            dateTUNGAY.MinDate = DateTime.Now;
            dateDENNGAY.MinDate = DateTime.Now;
            dateNGAYKY.Value = DateTime.Now;

            txtMAHD.Text = GenerateMaHD();

            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtLOAIPHONG.ReadOnly = true;
            txtGIAPHONG.ReadOnly = true;
            txtTHOIHAN.ReadOnly = false;
            txtTONGTIEN.ReadOnly = true;
            dateTUNGAY.Enabled = true;
            dateDENNGAY.Enabled = true;

            dateTUNGAY.ValueChanged += DateChanged;
            dateDENNGAY.ValueChanged += DateChanged;
        }

        private void DateChanged(object sender, EventArgs e)
        {
            TinhThoiHanVaTongTien();
        }

        private void TinhThoiHanVaTongTien()
        {
            try
            {
                if (dateTUNGAY.Value >= dateDENNGAY.Value)
                {
                    return;
                }

                int thang = ((dateDENNGAY.Value.Year - dateTUNGAY.Value.Year) * 12) +
                           dateDENNGAY.Value.Month - dateTUNGAY.Value.Month;

                if (dateDENNGAY.Value.Day >= dateTUNGAY.Value.Day)
                {
                    thang++;
                }

                txtTHOIHAN.Text = thang.ToString();

                if (!string.IsNullOrWhiteSpace(txtGIAPHONG.Text))
                {
                    decimal giaPhong = decimal.Parse(txtGIAPHONG.Text.Replace(",", ""));
                    decimal tongTien = giaPhong * thang;
                    txtTONGTIEN.Text = tongTien.ToString("N0");
                }
            }
            catch
            {
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtMASV.Text.Trim();
                string maPhong = txtMA_PHONG.Text.Trim();
                DateTime tuNgay = dateTUNGAY.Value;
                DateTime denNgay = dateDENNGAY.Value;

                if (string.IsNullOrEmpty(maSV))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMASV.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtTENSV.Text.Trim()))
                {
                    MessageBox.Show("Sinh viên không tồn tại! Vui lòng kiểm tra lại mã sinh viên.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMASV.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtTENHD.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập tên hợp đồng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTENHD.Focus();
                    return;
                }

                if (tuNgay >= denNgay)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(maNhanVien))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên! Vui lòng đăng nhập lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Sử dụng HopDongValidator
                if (validator.KiemTraHopDongTrung(maSV, tuNgay, denNgay))
                {
                    return;
                }

                if (!validator.KiemTraPhongConCho(maPhong, tuNgay, denNgay))
                {
                    return;
                }

                // Kiểm tra giới tính
                if (!KiemTraGioiTinh(maSV, maPhong))
                {
                    return;
                }

                // Lưu hợp đồng
                if (LuuHopDong())
                {
                    MessageBox.Show("Lưu hợp đồng thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraGioiTinh(string maSV, string maPhong)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = @"
                    SELECT SV.GIOITINH AS GT_SINHVIEN, N.GIOITINH AS GT_PHONG
                    FROM SINHVIEN SV
                    CROSS JOIN (
                        SELECT P.MA_PHONG, N.GIOITINH
                        FROM PHONG P
                        INNER JOIN NHA N ON P.MANHA = N.MANHA
                        WHERE P.MA_PHONG = @MA_PHONG
                    ) N
                    WHERE SV.MASV = @MASV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MASV", maSV);
                    cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string gtSV = reader["GT_SINHVIEN"].ToString().Trim();
                            string gtPhong = reader["GT_PHONG"].ToString().Trim();

                            if (!string.Equals(gtSV, gtPhong, StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show($"Sinh viên {gtSV} không thể ở phòng dành cho {gtPhong}!",
                                    "Lỗi giới tính", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra giới tính: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool LuuHopDong()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string maHD = txtMAHD.Text.Trim();

                if (string.IsNullOrEmpty(maHD))
                {
                    maHD = GenerateMaHD();
                    txtMAHD.Text = maHD;
                }

                string query = @"
                    INSERT INTO HOPDONG 
                    (MAHD, TENHD, MASV, MA_PHONG, TUNGAY, DENNGAY, THOIHAN, DONGIA, MANV, NGAYKY, TONGTIEN)
                    VALUES 
                    (@MAHD, @TENHD, @MASV, @MA_PHONG, @TUNGAY, @DENNGAY, @THOIHAN, @DONGIA, @MANV, @NGAYKY, @TONGTIEN)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MAHD", maHD);
                    cmd.Parameters.AddWithValue("@TENHD", txtTENHD.Text.Trim());
                    cmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                    cmd.Parameters.AddWithValue("@MA_PHONG", txtMA_PHONG.Text.Trim());
                    cmd.Parameters.AddWithValue("@TUNGAY", dateTUNGAY.Value);
                    cmd.Parameters.AddWithValue("@DENNGAY", dateDENNGAY.Value);
                    cmd.Parameters.AddWithValue("@THOIHAN", int.Parse(txtTHOIHAN.Text));
                    cmd.Parameters.AddWithValue("@DONGIA", decimal.Parse(txtGIAPHONG.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@MANV", maNhanVien);
                    cmd.Parameters.AddWithValue("@NGAYKY", dateNGAYKY.Value);
                    cmd.Parameters.AddWithValue("@TONGTIEN", decimal.Parse(txtTONGTIEN.Text.Replace(",", "")));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    MessageBox.Show("Mã hợp đồng đã tồn tại! Hệ thống sẽ tạo mã mới.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtMAHD.Text = GenerateMaHD();
                    return LuuHopDong();
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Tạo mã hợp đồng tự động HD000001, HD000002,...
        /// </summary>
        /// <summary>
        /// Tạo mã hợp đồng tự động HD000001, HD000002,...
        /// </summary>
        private string GenerateMaHD()
        {
            try
            {
                // Đảm bảo connection mở
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                // Query lấy mã hợp đồng lớn nhất có dạng HD + 6 chữ số
                string query = @"
            SELECT TOP 1 MAHD 
            FROM HOPDONG 
            WHERE MAHD LIKE 'HD[0-9][0-9][0-9][0-9][0-9][0-9]'
            ORDER BY CAST(SUBSTRING(MAHD, 3, 6) AS INT) DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string lastMaHD = result.ToString();

                        // Debug: Kiểm tra giá trị lấy được
                        System.Diagnostics.Debug.WriteLine($"Last MAHD: {lastMaHD}");

                        // Lấy phần số (6 ký tự cuối)
                        if (lastMaHD.Length >= 8) // HD + 6 số
                        {
                            string numberPart = lastMaHD.Substring(2); // Bỏ "HD"

                            if (int.TryParse(numberPart, out int lastNumber))
                            {
                                int newNumber = lastNumber + 1;
                                string newMaHD = "HD" + newNumber.ToString("D6");

                                // Debug
                                System.Diagnostics.Debug.WriteLine($"Generated MAHD: {newMaHD}");

                                return newMaHD;
                            }
                        }
                    }

                    // Nếu chưa có hợp đồng nào hoặc parse lỗi, bắt đầu từ HD000001
                    System.Diagnostics.Debug.WriteLine("No existing MAHD found, starting from HD000001");
                    return "HD000001";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã hợp đồng: " + ex.Message + "\n\n" + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fallback: dùng timestamp
                return "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}