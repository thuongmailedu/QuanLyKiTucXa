using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class UC_DANHMUCCSVC : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True"; // Thay bằng connection string của bạn

        public UC_DANHMUCCSVC()
        {
            InitializeComponent();
        }

        private void UC_DANHMUCCSVC_Load(object sender, EventArgs e)
        {
            LoadComboBoxFilter();
            LoadDanhMucCSVC();
        }

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
                                        NC.TEN_NHACC,
                                        DM.TRANGTHAI,
                                        DM.CHITIET,
                                        NC.MA_NHACC
                                    FROM DM_CSVC DM
                                    LEFT JOIN NHACC NC ON DM.MA_NHACC = NC.MA_NHACC
                                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        switch (filterType)
                        {
                            case "Tên CSVC":
                                query += " AND DM.TEN_CSVC LIKE @SearchValue";
                                break;
                            case "Nhà cung cấp":
                                query += " AND NC.TEN_NHACC LIKE @SearchValue";
                                break;
                            case "Trạng thái":
                                query += " AND DM.TRANGTHAI LIKE @SearchValue";
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

                        dgvDMCSVC.DataSource = dt;

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
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
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
            }
        }

        private void btnadd_DMCSVC_Click(object sender, EventArgs e)
        {
            frm_DM_CSVC frm = new frm_DM_CSVC();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhMucCSVC();
                MessageBox.Show("Thêm cơ sở vật chất thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnedit_DMCSVC_Click(object sender, EventArgs e)
        {
            if (dgvDMCSVC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cơ sở vật chất để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCSVC = dgvDMCSVC.SelectedRows[0].Cells["MA_CSVC"].Value.ToString();
            frm_DM_CSVC frm = new frm_DM_CSVC(maCSVC);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhMucCSVC();
                MessageBox.Show("Cập nhật cơ sở vật chất thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btndelete_DMCSVC_Click(object sender, EventArgs e)
        {
            if (dgvDMCSVC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cơ sở vật chất để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCSVC = dgvDMCSVC.SelectedRows[0].Cells["MA_CSVC"].Value.ToString();
            string tenCSVC = dgvDMCSVC.SelectedRows[0].Cells["TEN_CSVC"].Value.ToString();

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
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa cơ sở vật chất thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhMucCSVC();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}