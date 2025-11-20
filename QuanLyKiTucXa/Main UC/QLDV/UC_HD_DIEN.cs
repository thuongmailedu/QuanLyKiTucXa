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

            // Gắn sự kiện
            btnedit.Click += btnedit_Click;
            btnupdate.Click += btnupdate_Click;
            btncancel.Click += btncancel_Click;
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

        
    }
}