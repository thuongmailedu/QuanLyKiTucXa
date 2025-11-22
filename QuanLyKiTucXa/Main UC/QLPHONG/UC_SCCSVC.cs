using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class UC_SCCSVC : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_SCCSVC()
        {
            InitializeComponent();
        }

        private void UC_SCCSVC_Load(object sender, EventArgs e)
        {
            LoadComboBoxFilter();
            LoadDanhSachSuaChua();
        }

        private void LoadComboBoxFilter()
        {
            comfillter.Items.Clear();
            comfillter.Items.Add("Mã phòng");
            comfillter.Items.Add("Tên CSVC");
            comfillter.Items.Add("Trạng thái");
            comfillter.SelectedIndex = 0;
        }

        private void LoadDanhSachSuaChua(string filterType = "", string searchValue = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        SC.MA_SCCSVC,
                                        SC.MA_PHONG,
                                        P.MANHA,
                                        SC.MA_CSVC,
                                        DM.TEN_CSVC,
                                        ISNULL(NC.TEN_NHACC, N'') AS TEN_NHACC,
                                        SC.NGAY_YEUCAU,
                                        SC.NGAY_HOANTHANH,
                                        SC.CHITIET,
                                        SC.TRANGTHAI,
                                        ISNULL(NV.TENNV, N'') AS TENNV,
                                        SC.MANV
                                    FROM SC_CSVC SC
                                    INNER JOIN PHONG P ON SC.MA_PHONG = P.MA_PHONG
                                    INNER JOIN DM_CSVC DM ON SC.MA_CSVC = DM.MA_CSVC
                                    LEFT JOIN NHACC NC ON DM.MA_NHACC = NC.MA_NHACC
                                    LEFT JOIN NHANVIEN NV ON SC.MANV = NV.MANV
                                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        switch (filterType)
                        {
                            case "Mã phòng":
                                query += " AND SC.MA_PHONG COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                            case "Tên CSVC":
                                query += " AND DM.TEN_CSVC COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                            case "Trạng thái":
                                query += " AND SC.TRANGTHAI COLLATE Latin1_General_CI_AI LIKE @SearchValue";
                                break;
                        }
                    }

                    query += " ORDER BY SC.NGAY_YEUCAU DESC";

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

                        dgvSCCSVC.AutoGenerateColumns = false;
                        dgvSCCSVC.DataSource = dt;

                        // Bind dữ liệu vào các cột dựa vào DataPropertyName
                        foreach (DataGridViewColumn col in dgvSCCSVC.Columns)
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
                MessageBox.Show("Lỗi tải dữ liệu sửa chữa CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfillter_Click(object sender, EventArgs e)
        {
            string filterType = comfillter.SelectedItem?.ToString() ?? "";
            string searchValue = txtSearch.Text.Trim();
            LoadDanhSachSuaChua(filterType, searchValue);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnfillter_Click(sender, e);
                e.Handled = true;
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                frm_SCCSVC frm = new frm_SCCSVC();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachSuaChua();
                    MessageBox.Show("Thêm yêu cầu sửa chữa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form thêm yêu cầu sửa chữa: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSCCSVC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một yêu cầu để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maSCCSVC = dgvSCCSVC.SelectedRows[0].Cells["MA_SCCSVC"].Value?.ToString();

                if (string.IsNullOrEmpty(maSCCSVC))
                {
                    MessageBox.Show("Không lấy được mã yêu cầu!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frm_SCCSVC frm = new frm_SCCSVC(maSCCSVC);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachSuaChua();
                    MessageBox.Show("Cập nhật yêu cầu sửa chữa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form sửa yêu cầu: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSCCSVC.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một yêu cầu để xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maSCCSVC = dgvSCCSVC.SelectedRows[0].Cells["MA_SCCSVC"].Value?.ToString();
                string maPhong = dgvSCCSVC.SelectedRows[0].Cells["MA_PHONG"].Value?.ToString();
                string tenCSVC = dgvSCCSVC.SelectedRows[0].Cells["TEN_CSVC"].Value?.ToString();

                if (string.IsNullOrEmpty(maSCCSVC))
                {
                    MessageBox.Show("Không lấy được mã yêu cầu!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa yêu cầu sửa chữa '{tenCSVC}' tại phòng '{maPhong}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    XoaYeuCauSuaChua(maSCCSVC);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaYeuCauSuaChua(string maSCCSVC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM SC_CSVC WHERE MA_SCCSVC = @MA_SCCSVC";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_SCCSVC", maSCCSVC);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa yêu cầu sửa chữa thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachSuaChua();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy yêu cầu cần xóa!", "Thông báo",
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
    }
}