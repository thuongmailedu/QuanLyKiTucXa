using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class frm_SCCSVC : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private string maSCCSVC = "";
        private bool isEditMode = false;
        private DateTime? ngayHoanThanh = null;

        // Constructor cho chế độ thêm mới
        public frm_SCCSVC()
        {
            InitializeComponent();
            isEditMode = false;
            this.Text = "Thêm yêu cầu sửa chữa CSVC";
        }

        // Constructor cho chế độ sửa
        public frm_SCCSVC(string maSCCSVC)
        {
            InitializeComponent();
            this.maSCCSVC = maSCCSVC;
            isEditMode = true;
            this.Text = "Sửa yêu cầu sửa chữa CSVC";
        }

        private void frm_SCCSVC_Load(object sender, EventArgs e)
        {
            LoadComboBoxTrangThai();
            LoadComboBoxNha();

            // Thiết lập DateTimePicker cho phép NULL
            dtpNGAY_HOANTHANH.ShowCheckBox = true; // Hiển thị checkbox để bật/tắt
            dtpNGAY_HOANTHANH.Checked = false; // Mặc định không chọn (NULL)

            // ✅ Load thông tin nhân viên từ UserSession
            LoadNhanVienInfo();

            if (isEditMode)
            {
                txtMA_SCCSVC.ReadOnly = true;
                comNHA.Enabled = false;
                comPHONG.Enabled = false;
                LoadYeuCauInfo();
            }
            else
            {
                txtMA_SCCSVC.Text = GenerateNewMASCCSVC();
                txtMA_SCCSVC.ReadOnly = true;
                dtpNGAY_YEUCAU.Value = DateTime.Now;
                comTRANGTHAI.SelectedIndex = 0;
            }
        }

        // ✅ THÊM:  Load thông tin nhân viên từ UserSession
        private void LoadNhanVienInfo()
        {
            try
            {
                // Lấy MANV từ UserSession (TenDangNhap chính là MANV)
                string maNV = UserSession.TenDangNhap;

                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Không tìm thấy thông tin đăng nhập!\nVui lòng đăng nhập lại.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Điền MANV vào textbox
                txtMANV.Text = maNV;
                txtMANV.ReadOnly = true; // Không cho phép sửa

                // Truy vấn để lấy TENNV từ database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TENNV FROM NHANVIEN WHERE MANV = @MANV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANV", maNV);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            txtTENNV.Text = result.ToString();
                            txtTENNV.ReadOnly = true; // Không cho phép sửa
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy thông tin nhân viên với mã {maNV}!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtTENNV.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin nhân viên:  " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxTrangThai()
        {
            comTRANGTHAI.Items.Clear();
            comTRANGTHAI.Items.Add("Đã tiếp nhận");
            comTRANGTHAI.Items.Add("Đang xử lý");
            comTRANGTHAI.Items.Add("Hoàn thành");
        }

        private void LoadComboBoxNha()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MANHA FROM NHA ORDER BY MANHA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        comNHA.DataSource = dt;
                        comNHA.DisplayMember = "MANHA";
                        comNHA.ValueMember = "MANHA";
                        comNHA.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhà:  " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedIndex >= 0 && !isEditMode)
            {
                LoadComboBoxPhong();
                LoadDanhMucCSVCTheoNha();
            }
        }

        private void LoadComboBoxPhong()
        {
            try
            {
                if (comNHA.SelectedValue == null)
                    return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MA_PHONG FROM PHONG WHERE MANHA = @MANHA ORDER BY MA_PHONG";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", comNHA.SelectedValue.ToString());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        comPHONG.DataSource = dt;
                        comPHONG.DisplayMember = "MA_PHONG";
                        comPHONG.ValueMember = "MA_PHONG";
                        comPHONG.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhMucCSVCTheoNha()
        {
            try
            {
                if (comNHA.SelectedValue == null)
                    return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        NC.MA_CSVC,
                                        DM.TEN_CSVC,
                                        ISNULL(NCC.TEN_NHACC, N'') AS TEN_NHACC,
                                        NC.SOLUONG,
                                        ISNULL(DM. CHITIET, N'') AS CHITIET
                                    FROM NHA_CSVC NC
                                    INNER JOIN DM_CSVC DM ON NC.MA_CSVC = DM.MA_CSVC
                                    LEFT JOIN NHACC NCC ON DM.MA_NHACC = NCC.MA_NHACC
                                    WHERE NC.MANHA = @MANHA
                                    ORDER BY DM.TEN_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", comNHA.SelectedValue.ToString());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Thêm cột STT
                        dt.Columns.Add("STT", typeof(int));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["STT"] = i + 1;
                        }

                        dgvDM_CSVC.AutoGenerateColumns = false;
                        dgvDM_CSVC.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCSVCDuocChon(string maNha, string maCSVC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        NC.MA_CSVC,
                                        DM.TEN_CSVC,
                                        ISNULL(NCC.TEN_NHACC, N'') AS TEN_NHACC,
                                        NC.SOLUONG,
                                        ISNULL(DM.CHITIET, N'') AS CHITIET
                                    FROM NHA_CSVC NC
                                    INNER JOIN DM_CSVC DM ON NC.MA_CSVC = DM.MA_CSVC
                                    LEFT JOIN NHACC NCC ON DM.MA_NHACC = NCC.MA_NHACC
                                    WHERE NC.MANHA = @MANHA AND NC.MA_CSVC = @MA_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", maNha);
                        cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dt.Columns.Add("STT", typeof(int));
                        if (dt.Rows.Count > 0)
                        {
                            dt.Rows[0]["STT"] = 1;
                        }

                        dgvDM_CSVC.AutoGenerateColumns = false;
                        dgvDM_CSVC.DataSource = dt;

                        if (dgvDM_CSVC.Rows.Count > 0)
                        {
                            dgvDM_CSVC.Rows[0].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateNewMASCCSVC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP 1 MA_SCCSVC 
                                   FROM SC_CSVC 
                                   WHERE MA_SCCSVC LIKE 'SC%' 
                                   ORDER BY MA_SCCSVC DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string lastCode = result.ToString();
                            string numberPart = lastCode.Substring(2);
                            int number = int.Parse(numberPart);
                            return "SC" + (number + 1).ToString("D6");
                        }
                        else
                        {
                            return "SC000001";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sinh mã sửa chữa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "SC000001";
            }
        }

        // ✅ CẬP NHẬT: Load thông tin yêu cầu kèm MANV và TENNV
        private void LoadYeuCauInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        SC.MA_SCCSVC,
                                        SC.MA_PHONG,
                                        P. MANHA,
                                        SC.MA_CSVC,
                                        SC.NGAY_YEUCAU,
                                        SC.NGAY_HOANTHANH,
                                        SC.CHITIET,
                                        SC.TRANGTHAI,
                                        SC.MANV,
                                        NV. TENNV
                                    FROM SC_CSVC SC
                                    INNER JOIN PHONG P ON SC.MA_PHONG = P.MA_PHONG
                                    LEFT JOIN NHANVIEN NV ON SC. MANV = NV.MANV
                                    WHERE SC.MA_SCCSVC = @MA_SCCSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_SCCSVC", maSCCSVC);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMA_SCCSVC.Text = reader["MA_SCCSVC"].ToString();
                                string maNha = reader["MANHA"].ToString();
                                string maCSVC = reader["MA_CSVC"].ToString();

                                comNHA.SelectedValue = maNha;
                                LoadComboBoxPhong();
                                LoadCSVCDuocChon(maNha, maCSVC);

                                comPHONG.SelectedValue = reader["MA_PHONG"].ToString();
                                dtpNGAY_YEUCAU.Value = Convert.ToDateTime(reader["NGAY_YEUCAU"]);

                                // Xử lý NGAY_HOANTHANH nullable
                                if (reader["NGAY_HOANTHANH"] != DBNull.Value)
                                {
                                    dtpNGAY_HOANTHANH.Checked = true;
                                    dtpNGAY_HOANTHANH.Value = Convert.ToDateTime(reader["NGAY_HOANTHANH"]);
                                }
                                else
                                {
                                    dtpNGAY_HOANTHANH.Checked = false;
                                }

                                txtCHITIET.Text = reader["CHITIET"]?.ToString() ?? "";
                                comTRANGTHAI.SelectedItem = reader["TRANGTHAI"].ToString();

                                // ✅ Load thông tin nhân viên từ database (khi sửa)
                                if (reader["MANV"] != DBNull.Value)
                                {
                                    txtMANV.Text = reader["MANV"].ToString();
                                    txtTENNV.Text = reader["TENNV"]?.ToString() ?? "";
                                }
                                else
                                {
                                    // Nếu chưa có MANV, lấy từ UserSession
                                    txtMANV.Text = UserSession.TenDangNhap;
                                    // Load TENNV
                                    LoadNhanVienInfo();
                                }

                                txtMANV.ReadOnly = true;
                                txtTENNV.ReadOnly = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin yêu cầu: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (comNHA.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comNHA.Focus();
                return false;
            }

            if (comPHONG.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comPHONG.Focus();
                return false;
            }

            if (dgvDM_CSVC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn cơ sở vật chất cần sửa chữa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (comTRANGTHAI.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comTRANGTHAI.Focus();
                return false;
            }

            if (dtpNGAY_HOANTHANH.Checked && dtpNGAY_YEUCAU.Value > dtpNGAY_HOANTHANH.Value)
            {
                MessageBox.Show("Ngày hoàn thành phải sau ngày yêu cầu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNGAY_HOANTHANH.Focus();
                return false;
            }

            // ✅ THÊM:  Kiểm tra MANV
            if (string.IsNullOrWhiteSpace(txtMANV.Text))
            {
                MessageBox.Show("Không tìm thấy mã nhân viên!\nVui lòng đăng nhập lại.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ✅ CẬP NHẬT: Lưu MANV vào database
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                string maCSVC = dgvDM_CSVC.SelectedRows[0].Cells["MA_CSVC"].Value?.ToString();
                string maNV = txtMANV.Text.Trim(); // ✅ Lấy MANV từ textbox

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (isEditMode)
                    {
                        // ✅ Khi sửa, cập nhật cả MANV
                        query = @"UPDATE SC_CSVC 
                                SET NGAY_HOANTHANH = @NGAY_HOANTHANH,
                                    CHITIET = @CHITIET,
                                    TRANGTHAI = @TRANGTHAI,
                                    MANV = @MANV
                                WHERE MA_SCCSVC = @MA_SCCSVC";
                    }
                    else
                    {
                        // ✅ Khi thêm, insert MANV
                        query = @"INSERT INTO SC_CSVC (MA_SCCSVC, MA_PHONG, MA_CSVC, NGAY_YEUCAU, NGAY_HOANTHANH, CHITIET, TRANGTHAI, MANV)
                                VALUES (@MA_SCCSVC, @MA_PHONG, @MA_CSVC, @NGAY_YEUCAU, @NGAY_HOANTHANH, @CHITIET, @TRANGTHAI, @MANV)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_SCCSVC", txtMA_SCCSVC.Text.Trim());

                        if (!isEditMode)
                        {
                            cmd.Parameters.AddWithValue("@MA_PHONG", comPHONG.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);
                            cmd.Parameters.AddWithValue("@NGAY_YEUCAU", dtpNGAY_YEUCAU.Value);
                        }

                        // Xử lý ngày hoàn thành nullable
                        if (dtpNGAY_HOANTHANH.Checked)
                        {
                            cmd.Parameters.AddWithValue("@NGAY_HOANTHANH", dtpNGAY_HOANTHANH.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@NGAY_HOANTHANH", DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@CHITIET",
                            string.IsNullOrWhiteSpace(txtCHITIET.Text) ? (object)DBNull.Value : txtCHITIET.Text.Trim());
                        cmd.Parameters.AddWithValue("@TRANGTHAI", comTRANGTHAI.SelectedItem.ToString());

                        // ✅ Thêm parameter MANV
                        cmd.Parameters.AddWithValue("@MANV", maNV);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show(isEditMode ? "Cập nhật yêu cầu sửa chữa thành công!" : "Thêm yêu cầu sửa chữa thành công!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không thể lưu dữ liệu!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}