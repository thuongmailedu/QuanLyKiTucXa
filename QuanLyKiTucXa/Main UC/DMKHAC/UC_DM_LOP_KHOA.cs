using QuanLyKiTucXa.Formadd.DM_KHAC_FORM;
//using QuanLyKiTucXa.Formadd.QLKHOA_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    public partial class UC_DM_LOP_KHOA : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_DM_LOP_KHOA()
        {
            InitializeComponent();
            this.Load += UC_DM_LOP_KHOA_Load;
        }

        private void UC_DM_LOP_KHOA_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhoa();
            LoadDanhSachLop();
        }

        #region QUẢN LÝ KHOA

        private void LoadDanhSachKhoa()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        MAKHOA,
                                        TENKHOA
                                     FROM KHOA
                                     ORDER BY MAKHOA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_KHOA.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_KHOA.Rows.Add();

                            dgvDM_KHOA.Rows[index].Cells["STT"].Value = stt++;
                            dgvDM_KHOA.Rows[index].Cells["MAKHOA"].Value = row["MAKHOA"].ToString();
                            dgvDM_KHOA.Rows[index].Cells["TENKHOA"].Value = row["TENKHOA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_KHOA_Click(object sender, EventArgs e)
        {
            frmadd_KHOA form = new frmadd_KHOA();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachKhoa();
            }
        }

        private void btnedit_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvDM_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_KHOA.SelectedRows[0];
            string maKhoa = row.Cells["MAKHOA"].Value.ToString();

            frmadd_KHOA form = new frmadd_KHOA(maKhoa);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachKhoa();
                LoadDanhSachLop(); // Reload lớp vì tên khoa có thể đổi
            }
        }

        private void btndelete_KHOA_Click(object sender, EventArgs e)
        {
            if (dgvDM_KHOA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khoa để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_KHOA.SelectedRows[0];
            string maKhoa = row.Cells["MAKHOA"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có lớp nào thuộc khoa này không
                    string checkQuery = "SELECT COUNT(*) FROM LOP WHERE MAKHOA = @MAKHOA";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MAKHOA", maKhoa);
                        int countLop = (int)checkCmd.ExecuteScalar();

                        if (countLop > 0)
                        {
                            MessageBox.Show($"Không thể xóa khoa {maKhoa} vì đã có {countLop} lớp thuộc khoa này!\n" +
                                          "Vui lòng xóa các lớp trước khi xóa khoa.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa khoa {maKhoa}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM KHOA WHERE MAKHOA = @MAKHOA";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MAKHOA", maKhoa);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa khoa thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDanhSachKhoa();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region QUẢN LÝ LỚP

        private void LoadDanhSachLop()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        L.MALOP,
                                        L.TENLOP,
                                        L.MAKHOA,
                                        K.TENKHOA
                                     FROM LOP L
                                     INNER JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                                     ORDER BY L.MAKHOA, L.MALOP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_LOP.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_LOP.Rows.Add();

                            dgvDM_LOP.Rows[index].Cells["STT_2"].Value = stt++;
                            dgvDM_LOP.Rows[index].Cells["MAKHOA_2"].Value = row["MAKHOA"].ToString();
                            dgvDM_LOP.Rows[index].Cells["TENKHOA_2"].Value = row["TENKHOA"].ToString();
                            dgvDM_LOP.Rows[index].Cells["MALOP"].Value = row["MALOP"].ToString();
                            dgvDM_LOP.Rows[index].Cells["TENLOP"].Value = row["TENLOP"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_NHA_Click(object sender, EventArgs e)
        {
            frmadd_LOP form = new frmadd_LOP();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachLop();
            }
        }

        private void btnedit_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_LOP.SelectedRows[0];
            string maLop = row.Cells["MALOP"].Value.ToString();

            frmadd_LOP form = new frmadd_LOP(maLop);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachLop();
            }
        }

        private void btndelete_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_LOP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_LOP.SelectedRows[0];
            string maLop = row.Cells["MALOP"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có sinh viên nào thuộc lớp này không
                    string checkQuery = "SELECT COUNT(*) FROM SINHVIEN WHERE MALOP = @MALOP";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MALOP", maLop);
                        int countSV = (int)checkCmd.ExecuteScalar();

                        if (countSV > 0)
                        {
                            MessageBox.Show($"Không thể xóa lớp {maLop} vì đã có {countSV} sinh viên thuộc lớp này!\n" +
                                          "Vui lòng chuyển sinh viên sang lớp khác trước khi xóa lớp.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa lớp {maLop}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM LOP WHERE MALOP = @MALOP";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MALOP", maLop);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa lớp thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDanhSachLop();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void lblSinhVien_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvDMPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}