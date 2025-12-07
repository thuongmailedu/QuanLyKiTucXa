using QuanLyKiTucXa.Formadd.HSSV;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Ribbons
{
    public partial class UC_HOSOSINHVIEN_Ribbon : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_HOSOSINHVIEN_Ribbon()
        {
            InitializeComponent();
        }

        private void UC_HOSOSINHVIEN_Ribbon_Load(object sender, EventArgs e)
        {
            grdData.ColumnHeadersDefaultCellStyle.Font = new Font(grdData.Font, FontStyle.Bold);
            grdData.AutoGenerateColumns = false;

            // Đổi SelectionMode thành FullRowSelect
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ❌ XÓA CÁC DÒNG ĐĂNG KÝ EVENT - Đã có trong Designer rồi
            // btnedit_SINHVIEN.Click += btnedit_SINHVIEN_Click;
            // btndelete_SINHVIEN.Click += btndelete_SINHVIEN_Click;
            // btnfillter_SINHVIEN.Click += btnfillter_SINHVIEN_Click;

            LoadDanhSachSinhVien();
        }

        private void LoadDanhSachSinhVien(string searchText = "", string searchType = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        SV.MASV,
                                        SV.TENSV,
                                        HD. MA_PHONG,
                                        P.MANHA,
                                        CASE 
                                            WHEN HD. NGAYKTTT IS NOT NULL AND GETDATE() > HD. NGAYKTTT THEN N'Đã thanh lý hợp đồng'
                                            WHEN HD.NGAYKTTT IS NULL AND GETDATE() > HD.DENNGAY THEN N'Hết hạn hợp đồng'
                                            WHEN GETDATE() >= HD. TUNGAY AND GETDATE() <= HD.DENNGAY THEN N'Đang cư trú'
                                            WHEN GETDATE() < HD. TUNGAY THEN N'Chưa nhận phòng'
                                            ELSE N'Chưa có hợp đồng'
                                        END AS TINHTRANG_CUTRU
                                     FROM SINHVIEN SV
                                     LEFT JOIN (
                                         SELECT HD1.*
                                         FROM HOPDONG HD1
                                         INNER JOIN (
                                             SELECT MASV, MAX(TUNGAY) AS TUNGAY_GANNHAT
                                             FROM HOPDONG
                                             GROUP BY MASV
                                         ) HD2 ON HD1.MASV = HD2.MASV AND HD1.TUNGAY = HD2.TUNGAY_GANNHAT
                                     ) HD ON SV.MASV = HD.MASV
                                     LEFT JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG";

                    // Thêm điều kiện tìm kiếm
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        if (searchType == "Mã SV")
                        {
                            query += " WHERE SV.MASV LIKE @SearchText";
                        }
                        else if (searchType == "Tên SV")
                        {
                            query += " WHERE SV. TENSV LIKE @SearchText";
                        }
                    }

                    // Sắp xếp theo MASV giảm dần (mới nhất lên đầu)
                    query += " ORDER BY SV.MASV DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchText))
                        {
                            cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        grdData.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách sinh viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdData.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
        }

        private void btn_addHSSV_Click(object sender, EventArgs e)
        {
            using (frm_addHSSV form = new frm_addHSSV())
            {
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadDanhSachSinhVien();
                }
            }
        }

        private void btnedit_SINHVIEN_Click(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = grdData.SelectedRows[0];

            string maSV = row.Cells["MASV_2"].Value?.ToString() ?? "";
            string maPhong = row.Cells["MA_PHONG_2"].Value?.ToString() ?? "";
            string maNha = row.Cells["MANHA"].Value?.ToString() ?? "";
            string tinhTrangCuTru = row.Cells["TINHTRANG_CUTRU"].Value?.ToString() ?? "";

            using (frm_addHSSV form = new frm_addHSSV(maSV, maPhong, maNha, tinhTrangCuTru))
            {
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadDanhSachSinhVien();
                }
            }
        }

        private void btndelete_SINHVIEN_Click(object sender, EventArgs e)
        {
            if (grdData.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = grdData.SelectedRows[0];
            string maSV = row.Cells["MASV_2"].Value?.ToString() ?? "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra đã có hợp đồng chưa
                    string checkQuery = "SELECT COUNT(*) FROM HOPDONG WHERE MASV = @MASV";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MASV", maSV);
                        int countHD = (int)checkCmd.ExecuteScalar();

                        if (countHD > 0)
                        {
                            MessageBox.Show($"Không thể xóa sinh viên {maSV} vì đã có {countHD} hợp đồng liên quan!\n" +
                                          "Vui lòng xóa các hợp đồng trước khi xóa sinh viên.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa sinh viên {maSV}?\nThông tin thân nhân cũng sẽ bị xóa! ",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Xóa thân nhân trước
                            string deleteTN = "DELETE FROM THANNHAN WHERE MASV = @MASV";
                            using (SqlCommand deleteTNCmd = new SqlCommand(deleteTN, conn, transaction))
                            {
                                deleteTNCmd.Parameters.AddWithValue("@MASV", maSV);
                                deleteTNCmd.ExecuteNonQuery();
                            }

                            // Xóa sinh viên
                            string deleteSV = "DELETE FROM SINHVIEN WHERE MASV = @MASV";
                            using (SqlCommand deleteSVCmd = new SqlCommand(deleteSV, conn, transaction))
                            {
                                deleteSVCmd.Parameters.AddWithValue("@MASV", maSV);
                                deleteSVCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Xóa sinh viên thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadDanhSachSinhVien();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfillter_SINHVIEN_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            string searchType = comfillter.Text;

            LoadDanhSachSinhVien(searchText, searchType);
        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}