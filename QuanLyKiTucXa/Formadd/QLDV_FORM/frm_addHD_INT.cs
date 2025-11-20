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
    public partial class frm_addHD_INT : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private string maDichVuInternet = ""; // Lưu mã dịch vụ để dùng khi lưu

        public frm_addHD_INT()
        {
            InitializeComponent();
        }

        private void frm_addHD_INT_Load(object sender, EventArgs e)
        {
            LoadComboBoxNha();
            dtpTHOIGIAN.Format = DateTimePickerFormat.Custom;
            dtpTHOIGIAN.CustomFormat = "MM/yyyy";

            // Gắn sự kiện cho nút Lưu
            btn_Lưu.Click += btnLuu_Click;
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

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maNha = comNHA.SelectedValue.ToString();
                DateTime thoiGian = dtpTHOIGIAN.Value;
                int thang = thoiGian.Month;
                int nam = thoiGian.Year;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy đơn giá Internet hiện hành
                    decimal donGia = GetDonGiaInternet(conn, thoiGian);
                    maDichVuInternet = GetMaDichVuInternet(conn, thoiGian);

                    if (donGia == 0 || string.IsNullOrEmpty(maDichVuInternet))
                    {
                        MessageBox.Show("Không tìm thấy dịch vụ Internet phù hợp trong tháng này!\n" +
                            "Vui lòng kiểm tra bảng DICHVU có dịch vụ với MADV bắt đầu bằng 'INT_' không.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lấy danh sách phòng thuộc nhà
                    string queryPhong = @"SELECT MA_PHONG FROM PHONG 
                                         WHERE MANHA = @MANHA 
                                         ORDER BY MA_PHONG";

                    SqlCommand cmdPhong = new SqlCommand(queryPhong, conn);
                    cmdPhong.Parameters.AddWithValue("@MANHA", maNha);

                    SqlDataReader reader = cmdPhong.ExecuteReader();
                    DataTable dtPhong = new DataTable();
                    dtPhong.Load(reader);

                    int stt = GetNextHoaDonNumber(conn);

                    // Tạo DataTable mới để hiển thị
                    DataTable dtDisplay = new DataTable();
                    dtDisplay.Columns.Add("STT", typeof(int));
                    dtDisplay.Columns.Add("MAHD_INT", typeof(string));
                    dtDisplay.Columns.Add("TENHD", typeof(string));
                    dtDisplay.Columns.Add("MANHA", typeof(string));
                    dtDisplay.Columns.Add("MA_PHONG", typeof(string));
                    dtDisplay.Columns.Add("DONGIA", typeof(decimal));
                    dtDisplay.Columns.Add("SO_SV", typeof(int));
                    dtDisplay.Columns.Add("TONGTIEN", typeof(decimal));
                    dtDisplay.Columns.Add("THOIGIAN", typeof(string)); // Hiển thị dạng MM/yyyy
                    dtDisplay.Columns.Add("TINHTRANGTT", typeof(string));
                    dtDisplay.Columns.Add("THOIGIAN_SAVE", typeof(DateTime)); // Cột ẩn để lưu
                    dtDisplay.Columns.Add("MADV", typeof(string)); // Cột ẩn để lưu mã dịch vụ

                    int rowNum = 1;
                    foreach (DataRow row in dtPhong.Rows)
                    {
                        string maPhong = row["MA_PHONG"].ToString();

                        // Đếm số sinh viên trong phòng theo tháng
                        int soSV = DemSinhVienTheoPhongVaThang(conn, maPhong, thang, nam);

                        // Chỉ tạo hóa đơn nếu có sinh viên
                        if (soSV > 0)
                        {
                            string maHD = "HDINT" + stt.ToString("D7");
                            decimal tongTien = soSV * donGia;
                            DateTime thoiGianSave = new DateTime(nam, thang, 1);

                            DataRow newRow = dtDisplay.NewRow();
                            newRow["STT"] = rowNum;
                            newRow["MAHD_INT"] = maHD;
                            newRow["TENHD"] = "Hóa đơn Internet";
                            newRow["MANHA"] = maNha;
                            newRow["MA_PHONG"] = maPhong;
                            newRow["DONGIA"] = donGia;
                            newRow["SO_SV"] = soSV;
                            newRow["TONGTIEN"] = tongTien;
                            newRow["THOIGIAN"] = thang.ToString("D2") + "/" + nam.ToString(); // Hiển thị MM/yyyy
                            newRow["TINHTRANGTT"] = "Chưa thanh toán";
                            newRow["THOIGIAN_SAVE"] = thoiGianSave; // Lưu DateTime đầy đủ
                            newRow["MADV"] = maDichVuInternet; // Lưu mã dịch vụ

                            dtDisplay.Rows.Add(newRow);
                            stt++;
                            rowNum++;
                        }
                    }

                    if (dtDisplay.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có phòng nào có sinh viên trong tháng này!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dgv_add_HD_INT.DataSource = dtDisplay;

                        // Ẩn các cột không cần hiển thị
                        if (dgv_add_HD_INT.Columns["THOIGIAN_SAVE"] != null)
                            dgv_add_HD_INT.Columns["THOIGIAN_SAVE"].Visible = false;

                        if (dgv_add_HD_INT.Columns["MADV"] != null)
                            dgv_add_HD_INT.Columns["MADV"].Visible = false;

                        // Format tiền tệ
                        if (dgv_add_HD_INT.Columns["DONGIA"] != null)
                            dgv_add_HD_INT.Columns["DONGIA"].DefaultCellStyle.Format = "N0";

                        if (dgv_add_HD_INT.Columns["TONGTIEN"] != null)
                            dgv_add_HD_INT.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";

                        MessageBox.Show($"Đã tạo {dtDisplay.Rows.Count} hóa đơn!\nMã dịch vụ: {maDichVuInternet}",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn: " + ex.Message + "\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetDonGiaInternet(SqlConnection conn, DateTime thoiGian)
        {
            try
            {
                // Lấy dịch vụ Internet hiện hành
                string query = @"SELECT TOP 1 DONGIA 
                                FROM DICHVU 
                                WHERE MADV LIKE 'INT_%'
                                AND TUNGAY <= @THOIGIAN 
                                AND (DENNGAY IS NULL OR DENNGAY >= @THOIGIAN)
                                ORDER BY TUNGAY DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@THOIGIAN", thoiGian);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy đơn giá: " + ex.Message);
                return 0;
            }
        }

        private string GetMaDichVuInternet(SqlConnection conn, DateTime thoiGian)
        {
            try
            {
                // Lấy mã dịch vụ Internet hiện hành
                string query = @"SELECT TOP 1 MADV 
                                FROM DICHVU 
                                WHERE MADV LIKE 'INT_%'
                                AND TUNGAY <= @THOIGIAN 
                                AND (DENNGAY IS NULL OR DENNGAY >= @THOIGIAN)
                                ORDER BY TUNGAY DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@THOIGIAN", thoiGian);

                object result = cmd.ExecuteScalar();

                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    return result.ToString();
                }
                else
                {
                    // Nếu không tìm thấy, thử query không có điều kiện thời gian
                    query = @"SELECT TOP 1 MADV 
                            FROM DICHVU 
                            WHERE MADV LIKE 'INT_%'
                            ORDER BY TUNGAY DESC";

                    cmd = new SqlCommand(query, conn);
                    result = cmd.ExecuteScalar();

                    return result != null ? result.ToString() : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy mã dịch vụ: " + ex.Message);
                return "";
            }
        }

        private int DemSinhVienTheoPhongVaThang(SqlConnection conn, string maPhong, int thang, int nam)
        {
            try
            {
                // Tính ngày đầu và cuối tháng
                DateTime ngayDau = new DateTime(nam, thang, 1);
                DateTime ngayCuoi = ngayDau.AddMonths(1).AddDays(-1);

                string query = @"
                    SELECT COUNT(DISTINCT HD.MASV) AS SoSV
                    FROM HOPDONG HD
                    WHERE HD.MA_PHONG = @MA_PHONG
                    AND (
                        (HD.TUNGAY <= @NGAY_CUOI AND HD.DENNGAY >= @NGAY_DAU)
                        AND (HD.NGAYKTTT IS NULL OR HD.NGAYKTTT >= @NGAY_DAU)
                    )";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                cmd.Parameters.AddWithValue("@NGAY_DAU", ngayDau);
                cmd.Parameters.AddWithValue("@NGAY_CUOI", ngayCuoi);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch
            {
                return 0;
            }
        }

        private int GetNextHoaDonNumber(SqlConnection conn)
        {
            try
            {
                string query = @"SELECT ISNULL(MAX(CAST(SUBSTRING(MAHD_INT, 6, 7) AS INT)), 0) + 1 
                                FROM HD_INTERNET 
                                WHERE MAHD_INT LIKE 'HDINT%'";

                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 1;
            }
            catch
            {
                return 1;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgv_add_HD_INT.DataSource == null || dgv_add_HD_INT.Rows.Count == 0)
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
                        string query = @"INSERT INTO HD_INTERNET 
                                        (MAHD_INT,TENHD, MADV, MA_PHONG, DONGIA, THOIGIAN, TINHTRANGTT, TONGTIEN)
                                        VALUES 
                                        (@MAHD_INT,N'Hóa đơn Internet', @MADV, @MA_PHONG, @DONGIA, @THOIGIAN, @TINHTRANGTT, @TONGTIEN)";

                        DataTable dt = (DataTable)dgv_add_HD_INT.DataSource;
                        int count = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            SqlCommand cmd = new SqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@MAHD_INT", row["MAHD_INT"]);
                            cmd.Parameters.AddWithValue("@MADV", row["MADV"]); // Lấy từ cột ẩn
                            cmd.Parameters.AddWithValue("@MA_PHONG", row["MA_PHONG"]);
                            cmd.Parameters.AddWithValue("@DONGIA", row["DONGIA"]);
                            cmd.Parameters.AddWithValue("@THOIGIAN", row["THOIGIAN_SAVE"]); // Lưu DateTime đầy đủ
                            cmd.Parameters.AddWithValue("@TINHTRANGTT", row["TINHTRANGTT"]);
                            cmd.Parameters.AddWithValue("@TONGTIEN", row["TONGTIEN"]);

                            cmd.ExecuteNonQuery();
                            count++;
                        }

                        transaction.Commit();
                        MessageBox.Show($"Lưu {count} hóa đơn thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
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
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message + "\n\nChi tiết:\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}