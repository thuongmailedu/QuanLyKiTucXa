using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.DM_KHAC_FORM
{
    public partial class frmadd_KHOA : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMAKHOA = "";
        private DataTable dtKhoa = new DataTable();

        public frmadd_KHOA()
        {
            InitializeComponent();
            InitializeDataTable();
            LoadExistingKhoa();
        }

        // Constructor cho chế độ sửa - Load một khoa cụ thể
        public frmadd_KHOA(string maKhoa)
        {
            InitializeComponent();
            InitializeDataTable();
            isEditMode = true;
            editingMAKHOA = maKhoa;
            LoadSingleKhoa(maKhoa);

            this.Text = "Cập nhật thông tin khoa";
        }

        private void frmadd_KHOA_Load(object sender, EventArgs e)
        {
            dgvadd_KHOA.DataSource = dtKhoa;
            SetupGridView();
        }

        private void InitializeDataTable()
        {
            dtKhoa.Columns.Add("STT", typeof(int));
            dtKhoa.Columns.Add("MAKHOA", typeof(string));
            dtKhoa.Columns.Add("TENKHOA", typeof(string));
        }

        private void SetupGridView()
        {
            if (dgvadd_KHOA.Columns["STT"] != null)
            {
                dgvadd_KHOA.Columns["STT"].HeaderText = "STT";
                dgvadd_KHOA.Columns["STT"].Width = 50;
            }

            if (dgvadd_KHOA.Columns["MAKHOA"] != null)
                dgvadd_KHOA.Columns["MAKHOA"].HeaderText = "Mã khoa";

            if (dgvadd_KHOA.Columns["TENKHOA"] != null)
                dgvadd_KHOA.Columns["TENKHOA"].HeaderText = "Tên khoa";
        }

        private void LoadExistingKhoa()
        {
            // Nếu đang ở chế độ thêm mới, không load gì cả
            dtKhoa.Clear();
        }

        private void LoadSingleKhoa(string maKhoa)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MAKHOA, TENKHOA FROM KHOA WHERE MAKHOA = @MAKHOA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAKHOA", maKhoa);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DataRow row = dtKhoa.NewRow();
                                row["STT"] = 1;
                                row["MAKHOA"] = reader["MAKHOA"].ToString();
                                row["TENKHOA"] = reader["TENKHOA"].ToString();
                                dtKhoa.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddtemp_KHOA_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtMAKHOA.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khoa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMAKHOA.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTENKHOA.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khoa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENKHOA.Focus();
                return;
            }

            // Kiểm tra trùng trong lưới
            foreach (DataRow row in dtKhoa.Rows)
            {
                if (row["MAKHOA"].ToString() == txtMAKHOA.Text.Trim())
                {
                    MessageBox.Show("Mã khoa đã tồn tại trong danh sách!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm vào lưới
            DataRow newRow = dtKhoa.NewRow();
            newRow["STT"] = dtKhoa.Rows.Count + 1;
            newRow["MAKHOA"] = txtMAKHOA.Text.Trim();
            newRow["TENKHOA"] = txtTENKHOA.Text.Trim();
            dtKhoa.Rows.Add(newRow);

            // Cập nhật lại STT
            UpdateSTT();

            // Clear textbox
            txtMAKHOA.Clear();
            txtTENKHOA.Clear();
            txtMAKHOA.Focus();
        }

        private void btnedittemp_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvadd_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMAKHOA.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khoa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMAKHOA.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTENKHOA.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khoa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENKHOA.Focus();
                return;
            }

            DataGridViewRow selectedRow = dgvadd_KHOA.SelectedRows[0];
            int rowIndex = selectedRow.Index;
            string oldMAKHOA = dtKhoa.Rows[rowIndex]["MAKHOA"].ToString();

            // Kiểm tra trùng mã (nếu đổi mã)
            if (txtMAKHOA.Text.Trim() != oldMAKHOA)
            {
                foreach (DataRow row in dtKhoa.Rows)
                {
                    if (row["MAKHOA"].ToString() == txtMAKHOA.Text.Trim())
                    {
                        MessageBox.Show("Mã khoa đã tồn tại trong danh sách!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Cập nhật
            dtKhoa.Rows[rowIndex]["MAKHOA"] = txtMAKHOA.Text.Trim();
            dtKhoa.Rows[rowIndex]["TENKHOA"] = txtTENKHOA.Text.Trim();

            // Clear textbox
            txtMAKHOA.Clear();
            txtTENKHOA.Clear();
            txtMAKHOA.Focus();
        }

        private void btndeletetemp_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvadd_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khoa này khỏi danh sách?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DataGridViewRow selectedRow = dgvadd_KHOA.SelectedRows[0];
                int rowIndex = selectedRow.Index;
                dtKhoa.Rows.RemoveAt(rowIndex);

                // Cập nhật lại STT
                UpdateSTT();

                // Clear textbox
                txtMAKHOA.Clear();
                txtTENKHOA.Clear();
            }
        }

        private void dgvadd_KHOA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvadd_KHOA.Rows[e.RowIndex];
                txtMAKHOA.Text = row.Cells["MAKHOA"].Value.ToString();
                txtTENKHOA.Text = row.Cells["TENKHOA"].Value.ToString();
            }
        }

        private void UpdateSTT()
        {
            for (int i = 0; i < dtKhoa.Rows.Count; i++)
            {
                dtKhoa.Rows[i]["STT"] = i + 1;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtKhoa.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách khoa trống! Vui lòng thêm ít nhất một khoa.", "Thông báo",
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
                            // Chế độ sửa: Cập nhật khoa hiện tại
                            if (dtKhoa.Rows.Count != 1)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Chế độ sửa chỉ cho phép cập nhật một khoa!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            DataRow row = dtKhoa.Rows[0];
                            string query = @"UPDATE KHOA 
                                           SET MAKHOA = @MAKHOA_NEW,
                                               TENKHOA = @TENKHOA
                                           WHERE MAKHOA = @MAKHOA_OLD";

                            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MAKHOA_NEW", row["MAKHOA"].ToString());
                                cmd.Parameters.AddWithValue("@TENKHOA", row["TENKHOA"].ToString());
                                cmd.Parameters.AddWithValue("@MAKHOA_OLD", editingMAKHOA);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật khoa thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Chế độ thêm mới: Thêm tất cả khoa trong lưới
                            foreach (DataRow row in dtKhoa.Rows)
                            {
                                // Kiểm tra trùng trong database
                                string checkQuery = "SELECT COUNT(*) FROM KHOA WHERE MAKHOA = @MAKHOA";
                                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@MAKHOA", row["MAKHOA"].ToString());
                                    int count = (int)checkCmd.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Mã khoa {row["MAKHOA"]} đã tồn tại trong database!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                // Thêm mới
                                string insertQuery = @"INSERT INTO KHOA (MAKHOA, TENKHOA)
                                                     VALUES (@MAKHOA, @TENKHOA)";

                                using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MAKHOA", row["MAKHOA"].ToString());
                                    cmd.Parameters.AddWithValue("@TENKHOA", row["TENKHOA"].ToString());
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show($"Thêm mới thành công {dtKhoa.Rows.Count} khoa!", "Thông báo",
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