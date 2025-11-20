using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class frm_DM_CSVC : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True"; // Thay bằng connection string của bạn
        private string maCSVC = "";
        private bool isEditMode = false;

        // Constructor cho chế độ thêm mới
        public frm_DM_CSVC()
        {
            InitializeComponent();
            isEditMode = false;
            this.Text = "Thêm cơ sở vật chất";
        }

        // Constructor cho chế độ sửa
        public frm_DM_CSVC(string maCSVC)
        {
            InitializeComponent();
            this.maCSVC = maCSVC;
            isEditMode = true;
            this.Text = "Sửa cơ sở vật chất";
        }

        private void frm_DM_CSVC_Load(object sender, EventArgs e)
        {
            LoadComboBoxTrangThai();
            LoadComboBoxNhaCC();

            if (isEditMode)
            {
                txtMA_CSVC.ReadOnly = true;
                LoadCSVCInfo();
            }
            else
            {
                txtMA_CSVC.Text = GenerateNewMACSVC();
                txtMA_CSVC.ReadOnly = true;
                txtTEN_CSVC.Text = txtMA_CSVC.Text; // Hiển thị tên theo mã
                comTRANGTHAI.SelectedIndex = 0; // Mặc định "Áp dụng"
            }
        }

        private void LoadComboBoxTrangThai()
        {
            comTRANGTHAI.Items.Clear();
            comTRANGTHAI.Items.Add("Áp dụng");
            comTRANGTHAI.Items.Add("Ngừng áp dụng");
        }

        private void LoadComboBoxNhaCC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MA_NHACC, TEN_NHACC FROM NHACC ORDER BY TEN_NHACC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        comNHACC.DataSource = dt;
                        comNHACC.DisplayMember = "TEN_NHACC";
                        comNHACC.ValueMember = "MA_NHACC";
                        comNHACC.SelectedIndex = -1; // Không chọn gì mặc định
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateNewMACSVC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP 1 MA_CSVC 
                                   FROM DM_CSVC 
                                   WHERE MA_CSVC LIKE 'VC%' 
                                   ORDER BY MA_CSVC DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string lastCode = result.ToString();
                            // Lấy phần số sau "VC", bỏ số 0 đầu nếu có
                            string numberPart = lastCode.Substring(2);
                            int number = int.Parse(numberPart);
                            // Tăng lên 1 và format lại
                            return "VC" + (number + 1).ToString("D2");
                        }
                        else
                        {
                            return "VC01"; // Mã đầu tiên
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sinh mã CSVC: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "VC01";
            }
        }

        private void txtMA_CSVC_TextChanged(object sender, EventArgs e)
        {
            // Tự động cập nhật tên CSVC theo mã khi thêm mới
            if (!isEditMode)
            {
                txtTEN_CSVC.Text = txtMA_CSVC.Text;
            }
        }

        private void LoadCSVCInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT MA_CSVC, TEN_CSVC, TRANGTHAI, CHITIET, MA_NHACC 
                                   FROM DM_CSVC 
                                   WHERE MA_CSVC = @MA_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMA_CSVC.Text = reader["MA_CSVC"].ToString();
                                txtTEN_CSVC.Text = reader["TEN_CSVC"].ToString();
                                comTRANGTHAI.SelectedItem = reader["TRANGTHAI"].ToString();
                                txtCHITIET.Text = reader["CHITIET"].ToString();

                                if (reader["MA_NHACC"] != DBNull.Value)
                                {
                                    comNHACC.SelectedValue = reader["MA_NHACC"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin CSVC: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTEN_CSVC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên cơ sở vật chất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTEN_CSVC.Focus();
                return false;
            }

            if (comTRANGTHAI.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comTRANGTHAI.Focus();
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
                        query = @"UPDATE DM_CSVC 
                                SET TEN_CSVC = @TEN_CSVC,
                                    TRANGTHAI = @TRANGTHAI,
                                    CHITIET = @CHITIET,
                                    MA_NHACC = @MA_NHACC
                                WHERE MA_CSVC = @MA_CSVC";
                    }
                    else
                    {
                        query = @"INSERT INTO DM_CSVC (MA_CSVC, TEN_CSVC, TRANGTHAI, CHITIET, MA_NHACC)
                                VALUES (@MA_CSVC, @TEN_CSVC, @TRANGTHAI, @CHITIET, @MA_NHACC)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_CSVC", txtMA_CSVC.Text.Trim());
                        cmd.Parameters.AddWithValue("@TEN_CSVC", txtTEN_CSVC.Text.Trim());
                        cmd.Parameters.AddWithValue("@TRANGTHAI", comTRANGTHAI.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@CHITIET",
                            string.IsNullOrWhiteSpace(txtCHITIET.Text) ? (object)DBNull.Value : txtCHITIET.Text.Trim());

                        if (comNHACC.SelectedValue != null)
                        {
                            cmd.Parameters.AddWithValue("@MA_NHACC", comNHACC.SelectedValue.ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MA_NHACC", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}