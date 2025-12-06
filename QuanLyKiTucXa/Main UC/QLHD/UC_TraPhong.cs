using QuanLyKiTucXa.Formadd.QLHD_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLHD
{
    public partial class UC_TraPhong : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_TraPhong()
        {
            InitializeComponent();
        }

        private void UC_TraPhong_Load(object sender, EventArgs e)
        {
            // Thiết lập font cho header
            dgvTraPhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvTraPhong.Font, FontStyle.Bold);

            // Đổi SelectionMode
            dgvTraPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ✅ Tắt tự động chọn dòng đầu tiên
            dgvTraPhong.ClearSelection();

            // Đăng ký events
            btnedit_Traphong.Click += btnedit_Traphong_Click;
            btndelete_Traphong.Click += btndelete_Traphong_Click;
            btnfillter_TraPhong.Click += btnfillter_TraPhong_Click;

            // Load dữ liệu
            LoadData();
        }

        private void LoadData(string searchType = null, string keyword = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ Cập nhật query để lấy thông tin người thanh lý (MANV_TL)
                    string sql = @"SELECT 
                                    hd.MAHD, 
                                    hd. TENHD, 
                                    sv.MASV, 
                                    sv. TENSV, 
                                    sv.GIOITINH, 
                                    p. MA_PHONG, 
                                    n.MANHA, 
                                    n. LOAIPHONG, 
                                    hd.TUNGAY, 
                                    hd.DENNGAY,
                                    hd.NGAYKTTT, 
                                    hd. NGAYKY,
                                    nv_tl.TENNV AS TENNV_THANHLUY
                                   FROM SINHVIEN sv 
                                   INNER JOIN HOPDONG hd ON sv. MASV = hd. MASV
                                   INNER JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                                   INNER JOIN NHA n ON p.MANHA = n.MANHA
                                   LEFT JOIN NHANVIEN nv_tl ON nv_tl.MANV = hd.MANV_TL
                                   WHERE hd.NGAYKTTT IS NOT NULL";

                    // Thêm điều kiện lọc
                    if (!string.IsNullOrWhiteSpace(keyword) && !string.IsNullOrEmpty(searchType))
                    {
                        switch (searchType)
                        {
                            case "Mã HD":
                                sql += " AND hd.MAHD LIKE @Keyword";
                                break;
                            case "Mã SV":
                                sql += " AND sv.MASV LIKE @Keyword";
                                break;
                            case "Tên SV":
                                sql += " AND sv.TENSV LIKE @Keyword";
                                break;
                            case "Ngày thanh lý":
                                sql += " AND CONVERT(VARCHAR, hd.NGAYKTTT, 103) LIKE @Keyword";
                                break;
                        }
                    }

                    sql += " ORDER BY hd. NGAYKTTT DESC, hd.MAHD";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword) && !string.IsNullOrEmpty(searchType))
                        {
                            cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvTraPhong.DataSource = dt;

                        // ✅ Tắt tự động chọn dòng đầu tiên sau khi load dữ liệu
                        dgvTraPhong.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Traphong_Click(object sender, EventArgs e)
        {
            frm_Traphong form = new frm_Traphong();

            // ✅ Sử dụng using để đảm bảo form được dispose đúng cách
            using (form)
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh lại danh sách
                }
            }
        }

        private void btnedit_Traphong_Click(object sender, EventArgs e)
        {
            if (dgvTraPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvTraPhong.SelectedRows[0];
            string maHD = row.Cells["MAHD_2"].Value?.ToString();
            string maSV = row.Cells["MASV_2"].Value?.ToString();

            if (!string.IsNullOrEmpty(maHD) && !string.IsNullOrEmpty(maSV))
            {
                // ✅ Sử dụng using để đảm bảo form được dispose đúng cách
                using (frm_Traphong form = new frm_Traphong(maHD, maSV))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Refresh lại danh sách
                    }
                }
            }
        }

        private void btndelete_Traphong_Click(object sender, EventArgs e)
        {
            if (dgvTraPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hợp đồng để xóa ngày thanh lý!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvTraPhong.SelectedRows[0];
            string maHD = row.Cells["MAHD_2"].Value?.ToString();
            string maSV = row.Cells["MASV_2"].Value?.ToString();
            string tenSV = row.Cells["TENSV_2"].Value?.ToString();

            if (string.IsNullOrEmpty(maHD))
                return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn XÓA NGÀY THANH LÝ của hợp đồng:\n\n" +
                $"Mã HD: {maHD}\n" +
                $"Sinh viên: {tenSV} ({maSV})\n\n" +
                $"Hành động này sẽ đưa sinh viên về trạng thái đang cư trú! ",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // ✅ Xóa cả NGAYKTTT và MANV_TL
                        string query = "UPDATE HOPDONG SET NGAYKTTT = NULL, MANV_TL = NULL WHERE MAHD = @MAHD";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MAHD", maHD);
                            int affectedRows = cmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Đã xóa ngày thanh lý thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa ngày thanh lý: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnfillter_TraPhong_Click(object sender, EventArgs e)
        {
            string searchType = comfillter.Text;
            string keyword = txtSearch.Text.Trim();

            LoadData(searchType, keyword);
        }
    }
}