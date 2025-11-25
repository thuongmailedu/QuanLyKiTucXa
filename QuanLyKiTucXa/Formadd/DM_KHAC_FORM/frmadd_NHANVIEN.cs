using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.DM_KHAC_FORM
{
    public partial class frmadd_NHANVIEN : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string oldMANV = "";

        public frmadd_NHANVIEN()
        {
            InitializeComponent();
            LoadComboBoxNHA();
            LoadComboBoxQUYEN();
        }

        // Constructor cho chế độ sửa
        public frmadd_NHANVIEN(string maNV, string tenNV, string maNha, string quyen)
        {
            InitializeComponent();
            LoadComboBoxNHA();
            LoadComboBoxQUYEN();

            isEditMode = true;
            oldMANV = maNV;

            txtMANV.Text = maNV;
            txtMANV.ReadOnly = true; // Không cho sửa mã NV
            txtTENDN.Text = maNV; // Tên đăng nhập = mã NV
            txtTENDN.ReadOnly = true;

            txtTENNV.Text = tenNV;
            comMANHA.SelectedValue = maNha;

            // Set quyền
            if (quyen.ToLower() == "admin")
                comQUYEN.Text = "ADMIN";
            else
                comQUYEN.Text = "Quản lý";

            // Load mật khẩu hiện tại
            LoadMatKhau(maNV);

            this.Text = "Cập nhật thông tin nhân viên";
        }

        private void frm_add_NHANVIEN_Load(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                txtTENDN.ReadOnly = true; // Luôn readonly vì lấy từ MANV
            }
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

        private void LoadComboBoxQUYEN()
        {
            comQUYEN.Items.Clear();
            comQUYEN.Items.Add("ADMIN");
            comQUYEN.Items.Add("Quản lý");
            comQUYEN.SelectedIndex = -1;
        }

        private void LoadMatKhau(string maNV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MATKHAU FROM LOGIN WHERE TENDN = @TENDN";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TENDN", maNV);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            txtMATKHAU.Text = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load mật khẩu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMANV_TextChanged(object sender, EventArgs e)
        {
            // Tự động cập nhật TENDN = MANV
            txtTENDN.Text = txtMANV.Text;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(txtMANV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMANV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTENNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENNV.Focus();
                return;
            }

            //if (comMANHA.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Vui lòng chọn nhà!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    comMANHA.Focus();
            //    return;
            //}

            if (comQUYEN.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn quyền!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comQUYEN.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMATKHAU.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMATKHAU.Focus();
                return;
            }

            try
            {
                // Chuyển đổi quyền
                string quyen = comQUYEN.Text == "ADMIN" ? "admin" : "quanly";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        if (isEditMode)
                        {
                            // Cập nhật NHANVIEN
                            string queryNV = @"UPDATE NHANVIEN 
                                             SET TENNV = @TENNV,
                                                 MANHA = @MANHA
                                             WHERE MANV = @MANV";

                            using (SqlCommand cmd = new SqlCommand(queryNV, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MANV", txtMANV.Text.Trim());
                                cmd.Parameters.AddWithValue("@TENNV", txtTENNV.Text.Trim());
                                cmd.Parameters.AddWithValue("@MANHA", comMANHA.SelectedValue.ToString());
                                cmd.ExecuteNonQuery();
                            }

                            // Cập nhật LOGIN
                            string queryLogin = @"UPDATE LOGIN 
                                                SET MATKHAU = @MATKHAU,
                                                    QUYEN = @QUYEN
                                                WHERE TENDN = @TENDN";

                            using (SqlCommand cmd = new SqlCommand(queryLogin, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TENDN", txtMANV.Text.Trim());
                                cmd.Parameters.AddWithValue("@MATKHAU", txtMATKHAU.Text.Trim());
                                cmd.Parameters.AddWithValue("@QUYEN", quyen);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            // Kiểm tra trùng mã
                            string checkQuery = "SELECT COUNT(*) FROM NHANVIEN WHERE MANV = @MANV";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@MANV", txtMANV.Text.Trim());
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Mã nhân viên đã tồn tại!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Thêm mới NHANVIEN
                            string queryNV = @"INSERT INTO NHANVIEN (MANV, TENNV, MANHA)
                                             VALUES (@MANV, @TENNV, @MANHA)";

                            using (SqlCommand cmd = new SqlCommand(queryNV, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MANV", txtMANV.Text.Trim());
                                cmd.Parameters.AddWithValue("@TENNV", txtTENNV.Text.Trim());
                                //cmd.Parameters.AddWithValue("@MANHA", comMANHA.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@MANHA", string.IsNullOrEmpty(comMANHA.Text) ? (object)DBNull.Value : comMANHA.Text);

                                cmd.ExecuteNonQuery();
                            }

                            // Thêm mới LOGIN
                            string queryLogin = @"INSERT INTO LOGIN (TENDN, MATKHAU, QUYEN)
                                                VALUES (@TENDN, @MATKHAU, @QUYEN)";

                            using (SqlCommand cmd = new SqlCommand(queryLogin, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@TENDN", txtMANV.Text.Trim());
                                cmd.Parameters.AddWithValue("@MATKHAU", txtMATKHAU.Text.Trim());
                                cmd.Parameters.AddWithValue("@QUYEN", quyen);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
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

        private void frmadd_NHANVIEN_Load(object sender, EventArgs e)
        {

        }
    }
}