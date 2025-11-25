using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.DM_KHAC_FORM
{
    public partial class frmadd_LOP : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMALOP = "";
        private DataTable dtLop = new DataTable();

        public frmadd_LOP()
        {
            InitializeComponent();
            InitializeDataTable();
            LoadComboBoxKhoa();
        }

        // Constructor cho chế độ sửa - Load một lớp cụ thể
        public frmadd_LOP(string maLop)
        {
            InitializeComponent();
            InitializeDataTable();
            LoadComboBoxKhoa();

            isEditMode = true;
            editingMALOP = maLop;
            LoadSingleLop(maLop);

            this.Text = "Cập nhật thông tin lớp";
        }

        private void frmadd_LOP_Load(object sender, EventArgs e)
        {
            dgvadd_LOP.DataSource = dtLop;
            SetupGridView();

            // Đăng ký event
            comTENKHOA.SelectedIndexChanged += comTENKHOA_SelectedIndexChanged;
        }

        private void InitializeDataTable()
        {
            dtLop.Columns.Add("STT", typeof(int));
            dtLop.Columns.Add("MAKHOA", typeof(string));
            dtLop.Columns.Add("TENKHOA", typeof(string));
            dtLop.Columns.Add("MALOP", typeof(string));
            dtLop.Columns.Add("TENLOP", typeof(string));
        }

        private void SetupGridView()
        {
            if (dgvadd_LOP.Columns["STT"] != null)
            {
                dgvadd_LOP.Columns["STT"].HeaderText = "STT";
                dgvadd_LOP.Columns["STT"].Width = 50;
            }

            if (dgvadd_LOP.Columns["MAKHOA"] != null)
                dgvadd_LOP.Columns["MAKHOA"].HeaderText = "Mã khoa";

            if (dgvadd_LOP.Columns["TENKHOA"] != null)
                dgvadd_LOP.Columns["TENKHOA"].HeaderText = "Tên khoa";

            if (dgvadd_LOP.Columns["MALOP"] != null)
                dgvadd_LOP.Columns["MALOP"].HeaderText = "Mã lớp";

            if (dgvadd_LOP.Columns["TENLOP"] != null)
                dgvadd_LOP.Columns["TENLOP"].HeaderText = "Tên lớp";
        }

        private void LoadComboBoxKhoa()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MAKHOA, TENKHOA FROM KHOA ORDER BY MAKHOA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comTENKHOA.DataSource = dt;
                        comTENKHOA.DisplayMember = "TENKHOA";
                        comTENKHOA.ValueMember = "MAKHOA";
                        comTENKHOA.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comTENKHOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comTENKHOA.SelectedIndex != -1)
            {
                // Tự động điền Mã Khoa
                txtMAKHOA.Text = comTENKHOA.SelectedValue.ToString();
            }
            else
            {
                txtMAKHOA.Clear();
            }
        }

        private void LoadSingleLop(string maLop)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT L.MALOP, L.TENLOP, L.MAKHOA, K.TENKHOA 
                                   FROM LOP L
                                   INNER JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                                   WHERE L.MALOP = @MALOP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MALOP", maLop);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DataRow row = dtLop.NewRow();
                                row["STT"] = 1;
                                row["MAKHOA"] = reader["MAKHOA"].ToString();
                                row["TENKHOA"] = reader["TENKHOA"].ToString();
                                row["MALOP"] = reader["MALOP"].ToString();
                                row["TENLOP"] = reader["TENLOP"].ToString();
                                dtLop.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnaddtemp_LOP_Click(object sender, EventArgs e)
        {
            // Validate
            if (comTENKHOA.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comTENKHOA.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMALOP.Text))
            {
                MessageBox.Show("Vui lòng nhập mã lớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMALOP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTENLOP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENLOP.Focus();
                return;
            }

            // Kiểm tra trùng trong lưới
            foreach (DataRow row in dtLop.Rows)
            {
                if (row["MALOP"].ToString() == txtMALOP.Text.Trim())
                {
                    MessageBox.Show("Mã lớp đã tồn tại trong danh sách!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm vào lưới
            DataRow newRow = dtLop.NewRow();
            newRow["STT"] = dtLop.Rows.Count + 1;
            newRow["MAKHOA"] = comTENKHOA.SelectedValue.ToString();
            newRow["TENKHOA"] = comTENKHOA.Text;
            newRow["MALOP"] = txtMALOP.Text.Trim();
            newRow["TENLOP"] = txtTENLOP.Text.Trim();
            dtLop.Rows.Add(newRow);

            // Cập nhật lại STT
            UpdateSTT();

            // Clear textbox
            txtMALOP.Clear();
            txtTENLOP.Clear();
            comTENKHOA.SelectedIndex = -1;
            comTENKHOA.Focus();
        }

        private void btnedittemp_LOP_Click(object sender, EventArgs e)
        {
            if (dgvadd_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comTENKHOA.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comTENKHOA.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMALOP.Text))
            {
                MessageBox.Show("Vui lòng nhập mã lớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMALOP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTENLOP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENLOP.Focus();
                return;
            }

            DataGridViewRow selectedRow = dgvadd_LOP.SelectedRows[0];
            int rowIndex = selectedRow.Index;
            string oldMALOP = dtLop.Rows[rowIndex]["MALOP"].ToString();

            // Kiểm tra trùng mã (nếu đổi mã)
            if (txtMALOP.Text.Trim() != oldMALOP)
            {
                foreach (DataRow row in dtLop.Rows)
                {
                    if (row["MALOP"].ToString() == txtMALOP.Text.Trim())
                    {
                        MessageBox.Show("Mã lớp đã tồn tại trong danh sách!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Cập nhật
            dtLop.Rows[rowIndex]["MAKHOA"] = comTENKHOA.SelectedValue.ToString();
            dtLop.Rows[rowIndex]["TENKHOA"] = comTENKHOA.Text;
            dtLop.Rows[rowIndex]["MALOP"] = txtMALOP.Text.Trim();
            dtLop.Rows[rowIndex]["TENLOP"] = txtTENLOP.Text.Trim();

            // Clear textbox
            txtMALOP.Clear();
            txtTENLOP.Clear();
            comTENKHOA.SelectedIndex = -1;
            comTENKHOA.Focus();
        }

        private void btndeletetemp_LOP_Click(object sender, EventArgs e)
        {
            if (dgvadd_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lớp này khỏi danh sách?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DataGridViewRow selectedRow = dgvadd_LOP.SelectedRows[0];
                int rowIndex = selectedRow.Index;
                dtLop.Rows.RemoveAt(rowIndex);

                // Cập nhật lại STT
                UpdateSTT();

                // Clear textbox
                txtMALOP.Clear();
                txtTENLOP.Clear();
                comTENKHOA.SelectedIndex = -1;
            }
        }

        private void dgvadd_LOP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvadd_LOP.Rows[e.RowIndex];

                // Set combobox
                string maKhoa = row.Cells["MAKHOA"].Value.ToString();
                comTENKHOA.SelectedValue = maKhoa;

                txtMALOP.Text = row.Cells["MALOP"].Value.ToString();
                txtTENLOP.Text = row.Cells["TENLOP"].Value.ToString();
            }
        }

        private void UpdateSTT()
        {
            for (int i = 0; i < dtLop.Rows.Count; i++)
            {
                dtLop.Rows[i]["STT"] = i + 1;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtLop.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách lớp trống! Vui lòng thêm ít nhất một lớp.", "Thông báo",
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
                            // Chế độ sửa: Cập nhật lớp hiện tại
                            if (dtLop.Rows.Count != 1)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Chế độ sửa chỉ cho phép cập nhật một lớp!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            DataRow row = dtLop.Rows[0];
                            string query = @"UPDATE LOP 
                                           SET MALOP = @MALOP_NEW,
                                               TENLOP = @TENLOP,
                                               MAKHOA = @MAKHOA
                                           WHERE MALOP = @MALOP_OLD";

                            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MALOP_NEW", row["MALOP"].ToString());
                                cmd.Parameters.AddWithValue("@TENLOP", row["TENLOP"].ToString());
                                cmd.Parameters.AddWithValue("@MAKHOA", row["MAKHOA"].ToString());
                                cmd.Parameters.AddWithValue("@MALOP_OLD", editingMALOP);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật lớp thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Chế độ thêm mới: Thêm tất cả lớp trong lưới
                            foreach (DataRow row in dtLop.Rows)
                            {
                                // Kiểm tra trùng trong database
                                string checkQuery = "SELECT COUNT(*) FROM LOP WHERE MALOP = @MALOP";
                                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@MALOP", row["MALOP"].ToString());
                                    int count = (int)checkCmd.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Mã lớp {row["MALOP"]} đã tồn tại trong database!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                // Thêm mới
                                string insertQuery = @"INSERT INTO LOP (MALOP, TENLOP, MAKHOA)
                                                     VALUES (@MALOP, @TENLOP, @MAKHOA)";

                                using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MALOP", row["MALOP"].ToString());
                                    cmd.Parameters.AddWithValue("@TENLOP", row["TENLOP"].ToString());
                                    cmd.Parameters.AddWithValue("@MAKHOA", row["MAKHOA"].ToString());
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show($"Thêm mới thành công {dtLop.Rows.Count} lớp!", "Thông báo",
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