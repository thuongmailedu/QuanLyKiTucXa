using QuanLyKiTucXa.Formadd.DM_KHAC_FORM;
//using QuanLyKiTucXa.Formadd.QLCSVC_FORM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.DMKHAC
{
    public partial class UC_NHACC : UserControl
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public UC_NHACC()
        {
            InitializeComponent();
            this.Load += UC_NHACC_Load;
        }

        private void UC_NHACC_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhaCC();
        }

        private void LoadDanhSachNhaCC()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        MA_NHACC,
                                        TEN_NHACC,
                                        SDT,
                                        DIACHI,
                                        GHICHU
                                     FROM NHACC
                                     ORDER BY MA_NHACC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_NHACC.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_NHACC.Rows.Add();

                            dgvDM_NHACC.Rows[index].Cells["STT"].Value = stt++;
                            dgvDM_NHACC.Rows[index].Cells["MA_NHACC"].Value = row["MA_NHACC"].ToString();
                            dgvDM_NHACC.Rows[index].Cells["TEN_NHACC"].Value = row["TEN_NHACC"].ToString();
                            dgvDM_NHACC.Rows[index].Cells["SDT"].Value = row["SDT"] != DBNull.Value ? row["SDT"].ToString() : "";
                            dgvDM_NHACC.Rows[index].Cells["DIACHI"].Value = row["DIACHI"] != DBNull.Value ? row["DIACHI"].ToString() : "";
                            dgvDM_NHACC.Rows[index].Cells["GHICHU"].Value = row["GHICHU"] != DBNull.Value ? row["GHICHU"].ToString() : "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_NHACC_Click(object sender, EventArgs e)
        {
            frmadd_NHACC form = new frmadd_NHACC();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNhaCC();
            }
        }

        private void btnedit_NHACC_Click(object sender, EventArgs e)
        {
            if (dgvDM_NHACC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHACC.SelectedRows[0];
            string maNhaCC = row.Cells["MA_NHACC"].Value.ToString();

            frmadd_NHACC form = new frmadd_NHACC(maNhaCC);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNhaCC();
            }
        }

        private void btndelete_NHACC_Click(object sender, EventArgs e)
        {
            if (dgvDM_NHACC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHACC.SelectedRows[0];
            string maNhaCC = row.Cells["MA_NHACC"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có cơ sở vật chất nào từ nhà cung cấp này không
                    string checkQuery = "SELECT COUNT(*) FROM DM_CSVC WHERE MA_NHACC = @MA_NHACC";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MA_NHACC", maNhaCC);
                        int countCSVC = (int)checkCmd.ExecuteScalar();

                        if (countCSVC > 0)
                        {
                            MessageBox.Show($"Không thể xóa nhà cung cấp {maNhaCC} vì đã có {countCSVC} cơ sở vật chất liên quan!\n" +
                                          "Vui lòng xóa hoặc chuyển các cơ sở vật chất sang nhà cung cấp khác trước.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Xác nhận xóa
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa nhà cung cấp {maNhaCC}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM NHACC WHERE MA_NHACC = @MA_NHACC";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MA_NHACC", maNhaCC);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDanhSachNhaCC();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}