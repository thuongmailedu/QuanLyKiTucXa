using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLPHONG
{
    public partial class UC_NHA_PHONG : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_NHA_PHONG()
        {
            InitializeComponent();
        }

        private void UC_NHA_PHONG_Load(object sender, EventArgs e)
        {
            LoadDanhSachNha();
        }

        // Load danh sách nhà
        private void LoadDanhSachNha()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        MANHA,
                                        LOAIPHONG,
                                        GIOITINH,
                                        GIAPHONG,
                                        TOIDA
                                       
                                     FROM NHA
                                     ORDER BY MANHA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_NHA.DataSource = dt;

                        // Thiết lập tiêu đề cột
                        if (dgvDM_NHA.Columns["MANHA"] != null)
                            dgvDM_NHA.Columns["MANHA"].HeaderText = "Mã Nhà";

                        if (dgvDM_NHA.Columns["LOAIPHONG"] != null)
                            dgvDM_NHA.Columns["LOAIPHONG"].HeaderText = "Loại Phòng";

                        if (dgvDM_NHA.Columns["GIOITINH"] != null)
                            dgvDM_NHA.Columns["GIOITINH"].HeaderText = "Giới Tính";

                        if (dgvDM_NHA.Columns["GIAPHONG"] != null)
                        {
                            dgvDM_NHA.Columns["GIAPHONG"].HeaderText = "Giá Phòng";
                            dgvDM_NHA.Columns["GIAPHONG"].DefaultCellStyle.Format = "N0";
                        }

                        if (dgvDM_NHA.Columns["TOIDA"] != null)
                            dgvDM_NHA.Columns["TOIDA"].HeaderText = "Tối Đa";

                        //if (dgvDM_NHA.Columns["TRANGTHAI"] != null)
                        //    dgvDM_NHA.Columns["TRANGTHAI"].HeaderText = "Trạng Thái";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút thêm nhà mới
        private void btnadd_NHA_Click(object sender, EventArgs e)
        {
            frm_DM_NHA form = new frm_DM_NHA();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNha(); // Reload lại danh sách sau khi thêm
            }
        }

        // Nút sửa nhà
        private void btnedit_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_NHA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHA.SelectedRows[0];

            string maNha = row.Cells["MANHA"].Value.ToString();
            string loaiPhong = row.Cells["LOAIPHONG"].Value.ToString();
            string gioiTinh = row.Cells["GIOITINH"].Value.ToString();
            decimal giaPhong = Convert.ToDecimal(row.Cells["GIAPHONG"].Value);
            int toiDa = Convert.ToInt32(row.Cells["TOIDA"].Value);

            frm_DM_NHA form = new frm_DM_NHA(maNha, loaiPhong, gioiTinh, giaPhong, toiDa);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNha(); // Reload lại danh sách sau khi sửa
            }
        }

        // Nút xóa nhà
        private void btndelete_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_NHA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHA.SelectedRows[0];
            string maNha = row.Cells["MANHA"].Value.ToString();

            // Kiểm tra xem nhà đã có phòng chưa
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có phòng nào thuộc nhà này không
                    string checkQuery = "SELECT COUNT(*) FROM PHONG WHERE MANHA = @MANHA";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MANHA", maNha);
                        int countPhong = (int)checkCmd.ExecuteScalar();

                        if (countPhong > 0)
                        {
                            MessageBox.Show($"Không thể xóa nhà {maNha} vì đã có {countPhong} phòng được tạo!\n" +
                                          "Vui lòng xóa các phòng trước khi xóa nhà.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa nhà {maNha}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM NHA WHERE MANHA = @MANHA";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MANHA", maNha);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa nhà thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDanhSachNha(); // Reload lại danh sách
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}