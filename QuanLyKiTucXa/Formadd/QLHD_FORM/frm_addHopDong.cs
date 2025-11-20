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

namespace QuanLyKiTucXa.Formadd.QLHD_FORM
{
    public partial class frm_addHopDong : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        DataTable comdt = new DataTable();
        private HopDongValidator validator;
        public frm_addHopDong()
        {
            InitializeComponent();
            string connString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
            validator = new HopDongValidator(connString);
        }

        private void frm_addHopDong_Load(object sender, EventArgs e)
        {
            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

            conn.ConnectionString = constr;
            conn.Open();
        }

        // Phương thức để nhận dữ liệu từ form chính
        public void SetThongTinPhong(string maPhong, string maNha, string loaiPhong,
            decimal giaPhong, DateTime tuNgay, DateTime denNgay, int thoiHan, decimal tongTien)
        {
            txtMA_PHONG.Text = maPhong;
            txtMANHA.Text = maNha;
            txtLOAIPHONG.Text = loaiPhong;
            txtGIAPHONG.Text = giaPhong.ToString("N0"); // Format: 1,000,000

            dateTUNGAY.Value = tuNgay;
            dateDENNGAY.Value = denNgay;
            txtTHOIHAN.Text = thoiHan.ToString();
            txtTONGTIEN.Text = tongTien.ToString("N0"); // Format: 1,000,000
            dateNGAYKY.Value = DateTime.Now;

            // Disable các textbox không cho sửa
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtLOAIPHONG.ReadOnly = true;
            txtGIAPHONG.ReadOnly = true;
            txtTHOIHAN.ReadOnly = true;
            txtTONGTIEN.ReadOnly = true;
            dateTUNGAY.Enabled = false;
            dateDENNGAY.Enabled = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtMASV.Text.Trim();
                string maPhong = txtMA_PHONG.Text;
                DateTime tuNgay = dateTUNGAY.Value;
                DateTime denNgay = dateDENNGAY.Value;

                // Validate cơ bản
                if (string.IsNullOrEmpty(maSV))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (tuNgay >= denNgay)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // KIỂM TRA ĐIỀU KIỆN QUAN TRỌNG
                if (!validator.KiemTraDieuKienHopDong(maSV, maPhong, tuNgay, denNgay))
                {
                    return; // Không đạt điều kiện, đã hiển thị thông báo trong validator
                }

                // Nếu đạt điều kiện, tiến hành lưu hợp đồng
                LuuHopDong(maSV, maPhong, tuNgay, denNgay);

                MessageBox.Show("Tạo hợp đồng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LuuHopDong(string maSV, string maPhong, DateTime tuNgay, DateTime denNgay)
        {
            // Code lưu hợp đồng vào database của bạn
            // INSERT INTO HOPDONG ...
            try
            {
                string maHD = TaoMaHopDong();
              //  int thoiHan = ((denNgay.Year - tuNgay.Year) * 12) + denNgay.Month - tuNgay.Month;
               // decimal donGia = LayDonGiaPhong(maPhong);
               // decimal tongTien = donGia * thoiHan;
               // string maNV = "NV001"; // 👈 Thay bằng session user
                string tenHD = $"HĐ thuê phòng {maPhong}";

                string sql = @"INSERT INTO HOPDONG 
                               (MAHD, MASV, MA_PHONG, TUNGAY, DENNGAY )
                              --   MANV, NGAYKY, TONGTIEN, NGAYKTTT)
                               VALUES 
                               (@MAHD, @MASV, @MA_PHONG, @TUNGAY, @DENNGAY ) ;
                                -- @NGAYKY, @TONGTIEN, NULL)";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAHD", maHD);
                        cmd.Parameters.AddWithValue("@TENHD", tenHD);
                        cmd.Parameters.AddWithValue("@MASV", maSV);
                        cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                        cmd.Parameters.AddWithValue("@TUNGAY", tuNgay);
                        cmd.Parameters.AddWithValue("@DENNGAY", denNgay);
                      //  cmd.Parameters.AddWithValue("@THOIHAN", thoiHan);
                       // cmd.Parameters.AddWithValue("@DONGIA", donGia);
                      //  cmd.Parameters.AddWithValue("@MANV", maNV);
                        cmd.Parameters.AddWithValue("@NGAYKY", DateTime.Now);
                      //  cmd.Parameters.AddWithValue("@TONGTIEN", tongTien);

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show(
                                $"Tạo hợp đồng thành công!\n\n" +
                                $"Mã HĐ: {maHD}\n" +
                                $"Sinh viên: {maSV}\n" +
                                $"Phòng: {maPhong}\n" +
                                $"Thời hạn: tháng,\n" +
                                $"Tổng tiền:  VNĐ",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hợp đồng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        private string TaoMaHopDong()
        {
            string maHD = "";
            try
            {
                string sql = "SELECT TOP 1 MAHD FROM HOPDONG ORDER BY MAHD DESC";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string maHDCuoi = result.ToString();
                            int soThuTu = int.Parse(maHDCuoi.Substring(2)) + 1;
                            maHD = "HD" + soThuTu.ToString("D6");
                        }
                        else
                        {
                            maHD = "HD000001";
                        }
                    }
                }
            }
            catch
            {
                maHD = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            return maHD;
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void txtTHOIHAN_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtGIAPHONG.Text) &&
                    !string.IsNullOrWhiteSpace(txtTHOIHAN.Text))
                {
                    // Xóa dấu phẩy trong số tiền trước khi parse
                    decimal giaPhong = decimal.Parse(txtGIAPHONG.Text.Replace(",", ""));
                    int thoiHan = int.Parse(txtTHOIHAN.Text);

                    decimal tongTien = giaPhong * thoiHan;
                    txtTONGTIEN.Text = tongTien.ToString("N0");
                }
            }
            catch
            {
                // Bỏ qua lỗi khi đang nhập liệu
            }
        }
    }
    
}
