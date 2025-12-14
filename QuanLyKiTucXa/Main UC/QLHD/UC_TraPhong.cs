using QuanLyKiTucXa.Formadd.QLHD_FORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLHD
{
    public partial class UC_TraPhong : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isFilterMode = false; // Biến theo dõi trạng thái lọc
        private bool isDateFilterEnabled = false; // Biến theo dõi trạng thái lọc theo ngày

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
            dgvTraPhong.MultiSelect = false;

            // ✅ Tắt tự động chọn dòng đầu tiên
            dgvTraPhong.ClearSelection();

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
                    comfillter.Items.Add("Mã HD");
                    comfillter.Items.Add("Mã SV");
                    comfillter.Items.Add("Mã Phòng");
                    comfillter.Items.Add("Mã Nhà");
                    comfillter.Items.Add("Mã NV thanh lý");
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
                    comNgay.Items.Add("Ngày ký thanh lý");
                    comNgay.SelectedIndex = 0; // Mặc định không lọc theo ngày
                }

                // ✅ Vô hiệu hóa DateTimePicker ban đầu
                dtpTHANG.Enabled = false;
                dtpTHANG.CustomFormat = " "; // Hiển thị rỗng
                dtpTHANG.Format = DateTimePickerFormat.Custom;

                // ✅ Sự kiện khi thay đổi ComboBox Ngày
                comNgay.SelectedIndexChanged += ComNgay_SelectedIndexChanged;

                // Load dữ liệu ban đầu
                LoadAllContracts();

                // Reset trạng thái filter
                isFilterMode = false;
                //btnfillter_TraPhong.Text = "Lọc";
                //btnfillter_TraPhong.BackColor = SystemColors.Control;
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
                dtpTHANG.Enabled = false;
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

        // ✅ CHỨC NĂNG LỌC HỢP ĐỒNG TRẢ PHÒNG
        private void btnfillter_TraPhong_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter();
                    isFilterMode = false;
                    //btnfillter_TraPhong.Text = "Lọc";
                    //btnfillter_TraPhong.BackColor = SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter();
                    isFilterMode = true;
                    //btnfillter_TraPhong.Text = "Reset";
                    //btnfillter_TraPhong.BackColor = Color.LightCoral;
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
                    hd. MAHD, 
                    hd.TENHD, 
                    sv.MASV, 
                    sv. TENSV, 
                    sv.GIOITINH, 
                    p.MA_PHONG, 
                    n.MANHA, 
                    n. LOAIPHONG, 
                    hd.TUNGAY, 
                    hd.DENNGAY,
                    hd.NGAYKTTT, 
                    hd.MANV_TL,
                    nv_tl.TENNV AS TENNV_THANHLY,
                    hd.NGAYKY_TL
                FROM SINHVIEN sv 
                INNER JOIN HOPDONG hd ON sv.MASV = hd.MASV
                INNER JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                INNER JOIN NHA n ON p. MANHA = n.MANHA
                LEFT JOIN NHANVIEN nv_tl ON nv_tl.MANV = hd.MANV_TL
                WHERE hd.NGAYKTTT IS NOT NULL";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter (text-based filter)
            if (comfillter.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string selectedFilter = comfillter.SelectedItem.ToString();
                string searchValue = txtSearch.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã HD":
                        query += " AND hd.MAHD LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã SV":
                        query += " AND sv.MASV LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã Phòng":
                        query += " AND p.MA_PHONG LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã Nhà":
                        query += " AND n.MANHA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Mã NV thanh lý":
                        query += " AND hd.MANV_TL LIKE @SearchValue";
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
                        query += " AND CAST(hd.TUNGAY AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày kết thúc":
                        query += " AND CAST(hd.DENNGAY AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày thanh lý":
                        query += " AND CAST(hd.NGAYKTTT AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                    case "Ngày ký thanh lý":
                        query += " AND CAST(hd.NGAYKY_TL AS DATE) = @DateValue";
                        parameters.Add(new SqlParameter("@DateValue", dateValue));
                        break;
                }
            }

            query += " ORDER BY hd. NGAYKTTT DESC, hd.MAHD";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hợp đồng nào phù hợp với điều kiện lọc! ",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvTraPhong.DataSource = dt;
           // FormatDataGridView();
            dgvTraPhong.ClearSelection();
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
                    hd.MAHD, 
                    hd. TENHD, 
                    sv.MASV, 
                    sv.TENSV, 
                    sv.GIOITINH, 
                    p.MA_PHONG, 
                    n. MANHA, 
                    n.LOAIPHONG, 
                    hd.TUNGAY, 
                    hd.DENNGAY,
                    hd.NGAYKTTT, 
                    hd.MANV_TL,
                    nv_tl.TENNV AS TENNV_THANHLY,
                    hd.NGAYKY_TL
                FROM SINHVIEN sv 
                INNER JOIN HOPDONG hd ON sv.MASV = hd.MASV
                INNER JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                INNER JOIN NHA n ON p.MANHA = n. MANHA
                LEFT JOIN NHANVIEN nv_tl ON nv_tl. MANV = hd. MANV_TL
                WHERE hd.NGAYKTTT IS NOT NULL
                ORDER BY hd.NGAYKTTT DESC, hd. MAHD";

            DataTable dt = ExecuteQuery(query);
            dgvTraPhong.DataSource = dt;
           // FormatDataGridView();
            dgvTraPhong.ClearSelection();
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

        private void FormatDataGridView()
        {
            if (dgvTraPhong.Columns.Count == 0) return;

            try
            {
                // ✅ Đặt tiêu đề cho các cột theo đúng thứ tự
                dgvTraPhong.Columns["MAHD"].HeaderText = "Mã HĐ";
                dgvTraPhong.Columns["TENHD"].HeaderText = "Tên HĐ";
                dgvTraPhong.Columns["MASV"].HeaderText = "Mã SV";
                dgvTraPhong.Columns["TENSV"].HeaderText = "Tên sinh viên";
                dgvTraPhong.Columns["GIOITINH"].HeaderText = "Giới tính";
                dgvTraPhong.Columns["MA_PHONG"].HeaderText = "Mã phòng";
                dgvTraPhong.Columns["MANHA"].HeaderText = "Mã nhà";
                dgvTraPhong.Columns["LOAIPHONG"].HeaderText = "Loại phòng";
                dgvTraPhong.Columns["TUNGAY"].HeaderText = "Từ ngày";
                dgvTraPhong.Columns["DENNGAY"].HeaderText = "Đến ngày";
                dgvTraPhong.Columns["NGAYKTTT"].HeaderText = "Ngày thanh lý";
                dgvTraPhong.Columns["MANV_TL"].HeaderText = "Mã NV thanh lý";
                dgvTraPhong.Columns["TENNV_THANHLY"].HeaderText = "Tên NV thanh lý";
                dgvTraPhong.Columns["NGAYKY_TL"].HeaderText = "Ngày ký TL";

                // ✅ Format các cột ngày tháng
                string[] dateColumns = { "TUNGAY", "DENNGAY", "NGAYKTTT", "NGAYKY_TL" };
                foreach (string colName in dateColumns)
                {
                    if (dgvTraPhong.Columns[colName] != null)
                    {
                        dgvTraPhong.Columns[colName].DefaultCellStyle.Format = "dd/MM/yyyy";
                        dgvTraPhong.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                // ✅ Căn giữa các cột khác
                dgvTraPhong.Columns["MAHD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTraPhong.Columns["MASV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTraPhong.Columns["GIOITINH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTraPhong.Columns["MA_PHONG"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTraPhong.Columns["MANHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvTraPhong.Columns["MANV_TL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Tự động điều chỉnh độ rộng cột
                dgvTraPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi format DataGridView: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ GIỮ NGUYÊN CÁC CHỨC NĂNG CŨ
        private void LoadData(string searchType = null, string keyword = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ Cập nhật query để lấy thông tin người thanh lý (MANV_TL) và NGAYKY_TL
                    string sql = @"SELECT 
                                    hd. MAHD, 
                                    hd.TENHD, 
                                    sv. MASV, 
                                    sv.TENSV, 
                                    sv.GIOITINH, 
                                    p.MA_PHONG, 
                                    n. MANHA, 
                                    n.LOAIPHONG, 
                                    hd.TUNGAY, 
                                    hd.DENNGAY,
                                    hd.NGAYKTTT, 
                                    hd. MANV_TL,
                                    nv_tl. TENNV AS TENNV_THANHLY,
                                    hd.NGAYKY_TL
                                   FROM SINHVIEN sv 
                                   INNER JOIN HOPDONG hd ON sv. MASV = hd. MASV
                                   INNER JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                                   INNER JOIN NHA n ON p.MANHA = n.MANHA
                                   LEFT JOIN NHANVIEN nv_tl ON nv_tl.MANV = hd.MANV_TL
                                   WHERE hd. NGAYKTTT IS NOT NULL";

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
                    // Refresh lại danh sách
                    if (isFilterMode)
                    {
                        ApplyFilter(); // Nếu đang ở chế độ lọc
                    }
                    else
                    {
                        LoadAllContracts(); // Nếu ở chế độ bình thường
                    }
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
                        // Refresh lại danh sách
                        if (isFilterMode)
                        {
                            ApplyFilter(); // Nếu đang ở chế độ lọc
                        }
                        else
                        {
                            LoadAllContracts(); // Nếu ở chế độ bình thường
                        }
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

                        // ✅ Xóa cả NGAYKTTT, MANV_TL và NGAYKY_TL
                        string query = @"UPDATE HOPDONG 
                                        SET NGAYKTTT = NULL, 
                                            MANV_TL = NULL,
                                            NGAYKY_TL = NULL 
                                        WHERE MAHD = @MAHD";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MAHD", maHD);
                            int affectedRows = cmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Đã xóa ngày thanh lý thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh lại danh sách
                                if (isFilterMode)
                                {
                                    ApplyFilter(); // Nếu đang ở chế độ lọc
                                }
                                else
                                {
                                    LoadAllContracts(); // Nếu ở chế độ bình thường
                                }
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

        private void dgvTraPhong_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Vẽ số thứ tự ở cột đầu tiên
            dgvTraPhong.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
    }
}