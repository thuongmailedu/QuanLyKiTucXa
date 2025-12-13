using QuanLyKiTucXa.Formadd.QLHD_FORM;
using System;
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
            conn.Open();

            dgvThuePhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuePhong.MultiSelect = false;

            // ✅ Load dữ liệu ban đầu
            LoadData();

            // ✅ Load các tùy chọn cho ComboBox lọc
            LoadFilterOptions();
        }

        // ✅ THÊM: Load các tùy chọn cho ComboBox lọc
        private void LoadFilterOptions()
        {
            try
            {
                // Xóa items cũ
                comfillter.Items.Clear();

                // Thêm các tùy chọn lọc
                comfillter.Items.Add("Mã SV");
                comfillter.Items.Add("Mã hợp đồng");
                comfillter.Items.Add("Ngày bắt đầu");
                comfillter.Items.Add("Ngày kết thúc");
                comfillter.Items.Add("Ngày thanh lý");
                comfillter.Items.Add("Mã nhà");
                comfillter.Items.Add("Mã nhân viên");

                // Chọn mặc định
                comfillter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load filter options: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddHopDong_Click(object sender, EventArgs e)
        {
            // ✅ Sử dụng using để tránh lỗi form hiện 2 lần
            using (frm_addHopDong frm = new frm_addHopDong())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh lại danh sách sau khi thêm thành công
                    LoadData();
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
            // Code cho chức năng sửa hợp đồng (nếu cần)
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
                HD.TENHD,
                HD.MASV,
                SV.TENSV,
                SV.NGAYSINH,
                SV.GIOITINH,
                SV.CCCD,
                SV.SDT AS SDT_SV,
                SV.HKTT,
                HD.MA_PHONG,
                HD. TUNGAY,
                HD. DENNGAY,
                HD.THOIHAN,
                HD.NGAYKTTT,
                HD.DONGIA,
                HD.TONGTIEN,
                HD. MANV,
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
            INNER JOIN SINHVIEN SV ON HD.MASV = SV.MASV
            INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
            INNER JOIN NHA N ON P.MANHA = N.MANHA
            LEFT JOIN NHANVIEN NV ON HD.MANV = NV.MANV
            LEFT JOIN LOP L ON SV.MALOP = L. MALOP
            LEFT JOIN KHOA K ON L.MAKHOA = K.MAKHOA
            LEFT JOIN THANNHAN TN ON SV.MASV = TN.MASV
            WHERE HD.MAHD = @MAHD";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
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
                        new Microsoft. Reporting.WinForms. ReportParameter("prMASV", reader["MASV"].ToString()),
                        new Microsoft. Reporting.WinForms.ReportParameter("prTENSV", reader["TENSV"].ToString()),
                        new Microsoft. Reporting.WinForms.ReportParameter("prNGAYSINH", reader["NGAYSINH"] != DBNull.Value ? Convert. ToDateTime(reader["NGAYSINH"]).ToString("dd/MM/yyyy") : ""),
                        new Microsoft.Reporting. WinForms.ReportParameter("prGIOITINH", reader["GIOITINH"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prSDT_SINHVIEN", reader["SDT_SV"]?.ToString() ??  ""),
                        new Microsoft. Reporting.WinForms. ReportParameter("prHKTT_SINHVIEN", reader["HKTT"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTENLOP", reader["TENLOP"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTENKHOA", reader["TENKHOA"]?.ToString() ?? ""),
                        
                        // Thông tin phòng
                        new Microsoft.Reporting. WinForms.ReportParameter("prMA_PHONG", reader["MA_PHONG"].ToString()),
                        new Microsoft.Reporting. WinForms.ReportParameter("prMANHA", reader["MANHA"].ToString()),
                        
                        // Thông tin thời gian và tiền
                        new Microsoft.Reporting.WinForms.ReportParameter("prTUNGAY", reader["TUNGAY"] != DBNull.Value ? Convert. ToDateTime(reader["TUNGAY"]).ToString("dd/MM/yyyy") : ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prDENNGAY", reader["DENNGAY"] != DBNull.Value ? Convert.ToDateTime(reader["DENNGAY"]).ToString("dd/MM/yyyy") : ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTHOIHAN", reader["THOIHAN"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prDONGIA", reader["DONGIA"] != DBNull.Value ?  Convert.ToDecimal(reader["DONGIA"]). ToString("N0") : "0"),
                        
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
        }

        // ✅ CẬP NHẬT: Thêm tham số searchType và keyword
        private void LoadData(string searchType = "", string keyword = "")
        {
            try
            {
                sql = @"SELECT 
                        MAHD, 
                        TENHD, 
                        sv.MASV, 
                        TENSV, 
                        sv.GIOITINH, 
                        n.MANHA, 
                        hd.MA_PHONG, 
                        n.LOAIPHONG, 
                        hd. TUNGAY, 
                        hd.DENNGAY, 
                        hd.NGAYKTTT, 
                        hd.THOIHAN, 
                        hd. DONGIA, 
                        hd.TONGTIEN, 
                        hd.MANV, 
                        nv. TENNV, 
                        hd.NGAYKY
                    FROM SINHVIEN sv 
                    LEFT JOIN HOPDONG hd ON sv.MASV = hd.MASV
                    JOIN PHONG p ON hd.MA_PHONG = p.MA_PHONG
                    JOIN NHA n ON p.MANHA = n. MANHA 
                    LEFT JOIN NHANVIEN nv ON hd. MANV = nv. MANV";

                // ✅ THÊM: Điều kiện lọc
                bool hasWhere = false;

                if (!string.IsNullOrWhiteSpace(keyword) && !string.IsNullOrEmpty(searchType))
                {
                    sql += " WHERE ";
                    hasWhere = true;

                    switch (searchType)
                    {
                        case "Mã SV":
                            sql += "sv.MASV LIKE @Keyword";
                            break;
                        case "Mã hợp đồng":
                            sql += "hd. MAHD LIKE @Keyword";
                            break;
                        case "Ngày bắt đầu":
                            sql += "CONVERT(VARCHAR, hd.TUNGAY, 103) LIKE @Keyword";
                            break;
                        case "Ngày kết thúc":
                            sql += "CONVERT(VARCHAR, hd.DENNGAY, 103) LIKE @Keyword";
                            break;
                        case "Ngày thanh lý":
                            sql += "CONVERT(VARCHAR, hd. NGAYKTTT, 103) LIKE @Keyword";
                            break;
                        case "Mã nhà":
                            sql += "n.MANHA LIKE @Keyword";
                            break;
                        case "Mã nhân viên":
                            sql += "hd.MANV LIKE @Keyword";
                            break;
                    }
                }

                // ✅ Sắp xếp theo MAHD giảm dần (hợp đồng mới nhất lên đầu)
                sql += " ORDER BY hd.MAHD DESC";

                // Sử dụng SqlCommand thay vì SqlDataAdapter trực tiếp
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Thêm tham số nếu có keyword
                    if (!string.IsNullOrWhiteSpace(keyword) && hasWhere)
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    }

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dgvThuePhong.DataSource = dt;
                    dgvThuePhong.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_HOPDONG_Click(object sender, EventArgs e)
        {

        }



        // ✅ THÊM: Event handler cho nút lọc
        private void btnfillter_HOPDONG_Click(object sender, EventArgs e)
        {
            string searchType = comfillter.Text;
            string keyword = txtSearch.Text.Trim();

            // Load dữ liệu với điều kiện lọc
            LoadData(searchType, keyword);
        }
    }
}