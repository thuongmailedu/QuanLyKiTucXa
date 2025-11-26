using QuanLyKiTucXa.Formadd.QLDV_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLDV
{
    public partial class UC_DANHMUC_DV : UserControl
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string sql, constr;

        public UC_DANHMUC_DV()
        {
            InitializeComponent();
        }

        private void UC_DANHMUC_DV_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo connection
                constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
                conn.ConnectionString = constr;

                // Thiết lập font cho header
                dgv_DICHVU.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgv_DICHVU.Font, System.Drawing.FontStyle.Bold);

                // Load ComboBox lọc theo tên dịch vụ
                LoadComboBoxFilter();

                // Load dữ liệu lần đầu
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load UC: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load ComboBox để lọc
        private void LoadComboBoxFilter()
        {
            comGIATRI.Items.Clear();
            comGIATRI.Items.Add("Tất cả");
            comGIATRI.Items.Add("Điện");
            comGIATRI.Items.Add("Nước");
            comGIATRI.Items.Add("Internet");
            comGIATRI.SelectedIndex = 0; // Mặc định "Tất cả"
        }

        // Load dữ liệu từ database - THÊM TEN_NHACC
        private void LoadData(string tenDichVu = null, string keyword = null)
        {
            try
            {
                // SQL bổ sung JOIN với NHACC để lấy TEN_NHACC
                sql = @"SELECT 
                            DV.MADV, 
                            DV.TENDV, 
                            DV. DONGIA, 
                            DV.DONVI, 
                            DV.TUNGAY, 
                            DV.DENNGAY, 
                            DV.TRANGTHAI,
                            NC.TEN_NHACC
                        FROM DICHVU DV
                        LEFT JOIN NHACC NC ON DV.MA_NHACC = NC. MA_NHACC
                        WHERE 1=1";

                // Thêm điều kiện lọc theo tên dịch vụ
                if (!string.IsNullOrEmpty(tenDichVu) && tenDichVu != "Tất cả")
                {
                    sql += " AND DV.TENDV = @TENDV";
                }

                // Thêm điều kiện tìm kiếm
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += " AND (DV.MADV LIKE @Keyword OR DV. TENDV LIKE @Keyword OR DV.TRANGTHAI LIKE @Keyword OR NC.TEN_NHACC LIKE @Keyword)";
                }

                sql += " ORDER BY DV.TUNGAY DESC, DV.MADV";

                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // Thêm parameters
                        if (!string.IsNullOrEmpty(tenDichVu) && tenDichVu != "Tất cả")
                        {
                            cmd.Parameters.AddWithValue("@TENDV", tenDichVu);
                        }

                        if (!string.IsNullOrEmpty(keyword))
                        {
                            cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                        }

                        da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);

                        // Thêm cột STT
                        dt.Columns.Add("STT", typeof(int));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["STT"] = i + 1;
                        }

                        // Đổ dữ liệu vào DataGridView
                        dgv_DICHVU.DataSource = dt;
                        dgv_DICHVU.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Khi chọn ComboBox - TỰ ĐỘNG LỌC
        private void comGIATRI_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDV = comGIATRI.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();
            LoadData(tenDV, keyword);
        }

        // Nút THÊM dịch vụ
        private void btnadd_Click(object sender, EventArgs e)
        {
            using (frm_addDICHVU form = new frm_addDICHVU())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Refresh lại danh sách sau khi thêm
                    LoadData();
                }
            }
        }

        // Nút FILTER - Tìm kiếm
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string tenDV = comGIATRI.SelectedItem?.ToString();
            string keyword = txtSearch.Text.Trim();
            LoadData(tenDV, keyword);
        }

        // Nút LÀM MỚI - Reset bộ lọc
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            comGIATRI.SelectedIndex = 0;
            txtSearch.Clear();
            LoadData();
        }
    }
}