using QuanLyKiTucXa.Formadd.QLDV_FORM;
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
    public partial class UC_HD_DIEN : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_HD_DIEN()
        {
            InitializeComponent();
            LoadData();
        }

        private void UC_HD_DIEN_Load(object sender, EventArgs e)
        {
            LoadComboBoxNha();
            LoadComboBoxTrangThai();
            LoadDataGridView();
           // dtpTHOIGIAN.Value = DateTime.Now;
            // Gắn sự kiện
            // btnedit.Click += btnedit_Click;
            //btnupdate.Click += btnupdate_Click;
            // btncancel.Click += btncancel_Click;
        }

        private void LoadData()
        {
            LoadComboBoxNha();
            LoadComboBoxTrangThai();
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

                    DataRow row = dt.NewRow();
                    row["MANHA"] = "Tất cả";
                    dt.Rows.InsertAt(row, 0);

                    comNHA.DataSource = dt;
                    comNHA.DisplayMember = "MANHA";
                    comNHA.ValueMember = "MANHA";
                    comNHA.SelectedIndex = 0;

                    comNHA.SelectedIndexChanged += comNHA_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message);
            }
        }

        private void LoadComboBoxTrangThai()
        {
            comTRANGTHAI.Items.Clear();
            comTRANGTHAI.Items.Add("Tất cả");
            comTRANGTHAI.Items.Add("Đã thanh toán");
            comTRANGTHAI.Items.Add("Chưa thanh toán");
            comTRANGTHAI.SelectedIndex = 0;
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue != null && comNHA.SelectedValue.ToString() != "Tất cả")
            {
                LoadComboBoxPhong(comNHA.SelectedValue.ToString());
            }
            else
            {
                comPHONG.DataSource = null;
                comPHONG.Items.Clear();
                comPHONG.Items.Add("Tất cả");
                comPHONG.SelectedIndex = 0;
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
                            ROW_NUMBER() OVER (ORDER BY HD.THOIGIAN DESC, HD.MAHD_DIEN DESC) AS STT,
                            HD.MADV,
                            HD.MAHD_DIEN,
                            N'Hóa đơn tiền điện' AS TENHD,
                            P.MANHA,
                            HD.MA_PHONG,
                            HD.CHISOCU,
                            HD.CHISOMOI,
                            (HD.CHISOMOI - HD.CHISOCU) AS SODIEN,
                            HD.DONGIA,
                            HD.TONGTIEN,
                            FORMAT(HD.THOIGIAN, 'MM/yyyy') AS THOIGIAN,
                            FORMAT(HD.NGAYGHI, 'dd/MM/yyyy') AS NGAYGHI,
                            HD.TINHTRANGTT
                        FROM HD_DIEN HD
                        INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                        ORDER BY HD.THOIGIAN DESC, HD.MAHD_DIEN DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgv_HD_DIEN.DataSource = dt;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        private void FormatDataGridView()
        {
            if (dgv_HD_DIEN.Columns["DONGIA"] != null)
                dgv_HD_DIEN.Columns["DONGIA"].DefaultCellStyle.Format = "N0";

            if (dgv_HD_DIEN.Columns["TONGTIEN"] != null)
                dgv_HD_DIEN.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
        }

        private void btnfillter_Click(object sender, EventArgs e)
        {
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
                            ROW_NUMBER() OVER (ORDER BY HD.THOIGIAN DESC, HD.MAHD_DIEN DESC) AS STT,
                            HD.MADV,
                            HD.MAHD_DIEN,
                            N'Hóa đơn tiền điện' AS TENHD,
                            P.MANHA,
                            HD.MA_PHONG,
                            HD.CHISOCU,
                            HD.CHISOMOI,
                            (HD.CHISOMOI - HD.CHISOCU) AS SODIEN,
                            HD.DONGIA,
                            HD.TONGTIEN,
                            FORMAT(HD.THOIGIAN, 'MM/yyyy') AS THOIGIAN,
                            FORMAT(HD.NGAYGHI, 'dd/MM/yyyy') AS NGAYGHI,
                            HD.TINHTRANGTT
                        FROM HD_DIEN HD
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

                    // Lọc theo trạng thái
                    if (comTRANGTHAI.SelectedItem != null && comTRANGTHAI.SelectedItem.ToString() != "Tất cả")
                    {
                        query += " AND HD.TINHTRANGTT = @TRANGTHAI";
                        cmd.Parameters.AddWithValue("@TRANGTHAI", comTRANGTHAI.SelectedItem.ToString());
                    }

                    // Lọc theo tháng/năm
                    query += " AND MONTH(HD.THOIGIAN) = @THANG AND YEAR(HD.THOIGIAN) = @NAM";
                    cmd.Parameters.AddWithValue("@THANG", dtpTHOIGIAN.Value.Month);
                    cmd.Parameters.AddWithValue("@NAM", dtpTHOIGIAN.Value.Year);

                    query += " ORDER BY HD.THOIGIAN DESC, HD.MAHD_DIEN DESC";
                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgv_HD_DIEN.DataSource = dt;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            frm_addHD_DIEN form = new frm_addHD_DIEN();
            form.FormClosed += (s, args) => LoadDataGridView();
            form.ShowDialog();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dgv_HD_DIEN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgv_HD_DIEN.SelectedRows[0];

            // Truyền thông tin sang form chỉnh sửa
            frm_addHD_DIEN form = new frm_addHD_DIEN(
                row.Cells["MAHD_DIEN"].Value.ToString(),
                row.Cells["TENHD"].Value.ToString(),
                row.Cells["MADV"].Value.ToString(),
                row.Cells["MANHA"].Value.ToString(),
                row.Cells["MA_PHONG"].Value.ToString(),
                Convert.ToDouble(row.Cells["CHISOCU"].Value),
                Convert.ToDouble(row.Cells["CHISOMOI"].Value),
                Convert.ToDecimal(row.Cells["DONGIA"].Value),
                row.Cells["THOIGIAN"].Value.ToString(),
                row.Cells["NGAYGHI"].Value.ToString(),
                row.Cells["TINHTRANGTT"].Value.ToString()
            );

            form.FormClosed += (s, args) => LoadDataGridView();
            form.ShowDialog();
        }

        // ← HÀM MỚI: Cập nhật trạng thái "Đã thanh toán"
        private void btnupdate_Click(object sender, EventArgs e)
        {
            UpdateTrangThaiThanhToan("Đã thanh toán");
        }

        // ← HÀM MỚI: Cập nhật trạng thái "Chưa thanh toán"
        private void btncancel_Click(object sender, EventArgs e)
        {
            UpdateTrangThaiThanhToan("Chưa thanh toán");
        }

        // ← HÀM MỚI: Cập nhật trạng thái cho nhiều hóa đơn
        private void UpdateTrangThaiThanhToan(string trangThai)
        {
            // Kiểm tra có dòng nào được chọn không
            if (dgv_HD_DIEN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hóa đơn để cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy danh sách mã hóa đơn được chọn
            List<string> danhSachMaHD = new List<string>();
            foreach (DataGridViewRow row in dgv_HD_DIEN.SelectedRows)
            {
                string maHD = row.Cells["MAHD_DIEN"].Value.ToString();
                danhSachMaHD.Add(maHD);
            }

            // Xác nhận
            string message = $"Bạn có chắc muốn cập nhật {danhSachMaHD.Count} hóa đơn sang trạng thái '{trangThai}'?";
            if (MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Cập nhật từng hóa đơn
                        string updateQuery = @"UPDATE HD_DIEN 
                                             SET TINHTRANGTT = @TINHTRANGTT 
                                             WHERE MAHD_DIEN = @MAHD_DIEN";

                        int soLuongCapNhat = 0;
                        foreach (string maHD in danhSachMaHD)
                        {
                            SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction);
                            cmd.Parameters.AddWithValue("@TINHTRANGTT", trangThai);
                            cmd.Parameters.AddWithValue("@MAHD_DIEN", maHD);

                            soLuongCapNhat += cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show($"Đã cập nhật {soLuongCapNhat} hóa đơn sang trạng thái '{trangThai}'!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh lại dữ liệu
                        LoadDataGridView();
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
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            LoadDataGridView();
        }

        private void UC_HD_DIEN_Load_1(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // XÁC ĐỊNH DÒNG CẦN XUẤT
            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();

            if (dgv_HD_DIEN.SelectedRows.Count > 0)
            {
                // ✅ CHỈ XUẤT DÒNG ĐƯỢC CHỌN
                foreach (DataGridViewRow row in dgv_HD_DIEN.SelectedRows)
                {
                    if (!row.IsNewRow)
                        rowsToExport.Add(row);
                }
            }
            else
            {
                // ✅ XUẤT TẤT CẢ DÒNG
                foreach (DataGridViewRow row in dgv_HD_DIEN.Rows)
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
                FileName = $"HoaDonDien_{dtpTHOIGIAN.Value:MMyyyy}_{DateTime.Now:ddMMyyyy_HHmmss}"
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
                worksheet.Name = "Hóa đơn điện";

                // ===== TIÊU ĐỀ =====
                int currentRow = 1;
                worksheet.Cells[currentRow, 1] = "BÁO CÁO HÓA ĐƠN TIỀN ĐIỆN";
                Excel.Range titleRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 13]];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleRange.Interior.Color = ColorTranslator.ToOle(Color.Orange);
                titleRange.Font.Color = ColorTranslator.ToOle(Color.White);
                currentRow++;

                // ===== THÔNG TIN BÁO CÁO =====
                worksheet.Cells[currentRow, 1] = $"Tháng:  {dtpTHOIGIAN.Value:MM/yyyy}";
                Excel.Range infoRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 13]];
                infoRange.Merge();
                infoRange.Font.Italic = true;
                infoRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                currentRow++;

                // Thông tin lọc
                string filterInfo = "";
                if (comNHA.SelectedValue?.ToString() != "Tất cả")
                    filterInfo += $"Nhà:  {comNHA.SelectedValue}  ";
                if (comPHONG.SelectedValue?.ToString() != "Tất cả")
                    filterInfo += $"Phòng: {comPHONG.SelectedValue}  ";
                if (comTRANGTHAI.SelectedItem?.ToString() != "Tất cả")
                    filterInfo += $"Trạng thái: {comTRANGTHAI.SelectedItem}";

                if (!string.IsNullOrEmpty(filterInfo))
                {
                    worksheet.Cells[currentRow, 1] = filterInfo;
                    Excel.Range filterRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 13]];
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
            "Mã DV",
            "Tên Hóa Đơn",
            "Mã Nhà",
            "Mã Phòng",
            "Chỉ số cũ (kWh)",
            "Chỉ số mới (kWh)",
            "Số điện (kWh)",
            "Đơn giá",
            "Tổng tiền",
            "Ngày ghi",
            "Trạng thái"
                };

                foreach (string header in headers)
                {
                    worksheet.Cells[currentRow, colIndex] = header;

                    // Định dạng header
                    Excel.Range headerCell = worksheet.Cells[currentRow, colIndex];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.LightGoldenrodYellow);
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    headerCell.WrapText = true;

                    colIndex++;
                }
                currentRow++;

                // ===== DỮ LIỆU =====
                int stt = 1;
                double tongSoDien = 0;
                decimal tongTien = 0;
                int soHDDaTT = 0;
                int soHDChuaTT = 0;

                foreach (DataGridViewRow row in rowsToExport)
                {
                    colIndex = 1;

                    // STT
                    worksheet.Cells[currentRow, colIndex++] = stt++;

                    // Mã HĐ
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MAHD_DIEN"].Value?.ToString() ?? "";

                    // Mã DV
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MADV"].Value?.ToString() ?? "";

                    // Tên Hóa Đơn
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["TENHD"].Value?.ToString() ?? "";

                    // Mã Nhà
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MANHA"].Value?.ToString() ?? "";

                    // Mã Phòng
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MA_PHONG"].Value?.ToString() ?? "";

                    // Chỉ số cũ
                    if (row.Cells["CHISOCU"].Value != null && row.Cells["CHISOCU"].Value != DBNull.Value)
                    {
                        double chiSoCu = Convert.ToDouble(row.Cells["CHISOCU"].Value);
                        worksheet.Cells[currentRow, colIndex] = chiSoCu;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0.0";
                    }
                    colIndex++;

                    // Chỉ số mới
                    if (row.Cells["CHISOMOI"].Value != null && row.Cells["CHISOMOI"].Value != DBNull.Value)
                    {
                        double chiSoMoi = Convert.ToDouble(row.Cells["CHISOMOI"].Value);
                        worksheet.Cells[currentRow, colIndex] = chiSoMoi;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0.0";
                    }
                    colIndex++;

                    // Số điện (kWh)
                    if (row.Cells["SODIEN"].Value != null && row.Cells["SODIEN"].Value != DBNull.Value)
                    {
                        double soDien = Convert.ToDouble(row.Cells["SODIEN"].Value);
                        worksheet.Cells[currentRow, colIndex] = soDien;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0.0";
                        Excel.Range soDienCell = worksheet.Cells[currentRow, colIndex];
                        soDienCell.Font.Bold = true;
                        soDienCell.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
                        tongSoDien += soDien;
                    }
                    colIndex++;

                    // Đơn giá
                    if (row.Cells["DONGIA"].Value != null && row.Cells["DONGIA"].Value != DBNull.Value)
                    {
                        decimal donGia = Convert.ToDecimal(row.Cells["DONGIA"].Value);
                        worksheet.Cells[currentRow, colIndex] = donGia;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                    }
                    colIndex++;

                    // Tổng tiền
                    if (row.Cells["TONGTIEN"].Value != null && row.Cells["TONGTIEN"].Value != DBNull.Value)
                    {
                        decimal tongTienHD = Convert.ToDecimal(row.Cells["TONGTIEN"].Value);
                        worksheet.Cells[currentRow, colIndex] = tongTienHD;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                        Excel.Range TongTienCell = worksheet.Cells[currentRow, colIndex];
                        TongTienCell.Font.Bold = true;
                        tongTien += tongTienHD;
                    }
                    colIndex++;

                    // Ngày ghi
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["NGAYGHI"].Value?.ToString() ?? "";

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
                Excel.Range tongCongLabel = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 8]];
                tongCongLabel.Merge();
                tongCongLabel.Font.Bold = true;
                tongCongLabel.Font.Size = 12;
                tongCongLabel.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                tongCongLabel.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);

                // Tổng số điện
                worksheet.Cells[currentRow, 9] = tongSoDien;
                worksheet.Cells[currentRow, 9].NumberFormat = "#,##0.0";
                worksheet.Cells[currentRow, 9].Font.Bold = true;
                worksheet.Cells[currentRow, 9].Interior.Color = ColorTranslator.ToOle(Color.LightYellow);

                // Bỏ qua cột đơn giá
                worksheet.Cells[currentRow, 10] = "";

                // Tổng tiền
                worksheet.Cells[currentRow, 11] = tongTien;
                worksheet.Cells[currentRow, 11].NumberFormat = "#,##0";
                Excel.Range tongTienCell = worksheet.Cells[currentRow, 11];
                tongTienCell.Font.Bold = true;
                tongTienCell.Font.Size = 12;
                tongTienCell.Font.Color = ColorTranslator.ToOle(Color.Red);
                tongTienCell.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);

                currentRow += 2;

                // ===== THỐNG KÊ =====
                worksheet.Cells[currentRow, 1] = "THỐNG KÊ TRẠNG THÁI";
                Excel.Range thongKeLabel = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 4]];
                thongKeLabel.Merge();
                thongKeLabel.Font.Bold = true;
                thongKeLabel.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                thongKeLabel.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Đã thanh toán: ";
                worksheet.Cells[currentRow, 2] = soHDDaTT + " hóa đơn";
                Excel.Range daTTCell = worksheet.Cells[currentRow, 2];
                daTTCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Chưa thanh toán:";
                worksheet.Cells[currentRow, 2] = soHDChuaTT + " hóa đơn";
                Excel.Range chuaTTCell = worksheet.Cells[currentRow, 2];
                chuaTTCell.Interior.Color = ColorTranslator.ToOle(Color.LightCoral);
                currentRow++;

                worksheet.Cells[currentRow, 1] = "Tổng số hóa đơn:";
                worksheet.Cells[currentRow, 2] = rowsToExport.Count + " hóa đơn";
                worksheet.Cells[currentRow, 2].Font.Bold = true;

                // ===== ĐỊNH DẠNG CỘT =====
                worksheet.Columns[1].ColumnWidth = 6;   // STT
                worksheet.Columns[2].ColumnWidth = 18;  // Mã HĐ
                worksheet.Columns[3].ColumnWidth = 10;  // Mã DV
                worksheet.Columns[4].ColumnWidth = 22;  // Tên HĐ
                worksheet.Columns[5].ColumnWidth = 10;  // Mã Nhà
                worksheet.Columns[6].ColumnWidth = 12;  // Mã Phòng
                worksheet.Columns[7].ColumnWidth = 15;  // Chỉ số cũ
                worksheet.Columns[8].ColumnWidth = 15;  // Chỉ số mới
                worksheet.Columns[9].ColumnWidth = 14;  // Số điện
                worksheet.Columns[10].ColumnWidth = 13; // Đơn giá
                worksheet.Columns[11].ColumnWidth = 15; // Tổng tiền
                worksheet.Columns[12].ColumnWidth = 12; // Ngày ghi
                worksheet.Columns[13].ColumnWidth = 16; // Trạng thái

                // Căn giữa cho các cột chỉ số
                for (int i = 7; i <= 9; i++)
                {
                    worksheet.Columns[i].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                // Căn phải cho các cột tiền
                for (int i = 10; i <= 11; i++)
                {
                    worksheet.Columns[i].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                }

                // ===== VẼ VIỀN CHO BẢNG =====
                Excel.Range tableRange = worksheet.Range[
                    worksheet.Cells[4, 1], // Bắt đầu từ dòng header
                    worksheet.Cells[currentRow - 4, 13] // Đến dòng cuối cùng của bảng dữ liệu
                ];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // ===== BẬT AUTOFILTER =====
                Excel.Range headerRange = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 13]];
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
                    $"Tổng số điện: {tongSoDien: N1} kWh\n" +
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