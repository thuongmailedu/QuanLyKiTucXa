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

            txtMAHD.BackColor = System.Drawing.SystemColors.Control;
            txtTENHD.BackColor = System.Drawing.SystemColors.Control;
            txtTENSV.BackColor = System.Drawing.SystemColors.Control;
            txtGIOITINH.BackColor = System.Drawing.SystemColors.Control;
            txtMA_PHONG.BackColor = System.Drawing.SystemColors.Control;
            txtMANHA.BackColor = System.Drawing.SystemColors.Control;
            txtLOAIPHONG.BackColor = System.Drawing.SystemColors.Control;
            txtTENNV.BackColor = System.Drawing.SystemColors.Control;
            txtGIAPHONG.BackColor = System.Drawing.SystemColors.Control;

            // Disable các DateTimePicker trừ dtpNGAYKTTT
            dtpTUNGAY.Enabled = false;
            dtpDENNGAY.Enabled = false;
            dtpNGAYKY.Enabled = false;

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

                // Đăng ký sự kiện KeyDown cho txtMASV
                txtMASV.KeyDown += txtMASV_KeyDown;
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

                    // Lấy hợp đồng mới nhất của sinh viên (chưa có NGAYKTTT)
                    string query = @"SELECT TOP 1
                                        hd.MAHD, 
                                        hd. TENHD, 
                                        sv.MASV, 
                                        sv.TENSV, 
                                        sv.GIOITINH, 
                                        p.MA_PHONG, 
                                        n.MANHA, 
                                        n. LOAIPHONG,
                                        n.GIAPHONG,
                                        hd.TUNGAY, 
                                        hd.DENNGAY,
                                        hd.NGAYKTTT, 
                                        hd. NGAYKY,
                                        nv. TENNV
                                     FROM SINHVIEN sv 
                                     INNER JOIN HOPDONG hd ON sv. MASV = hd. MASV
                                     INNER JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                                     INNER JOIN NHA n ON p.MANHA = n.MANHA
                                     INNER JOIN NHANVIEN nv ON nv.MANV = hd.MANV
                                     WHERE sv. MASV = @MASV AND hd.NGAYKTTT IS NULL
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
                                MessageBox.Show($"Không tìm thấy hợp đồng đang có hiệu lực của sinh viên {maSV}!",
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
                                        hd.NGAYKY,
                                        nv.TENNV
                                     FROM HOPDONG hd
                                     INNER JOIN SINHVIEN sv ON hd. MASV = sv.MASV
                                     INNER JOIN PHONG p ON hd. MA_PHONG = p. MA_PHONG
                                     INNER JOIN NHA n ON p.MANHA = n. MANHA
                                     INNER JOIN NHANVIEN nv ON nv.MANV = hd.MANV
                                     WHERE hd. MAHD = @MAHD";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAHD", maHD);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FillFormData(reader);
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
            txtTENNV.Text = reader["TENNV"] != DBNull.Value ? reader["TENNV"].ToString() : "";

            // Lưu ngày bắt đầu và kết thúc để kiểm tra
            tuNgay = Convert.ToDateTime(reader["TUNGAY"]);
            denNgay = Convert.ToDateTime(reader["DENNGAY"]);

            dtpTUNGAY.Value = tuNgay;
            dtpDENNGAY.Value = denNgay;

            if (reader["NGAYKY"] != DBNull.Value)
                dtpNGAYKY.Value = Convert.ToDateTime(reader["NGAYKY"]);

            if (reader["NGAYKTTT"] != DBNull.Value)
            {
                dtpNGAYKTTT.Value = Convert.ToDateTime(reader["NGAYKTTT"]);
            }
            else
            {
                dtpNGAYKTTT.Value = DateTime.Now; // Mặc định là ngày hiện tại
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
            txtTENNV.Clear();
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

            // Kiểm tra ngày thanh lý phải nằm trong khoảng TUNGAY và DENNGAY
            DateTime ngayKTTT = dtpNGAYKTTT.Value.Date;

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

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE HOPDONG SET NGAYKTTT = @NGAYKTTT WHERE MAHD = @MAHD";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAHD", txtMAHD.Text.Trim());
                        cmd.Parameters.AddWithValue("@NGAYKTTT", ngayKTTT);

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