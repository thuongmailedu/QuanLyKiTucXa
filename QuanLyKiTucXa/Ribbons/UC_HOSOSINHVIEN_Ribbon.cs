using QuanLyKiTucXa.Formadd.HSSV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

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
            // ✅ HIỂN THỊ DIALOG CHỌN CHỨC NĂNG
            DialogResult result = MessageBox.Show(
                "Bạn muốn thực hiện thao tác nào?\n\n" +
                "☑ YES - Xuất file mẫu Excel\n" +
                "☑ NO - Nhập dữ liệu từ file Excel\n" +
                "☑ CANCEL - Hủy thao tác",
                "Chức năng Import/Export Hồ Sơ Sinh Viên",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // XUẤT FILE MẪU
                ExportTemplateSinhVien();
            }
            else if (result == DialogResult.No)
            {
                // NHẬP DỮ LIỆU TỪ FILE
                ImportSinhVienFromExcel();
            }
        }
        private void ExportTemplateSinhVien()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"MauNhapSinhVien_{DateTime.Now:ddMMyyyy_HHmmss}.xlsx"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

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
                worksheet.Name = "SinhVien";

                // ===== TIÊU ĐỀ =====
                worksheet.Cells[1, 1] = "FILE MẪU NHẬP HỒ SƠ SINH VIÊN";
                Excel.Range title = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 21]];
                title.Merge();
                title.Font.Bold = true;
                title.Font.Size = 14;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                title.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);

                // ===== LƯU Ý =====
                worksheet.Cells[2, 1] = "LƯU Ý QUAN TRỌNG: ";
                Excel.Range noteTitle = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 21]];
                noteTitle.Merge();
                noteTitle.Font.Bold = true;
                noteTitle.Font.Color = ColorTranslator.ToOle(Color.Red);

                worksheet.Cells[3, 1] = "1.  Định dạng ngày:  dd/mm/yyyy (VD: 15/03/2004) - Format ô thành 'Date' trước khi nhập";
                Excel.Range note1 = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, 21]];
                note1.Merge();
                note1.Font.Italic = true;

                worksheet.Cells[4, 1] = "2. Các cột có dấu (*) là bắt buộc phải nhập";
                Excel.Range note2 = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 21]];
                note2.Merge();
                note2.Font.Italic = true;

                worksheet.Cells[5, 1] = "3. Nhập thông tin thân nhân để có người liên hệ khi cần thiết";
                Excel.Range note3 = worksheet.Range[worksheet.Cells[5, 1], worksheet.Cells[5, 21]];
                note3.Merge();
                note3.Font.Italic = true;

                // ===== HEADER =====
                int headerRow = 7;
                string[] headers = new string[]
                {
            "Mã SV *",
            "Tên SV *",
            "Giới tính *",
            "Ngày sinh *\n(dd/mm/yyyy)",
            "CCCD *",
            "SĐT",
            "Hộ khẩu\nthường trú",
            "Mã lớp *",
            "Tên lớp",
            "Mã khoa",
            "Tên khoa",
            "Mã phòng",
            "Mã nhà",
            "Loại phòng",
            "Từ ngày\n(dd/mm/yyyy)",
            "Đến ngày\n(dd/mm/yyyy)",
            "Ngày thanh lý\n(dd/mm/yyyy)",
            "Họ tên\nthân nhân",
            "Mối quan hệ",
            "SĐT thân nhân",
            "Địa chỉ\nthân nhân"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[headerRow, i + 1] = headers[i];
                    Excel.Range headerCell = worksheet.Cells[headerRow, i + 1];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    headerCell.WrapText = true;
                }

                // ===== DỮ LIỆU MẪU =====
                int dataRow = headerRow + 1;

                // Mẫu 1: Sinh viên đang ở KTX
                worksheet.Cells[dataRow, 1] = "2151010001";
                worksheet.Cells[dataRow, 2] = "Nguyễn Văn An";
                worksheet.Cells[dataRow, 3] = "Nam";
                worksheet.Cells[dataRow, 4] = "15/03/2003";
                worksheet.Cells[dataRow, 5] = "001203012345";
                worksheet.Cells[dataRow, 6] = "0901234567";
                worksheet.Cells[dataRow, 7] = "123 Đường ABC, Phường XYZ, Quận 1, TP. HCM";
                worksheet.Cells[dataRow, 8] = "CNTT01";
                worksheet.Cells[dataRow, 9] = "Công nghệ thông tin 01";
                worksheet.Cells[dataRow, 10] = "CNTT";
                worksheet.Cells[dataRow, 11] = "Khoa Công nghệ thông tin";
                worksheet.Cells[dataRow, 12] = "A101";
                worksheet.Cells[dataRow, 13] = "A";
                worksheet.Cells[dataRow, 14] = "Phòng 4 người";
                worksheet.Cells[dataRow, 15] = "01/09/2024";
                worksheet.Cells[dataRow, 16] = "31/08/2025";
                worksheet.Cells[dataRow, 17] = ""; // Chưa thanh lý
                worksheet.Cells[dataRow, 18] = "Nguyễn Thị Bình";
                worksheet.Cells[dataRow, 19] = "Mẹ";
                worksheet.Cells[dataRow, 20] = "0912345678";
                worksheet.Cells[dataRow, 21] = "123 Đường ABC, Phường XYZ, Quận 1, TP.HCM";

                dataRow++;

                // Mẫu 2: Sinh viên chưa có phòng
                worksheet.Cells[dataRow, 1] = "2151010002";
                worksheet.Cells[dataRow, 2] = "Trần Thị Mai";
                worksheet.Cells[dataRow, 3] = "Nữ";
                worksheet.Cells[dataRow, 4] = "20/07/2003";
                worksheet.Cells[dataRow, 5] = "001203098765";
                worksheet.Cells[dataRow, 6] = "0987654321";
                worksheet.Cells[dataRow, 7] = "456 Đường DEF, Phường UVW, Quận 2, TP.HCM";
                worksheet.Cells[dataRow, 8] = "KTDL01";
                worksheet.Cells[dataRow, 9] = "Kế toán - Dữ liệu 01";
                worksheet.Cells[dataRow, 10] = "KTDL";
                worksheet.Cells[dataRow, 11] = "Khoa Kế toán - Dữ liệu";
                worksheet.Cells[dataRow, 12] = ""; // Chưa có phòng
                worksheet.Cells[dataRow, 13] = "";
                worksheet.Cells[dataRow, 14] = "";
                worksheet.Cells[dataRow, 15] = "";
                worksheet.Cells[dataRow, 16] = "";
                worksheet.Cells[dataRow, 17] = "";
                worksheet.Cells[dataRow, 18] = "Trần Văn Hùng";
                worksheet.Cells[dataRow, 19] = "Bố";
                worksheet.Cells[dataRow, 20] = "0923456789";
                worksheet.Cells[dataRow, 21] = "456 Đường DEF, Phường UVW, Quận 2, TP.HCM";

                // ===== FORMAT CÁC CỘT NGÀY =====
                Excel.Range dateCol1 = worksheet.Range[worksheet.Cells[dataRow - 1, 4], worksheet.Cells[dataRow + 100, 4]];
                dateCol1.NumberFormat = "dd/mm/yyyy";

                Excel.Range dateCol2 = worksheet.Range[worksheet.Cells[dataRow - 1, 15], worksheet.Cells[dataRow + 100, 17]];
                dateCol2.NumberFormat = "dd/mm/yyyy";

                // ===== ĐỊNH DẠNG CỘT =====
                worksheet.Columns[1].ColumnWidth = 12;  // Mã SV
                worksheet.Columns[2].ColumnWidth = 20;  // Tên SV
                worksheet.Columns[3].ColumnWidth = 10;  // Giới tính
                worksheet.Columns[4].ColumnWidth = 13;  // Ngày sinh
                worksheet.Columns[5].ColumnWidth = 13;  // CCCD
                worksheet.Columns[6].ColumnWidth = 12;  // SĐT
                worksheet.Columns[7].ColumnWidth = 40;  // HKTT
                worksheet.Columns[8].ColumnWidth = 10;  // Mã lớp
                worksheet.Columns[9].ColumnWidth = 25;  // Tên lớp
                worksheet.Columns[10].ColumnWidth = 10; // Mã khoa
                worksheet.Columns[11].ColumnWidth = 25; // Tên khoa
                worksheet.Columns[12].ColumnWidth = 10; // Mã phòng
                worksheet.Columns[13].ColumnWidth = 8;  // Mã nhà
                worksheet.Columns[14].ColumnWidth = 15; // Loại phòng
                worksheet.Columns[15].ColumnWidth = 12; // Từ ngày
                worksheet.Columns[16].ColumnWidth = 12; // Đến ngày
                worksheet.Columns[17].ColumnWidth = 12; // Ngày TL
                worksheet.Columns[18].ColumnWidth = 20; // Tên thân nhân
                worksheet.Columns[19].ColumnWidth = 12; // Mối quan hệ
                worksheet.Columns[20].ColumnWidth = 12; // SĐT TN
                worksheet.Columns[21].ColumnWidth = 40; // Địa chỉ TN

                // ===== VẼ VIỀN =====
                Excel.Range tableRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[dataRow + 50, 21]];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // ===== LƯU FILE =====
                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();

                MessageBox.Show(
                    "Xuất file mẫu thành công!\n\n" +
                    "📄 File chứa 21 cột thông tin đầy đủ:\n" +
                    "  - Thông tin cá nhân (7 cột)\n" +
                    "  - Thông tin học vấn (4 cột)\n" +
                    "  - Thông tin phòng ở (6 cột)\n" +
                    "  - Thông tin thân nhân (4 cột)\n\n" +
                    "⚠️ Lưu ý: Format ô ngày thành 'Date' trước khi nhập! ",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = saveDialog.FileName,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file mẫu: {ex.Message}",
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

        private void ImportSinhVienFromExcel()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                Title = "Chọn file Excel để nhập sinh viên"
            };

            if (openDialog.ShowDialog() != DialogResult.OK) return;

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Open(openDialog.FileName);
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                // ĐỌC DỮ LIỆU TỪ EXCEL
                List<SinhVienImportData> danhSachSV = ReadSinhVienFromExcel(worksheet);

                workbook.Close(false);

                if (danhSachSV.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hợp lệ để import!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // IMPORT VÀO DATABASE
                ImportSinhVienToDatabase(danhSachSV);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập dữ liệu: {ex.Message}\n\n{ex.StackTrace}",
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

        // Helper class
        private class SinhVienImportData
        {
            // Thông tin cá nhân
            public string MASV { get; set; }
            public string TENSV { get; set; }
            public string GIOITINH { get; set; }
            public DateTime? NGAYSINH { get; set; }
            public string CCCD { get; set; }
            public string SDT { get; set; }
            public string HKTT { get; set; }
            public string MALOP { get; set; }

            // Thông tin phòng ở (nếu có)
            public string MA_PHONG { get; set; }
            public DateTime? TUNGAY { get; set; }
            public DateTime? DENNGAY { get; set; }
            public DateTime? NGAYKTTT { get; set; }

            // Thông tin thân nhân
            public string TEN_THANNHAN { get; set; }
            public string SDT_THANNHAN { get; set; }
            public string MOIQUANHE { get; set; }
            public string DIACHI_THANNHAN { get; set; }
        }

        private List<SinhVienImportData> ReadSinhVienFromExcel(Excel.Worksheet worksheet)
        {
            List<SinhVienImportData> list = new List<SinhVienImportData>();
            int row = 8; // Bắt đầu từ dòng 8 (sau header)

            while (true)
            {
                string maSV = worksheet.Cells[row, 1].Value?.ToString()?.Trim();

                if (string.IsNullOrEmpty(maSV))
                    break;

                try
                {
                    SinhVienImportData sv = new SinhVienImportData
                    {
                        MASV = maSV,
                        TENSV = worksheet.Cells[row, 2].Value?.ToString()?.Trim(),
                        GIOITINH = worksheet.Cells[row, 3].Value?.ToString()?.Trim(),
                        NGAYSINH = ParseExcelDateNullable(worksheet.Cells[row, 4].Value),
                        CCCD = worksheet.Cells[row, 5].Value?.ToString()?.Trim(),
                        SDT = worksheet.Cells[row, 6].Value?.ToString()?.Trim(),
                        HKTT = worksheet.Cells[row, 7].Value?.ToString()?.Trim(),
                        MALOP = worksheet.Cells[row, 8].Value?.ToString()?.Trim(),
                        MA_PHONG = worksheet.Cells[row, 12].Value?.ToString()?.Trim(),
                        TUNGAY = ParseExcelDateNullable(worksheet.Cells[row, 15].Value),
                        DENNGAY = ParseExcelDateNullable(worksheet.Cells[row, 16].Value),
                        NGAYKTTT = ParseExcelDateNullable(worksheet.Cells[row, 17].Value),
                        TEN_THANNHAN = worksheet.Cells[row, 18].Value?.ToString()?.Trim(),
                        MOIQUANHE = worksheet.Cells[row, 19].Value?.ToString()?.Trim(),
                        SDT_THANNHAN = worksheet.Cells[row, 20].Value?.ToString()?.Trim(),
                        DIACHI_THANNHAN = worksheet.Cells[row, 21].Value?.ToString()?.Trim()
                    };

                    // Validate bắt buộc
                    if (string.IsNullOrEmpty(sv.TENSV) || string.IsNullOrEmpty(sv.MALOP))
                    {
                        throw new Exception($"Dòng {row}: Thiếu tên sinh viên hoặc mã lớp");
                    }

                    list.Add(sv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi đọc dòng {row}: {ex.Message}", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                row++;
            }

            return list;
        }

        // Helper methods (dùng chung)
        private DateTime? ParseExcelDateNullable(object value)
        {
            if (value == null) return null;
            if (value is DateTime dt) return dt;
            if (value is double dbl) return DateTime.FromOADate(dbl);
            if (DateTime.TryParse(value.ToString(), out DateTime result)) return result;
            return null;
        }

        private void ImportSinhVienToDatabase(List<SinhVienImportData> danhSachSV)
        {
            int soSVThem = 0;
            int soSVBiTrung = 0;
            int soThanNhanThem = 0;
            int soHopDongThem = 0;
            List<string> errors = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        foreach (var sv in danhSachSV)
                        {
                            // 1. KIỂM TRA & INSERT SINH VIÊN
                            string checkSVQuery = "SELECT COUNT(*) FROM SINHVIEN WHERE MASV = @MASV";
                            using (SqlCommand checkCmd = new SqlCommand(checkSVQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@MASV", sv.MASV);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    soSVBiTrung++;
                                    continue; // Bỏ qua nếu sinh viên đã tồn tại
                                }
                            }

                            // Insert sinh viên
                            string insertSVQuery = @"INSERT INTO SINHVIEN 
                        (MASV, TENSV, NGAYSINH, GIOITINH, CCCD, SDT, MALOP, HKTT)
                        VALUES (@MASV, @TENSV, @NGAYSINH, @GIOITINH, @CCCD, @SDT, @MALOP, @HKTT)";

                            using (SqlCommand insertCmd = new SqlCommand(insertSVQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@MASV", sv.MASV);
                                insertCmd.Parameters.AddWithValue("@TENSV", sv.TENSV);
                                insertCmd.Parameters.AddWithValue("@NGAYSINH", (object)sv.NGAYSINH ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@GIOITINH", (object)sv.GIOITINH ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@CCCD", (object)sv.CCCD ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@SDT", (object)sv.SDT ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@MALOP", sv.MALOP);
                                insertCmd.Parameters.AddWithValue("@HKTT", (object)sv.HKTT ?? DBNull.Value);

                                insertCmd.ExecuteNonQuery();
                                soSVThem++;
                            }

                            // 2. INSERT THÂN NHÂN (nếu có)
                            if (!string.IsNullOrEmpty(sv.TEN_THANNHAN))
                            {
                                string insertTNQuery = @"INSERT INTO THANNHAN 
                            (MASV, TEN_THANNHAN, SDT, MOIQUANHE, DIACHI)
                            VALUES (@MASV, @TEN_THANNHAN, @SDT, @MOIQUANHE, @DIACHI)";

                                using (SqlCommand insertTNCmd = new SqlCommand(insertTNQuery, conn, transaction))
                                {
                                    insertTNCmd.Parameters.AddWithValue("@MASV", sv.MASV);
                                    insertTNCmd.Parameters.AddWithValue("@TEN_THANNHAN", sv.TEN_THANNHAN);
                                    insertTNCmd.Parameters.AddWithValue("@SDT", (object)sv.SDT_THANNHAN ?? DBNull.Value);
                                    insertTNCmd.Parameters.AddWithValue("@MOIQUANHE", (object)sv.MOIQUANHE ?? DBNull.Value);
                                    insertTNCmd.Parameters.AddWithValue("@DIACHI", (object)sv.DIACHI_THANNHAN ?? DBNull.Value);

                                    insertTNCmd.ExecuteNonQuery();
                                    soThanNhanThem++;
                                }
                            }

                            // 3. INSERT HỢP ĐỒNG (nếu có thông tin phòng)
                            if (!string.IsNullOrEmpty(sv.MA_PHONG) && sv.TUNGAY.HasValue && sv.DENNGAY.HasValue)
                            {
                                string maHD = "HD_" + sv.MASV + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                                string insertHDQuery = @"INSERT INTO HOPDONG 
                            (MAHD, MASV, MA_PHONG, TUNGAY, DENNGAY, NGAYKTTT, TENHD)
                            VALUES (@MAHD, @MASV, @MA_PHONG, @TUNGAY, @DENNGAY, @NGAYKTTT, @TENHD)";

                                using (SqlCommand insertHDCmd = new SqlCommand(insertHDQuery, conn, transaction))
                                {
                                    insertHDCmd.Parameters.AddWithValue("@MAHD", maHD);
                                    insertHDCmd.Parameters.AddWithValue("@MASV", sv.MASV);
                                    insertHDCmd.Parameters.AddWithValue("@MA_PHONG", sv.MA_PHONG);
                                    insertHDCmd.Parameters.AddWithValue("@TUNGAY", sv.TUNGAY.Value);
                                    insertHDCmd.Parameters.AddWithValue("@DENNGAY", sv.DENNGAY.Value);
                                    insertHDCmd.Parameters.AddWithValue("@NGAYKTTT", (object)sv.NGAYKTTT ?? DBNull.Value);
                                    insertHDCmd.Parameters.AddWithValue("@TENHD", "Hợp đồng thuê phòng KTX");

                                    insertHDCmd.ExecuteNonQuery();
                                    soHopDongThem++;
                                }
                            }
                        }

                        transaction.Commit();

                        string message = "✅ IMPORT SINH VIÊN THÀNH CÔNG!\n\n";
                        message += $"📊 KẾT QUẢ:\n";
                        message += $"  - Sinh viên thêm mới: {soSVThem}\n";
                        message += $"  - Sinh viên bị trùng: {soSVBiTrung}\n";
                        message += $"  - Thân nhân thêm mới: {soThanNhanThem}\n";
                        message += $"  - Hợp đồng thêm mới: {soHopDongThem}\n";

                        if (errors.Count > 0)
                        {
                            message += $"\n⚠️ CÓ {errors.Count} LỖI:\n";
                            message += string.Join("\n", errors.Take(5));
                        }

                        MessageBox.Show(message, "Kết quả Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh dữ liệu
                        LoadDanhSachSinhVien();
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
                MessageBox.Show($"Lỗi khi import vào database: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





    }
}