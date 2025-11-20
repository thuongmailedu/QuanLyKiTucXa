using QuanLyKiTucXa.Formadd.QLDV_FORM;
using QuanLyKiTucXa.Formadd.QLHD_FORM;
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
    public partial class UC_HD_INT : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_HD_INT()
        {
            InitializeComponent();
            LoadData();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            frm_addHD_INT form = new frm_addHD_INT();
            form.ShowDialog();

            LoadDataGridView();
            FilterData();
        }

        private void UC_HD_INT_Load(object sender, EventArgs e)
        {
            LoadComboBoxNha();
            LoadDataGridView();

            // Đăng ký sự kiện thay đổi
            comNHA.SelectedIndexChanged += Filter_Changed;
            comPHONG.SelectedIndexChanged += Filter_Changed;
            dtpTHOIGIAN.ValueChanged += Filter_Changed;
        }

        private void LoadData()
        {
            LoadComboBoxNha();
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

                    // Thêm dòng "Tất cả"
                    DataRow row = dt.NewRow();
                    row["MANHA"] = "Tất cả";
                    dt.Rows.InsertAt(row, 0);

                    comNHA.DataSource = dt;
                    comNHA.DisplayMember = "MANHA";
                    comNHA.ValueMember = "MANHA";

                    comNHA.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message);
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

                    // Thêm dòng "Tất cả"
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
                            ROW_NUMBER() OVER (ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC) AS STT,
                            HD.MAHD_INT,
                            N'Hóa đơn Internet' AS TENHD,
                            P.MANHA,
                            HD.MA_PHONG,
                            HD.DONGIA,
                            (
                                SELECT COUNT(DISTINCT HDP.MASV)
                                FROM HOPDONG HDP
                                WHERE HDP.MA_PHONG = HD.MA_PHONG
                                AND MONTH(HDP.TUNGAY) <= MONTH(HD.THOIGIAN) 
                                AND YEAR(HDP.TUNGAY) <= YEAR(HD.THOIGIAN)
                                AND (MONTH(HDP.DENNGAY) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.DENNGAY) >= YEAR(HD.THOIGIAN))
                                AND (HDP.NGAYKTTT IS NULL OR (MONTH(HDP.NGAYKTTT) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.NGAYKTTT) >= YEAR(HD.THOIGIAN)))
                            ) AS SO_SV,
                            HD.TONGTIEN,
                            FORMAT(HD.THOIGIAN, 'MM/yyyy') AS THOIGIAN,
                            HD.TINHTRANGTT
                        FROM HD_INTERNET HD
                        INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
                        ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgv_HD_INT.DataSource = dt;

                    // Format tiền tệ
                    if (dgv_HD_INT.Columns["DONGIA"] != null)
                        dgv_HD_INT.Columns["DONGIA"].DefaultCellStyle.Format = "N0";

                    if (dgv_HD_INT.Columns["TONGTIEN"] != null)
                        dgv_HD_INT.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            // Load phòng khi thay đổi nhà
            if (sender == comNHA && comNHA.SelectedValue != null &&
                comNHA.SelectedValue.ToString() != "Tất cả")
            {
                LoadComboBoxPhong(comNHA.SelectedValue.ToString());
            }

            // Tự động lọc dữ liệu
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
                            ROW_NUMBER() OVER (ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC) AS STT,
                            HD.MAHD_INT,
                            N'Hóa đơn Internet' AS TENHD,
                            P.MANHA,
                            HD.MA_PHONG,
                            HD.DONGIA,
                            (
                                SELECT COUNT(DISTINCT HDP.MASV)
                                FROM HOPDONG HDP
                                WHERE HDP.MA_PHONG = HD.MA_PHONG
                                AND MONTH(HDP.TUNGAY) <= MONTH(HD.THOIGIAN) 
                                AND YEAR(HDP.TUNGAY) <= YEAR(HD.THOIGIAN)
                                AND (MONTH(HDP.DENNGAY) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.DENNGAY) >= YEAR(HD.THOIGIAN))
                                AND (HDP.NGAYKTTT IS NULL OR (MONTH(HDP.NGAYKTTT) >= MONTH(HD.THOIGIAN) AND YEAR(HDP.NGAYKTTT) >= YEAR(HD.THOIGIAN)))
                            ) AS SO_SV,
                            HD.TONGTIEN,
                            FORMAT(HD.THOIGIAN, 'MM/yyyy') AS THOIGIAN,
                            HD.TINHTRANGTT
                        FROM HD_INTERNET HD
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

                    // Lọc theo tháng/năm
                    query += " AND MONTH(HD.THOIGIAN) = @THANG AND YEAR(HD.THOIGIAN) = @NAM";
                    cmd.Parameters.AddWithValue("@THANG", dtpTHOIGIAN.Value.Month);
                    cmd.Parameters.AddWithValue("@NAM", dtpTHOIGIAN.Value.Year);

                    query += " ORDER BY HD.THOIGIAN DESC, HD.MAHD_INT DESC";
                    cmd.CommandText = query;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgv_HD_INT.DataSource = dt;

                    // Format tiền tệ
                    if (dgv_HD_INT.Columns["DONGIA"] != null)
                        dgv_HD_INT.Columns["DONGIA"].DefaultCellStyle.Format = "N0";

                    if (dgv_HD_INT.Columns["TONGTIEN"] != null)
                        dgv_HD_INT.Columns["TONGTIEN"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }
    }
}