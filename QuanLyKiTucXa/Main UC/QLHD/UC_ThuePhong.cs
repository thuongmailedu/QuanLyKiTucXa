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

namespace QuanLyKiTucXa.Main_UC.QLHD
{
    public partial class UC_ThuePhong : UserControl
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql , constr;
        DataTable comdt = new DataTable();
        public UC_ThuePhong()
        {
            InitializeComponent();
        }
        
        private void UC_ThuePhong_Load(object sender, EventArgs e)
        {
            //Khong sua, header no bi loi stytle
            dgvThuePhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvThuePhong.Font, FontStyle.Bold);

            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

            conn.ConnectionString = constr;
            conn.Open();

            //sql = "Select MAHD, TENHD, sv.MASV,TENSV, NGAYKY,nv.MANV, nv. TENNV, NGAYKTTT, sv. GIOITINH, " +
            //    "hd. MA_PHONG, n. MANHA, LOAIPHONG, hd. DONGIA, TONGTIEN, TUNGAY, DENNGAY, THOIHAN " +
            //    " from SINHVIEN sv " +
            //    " LEFT JOIN HOPDONG hd ON sv.MASV = hd. MASV" +
            //    " JOIN PHONG p on hd.MA_PHONG = p.MA_PHONG" +
            //    " JOIN NHA n on p.MANHA = n.MANHA " +
            //    " JOIN NHANVIEN nv on hd. MANV = nv. MANV" +
            //    " ORDER BY MAHD";
            //da = new SqlDataAdapter(sql, conn);
            //dt.Clear();
            //da.Fill(dt);
            //dgvThuePhong.DataSource = dt;
            //dgvThuePhong.Refresh();

            dgvThuePhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuePhong.MultiSelect = false; // Chỉ cho chọn 1 dòng
            LoadData();
          

        }

        private void btn_AddHopDong_Click(object sender, EventArgs e)
        {

            frm_addHopDong frm = new frm_addHopDong();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Refresh lại danh sách sau khi thêm thành công
                LoadData();
            }

        }

        private void dgvThuePhong_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Vẽ số thứ tự ở cột đầu tiên
            dgvThuePhong.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void btnedit_HOPDONG_Click(object sender, EventArgs e)
        {

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
                SV. NGAYSINH,
                SV.GIOITINH,
                SV.CCCD,
                SV.SDT AS SDT_SV,
                SV. HKTT,
                HD.MA_PHONG,
                HD. TUNGAY,
                HD.DENNGAY,
                HD. THOIHAN,
                HD. NGAYKTTT,
                HD.DONGIA,
                HD.TONGTIEN,
                HD.MANV,
                NV.TENNV,
                NV.SDT AS SDT_NV,
                HD.NGAYKY,
                P.MANHA,
                N.LOAIPHONG,
                L.TENLOP,
                K.TENKHOA,
                TN.TEN_THANNHAN,
                TN.SDT AS SDT_THANNHAN,
                TN.MOIQUANHE,
                TN.DIACHI AS DIACHI_THANNHAN
            FROM HOPDONG HD
            INNER JOIN SINHVIEN SV ON HD. MASV = SV. MASV
            INNER JOIN PHONG P ON HD.MA_PHONG = P.MA_PHONG
            INNER JOIN NHA N ON P.MANHA = N. MANHA
            LEFT JOIN NHANVIEN NV ON HD. MANV = NV. MANV
            LEFT JOIN LOP L ON SV.MALOP = L.MALOP
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
                        // Thông tin hợp đồng
                     //   new Microsoft.Reporting.WinForms.ReportParameter("prMAHD", reader["MAHD"].ToString()),
                     //   new Microsoft.Reporting. WinForms.ReportParameter("prTENHD", reader["TENHD"]?.ToString() ?? ""),
                        
                        // Thông tin sinh viên
                        new Microsoft. Reporting.WinForms. ReportParameter("prMASV", reader["MASV"].ToString()),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTENSV", reader["TENSV"]. ToString()),
                        new Microsoft. Reporting.WinForms. ReportParameter("prNGAYSINH", reader["NGAYSINH"] != DBNull.Value ? Convert.ToDateTime(reader["NGAYSINH"]).ToString("dd/MM/yyyy") : ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prGIOITINH", reader["GIOITINH"]?.ToString() ?? ""),
                     //   new Microsoft.Reporting.WinForms.ReportParameter("prCCCD", reader["CCCD"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prSDT_SINHVIEN", reader["SDT_SV"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prHKTT_SINHVIEN", reader["HKTT"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTENLOP", reader["TENLOP"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTENKHOA", reader["TENKHOA"]?.ToString() ?? ""),
                        
                        // Thông tin phòng
                        new Microsoft. Reporting.WinForms.ReportParameter("prMA_PHONG", reader["MA_PHONG"]. ToString()),
                        new Microsoft. Reporting.WinForms. ReportParameter("prMANHA", reader["MANHA"]. ToString()),
                      //  new Microsoft. Reporting.WinForms. ReportParameter("prLOAIPHONG", reader["LOAIPHONG"].ToString()),
                        
                        // Thông tin thời gian và tiền
                        new Microsoft. Reporting.WinForms.ReportParameter("prTUNGAY", reader["TUNGAY"] != DBNull.Value ? Convert. ToDateTime(reader["TUNGAY"]).ToString("dd/MM/yyyy") : ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prDENNGAY", reader["DENNGAY"] != DBNull.Value ? Convert.ToDateTime(reader["DENNGAY"]).ToString("dd/MM/yyyy") : ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTHOIHAN", reader["THOIHAN"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prDONGIA", reader["DONGIA"] != DBNull.Value ? Convert. ToDecimal(reader["DONGIA"]). ToString("N0") : "0"),
                      //  new Microsoft.Reporting.WinForms.ReportParameter("prTONGTIEN", reader["TONGTIEN"] != DBNull.Value ?  Convert.ToDecimal(reader["TONGTIEN"]).ToString("N0") : "0"),
                        
                        // Ngày ký (tách thành ngày, tháng, năm)
                        new Microsoft.Reporting.WinForms.ReportParameter("prNgay_NGAYKY", ngayKy. Day.ToString()),
                        new Microsoft.Reporting.WinForms.ReportParameter("prThang_NGAYKY", ngayKy.Month.ToString()),
                        new Microsoft.Reporting.WinForms.ReportParameter("prNam_NGAYKY", ngayKy.Year.ToString()),
                        
                        // Thông tin nhân viên
                      //  new Microsoft.Reporting.WinForms.ReportParameter("prMANV", reader["MANV"]?.ToString() ?? ""),
                        new Microsoft.Reporting.WinForms.ReportParameter("prTENNV", reader["TENNV"]?.ToString() ??  ""),
                        new Microsoft. Reporting.WinForms. ReportParameter("prSDT_NHANVIEN", reader["SDT_NV"]?.ToString() ?? ""),
                        
                        // Thông tin thân nhân
                        //new Microsoft. Reporting.WinForms. ReportParameter("prTEN_THANNHAN", reader["TEN_THANNHAN"]?.ToString() ?? ""),
                        //new Microsoft.Reporting.WinForms.ReportParameter("prSDT_THANNHAN", reader["SDT_THANNHAN"]?.ToString() ?? ""),
                        //new Microsoft. Reporting.WinForms.ReportParameter("prMOIQUANHE", reader["MOIQUANHE"]?.ToString() ?? ""),
                        //new Microsoft.Reporting.WinForms.ReportParameter("prDIACHI_THANNHAN", reader["DIACHI_THANNHAN"]?.ToString() ?? "")
                            };

                            // Truyền parameters vào form (cần thêm property/method trong frm_IN_HOPDONG)
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

        private void LoadData()
        {
            try
            {
                sql = 
                    "Select MAHD ,TENHD,sv.MASV,TENSV, sv.GIOITINH , n.MANHA, hd.MA_PHONG , n.LOAIPHONG, hd.TUNGAY , hd.DENNGAY , hd.NGAYKTTT, hd.THOIHAN , hd. DONGIA, hd. TONGTIEN, hd.MANV, nv.TENNV, hd.NGAYKY"+
                    " from SINHVIEN sv " +
                    " LEFT JOIN HOPDONG hd ON sv.MASV = hd.MASV" +
                    " JOIN PHONG p on hd.MA_PHONG = p.MA_PHONG" +
                    " JOIN NHA n on p.MANHA = n.MANHA " +
                    " LEFT JOIN NHANVIEN nv on hd.MANV = nv.MANV" +
                    " ORDER BY MAHD";

                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);
                dgvThuePhong.DataSource = dt;
                dgvThuePhong.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
