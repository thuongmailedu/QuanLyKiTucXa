using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                Width = 50,
                ReadOnly = true
            });

            // Các cột dữ liệu
            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MANHA",
                HeaderText = "Mã Nhà",
                Name = "MANHA",
                Width = 80
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MA_PHONG",
                HeaderText = "Mã Phòng",
                Name = "MA_PHONG",
                Width = 100
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TENHD",
                HeaderText = "Tên Hóa Đơn",
                Name = "TENHD",
                Width = 150
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MAHD_DIEN",
                HeaderText = "Mã HĐ Điện",
                Name = "MAHD_DIEN",
                Width = 100
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MAHD_NUOC",
                HeaderText = "Mã HĐ Nước",
                Name = "MAHD_NUOC",
                Width = 100
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MAHD_INT",
                HeaderText = "Mã HĐ Internet",
                Name = "MAHD_INT",
                Width = 120
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TIENDIEN",
                HeaderText = "Tiền Điện",
                Name = "TIENDIEN",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TIENNUOC",
                HeaderText = "Tiền Nước",
                Name = "TIENNUOC",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TIENINTERNET",
                HeaderText = "Tiền Internet",
                Name = "TIENINTERNET",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TONGTIEN",
                HeaderText = "Tổng Tiền",
                Name = "TONGTIEN",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.Red,
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgv_HD_TONGHOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TINHTRANGTT",
                HeaderText = "Trạng Thái",
                Name = "TINHTRANGTT",
                Width = 120
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

        private void btnupdate_Click(object sender, EventArgs e)
        {
            CapNhatTrangThai("Đã thanh toán");
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            CapNhatTrangThai("Chưa thanh toán");
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
    }
}