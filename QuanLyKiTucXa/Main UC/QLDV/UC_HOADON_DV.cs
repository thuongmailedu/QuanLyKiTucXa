using QuanLyKiTucXa.Formadd.QLDV_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

// ✅ THÊM CHO EXCEL INTEROP
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;

namespace QuanLyKiTucXa
{
    public partial class UC_HOADON_DV : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isInitializing = false;

        public UC_HOADON_DV()
        {
            InitializeComponent();
        }

        private void UC_HOADON_DV_Load(object sender, EventArgs e)
        {
            try
            {
                isInitializing = true;
                InitializeControls();
                isInitializing = false;
               // dtpTHOIGIAN.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                isInitializing = false;
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message + "\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeControls()
        {
            // Thiết lập DateTimePicker
            
            // Thiết lập ComboBox
            comNHA.DropDownStyle = ComboBoxStyle.DropDownList;
            comPHONG.DropDownStyle = ComboBoxStyle.DropDownList;
            comTRANGTHAI.DropDownStyle = ComboBoxStyle.DropDownList;

            // Thiết lập DataGridView TRƯỚC
            SetupDataGridView();

            // Load ComboBox
            LoadComboBoxTrangThai();
            LoadComboBoxNha();
            LoadComboBoxPhong(null);

            // Gắn events SAU KHI đã load xong
            comNHA.SelectedIndexChanged += comNHA_SelectedIndexChanged;

            // Load dữ liệu lưới
            LoadDataGridView();
        }

       private void SetupDataGridView()
        {
            dgv_HD_TONGHOP.AutoGenerateColumns = false;
            dgv_HD_TONGHOP.AllowUserToAddRows = false;
            dgv_HD_TONGHOP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_HD_TONGHOP.MultiSelect = true;
            dgv_HD_TONGHOP.ReadOnly = true;

            dgv_HD_TONGHOP.Columns.Clear();

            // Cột STT
            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                Width = 60,
                ReadOnly = true
            });

            // Các cột dữ liệu
            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MANHA",
                HeaderText = "Mã Nhà",
                Name = "MANHA",
                Width = 110
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MA_PHONG",
                HeaderText = "Mã Phòng",
                Name = "MA_PHONG",
                Width = 120
            });
            
            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TENHD",
                HeaderText = "Tên Hóa Đơn",
                Name = "TENHD",
                   Width = 170
            });
            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "THOIGIAN",
                HeaderText = "Thời gian",
                Name = "THOIGIAN",
                Width = 120
            });
            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MAHD_DIEN",
                HeaderText = "Mã HĐ Điện",
                Name = "MAHD_DIEN",
                Width = 150
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MAHD_NUOC",
                HeaderText = "Mã HĐ Nước",
                Name = "MAHD_NUOC",
                Width = 150
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MAHD_INT",
                HeaderText = "Mã HĐ Internet",
                Name = "MAHD_INT",
                Width = 150
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TIENDIEN",
                HeaderText = "Tiền Điện",
                Name = "TIENDIEN",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TIENNUOC",
                HeaderText = "Tiền Nước",
                Name = "TIENNUOC",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TIENINTERNET",
                HeaderText = "Tiền Internet",
                Name = "TIENINTERNET",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TONGTIEN",
                HeaderText = "Tổng Tiền",
                Name = "TONGTIEN",
                Width = 120,
                //DefaultCellStyle = new DataGridViewCellStyle
                //{
                //    Format = "N0",
                //    Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold),
                //    ForeColor = System.Drawing.Color.Red,
                //    Alignment = DataGridViewContentAlignment.MiddleRight
                //}
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TINHTRANGTT",
                HeaderText = "Trạng Thái",
                Name = "TINHTRANGTT",
                Width = 150
            });

            dgv_HD_TONGHOP.RowPostPaint += dgv_HD_TONGHOP_RowPostPaint;
        }

        private void dgv_HD_TONGHOP_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgv_HD_TONGHOP.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
        }

        private void LoadComboBoxNha()
        {
            try
            {
                comNHA.Items.Clear();
                comNHA.Items.Add("-- Tất cả --");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MANHA FROM NHA ORDER BY MANHA", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comNHA.Items.Add(reader["MANHA"].ToString());
                    }
                    reader.Close();
                }

                if (comNHA.Items.Count > 0)
                    comNHA.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxPhong(string maNha)
        {
            try
            {
                comPHONG.Items.Clear();
                comPHONG.Items.Add("-- Tất cả --");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MA_PHONG FROM PHONG";

                    if (!string.IsNullOrEmpty(maNha) && maNha != "-- Tất cả --")
                        query += " WHERE MANHA = @MANHA";

                    query += " ORDER BY MA_PHONG";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (!string.IsNullOrEmpty(maNha) && maNha != "-- Tất cả --")
                        cmd.Parameters.AddWithValue("@MANHA", maNha);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comPHONG.Items.Add(reader["MA_PHONG"].ToString());
                    }
                    reader.Close();
                }

                if (comPHONG.Items.Count > 0)
                    comPHONG.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxTrangThai()
        {
            comTRANGTHAI.Items.Clear();
            comTRANGTHAI.Items.Add("-- Tất cả --");
            comTRANGTHAI.Items.Add("Chưa thanh toán");
            comTRANGTHAI.Items.Add("Đã thanh toán");

            if (comTRANGTHAI.Items.Count > 0)
                comTRANGTHAI.SelectedIndex = 0;
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            if (comNHA.SelectedItem != null)
            {
                string maNha = comNHA.SelectedItem.ToString();
                LoadComboBoxPhong(maNha == "-- Tất cả --" ? null : maNha);
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                int thang = dtpTHOIGIAN.Value.Month;
                int nam = dtpTHOIGIAN.Value.Year;

                string maNha = (comNHA.SelectedItem?.ToString() == "-- Tất cả --") ? null : comNHA.SelectedItem?.ToString();
                string maPhong = (comPHONG.SelectedItem?.ToString() == "-- Tất cả --") ? null : comPHONG.SelectedItem?.ToString();
                string trangThai = (comTRANGTHAI.SelectedItem?.ToString() == "-- Tất cả --") ? null : comTRANGTHAI.SelectedItem?.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_LayHoaDonTongHop", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;

                    cmd.Parameters.AddWithValue("@THANG", thang);
                    cmd.Parameters.AddWithValue("@NAM", nam);
                    cmd.Parameters.AddWithValue("@MANHA", (object)maNha ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MA_PHONG", (object)maPhong ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TINHTRANGTT", (object)trangThai ?? DBNull.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgv_HD_TONGHOP.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnfillter_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        // Thêm vào class UC_HOADON_DV

        // Trong UC_HOADON_DV.cs

        // ✅ Connection string của bạn
      //  private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_HD_TONGHOP.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xử lý!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgv_HD_TONGHOP.SelectedRows[0];

                string maPhong = row.Cells["MA_PHONG"].Value.ToString();
                string maNha = row.Cells["MANHA"].Value.ToString();
                DateTime thoiGian = Convert.ToDateTime(row.Cells["THOIGIAN"].Value);
                decimal tongTien = Convert.ToDecimal(row.Cells["TONGTIEN"].Value);
                string tinhTrang = row.Cells["TINHTRANGTT"].Value.ToString();

                // Mở form thanh toán
                frm_TTHOADON frm = new frm_TTHOADON();
                frm.MaNha = maNha;
                frm.MaPhong = maPhong;
                frm.ThoiGian = thoiGian;
                frm.TongTien = tongTien;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh lại danh sách
                    LoadDataGridView(); // Gọi method load dữ liệu của bạn
                    MessageBox.Show("Cập nhật thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_HD_TONGHOP.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần hủy!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgv_HD_TONGHOP.SelectedRows[0];

                string maPhong = row.Cells["MA_PHONG"].Value.ToString();
                DateTime thoiGian = Convert.ToDateTime(row.Cells["THOIGIAN"].Value);
                string tinhTrang = row.Cells["TINHTRANGTT"].Value.ToString();

                // Kiểm tra đã thanh toán chưa
                if (tinhTrang != "Đã thanh toán")
                {
                    MessageBox.Show("Hóa đơn này chưa thanh toán, không thể hủy!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm
                DialogResult confirm = MessageBox.Show(
                    $"Xác nhận HỦY thanh toán:\n\n" +
                    $"Phòng: {maPhong}\n" +
                    $"Tháng: {thoiGian:MM/yyyy}\n\n" +
                    $"Hành động này sẽ xóa thông tin thanh toán và đặt lại trạng thái hóa đơn! ",
                    "Xác nhận hủy",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.No) return;

                // Hủy thanh toán
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CapNhatTrangThaiHoaDon", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                    cmd.Parameters.AddWithValue("@THANG", thoiGian.Month);
                    cmd.Parameters.AddWithValue("@NAM", thoiGian.Year);
                    cmd.Parameters.AddWithValue("@TINHTRANGTT", "Chưa thanh toán");
                    cmd.Parameters.AddWithValue("@MASV", DBNull.Value);
                    cmd.Parameters.AddWithValue("@MANV", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NGAYTHANHTOAN", DBNull.Value);
                    cmd.Parameters.AddWithValue("@HINHTHUC_TT", DBNull.Value);
                    cmd.Parameters.AddWithValue("@GHICHU", DBNull.Value);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int success = Convert.ToInt32(reader["Success"]);
                        if (success == 1)
                        {
                            MessageBox.Show("Hủy thanh toán thành công!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataGridView(); // Refresh
                        }
                        else
                        {
                            string errorMsg = reader["ErrorMessage"].ToString();
                            MessageBox.Show($"Lỗi: {errorMsg}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hủy thanh toán: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTrangThai(string trangThai)
        {
            if (dgv_HD_TONGHOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hóa đơn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string action = trangThai == "Đã thanh toán" ? "thanh toán" : "hủy thanh toán";
            if (MessageBox.Show($"Bạn có chắc muốn {action} {dgv_HD_TONGHOP.SelectedRows.Count} hóa đơn?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int thang = dtpTHOIGIAN.Value.Month;
            int nam = dtpTHOIGIAN.Value.Year;
            int successCount = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgv_HD_TONGHOP.SelectedRows)
                    {
                        string maPhong = row.Cells["MA_PHONG"].Value?.ToString();
                        if (string.IsNullOrEmpty(maPhong)) continue;

                        SqlCommand cmd = new SqlCommand("sp_CapNhatTrangThaiHoaDon", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                        cmd.Parameters.AddWithValue("@THANG", thang);
                        cmd.Parameters.AddWithValue("@NAM", nam);
                        cmd.Parameters.AddWithValue("@TINHTRANGTT", trangThai);
                        cmd.ExecuteNonQuery();
                        successCount++;
                    }
                }

                MessageBox.Show($"Cập nhật thành công {successCount} hóa đơn!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có chọn dòng không
                if (dgv_HD_TONGHOP.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một hóa đơn để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgv_HD_TONGHOP.SelectedRows[0];

                // Lấy các giá trị từ các cột
                string maNha = selectedRow.Cells["MANHA"].Value?.ToString();
                string maPhong = selectedRow.Cells["MA_PHONG"].Value?.ToString();
                DateTime thoiGian = Convert.ToDateTime(selectedRow.Cells["THOIGIAN"].Value);
                string ttThanhToan = selectedRow.Cells["TINHTRANGTT"].Value?.ToString();

                // Kiểm tra dữ liệu
                if (string.IsNullOrEmpty(maPhong))
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở form hóa đơn tổng hợp
                frm_HD_TONGHOP frmHoaDon = new frm_HD_TONGHOP(
                    maNha,
                    maPhong,
                    thoiGian.Month,
                    thoiGian.Year,
                    ttThanhToan
                );

                frmHoaDon.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

    }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // XÁC ĐỊNH DÒNG CẦN XUẤT
            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();

            if (dgv_HD_TONGHOP.SelectedRows.Count > 0)
            {
                // ✅ CHỈ XUẤT DÒNG ĐƯỢC CHỌN
                foreach (DataGridViewRow row in dgv_HD_TONGHOP.SelectedRows)
                {
                    if (!row.IsNewRow)
                        rowsToExport.Add(row);
                }
            }
            else
            {
                // ✅ XUẤT TẤT CẢ DÒNG
                foreach (DataGridViewRow row in dgv_HD_TONGHOP.Rows)
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
                FileName = $"HoaDonDichVu_{dtpTHOIGIAN.Value:MMyyyy}_{DateTime.Now:ddMMyyyy_HHmmss}"
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
                worksheet.Name = "Hóa đơn dịch vụ";

                // ===== TIÊU ĐỀ =====
                int currentRow = 1;
                worksheet.Cells[currentRow, 1] = "BÁO CÁO HÓA ĐƠN DỊCH VỤ";
                Excel.Range titleRange = worksheet.Range[worksheet.Cells[currentRow, 1], worksheet.Cells[currentRow, 13]];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleRange.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);
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
                if (comNHA.SelectedItem?.ToString() != "-- Tất cả --")
                    filterInfo += $"Nhà:  {comNHA.SelectedItem}  ";
                if (comPHONG.SelectedItem?.ToString() != "-- Tất cả --")
                    filterInfo += $"Phòng: {comPHONG.SelectedItem}  ";
                if (comTRANGTHAI.SelectedItem?.ToString() != "-- Tất cả --")
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
            "Mã Nhà",
            "Mã Phòng",
            "Tên Hóa Đơn",
            "Thời gian",
            "Mã HĐ Điện",
            "Mã HĐ Nước",
            "Mã HĐ Internet",
            "Tiền Điện",
            "Tiền Nước",
            "Tiền Internet",
            "Tổng Tiền",
            "Trạng Thái"
                };

                foreach (string header in headers)
                {
                    worksheet.Cells[currentRow, colIndex] = header;

                    // Định dạng header
                    Excel.Range headerCell = worksheet.Cells[currentRow, colIndex];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    headerCell.WrapText = true;

                    colIndex++;
                }
                currentRow++;

                // ===== DỮ LIỆU =====
                int stt = 1;
                decimal tongTienDien = 0;
                decimal tongTienNuoc = 0;
                decimal tongTienInternet = 0;
                decimal tongTatCa = 0;

                foreach (DataGridViewRow row in rowsToExport)
                {
                    colIndex = 1;

                    // STT
                    worksheet.Cells[currentRow, colIndex++] = stt++;

                    // Mã Nhà
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MANHA"].Value?.ToString() ?? "";

                    // Mã Phòng
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MA_PHONG"].Value?.ToString() ?? "";

                    // Tên Hóa Đơn
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["TENHD"].Value?.ToString() ?? "";

                    // Thời gian
                    if (row.Cells["THOIGIAN"].Value != null && row.Cells["THOIGIAN"].Value != DBNull.Value)
                    {
                        DateTime thoiGian = Convert.ToDateTime(row.Cells["THOIGIAN"].Value);
                        worksheet.Cells[currentRow, colIndex] = thoiGian.ToString("MM/yyyy");
                    }
                    colIndex++;

                    // Mã HĐ Điện
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MAHD_DIEN"].Value?.ToString() ?? "";

                    // Mã HĐ Nước
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MAHD_NUOC"].Value?.ToString() ?? "";

                    // Mã HĐ Internet
                    worksheet.Cells[currentRow, colIndex++] = row.Cells["MAHD_INT"].Value?.ToString() ?? "";

                    // Tiền Điện
                    decimal tienDien = 0;
                    if (row.Cells["TIENDIEN"].Value != null && row.Cells["TIENDIEN"].Value != DBNull.Value)
                    {
                        tienDien = Convert.ToDecimal(row.Cells["TIENDIEN"].Value);
                        worksheet.Cells[currentRow, colIndex] = tienDien;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                        tongTienDien += tienDien;
                    }
                    colIndex++;

                    // Tiền Nước
                    decimal tienNuoc = 0;
                    if (row.Cells["TIENNUOC"].Value != null && row.Cells["TIENNUOC"].Value != DBNull.Value)
                    {
                        tienNuoc = Convert.ToDecimal(row.Cells["TIENNUOC"].Value);
                        worksheet.Cells[currentRow, colIndex] = tienNuoc;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                        tongTienNuoc += tienNuoc;
                    }
                    colIndex++;

                    // Tiền Internet
                    decimal tienInternet = 0;
                    if (row.Cells["TIENINTERNET"].Value != null && row.Cells["TIENINTERNET"].Value != DBNull.Value)
                    {
                        tienInternet = Convert.ToDecimal(row.Cells["TIENINTERNET"].Value);
                        worksheet.Cells[currentRow, colIndex] = tienInternet;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                        tongTienInternet += tienInternet;
                    }
                    colIndex++;

                    // Tổng Tiền
                    decimal tongTien = 0;
                    if (row.Cells["TONGTIEN"].Value != null && row.Cells["TONGTIEN"].Value != DBNull.Value)
                    {
                        tongTien = Convert.ToDecimal(row.Cells["TONGTIEN"].Value);
                        worksheet.Cells[currentRow, colIndex] = tongTien;
                        worksheet.Cells[currentRow, colIndex].NumberFormat = "#,##0";
                        Excel.Range tongTienCell = worksheet.Cells[currentRow, colIndex];
                        tongTienCell.Font.Bold = true;
                        tongTatCa += tongTien;
                    }
                    colIndex++;

                    // Trạng Thái (có tô màu)
                    string trangThai = row.Cells["TINHTRANGTT"].Value?.ToString() ?? "";
                    worksheet.Cells[currentRow, colIndex] = trangThai;
                    Excel.Range statusCell = worksheet.Cells[currentRow, colIndex];
                    statusCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    if (trangThai == "Đã thanh toán")
                    {
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                        statusCell.Font.Bold = true;
                    }
                    else if (trangThai == "Chưa thanh toán")
                    {
                        statusCell.Interior.Color = ColorTranslator.ToOle(Color.LightCoral);
                        statusCell.Font.Bold = true;
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

                worksheet.Cells[currentRow, 9] = tongTienDien;
                worksheet.Cells[currentRow, 9].NumberFormat = "#,##0";
                worksheet.Cells[currentRow, 9].Font.Bold = true;

                worksheet.Cells[currentRow, 10] = tongTienNuoc;
                worksheet.Cells[currentRow, 10].NumberFormat = "#,##0";
                worksheet.Cells[currentRow, 10].Font.Bold = true;

                worksheet.Cells[currentRow, 11] = tongTienInternet;
                worksheet.Cells[currentRow, 11].NumberFormat = "#,##0";
                worksheet.Cells[currentRow, 11].Font.Bold = true;

                worksheet.Cells[currentRow, 12] = tongTatCa;
                worksheet.Cells[currentRow, 12].NumberFormat = "#,##0";
                Excel.Range tongTatCaCell = worksheet.Cells[currentRow, 12];
                tongTatCaCell.Font.Bold = true;
                tongTatCaCell.Font.Size = 12;
                tongTatCaCell.Font.Color = ColorTranslator.ToOle(Color.Red);
                tongTatCaCell.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);

                // ===== ĐỊNH DẠNG CỘT =====
                worksheet.Columns[1].ColumnWidth = 6;   // STT
                worksheet.Columns[2].ColumnWidth = 10;  // Mã Nhà
                worksheet.Columns[3].ColumnWidth = 12;  // Mã Phòng
                worksheet.Columns[4].ColumnWidth = 20;  // Tên HĐ
                worksheet.Columns[5].ColumnWidth = 12;  // Thời gian
                worksheet.Columns[6].ColumnWidth = 18;  // Mã HĐ Điện
                worksheet.Columns[7].ColumnWidth = 18;  // Mã HĐ Nước
                worksheet.Columns[8].ColumnWidth = 18;  // Mã HĐ Internet
                worksheet.Columns[9].ColumnWidth = 15;  // Tiền Điện
                worksheet.Columns[10].ColumnWidth = 15; // Tiền Nước
                worksheet.Columns[11].ColumnWidth = 15; // Tiền Internet
                worksheet.Columns[12].ColumnWidth = 16; // Tổng Tiền
                worksheet.Columns[13].ColumnWidth = 16; // Trạng Thái

                // Căn phải cho các cột tiền
                for (int i = 9; i <= 12; i++)
                {
                    worksheet.Columns[i].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                }

                // ===== VẼ VIỀN CHO BẢNG =====
                Excel.Range tableRange = worksheet.Range[
                    worksheet.Cells[4, 1], // Bắt đầu từ dòng header
                    worksheet.Cells[currentRow, 13]
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

                MessageBox.Show($"Xuất file Excel thành công!\nĐã xuất {rowsToExport.Count} hóa đơn.\n\nTổng tiền: {tongTatCa:N0} VNĐ",
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