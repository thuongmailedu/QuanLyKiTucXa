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
        private string maNhanVien = ""; // Lưu MANV (chính là TENDN)

        public frm_addHopDong()
        {
            InitializeComponent();
            validator = new HopDongValidator(constr);
        }

        private void frm_addHopDong_Load(object sender, EventArgs e)
        {
            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();

            // Tự động điền tên hợp đồng
            txtTENHD.Text = "HỢP ĐỒNG ĐĂNG KÝ PHÒNG KTX";

            // Load thông tin nhân viên đang đăng nhập
            LoadThongTinNhanVien();

            // Đăng ký event cho txtMASV
            txtMASV.Leave += txtMASV_Leave;
        }

        /// <summary>
        /// Load thông tin nhân viên dựa trên TENDN từ UserSession
        /// </summary>
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

                // TENDN chính là MANV
                maNhanVien = UserSession.TenDangNhap;

                // Lấy tên nhân viên từ bảng NHANVIEN
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
        /// Kiểm tra và hiển thị tên sinh viên khi nhập MASV
        /// </summary>
        private void txtMASV_Leave(object sender, EventArgs e)
        {
            string maSV = txtMASV.Text.Trim();

            if (string.IsNullOrEmpty(maSV))
            {
                txtTENSV.Clear();
                return;
            }

            try
            {
                string query = "SELECT TENSV FROM SINHVIEN WHERE MASV = @MASV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MASV", maSV);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtTENSV.Text = result.ToString();
                    }
                    else
                    {
                        txtTENSV.Clear();
                        MessageBox.Show($"Sinh viên với mã {maSV} chưa tồn tại trong hệ thống!",
                            "Không tìm thấy sinh viên",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtMASV.Focus();
                        txtMASV.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm sinh viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method gọi từ UC_DANHMUCPHONG
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

            // Disable/Enable các control
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtLOAIPHONG.ReadOnly = true;
            txtGIAPHONG.ReadOnly = true;
            txtTHOIHAN.ReadOnly = true;
            txtTONGTIEN.ReadOnly = true;
            dateTUNGAY.Enabled = false;
            dateDENNGAY.Enabled = true; // Cho phép sửa DENNGAY

            // Đăng ký event để tính lại khi thay đổi DENNGAY
            dateDENNGAY.ValueChanged += DateChanged;
        }

        // Method gọi từ UC_ThuePhong
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

            // Tạo mã hợp đồng
            txtMAHD.Text = GenerateMaHD();

            // Enable/Disable controls
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtLOAIPHONG.ReadOnly = true;
            txtGIAPHONG.ReadOnly = true;
            txtTHOIHAN.ReadOnly = false;
            txtTONGTIEN.ReadOnly = true;
            dateTUNGAY.Enabled = true;
            dateDENNGAY.Enabled = true;

            // Đăng ký events
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

                // Tính số tháng
                int thang = ((dateDENNGAY.Value.Year - dateTUNGAY.Value.Year) * 12) +
                           dateDENNGAY.Value.Month - dateTUNGAY.Value.Month;

                if (dateDENNGAY.Value.Day >= dateTUNGAY.Value.Day)
                {
                    thang++;
                }

                txtTHOIHAN.Text = thang.ToString();

                // Tính tổng tiền
                if (!string.IsNullOrWhiteSpace(txtGIAPHONG.Text))
                {
                    decimal giaPhong = decimal.Parse(txtGIAPHONG.Text.Replace(",", ""));
                    decimal tongTien = giaPhong * thang;
                    txtTONGTIEN.Text = tongTien.ToString("N0");
                }
            }
            catch
            {
                // Bỏ qua lỗi
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

                // 1. Validate cơ bản
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

                // Kiểm tra có mã nhân viên không
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    MessageBox.Show("Không tìm thấy mã nhân viên! Vui lòng đăng nhập lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. SỬ DỤNG HopDongValidator
                if (validator.KiemTraHopDongTrung(maSV, tuNgay, denNgay))
                {
                    return; // Validator đã hiển thị message
                }

                if (!validator.KiemTraPhongConCho(maPhong, tuNgay, denNgay))
                {
                    return; // Validator đã hiển thị message
                }

                // 3. Kiểm tra giới tính
                if (!KiemTraGioiTinh(maSV, maPhong))
                {
                    return;
                }

                // 4. Lưu hợp đồng
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
                string query = @"
                    SELECT SV.GIOITINH AS GT_SINHVIEN, N.GIOITINH AS GT_PHONG
                    FROM SINHVIEN SV, PHONG P
                    INNER JOIN NHA N ON P.MANHA = N.MANHA
                    WHERE SV.MASV = @MASV AND P.MA_PHONG = @MA_PHONG";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MASV", maSV);
                    cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string gtSV = reader["GT_SINHVIEN"].ToString();
                            string gtPhong = reader["GT_PHONG"].ToString();

                            if (gtSV != gtPhong)
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
                MessageBox.Show("Lỗi kiểm tra giới tính: " + ex.Message);
                return false;
            }
        }

        private bool LuuHopDong()
        {
            try
            {
                string maHD = txtMAHD.Text.Trim();

                // Kiểm tra nếu mã hợp đồng trống thì tạo mới
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
                    cmd.Parameters.AddWithValue("@MANV", maNhanVien); // TENDN từ UserSession
                    cmd.Parameters.AddWithValue("@NGAYKY", dateNGAYKY.Value);
                    cmd.Parameters.AddWithValue("@TONGTIEN", decimal.Parse(txtTONGTIEN.Text.Replace(",", "")));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627) // Lỗi trùng khóa chính
                {
                    MessageBox.Show("Mã hợp đồng đã tồn tại! Hệ thống sẽ tạo mã mới.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Tạo mã mới và thử lại
                    txtMAHD.Text = GenerateMaHD();
                    return LuuHopDong(); // Đệ quy để thử lại
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
        /// Tạo mã hợp đồng tự động theo format HD000001, HD000002, ...
        /// </summary>
        private string GenerateMaHD()
        {
            try
            {
                string query = @"
                    SELECT TOP 1 MAHD 
                    FROM HOPDONG 
                    WHERE MAHD LIKE 'HD%' 
                    ORDER BY MAHD DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastMaHD = result.ToString();

                        // Lấy phần số từ mã cuối cùng (VD: HD000047 -> 47)
                        string numberPart = lastMaHD.Substring(2); // Bỏ "HD"

                        if (int.TryParse(numberPart, out int lastNumber))
                        {
                            // Tăng lên 1
                            int newNumber = lastNumber + 1;

                            // Format lại thành HD000048
                            return "HD" + newNumber.ToString("D6");
                        }
                    }

                    // Nếu chưa có hợp đồng nào, bắt đầu từ HD000001
                    return "HD000001";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã hợp đồng: " + ex.Message);
                // Fallback: dùng timestamp
                return "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}