using QuanLyKiTucXa.Formadd.QLHD_FORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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

            dgvThuePhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuePhong.MultiSelect = false;

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

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void dgvThuePhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý sự kiện click vào cell (nếu cần)
        }
    }
}