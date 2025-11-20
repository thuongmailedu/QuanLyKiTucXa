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
    public partial class frm_addDICHVU : Form
    {

        private string constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public frm_addDICHVU()
        {
            InitializeComponent();
        }

        

        private void frm_addDICHVU_Load(object sender, EventArgs e)
        {
            // Load ComboBox tên dịch vụ
            comTENDV.Items.Clear();
            comTENDV.Items.Add("Điện");
            comTENDV.Items.Add("Nước");
            comTENDV.Items.Add("Internet");

            // Mặc định chọn dịch vụ đầu tiên
            comTENDV.SelectedIndex = 0;

            // Set ngày mặc định
            dtpTUNGAY.Value = DateTime.Now;
            dtpDENNGAY.Value = DateTime.Now.AddMonths(12); // Mặc định 1 năm

            // Disable txtMADV vì sẽ tự sinh
            txtMADV.Enabled = false;

        }
        // ⭐ Khi chọn tên dịch vụ
        private void comTENDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDV = comTENDV.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(tenDV))
                return;

            switch (tenDV)
            {
                case "Điện":
                    txtMADV.Text = TaoMaDichVu("DIEN");
                    txtDONVI.Text = "KwH";
                    break;

                case "Nước":
                    txtMADV.Text = TaoMaDichVu("NUOC");
                    txtDONVI.Text = "Khối";
                    break;

                case "Internet":
                    txtMADV.Text = TaoMaDichVu("INT");
                    txtDONVI.Text = "Người";
                    break;
            }
        }

        // ⭐ Tự động sinh mã dịch vụ
        private string TaoMaDichVu(string prefix)
        {
            string maDV = "";
            try
            {
                string sql = $@"SELECT TOP 1 MADV 
                               FROM DICHVU 
                               WHERE MADV LIKE '{prefix}_%' 
                               ORDER BY MADV DESC";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Ví dụ: DIEN_01 -> DIEN_02
                            string maCuoi = result.ToString();
                            string[] parts = maCuoi.Split('_');
                            int soThuTu = int.Parse(parts[1]) + 1;
                            maDV = $"{prefix}_{soThuTu:D2}";
                        }
                        else
                        {
                            // Chưa có dịch vụ nào
                            maDV = $"{prefix}_01";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                maDV = $"{prefix}_{DateTime.Now:yyyyMMddHHmmss}";
            }

            return maDV;
        }

        // ⭐ Kiểm tra trùng thời gian dịch vụ
        private bool KiemTraTrungThoiGian(string tenDV, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_KiemTraTrungThoiGianDichVu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LOAI_DV", tenDV);
                        cmd.Parameters.AddWithValue("@TUNGAY", tuNgay);
                        cmd.Parameters.AddWithValue("@DENNGAY", denNgay);
                        cmd.Parameters.AddWithValue("@MADV_HIENTAI", DBNull.Value);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int biTrung = Convert.ToInt32(reader["BiTrung"]);

                                if (biTrung == 1)
                                {
                                    string maDV = reader["MADV"].ToString();
                                    DateTime tungay = Convert.ToDateTime(reader["TUNGAY"]);
                                    DateTime denngay = reader["DENNGAY"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["DENNGAY"])
                                        : DateTime.MaxValue;

                                    MessageBox.Show(
                                        $"Dịch vụ '{tenDV}' đã có thời gian áp dụng trùng lặp!\n\n" +
                                        $"Mã DV: {maDV}\n" +
                                        $"Từ ngày: {tungay:dd/MM/yyyy}\n" +
                                        $"Đến ngày: {(denngay == DateTime.MaxValue ? "Không giới hạn" : denngay.ToString("dd/MM/yyyy"))}\n\n" +
                                        "Vui lòng chọn thời gian khác!",
                                        "Cảnh báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                    return true; // Có trùng
                                }
                            }
                        }
                    }
                }
                return false; // Không trùng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra thời gian: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Coi như lỗi
            }
        }

        // ⭐ Nút LƯU
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu
                string maDV = txtMADV.Text.Trim();
                string tenDV = comTENDV.SelectedItem?.ToString();
                decimal donGia = 0;
                string donVi = txtDONVI.Text.Trim();
                DateTime tuNgay = dtpTUNGAY.Value.Date;
                DateTime denNgay = dtpDENNGAY.Value.Date;
                string trangThai = comTRANGTHAI.SelectedItem?.ToString() ?? "Đang áp dụng";

                // ⭐ VALIDATE
                if (string.IsNullOrEmpty(tenDV))
                {
                    MessageBox.Show("Vui lòng chọn tên dịch vụ!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comTENDV.Focus();
                    return;
                }

                if (!decimal.TryParse(txtDONGIA.Text.Trim(), out donGia) || donGia <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đơn giá hợp lệ (> 0)!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDONGIA.Focus();
                    return;
                }

                // ⭐ Kiểm tra ngày
                if (denNgay <= tuNgay)
                {
                    MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDENNGAY.Focus();
                    return;
                }

                // ⭐ Kiểm tra trùng thời gian
                if (KiemTraTrungThoiGian(tenDV, tuNgay, denNgay))
                {
                    return; // Đã có thông báo trong hàm KiemTraTrungThoiGian
                }

                // ⭐ LƯU VÀO DATABASE
                string sql = @"INSERT INTO DICHVU (MADV, TENDV, DONGIA, DONVI, TUNGAY, DENNGAY, TRANGTHAI)
                              VALUES (@MADV, @TENDV, @DONGIA, @DONVI, @TUNGAY, @DENNGAY, @TRANGTHAI)";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MADV", maDV);
                        cmd.Parameters.AddWithValue("@TENDV", tenDV);
                        cmd.Parameters.AddWithValue("@DONGIA", donGia);
                        cmd.Parameters.AddWithValue("@DONVI", donVi);
                        cmd.Parameters.AddWithValue("@TUNGAY", tuNgay);
                        cmd.Parameters.AddWithValue("@DENNGAY", denNgay);
                        cmd.Parameters.AddWithValue("@TRANGTHAI", trangThai);

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show(
                                $"Thêm dịch vụ thành công!\n\n" +
                                $"Mã DV: {maDV}\n" +
                                $"Tên DV: {tenDV}\n" +
                                $"Đơn giá: {donGia:N0} VNĐ/{donVi}\n" +
                                $"Từ ngày: {tuNgay:dd/MM/yyyy}\n" +
                                $"Đến ngày: {denNgay:dd/MM/yyyy}",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút HỦY
        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // 👈 Dispose luôn để chắc chắn
        }

        // Validate khi thay đổi ngày
        private void dtpDENNGAY_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDENNGAY.Value <= dtpTUNGAY.Value)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDENNGAY.Value = dtpTUNGAY.Value.AddMonths(1);
            }
        }
    }
}
    

