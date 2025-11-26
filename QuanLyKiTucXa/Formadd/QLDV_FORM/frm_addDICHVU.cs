using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    public partial class frm_addDICHVU : Form
    {
        private string constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMADV = "";

        public frm_addDICHVU()
        {
            InitializeComponent();
        }

        // Constructor cho chế độ sửa
        public frm_addDICHVU(string maDV)
        {
            InitializeComponent();
            isEditMode = true;
            editingMADV = maDV;
            this.Text = "Cập nhật thông tin dịch vụ";
        }

        private void frm_addDICHVU_Load(object sender, EventArgs e)
        {
            // Load ComboBox nhà cung cấp
            LoadComboBoxNhaCC();

            if (isEditMode)
            {
                // Chế độ sửa: Load dữ liệu
                LoadDichVu(editingMADV);
            }
            else
            {
                // Chế độ thêm mới: Thiết lập các giá trị mặc định

                // Load ComboBox tên dịch vụ
                comTENDV.Items.Clear();
                comTENDV.Items.Add("Điện");
                comTENDV.Items.Add("Nước");
                comTENDV.Items.Add("Internet");
                comTENDV.SelectedIndex = 0;

                // Set ngày mặc định
                dtpTUNGAY.Value = DateTime.Now;
                dtpDENNGAY.Value = DateTime.Now.AddMonths(12); // Mặc định 1 năm

                // Disable txtMADV vì sẽ tự sinh
                txtMADV.Enabled = false;

                // Đăng ký event cho comTENDV
                comTENDV.SelectedIndexChanged += comTENDV_SelectedIndexChanged;
            }
        }

        // Load ComboBox Nhà cung cấp
        private void LoadComboBoxNhaCC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string query = "SELECT MA_NHACC, TEN_NHACC FROM NHACC ORDER BY TEN_NHACC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comTEN_NHACC.DataSource = dt;
                        comTEN_NHACC.DisplayMember = "TEN_NHACC";
                        comTEN_NHACC.ValueMember = "MA_NHACC";
                        comTEN_NHACC.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ⭐ Khi chọn tên dịch vụ (chỉ khi thêm mới)
        private void comTENDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDV = comTENDV.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(tenDV))
                return;

            switch (tenDV)
            {
                case "Điện":
                    txtMADV.Text = TaoMaDichVu("DIEN");
                    txtDONVI.Text = "KwH";
                    break;

                case "Nước":
                    txtMADV.Text = TaoMaDichVu("NUOC");
                    txtDONVI.Text = "Khối";
                    break;

                case "Internet":
                    txtMADV.Text = TaoMaDichVu("INT");
                    txtDONVI.Text = "Người";
                    break;
            }
        }

        // ⭐ Tự động sinh mã dịch vụ
        private string TaoMaDichVu(string prefix)
        {
            string maDV = "";
            try
            {
                string sql = $@"SELECT TOP 1 MADV 
                               FROM DICHVU 
                               WHERE MADV LIKE '{prefix}_%' 
                               ORDER BY MADV DESC";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Ví dụ: DIEN_01 -> DIEN_02
                            string maCuoi = result.ToString();
                            string[] parts = maCuoi.Split('_');
                            int soThuTu = int.Parse(parts[1]) + 1;
                            maDV = $"{prefix}_{soThuTu:D2}";
                        }
                        else
                        {
                            // Chưa có dịch vụ nào
                            maDV = $"{prefix}_01";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                maDV = $"{prefix}_{DateTime.Now:yyyyMMddHHmmss}";
            }

            return maDV;
        }

        // ⭐ Kiểm tra trùng thời gian dịch vụ
        private bool KiemTraTrungThoiGian(string tenDV, DateTime tuNgay, DateTime denNgay, string maDV_HienTai = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();

                    // Gọi stored procedure
                    string query = "EXEC sp_KiemTraTrungThoiGianDichVu @LOAI_DV, @TUNGAY, @DENNGAY, @MADV_HIENTAI";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LOAI_DV", tenDV);
                        cmd.Parameters.AddWithValue("@TUNGAY", tuNgay);
                        cmd.Parameters.AddWithValue("@DENNGAY", denNgay);
                        cmd.Parameters.AddWithValue("@MADV_HIENTAI", string.IsNullOrEmpty(maDV_HienTai) ? (object)DBNull.Value : maDV_HienTai);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int biTrung = Convert.ToInt32(reader["BiTrung"]);

                                if (biTrung == 1)
                                {
                                    // Đọc thông tin dịch vụ bị trùng
                                    string maDV = reader["MADV"].ToString();
                                    DateTime tungay = Convert.ToDateTime(reader["TUNGAY"]);
                                    DateTime denngay = reader["DENNGAY"] != DBNull.Value ?
                                        Convert.ToDateTime(reader["DENNGAY"]) : DateTime.MaxValue;

                                    MessageBox.Show(
                                        $"Dịch vụ '{tenDV}' đã tồn tại trong khoảng thời gian này!\n\n" +
                                        $"Mã DV: {maDV}\n" +
                                        $"Từ ngày: {tungay:dd/MM/yyyy}\n" +
                                        $"Đến ngày: {(denngay == DateTime.MaxValue ? "Không giới hạn" : denngay.ToString("dd/MM/yyyy"))}\n\n" +
                                        $"Vui lòng chọn khoảng thời gian khác! ",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                                    return true; // Có trùng
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra trùng thời gian: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Coi như có lỗi, không cho lưu
            }

            return false; // Không trùng
        }

        // Load dữ liệu dịch vụ khi sửa
        private void LoadDichVu(string maDV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    string query = @"SELECT MADV, TENDV, DONGIA, DONVI, TUNGAY, DENNGAY, TRANGTHAI, MA_NHACC
                                   FROM DICHVU 
                                   WHERE MADV = @MADV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MADV", maDV);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMADV.Text = reader["MADV"].ToString();
                                txtMADV.Enabled = false; // Không cho sửa mã

                                comTENDV.Text = reader["TENDV"].ToString();
                                comTENDV.Enabled = false; // Không cho đổi loại dịch vụ

                                txtDONGIA.Text = reader["DONGIA"].ToString();
                                txtDONVI.Text = reader["DONVI"] != DBNull.Value ? reader["DONVI"].ToString() : "";
                                dtpTUNGAY.Value = Convert.ToDateTime(reader["TUNGAY"]);

                                if (reader["DENNGAY"] != DBNull.Value)
                                    dtpDENNGAY.Value = Convert.ToDateTime(reader["DENNGAY"]);

                                comTRANGTHAI.Text = reader["TRANGTHAI"].ToString();

                                // Set nhà cung cấp
                                if (reader["MA_NHACC"] != DBNull.Value)
                                {
                                    comTEN_NHACC.SelectedValue = reader["MA_NHACC"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtMADV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMADV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(comTENDV.Text))
            {
                MessageBox.Show("Vui lòng chọn tên dịch vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comTENDV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDONGIA.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn giá!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDONGIA.Focus();
                return;
            }

            // ⭐ KIỂM TRA TRÙNG THỜI GIAN
            if (KiemTraTrungThoiGian(comTENDV.Text, dtpTUNGAY.Value, dtpDENNGAY.Value, isEditMode ? editingMADV : null))
            {
                return; // Có trùng, không cho lưu
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();

                    if (isEditMode)
                    {
                        // Cập nhật - Bổ sung MA_NHACC
                        string query = @"UPDATE DICHVU 
                                       SET TENDV = @TENDV,
                                           DONGIA = @DONGIA,
                                           DONVI = @DONVI,
                                           TUNGAY = @TUNGAY,
                                           DENNGAY = @DENNGAY,
                                           TRANGTHAI = @TRANGTHAI,
                                           MA_NHACC = @MA_NHACC
                                       WHERE MADV = @MADV";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MADV", txtMADV.Text.Trim());
                            cmd.Parameters.AddWithValue("@TENDV", comTENDV.Text.Trim());
                            cmd.Parameters.AddWithValue("@DONGIA", decimal.Parse(txtDONGIA.Text.Trim()));
                            cmd.Parameters.AddWithValue("@DONVI", string.IsNullOrEmpty(txtDONVI.Text) ? (object)DBNull.Value : txtDONVI.Text.Trim());
                            cmd.Parameters.AddWithValue("@TUNGAY", dtpTUNGAY.Value);
                            cmd.Parameters.AddWithValue("@DENNGAY", dtpDENNGAY.Value);
                            cmd.Parameters.AddWithValue("@TRANGTHAI", comTRANGTHAI.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_NHACC", comTEN_NHACC.SelectedIndex != -1 ? comTEN_NHACC.SelectedValue : (object)DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Kiểm tra trùng mã
                        string checkQuery = "SELECT COUNT(*) FROM DICHVU WHERE MADV = @MADV";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@MADV", txtMADV.Text.Trim());
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Mã dịch vụ đã tồn tại!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Thêm mới - Bổ sung MA_NHACC
                        string query = @"INSERT INTO DICHVU (MADV, TENDV, DONGIA, DONVI, TUNGAY, DENNGAY, TRANGTHAI, MA_NHACC)
                                       VALUES (@MADV, @TENDV, @DONGIA, @DONVI, @TUNGAY, @DENNGAY, @TRANGTHAI, @MA_NHACC)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MADV", txtMADV.Text.Trim());
                            cmd.Parameters.AddWithValue("@TENDV", comTENDV.Text.Trim());
                            cmd.Parameters.AddWithValue("@DONGIA", decimal.Parse(txtDONGIA.Text.Trim()));
                            cmd.Parameters.AddWithValue("@DONVI", string.IsNullOrEmpty(txtDONVI.Text) ? (object)DBNull.Value : txtDONVI.Text.Trim());
                            cmd.Parameters.AddWithValue("@TUNGAY", dtpTUNGAY.Value);
                            cmd.Parameters.AddWithValue("@DENNGAY", dtpDENNGAY.Value);
                            cmd.Parameters.AddWithValue("@TRANGTHAI", comTRANGTHAI.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_NHACC", comTEN_NHACC.SelectedIndex != -1 ? comTEN_NHACC.SelectedValue : (object)DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Thêm dịch vụ mới thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}