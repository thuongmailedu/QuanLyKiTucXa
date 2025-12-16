using QuanLyKiTucXa.Formadd.DM_KHAC_FORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Linq;

namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    public partial class UC_DM_LOP_KHOA : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        // Biến theo dõi trạng thái lọc
        private bool isFilterMode_KHOA = false;
        private bool isFilterMode_LOP = false;

        public UC_DM_LOP_KHOA()
        {
            InitializeComponent();
            this.Load += UC_DM_LOP_KHOA_Load;
        }

        private void UC_DM_LOP_KHOA_Load(object sender, EventArgs e)
        {
            InitializeFilterControls();
            LoadDanhSachKhoa();
            LoadDanhSachLop();
        }

        private void InitializeFilterControls()
        {
            try
            {
                // ✅ Khởi tạo ComboBox Filter cho KHOA
                if (comfillter_KHOA.Items.Count == 0)
                {
                    comfillter_KHOA.Items.Clear();
                    comfillter_KHOA.Items.Add("Mã khoa");
                    comfillter_KHOA.Items.Add("Tên khoa");
                    comfillter_KHOA.SelectedIndex = 0;
                }

                // ✅ Khởi tạo ComboBox Filter cho LỚP
                if (comfillter_LOP.Items.Count == 0)
                {
                    comfillter_LOP.Items.Clear();
                    comfillter_LOP.Items.Add("Mã lớp");
                    comfillter_LOP.Items.Add("Tên khoa");
                    comfillter_LOP.Items.Add("Tên lớp");
                    comfillter_LOP.SelectedIndex = 0;
                }

                // Reset trạng thái filter
                isFilterMode_KHOA = false;
                isFilterMode_LOP = false;

                if (btnfillter_KHOA != null)
                {
                    //btnfillter_KHOA.Text = "Lọc";
                    //btnfillter_KHOA.BackColor = System.Drawing.SystemColors.Control;
                }

                if (btnfillter_LOP != null)
                {
                    //btnfillter_LOP.Text = "Lọc";
                    //btnfillter_LOP.BackColor = System.Drawing.SystemColors.Control;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo controls: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region QUẢN LÝ KHOA

        private void LoadDanhSachKhoa()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        MAKHOA,
                                        TENKHOA
                                     FROM KHOA
                                     ORDER BY MAKHOA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_KHOA.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_KHOA.Rows.Add();

                            dgvDM_KHOA.Rows[index].Cells["STT"].Value = stt++;
                            dgvDM_KHOA.Rows[index].Cells["MAKHOA"].Value = row["MAKHOA"].ToString();
                            dgvDM_KHOA.Rows[index].Cells["TENKHOA"].Value = row["TENKHOA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CHỨC NĂNG LỌC KHOA
        private void btnfillter_KHOA_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode_KHOA)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter_KHOA();
                    isFilterMode_KHOA = false;
                    //btnfillter_KHOA.Text = "Lọc";
                    //btnfillter_KHOA.BackColor = System.Drawing.SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter_KHOA();
                    isFilterMode_KHOA = true;
                    //btnfillter_KHOA.Text = "Reset";
                    //btnfillter_KHOA.BackColor = System.Drawing.Color.LightCoral;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu khoa: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter_KHOA()
        {
            string query = @"SELECT 
                                MAKHOA,
                                TENKHOA
                             FROM KHOA
                             WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter_KHOA
            if (comfillter_KHOA.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch_KHOA.Text))
            {
                string selectedFilter = comfillter_KHOA.SelectedItem.ToString();
                string searchValue = txtSearch_KHOA.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã khoa":
                        query += " AND MAKHOA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Tên khoa":
                        query += " AND TENKHOA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                }
            }

            query += " ORDER BY MAKHOA";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khoa nào phù hợp với điều kiện lọc! ",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Hiển thị lên DataGridView
            dgvDM_KHOA.Rows.Clear();
            int stt = 1;
            foreach (DataRow row in dt.Rows)
            {
                int index = dgvDM_KHOA.Rows.Add();
                dgvDM_KHOA.Rows[index].Cells["STT"].Value = stt++;
                dgvDM_KHOA.Rows[index].Cells["MAKHOA"].Value = row["MAKHOA"].ToString();
                dgvDM_KHOA.Rows[index].Cells["TENKHOA"].Value = row["TENKHOA"].ToString();
            }
        }

        private void ResetFilter_KHOA()
        {
            // Load lại toàn bộ dữ liệu
            LoadDanhSachKhoa();

            // Reset các control về trạng thái mặc định
            txtSearch_KHOA.Clear();

            if (comfillter_KHOA.Items.Count > 0)
                comfillter_KHOA.SelectedIndex = 0;
        }

        private void btnadd_KHOA_Click(object sender, EventArgs e)
        {
            frmadd_KHOA form = new frmadd_KHOA();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_KHOA)
                {
                    ApplyFilter_KHOA();
                }
                else
                {
                    LoadDanhSachKhoa();
                }
            }
        }

        private void btnedit_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvDM_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_KHOA.SelectedRows[0];
            string maKhoa = row.Cells["MAKHOA"].Value.ToString();

            frmadd_KHOA form = new frmadd_KHOA(maKhoa);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_KHOA)
                {
                    ApplyFilter_KHOA();
                }
                else
                {
                    LoadDanhSachKhoa();
                }

                // Reload lớp vì tên khoa có thể đổi
                if (isFilterMode_LOP)
                {
                    ApplyFilter_LOP();
                }
                else
                {
                    LoadDanhSachLop();
                }
            }
        }

        private void btndelete_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvDM_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_KHOA.SelectedRows[0];
            string maKhoa = row.Cells["MAKHOA"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có lớp nào thuộc khoa này không
                    string checkQuery = "SELECT COUNT(*) FROM LOP WHERE MAKHOA = @MAKHOA";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MAKHOA", maKhoa);
                        int countLop = (int)checkCmd.ExecuteScalar();

                        if (countLop > 0)
                        {
                            MessageBox.Show($"Không thể xóa khoa {maKhoa} vì đã có {countLop} lớp thuộc khoa này!\n" +
                                          "Vui lòng xóa các lớp trước khi xóa khoa.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa khoa {maKhoa}? ",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM KHOA WHERE MAKHOA = @MAKHOA";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MAKHOA", maKhoa);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa khoa thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (isFilterMode_KHOA)
                                {
                                    ApplyFilter_KHOA();
                                }
                                else
                                {
                                    LoadDanhSachKhoa();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region QUẢN LÝ LỚP

        private void LoadDanhSachLop()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        L.MALOP,
                                        L. TENLOP,
                                        L.MAKHOA,
                                        K.TENKHOA
                                     FROM LOP L
                                     INNER JOIN KHOA K ON L.MAKHOA = K. MAKHOA
                                     ORDER BY L.MAKHOA, L.MALOP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_LOP.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_LOP.Rows.Add();

                            dgvDM_LOP.Rows[index].Cells["STT_2"].Value = stt++;
                            dgvDM_LOP.Rows[index].Cells["MAKHOA_2"].Value = row["MAKHOA"].ToString();
                            dgvDM_LOP.Rows[index].Cells["TENKHOA_2"].Value = row["TENKHOA"].ToString();
                            dgvDM_LOP.Rows[index].Cells["MALOP"].Value = row["MALOP"].ToString();
                            dgvDM_LOP.Rows[index].Cells["TENLOP"].Value = row["TENLOP"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CHỨC NĂNG LỌC LỚP
        private void btnfillter_LOP_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFilterMode_LOP)
                {
                    // Đang ở chế độ lọc -> Reset về chế độ bình thường
                    ResetFilter_LOP();
                    isFilterMode_LOP = false;
                    btnfillter_LOP.Text = "Lọc";
                    btnfillter_LOP.BackColor = System.Drawing.SystemColors.Control;
                }
                else
                {
                    // Chế độ bình thường -> Thực hiện lọc
                    ApplyFilter_LOP();
                    isFilterMode_LOP = true;
                    btnfillter_LOP.Text = "Reset";
                    btnfillter_LOP.BackColor = System.Drawing.Color.LightCoral;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu lớp: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter_LOP()
        {
            string query = @"SELECT 
                                L. MALOP,
                                L.TENLOP,
                                L.MAKHOA,
                                K.TENKHOA
                             FROM LOP L
                             INNER JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                             WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            // ✅ Lọc theo comfillter_LOP
            if (comfillter_LOP.SelectedItem != null && !string.IsNullOrWhiteSpace(txtSearch_LOP.Text))
            {
                string selectedFilter = comfillter_LOP.SelectedItem.ToString();
                string searchValue = txtSearch_LOP.Text.Trim();

                switch (selectedFilter)
                {
                    case "Mã lớp":
                        query += " AND L.MALOP LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Tên khoa":
                        query += " AND K.TENKHOA LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                    case "Tên lớp":
                        query += " AND L.TENLOP LIKE @SearchValue";
                        parameters.Add(new SqlParameter("@SearchValue", "%" + searchValue + "%"));
                        break;
                }
            }

            query += " ORDER BY L.MAKHOA, L.MALOP";

            // Thực hiện truy vấn
            DataTable dt = ExecuteQuery(query, parameters.ToArray());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy lớp nào phù hợp với điều kiện lọc!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Hiển thị lên DataGridView
            dgvDM_LOP.Rows.Clear();
            int stt = 1;
            foreach (DataRow row in dt.Rows)
            {
                int index = dgvDM_LOP.Rows.Add();
                dgvDM_LOP.Rows[index].Cells["STT_2"].Value = stt++;
                dgvDM_LOP.Rows[index].Cells["MAKHOA_2"].Value = row["MAKHOA"].ToString();
                dgvDM_LOP.Rows[index].Cells["TENKHOA_2"].Value = row["TENKHOA"].ToString();
                dgvDM_LOP.Rows[index].Cells["MALOP"].Value = row["MALOP"].ToString();
                dgvDM_LOP.Rows[index].Cells["TENLOP"].Value = row["TENLOP"].ToString();
            }
        }

        private void ResetFilter_LOP()
        {
            // Load lại toàn bộ dữ liệu
            LoadDanhSachLop();

            // Reset các control về trạng thái mặc định
            txtSearch_LOP.Clear();

            if (comfillter_LOP.Items.Count > 0)
                comfillter_LOP.SelectedIndex = 0;
        }

        private void btnadd_NHA_Click(object sender, EventArgs e)
        {
            frmadd_LOP form = new frmadd_LOP();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_LOP)
                {
                    ApplyFilter_LOP();
                }
                else
                {
                    LoadDanhSachLop();
                }
            }
        }

        private void btnedit_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_LOP.SelectedRows[0];
            string maLop = row.Cells["MALOP"].Value.ToString();

            frmadd_LOP form = new frmadd_LOP(maLop);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (isFilterMode_LOP)
                {
                    ApplyFilter_LOP();
                }
                else
                {
                    LoadDanhSachLop();
                }
            }
        }

        private void btndelete_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_LOP.SelectedRows[0];
            string maLop = row.Cells["MALOP"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có sinh viên nào thuộc lớp này không
                    string checkQuery = "SELECT COUNT(*) FROM SINHVIEN WHERE MALOP = @MALOP";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MALOP", maLop);
                        int countSV = (int)checkCmd.ExecuteScalar();

                        if (countSV > 0)
                        {
                            MessageBox.Show($"Không thể xóa lớp {maLop} vì đã có {countSV} sinh viên thuộc lớp này!\n" +
                                          "Vui lòng chuyển sinh viên sang lớp khác trước khi xóa lớp.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa lớp {maLop}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM LOP WHERE MALOP = @MALOP";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MALOP", maLop);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa lớp thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (isFilterMode_LOP)
                                {
                                    ApplyFilter_LOP();
                                }
                                else
                                {
                                    LoadDanhSachLop();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region HELPER METHODS

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

        #endregion

        #region UNUSED EVENT HANDLERS

        private void lblSinhVien_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvDMPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        #endregion

        private void btnImport_Click(object sender, EventArgs e)
        {
            // ✅ HIỂN THỊ DIALOG CHỌN CHỨC NĂNG
            DialogResult result = MessageBox.Show(
                "Bạn muốn thực hiện thao tác nào?\n\n" +
                "☑ YES - Xuất file mẫu Excel\n" +
                "☑ NO - Nhập dữ liệu từ file Excel\n" +
                "☑ CANCEL - Hủy thao tác",
                "Chức năng Import/Export",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // XUẤT FILE MẪU
                ExportTemplateLopKhoa();
            }
            else if (result == DialogResult.No)
            {
                // NHẬP DỮ LIỆU TỪ FILE
                ImportDataFromExcel();
            }
            // Cancel thì không làm gì
        }
        private void ExportTemplateLopKhoa()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"MauNhapLopKhoa_{DateTime.Now:ddMMyyyy_HHmmss}.xlsx"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheetKhoa = null;
            Excel.Worksheet worksheetLop = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add();

                // ===== SHEET 1: KHOA =====
                worksheetKhoa = (Excel.Worksheet)workbook.Worksheets[1];
                worksheetKhoa.Name = "Khoa";

                // Tiêu đề
                worksheetKhoa.Cells[1, 1] = "FILE MẪU NHẬP DANH MỤC KHOA";
                Excel.Range titleKhoa = worksheetKhoa.Range[worksheetKhoa.Cells[1, 1], worksheetKhoa.Cells[1, 2]];
                titleKhoa.Merge();
                titleKhoa.Font.Bold = true;
                titleKhoa.Font.Size = 14;
                titleKhoa.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleKhoa.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);

                worksheetKhoa.Cells[2, 1] = "Lưu ý: Nhập sheet Khoa trước, sau đó nhập sheet Lớp";
                Excel.Range noteKhoa = worksheetKhoa.Range[worksheetKhoa.Cells[2, 1], worksheetKhoa.Cells[2, 2]];
                noteKhoa.Merge();
                noteKhoa.Font.Italic = true;
                noteKhoa.Font.Color = ColorTranslator.ToOle(Color.Red);

                // Header
                worksheetKhoa.Cells[4, 1] = "Mã Khoa *";
                worksheetKhoa.Cells[4, 2] = "Tên Khoa *";

                Excel.Range headerKhoa = worksheetKhoa.Range[worksheetKhoa.Cells[4, 1], worksheetKhoa.Cells[4, 2]];
                headerKhoa.Font.Bold = true;
                headerKhoa.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);
                headerKhoa.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Dữ liệu mẫu
                worksheetKhoa.Cells[5, 1] = "CNTT";
                worksheetKhoa.Cells[5, 2] = "Khoa Công nghệ thông tin";
                worksheetKhoa.Cells[6, 1] = "KTDL";
                worksheetKhoa.Cells[6, 2] = "Khoa Kế toán - Dữ liệu";

                worksheetKhoa.Columns[1].ColumnWidth = 15;
                worksheetKhoa.Columns[2].ColumnWidth = 40;

                // ===== SHEET 2: LỚP =====
                worksheetLop = (Excel.Worksheet)workbook.Worksheets.Add(After: worksheetKhoa);
                worksheetLop.Name = "Lop";

                // Tiêu đề
                worksheetLop.Cells[1, 1] = "FILE MẪU NHẬP DANH MỤC LỚP";
                Excel.Range titleLop = worksheetLop.Range[worksheetLop.Cells[1, 1], worksheetLop.Cells[1, 4]];
                titleLop.Merge();
                titleLop.Font.Bold = true;
                titleLop.Font.Size = 14;
                titleLop.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleLop.Interior.Color = ColorTranslator.ToOle(Color.LightCoral);

                worksheetLop.Cells[2, 1] = "Lưu ý: Cột 'Tên Khoa' sẽ tự động điền khi chọn Mã Khoa";
                Excel.Range noteLop = worksheetLop.Range[worksheetLop.Cells[2, 1], worksheetLop.Cells[2, 4]];
                noteLop.Merge();
                noteLop.Font.Italic = true;
                noteLop.Font.Color = ColorTranslator.ToOle(Color.Red);

                // Header
                worksheetLop.Cells[4, 1] = "Mã Khoa *";
                worksheetLop.Cells[4, 2] = "Tên Khoa";
                worksheetLop.Cells[4, 3] = "Mã Lớp *";
                worksheetLop.Cells[4, 4] = "Tên Lớp *";

                Excel.Range headerLop = worksheetLop.Range[worksheetLop.Cells[4, 1], worksheetLop.Cells[4, 4]];
                headerLop.Font.Bold = true;
                headerLop.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
                headerLop.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Dữ liệu mẫu
                worksheetLop.Cells[5, 1] = "CNTT";
                worksheetLop.Cells[5, 2] = "=IFERROR(VLOOKUP(A5,Khoa!$A$5:$B$100,2,FALSE),\"\")";
                worksheetLop.Cells[5, 3] = "CNTT01";
                worksheetLop.Cells[5, 4] = "Công nghệ thông tin 01";

                worksheetLop.Cells[6, 1] = "KTDL";
                worksheetLop.Cells[6, 2] = "=IFERROR(VLOOKUP(A6,Khoa!$A$5:$B$100,2,FALSE),\"\")";
                worksheetLop.Cells[6, 3] = "KTDL01";
                worksheetLop.Cells[6, 4] = "Kế toán - Dữ liệu 01";

                // ✅ TẠO DATA VALIDATION (Dropdown) cho cột Mã Khoa trong sheet Lớp
                Excel.Range validationRange = worksheetLop.Range[worksheetLop.Cells[5, 1], worksheetLop.Cells[100, 1]];
                validationRange.Validation.Delete();
                validationRange.Validation.Add(
                    Type: Excel.XlDVType.xlValidateList,
                    AlertStyle: Excel.XlDVAlertStyle.xlValidAlertStop,
                    Operator: Excel.XlFormatConditionOperator.xlBetween,
                    Formula1: "=Khoa!$A$5:$A$100"
                );
                validationRange.Validation.IgnoreBlank = true;
                validationRange.Validation.InCellDropdown = true;
                validationRange.Validation.ShowError = true;
                validationRange.Validation.ErrorTitle = "Lỗi";
                validationRange.Validation.ErrorMessage = "Vui lòng chọn Mã Khoa từ danh sách! ";

                // ✅ COPY FORMULA cho cột Tên Khoa
                Excel.Range formulaSource = worksheetLop.Range[worksheetLop.Cells[5, 2], worksheetLop.Cells[6, 2]];
                Excel.Range formulaDest = worksheetLop.Range[worksheetLop.Cells[5, 2], worksheetLop.Cells[100, 2]];
                formulaSource.Copy(formulaDest);

                // ✅ ĐỊNH DẠNG CỘT
                worksheetLop.Columns[1].ColumnWidth = 12;
                worksheetLop.Columns[2].ColumnWidth = 35;
                worksheetLop.Columns[3].ColumnWidth = 12;
                worksheetLop.Columns[4].ColumnWidth = 35;

                // ✅ BẢO VỆ SHEET (cột Tên Khoa read-only)
                try
                {
                    // Unlock toàn bộ sheet trước
                    worksheetLop.Cells.Locked = false;

                    // Lock riêng cột Tên Khoa (cột B từ dòng 5 đến 1000)
                    Excel.Range colTenKhoa = worksheetLop.Range[worksheetLop.Cells[5, 2], worksheetLop.Cells[1000, 2]];
                    colTenKhoa.Locked = true;

                    // Protect sheet
                    worksheetLop.Protect(
                        DrawingObjects: false,
                        Contents: true,
                        Scenarios: false,
                        AllowFormattingCells: true,
                        AllowFormattingColumns: true,
                        AllowFormattingRows: true,
                        AllowInsertingRows: true,
                        AllowDeletingRows: true
                    );
                }
                catch
                {
                    // Nếu protect lỗi thì bỏ qua, không quan trọng
                }

                // ===== LƯU FILE =====
                workbook.SaveAs(saveDialog.FileName);
                workbook.Close();

                MessageBox.Show(
                    "Xuất file mẫu thành công!\n\n" +
                    "📄 Sheet 'Khoa':  Nhập danh sách khoa\n" +
                    "📄 Sheet 'Lop': Nhập danh sách lớp (có dropdown chọn Mã Khoa)\n\n" +
                    "⚠️ Lưu ý: Import sheet Khoa trước, sau đó mới import sheet Lớp! ",
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
                if (worksheetLop != null) Marshal.ReleaseComObject(worksheetLop);
                if (worksheetKhoa != null) Marshal.ReleaseComObject(worksheetKhoa);
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
        private void ImportDataFromExcel()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls",
                FilterIndex = 1,
                RestoreDirectory = true,
                Title = "Chọn file Excel để nhập dữ liệu"
            };

            if (openDialog.ShowDialog() != DialogResult.OK) return;

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheetKhoa = null;
            Excel.Worksheet worksheetLop = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Open(openDialog.FileName);

                // Tìm sheet Khoa và Lop
                worksheetKhoa = FindWorksheet(workbook, "Khoa");
                worksheetLop = FindWorksheet(workbook, "Lop");

                if (worksheetKhoa == null || worksheetLop == null)
                {
                    MessageBox.Show(
                        "File Excel không đúng định dạng!\n\n" +
                        "Vui lòng sử dụng file mẫu có 2 sheet:  'Khoa' và 'Lop'",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ===== ĐỌC DỮ LIỆU TỪ EXCEL =====
                List<KhoaData> danhSachKhoa = ReadKhoaFromExcel(worksheetKhoa);
                List<LopData> danhSachLop = ReadLopFromExcel(worksheetLop);

                workbook.Close(false);

                // ===== IMPORT VÀO DATABASE =====
                ImportToDatabase(danhSachKhoa, danhSachLop);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập dữ liệu:  {ex.Message}\n\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (worksheetLop != null) Marshal.ReleaseComObject(worksheetLop);
                if (worksheetKhoa != null) Marshal.ReleaseComObject(worksheetKhoa);
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

        // Helper classes
        private class KhoaData
        {
            public string MaKhoa { get; set; }
            public string TenKhoa { get; set; }
        }

        private class LopData
        {
            public string MaKhoa { get; set; }
            public string MaLop { get; set; }
            public string TenLop { get; set; }
        }

        private Excel.Worksheet FindWorksheet(Excel.Workbook workbook, string sheetName)
        {
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
                {
                    return sheet;
                }
            }
            return null;
        }

        private List<KhoaData> ReadKhoaFromExcel(Excel.Worksheet worksheet)
        {
            List<KhoaData> list = new List<KhoaData>();
            int row = 5; // Bắt đầu từ dòng 5 (sau header)

            while (true)
            {
                string maKhoa = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                string tenKhoa = worksheet.Cells[row, 2].Value?.ToString()?.Trim();

                if (string.IsNullOrEmpty(maKhoa))
                    break;

                if (!string.IsNullOrEmpty(tenKhoa))
                {
                    list.Add(new KhoaData
                    {
                        MaKhoa = maKhoa,
                        TenKhoa = tenKhoa
                    });
                }

                row++;
            }

            return list;
        }

        private List<LopData> ReadLopFromExcel(Excel.Worksheet worksheet)
        {
            List<LopData> list = new List<LopData>();
            int row = 5; // Bắt đầu từ dòng 5 (sau header)

            while (true)
            {
                string maKhoa = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                string maLop = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                string tenLop = worksheet.Cells[row, 4].Value?.ToString()?.Trim();

                if (string.IsNullOrEmpty(maLop))
                    break;

                if (!string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(tenLop))
                {
                    list.Add(new LopData
                    {
                        MaKhoa = maKhoa,
                        MaLop = maLop,
                        TenLop = tenLop
                    });
                }

                row++;
            }

            return list;
        }

        private void ImportToDatabase(List<KhoaData> danhSachKhoa, List<LopData> danhSachLop)
        {
            int soKhoaThem = 0;
            int soKhoaBiTrung = 0;
            int soLopThem = 0;
            int soLopBiTrung = 0;
            List<string> errors = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // ===== INSERT KHOA TRƯỚC =====
                        foreach (var khoa in danhSachKhoa)
                        {
                            // Kiểm tra tồn tại
                            string checkQuery = "SELECT COUNT(*) FROM KHOA WHERE MAKHOA = @MAKHOA";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@MAKHOA", khoa.MaKhoa);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    soKhoaBiTrung++;
                                    continue; // Bỏ qua nếu đã tồn tại
                                }
                            }

                            // Insert
                            string insertQuery = "INSERT INTO KHOA (MAKHOA, TENKHOA) VALUES (@MAKHOA, @TENKHOA)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@MAKHOA", khoa.MaKhoa);
                                insertCmd.Parameters.AddWithValue("@TENKHOA", khoa.TenKhoa);
                                insertCmd.ExecuteNonQuery();
                                soKhoaThem++;
                            }
                        }

                        // ===== INSERT LỚP SAU =====
                        foreach (var lop in danhSachLop)
                        {
                            // Kiểm tra tồn tại
                            string checkQuery = "SELECT COUNT(*) FROM LOP WHERE MALOP = @MALOP";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@MALOP", lop.MaLop);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    soLopBiTrung++;
                                    continue;
                                }
                            }

                            // Kiểm tra khoa có tồn tại không
                            string checkKhoaQuery = "SELECT COUNT(*) FROM KHOA WHERE MAKHOA = @MAKHOA";
                            using (SqlCommand checkKhoaCmd = new SqlCommand(checkKhoaQuery, conn, transaction))
                            {
                                checkKhoaCmd.Parameters.AddWithValue("@MAKHOA", lop.MaKhoa);
                                int count = (int)checkKhoaCmd.ExecuteScalar();

                                if (count == 0)
                                {
                                    errors.Add($"Lớp '{lop.MaLop}':  Khoa '{lop.MaKhoa}' không tồn tại!");
                                    continue;
                                }
                            }

                            // Insert
                            string insertQuery = "INSERT INTO LOP (MALOP, TENLOP, MAKHOA) VALUES (@MALOP, @TENLOP, @MAKHOA)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@MALOP", lop.MaLop);
                                insertCmd.Parameters.AddWithValue("@TENLOP", lop.TenLop);
                                insertCmd.Parameters.AddWithValue("@MAKHOA", lop.MaKhoa);
                                insertCmd.ExecuteNonQuery();
                                soLopThem++;
                            }
                        }

                        transaction.Commit();

                        // Hiển thị kết quả
                        string message = "✅ IMPORT DỮ LIỆU THÀNH CÔNG!\n\n";
                        message += $"📊 KHOA:\n";
                        message += $"  - Thêm mới: {soKhoaThem}\n";
                        message += $"  - Trùng (bỏ qua): {soKhoaBiTrung}\n\n";
                        message += $"📊 LỚP:\n";
                        message += $"  - Thêm mới: {soLopThem}\n";
                        message += $"  - Trùng (bỏ qua): {soLopBiTrung}\n";

                        if (errors.Count > 0)
                        {
                            message += $"\n⚠️ CÓ {errors.Count} LỖI:\n";
                            message += string.Join("\n", errors.Take(5));
                            if (errors.Count > 5)
                                message += $"\n... và {errors.Count - 5} lỗi khác";
                        }

                        MessageBox.Show(message, "Kết quả Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh dữ liệu
                        LoadDanhSachKhoa();
                        LoadDanhSachLop();
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