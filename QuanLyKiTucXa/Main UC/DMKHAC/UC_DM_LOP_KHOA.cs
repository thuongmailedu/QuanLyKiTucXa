using QuanLyKiTucXa.Formadd.DM_KHAC_FORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    public partial class UC_DM_LOP_KHOA : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        // Biến theo dõi trạng thái lọc
        private bool isFilterMode_KHOA = false;
        private bool isFilterMode_LOP = false;

        public UC_DM_LOP_KHOA()
        {
            InitializeComponent();
            this.Load += UC_DM_LOP_KHOA_Load;
        }

        private void UC_DM_LOP_KHOA_Load(object sender, EventArgs e)
        {
            InitializeFilterControls();
            LoadDanhSachKhoa();
            LoadDanhSachLop();
        }

        private void InitializeFilterControls()
        {
            try
            {
                // ✅ Khởi tạo ComboBox Filter cho KHOA
                if (comfillter_KHOA.Items.Count == 0)
                {
                    comfillter_KHOA.Items.Clear();
                    comfillter_KHOA.Items.Add("Mã khoa");
                    comfillter_KHOA.Items.Add("Tên khoa");
                    comfillter_KHOA.SelectedIndex = 0;
                }

                // ✅ Khởi tạo ComboBox Filter cho LỚP
                if (comfillter_LOP.Items.Count == 0)
                {
                    comfillter_LOP.Items.Clear();
                    comfillter_LOP.Items.Add("Mã lớp");
                    comfillter_LOP.Items.Add("Tên khoa");
                    comfillter_LOP.Items.Add("Tên lớp");
                    comfillter_LOP.SelectedIndex = 0;
                }

                // Reset trạng thái filter
                isFilterMode_KHOA = false;
                isFilterMode_LOP = false;

                if (btnfillter_KHOA != null)
                {
                    //btnfillter_KHOA.Text = "Lọc";
                    //btnfillter_KHOA.BackColor = System.Drawing.SystemColors.Control;
                }

                if (btnfillter_LOP != null)
                {
                    //btnfillter_LOP.Text = "Lọc";
                    //btnfillter_LOP.BackColor = System.Drawing.SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo controls: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region QUẢN LÝ KHOA

        private void LoadDanhSachKhoa()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        MAKHOA,
                                        TENKHOA
                                     FROM KHOA
                                     ORDER BY MAKHOA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_KHOA.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_KHOA.Rows.Add();

                            dgvDM_KHOA.Rows[index].Cells["STT"].Value = stt++;
                            dgvDM_KHOA.Rows[index].Cells["MAKHOA"].Value = row["MAKHOA"].ToString();
                            dgvDM_KHOA.Rows[index].Cells["TENKHOA"].Value = row["TENKHOA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CHỨC NĂNG LỌC KHOA
        private void btnfillter_KHOA_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode_KHOA)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter_KHOA();
                    isFilterMode_KHOA = false;
                    //btnfillter_KHOA.Text = "Lọc";
                    //btnfillter_KHOA.BackColor = System.Drawing.SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter_KHOA();
                    isFilterMode_KHOA = true;
                    //btnfillter_KHOA.Text = "Reset";
                    //btnfillter_KHOA.BackColor = System.Drawing.Color.LightCoral;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu khoa: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter_KHOA()
        {
            string query = @"SELECT 
                                MAKHOA,
                                TENKHOA
                             FROM KHOA
                             WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter_KHOA
            if (comfillter_KHOA.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch_KHOA.Text))
            {
                string selectedFilter = comfillter_KHOA.SelectedItem.ToString();
                string searchValue = txtSearch_KHOA.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã khoa":
                        query += " AND MAKHOA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Tên khoa":
                        query += " AND TENKHOA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                }
            }

            query += " ORDER BY MAKHOA";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khoa nào phù hợp với điều kiện lọc! ",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Hiển thị lên DataGridView
            dgvDM_KHOA.Rows.Clear();
            int stt = 1;
            foreach (DataRow row in dt.Rows)
            {
                int index = dgvDM_KHOA.Rows.Add();
                dgvDM_KHOA.Rows[index].Cells["STT"].Value = stt++;
                dgvDM_KHOA.Rows[index].Cells["MAKHOA"].Value = row["MAKHOA"].ToString();
                dgvDM_KHOA.Rows[index].Cells["TENKHOA"].Value = row["TENKHOA"].ToString();
            }
        }

        private void ResetFilter_KHOA()
        {
            // Load lại toàn bộ dữ liệu
            LoadDanhSachKhoa();

            // Reset các control về trạng thái mặc định
            txtSearch_KHOA.Clear();

            if (comfillter_KHOA.Items.Count > 0)
                comfillter_KHOA.SelectedIndex = 0;
        }

        private void btnadd_KHOA_Click(object sender, EventArgs e)
        {
            frmadd_KHOA form = new frmadd_KHOA();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_KHOA)
                {
                    ApplyFilter_KHOA();
                }
                else
                {
                    LoadDanhSachKhoa();
                }
            }
        }

        private void btnedit_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvDM_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_KHOA.SelectedRows[0];
            string maKhoa = row.Cells["MAKHOA"].Value.ToString();

            frmadd_KHOA form = new frmadd_KHOA(maKhoa);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_KHOA)
                {
                    ApplyFilter_KHOA();
                }
                else
                {
                    LoadDanhSachKhoa();
                }

                // Reload lớp vì tên khoa có thể đổi
                if (isFilterMode_LOP)
                {
                    ApplyFilter_LOP();
                }
                else
                {
                    LoadDanhSachLop();
                }
            }
        }

        private void btndelete_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvDM_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_KHOA.SelectedRows[0];
            string maKhoa = row.Cells["MAKHOA"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có lớp nào thuộc khoa này không
                    string checkQuery = "SELECT COUNT(*) FROM LOP WHERE MAKHOA = @MAKHOA";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MAKHOA", maKhoa);
                        int countLop = (int)checkCmd.ExecuteScalar();

                        if (countLop > 0)
                        {
                            MessageBox.Show($"Không thể xóa khoa {maKhoa} vì đã có {countLop} lớp thuộc khoa này!\n" +
                                          "Vui lòng xóa các lớp trước khi xóa khoa.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa khoa {maKhoa}? ",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM KHOA WHERE MAKHOA = @MAKHOA";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MAKHOA", maKhoa);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa khoa thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (isFilterMode_KHOA)
                                {
                                    ApplyFilter_KHOA();
                                }
                                else
                                {
                                    LoadDanhSachKhoa();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region QUẢN LÝ LỚP

        private void LoadDanhSachLop()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        L.MALOP,
                                        L. TENLOP,
                                        L.MAKHOA,
                                        K.TENKHOA
                                     FROM LOP L
                                     INNER JOIN KHOA K ON L.MAKHOA = K. MAKHOA
                                     ORDER BY L.MAKHOA, L.MALOP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_LOP.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_LOP.Rows.Add();

                            dgvDM_LOP.Rows[index].Cells["STT_2"].Value = stt++;
                            dgvDM_LOP.Rows[index].Cells["MAKHOA_2"].Value = row["MAKHOA"].ToString();
                            dgvDM_LOP.Rows[index].Cells["TENKHOA_2"].Value = row["TENKHOA"].ToString();
                            dgvDM_LOP.Rows[index].Cells["MALOP"].Value = row["MALOP"].ToString();
                            dgvDM_LOP.Rows[index].Cells["TENLOP"].Value = row["TENLOP"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CHỨC NĂNG LỌC LỚP
        private void btnfillter_LOP_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode_LOP)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter_LOP();
                    isFilterMode_LOP = false;
                    btnfillter_LOP.Text = "Lọc";
                    btnfillter_LOP.BackColor = System.Drawing.SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter_LOP();
                    isFilterMode_LOP = true;
                    btnfillter_LOP.Text = "Reset";
                    btnfillter_LOP.BackColor = System.Drawing.Color.LightCoral;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu lớp: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter_LOP()
        {
            string query = @"SELECT 
                                L. MALOP,
                                L.TENLOP,
                                L.MAKHOA,
                                K.TENKHOA
                             FROM LOP L
                             INNER JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                             WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter_LOP
            if (comfillter_LOP.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch_LOP.Text))
            {
                string selectedFilter = comfillter_LOP.SelectedItem.ToString();
                string searchValue = txtSearch_LOP.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã lớp":
                        query += " AND L.MALOP LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Tên khoa":
                        query += " AND K.TENKHOA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Tên lớp":
                        query += " AND L.TENLOP LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                }
            }

            query += " ORDER BY L.MAKHOA, L.MALOP";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy lớp nào phù hợp với điều kiện lọc!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Hiển thị lên DataGridView
            dgvDM_LOP.Rows.Clear();
            int stt = 1;
            foreach (DataRow row in dt.Rows)
            {
                int index = dgvDM_LOP.Rows.Add();
                dgvDM_LOP.Rows[index].Cells["STT_2"].Value = stt++;
                dgvDM_LOP.Rows[index].Cells["MAKHOA_2"].Value = row["MAKHOA"].ToString();
                dgvDM_LOP.Rows[index].Cells["TENKHOA_2"].Value = row["TENKHOA"].ToString();
                dgvDM_LOP.Rows[index].Cells["MALOP"].Value = row["MALOP"].ToString();
                dgvDM_LOP.Rows[index].Cells["TENLOP"].Value = row["TENLOP"].ToString();
            }
        }

        private void ResetFilter_LOP()
        {
            // Load lại toàn bộ dữ liệu
            LoadDanhSachLop();

            // Reset các control về trạng thái mặc định
            txtSearch_LOP.Clear();

            if (comfillter_LOP.Items.Count > 0)
                comfillter_LOP.SelectedIndex = 0;
        }

        private void btnadd_NHA_Click(object sender, EventArgs e)
        {
            frmadd_LOP form = new frmadd_LOP();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_LOP)
                {
                    ApplyFilter_LOP();
                }
                else
                {
                    LoadDanhSachLop();
                }
            }
        }

        private void btnedit_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_LOP.SelectedRows[0];
            string maLop = row.Cells["MALOP"].Value.ToString();

            frmadd_LOP form = new frmadd_LOP(maLop);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_LOP)
                {
                    ApplyFilter_LOP();
                }
                else
                {
                    LoadDanhSachLop();
                }
            }
        }

        private void btndelete_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_LOP.SelectedRows[0];
            string maLop = row.Cells["MALOP"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có sinh viên nào thuộc lớp này không
                    string checkQuery = "SELECT COUNT(*) FROM SINHVIEN WHERE MALOP = @MALOP";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MALOP", maLop);
                        int countSV = (int)checkCmd.ExecuteScalar();

                        if (countSV > 0)
                        {
                            MessageBox.Show($"Không thể xóa lớp {maLop} vì đã có {countSV} sinh viên thuộc lớp này!\n" +
                                          "Vui lòng chuyển sinh viên sang lớp khác trước khi xóa lớp.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa lớp {maLop}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM LOP WHERE MALOP = @MALOP";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MALOP", maLop);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa lớp thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (isFilterMode_LOP)
                                {
                                    ApplyFilter_LOP();
                                }
                                else
                                {
                                    LoadDanhSachLop();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region HELPER METHODS

        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi truy vấn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        #endregion

        #region UNUSED EVENT HANDLERS

        private void lblSinhVien_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvDMPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        #endregion
    }
}