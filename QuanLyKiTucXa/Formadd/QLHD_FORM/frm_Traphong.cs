using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLHD_FORM
{
    public partial class frm_Traphong : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMAHD = "";
        private DateTime tuNgay;
        private DateTime denNgay;

        public frm_Traphong()
        {
            InitializeComponent();
        }

        // Constructor cho chế độ sửa
        public frm_Traphong(string maHD, string maSV)
        {
            InitializeComponent();
            isEditMode = true;
            editingMAHD = maHD;

            // Set mã SV và không cho sửa
            txtMASV.Text = maSV;
            txtMASV.ReadOnly = true;

            this.Text = "Cập nhật ngày thanh lý hợp đồng";
        }

        private void frm_Traphong_Load(object sender, EventArgs e)
        {
            // Disable tất cả textbox trừ txtMASV (khi thêm mới) và dtpNGAYKTTT
            txtMAHD.ReadOnly = true;
            txtTENHD.ReadOnly = true;
            txtTENSV.ReadOnly = true;
            txtGIOITINH.ReadOnly = true;
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtLOAIPHONG.ReadOnly = true;
            txtTENNV.ReadOnly = true;
            txtGIAPHONG.ReadOnly = true;
            txtMANV_TL.ReadOnly = true; // Không cho sửa mã nhân viên thanh lý

            txtMAHD.BackColor = System.Drawing.SystemColors.Control;
            txtTENHD.BackColor = System.Drawing.SystemColors.Control;
            txtTENSV.BackColor = System.Drawing.SystemColors.Control;
            txtGIOITINH.BackColor = System.Drawing.SystemColors.Control;
            txtMA_PHONG.BackColor = System.Drawing.SystemColors.Control;
            txtMANHA.BackColor = System.Drawing.SystemColors.Control;
            txtLOAIPHONG.BackColor = System.Drawing.SystemColors.Control;
            txtTENNV.BackColor = System.Drawing.SystemColors.Control;
            txtGIAPHONG.BackColor = System.Drawing.SystemColors.Control;
            txtMANV_TL.BackColor = System.Drawing.SystemColors.Control;

            // Disable các DateTimePicker trừ dtpNGAYKTTT và dtpNGAYKY_TL
            dtpTUNGAY.Enabled = false;
            dtpDENNGAY.Enabled = false;

            // ✅ Load thông tin nhân viên thanh lý từ UserSession ngay khi mở form
            LoadThongTinNhanVienThanhLy();

            if (isEditMode)
            {
                // Chế độ sửa: txtMASV đã được set, chỉ load dữ liệu
                txtMASV.ReadOnly = true;
                LoadHopDongByMAHD(editingMAHD);
            }
            else
            {
                // Chế độ thêm mới: Cho phép nhập MASV
                txtMASV.ReadOnly = false;

                // ✅ Set ngày ký thanh lý mặc định là ngày hiện tại
                dtpNGAYKY_TL.Value = DateTime.Now;

                // Đăng ký sự kiện KeyDown cho txtMASV
                txtMASV.KeyDown += txtMASV_KeyDown;
            }
        }

        // ✅ Load thông tin nhân viên thanh lý từ UserSession
        private void LoadThongTinNhanVienThanhLy()
        {
            try
            {
                // Lấy MANV từ UserSession. TenDangNhap
                string manvTL = UserSession.TenDangNhap;

                if (!string.IsNullOrEmpty(manvTL))
                {
                    txtMANV_TL.Text = manvTL;

                    // Lấy tên nhân viên từ database
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT TENNV FROM NHANVIEN WHERE MANV = @MANV";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MANV", manvTL);
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                txtTENNV.Text = result.ToString();
                            }
                            else
                            {
                                txtTENNV.Text = "";
                                MessageBox.Show($"Không tìm thấy thông tin nhân viên: {manvTL}",
                                    "Cảnh báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không xác định được thông tin người dùng đăng nhập! ",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin nhân viên thanh lý: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Event khi nhấn Enter trong txtMASV (chỉ khi thêm mới)
        private void txtMASV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string maSV = txtMASV.Text.Trim();
                if (!string.IsNullOrEmpty(maSV))
                {
                    LoadHopDongByMASV(maSV);
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // Load hợp đồng theo MASV (chỉ lấy hợp đồng mới nhất chưa thanh lý)
        private void LoadHopDongByMASV(string maSV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ Lấy hợp đồng mới nhất của sinh viên (chưa có NGAYKTTT)
                    string query = @"SELECT TOP 1
                                        hd.MAHD, 
                                        hd. TENHD, 
                                        sv.MASV, 
                                        sv.TENSV, 
                                        sv.GIOITINH, 
                                        p.MA_PHONG, 
                                        n.MANHA, 
                                        n.LOAIPHONG,
                                        n.GIAPHONG,
                                        hd.TUNGAY, 
                                        hd. DENNGAY,
                                        hd. NGAYKTTT, 
                                        hd. MANV_TL,
                                        hd. NGAYKY_TL
                                     FROM SINHVIEN sv 
                                     INNER JOIN HOPDONG hd ON sv.MASV = hd.MASV
                                     INNER JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                                     INNER JOIN NHA n ON p. MANHA = n.MANHA
                                     WHERE sv.MASV = @MASV AND hd.NGAYKTTT IS NULL
                                     ORDER BY hd. TUNGAY DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MASV", maSV);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FillFormData(reader);
                                txtMASV.ReadOnly = true; // Không cho sửa nữa
                            }
                            else
                            {
                                MessageBox.Show($"Không tìm thấy hợp đồng đang có hiệu lực của sinh viên {maSV}! ",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                ClearForm();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load hợp đồng theo MAHD (cho chế độ sửa)
        private void LoadHopDongByMAHD(string maHD)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ Lấy MANV_TL, NGAYKY_TL và tên nhân viên thanh lý
                    string query = @"SELECT 
                                        hd. MAHD, 
                                        hd.TENHD, 
                                        sv. MASV, 
                                        sv.TENSV, 
                                        sv.GIOITINH, 
                                        p.MA_PHONG, 
                                        n. MANHA, 
                                        n.LOAIPHONG,
                                        n. GIAPHONG,
                                        hd.TUNGAY, 
                                        hd. DENNGAY,
                                        hd.NGAYKTTT, 
                                        hd.MANV_TL,
                                        hd. NGAYKY_TL,
                                        nv_tl.TENNV AS TENNV_TL
                                     FROM HOPDONG hd
                                     INNER JOIN SINHVIEN sv ON hd. MASV = sv.MASV
                                     INNER JOIN PHONG p ON hd. MA_PHONG = p. MA_PHONG
                                     INNER JOIN NHA n ON p.MANHA = n.MANHA
                                     LEFT JOIN NHANVIEN nv_tl ON hd.MANV_TL = nv_tl. MANV
                                     WHERE hd.MAHD = @MAHD";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAHD", maHD);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FillFormData(reader);

                                // ✅ Load thông tin người thanh lý nếu đã có
                                if (reader["MANV_TL"] != DBNull.Value)
                                {
                                    txtMANV_TL.Text = reader["MANV_TL"].ToString();
                                    txtTENNV.Text = reader["TENNV_TL"] != DBNull.Value ? reader["TENNV_TL"].ToString() : "";
                                }

                                // ✅ Load ngày ký thanh lý nếu đã có
                                if (reader["NGAYKY_TL"] != DBNull.Value)
                                {
                                    dtpNGAYKY_TL.Value = Convert.ToDateTime(reader["NGAYKY_TL"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Điền dữ liệu vào form
        private void FillFormData(SqlDataReader reader)
        {
            txtMAHD.Text = reader["MAHD"].ToString();
            txtTENHD.Text = reader["TENHD"] != DBNull.Value ? reader["TENHD"].ToString() : "";
            txtMASV.Text = reader["MASV"].ToString();
            txtTENSV.Text = reader["TENSV"].ToString();
            txtGIOITINH.Text = reader["GIOITINH"] != DBNull.Value ? reader["GIOITINH"].ToString() : "";
            txtMA_PHONG.Text = reader["MA_PHONG"].ToString();
            txtMANHA.Text = reader["MANHA"].ToString();
            txtLOAIPHONG.Text = reader["LOAIPHONG"].ToString();
            txtGIAPHONG.Text = reader["GIAPHONG"] != DBNull.Value ? reader["GIAPHONG"].ToString() : "";

            // ✅ Không load TENNV từ hợp đồng cũ nữa vì đã load từ UserSession

            // Lưu ngày bắt đầu và kết thúc để kiểm tra
            tuNgay = Convert.ToDateTime(reader["TUNGAY"]);
            denNgay = Convert.ToDateTime(reader["DENNGAY"]);

            dtpTUNGAY.Value = tuNgay;
            dtpDENNGAY.Value = denNgay;

            // ✅ Load ngày thanh lý
            if (reader["NGAYKTTT"] != DBNull.Value)
            {
                dtpNGAYKTTT.Value = Convert.ToDateTime(reader["NGAYKTTT"]);
            }
            else
            {
                dtpNGAYKTTT.Value = DateTime.Now; // Mặc định là ngày hiện tại
            }

            // ✅ Load ngày ký thanh lý
            if (reader["NGAYKY_TL"] != DBNull.Value)
            {
                dtpNGAYKY_TL.Value = Convert.ToDateTime(reader["NGAYKY_TL"]);
            }
            else
            {
                dtpNGAYKY_TL.Value = DateTime.Now; // Mặc định là ngày hiện tại
            }
        }

        private void ClearForm()
        {
            txtMAHD.Clear();
            txtTENHD.Clear();
            txtTENSV.Clear();
            txtGIOITINH.Clear();
            txtMA_PHONG.Clear();
            txtMANHA.Clear();
            txtLOAIPHONG.Clear();
            txtGIAPHONG.Clear();
            // Không clear txtTENNV và txtMANV_TL vì đã load từ UserSession
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtMAHD.Text))
            {
                MessageBox.Show("Không có thông tin hợp đồng để cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra thông tin nhân viên thanh lý
            if (string.IsNullOrWhiteSpace(txtMANV_TL.Text))
            {
                MessageBox.Show("Không xác định được nhân viên thanh lý!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ngày thanh lý phải nằm trong khoảng TUNGAY và DENNGAY
            DateTime ngayKTTT = dtpNGAYKTTT.Value.Date;
            DateTime ngayKyTL = dtpNGAYKY_TL.Value.Date;

            if (ngayKTTT < tuNgay.Date)
            {
                MessageBox.Show($"Ngày thanh lý không thể trước ngày bắt đầu hợp đồng ({tuNgay:dd/MM/yyyy})!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (ngayKTTT > denNgay.Date)
            {
                MessageBox.Show($"Ngày thanh lý không thể sau ngày kết thúc hợp đồng ({denNgay:dd/MM/yyyy})!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ✅ Kiểm tra ngày ký thanh lý phải >= ngày thanh lý
            if (ngayKyTL < ngayKTTT)
            {
                MessageBox.Show($"Ngày ký biên bản thanh lý không thể trước ngày thanh lý ({ngayKTTT:dd/MM/yyyy})!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ Cập nhật NGAYKTTT, MANV_TL và NGAYKY_TL
                    string query = @"UPDATE HOPDONG 
                                    SET NGAYKTTT = @NGAYKTTT, 
                                        MANV_TL = @MANV_TL,
                                        NGAYKY_TL = @NGAYKY_TL
                                    WHERE MAHD = @MAHD";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAHD", txtMAHD.Text.Trim());
                        cmd.Parameters.AddWithValue("@NGAYKTTT", ngayKTTT);
                        cmd.Parameters.AddWithValue("@MANV_TL", txtMANV_TL.Text.Trim());
                        cmd.Parameters.AddWithValue("@NGAYKY_TL", ngayKyTL);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Cập nhật ngày thanh lý thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}