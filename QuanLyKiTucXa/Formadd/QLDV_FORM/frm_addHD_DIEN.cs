using QuanLyKiTucXa.Formadd.HSSV;
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

namespace QuanLyKiTucXa.Formadd.QLDV_FORM
{
    public partial class frm_addHD_DIEN : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private int editingRowIndex = -1;
        private string originalMaHD = ""; // Lưu mã hóa đơn gốc khi edit

        // Constructor mặc định cho chế độ thêm mới
        public frm_addHD_DIEN()
        {
            InitializeComponent();
            isEditMode = false;
        }

        // Constructor cho chế độ chỉnh sửa
        public frm_addHD_DIEN(string maHD, string tenHD, string maDV, string maNha, string maPhong,
            double chiSoCu, double chiSoMoi, decimal donGia, string thoiGian, string ngayGhi, string trangThai)
        {
            InitializeComponent();
            isEditMode = true;
            originalMaHD = maHD; // Lưu mã hóa đơn gốc

            // Lưu tạm các giá trị để load sau
            this.Tag = new
            {
                MaHD = maHD,
                TenHD = tenHD,
                MaDV = maDV,
                MaNha = maNha,
                MaPhong = maPhong,
                ChiSoCu = chiSoCu,
                ChiSoMoi = chiSoMoi,
                DonGia = donGia,
                ThoiGian = thoiGian,
                NgayGhi = ngayGhi,
                TrangThai = trangThai
            };
        }

        private void frm_addHD_DIEN_Load(object sender, EventArgs e)
        {
            // Load dữ liệu trước
            LoadComboBoxNha();
            LoadComboBoxTrangThai();

            txtTENHD.Text = "Hóa đơn tiền điện";
            txtTENHD.ReadOnly = true;

            // Đổi label theo mode
            if (isEditMode)
            {
                lblformtype.Text = "Chỉnh sửa hóa đơn Điện"; // ← ĐỔI LABEL

                // Load dữ liệu từ Tag
                dynamic data = this.Tag;

                txtMAHD_DIEN.Text = data.MaHD;
                txtMAHD_DIEN.ReadOnly = true; // Không cho sửa mã

                // Đợi combobox load xong rồi mới set giá trị
                comNHA.SelectedIndexChanged -= comNHA_SelectedIndexChanged; // Tắt event tạm
                comNHA.Text = data.MaNha;

                // Load phòng trước
                LoadComboBoxPhong(data.MaNha);
                comPHONG.Text = data.MaPhong;

                // Set các giá trị khác
                txtCHISOCU.Text = data.ChiSoCu.ToString();
                txtCHISOMOI.Text = data.ChiSoMoi.ToString();
                txtTIEUTHU.Text = (data.ChiSoMoi - data.ChiSoCu).ToString();
                txtDONGIA.Text = ((decimal)data.DonGia).ToString("N0");
                txtTONGTIEN.Text = (((double)data.ChiSoMoi - (double)data.ChiSoCu) * (double)data.DonGia).ToString("N0");

                // Parse thời gian từ MM/yyyy
                if (!string.IsNullOrEmpty(data.ThoiGian) && data.ThoiGian.Contains("/"))
                {
                    string[] parts = data.ThoiGian.Split('/');
                    dtpTHOIGIAN.Value = new DateTime(int.Parse(parts[1]), int.Parse(parts[0]), 1);
                }

                // Parse ngày ghi từ dd/MM/yyyy
                if (!string.IsNullOrEmpty(data.NgayGhi) && data.NgayGhi.Contains("/"))
                {
                    string[] parts = data.NgayGhi.Split('/');
                    dtpNGAYGHI.Value = new DateTime(int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0]));
                }

                comTRANGTHAI.Text = data.TrangThai;

                // Thêm vào gridview ngay
                AddToGridView();

