using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.DM_KHAC_FORM
{
    public partial class frmadd_NHACC : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMA_NHACC = "";
        private DataTable dtNhaCC = new DataTable();

        public frmadd_NHACC()
        {
            InitializeComponent();
            InitializeDataTable();
        }

        // Constructor cho chế độ sửa
        public frmadd_NHACC(string maNhaCC)
        {
            InitializeComponent();
            InitializeDataTable();

            isEditMode = true;
            editingMA_NHACC = maNhaCC;
            LoadSingleNhaCC(maNhaCC);

            this.Text = "Cập nhật thông tin nhà cung cấp";
        }

        private void frmadd_NHACC_Load(object sender, EventArgs e)
        {
            dgvadd_NHACC.DataSource = dtNhaCC;
            SetupGridView();
        }

        private void InitializeDataTable()
        {
            dtNhaCC.Columns.Add("STT", typeof(int));
            dtNhaCC.Columns.Add("MA_NHACC", typeof(string));
            dtNhaCC.Columns.Add("TEN_NHACC", typeof(string));
            dtNhaCC.Columns.Add("SDT", typeof(string));
            dtNhaCC.Columns.Add("DIACHI", typeof(string));
            dtNhaCC.Columns.Add("GHICHU", typeof(string));
        }

        private void SetupGridView()
        {
            if (dgvadd_NHACC.Columns["STT"] != null)
            {
                dgvadd_NHACC.Columns["STT"].HeaderText = "STT";
                dgvadd_NHACC.Columns["STT"].Width = 50;
            }

            if (dgvadd_NHACC.Columns["MA_NHACC"] != null)
                dgvadd_NHACC.Columns["MA_NHACC"].HeaderText = "Mã NCC";

            if (dgvadd_NHACC.Columns["TEN_NHACC"] != null)
                dgvadd_NHACC.Columns["TEN_NHACC"].HeaderText = "Tên nhà cung cấp";

            if (dgvadd_NHACC.Columns["SDT"] != null)
                dgvadd_NHACC.Columns["SDT"].HeaderText = "Số điện thoại";

            if (dgvadd_NHACC.Columns["DIACHI"] != null)
                dgvadd_NHACC.Columns["DIACHI"].HeaderText = "Địa chỉ";

            if (dgvadd_NHACC.Columns["GHICHU"] != null)
                dgvadd_NHACC.Columns["GHICHU"].HeaderText = "Ghi chú";
        }

        private void LoadSingleNhaCC(string maNhaCC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MA_NHACC, TEN_NHACC, SDT, DIACHI, GHICHU FROM NHACC WHERE MA_NHACC = @MA_NHACC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_NHACC", maNhaCC);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DataRow row = dtNhaCC.NewRow();
                                row["STT"] = 1;
                                row["MA_NHACC"] = reader["MA_NHACC"].ToString();
                                row["TEN_NHACC"] = reader["TEN_NHACC"].ToString();
                                row["SDT"] = reader["SDT"] != DBNull.Value ? reader["SDT"].ToString() : "";
                                row["DIACHI"] = reader["DIACHI"] != DBNull.Value ? reader["DIACHI"].ToString() : "";
                                row["GHICHU"] = reader["GHICHU"] != DBNull.Value ? reader["GHICHU"].ToString() : "";
                                dtNhaCC.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddtemp_NHACC_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtMA_NHACC.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMA_NHACC.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTEN_NHACC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTEN_NHACC.Focus();
                return;
            }

            // Nếu ở chế độ sửa và lưới đã có dữ liệu (1 dòng)
            if (isEditMode && dtNhaCC.Rows.Count > 0)
            {
                // Ghi đè lên dòng đầu tiên
                dtNhaCC.Rows[0]["MA_NHACC"] = txtMA_NHACC.Text.Trim();
                dtNhaCC.Rows[0]["TEN_NHACC"] = txtTEN_NHACC.Text.Trim();
                dtNhaCC.Rows[0]["SDT"] = txtSDT.Text.Trim();
                dtNhaCC.Rows[0]["DIACHI"] = txtDIACHI.Text.Trim();
                dtNhaCC.Rows[0]["GHICHU"] = txtGHICHU.Text.Trim();

                MessageBox.Show("Đã cập nhật thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear textbox
                ClearTextBoxes();
                return;
            }

            // Kiểm tra trùng trong lưới (chỉ khi thêm mới)
            foreach (DataRow row in dtNhaCC.Rows)
            {
                if (row["MA_NHACC"].ToString() == txtMA_NHACC.Text.Trim())
                {
                    MessageBox.Show("Mã nhà cung cấp đã tồn tại trong danh sách!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm vào lưới
            DataRow newRow = dtNhaCC.NewRow();
            newRow["STT"] = dtNhaCC.Rows.Count + 1;
            newRow["MA_NHACC"] = txtMA_NHACC.Text.Trim();
            newRow["TEN_NHACC"] = txtTEN_NHACC.Text.Trim();
            newRow["SDT"] = txtSDT.Text.Trim();
            newRow["DIACHI"] = txtDIACHI.Text.Trim();
            newRow["GHICHU"] = txtGHICHU.Text.Trim();
            dtNhaCC.Rows.Add(newRow);

            // Cập nhật lại STT
            UpdateSTT();

            // Clear textbox
            ClearTextBoxes();
        }

        private void btnedittemp_NHACC_Click(object sender, EventArgs e)
        {
            if (dgvadd_NHACC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMA_NHACC.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMA_NHACC.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTEN_NHACC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTEN_NHACC.Focus();
                return;
            }

            DataGridViewRow selectedRow = dgvadd_NHACC.SelectedRows[0];
            int rowIndex = selectedRow.Index;
            string oldMA_NHACC = dtNhaCC.Rows[rowIndex]["MA_NHACC"].ToString();

            // Kiểm tra trùng mã (nếu đổi mã)
            if (txtMA_NHACC.Text.Trim() != oldMA_NHACC)
            {
                foreach (DataRow row in dtNhaCC.Rows)
                {
                    if (row["MA_NHACC"].ToString() == txtMA_NHACC.Text.Trim())
                    {
                        MessageBox.Show("Mã nhà cung cấp đã tồn tại trong danh sách!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Cập nhật
            dtNhaCC.Rows[rowIndex]["MA_NHACC"] = txtMA_NHACC.Text.Trim();
            dtNhaCC.Rows[rowIndex]["TEN_NHACC"] = txtTEN_NHACC.Text.Trim();
            dtNhaCC.Rows[rowIndex]["SDT"] = txtSDT.Text.Trim();
            dtNhaCC.Rows[rowIndex]["DIACHI"] = txtDIACHI.Text.Trim();
            dtNhaCC.Rows[rowIndex]["GHICHU"] = txtGHICHU.Text.Trim();

            MessageBox.Show("Đã cập nhật thông tin!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear textbox
            ClearTextBoxes();
        }

        private void btndeletetemp_NHACC_Click(object sender, EventArgs e)
        {
            if (dgvadd_NHACC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nhà cung cấp này khỏi danh sách?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DataGridViewRow selectedRow = dgvadd_NHACC.SelectedRows[0];
                int rowIndex = selectedRow.Index;
                dtNhaCC.Rows.RemoveAt(rowIndex);

                // Cập nhật lại STT
                UpdateSTT();

                // Clear textbox
                ClearTextBoxes();
            }
        }

        private void dgvadd_NHACC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvadd_NHACC.Rows[e.RowIndex];
                txtMA_NHACC.Text = row.Cells["MA_NHACC"].Value.ToString();
                txtTEN_NHACC.Text = row.Cells["TEN_NHACC"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString() ?? "";
                txtDIACHI.Text = row.Cells["DIACHI"].Value?.ToString() ?? "";
                txtGHICHU.Text = row.Cells["GHICHU"].Value?.ToString() ?? "";
            }
        }

        private void UpdateSTT()
        {
            for (int i = 0; i < dtNhaCC.Rows.Count; i++)
            {
                dtNhaCC.Rows[i]["STT"] = i + 1;
            }
        }

        private void ClearTextBoxes()
        {
            txtMA_NHACC.Clear();
            txtTEN_NHACC.Clear();
            txtSDT.Clear();
            txtDIACHI.Clear();
            txtGHICHU.Clear();
            txtMA_NHACC.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtNhaCC.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách nhà cung cấp trống! Vui lòng thêm ít nhất một nhà cung cấp.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        if (isEditMode)
                        {
                            // Chế độ sửa
                            if (dtNhaCC.Rows.Count != 1)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Chế độ sửa chỉ cho phép cập nhật một nhà cung cấp!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            DataRow row = dtNhaCC.Rows[0];
                            string query = @"UPDATE NHACC 
                                           SET MA_NHACC = @MA_NHACC_NEW,
                                               TEN_NHACC = @TEN_NHACC,
                                               SDT = @SDT,
                                               DIACHI = @DIACHI,
                                               GHICHU = @GHICHU
                                           WHERE MA_NHACC = @MA_NHACC_OLD";

                            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MA_NHACC_NEW", row["MA_NHACC"].ToString());
                                cmd.Parameters.AddWithValue("@TEN_NHACC", row["TEN_NHACC"].ToString());
                                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(row["SDT"].ToString()) ? (object)DBNull.Value : row["SDT"].ToString());
                                cmd.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(row["DIACHI"].ToString()) ? (object)DBNull.Value : row["DIACHI"].ToString());
                                cmd.Parameters.AddWithValue("@GHICHU", string.IsNullOrEmpty(row["GHICHU"].ToString()) ? (object)DBNull.Value : row["GHICHU"].ToString());
                                cmd.Parameters.AddWithValue("@MA_NHACC_OLD", editingMA_NHACC);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Chế độ thêm mới
                            foreach (DataRow row in dtNhaCC.Rows)
                            {
                                // Kiểm tra trùng trong database
                                string checkQuery = "SELECT COUNT(*) FROM NHACC WHERE MA_NHACC = @MA_NHACC";
                                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@MA_NHACC", row["MA_NHACC"].ToString());
                                    int count = (int)checkCmd.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Mã nhà cung cấp {row["MA_NHACC"]} đã tồn tại trong database!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                // Thêm mới
                                string insertQuery = @"INSERT INTO NHACC (MA_NHACC, TEN_NHACC, SDT, DIACHI, GHICHU)
                                                     VALUES (@MA_NHACC, @TEN_NHACC, @SDT, @DIACHI, @GHICHU)";

                                using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MA_NHACC", row["MA_NHACC"].ToString());
                                    cmd.Parameters.AddWithValue("@TEN_NHACC", row["TEN_NHACC"].ToString());
                                    cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(row["SDT"].ToString()) ? (object)DBNull.Value : row["SDT"].ToString());
                                    cmd.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(row["DIACHI"].ToString()) ? (object)DBNull.Value : row["DIACHI"].ToString());
                                    cmd.Parameters.AddWithValue("@GHICHU", string.IsNullOrEmpty(row["GHICHU"].ToString()) ? (object)DBNull.Value : row["GHICHU"].ToString());
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show($"Thêm mới thành công {dtNhaCC.Rows.Count} nhà cung cấp!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
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
    }
}