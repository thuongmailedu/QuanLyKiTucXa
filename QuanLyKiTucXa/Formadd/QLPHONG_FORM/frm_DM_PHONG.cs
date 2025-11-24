using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLPHONG_FORM
{
    public partial class frm_DM_PHONG : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string oldMA_PHONG = "";

        public frm_DM_PHONG()
        {
            InitializeComponent();
            LoadComboBoxNHA();
        }

        // Constructor cho chế độ sửa
        public frm_DM_PHONG(string maPhong, string maNha)
        {
            InitializeComponent();
            LoadComboBoxNHA();

            isEditMode = true;
            oldMA_PHONG = maPhong;

            txtMA_PHONG.Text = maPhong;
            comMANHA.SelectedValue = maNha;

            this.Text = "Cập nhật thông tin phòng";
        }

        private void frm_DM_PHONG_Load(object sender, EventArgs e)
        {
            // Đăng ký sự kiện khi combobox thay đổi
            comMANHA.SelectedIndexChanged += comMANHA_SelectedIndexChanged;
        }

        private void LoadComboBoxNHA()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MANHA, MANHA + ' - ' + LOAIPHONG + ' - ' + GIOITINH AS DISPLAY_TEXT FROM NHA ORDER BY MANHA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comMANHA.DataSource = dt;
                        comMANHA.DisplayMember = "DISPLAY_TEXT";
                        comMANHA.ValueMember = "MANHA";
                        comMANHA.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comMANHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comMANHA.SelectedIndex == -1)
                return;

            try
            {
                string maNha = comMANHA.SelectedValue.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT GIOITINH, LOAIPHONG, TOIDA, GIAPHONG FROM NHA WHERE MANHA = @MANHA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", maNha);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtGIOITINH.Text = reader["GIOITINH"].ToString();
                                txtLOAIPHONG.Text = reader["LOAIPHONG"].ToString();
                                txtTOIDA.Text = reader["TOIDA"].ToString();
                                txtGIAPHONG.Text = Convert.ToDecimal(reader["GIAPHONG"]).ToString("N0");

                                // Set ReadOnly = true để không cho sửa
                                txtGIOITINH.ReadOnly = true;
                                txtLOAIPHONG.ReadOnly = true;
                                txtTOIDA.ReadOnly = true;
                                txtGIAPHONG.ReadOnly = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (comMANHA.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comMANHA.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMA_PHONG.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMA_PHONG.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (isEditMode)
                    {
                        // Kiểm tra nếu đổi mã phòng thì phải kiểm tra trùng
                        if (txtMA_PHONG.Text.Trim() != oldMA_PHONG)
                        {
                            string checkQuery = "SELECT COUNT(*) FROM PHONG WHERE MA_PHONG = @MA_PHONG";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@MA_PHONG", txtMA_PHONG.Text.Trim());
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    MessageBox.Show("Mã phòng đã tồn tại!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }

                        // Cập nhật
                        string query = @"UPDATE PHONG 
                                       SET MA_PHONG = @MA_PHONG_NEW,
                                           MANHA = @MANHA,
                                           TRANGTHAI = @TRANGTHAI
                                       WHERE MA_PHONG = @MA_PHONG_OLD";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MA_PHONG_NEW", txtMA_PHONG.Text.Trim());
                            cmd.Parameters.AddWithValue("@MANHA", comMANHA.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@TRANGTHAI", "Còn trống");
                            cmd.Parameters.AddWithValue("@MA_PHONG_OLD", oldMA_PHONG);

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Cập nhật thông tin phòng thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        // Kiểm tra trùng mã
                        string checkQuery = "SELECT COUNT(*) FROM PHONG WHERE MA_PHONG = @MA_PHONG";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@MA_PHONG", txtMA_PHONG.Text.Trim());
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Mã phòng đã tồn tại!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Thêm mới
                        string query = @"INSERT INTO PHONG (MA_PHONG, MANHA, TRANGTHAI)
                                       VALUES (@MA_PHONG, @MANHA, @TRANGTHAI)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MA_PHONG", txtMA_PHONG.Text.Trim());
                            cmd.Parameters.AddWithValue("@MANHA", comMANHA.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@TRANGTHAI", "Còn trống");

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Thêm phòng mới thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
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