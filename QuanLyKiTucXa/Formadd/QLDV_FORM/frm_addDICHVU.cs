using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    public partial class frm_addDICHVU : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
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

            // Đăng ký event
            comTEN_NHACC.SelectedIndexChanged += comTEN_NHACC_SelectedIndexChanged;

            if (isEditMode)
            {
                LoadDichVu(editingMADV);
            }
        }

        // Load ComboBox Nhà cung cấp
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

        // Event khi chọn nhà cung cấp (nếu cần hiển thị thêm thông tin)
        private void comTEN_NHACC_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể bổ sung logic hiển thị thông tin nhà cung cấp nếu cần
        }

        // Load dữ liệu dịch vụ khi sửa
        private void LoadDichVu(string maDV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
                                txtMADV.ReadOnly = true; // Không cho sửa mã

                                comTENDV.Text = reader["TENDV"].ToString();
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
                MessageBox.Show("Vui lòng nhập tên dịch vụ!", "Thông báo",
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

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}