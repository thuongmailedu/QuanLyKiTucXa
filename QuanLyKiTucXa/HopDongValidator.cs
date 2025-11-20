using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public class HopDongValidator
    {
        private string connectionString;

        public HopDongValidator(string connString)
        {
            this.connectionString = connString;
        }

        /// <summary>
        /// Kiểm tra xem sinh viên có hợp đồng trùng thời gian không
        /// </summary>
        public bool KiemTraHopDongTrung(string maSV, DateTime tuNgay, DateTime denNgay, string maHDHienTai = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_KiemTraHopDongTrung", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MASV", maSV);
                    cmd.Parameters.AddWithValue("@TUNGAY", tuNgay);
                    cmd.Parameters.AddWithValue("@DENNGAY", denNgay);

                    if (!string.IsNullOrEmpty(maHDHienTai))
                        cmd.Parameters.AddWithValue("@MAHD_HIENTAI", maHDHienTai);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int biTrung = Convert.ToInt32(reader["BiTrung"]);

                                if (biTrung == 1)
                                {
                                    string maPhong = reader["MA_PHONG"].ToString();
                                    DateTime ngayBatDau = Convert.ToDateTime(reader["TUNGAY"]);
                                    DateTime ngayKetThuc = Convert.ToDateTime(reader["NgayKetThucThucTe"]);

                                    MessageBox.Show(
                                        $"Sinh viên {maSV} đã có hợp đồng từ {ngayBatDau:dd/MM/yyyy} đến {ngayKetThuc:dd/MM/yyyy} tại phòng {maPhong}.\n\n" +
                                        "Không thể tạo hợp đồng trùng thời gian!",
                                        "Cảnh báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                    return true; // Có trùng
                                }
                            }
                        }
                        return false; // Không trùng
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kiểm tra hợp đồng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true; // Coi như có lỗi, không cho phép tạo
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra xem phòng còn chỗ trống không
        /// </summary>
        public bool KiemTraPhongConCho(string maPhong, DateTime tuNgay, DateTime denNgay, string maHDHienTai = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_KiemTraSoLuongPhong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                    cmd.Parameters.AddWithValue("@TUNGAY", tuNgay);
                    cmd.Parameters.AddWithValue("@DENNGAY", denNgay);

                    if (!string.IsNullOrEmpty(maHDHienTai))
                        cmd.Parameters.AddWithValue("@MAHD_HIENTAI", maHDHienTai);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int conCho = Convert.ToInt32(reader["ConCho"]);
                                int soLuongToiDa = Convert.ToInt32(reader["SoLuongToiDa"]);
                                int soSVHienTai = Convert.ToInt32(reader["SoSVHienTai"]);
                                int soChoConLai = Convert.ToInt32(reader["SoChoConLai"]);

                                if (conCho == 0)
                                {
                                    MessageBox.Show(
                                        $"Phòng {maPhong} đã đầy trong khoảng thời gian này!\n\n" +
                                        $"Số lượng tối đa: {soLuongToiDa}\n" +
                                        $"Đã có: {soSVHienTai} sinh viên\n" +
                                        $"Còn lại: {soChoConLai} chỗ",
                                        "Cảnh báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                    return false; // Không còn chỗ
                                }
                                return true; // Còn chỗ
                            }
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kiểm tra số lượng phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra tất cả điều kiện trước khi tạo/update hợp đồng
        /// </summary>
        public bool KiemTraDieuKienHopDong(string maSV, string maPhong, DateTime tuNgay, DateTime denNgay, string maHDHienTai = null)
        {
            // Kiểm tra hợp đồng trùng
            if (KiemTraHopDongTrung(maSV, tuNgay, denNgay, maHDHienTai))
            {
                return false;
            }

            // Kiểm tra phòng còn chỗ
            if (!KiemTraPhongConCho(maPhong, tuNgay, denNgay, maHDHienTai))
            {
                return false;
            }

            return true; // Đạt tất cả điều kiện
        }
    }
}