                comNHA.SelectedIndexChanged += comNHA_SelectedIndexChanged; // Bật lại event
            }
            else
            {
                lblformtype.Text = "Thêm mới hóa đơn Điện";
                GenerateMaHoaDon();
                comTRANGTHAI.SelectedIndex = 1; // Mặc định "Chưa thanh toán"
            }

            // Đăng ký sự kiện SAU KHI đã load xong dữ liệu
            comNHA.SelectedIndexChanged += comNHA_SelectedIndexChanged;
            comPHONG.SelectedIndexChanged += comPHONG_SelectedIndexChanged;
            txtCHISOMOI.TextChanged += CalculateTieuThu;
            txtCHISOCU.TextChanged += CalculateTieuThu;
            dtpTHOIGIAN.ValueChanged += dtpTHOIGIAN_ValueChanged;
            btn_Luu.Click += btnLuu_Click;
            btn_Huy.Click += btnHuy_Click;
            btnedit.Click += btnedit_Click;
            btnDelete.Click += btnDelete_Click;

            // Load đơn giá khi form mới mở (chỉ khi không phải edit mode)
            if (!isEditMode)
            {
                LoadDonGiaDien();

                if (comPHONG.SelectedValue != null && comPHONG.Items.Count > 0)
                {
                    LoadChiSoCu(comPHONG.SelectedValue.ToString());
                }
            }
        }

        private void AddToGridView()
        {
            DataTable dt = dgv_addHD_DIEN.DataSource as DataTable;
            if (dt == null)
            {
                dt = CreateDataTable();
                dgv_addHD_DIEN.DataSource = dt;
            }

            DataRow newRow = dt.NewRow();
            newRow["STT"] = 1;
            newRow["MAHD_DIEN"] = txtMAHD_DIEN.Text;
            newRow["TENHD"] = txtTENHD.Text;

            // Lấy mã dịch vụ
            dynamic data = this.Tag;
            newRow["MADV"] = data.MaDV;

            newRow["MANHA"] = comNHA.Text;
            newRow["MA_PHONG"] = comPHONG.Text;
            newRow["CHISOCU"] = Convert.ToDouble(txtCHISOCU.Text);
            newRow["CHISOMOI"] = Convert.ToDouble(txtCHISOMOI.Text);
            newRow["SODIEN"] = Convert.ToDouble(txtTIEUTHU.Text);
            newRow["DONGIA"] = Convert.ToDecimal(txtDONGIA.Text.Replace(",", ""));
            newRow["TONGTIEN"] = Convert.ToDecimal(txtTONGTIEN.Text.Replace(",", ""));
            newRow["THOIGIAN"] = dtpTHOIGIAN.Value;
            newRow["NGAYGHI"] = dtpNGAYGHI.Value;
            newRow["TINHTRANGTT"] = comTRANGTHAI.Text;

            dt.Rows.Add(newRow);
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

                    comNHA.DataSource = dt;
                    comNHA.DisplayMember = "MANHA";
                    comNHA.ValueMember = "MANHA";
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
            comTRANGTHAI.Items.Add("Đã thanh toán");
            comTRANGTHAI.Items.Add("Chưa thanh toán");
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue != null && !isEditMode)
            {
                LoadComboBoxPhong(comNHA.SelectedValue.ToString());
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

                    comPHONG.DataSource = dt;
                    comPHONG.DisplayMember = "MA_PHONG";
                    comPHONG.ValueMember = "MA_PHONG";

                    if (comPHONG.Items.Count > 0 && !isEditMode)
                    {
                        LoadChiSoCu(comPHONG.SelectedValue.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách phòng: " + ex.Message);
            }
        }

        private void comPHONG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comPHONG.SelectedValue != null && !isEditMode)
            {
                LoadChiSoCu(comPHONG.SelectedValue.ToString());
            }
        }

        private void LoadChiSoCu(string maPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP 1 CHISOMOI 
                                    FROM HD_DIEN 
                                    WHERE MA_PHONG = @MA_PHONG 
                                    AND THOIGIAN < @THOIGIAN
                                    ORDER BY THOIGIAN DESC, MAHD_DIEN DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                    cmd.Parameters.AddWithValue("@THOIGIAN", dtpTHOIGIAN.Value);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtCHISOCU.Text = result.ToString();
                    }
                    else
                    {
                        txtCHISOCU.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load chỉ số cũ: " + ex.Message);
            }
        }

        private void dtpTHOIGIAN_ValueChanged(object sender, EventArgs e)
        {
            if (!isEditMode) // Chỉ load khi không phải edit mode
            {
                LoadDonGiaDien();
                if (comPHONG.SelectedValue != null)
                {
                    LoadChiSoCu(comPHONG.SelectedValue.ToString());
                }
            }
        }

        private void LoadDonGiaDien()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP 1 DONGIA 
                                    FROM DICHVU 
                                    WHERE MADV LIKE 'DIEN_%'
                                    AND TUNGAY <= @THOIGIAN 
                                    AND (DENNGAY IS NULL OR DENNGAY >= @THOIGIAN)
                                    ORDER BY TUNGAY DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@THOIGIAN", dtpTHOIGIAN.Value);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        decimal donGia = Convert.ToDecimal(result);
                        txtDONGIA.Text = donGia.ToString("N0");

                        CalculateTongTien();
                    }
                    else
                    {
                        txtDONGIA.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load đơn giá: " + ex.Message);
            }
        }

        private void CalculateTieuThu(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCHISOCU.Text) && !string.IsNullOrEmpty(txtCHISOMOI.Text))
                {
                    double chiSoCu = Convert.ToDouble(txtCHISOCU.Text);
                    double chiSoMoi = Convert.ToDouble(txtCHISOMOI.Text);
                    double tieuThu = chiSoMoi - chiSoCu;

                    txtTIEUTHU.Text = tieuThu.ToString();

                    CalculateTongTien();
                }
            }
            catch { }
        }

        private void CalculateTongTien()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTIEUTHU.Text) && !string.IsNullOrEmpty(txtDONGIA.Text))
                {
                    double tieuThu = Convert.ToDouble(txtTIEUTHU.Text);
                    decimal donGia = Convert.ToDecimal(txtDONGIA.Text.Replace(",", ""));
                    decimal tongTien = (decimal)tieuThu * donGia;
                    txtTONGTIEN.Text = tongTien.ToString("N0");
                }
            }
            catch { }
        }

        private void GenerateMaHoaDon()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT ISNULL(MAX(CAST(SUBSTRING(MAHD_DIEN, 7, 7) AS INT)), 0) + 1 
                                    FROM HD_DIEN 
                                    WHERE MAHD_DIEN LIKE 'HDDIEN%'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();
                    int nextNum = result != null ? Convert.ToInt32(result) : 1;

                    if (dgv_addHD_DIEN.DataSource != null)
                    {
                        DataTable dt = (DataTable)dgv_addHD_DIEN.DataSource;
                        if (dt.Rows.Count > 0)
                        {
                            string lastMaHD = dt.Rows[dt.Rows.Count - 1]["MAHD_DIEN"].ToString();
                            int lastNum = int.Parse(lastMaHD.Substring(6));

                            if (lastNum >= nextNum)
                            {
                                nextNum = lastNum + 1;
                            }
                        }
                    }

                    txtMAHD_DIEN.Text = "HDDIEN" + nextNum.ToString("D7");
                    txtMAHD_DIEN.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã hóa đơn: " + ex.Message);
            }
        }

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                DataTable dt = dgv_addHD_DIEN.DataSource as DataTable;
                if (dt == null)
                {
                    dt = CreateDataTable();
                    dgv_addHD_DIEN.DataSource = dt;
                }

                if (editingRowIndex >= 0)
                {
                    DataRow row = dt.Rows[editingRowIndex];
                    UpdateDataRow(row);
                    editingRowIndex = -1;
                }
                else
                {
                    // Trong chế độ edit, chỉ cập nhật dòng đầu tiên
                    if (isEditMode && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        UpdateDataRow(row);
                    }
                    else
                    {
                        // Thêm dòng mới
                        DataRow newRow = dt.NewRow();
                        newRow["STT"] = dt.Rows.Count + 1;
                        UpdateDataRow(newRow);
                        dt.Rows.Add(newRow);
                    }
                }

                if (!isEditMode)
                {
                    RefreshForm();
                }

                MessageBox.Show(isEditMode ? "Đã cập nhật thông tin!" : "Đã thêm vào danh sách!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("MAHD_DIEN", typeof(string));
            dt.Columns.Add("TENHD", typeof(string));
            dt.Columns.Add("MADV", typeof(string));
            dt.Columns.Add("MANHA", typeof(string));
            dt.Columns.Add("MA_PHONG", typeof(string));
            dt.Columns.Add("CHISOCU", typeof(double));
            dt.Columns.Add("CHISOMOI", typeof(double));
            dt.Columns.Add("SODIEN", typeof(double));
            dt.Columns.Add("DONGIA", typeof(decimal));
            dt.Columns.Add("TONGTIEN", typeof(decimal));
            dt.Columns.Add("THOIGIAN", typeof(DateTime));
            dt.Columns.Add("NGAYGHI", typeof(DateTime));
            dt.Columns.Add("TINHTRANGTT", typeof(string));
            return dt;
        }

        private void UpdateDataRow(DataRow row)
        {
            row["MAHD_DIEN"] = txtMAHD_DIEN.Text;
            row["TENHD"] = txtTENHD.Text;
            row["MADV"] = GetMaDichVuDien();
            row["MANHA"] = comNHA.Text;
            row["MA_PHONG"] = comPHONG.Text;
            row["CHISOCU"] = Convert.ToDouble(txtCHISOCU.Text);
            row["CHISOMOI"] = Convert.ToDouble(txtCHISOMOI.Text);
            row["SODIEN"] = Convert.ToDouble(txtTIEUTHU.Text);
            row["DONGIA"] = Convert.ToDecimal(txtDONGIA.Text.Replace(",", ""));
            row["TONGTIEN"] = Convert.ToDecimal(txtTONGTIEN.Text.Replace(",", ""));
            row["THOIGIAN"] = new DateTime(dtpTHOIGIAN.Value.Year, dtpTHOIGIAN.Value.Month, 1);
            row["NGAYGHI"] = dtpNGAYGHI.Value;
            row["TINHTRANGTT"] = comTRANGTHAI.Text;
        }

        private string GetMaDichVuDien()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP 1 MADV 
                                    FROM DICHVU 
                                    WHERE MADV LIKE 'DIEN_%'
                                    AND TUNGAY <= @THOIGIAN 
                                    AND (DENNGAY IS NULL OR DENNGAY >= @THOIGIAN)
                                    ORDER BY TUNGAY DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@THOIGIAN", dtpTHOIGIAN.Value);

                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "";
                }
            }
            catch
            {
                return "";
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtMAHD_DIEN.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(comNHA.Text) || string.IsNullOrEmpty(comPHONG.Text))
            {
                MessageBox.Show("Vui lòng chọn nhà và phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtCHISOMOI.Text))
            {
                MessageBox.Show("Vui lòng nhập chỉ số mới!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            double chiSoCu = Convert.ToDouble(txtCHISOCU.Text);
            double chiSoMoi = Convert.ToDouble(txtCHISOMOI.Text);

            if (chiSoMoi < chiSoCu)
            {
                MessageBox.Show("Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void RefreshForm()
        {
            GenerateMaHoaDon();

            if (comPHONG.Items.Count > 0)
                comPHONG.SelectedIndex = 0;

            txtCHISOCU.Clear();
            txtCHISOMOI.Clear();
            txtTIEUTHU.Clear();
            txtTONGTIEN.Clear();
            dtpNGAYGHI.Value = DateTime.Now;
            comTRANGTHAI.SelectedIndex = 1;

            LoadDonGiaDien();
            if (comPHONG.SelectedValue != null)
            {
                LoadChiSoCu(comPHONG.SelectedValue.ToString());
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dgv_addHD_DIEN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            editingRowIndex = dgv_addHD_DIEN.SelectedRows[0].Index;
            DataGridViewRow row = dgv_addHD_DIEN.SelectedRows[0];

            txtMAHD_DIEN.Text = row.Cells["MAHD_DIEN"].Value.ToString();
            comNHA.Text = row.Cells["MANHA"].Value.ToString();
            comPHONG.Text = row.Cells["MA_PHONG"].Value.ToString();
            txtCHISOCU.Text = row.Cells["CHISOCU"].Value.ToString();
            txtCHISOMOI.Text = row.Cells["CHISOMOI"].Value.ToString();
            txtTIEUTHU.Text = row.Cells["SODIEN"].Value.ToString();
            txtDONGIA.Text = Convert.ToDecimal(row.Cells["DONGIA"].Value).ToString("N0");
            txtTONGTIEN.Text = Convert.ToDecimal(row.Cells["TONGTIEN"].Value).ToString("N0");
            dtpNGAYGHI.Value = Convert.ToDateTime(row.Cells["NGAYGHI"].Value);
            comTRANGTHAI.Text = row.Cells["TINHTRANGTT"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv_addHD_DIEN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgv_addHD_DIEN.Rows.RemoveAt(dgv_addHD_DIEN.SelectedRows[0].Index);

                DataTable dt = (DataTable)dgv_addHD_DIEN.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgv_addHD_DIEN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để lưu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        DataTable dt = (DataTable)dgv_addHD_DIEN.DataSource;

                        foreach (DataRow row in dt.Rows)
                        {
                            string maHD = row["MAHD_DIEN"].ToString();

                            if (isEditMode && maHD == originalMaHD)
                            {
                                // ← UPDATE cho chế độ edit
                                string updateQuery = @"UPDATE HD_DIEN SET 
                                                      MA_PHONG = @MA_PHONG,
                                                      MADV = @MADV,
                                                      CHISOCU = @CHISOCU,
                                                      CHISOMOI = @CHISOMOI,
                                                      DONGIA = @DONGIA,
                                                      THOIGIAN = @THOIGIAN,
                                                      NGAYGHI = @NGAYGHI,
                                                      TINHTRANGTT = @TINHTRANGTT,
                                                      TONGTIEN = @TONGTIEN
                                                      WHERE MAHD_DIEN = @MAHD_DIEN";

                                SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn, transaction);
                                cmdUpdate.Parameters.AddWithValue("@MAHD_DIEN", maHD);
                                cmdUpdate.Parameters.AddWithValue("@MA_PHONG", row["MA_PHONG"]);
                                cmdUpdate.Parameters.AddWithValue("@MADV", row["MADV"]);
                                cmdUpdate.Parameters.AddWithValue("@CHISOCU", row["CHISOCU"]);
                                cmdUpdate.Parameters.AddWithValue("@CHISOMOI", row["CHISOMOI"]);
                                cmdUpdate.Parameters.AddWithValue("@DONGIA", row["DONGIA"]);
                                cmdUpdate.Parameters.AddWithValue("@THOIGIAN", row["THOIGIAN"]);
                                cmdUpdate.Parameters.AddWithValue("@NGAYGHI", row["NGAYGHI"]);
                                cmdUpdate.Parameters.AddWithValue("@TINHTRANGTT", row["TINHTRANGTT"]);
                                cmdUpdate.Parameters.AddWithValue("@TONGTIEN", row["TONGTIEN"]);

                                cmdUpdate.ExecuteNonQuery();
                            }
                            else
                            {
                                // ← INSERT cho hóa đơn mới
                                string insertQuery = @"INSERT INTO HD_DIEN 
                                                      (MAHD_DIEN, MA_PHONG, MADV, CHISOCU, CHISOMOI, DONGIA, THOIGIAN, NGAYGHI, TINHTRANGTT, TONGTIEN)
                                                      VALUES 
                                                      (@MAHD_DIEN, @MA_PHONG, @MADV, @CHISOCU, @CHISOMOI, @DONGIA, @THOIGIAN, @NGAYGHI, @TINHTRANGTT, @TONGTIEN)";

                                SqlCommand cmdInsert = new SqlCommand(insertQuery, conn, transaction);
                                cmdInsert.Parameters.AddWithValue("@MAHD_DIEN", maHD);
                                cmdInsert.Parameters.AddWithValue("@MA_PHONG", row["MA_PHONG"]);
                                cmdInsert.Parameters.AddWithValue("@MADV", row["MADV"]);
                                cmdInsert.Parameters.AddWithValue("@CHISOCU", row["CHISOCU"]);
                                cmdInsert.Parameters.AddWithValue("@CHISOMOI", row["CHISOMOI"]);
                                cmdInsert.Parameters.AddWithValue("@DONGIA", row["DONGIA"]);
                                cmdInsert.Parameters.AddWithValue("@THOIGIAN", row["THOIGIAN"]);
                                cmdInsert.Parameters.AddWithValue("@NGAYGHI", row["NGAYGHI"]);
                                cmdInsert.Parameters.AddWithValue("@TINHTRANGTT", row["TINHTRANGTT"]);
                                cmdInsert.Parameters.AddWithValue("@TONGTIEN", row["TONGTIEN"]);

                                cmdInsert.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        dgv_addHD_DIEN.DataSource = null;

                        MessageBox.Show(isEditMode ? "Cập nhật hóa đơn thành công!" : "Lưu hóa đơn thành công!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
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
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}