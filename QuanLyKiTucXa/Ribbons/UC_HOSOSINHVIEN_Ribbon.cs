using QuanLyKiTucXa.Formadd.HSSV;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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
                                            WHEN HD. NGAYKTTT IS NOT NULL AND GETDATE() > HD. NGAYKTTT THEN N'Đã rời KTX'
                                            WHEN HD.NGAYKTTT IS NULL AND GETDATE() > HD.DENNGAY THEN N'Đã rời KTX'
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
        // ✅ CLASS LƯU THÔNG TIN CHI TIẾT SINH VIÊN
        private class SinhVienChiTiet
        {
            // Thông tin cá nhân
            public string MASV { get; set; }
            public string TENSV { get; set; }
            public string GIOITINH { get; set; }
            public DateTime? NGAYSINH { get; set; }
            public string CCCD { get; set; }
            public string SDT { get; set; }
            public string HKTT { get; set; }

            // Thông tin học vấn
            public string MALOP { get; set; }
            public string TENLOP { get; set; }
            public string MAKHOA { get; set; }
            public string TENKHOA { get; set; }

            // Thông tin phòng ở
            public string MA_PHONG { get; set; }
            public string MANHA { get; set; }
            public string LOAIPHONG { get; set; }
            public string TINHTRANG_CUTRU { get; set; }
            public DateTime? TUNGAY { get; set; }
            public DateTime? DENNGAY { get; set; }
            public DateTime? NGAYKTTT { get; set; }

            // Thông tin thân nhân
            public string TEN_THANNHAN { get; set; }
            public string SDT_THANNHAN { get; set; }
            public string MOIQUANHE { get; set; }
            public string DIACHI_THANNHAN { get; set; }
        }

        // ✅ METHOD LẤY THÔNG TIN CHI TIẾT
        private SinhVienChiTiet LayThongTinChiTiet(string maSV)
        {
            SinhVienChiTiet sv = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    SV.MASV,
                    SV. TENSV,
                    SV.GIOITINH,
                    SV. NGAYSINH,
                    SV.CCCD,
                    SV.SDT,
                    SV.HKTT,
                    SV.MALOP,
                    L.TENLOP,
                    L.MAKHOA,
                    K.TENKHOA,
                    HD.MA_PHONG,
                    P.MANHA,
                    N.LOAIPHONG,
                    HD.TUNGAY,
                    HD. DENNGAY,
                    HD.NGAYKTTT,
                    CASE 
                        WHEN HD.NGAYKTTT IS NOT NULL AND GETDATE() > HD.NGAYKTTT THEN N'Đã rời KTX'
                        WHEN HD.NGAYKTTT IS NULL AND GETDATE() > HD.DENNGAY THEN N'Đã rời KTX'
                        WHEN GETDATE() >= HD.TUNGAY AND GETDATE() <= HD.DENNGAY THEN N'Đang cư trú'
                        WHEN GETDATE() < HD.TUNGAY THEN N'Chưa nhận phòng'
                        ELSE N'Chưa có hợp đồng'
                    END AS TINHTRANG_CUTRU,
                    TN.TEN_THANNHAN,
                    TN.SDT AS SDT_THANNHAN,
                    TN.MOIQUANHE,
                    TN.DIACHI AS DIACHI_THANNHAN
                FROM SINHVIEN SV
                LEFT JOIN LOP L ON SV.MALOP = L.MALOP
                LEFT JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                LEFT JOIN (
                    SELECT HD1.*
                    FROM HOPDONG HD1
                    INNER JOIN (
                        SELECT MASV, MAX(TUNGAY) AS TUNGAY_GANNHAT
                        FROM HOPDONG
                        GROUP BY MASV
                    ) HD2 ON HD1.MASV = HD2.MASV AND HD1.TUNGAY = HD2.TUNGAY_GANNHAT
                ) HD ON SV.MASV = HD.MASV
                LEFT JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                LEFT JOIN NHA N ON P.MANHA = N. MANHA
                LEFT JOIN THANNHAN TN ON SV.MASV = TN.MASV
                WHERE SV.MASV = @MASV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MASV", maSV);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sv = new SinhVienChiTiet
                                {
                                    MASV = reader["MASV"].ToString(),
                                    TENSV = reader["TENSV"].ToString(),
                                    GIOITINH = reader["GIOITINH"]?.ToString() ?? "",
                                    NGAYSINH = reader["NGAYSINH"] != DBNull.Value ? (DateTime?)reader["NGAYSINH"] : null,
                                    CCCD = reader["CCCD"]?.ToString() ?? "",
                                    SDT = reader["SDT"]?.ToString() ?? "",
                                    HKTT = reader["HKTT"]?.ToString() ?? "",
                                    MALOP = reader["MALOP"]?.ToString() ?? "",
                                    TENLOP = reader["TENLOP"]?.ToString() ?? "",
                                    MAKHOA = reader["MAKHOA"]?.ToString() ?? "",
                                    TENKHOA = reader["TENKHOA"]?.ToString() ?? "",
                                    MA_PHONG = reader["MA_PHONG"]?.ToString() ?? "",
                                    MANHA = reader["MANHA"]?.ToString() ?? "",
                                    LOAIPHONG = reader["LOAIPHONG"]?.ToString() ?? "",
                                    TINHTRANG_CUTRU = reader["TINHTRANG_CUTRU"]?.ToString() ?? "",
                                    TUNGAY = reader["TUNGAY"] != DBNull.Value ? (DateTime?)reader["TUNGAY"] : null,
                                    DENNGAY = reader["DENNGAY"] != DBNull.Value ? (DateTime?)reader["DENNGAY"] : null,
                                    NGAYKTTT = reader["NGAYKTTT"] != DBNull.Value ? (DateTime?)reader["NGAYKTTT"] : null,
                                    TEN_THANNHAN = reader["TEN_THANNHAN"]?.ToString() ?? "",
                                    SDT_THANNHAN = reader["SDT_THANNHAN"]?.ToString() ?? "",
                                    MOIQUANHE = reader["MOIQUANHE"]?.ToString() ?? "",
                                    DIACHI_THANNHAN = reader["DIACHI_THANNHAN"]?.ToString() ?? ""
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin chi tiết:  " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sv;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            // XÁC ĐỊNH SINH VIÊN CẦN XUẤT
            List<string> danhSachMaSV = new List<string>();

            if (grdData.SelectedRows.Count > 0)
            {
                // ✅ CHỈ XUẤT SINH VIÊN ĐƯỢC CHỌN
                foreach (DataGridViewRow row in grdData.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        string maSV = row.Cells["MASV_2"].Value?.ToString();
                        if (!string.IsNullOrEmpty(maSV))
                            danhSachMaSV.Add(maSV);
                    }
                }
            }
            else
            {
                // ✅ XUẤT TẤT CẢ SINH VIÊN
                foreach (DataGridViewRow row in grdData.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string maSV = row.Cells["MASV_2"].Value?.ToString();
                        if (!string.IsNullOrEmpty(maSV))
                            danhSachMaSV.Add(maSV);
                    }
                }
            }

            if (danhSachMaSV.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo SaveFileDialog
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"HoSoSinhVien_ChiTiet_{DateTime.Now:ddMMyyyy_HHmmss}"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            // ✅ XUẤT EXCEL
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Hồ sơ sinh viên";

                // ===== HEADER (Dòng 1) =====
                int colIndex = 1;
                string[] headers = new string[]
                {
            "STT",
            "Mã SV",
            "Tên sinh viên",
            "Giới tính",
            "Ngày sinh",
            "CCCD",
            "Số điện thoại",
            "Hộ khẩu thường trú",
            "Khoa",
            "Lớp",
            "Mã nhà",
            "Mã phòng",
            "Loại phòng",
            "Tình trạng cư trú",
            "Từ ngày",
            "Đến ngày",
            "Ngày thanh lý",
            "Họ tên thân nhân",
            "Mối quan hệ",
            "SĐT thân nhân",
            "Địa chỉ thân nhân"
                };

                foreach (string header in headers)
                {
                    worksheet.Cells[1, colIndex] = header;

                    // Định dạng header
                    Excel.Range headerCell = worksheet.Cells[1, colIndex];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    headerCell.WrapText = true;

                    colIndex++;
                }

                // ===== DỮ LIỆU (Từ dòng 2) =====
                int rowIndex = 2;
                int stt = 1;

                foreach (string maSV in danhSachMaSV)
                {
                    SinhVienChiTiet sv = LayThongTinChiTiet(maSV);
                    if (sv == null) continue;

                    colIndex = 1;

                    // STT
                    worksheet.Cells[rowIndex, colIndex++] = stt++;

                    // Thông tin cá nhân
                    worksheet.Cells[rowIndex, colIndex++] = sv.MASV;
                    worksheet.Cells[rowIndex, colIndex++] = sv.TENSV;
                    worksheet.Cells[rowIndex, colIndex++] = sv.GIOITINH;
                    worksheet.Cells[rowIndex, colIndex++] = sv.NGAYSINH?.ToString("dd/MM/yyyy") ?? "";
                    worksheet.Cells[rowIndex, colIndex++] = sv.CCCD;
                    worksheet.Cells[rowIndex, colIndex++] = sv.SDT;
                    worksheet.Cells[rowIndex, colIndex++] = sv.HKTT;

                    // Thông tin học vấn
                    worksheet.Cells[rowIndex, colIndex++] = sv.TENKHOA;
                    worksheet.Cells[rowIndex, colIndex++] = sv.TENLOP;

                    // Thông tin phòng ở
                    worksheet.Cells[rowIndex, colIndex++] = sv.MANHA;
                    worksheet.Cells[rowIndex, colIndex++] = sv.MA_PHONG;
                    worksheet.Cells[rowIndex, colIndex++] = sv.LOAIPHONG;

                    // Tình trạng cư trú (có tô màu)
                    worksheet.Cells[rowIndex, colIndex] = sv.TINHTRANG_CUTRU;
                    Excel.Range statusCell = worksheet.Cells[rowIndex, colIndex];
                    if (sv.TINHTRANG_CUTRU == "Đang cư trú")
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                    else if (sv.TINHTRANG_CUTRU == "Đã rời KTX")
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                    else if (sv.TINHTRANG_CUTRU == "Chưa nhận phòng")
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
                    else if (sv.TINHTRANG_CUTRU == "Chưa có hợp đồng")
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightCoral);
                    colIndex++;

                    worksheet.Cells[rowIndex, colIndex++] = sv.TUNGAY?.ToString("dd/MM/yyyy") ?? "";
                    worksheet.Cells[rowIndex, colIndex++] = sv.DENNGAY?.ToString("dd/MM/yyyy") ?? "";
                    worksheet.Cells[rowIndex, colIndex++] = sv.NGAYKTTT?.ToString("dd/MM/yyyy") ?? "";

                    // Thông tin thân nhân
                    worksheet.Cells[rowIndex, colIndex++] = sv.TEN_THANNHAN;
                    worksheet.Cells[rowIndex, colIndex++] = sv.MOIQUANHE;
                    worksheet.Cells[rowIndex, colIndex++] = sv.SDT_THANNHAN;
                    worksheet.Cells[rowIndex, colIndex++] = sv.DIACHI_THANNHAN;

                    rowIndex++;
                }

                // ===== TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT =====
                worksheet.Columns.AutoFit();

                // Đặt chiều rộng tối đa cho một số cột dài
                worksheet.Columns[8].ColumnWidth = Math.Min(worksheet.Columns[8].ColumnWidth, 40); // HKTT
                worksheet.Columns[21].ColumnWidth = Math.Min(worksheet.Columns[21].ColumnWidth, 40); // Địa chỉ thân nhân

                // ===== VẼ VIỀN CHO BẢNG =====
                Excel.Range tableRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[rowIndex - 1, headers.Length]
                ];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // ===== BẬT AUTOFILTER ĐỂ DỄ LỌC =====
                worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, headers.Length]].AutoFilter(1);

                // ===== ĐÓNG BĂNG DÒNG HEADER =====
                worksheet.Application.ActiveWindow.SplitRow = 1;
                worksheet.Application.ActiveWindow.FreezePanes = true;

                // ===== LƯU FILE =====
                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();

                MessageBox.Show($"Xuất file Excel thành công!\nĐã xuất {danhSachMaSV.Count} hồ sơ sinh viên.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // MỞ FILE
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = saveDialog.FileName,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file Excel: {ex.Message}\n\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }
    }
}