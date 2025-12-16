using QuanLyKiTucXa.Formadd.QLDV_FORM;
using QuanLyKiTucXa.Formadd.QLHD_FORM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace QuanLyKiTucXa.Main_UC.QLDV
{
    public partial class UC_HD_INT : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_HD_INT()
        {
            InitializeComponent();
            LoadData();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            frm_addHD_INT form = new frm_addHD_INT();
            form.ShowDialog();

            LoadDataGridView();
            FilterData();
        }

        private void UC_HD_INT_Load(object sender, EventArgs e)
        {
            LoadComboBoxNha();
            LoadDataGridView();

            // Đăng ký sự kiện thay đổi
            comNHA.SelectedIndexChanged += Filter_Changed;
            comPHONG.SelectedIndexChanged += Filter_Changed;
            dtpTHOIGIAN.ValueChanged += Filter_Changed;
            //dtpTHOIGIAN.Value = DateTime.Now;
        }

        private void LoadData()
        {
            LoadComboBoxNha();
            LoadDataGridView();
        }

        private void LoadComboBoxNha()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MANHA FROM NHA ORDER BY MANHA";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm dòng "Tất cả"
                    DataRow row = dt.NewRow();
                    row["MANHA"] = "Tất cả";
                    dt.Rows.InsertAt(row, 0);

                    comNHA.DataSource = dt;
                    comNHA.DisplayMember = "MANHA";
                    comNHA.ValueMember = "MANHA";

                    comNHA.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message);
            }
        }

        private void LoadComboBoxPhong(string maNha)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT MA_PHONG FROM PHONG 
                                    WHERE MANHA = @MANHA 
                                    ORDER BY MA_PHONG";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MANHA", maNha);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm dòng "Tất cả"
                    DataRow row = dt.NewRow();
                    row["MA_PHONG"] = "Tất cả";
                    dt.Rows.InsertAt(row, 0);

                    comPHONG.DataSource = dt;
                    comPHONG.DisplayMember = "MA_PHONG";
                    comPHONG.ValueMember = "MA_PHONG";
                    comPHONG.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách phòng: " + ex.Message);
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC) AS STT,
                            HD.MAHD_INT,
                            N'Hóa đơn Internet' AS TENHD,
                            P.MANHA,
                            HD.MA_PHONG,
                            HD.DONGIA,
                            (
                                SELECT COUNT(DISTINCT HDP.MASV)
                                FROM HOPDONG HDP
                                WHERE HDP.MA_PHONG = HD.MA_PHONG
                                AND MONTH(HDP.TUNGAY) <= MONTH(HD.THOIGIAN) 
                                AND YEAR(HDP.TUNGAY) <= YEAR(HD.THOIGIAN)
                                AND (MONTH(HDP.DENNGAY) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.DENNGAY) >= YEAR(HD.THOIGIAN))
                                AND (HDP.NGAYKTTT IS NULL OR (MONTH(HDP.NGAYKTTT) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.NGAYKTTT) >= YEAR(HD.THOIGIAN)))
                            ) AS SO_SV,
                            HD.TONGTIEN,
                            FORMAT(HD.THOIGIAN, 'MM/yyyy') AS THOIGIAN,
                            HD.TINHTRANGTT
                        FROM HD_INTERNET HD
                        INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                        ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgv_HD_INT.DataSource = dt;

                    // Format tiền tệ
                    if (dgv_HD_INT.Columns["DONGIA"] != null)
                        dgv_HD_INT.Columns["DONGIA"].DefaultCellStyle.Format = "N0";

                    if (dgv_HD_INT.Columns["TONGTIEN"] != null)
                        dgv_HD_INT.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            // Load phòng khi thay đổi nhà
            if (sender == comNHA && comNHA.SelectedValue != null &&
                comNHA.SelectedValue.ToString() != "Tất cả")
            {
                LoadComboBoxPhong(comNHA.SelectedValue.ToString());
            }

            // Tự động lọc dữ liệu
            FilterData();
        }

        private void FilterData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC) AS STT,
                            HD.MAHD_INT,
                            N'Hóa đơn Internet' AS TENHD,
                            P.MANHA,
                            HD.MA_PHONG,
                            HD.DONGIA,
                            (
                                SELECT COUNT(DISTINCT HDP.MASV)
                                FROM HOPDONG HDP
                                WHERE HDP.MA_PHONG = HD.MA_PHONG
                                AND MONTH(HDP.TUNGAY) <= MONTH(HD.THOIGIAN) 
                                AND YEAR(HDP.TUNGAY) <= YEAR(HD.THOIGIAN)
                                AND (MONTH(HDP.DENNGAY) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.DENNGAY) >= YEAR(HD.THOIGIAN))
                                AND (HDP.NGAYKTTT IS NULL OR (MONTH(HDP.NGAYKTTT) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.NGAYKTTT) >= YEAR(HD.THOIGIAN)))
                            ) AS SO_SV,
                            HD.TONGTIEN,
                            FORMAT(HD.THOIGIAN, 'MM/yyyy') AS THOIGIAN,
                            HD.TINHTRANGTT
                        FROM HD_INTERNET HD
                        INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                        WHERE 1=1";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    // Lọc theo nhà
                    if (comNHA.SelectedValue != null && comNHA.SelectedValue.ToString() != "Tất cả")
                    {
                        query += " AND P.MANHA = @MANHA";
                        cmd.Parameters.AddWithValue("@MANHA", comNHA.SelectedValue.ToString());
                    }

                    // Lọc theo phòng
                    if (comPHONG.SelectedValue != null && comPHONG.SelectedValue.ToString() != "Tất cả")
                    {
                        query += " AND HD.MA_PHONG = @MA_PHONG";
                        cmd.Parameters.AddWithValue("@MA_PHONG", comPHONG.SelectedValue.ToString());
                    }

                    // Lọc theo tháng/năm
                    query += " AND MONTH(HD.THOIGIAN) = @THANG AND YEAR(HD.THOIGIAN) = @NAM";
                    cmd.Parameters.AddWithValue("@THANG", dtpTHOIGIAN.Value.Month);
                    cmd.Parameters.AddWithValue("@NAM", dtpTHOIGIAN.Value.Year);

                    query += " ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC";
                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgv_HD_INT.DataSource = dt;

                    // Format tiền tệ
                    if (dgv_HD_INT.Columns["DONGIA"] != null)
                        dgv_HD_INT.Columns["DONGIA"].DefaultCellStyle.Format = "N0";

                    if (dgv_HD_INT.Columns["TONGTIEN"] != null)
                        dgv_HD_INT.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // XÁC ĐỊNH DÒNG CẦN XUẤT
            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();

            if (dgv_HD_INT.SelectedRows.Count > 0)
            {
                // ✅ CHỈ XUẤT DÒNG ĐƯỢC CHỌN
                foreach (DataGridViewRow row in dgv_HD_INT.SelectedRows)
                {
                    if (!row.IsNewRow)
                        rowsToExport.Add(row);
                }
            }
            else
            {
                // ✅ XUẤT TẤT CẢ DÒNG
                foreach (DataGridViewRow row in dgv_HD_INT.Rows)
                {
                    if (!row.IsNewRow)
                        rowsToExport.Add(row);
                }
            }

            if (rowsToExport.Count == 0)
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
                FileName = $"HoaDonInternet_{dtpTHOIGIAN.Value:MMyyyy}_{DateTime.Now:ddMMyyyy_HHmmss}"
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
                worksheet.Name = "Hóa đơn Internet";

                // ===== TIÊU ĐỀ =====
                int currentRow = 1;
                worksheet.Cells[currentRow, 1] = "BÁO CÁO HÓA ĐƠN INTERNET";
                Excel.Range titleRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 9]];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleRange.Interior.Color = ColorTranslator.ToOle(Color.Purple);
                titleRange.Font.Color = ColorTranslator.ToOle(Color.White);
                currentRow++;

                // ===== THÔNG TIN BÁO CÁO =====
                worksheet.Cells[currentRow, 1] = $"Tháng:  {dtpTHOIGIAN.Value:MM/yyyy}";
                Excel.Range infoRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 9]];
                infoRange.Merge();
                infoRange.Font.Italic = true;
                infoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                currentRow++;

                // Thông tin lọc
                string filterInfo = "";
                if (comNHA.SelectedValue?.ToString() != "Tất cả")
                    filterInfo += $"Nhà: {comNHA.SelectedValue}  ";
                if (comPHONG.SelectedValue?.ToString() != "Tất cả")
                    filterInfo += $"Phòng: {comPHONG.SelectedValue}";

                if (!string.IsNullOrEmpty(filterInfo))
                {
                    worksheet.Cells[currentRow, 1] = filterInfo;
                    Excel.Range filterRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 9]];
                    filterRange.Merge();
                    filterRange.Font.Italic = true;
                    filterRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    currentRow++;
                }

                currentRow++; // Dòng trống

                // ===== HEADER (Tên cột) =====
                int colIndex = 1;
                string[] headers = new string[]
                {
            "STT",
            "Mã HĐ",
            "Tên Hóa Đơn",
            "Mã Nhà",
            "Mã Phòng",
            "Đơn giá/người",
            "Số sinh viên",
            "Tổng tiền",
            "Trạng thái"
                };

                foreach (string header in headers)
                {
                    worksheet.Cells[currentRow, colIndex] = header;

                    // Định dạng header
                    Excel.Range headerCell = worksheet.Cells[currentRow, colIndex];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.Plum);
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    headerCell.WrapText = true;

                    colIndex++;
                }
                currentRow++;

                // ===== DỮ LIỆU =====
                int stt = 1;
                int tongSoSV = 0;
                decimal tongTien = 0;
                int soHDDaTT = 0;
                int soHDChuaTT = 0;

                foreach (DataGridViewRow row in rowsToExport)
                {
                    colIndex = 1;

                    // STT
                    worksheet.Cells[currentRow, colIndex++] = stt++;

                    // Mã HĐ
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MAHD_INT"].Value?.ToString() ?? "";

                    // Tên Hóa Đơn
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["TENHD"].Value?.ToString() ?? "";

                    // Mã Nhà
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MANHA"].Value?.ToString() ?? "";

                    // Mã Phòng
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MA_PHONG"].Value?.ToString() ?? "";

                    // Đơn giá/người
                    if (row.Cells["DONGIA"].Value != null && row.Cells["DONGIA"].Value != DBNull.Value)
                    {
                        decimal donGia = Convert.ToDecimal(row.Cells["DONGIA"].Value);
                        worksheet.Cells[currentRow, colIndex] = donGia;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                    }
                    colIndex++;

                    // Số sinh viên
                    if (row.Cells["SO_SV"].Value != null && row.Cells["SO_SV"].Value != DBNull.Value)
                    {
                        int soSV = Convert.ToInt32(row.Cells["SO_SV"].Value);
                        worksheet.Cells[currentRow, colIndex] = soSV;
                        Excel.Range soSVCell = worksheet.Cells[currentRow, colIndex];
                        soSVCell.Font.Bold = true;
                        soSVCell.Interior.Color = ColorTranslator.ToOle(Color.LightCyan);
                        soSVCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        tongSoSV += soSV;
                    }
                    colIndex++;

                    // Tổng tiền
                    if (row.Cells["TONGTIEN"].Value != null && row.Cells["TONGTIEN"].Value != DBNull.Value)
                    {
                        decimal tongTienHD = Convert.ToDecimal(row.Cells["TONGTIEN"].Value);
                        worksheet.Cells[currentRow, colIndex] = tongTienHD;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                        Excel.Range tongTienCell = worksheet.Cells[currentRow, colIndex];
                        tongTienCell.Font.Bold = true;
                        tongTien += tongTienHD;
                    }
                    colIndex++;

                    // Trạng thái (có tô màu)
                    string trangThai = row.Cells["TINHTRANGTT"].Value?.ToString() ?? "";
                    worksheet.Cells[currentRow, colIndex] = trangThai;
                    Excel.Range statusCell = worksheet.Cells[currentRow, colIndex];
                    statusCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    if (trangThai == "Đã thanh toán")
                    {
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                        statusCell.Font.Bold = true;
                        soHDDaTT++;
                    }
                    else if (trangThai == "Chưa thanh toán")
                    {
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightCoral);
                        statusCell.Font.Bold = true;
                        soHDChuaTT++;
                    }

                    currentRow++;
                }

                // ===== TỔNG CỘNG =====
                currentRow++;
                worksheet.Cells[currentRow, 1] = "TỔNG CỘNG";
                Excel.Range tongCongLabel = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 6]];
                tongCongLabel.Merge();
                tongCongLabel.Font.Bold = true;
                tongCongLabel.Font.Size = 12;
                tongCongLabel.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                tongCongLabel.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);

                // Tổng số sinh viên
                worksheet.Cells[currentRow, 7] = tongSoSV;
                Excel.Range tongSVCell = worksheet.Cells[currentRow, 7];
                tongSVCell.Font.Bold = true;
                tongSVCell.Font.Size = 12;
                tongSVCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                tongSVCell.Interior.Color = ColorTranslator.ToOle(Color.LightCyan);

                // Tổng tiền
                worksheet.Cells[currentRow, 8] = tongTien;
                worksheet.Cells[currentRow, 8].NumberFormat = "#,##0";
                Excel.Range tongTienFinalCell = worksheet.Cells[currentRow, 8];
                tongTienFinalCell.Font.Bold = true;
                tongTienFinalCell.Font.Size = 12;
                tongTienFinalCell.Font.Color = ColorTranslator.ToOle(Color.Red);
                tongTienFinalCell.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);

                currentRow += 2;

                // ===== THỐNG KÊ =====
                worksheet.Cells[currentRow, 1] = "THỐNG KÊ CHI TIẾT";
                Excel.Range thongKeLabel = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 4]];
                thongKeLabel.Merge();
                thongKeLabel.Font.Bold = true;
                thongKeLabel.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                thongKeLabel.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Tổng số hóa đơn:";
                worksheet.Cells[currentRow, 2] = rowsToExport.Count + " hóa đơn";
                worksheet.Cells[currentRow, 2].Font.Bold = true;
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Đã thanh toán:";
                worksheet.Cells[currentRow, 2] = soHDDaTT + " hóa đơn";
                Excel.Range daTTCell = worksheet.Cells[currentRow, 2];
                daTTCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Chưa thanh toán:";
                worksheet.Cells[currentRow, 2] = soHDChuaTT + " hóa đơn";
                Excel.Range chuaTTCell = worksheet.Cells[currentRow, 2];
                chuaTTCell.Interior.Color = ColorTranslator.ToOle(Color.LightCoral);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Tổng số sinh viên:";
                worksheet.Cells[currentRow, 2] = tongSoSV + " sinh viên";
                worksheet.Cells[currentRow, 2].Font.Bold = true;
                worksheet.Cells[currentRow, 2].Interior.Color = ColorTranslator.ToOle(Color.LightCyan);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Trung bình SV/phòng:";
                double tbSV = rowsToExport.Count > 0 ? (double)tongSoSV / rowsToExport.Count : 0;
                worksheet.Cells[currentRow, 2] = tbSV.ToString("N1") + " sinh viên";
                worksheet.Cells[currentRow, 2].Font.Italic = true;

                // ===== ĐỊNH DẠNG CỘT =====
                worksheet.Columns[1].ColumnWidth = 6;   // STT
                worksheet.Columns[2].ColumnWidth = 20;  // Mã HĐ
                worksheet.Columns[3].ColumnWidth = 20;  // Tên HĐ
                worksheet.Columns[4].ColumnWidth = 10;  // Mã Nhà
                worksheet.Columns[5].ColumnWidth = 12;  // Mã Phòng
                worksheet.Columns[6].ColumnWidth = 15;  // Đơn giá
                worksheet.Columns[7].ColumnWidth = 14;  // Số SV
                worksheet.Columns[8].ColumnWidth = 15;  // Tổng tiền
                worksheet.Columns[9].ColumnWidth = 16;  // Trạng thái

                // Căn phải cho cột tiền
                worksheet.Columns[6].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                worksheet.Columns[8].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                // Căn giữa cho cột số SV
                worksheet.Columns[7].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // ===== VẼ VIỀN CHO BẢNG =====
                Excel.Range tableRange = worksheet.Range[
                    worksheet.Cells[4, 1], // Bắt đầu từ dòng header
                    worksheet.Cells[currentRow - 6, 9] // Đến dòng cuối cùng của bảng dữ liệu
                ];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // ===== BẬT AUTOFILTER =====
                Excel.Range headerRange = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 9]];
                headerRange.AutoFilter(1);

                // ===== ĐÓNG BĂNG DÒNG HEADER =====
                worksheet.Application.ActiveWindow.SplitRow = 4;
                worksheet.Application.ActiveWindow.FreezePanes = true;

                // ===== LƯU FILE =====
                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();

                MessageBox.Show(
                    $"Xuất file Excel thành công!\n\n" +
                    $"Tổng số hóa đơn: {rowsToExport.Count}\n" +
                    $"Tổng số sinh viên: {tongSoSV} sinh viên\n" +
                    $"Trung bình:  {tbSV: N1} SV/phòng\n" +
                    $"Tổng tiền: {tongTien:N0} VNĐ\n\n" +
                    $"Đã thanh toán: {soHDDaTT} hóa đơn\n" +
                    $"Chưa thanh toán: {soHDChuaTT} hóa đơn",
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
    }
}