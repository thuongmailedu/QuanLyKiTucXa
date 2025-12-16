using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyKiTucXa.Formadd.QLHD_FORM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Linq;


namespace QuanLyKiTucXa.Main_UC.QLHD
{
    public partial class UC_ThuePhong : UserControl
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;

        private bool isFilterMode = false; // Biến theo dõi trạng thái lọc
        private bool isDateFilterEnabled = false; // Biến theo dõi trạng thái lọc theo ngày

        public UC_ThuePhong()
        {
            InitializeComponent();
        }

        private void UC_ThuePhong_Load(object sender, EventArgs e)
        {
            // Thiết lập font cho header
            dgvThuePhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvThuePhong.Font, FontStyle.Bold);

            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
            conn.ConnectionString = constr;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

           // dgvThuePhong.ClearSelection();
            // dgvThuePhong.MultiSelect = false;

            // ✅ Khởi tạo các control và load dữ liệu
            InitializeFilterControls();
        }

        private void InitializeFilterControls()
        {
            try
            {
                // ✅ Khởi tạo ComboBox Filter (text-based)
                if (comfillter.Items.Count == 0)
                {
                    comfillter.Items.Clear();
                    comfillter.Items.Add("Mã SV");
                    comfillter.Items.Add("Mã phòng");
                    comfillter.Items.Add("Mã nhà");
                    comfillter.Items.Add("Mã NV");
                    comfillter.SelectedIndex = 0;
                }

                // ✅ Khởi tạo ComboBox Ngày (date-based)
                if (comNgay.Items.Count == 0)
                {
                    comNgay.Items.Clear();
                    comNgay.Items.Add("-- Không lọc theo ngày --");
                    comNgay.Items.Add("Ngày bắt đầu");
                    comNgay.Items.Add("Ngày kết thúc");
                    comNgay.Items.Add("Ngày thanh lý");
                    comNgay.Items.Add("Ngày ký");
                    comNgay.SelectedIndex = 0; // Mặc định không lọc theo ngày
                }

                // ✅ Vô hiệu hóa DateTimePicker ban đầu
               // dtpTHANG.Enabled = false;
                dtpTHANG.CustomFormat = " "; // Hiển thị rỗng
                dtpTHANG.Format = DateTimePickerFormat.Custom;

                // ✅ Sự kiện khi thay đổi ComboBox Ngày
                comNgay.SelectedIndexChanged += ComNgay_SelectedIndexChanged;

                // Load dữ liệu ban đầu
                LoadAllContracts();

                // Reset trạng thái filter
                isFilterMode = false;
                // btnfillter_HOPDONG.Text = "Lọc";
                // btnfillter_HOPDONG.BackColor = SystemColors.Control;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo controls: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Sự kiện khi chọn loại lọc theo ngày
        private void ComNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNgay.SelectedIndex == 0) // "-- Không lọc theo ngày --"
            {
               // dtpTHANG.Enabled = false;
                dtpTHANG.CustomFormat = " "; // Hiển thị rỗng
                dtpTHANG.Format = DateTimePickerFormat.Custom;
                isDateFilterEnabled = false;
            }
            else
            {
                dtpTHANG.Enabled = true;
                dtpTHANG.Format = DateTimePickerFormat.Short; // Hiển thị ngày
                dtpTHANG.Value = DateTime.Now;
                isDateFilterEnabled = true;
            }
        }

        // ✅ CHỨC NĂNG LỌC HỢP ĐỒNG
        private void btnfillter_HOPDONG_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter();
                    isFilterMode = false;
                  //  btnfillter_HOPDONG.Text = "Lọc";
                  //  btnfillter_HOPDONG.BackColor = SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter();
                    isFilterMode = true;
                 //   btnfillter_HOPDONG.Text = "Reset";
                   // btnfillter_HOPDONG.BackColor = Color.LightCoral;
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
            // ✅ Query đầy đủ với thứ tự cột rõ ràng
            string query = @"
                SELECT 
                    HD.MAHD,
                    HD.TENHD,
                    HD. MASV,
                    SV.TENSV,
                    SV.GIOITINH,
                    P.MANHA,
                    HD.MA_PHONG,
                    N.LOAIPHONG,
                    HD. TUNGAY,
                    HD.DENNGAY,
                    HD.THOIHAN,
                    HD.NGAYKTTT,
                    HD.DONGIA,
                    HD.TONGTIEN,
                    HD.MANV,
                    NV.TENNV,
                    HD. NGAYKY
                   
                FROM HOPDONG HD
                INNER JOIN SINHVIEN SV ON HD.MASV = SV.MASV
                INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                INNER JOIN NHA N ON P.MANHA = N. MANHA
                LEFT JOIN NHANVIEN NV ON HD. MANV = NV. MANV
                WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter (text-based filter)
            if (comfillter.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string selectedFilter = comfillter.SelectedItem.ToString();
                string searchValue = txtSearch.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã SV":
                        query += " AND HD. MASV LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã phòng":
                        query += " AND HD.MA_PHONG LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã nhà":
                        query += " AND P.MANHA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã NV":
                        query += " AND HD.MANV LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                }
            }

            // ✅ Lọc theo comNgay (date-based filter) - CHỈ KHI ĐƯỢC KÍCH HOẠT
            if (isDateFilterEnabled && comNgay.SelectedIndex > 0)
            {
                string selectedDate = comNgay.SelectedItem.ToString();
                DateTime dateValue = dtpTHANG.Value.Date;

                switch (selectedDate)
                {
                    case "Ngày bắt đầu":
                        query += " AND CAST(HD.TUNGAY AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày kết thúc":
                        query += " AND CAST(HD. DENNGAY AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày thanh lý":
                        query += " AND CAST(HD.NGAYKTTT AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày ký":
                        query += " AND CAST(HD. NGAYKY AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                }
            }

            query += " ORDER BY HD.TUNGAY DESC";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hợp đồng nào phù hợp với điều kiện lọc! ",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvThuePhong.DataSource = dt;
          //  FormatDataGridView();
        }

        private void ResetFilter()
        {
            // Load lại toàn bộ dữ liệu
            LoadAllContracts();

            // Reset các control về trạng thái mặc định
            txtSearch.Clear();

            if (comfillter.Items.Count > 0)
                comfillter.SelectedIndex = 0;

            if (comNgay.Items.Count > 0)
                comNgay.SelectedIndex = 0; // "-- Không lọc theo ngày --"

            // Vô hiệu hóa DateTimePicker
            dtpTHANG.Enabled = false;
            dtpTHANG.CustomFormat = " ";
            dtpTHANG.Format = DateTimePickerFormat.Custom;
            isDateFilterEnabled = false;
        }

        private void LoadAllContracts()
        {
            // ✅ Query đầy đủ với thứ tự cột rõ ràng
            string query = @"
                SELECT 
                    HD.MAHD,
                    HD.TENHD,
                    HD. MASV,
                    SV.TENSV,
                    SV.GIOITINH,
                    P. MANHA,
                    HD. MA_PHONG,
                    N.LOAIPHONG,
                    HD.TUNGAY,
                    HD. DENNGAY,
                    HD.THOIHAN,
                    HD.NGAYKTTT,
                    HD.DONGIA,
                    HD. TONGTIEN,
                    HD.MANV,
                    NV.TENNV,
                    HD.NGAYKY
                
                FROM HOPDONG HD
                INNER JOIN SINHVIEN SV ON HD. MASV = SV. MASV
                INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                INNER JOIN NHA N ON P.MANHA = N.MANHA
                LEFT JOIN NHANVIEN NV ON HD.MANV = NV.MANV
                ORDER BY HD.TUNGAY DESC";

            DataTable dt = ExecuteQuery(query);
            dgvThuePhong.DataSource = dt;
            // ✅ THÊM DÒNG NÀY ĐỂ BỎ CHỌN TẤT CẢ CÁC DÒNG
            if (dgvThuePhong.Rows.Count > 0)
            {
                dgvThuePhong.ClearSelection();
                dgvThuePhong.CurrentCell = null;
            }
            //  FormatDataGridView();
        }

        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection tempConn = new SqlConnection(constr))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, tempConn))
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

        private void FormatDataGridView()
        {
            if (dgvThuePhong.Columns.Count == 0) return;

            try
            {
                // ✅ Đặt tiêu đề cho các cột theo đúng thứ tự
                dgvThuePhong.Columns["MAHD"].HeaderText = "Mã HĐ";
                dgvThuePhong.Columns["TENHD"].HeaderText = "Tên HĐ";
                dgvThuePhong.Columns["MASV"].HeaderText = "Mã SV";
                dgvThuePhong.Columns["TENSV"].HeaderText = "Tên sinh viên";
                dgvThuePhong.Columns["GIOITINH"].HeaderText = "Giới tính";
                dgvThuePhong.Columns["MANHA"].HeaderText = "Mã nhà";
                dgvThuePhong.Columns["MA_PHONG"].HeaderText = "Mã phòng";
                dgvThuePhong.Columns["LOAIPHONG"].HeaderText = "Loại phòng";
                dgvThuePhong.Columns["TUNGAY"].HeaderText = "Từ ngày";
                dgvThuePhong.Columns["DENNGAY"].HeaderText = "Đến ngày";
                dgvThuePhong.Columns["THOIHAN"].HeaderText = "Thời hạn (tháng)";
                dgvThuePhong.Columns["NGAYKTTT"].HeaderText = "Ngày thanh lý";
                dgvThuePhong.Columns["DONGIA"].HeaderText = "Đơn giá";
                dgvThuePhong.Columns["TONGTIEN"].HeaderText = "Tổng tiền";
                dgvThuePhong.Columns["MANV"].HeaderText = "Mã NV";
                dgvThuePhong.Columns["TENNV"].HeaderText = "Tên nhân viên";
                dgvThuePhong.Columns["NGAYKY"].HeaderText = "Ngày ký";
                //dgvThuePhong.Columns["MANV_TL"].HeaderText = "Mã NV thanh lý";
                //dgvThuePhong.Columns["NGAYKY_TL"].HeaderText = "Ngày ký TL";

                // ✅ Format các cột tiền tệ
                dgvThuePhong.Columns["DONGIA"].DefaultCellStyle.Format = "N0";
                dgvThuePhong.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
                dgvThuePhong.Columns["DONGIA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvThuePhong.Columns["TONGTIEN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // ✅ Format các cột ngày tháng
                string[] dateColumns = { "TUNGAY", "DENNGAY", "NGAYKTTT", "NGAYKY", "NGAYKY_TL" };
                foreach (string colName in dateColumns)
                {
                    if (dgvThuePhong.Columns[colName] != null)
                    {
                        dgvThuePhong.Columns[colName].DefaultCellStyle.Format = "dd/MM/yyyy";
                        dgvThuePhong.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                // ✅ Căn giữa các cột khác
                dgvThuePhong.Columns["MAHD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThuePhong.Columns["MASV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThuePhong.Columns["GIOITINH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThuePhong.Columns["MANHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThuePhong.Columns["MA_PHONG"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThuePhong.Columns["MANV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgvThuePhong.Columns["MANV_TL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Tự động điều chỉnh độ rộng cột
                dgvThuePhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi format DataGridView: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CHỨC NĂNG XÓA HỢP ĐỒNG
        private void btndelete_HOPDONG_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dòng nào được chọn không
                if (dgvThuePhong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy MAHD từ dòng được chọn
                string maHD = dgvThuePhong.SelectedRows[0].Cells["MAHD"].Value?.ToString();
                string tenSV = dgvThuePhong.SelectedRows[0].Cells["TENSV"].Value?.ToString();

                if (string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show("Không tìm thấy mã hợp đồng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa hợp đồng:\n\n" +
                    $"Mã HĐ: {maHD}\n" +
                    $"Sinh viên: {tenSV}\n\n", 
                  //  $"⚠️ Lưu ý: Thao tác này KHÔNG THỂ HOÀN TÁC! ",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa
                    string deleteQuery = "DELETE FROM HOPDONG WHERE MAHD = @MAHD";

                    using (SqlConnection tempConn = new SqlConnection(constr))
                    {
                        tempConn.Open();
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, tempConn))
                        {
                            cmd.Parameters.AddWithValue("@MAHD", maHD);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa hợp đồng thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh lại DataGridView
                                if (isFilterMode)
                                {
                                    ApplyFilter(); // Nếu đang ở chế độ lọc
                                }
                                else
                                {
                                    LoadAllContracts(); // Nếu ở chế độ bình thường
                                }
                            }
                            else
                            {
                                MessageBox.Show("Không thể xóa hợp đồng!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547) // Foreign key constraint error
                {
                    MessageBox.Show(
                        "Không thể xóa hợp đồng này vì đã có dữ liệu liên quan " +
                        "(hóa đơn điện, nước, internet, v.v... )\n\n" +
                        "Vui lòng xóa các dữ liệu liên quan trước! ",
                        "Lỗi ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CÁC CHỨC NĂNG KHÁC (GIỮ NGUYÊN LOGIC CŨ)
        private void btn_AddHopDong_Click(object sender, EventArgs e)
        {
            using (frm_addHopDong frm = new frm_addHopDong())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh lại danh sách sau khi thêm thành công
                    if (isFilterMode)
                    {
                        ApplyFilter();
                    }
                    else
                    {
                        LoadAllContracts();
                    }
                }
            }
        }

        private void dgvThuePhong_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Vẽ số thứ tự ở cột đầu tiên
            dgvThuePhong.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void btnedit_HOPDONG_Click(object sender, EventArgs e)
        {
            // TODO: Code cho chức năng sửa hợp đồng (nếu cần)
            MessageBox.Show("Chức năng đang phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnIN_HOPDONG_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dòng nào được chọn không
                if (dgvThuePhong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng cần in!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy MAHD từ dòng được chọn
                string maHD = dgvThuePhong.SelectedRows[0].Cells["MAHD"].Value?.ToString();

                if (string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show("Không tìm thấy mã hợp đồng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy thông tin chi tiết từ database
                string query = @"
                    SELECT 
                        HD.MAHD,
                        HD. TENHD,
                        HD. MASV,
                        SV.TENSV,
                        SV.NGAYSINH,
                        SV. GIOITINH,
                        SV.CCCD,
                        SV. SDT AS SDT_SV,
                        SV.HKTT,
                        HD.MA_PHONG,
                        HD.TUNGAY,
                        HD. DENNGAY,
                        HD.THOIHAN,
                        HD. NGAYKTTT,
                        HD.DONGIA,
                        HD.TONGTIEN,
                        HD.MANV,
                        NV. TENNV,
                        NV.SDT AS SDT_NV,
                        HD.NGAYKY,
                        P.MANHA,
                        N.LOAIPHONG,
                        L.TENLOP,
                        K.TENKHOA,
                        TN.TEN_THANNHAN,
                        TN.SDT AS SDT_THANNHAN,
                        TN.MOIQUANHE,
                        TN. DIACHI AS DIACHI_THANNHAN
                    FROM HOPDONG HD
                    INNER JOIN SINHVIEN SV ON HD. MASV = SV. MASV
                    INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                    INNER JOIN NHA N ON P.MANHA = N.MANHA
                    LEFT JOIN NHANVIEN NV ON HD.MANV = NV.MANV
                    LEFT JOIN LOP L ON SV.MALOP = L.MALOP
                    LEFT JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                    LEFT JOIN THANNHAN TN ON SV.MASV = TN.MASV
                    WHERE HD.MAHD = @MAHD";

                using (SqlConnection tempConn = new SqlConnection(constr))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@MAHD", maHD);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Tách ngày ký thành ngày, tháng, năm
                            DateTime ngayKy = reader["NGAYKY"] != DBNull.Value
                                ? Convert.ToDateTime(reader["NGAYKY"])
                                : DateTime.Now;

                            // Mở form in hợp đồng
                            frm_IN_HOPDONG frmIn = new frm_IN_HOPDONG();

                            // Truyền tham số vào ReportViewer
                            Microsoft.Reporting.WinForms.ReportParameter[] parameters = new Microsoft.Reporting.WinForms.ReportParameter[]
                            {
                                // Thông tin sinh viên
                                new Microsoft. Reporting.WinForms. ReportParameter("prMASV", reader["MASV"]. ToString()),
                                new Microsoft. Reporting.WinForms. ReportParameter("prTENSV", reader["TENSV"]. ToString()),
                                new Microsoft. Reporting.WinForms. ReportParameter("prNGAYSINH", reader["NGAYSINH"] != DBNull.Value ? Convert.ToDateTime(reader["NGAYSINH"]).ToString("dd/MM/yyyy") : ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prGIOITINH", reader["GIOITINH"]?.ToString() ?? ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prSDT_SINHVIEN", reader["SDT_SV"]?.ToString() ?? ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prHKTT_SINHVIEN", reader["HKTT"]?.ToString() ?? ""),
                                new Microsoft. Reporting.WinForms.ReportParameter("prTENLOP", reader["TENLOP"]?.ToString() ?? ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prTENKHOA", reader["TENKHOA"]?.ToString() ?? ""),

                                // Thông tin phòng
                                new Microsoft. Reporting.WinForms.ReportParameter("prMA_PHONG", reader["MA_PHONG"]. ToString()),
                                new Microsoft. Reporting.WinForms. ReportParameter("prMANHA", reader["MANHA"].ToString()),

                                // Thông tin thời gian và tiền
                                new Microsoft. Reporting.WinForms.ReportParameter("prTUNGAY", reader["TUNGAY"] != DBNull.Value ? Convert. ToDateTime(reader["TUNGAY"]).ToString("dd/MM/yyyy") : ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prDENNGAY", reader["DENNGAY"] != DBNull.Value ? Convert.ToDateTime(reader["DENNGAY"]).ToString("dd/MM/yyyy") : ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prTHOIHAN", reader["THOIHAN"]?.ToString() ?? ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prDONGIA", reader["DONGIA"] != DBNull.Value ?  Convert.ToDecimal(reader["DONGIA"]).ToString("N0") : "0"),

                                // Ngày ký (tách thành ngày, tháng, năm)
                                new Microsoft.Reporting.WinForms.ReportParameter("prNgay_NGAYKY", ngayKy. Day.ToString()),
                                new Microsoft.Reporting.WinForms.ReportParameter("prThang_NGAYKY", ngayKy.Month.ToString()),
                                new Microsoft.Reporting.WinForms.ReportParameter("prNam_NGAYKY", ngayKy.Year.ToString()),

                                // Thông tin nhân viên
                                new Microsoft.Reporting.WinForms.ReportParameter("prTENNV", reader["TENNV"]?.ToString() ?? ""),
                                new Microsoft.Reporting.WinForms.ReportParameter("prSDT_NHANVIEN", reader["SDT_NV"]?.ToString() ?? "")
                            };

                            // Truyền parameters vào form
                            frmIn.SetReportParameters(parameters);
                            frmIn.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin hợp đồng!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void dgvThuePhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý sự kiện click vào cell (nếu cần)
        }

      

        private void btnExport_Click(object sender, EventArgs e)
        {
            // XÁC ĐỊNH DÒNG CẦN XUẤT
            List<DataGridViewRow> rowsToExport = new List<DataGridViewRow>();

            if (dgvThuePhong.SelectedRows.Count > 0)
            {
                // ✅ CHỈ XUẤT DÒNG ĐƯỢC CHỌN
                foreach (DataGridViewRow row in dgvThuePhong.SelectedRows)
                {
                    if (!row.IsNewRow)
                        rowsToExport.Add(row);
                }
            }
            else
            {
                // ✅ XUẤT TẤT CẢ DÒNG (KHI KHÔNG CHỌN GÌ)
                foreach (DataGridViewRow row in dgvThuePhong.Rows)
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
                FileName = $"DanhSachHopDong_{DateTime.Now:ddMMyyyy_HHmmss}"
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
                worksheet.Name = "Danh sách hợp đồng";

                // ✅ THÊM HEADER (Dòng 1)
                int colIndex = 1;
                foreach (DataGridViewColumn col in dgvThuePhong.Columns)
                {
                    if (col.Visible)
                    {
                        worksheet.Cells[1, colIndex] = col.HeaderText;

                        // Định dạng header
                        Excel.Range headerCell = worksheet.Cells[1, colIndex];
                        headerCell.Font.Bold = true;
                        headerCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightBlue);
                        headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        colIndex++;
                    }
                }

                // ✅ THÊM DỮ LIỆU (Từ dòng 2)
                int rowIndex = 2;
                foreach (DataGridViewRow row in rowsToExport)
                {
                    colIndex = 1;
                    foreach (DataGridViewColumn col in dgvThuePhong.Columns)
                    {
                        if (col.Visible)
                        {
                            var cellValue = row.Cells[col.Index].Value;

                            if (cellValue != null)
                            {
                                // Xử lý DateTime
                                if (cellValue is DateTime dateValue)
                                {
                                    worksheet.Cells[rowIndex, colIndex] = dateValue.ToString("dd/MM/yyyy");
                                }
                                // Xử lý số
                                else if (cellValue is decimal || cellValue is double || cellValue is int)
                                {
                                    worksheet.Cells[rowIndex, colIndex] = cellValue;
                                    Excel.Range numCell = worksheet.Cells[rowIndex, colIndex];
                                    numCell.NumberFormat = "#,##0.00";
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
        private void btnImport_Click(object sender, EventArgs e)
        {
            // ✅ HIỂN THỊ DIALOG CHỌN CHỨC NĂNG
            DialogResult result = MessageBox.Show(
                "Bạn muốn thực hiện thao tác nào?\n\n" +
                "☑ YES - Xuất file mẫu Excel\n" +
                "☑ NO - Nhập dữ liệu từ file Excel\n" +
                "☑ CANCEL - Hủy thao tác",
                "Chức năng Import/Export Hợp Đồng",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // XUẤT FILE MẪU
                ExportTemplateHopDong();
            }
            else if (result == DialogResult.No)
            {
                // NHẬP DỮ LIỆU TỪ FILE
                ImportHopDongFromExcel();
            }
        }
        private void ExportTemplateHopDong()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"MauNhapHopDong_{DateTime.Now:ddMMyyyy_HHmmss}.xlsx"
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
                worksheet.Name = "HopDong";

                // ===== TIÊU ĐỀ =====
                worksheet.Cells[1, 1] = "FILE MẪU NHẬP HỢP ĐỒNG THUÊ PHÒNG";
                Excel.Range title = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 13]];
                title.Merge();
                title.Font.Bold = true;
                title.Font.Size = 14;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                title.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);

                // ===== LƯU Ý =====
                worksheet.Cells[2, 1] = "LƯU Ý QUAN TRỌNG: ";
                Excel.Range noteTitle = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 13]];
                noteTitle.Merge();
                noteTitle.Font.Bold = true;
                noteTitle.Font.Color = ColorTranslator.ToOle(Color.Red);

                worksheet.Cells[3, 1] = "1. Định dạng ngày:  dd/mm/yyyy (VD: 15/03/2024) - Format ô thành 'Date' trước khi nhập";
                Excel.Range note1 = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[3, 13]];
                note1.Merge();
                note1.Font.Italic = true;

                worksheet.Cells[4, 1] = "2. Các cột có dấu (*) là bắt buộc phải nhập";
                Excel.Range note2 = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[4, 13]];
                note2.Merge();
                note2.Font.Italic = true;

                worksheet.Cells[5, 1] = "3. Thông tin thanh lý (NGAYKTTT, MANV_TL, NGAYKY_TL) để trống nếu chưa thanh lý";
                Excel.Range note3 = worksheet.Range[worksheet.Cells[5, 1], worksheet.Cells[5, 13]];
                note3.Merge();
                note3.Font.Italic = true;

                // ===== HEADER =====
                int headerRow = 7;
                string[] headers = new string[]
                {
            "Mã HĐ *",
            "Tên HĐ",
            "Mã SV *",
            "Mã Phòng *",
            "Từ ngày *\n(dd/mm/yyyy)",
            "Đến ngày *\n(dd/mm/yyyy)",
            "Thời hạn\n(tháng)",
            "Đơn giá *",
            "Tổng tiền",
            "Mã NV ký",
            "Ngày ký\n(dd/mm/yyyy)",
            "Ngày thanh lý\n(dd/mm/yyyy)",
            "Mã NV thanh lý",
            "Ngày ký TL\n(dd/mm/yyyy)"
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

                // Mẫu 1: Hợp đồng đang còn hiệu lực
                worksheet.Cells[dataRow, 1] = "HD001";
                worksheet.Cells[dataRow, 2] = "Hợp đồng thuê phòng KTX";
                worksheet.Cells[dataRow, 3] = "SV001";
                worksheet.Cells[dataRow, 4] = "A101";
                worksheet.Cells[dataRow, 5] = "01/09/2024";
                worksheet.Cells[dataRow, 6] = "31/08/2025";
                worksheet.Cells[dataRow, 7] = 12;
                worksheet.Cells[dataRow, 8] = 500000;
                worksheet.Cells[dataRow, 9] = 6000000;
                worksheet.Cells[dataRow, 10] = "NV001";
                worksheet.Cells[dataRow, 11] = "25/08/2024";
                worksheet.Cells[dataRow, 12] = ""; // Chưa thanh lý
                worksheet.Cells[dataRow, 13] = "";
                worksheet.Cells[dataRow, 14] = "";

                dataRow++;

                // Mẫu 2: Hợp đồng đã thanh lý
                worksheet.Cells[dataRow, 1] = "HD002";
                worksheet.Cells[dataRow, 2] = "Hợp đồng thuê phòng KTX";
                worksheet.Cells[dataRow, 3] = "SV002";
                worksheet.Cells[dataRow, 4] = "B201";
                worksheet.Cells[dataRow, 5] = "01/09/2023";
                worksheet.Cells[dataRow, 6] = "31/08/2024";
                worksheet.Cells[dataRow, 7] = 12;
                worksheet.Cells[dataRow, 8] = 450000;
                worksheet.Cells[dataRow, 9] = 5400000;
                worksheet.Cells[dataRow, 10] = "NV001";
                worksheet.Cells[dataRow, 11] = "20/08/2023";
                worksheet.Cells[dataRow, 12] = "15/06/2024"; // Đã thanh lý trước hạn
                worksheet.Cells[dataRow, 13] = "NV002";
                worksheet.Cells[dataRow, 14] = "15/06/2024";

                // ===== FORMAT CÁC CỘT NGÀY =====
                Excel.Range dateColumns = worksheet.Range[worksheet.Cells[dataRow - 1, 5], worksheet.Cells[dataRow + 100, 6]];
                dateColumns.NumberFormat = "dd/mm/yyyy";

                Excel.Range dateColumns2 = worksheet.Range[worksheet.Cells[dataRow - 1, 11], worksheet.Cells[dataRow + 100, 11]];
                dateColumns2.NumberFormat = "dd/mm/yyyy";

                Excel.Range dateColumns3 = worksheet.Range[worksheet.Cells[dataRow - 1, 12], worksheet.Cells[dataRow + 100, 12]];
                dateColumns3.NumberFormat = "dd/mm/yyyy";

                Excel.Range dateColumns4 = worksheet.Range[worksheet.Cells[dataRow - 1, 14], worksheet.Cells[dataRow + 100, 14]];
                dateColumns4.NumberFormat = "dd/mm/yyyy";

                // ===== FORMAT CỘT TIỀN =====
                Excel.Range moneyColumns = worksheet.Range[worksheet.Cells[dataRow - 1, 8], worksheet.Cells[dataRow + 100, 9]];
                moneyColumns.NumberFormat = "#,##0";

                // ===== ĐỊNH DẠNG CỘT =====
                worksheet.Columns[1].ColumnWidth = 12;  // Mã HĐ
                worksheet.Columns[2].ColumnWidth = 25;  // Tên HĐ
                worksheet.Columns[3].ColumnWidth = 12;  // Mã SV
                worksheet.Columns[4].ColumnWidth = 12;  // Mã Phòng
                worksheet.Columns[5].ColumnWidth = 13;  // Từ ngày
                worksheet.Columns[6].ColumnWidth = 13;  // Đến ngày
                worksheet.Columns[7].ColumnWidth = 10;  // Thời hạn
                worksheet.Columns[8].ColumnWidth = 13;  // Đơn giá
                worksheet.Columns[9].ColumnWidth = 13;  // Tổng tiền
                worksheet.Columns[10].ColumnWidth = 12; // Mã NV
                worksheet.Columns[11].ColumnWidth = 13; // Ngày ký
                worksheet.Columns[12].ColumnWidth = 13; // Ngày thanh lý
                worksheet.Columns[13].ColumnWidth = 14; // Mã NV TL
                worksheet.Columns[14].ColumnWidth = 13; // Ngày ký TL

                // ===== VẼ VIỀN =====
                Excel.Range tableRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[dataRow + 50, 14]];
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // ===== LƯU FILE =====
                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();

                MessageBox.Show(
                    "Xuất file mẫu thành công!\n\n" +
                    "📄 File chứa:\n" +
                    "  - Hướng dẫn nhập ngày\n" +
                    "  - 2 mẫu dữ liệu (HĐ bình thường và HĐ đã thanh lý)\n" +
                    "  - Format sẵn cột ngày và tiền\n\n" +
                    "⚠️ Lưu ý:  Format ô ngày thành 'Date' trước khi nhập! ",
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
        private void ImportHopDongFromExcel()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                Title = "Chọn file Excel để nhập hợp đồng"
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
                List<HopDongImportData> danhSachHD = ReadHopDongFromExcel(worksheet);

                workbook.Close(false);

                if (danhSachHD.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hợp lệ để import!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // IMPORT VÀO DATABASE
                ImportHopDongToDatabase(danhSachHD);
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
        private class HopDongImportData
        {
            public string MAHD { get; set; }
            public string TENHD { get; set; }
            public string MASV { get; set; }
            public string MA_PHONG { get; set; }
            public DateTime TUNGAY { get; set; }
            public DateTime DENNGAY { get; set; }
            public int? THOIHAN { get; set; }
            public decimal DONGIA { get; set; }
            public decimal? TONGTIEN { get; set; }
            public string MANV { get; set; }
            public DateTime? NGAYKY { get; set; }
            public DateTime? NGAYKTTT { get; set; }
            public string MANV_TL { get; set; }
            public DateTime? NGAYKY_TL { get; set; }
        }

        private List<HopDongImportData> ReadHopDongFromExcel(Excel.Worksheet worksheet)
        {
            List<HopDongImportData> list = new List<HopDongImportData>();
            int row = 8; // Bắt đầu từ dòng 8 (sau header và mẫu)

            while (true)
            {
                string maHD = worksheet.Cells[row, 1].Value?.ToString()?.Trim();

                if (string.IsNullOrEmpty(maHD))
                    break;

                try
                {
                    HopDongImportData hd = new HopDongImportData
                    {
                        MAHD = maHD,
                        TENHD = worksheet.Cells[row, 2].Value?.ToString()?.Trim(),
                        MASV = worksheet.Cells[row, 3].Value?.ToString()?.Trim(),
                        MA_PHONG = worksheet.Cells[row, 4].Value?.ToString()?.Trim(),
                        TUNGAY = ParseExcelDate(worksheet.Cells[row, 5].Value),
                        DENNGAY = ParseExcelDate(worksheet.Cells[row, 6].Value),
                        THOIHAN = ParseInt(worksheet.Cells[row, 7].Value),
                        DONGIA = ParseDecimal(worksheet.Cells[row, 8].Value),
                        TONGTIEN = ParseDecimalNullable(worksheet.Cells[row, 9].Value),
                        MANV = worksheet.Cells[row, 10].Value?.ToString()?.Trim(),
                        NGAYKY = ParseExcelDateNullable(worksheet.Cells[row, 11].Value),
                        NGAYKTTT = ParseExcelDateNullable(worksheet.Cells[row, 12].Value),
                        MANV_TL = worksheet.Cells[row, 13].Value?.ToString()?.Trim(),
                        NGAYKY_TL = ParseExcelDateNullable(worksheet.Cells[row, 14].Value)
                    };

                    // Validate bắt buộc
                    if (string.IsNullOrEmpty(hd.MASV) || string.IsNullOrEmpty(hd.MA_PHONG))
                    {
                        throw new Exception($"Dòng {row}: Thiếu Mã SV hoặc Mã Phòng");
                    }

                    list.Add(hd);
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

        // Helper methods để parse dữ liệu
        private DateTime ParseExcelDate(object value)
        {
            if (value == null) return DateTime.Now;

            if (value is DateTime dt)
                return dt;

            if (value is double dbl)
                return DateTime.FromOADate(dbl);

            if (DateTime.TryParse(value.ToString(), out DateTime result))
                return result;

            return DateTime.Now;
        }

        private DateTime? ParseExcelDateNullable(object value)
        {
            if (value == null) return null;

            if (value is DateTime dt)
                return dt;

            if (value is double dbl)
                return DateTime.FromOADate(dbl);

            if (DateTime.TryParse(value.ToString(), out DateTime result))
                return result;

            return null;
        }

        private int? ParseInt(object value)
        {
            if (value == null) return null;

            if (value is int i)
                return i;

            if (int.TryParse(value.ToString(), out int result))
                return result;

            return null;
        }

        private decimal ParseDecimal(object value)
        {
            if (value == null) return 0;

            if (value is decimal d)
                return d;

            if (decimal.TryParse(value.ToString(), out decimal result))
                return result;

            return 0;
        }

        private decimal? ParseDecimalNullable(object value)
        {
            if (value == null) return null;

            if (value is decimal d)
                return d;

            if (decimal.TryParse(value.ToString(), out decimal result))
                return result;

            return null;
        }

        private void ImportHopDongToDatabase(List<HopDongImportData> danhSachHD)
        {
            int soHDThem = 0;
            int soHDBiTrung = 0;
            List<string> errors = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        foreach (var hd in danhSachHD)
                        {
                            // Kiểm tra tồn tại
                            string checkQuery = "SELECT COUNT(*) FROM HOPDONG WHERE MAHD = @MAHD";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@MAHD", hd.MAHD);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    soHDBiTrung++;
                                    continue;
                                }
                            }

                            // Insert
                            string insertQuery = @"INSERT INTO HOPDONG 
                        (MAHD, TENHD, MASV, MA_PHONG, TUNGAY, DENNGAY, THOIHAN, DONGIA, TONGTIEN, 
                         MANV, NGAYKY, NGAYKTTT, MANV_TL, NGAYKY_TL)
                        VALUES 
                        (@MAHD, @TENHD, @MASV, @MA_PHONG, @TUNGAY, @DENNGAY, @THOIHAN, @DONGIA, @TONGTIEN,
                         @MANV, @NGAYKY, @NGAYKTTT, @MANV_TL, @NGAYKY_TL)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@MAHD", hd.MAHD);
                                insertCmd.Parameters.AddWithValue("@TENHD", (object)hd.TENHD ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@MASV", hd.MASV);
                                insertCmd.Parameters.AddWithValue("@MA_PHONG", hd.MA_PHONG);
                                insertCmd.Parameters.AddWithValue("@TUNGAY", hd.TUNGAY);
                                insertCmd.Parameters.AddWithValue("@DENNGAY", hd.DENNGAY);
                                insertCmd.Parameters.AddWithValue("@THOIHAN", (object)hd.THOIHAN ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@DONGIA", hd.DONGIA);
                                insertCmd.Parameters.AddWithValue("@TONGTIEN", (object)hd.TONGTIEN ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@MANV", (object)hd.MANV ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@NGAYKY", (object)hd.NGAYKY ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@NGAYKTTT", (object)hd.NGAYKTTT ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@MANV_TL", (object)hd.MANV_TL ?? DBNull.Value);
                                insertCmd.Parameters.AddWithValue("@NGAYKY_TL", (object)hd.NGAYKY_TL ?? DBNull.Value);

                                insertCmd.ExecuteNonQuery();
                                soHDThem++;
                            }
                        }

                        transaction.Commit();

                        string message = "✅ IMPORT HỢP ĐỒNG THÀNH CÔNG!\n\n";
                        message += $"📊 KẾT QUẢ:\n";
                        message += $"  - Thêm mới: {soHDThem} hợp đồng\n";
                        message += $"  - Trùng (bỏ qua): {soHDBiTrung} hợp đồng\n";

                        if (errors.Count > 0)
                        {
                            message += $"\n⚠️ CÓ {errors.Count} LỖI:\n";
                            message += string.Join("\n", errors.Take(5));
                        }

                        MessageBox.Show(message, "Kết quả Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh dữ liệu
                        LoadAllContracts();
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