using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace QuanLyKiTucXa
{
    public partial class UC_SCCSVC : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isFilterMode = false; // Biến theo dõi trạng thái lọc
        private bool isDateFilterEnabled = false; // Biến theo dõi trạng thái lọc theo ngày

        public UC_SCCSVC()
        {
            InitializeComponent();
        }

        private void UC_SCCSVC_Load(object sender, EventArgs e)
        {
            InitializeFilterControls();
            LoadDanhSachSuaChua();
        }

        private void InitializeFilterControls()
        {
            try
            {
                // ✅ Khởi tạo ComboBox Filter (text-based)
                if (comfillter.Items.Count == 0)
                {
                    comfillter.Items.Clear();
                    comfillter.Items.Add("Mã nhà");
                    comfillter.Items.Add("Mã phòng");
                    comfillter.Items.Add("Mã nhân viên");
                    comfillter.Items.Add("Mã CSVC");
                    comfillter.SelectedIndex = 0;
                }

                // ✅ Khởi tạo ComboBox Ngày (date-based)
                if (comNGAY.Items.Count == 0)
                {
                    comNGAY.Items.Clear();
                    comNGAY.Items.Add("-- Không lọc theo ngày --");
                    comNGAY.Items.Add("Ngày yêu cầu");
                    comNGAY.Items.Add("Ngày hoàn thành");
                    comNGAY.SelectedIndex = 0; // Mặc định không lọc theo ngày
                }

                // ✅ Khởi tạo ComboBox Trạng thái
                if (comTRANGTHAI.Items.Count == 0)
                {
                    comTRANGTHAI.Items.Clear();
                    comTRANGTHAI.Items.Add("Tất cả trạng thái");
                    comTRANGTHAI.Items.Add("Đã tiếp nhận");
                    comTRANGTHAI.Items.Add("Đang xử lý");
                    comTRANGTHAI.Items.Add("Hoàn thành");
                    comTRANGTHAI.SelectedIndex = 0; // Mặc định "Tất cả"
                }

                // ✅ Vô hiệu hóa DateTimePicker ban đầu
                dtpTHOIGIAN.Enabled = false;
                dtpTHOIGIAN.CustomFormat = " "; // Hiển thị rỗng
                dtpTHOIGIAN.Format = DateTimePickerFormat.Custom;

                // ✅ Sự kiện khi thay đổi ComboBox Ngày
                comNGAY.SelectedIndexChanged += ComNGAY_SelectedIndexChanged;

                // Reset trạng thái filter
                isFilterMode = false;
                //btnfillter.Text = "Lọc";
                //btnfillter.BackColor = SystemColors.Control;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo controls: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Sự kiện khi chọn loại lọc theo ngày
        private void ComNGAY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNGAY.SelectedIndex == 0) // "-- Không lọc theo ngày --"
            {
                dtpTHOIGIAN.Enabled = false;
                dtpTHOIGIAN.CustomFormat = " "; // Hiển thị rỗng
                dtpTHOIGIAN.Format = DateTimePickerFormat.Custom;
                isDateFilterEnabled = false;
            }
            else
            {
                dtpTHOIGIAN.Enabled = true;
                dtpTHOIGIAN.Format = DateTimePickerFormat.Short; // Hiển thị ngày
                dtpTHOIGIAN.Value = DateTime.Now;
                isDateFilterEnabled = true;
            }
        }

        // ✅ CHỨC NĂNG LỌC YÊU CẦU SỬA CHỮA
        private void btnfillter_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter();
                    isFilterMode = false;
                    //btnfillter.Text = "Lọc";
                    //btnfillter.BackColor = SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter();
                    isFilterMode = true;
                    //btnfillter.Text = "Reset";
                    //btnfillter.BackColor = Color.LightCoral;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            string query = @"SELECT 
                                SC.MA_SCCSVC,
                                SC.MA_PHONG,
                                P.MANHA,
                                SC.MA_CSVC,
                                DM.TEN_CSVC,
                                ISNULL(NC.TEN_NHACC, N'') AS TEN_NHACC,
                                SC. NGAY_YEUCAU,
                                SC. NGAY_HOANTHANH,
                                SC. CHITIET,
                                SC. TRANGTHAI,
                                ISNULL(NV. TENNV, N'') AS TENNV,
                                SC. MANV
                            FROM SC_CSVC SC
                            INNER JOIN PHONG P ON SC.MA_PHONG = P.MA_PHONG
                            INNER JOIN DM_CSVC DM ON SC.MA_CSVC = DM.MA_CSVC
                            LEFT JOIN NHACC NC ON DM.MA_NHACC = NC.MA_NHACC
                            LEFT JOIN NHANVIEN NV ON SC.MANV = NV.MANV
                            WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter (text-based filter)
            if (comfillter.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string selectedFilter = comfillter.SelectedItem.ToString();
                string searchValue = txtSearch.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã nhà":
                        query += " AND P. MANHA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã phòng":
                        query += " AND SC.MA_PHONG LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã nhân viên":
                        query += " AND SC. MANV LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã CSVC":
                        query += " AND SC.MA_CSVC LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                }
            }

            // ✅ Lọc theo comNGAY (date-based filter) - CHỈ KHI ĐƯỢC KÍCH HOẠT
            if (isDateFilterEnabled && comNGAY.SelectedIndex > 0)
            {
                string selectedDate = comNGAY.SelectedItem.ToString();
                DateTime dateValue = dtpTHOIGIAN.Value.Date;

                switch (selectedDate)
                {
                    case "Ngày yêu cầu":
                        query += " AND CAST(SC.NGAY_YEUCAU AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày hoàn thành":
                        query += " AND CAST(SC.NGAY_HOANTHANH AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                }
            }

            // ✅ Lọc theo comTRANGTHAI
            if (comTRANGTHAI.SelectedItem != null && comTRANGTHAI.SelectedItem.ToString() != "Tất cả trạng thái")
            {
                string trangThai = comTRANGTHAI.SelectedItem.ToString();
                query += " AND SC. TRANGTHAI = @TrangThai";
                parameters.Add(new SqlParameter("@TrangThai", trangThai));
            }

            query += " ORDER BY SC.NGAY_YEUCAU DESC";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy yêu cầu nào phù hợp với điều kiện lọc! ",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Thêm cột STT
            dt.Columns.Add("STT", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["STT"] = i + 1;
            }

            dgvSCCSVC.AutoGenerateColumns = false;
            dgvSCCSVC.DataSource = dt;
        }

        private void ResetFilter()
        {
            // Load lại toàn bộ dữ liệu
            LoadDanhSachSuaChua();

            // Reset các control về trạng thái mặc định
            txtSearch.Clear();

            if (comfillter.Items.Count > 0)
                comfillter.SelectedIndex = 0;

            if (comNGAY.Items.Count > 0)
                comNGAY.SelectedIndex = 0; // "-- Không lọc theo ngày --"

            if (comTRANGTHAI.Items.Count > 0)
                comTRANGTHAI.SelectedIndex = 0; // "Tất cả"

            // Vô hiệu hóa DateTimePicker
            dtpTHOIGIAN.Enabled = false;
            dtpTHOIGIAN.CustomFormat = " ";
            dtpTHOIGIAN.Format = DateTimePickerFormat.Custom;
            isDateFilterEnabled = false;
        }

        private void LoadDanhSachSuaChua(string filterType = "", string searchValue = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        SC. MA_SCCSVC,
                                        SC.MA_PHONG,
                                        P.MANHA,
                                        SC.MA_CSVC,
                                        DM. TEN_CSVC,
                                        ISNULL(NC.TEN_NHACC, N'') AS TEN_NHACC,
                                        SC.NGAY_YEUCAU,
                                        SC.NGAY_HOANTHANH,
                                        SC.CHITIET,
                                        SC.TRANGTHAI,
                                        ISNULL(NV.TENNV, N'') AS TENNV,
                                        SC. MANV
                                    FROM SC_CSVC SC
                                    INNER JOIN PHONG P ON SC.MA_PHONG = P.MA_PHONG
                                    INNER JOIN DM_CSVC DM ON SC.MA_CSVC = DM.MA_CSVC
                                    LEFT JOIN NHACC NC ON DM. MA_NHACC = NC.MA_NHACC
                                    LEFT JOIN NHANVIEN NV ON SC. MANV = NV.MANV
                                    ORDER BY SC.NGAY_YEUCAU DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu sửa chữa CSVC: " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi truy vấn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
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
                    // Refresh lại danh sách
                    if (isFilterMode)
                    {
                        ApplyFilter(); // Nếu đang ở chế độ lọc
                    }
                    else
                    {
                        LoadDanhSachSuaChua(); // Nếu ở chế độ bình thường
                    }
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
                    // Refresh lại danh sách
                    if (isFilterMode)
                    {
                        ApplyFilter(); // Nếu đang ở chế độ lọc
                    }
                    else
                    {
                        LoadDanhSachSuaChua(); // Nếu ở chế độ bình thường
                    }
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
                MessageBox.Show("Lỗi khi xóa:  " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
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

                            // Refresh lại danh sách
                            if (isFilterMode)
                            {
                                ApplyFilter(); // Nếu đang ở chế độ lọc
                            }
                            else
                            {
                                LoadDanhSachSuaChua(); // Nếu ở chế độ bình thường
                            }
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
                MessageBox.Show("Lỗi khi xóa:  " + ex.Message + "\n\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // XÁC ĐỊNH DÒNG CẦN XUẤT
            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();

            if (dgvSCCSVC.SelectedRows.Count > 0)
            {
                // ✅ CHỈ XUẤT DÒNG ĐƯỢC CHỌN
                foreach (DataGridViewRow row in dgvSCCSVC.SelectedRows)
                {
                    if (!row.IsNewRow)
                        rowsToExport.Add(row);
                }
            }
            else
            {
                // ✅ XUẤT TẤT CẢ DÒNG (KHI KHÔNG CHỌN GÌ)
                foreach (DataGridViewRow row in dgvSCCSVC.Rows)
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
                Filter = "Excel files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"DanhSachSuaChuaCSVC_{DateTime.Now:ddMMyyyy_HHmmss}"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            // ✅ XUẤT EXCEL DÙNG INTEROP
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // Tạo Excel Application
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                // Tạo Workbook và Worksheet
                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Danh sách sửa chữa CSVC";

                // ✅ THÊM HEADER (Dòng 1)
                int colIndex = 1;
                foreach (DataGridViewColumn col in dgvSCCSVC.Columns)
                {
                    if (col.Visible)
                    {
                        worksheet.Cells[1, colIndex] = col.HeaderText;

                        // Định dạng header
                        Excel.Range headerCell = worksheet.Cells[1, colIndex];
                        headerCell.Font.Bold = true;
                        headerCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightGreen);
                        headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        colIndex++;
                    }
                }

                // ✅ THÊM DỮ LIỆU (Từ dòng 2)
                int rowIndex = 2;
                foreach (DataGridViewRow row in rowsToExport)
                {
                    colIndex = 1;
                    foreach (DataGridViewColumn col in dgvSCCSVC.Columns)
                    {
                        if (col.Visible)
                        {
                            var cellValue = row.Cells[col.Index].Value;

                            if (cellValue != null)
                            {
                                // Xử lý DateTime
                                if (cellValue is DateTime dateValue)
                                {
                                    worksheet.Cells[rowIndex, colIndex] = dateValue.ToString("dd/MM/yyyy HH:mm");
                                }
                                // Xử lý số
                                else if (cellValue is decimal || cellValue is double || cellValue is int)
                                {
                                    worksheet.Cells[rowIndex, colIndex] = cellValue;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, colIndex] = cellValue.ToString();
                                }
                            }

                            colIndex++;
                        }
                    }
                    rowIndex++;
                }

                // ✅ TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT
                worksheet.Columns.AutoFit();

                // ✅ VẼ VIỀN CHO BẢNG
                Excel.Range tableRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[rowIndex - 1, colIndex - 1]
                ];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // ✅ ĐẶT MÀU CHO TRẠNG THÁI (nếu có cột TRANGTHAI)
                for (int i = 2; i < rowIndex; i++)
                {
                    // Tìm cột TRANGTHAI
                    int trangThaiColIndex = 0;
                    foreach (DataGridViewColumn col in dgvSCCSVC.Columns)
                    {
                        if (col.Visible)
                        {
                            trangThaiColIndex++;
                            if (col.DataPropertyName == "TRANGTHAI" || col.Name == "TRANGTHAI")
                            {
                                string trangThai = worksheet.Cells[i, trangThaiColIndex].Value?.ToString();
                                Excel.Range statusCell = worksheet.Cells[i, trangThaiColIndex];

                                if (trangThai == "Hoàn thành")
                                {
                                    statusCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightGreen);
                                }
                                else if (trangThai == "Đang xử lý")
                                {
                                    statusCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightYellow);
                                }
                                else if (trangThai == "Đã tiếp nhận")
                                {
                                    statusCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightBlue);
                                }
                                break;
                            }
                        }
                    }
                }

                // ✅ LƯU FILE
                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();

                MessageBox.Show($"Xuất file Excel thành công!\nĐã xuất {rowsToExport.Count} dòng dữ liệu.",
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
                // ✅ GIẢI PHÓNG TÀI NGUYÊN COM
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