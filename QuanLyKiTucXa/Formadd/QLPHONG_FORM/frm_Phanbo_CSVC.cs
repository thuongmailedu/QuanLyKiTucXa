using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class frm_Phanbo_CSVC : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private string maNha = "";
        private string maCSVC = "";
        private bool isEditMode = false;

        // Constructor cho chế độ thêm mới
        public frm_Phanbo_CSVC()
        {
            InitializeComponent();
            isEditMode = false;
            this.Text = "Phân bổ cơ sở vật chất";
        }

        // Constructor cho chế độ sửa
        public frm_Phanbo_CSVC(string maNha, string maCSVC)
        {
            InitializeComponent();
            this.maNha = maNha;
            this.maCSVC = maCSVC;
            isEditMode = true;
            this.Text = "Sửa phân bổ cơ sở vật chất";
        }

        private void frm_Phanbo_CSVC_Load(object sender, EventArgs e)
        {
            LoadComboBoxNha();
            LoadComboBoxCSVC();

            // Set ReadOnly cho các textbox thông tin
            txtTEN_CSVC.ReadOnly = true;
            txtTEN_NHACC.ReadOnly = true;
            txtCHITIET.ReadOnly = true; // Chi tiết chỉ đọc

            if (isEditMode)
            {
                comNHA.Enabled = false;
                comMA_CSVC.Enabled = false;
                LoadPhanBoInfo();
            }
            else
            {
                txtTEN_CSVC.Text = "";
                txtTEN_NHACC.Text = "";
                txtCHITIET.Text = "";
                txtSOLUONG.Text = "";
            }
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
                MessageBox.Show("Lỗi tải danh sách nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxCSVC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // THÊM CỘT CHITIET VÀO QUERY
                    string query = @"SELECT 
                                        DM.MA_CSVC,
                                        DM.TEN_CSVC,
                                        ISNULL(NC.TEN_NHACC, N'') AS TEN_NHACC,
                                        ISNULL(DM.CHITIET, N'') AS CHITIET
                                    FROM DM_CSVC DM
                                    LEFT JOIN NHACC NC ON DM.MA_NHACC = NC.MA_NHACC
                                    WHERE DM.TRANGTHAI = N'Áp dụng'
                                    ORDER BY DM.MA_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        comMA_CSVC.DataSource = dt;
                        comMA_CSVC.DisplayMember = "MA_CSVC";
                        comMA_CSVC.ValueMember = "MA_CSVC";
                        comMA_CSVC.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách CSVC: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ĐỔI TÊN HÀM CHO ĐÚNG
        private void comMA_CSVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comMA_CSVC.SelectedIndex >= 0)
            {
                try
                {
                    DataRowView drv = (DataRowView)comMA_CSVC.SelectedItem;
                    // Tự động điền thông tin CSVC
                    txtTEN_CSVC.Text = drv["TEN_CSVC"].ToString();
                    txtTEN_NHACC.Text = drv["TEN_NHACC"] != DBNull.Value ? drv["TEN_NHACC"].ToString() : "";
                    txtCHITIET.Text = drv["CHITIET"] != DBNull.Value ? drv["CHITIET"].ToString() : ""; // THÊM DÒNG NÀY
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // GIỮ LẠI HÀM CŨ ĐỂ TƯƠNG THÍCH (nếu có chỗ nào còn dùng)
        private void comTEN_CSVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gọi hàm mới
            comMA_CSVC_SelectedIndexChanged(sender, e);
        }

        private void LoadPhanBoInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        NC.MANHA,
                                        NC.MA_CSVC,
                                        NC.SOLUONG,
                                        NC.GHICHU,
                                        DM.TEN_CSVC,
                                        ISNULL(NCC.TEN_NHACC, N'') AS TEN_NHACC,
                                        ISNULL(DM.CHITIET, N'') AS CHITIET
                                    FROM NHA_CSVC NC
                                    INNER JOIN DM_CSVC DM ON NC.MA_CSVC = DM.MA_CSVC
                                    LEFT JOIN NHACC NCC ON DM.MA_NHACC = NCC.MA_NHACC
                                    WHERE NC.MANHA = @MANHA AND NC.MA_CSVC = @MA_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", maNha);
                        cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                comNHA.SelectedValue = reader["MANHA"].ToString();
                                comMA_CSVC.SelectedValue = reader["MA_CSVC"].ToString();
                                txtTEN_CSVC.Text = reader["TEN_CSVC"].ToString();
                                txtTEN_NHACC.Text = reader["TEN_NHACC"].ToString();
                                txtCHITIET.Text = reader["CHITIET"].ToString(); // THÊM DÒNG NÀY
                                txtSOLUONG.Text = reader["SOLUONG"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin phân bổ: " + ex.Message, "Lỗi",
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

            if (comMA_CSVC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn cơ sở vật chất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comMA_CSVC.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSOLUONG.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSOLUONG.Focus();
                return false;
            }

            int soLuong;
            if (!int.TryParse(txtSOLUONG.Text, out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSOLUONG.Focus();
                return false;
            }

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (isEditMode)
                    {
                        // Chế độ sửa: chỉ cập nhật số lượng
                        query = @"UPDATE NHA_CSVC 
                                SET SOLUONG = @SOLUONG
                                WHERE MANHA = @MANHA AND MA_CSVC = @MA_CSVC";
                    }
                    else
                    {
                        // Kiểm tra xem đã tồn tại chưa
                        string checkQuery = "SELECT COUNT(*) FROM NHA_CSVC WHERE MANHA = @MANHA AND MA_CSVC = @MA_CSVC";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@MANHA", comNHA.SelectedValue.ToString());
                            checkCmd.Parameters.AddWithValue("@MA_CSVC", comMA_CSVC.SelectedValue.ToString());

                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show("Cơ sở vật chất này đã được phân bổ cho nhà này rồi!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        query = @"INSERT INTO NHA_CSVC (MANHA, MA_CSVC, SOLUONG, GHICHU)
                                VALUES (@MANHA, @MA_CSVC, @SOLUONG, NULL)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (isEditMode)
                        {
                            cmd.Parameters.AddWithValue("@MANHA", maNha);
                            cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MANHA", comNHA.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_CSVC", comMA_CSVC.SelectedValue.ToString());
                        }

                        cmd.Parameters.AddWithValue("@SOLUONG", int.Parse(txtSOLUONG.Text.Trim()));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
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

        private void txtSOLUONG_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMA_CSVC_TextChanged(object sender, EventArgs e)
        {
            // Không làm gì
        }
    }
}