using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class UC_DANHMUCCSVC : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_DANHMUCCSVC()
        {
            InitializeComponent();
        }

        private void UC_DANHMUCCSVC_Load(object sender, EventArgs e)
        {
            LoadComboBoxFilter();
            LoadDanhMucCSVC();
            LoadComboBoxFilterNhaCSVC();
            LoadNhaCSVC();
        }

        #region DANH MỤC CSVC

        private void LoadComboBoxFilter()
        {
            comfillter_DMCSVC.Items.Clear();
            comfillter_DMCSVC.Items.Add("Tên CSVC");
            comfillter_DMCSVC.Items.Add("Nhà cung cấp");
            comfillter_DMCSVC.Items.Add("Trạng thái");
            comfillter_DMCSVC.SelectedIndex = 0;
        }

        private void LoadDanhMucCSVC(string filterType = "", string searchValue = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        DM.MA_CSVC,
                                        DM.TEN_CSVC,
                                        ISNULL(NC.TEN_NHACC, N'') AS TEN_NHACC,
                                        DM.TRANGTHAI,
                                        DM.CHITIET,
                                        DM.MA_NHACC
                                    FROM DM_CSVC DM
                                    LEFT JOIN NHACC NC ON DM.MA_NHACC = NC.MA_NHACC
                                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        switch (filterType)
                        {
                            case "Tên CSVC":
                                query += " AND DM.TEN_CSVC COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                            case "Nhà cung cấp":
                                query += " AND NC.TEN_NHACC COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                            case "Trạng thái":
                                query += " AND DM.TRANGTHAI COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                        }
                    }

                    query += " ORDER BY DM.MA_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchValue))
                        {
                            cmd.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Thêm cột STT
                        dt.Columns.Add("STT", typeof(int));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["STT"] = i + 1;
                        }

                        dgvDMCSVC.AutoGenerateColumns = false;
                        dgvDMCSVC.DataSource = dt;

                        // Bind dữ liệu vào các cột
                        foreach (DataGridViewColumn col in dgvDMCSVC.Columns)
                        {
                            if (col.DataPropertyName != null && dt.Columns.Contains(col.DataPropertyName))
                            {
                                col.DataPropertyName = col.DataPropertyName;
                            }
                        }

                        // Ẩn cột MA_NHACC nếu có
                        if (dgvDMCSVC.Columns["MA_NHACC"] != null)
                        {
                            dgvDMCSVC.Columns["MA_NHACC"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu danh mục CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfillter_DMCSVC_Click(object sender, EventArgs e)
        {
            string filterType = comfillter_DMCSVC.SelectedItem?.ToString() ?? "";
            string searchValue = txtSearch_DMCSVC.Text.Trim();
            LoadDanhMucCSVC(filterType, searchValue);
        }

        private void txtSearch_DMCSVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnfillter_DMCSVC_Click(sender, e);
                e.Handled = true;
            }
        }

        private void btnadd_DMCSVC_Click(object sender, EventArgs e)
        {
            try
            {
                frm_DM_CSVC frm = new frm_DM_CSVC();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhMucCSVC();
                    MessageBox.Show("Thêm cơ sở vật chất thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form thêm CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnedit_DMCSVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDMCSVC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một cơ sở vật chất để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCSVC = dgvDMCSVC.SelectedRows[0].Cells["MA_CSVC"].Value?.ToString();

                if (string.IsNullOrEmpty(maCSVC))
                {
                    MessageBox.Show("Không lấy được mã CSVC!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_DM_CSVC frm = new frm_DM_CSVC(maCSVC);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhMucCSVC();
                    MessageBox.Show("Cập nhật cơ sở vật chất thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form sửa CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_DMCSVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDMCSVC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một cơ sở vật chất để xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCSVC = dgvDMCSVC.SelectedRows[0].Cells["MA_CSVC"].Value?.ToString();
                string tenCSVC = dgvDMCSVC.SelectedRows[0].Cells["TEN_CSVC"].Value?.ToString();

                if (string.IsNullOrEmpty(maCSVC))
                {
                    MessageBox.Show("Không lấy được mã CSVC!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa cơ sở vật chất '{tenCSVC}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    XoaCSVC(maCSVC);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaCSVC(string maCSVC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra xem CSVC đã được phân bổ cho nhà nào chưa
                    string checkQuery = "SELECT COUNT(*) FROM NHA_CSVC WHERE MA_CSVC = @MA_CSVC";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "Không thể xóa cơ sở vật chất này vì đã được phân bổ cho nhà!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Kiểm tra xem CSVC có trong yêu cầu sửa chữa không
                    string checkSCQuery = "SELECT COUNT(*) FROM SC_CSVC WHERE MA_CSVC = @MA_CSVC";
                    using (SqlCommand checkCmd = new SqlCommand(checkSCQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "Không thể xóa cơ sở vật chất này vì có yêu cầu sửa chữa liên quan!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Xóa CSVC
                    string deleteQuery = "DELETE FROM DM_CSVC WHERE MA_CSVC = @MA_CSVC";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa cơ sở vật chất thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhMucCSVC();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy cơ sở vật chất cần xóa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region PHÂN BỔ CSVC CHO NHÀ

        private void LoadComboBoxFilterNhaCSVC()
        {
            comfillter_NHA_CSVC.Items.Clear();
            comfillter_NHA_CSVC.Items.Add("Mã nhà");
            comfillter_NHA_CSVC.Items.Add("Tên CSVC");
            comfillter_NHA_CSVC.SelectedIndex = 0;
        }

        private void LoadNhaCSVC(string filterType = "", string searchValue = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        NC.MANHA,
                                        NC.MA_CSVC,
                                        DM.TEN_CSVC,
                                        NCC.TEN_NHACC,
                                        NC.SOLUONG,
                                        NC.GHICHU
                                    FROM NHA_CSVC NC
                                    INNER JOIN DM_CSVC DM ON NC.MA_CSVC = DM.MA_CSVC
                                    LEFT JOIN NHACC NCC ON DM.MA_NHACC = NCC.MA_NHACC
                                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        switch (filterType)
                        {
                            case "Mã nhà":
                                query += " AND NC.MANHA COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                            case "Tên CSVC":
                                query += " AND DM.TEN_CSVC COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                        }
                    }

                    query += " ORDER BY NC.MANHA, DM.TEN_CSVC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchValue))
                        {
                            cmd.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Thêm cột STT
                        dt.Columns.Add("STT", typeof(int));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["STT"] = i + 1;
                        }

                        dgvNHA_CSVC.AutoGenerateColumns = false;
                        dgvNHA_CSVC.DataSource = dt;

                        // Bind dữ liệu vào các cột dựa vào DataPropertyName
                        foreach (DataGridViewColumn col in dgvNHA_CSVC.Columns)
                        {
                            if (!string.IsNullOrEmpty(col.DataPropertyName) && dt.Columns.Contains(col.DataPropertyName))
                            {
                                // Cột đã được bind qua DataPropertyName trong Designer
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phân bổ CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfillter_NHA_CSVC_Click(object sender, EventArgs e)
        {
            string filterType = comfillter_NHA_CSVC.SelectedItem?.ToString() ?? "";
            string searchValue = txtSearch_NHA_CSVC.Text.Trim();
            LoadNhaCSVC(filterType, searchValue);
        }

        private void txtSearch_NHA_CSVC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnfillter_NHA_CSVC_Click(sender, e);
                e.Handled = true;
            }
        }

        private void btnadd_NHA_CSVC_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Phanbo_CSVC frm = new frm_Phanbo_CSVC();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadNhaCSVC();
                    MessageBox.Show("Phân bổ cơ sở vật chất thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form phân bổ CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnedit_NHA_CSVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNHA_CSVC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNha = dgvNHA_CSVC.SelectedRows[0].Cells["MANHA"].Value?.ToString();
                string maCSVC = dgvNHA_CSVC.SelectedRows[0].Cells["MA_CSVC_2"].Value?.ToString();

                if (string.IsNullOrEmpty(maNha) || string.IsNullOrEmpty(maCSVC))
                {
                    MessageBox.Show("Không lấy được thông tin bản ghi!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_Phanbo_CSVC frm = new frm_Phanbo_CSVC(maNha, maCSVC);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadNhaCSVC();
                    MessageBox.Show("Cập nhật phân bổ CSVC thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form sửa phân bổ CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_NHA_CSVC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNHA_CSVC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi để xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNha = dgvNHA_CSVC.SelectedRows[0].Cells["MANHA"].Value?.ToString();
                string maCSVC = dgvNHA_CSVC.SelectedRows[0].Cells["MA_CSVC_2"].Value?.ToString();
                string tenCSVC = dgvNHA_CSVC.SelectedRows[0].Cells["TEN_CSVC_2"].Value?.ToString();

                if (string.IsNullOrEmpty(maNha) || string.IsNullOrEmpty(maCSVC))
                {
                    MessageBox.Show("Không lấy được thông tin bản ghi!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa phân bổ '{tenCSVC}' cho nhà '{maNha}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    XoaNhaCSVC(maNha, maCSVC);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaNhaCSVC(string maNha, string maCSVC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM NHA_CSVC WHERE MANHA = @MANHA AND MA_CSVC = @MA_CSVC";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", maNha);
                        cmd.Parameters.AddWithValue("@MA_CSVC", maCSVC);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa phân bổ CSVC thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNhaCSVC();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy bản ghi cần xóa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}