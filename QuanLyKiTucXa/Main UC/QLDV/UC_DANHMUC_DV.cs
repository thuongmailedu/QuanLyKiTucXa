using QuanLyKiTucXa.Formadd.QLDV_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLDV
{
    public partial class UC_DANHMUC_DV : UserControl
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string sql, constr;

        public UC_DANHMUC_DV()
        {
            InitializeComponent();
        }

        private void UC_DANHMUC_DV_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo connection
                constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
                conn.ConnectionString = constr;

                // Thiết lập font cho header
                dgv_DICHVU.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgv_DICHVU.Font, System.Drawing.FontStyle.Bold);

                // Load ComboBox lọc theo tên dịch vụ
                LoadComboBoxFilter();

                // Load dữ liệu lần đầu
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load UC: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load ComboBox để lọc
        private void LoadComboBoxFilter()
        {
            comGIATRI.Items.Clear();
            comGIATRI.Items.Add("Tất cả");
            comGIATRI.Items.Add("Điện");
            comGIATRI.Items.Add("Nước");
            comGIATRI.Items.Add("Internet");
            comGIATRI.SelectedIndex = 0; // Mặc định "Tất cả"
        }

        // Load dữ liệu từ database - THÊM TEN_NHACC
        private void LoadData(string tenDichVu = null, string keyword = null)
        {
            try
            {
                // SQL bổ sung JOIN với NHACC để lấy TEN_NHACC
                sql = @"SELECT 
                            DV.MADV, 
                            DV.TENDV, 
                            DV. DONGIA, 
                            DV.DONVI, 
                            DV.TUNGAY, 
                            DV.DENNGAY, 
                            DV.TRANGTHAI,
                            NC.TEN_NHACC
                        FROM DICHVU DV
                        LEFT JOIN NHACC NC ON DV.MA_NHACC = NC. MA_NHACC
                        WHERE 1=1";

                // Thêm điều kiện lọc theo tên dịch vụ
                if (!string.IsNullOrEmpty(tenDichVu) && tenDichVu != "Tất cả")
                {
                    sql += " AND DV.TENDV = @TENDV";
                }

                // Thêm điều kiện tìm kiếm
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += " AND (DV.MADV LIKE @Keyword OR DV. TENDV LIKE @Keyword OR DV.TRANGTHAI LIKE @Keyword OR NC.TEN_NHACC LIKE @Keyword)";
                }

                sql += " ORDER BY DV.TUNGAY DESC, DV.MADV";

                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // Thêm parameters
                        if (!string.IsNullOrEmpty(tenDichVu) && tenDichVu != "Tất cả")
                        {
                            cmd.Parameters.AddWithValue("@TENDV", tenDichVu);
                        }

                        if (!string.IsNullOrEmpty(keyword))
                        {
                            cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                        }

                        da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                        // Thêm cột STT
                        dt.Columns.Add("STT", typeof(int));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["STT"] = i + 1;
                        }

                        // Đổ dữ liệu vào DataGridView
                        dgv_DICHVU.DataSource = dt;
                        dgv_DICHVU.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Khi chọn ComboBox - TỰ ĐỘNG LỌC
        private void comGIATRI_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDV = comGIATRI.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();
            LoadData(tenDV, keyword);
        }

        // Nút THÊM dịch vụ
        private void btnadd_Click(object sender, EventArgs e)
        {
            using (frm_addDICHVU form = new frm_addDICHVU())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Refresh lại danh sách sau khi thêm
                    LoadData();
                }
            }
        }

        // Nút FILTER - Tìm kiếm
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string tenDV = comGIATRI.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();
            LoadData(tenDV, keyword);
        }



        // Nút XÓA dịch vụ
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv_DICHVU.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgv_DICHVU.SelectedRows[0];
            string maDV = row.Cells["MADV"].Value?.ToString();
            string tenDV = row.Cells["TENDV"].Value?.ToString();

            if (string.IsNullOrEmpty(maDV))
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();

                    // Kiểm tra xem dịch vụ đã được sử dụng trong hóa đơn chưa
                    string checkQuery = @"
                SELECT 
                    (SELECT COUNT(*) FROM HD_DIEN WHERE MADV = @MADV) +
                    (SELECT COUNT(*) FROM HD_NUOC WHERE MADV = @MADV) +
                    (SELECT COUNT(*) FROM HD_INTERNET WHERE MADV = @MADV) AS TongHoaDon";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MADV", maDV);
                        int tongHoaDon = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (tongHoaDon > 0)
                        {
                            MessageBox.Show(
                                $"Không thể xóa dịch vụ '{tenDV}' (Mã: {maDV})!\n\n" +
                                $"Dịch vụ này đã được sử dụng trong {tongHoaDon} hóa đơn.\n" +
                                $"Vui lòng xóa các hóa đơn liên quan trước khi xóa dịch vụ.",
                                "Không thể xóa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa dịch vụ '{tenDV}' (Mã: {maDV})?\n\n" +
                        $"Hành động này không thể hoàn tác! ",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Thực hiện xóa
                        string deleteQuery = "DELETE FROM DICHVU WHERE MADV = @MADV";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MADV", maDV);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show(
                                    $"Đã xóa dịch vụ '{tenDV}' thành công! ",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                // Refresh lại danh sách
                                LoadData();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi xóa dịch vụ: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Nút LÀM MỚI - Reset bộ lọc
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            comGIATRI.SelectedIndex = 0;
            txtSearch.Clear();
            LoadData();
        }
        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dgv_DICHVU.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgv_DICHVU.SelectedRows[0];
            string maDV = row.Cells["MADV"].Value?.ToString();

            if (!string.IsNullOrEmpty(maDV))
            {
                using (frm_addDICHVU form = new frm_addDICHVU(maDV))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Refresh lại danh sách
                    }
                }
            }
        }
    }
}