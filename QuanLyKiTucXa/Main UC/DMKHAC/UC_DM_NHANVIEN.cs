using QuanLyKiTucXa.Formadd.DM_KHAC_FORM;
//using QuanLyKiTucXa.Formadd.QLNV_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    public partial class UC_DM_NHANVIEN : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_DM_NHANVIEN()
        {
            InitializeComponent();
            this.Load += UC_DM_NHANVIEN_Load;
        }

        private void UC_DM_NHANVIEN_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
            SetButtonPermissions();
        }

        private void SetButtonPermissions()
        {
            // Kiểm tra quyền từ UserSession
            if (!UserSession.IsAdmin())
            {
                // Nếu không phải admin thì disable các nút
                btnadd_NHANVIEN.Enabled = false;
                btnedit_NHANVIEN.Enabled = false;
                btndelete_NHA.Enabled = false;

                MessageBox.Show("Bạn không có quyền quản lý nhân viên!\nChỉ ADMIN mới có quyền này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                btnadd_NHANVIEN.Enabled = true;
                btnedit_NHANVIEN.Enabled = true;
                btndelete_NHA.Enabled = true;
            }
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        NV.MANV,
                                        NV.TENNV,
                                        NV.MANHA
                                     FROM NHANVIEN NV
                                     ORDER BY NV.MANV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_NHANVIEN.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_NHANVIEN.Rows.Add();

                            dgvDM_NHANVIEN.Rows[index].Cells["STT"].Value = stt++;
                            dgvDM_NHANVIEN.Rows[index].Cells["MANV"].Value = row["MANV"].ToString();
                            dgvDM_NHANVIEN.Rows[index].Cells["TENNV"].Value = row["TENNV"].ToString();
                            dgvDM_NHANVIEN.Rows[index].Cells["MANHA"].Value = row["MANHA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_NHANVIEN_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền
            if (!UserSession.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền thêm nhân viên!\nChỉ ADMIN mới có quyền này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmadd_NHANVIEN form = new frmadd_NHANVIEN();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNhanVien();
            }
        }

        private void btnedit_NHANVIEN_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền
            if (!UserSession.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền sửa thông tin nhân viên!\nChỉ ADMIN mới có quyền này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDM_NHANVIEN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHANVIEN.SelectedRows[0];

            string maNV = row.Cells["MANV"].Value.ToString();
            string tenNV = row.Cells["TENNV"].Value.ToString();
            string maNha = row.Cells["MANHA"].Value.ToString();

            // Lấy quyền từ bảng LOGIN
            string quyen = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT QUYEN FROM LOGIN WHERE TENDN = @TENDN";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TENDN", maNV);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                            quyen = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin quyền: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmadd_NHANVIEN form = new frmadd_NHANVIEN(maNV, tenNV, maNha, quyen);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNhanVien();
            }
        }

        private void btndelete_NHA_Click(object sender, EventArgs e)
        {
            // Kiểm tra quyền
            if (!UserSession.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền xóa nhân viên!\nChỉ ADMIN mới có quyền này.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDM_NHANVIEN.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHANVIEN.SelectedRows[0];
            string maNV = row.Cells["MANV"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có hợp đồng nào do nhân viên này lập không
                    string checkQuery = "SELECT COUNT(*) FROM HOPDONG WHERE MANV = @MANV";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MANV", maNV);
                        int countHD = (int)checkCmd.ExecuteScalar();

                        if (countHD > 0)
                        {
                            MessageBox.Show($"Không thể xóa nhân viên {maNV} vì đã có {countHD} hợp đồng liên quan!\n" +
                                          "Vui lòng xóa các hợp đồng trước khi xóa nhân viên.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa nhân viên {maNV}?\nDữ liệu tài khoản cũng sẽ bị xóa!",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Xóa từ LOGIN trước
                            string deleteLoginQuery = "DELETE FROM LOGIN WHERE TENDN = @TENDN";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteLoginQuery, conn, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@TENDN", maNV);
                                deleteCmd.ExecuteNonQuery();
                            }

                            // Xóa từ NHANVIEN
                            string deleteNVQuery = "DELETE FROM NHANVIEN WHERE MANV = @MANV";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteNVQuery, conn, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@MANV", maNV);
                                deleteCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachNhanVien();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}