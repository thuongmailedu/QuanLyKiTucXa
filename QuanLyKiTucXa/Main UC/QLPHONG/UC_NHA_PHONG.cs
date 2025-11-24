using Guna.UI2.WinForms;
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
            this.Load += UC_NHA_PHONG_Load;
        }

        private void UC_NHA_PHONG_Load(object sender, EventArgs e)
        {
            LoadComboBoxNHA();
            LoadDanhSachNha();
            LoadDanhSachPhong(null); // Load tất cả phòng
        }

        #region QUẢN LÝ NHÀ

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

                        dgvDM_NHA.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_NHA.Rows.Add();

                            dgvDM_NHA.Rows[index].Cells["STT"].Value = stt++;
                            dgvDM_NHA.Rows[index].Cells["MANHA_2"].Value = row["MANHA"].ToString();
                            dgvDM_NHA.Rows[index].Cells["GIOITINH_2"].Value = row["GIOITINH"].ToString();
                            dgvDM_NHA.Rows[index].Cells["LOAIPHONG"].Value = row["LOAIPHONG"].ToString();
                            dgvDM_NHA.Rows[index].Cells["GIAPHONG"].Value = Convert.ToDecimal(row["GIAPHONG"]).ToString("N0");
                            dgvDM_NHA.Rows[index].Cells["TOIDA_2"].Value = row["TOIDA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnadd_NHA_Click(object sender, EventArgs e)
        {
            frm_DM_NHA form = new frm_DM_NHA();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNha();
                LoadComboBoxNHA(); // Reload combobox
            }
        }

        private void btnedit_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_NHA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHA.SelectedRows[0];

            string maNha = row.Cells["MANHA_2"].Value.ToString();
            string loaiPhong = row.Cells["LOAIPHONG"].Value.ToString();
            string gioiTinh = row.Cells["GIOITINH_2"].Value.ToString();

            string giaPhongStr = row.Cells["GIAPHONG"].Value.ToString().Replace(",", "");
            decimal giaPhong = Convert.ToDecimal(giaPhongStr);

            int toiDa = Convert.ToInt32(row.Cells["TOIDA_2"].Value);

            frm_DM_NHA form = new frm_DM_NHA(maNha, loaiPhong, gioiTinh, giaPhong, toiDa);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNha();
                LoadComboBoxNHA();
            }
        }

        private void btndelete_NHA_Click(object sender, EventArgs e)
        {
            if (dgvDM_NHA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_NHA.SelectedRows[0];
            string maNha = row.Cells["MANHA_2"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

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
                                LoadDanhSachNha();
                                LoadComboBoxNHA();
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

        #endregion

        #region QUẢN LÝ PHÒNG

        private void LoadComboBoxNHA()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MANHA, MANHA + ' - ' + LOAIPHONG + ' - ' + GIOITINH AS DISPLAY_TEXT FROM NHA ORDER BY MANHA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Thêm dòng "Tất cả"
                        DataRow newRow = dt.NewRow();
                        newRow["MANHA"] = DBNull.Value;
                        newRow["DISPLAY_TEXT"] = "-- Tất cả nhà --";
                        dt.Rows.InsertAt(newRow, 0);

                        comNHA.DataSource = dt;
                        comNHA.DisplayMember = "DISPLAY_TEXT";
                        comNHA.ValueMember = "MANHA";
                        comNHA.SelectedIndex = 0;

                        // Đăng ký sự kiện
                        comNHA.SelectedIndexChanged += comNHA_SelectedIndexChanged;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load combobox nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comNHA.SelectedValue == null || comNHA.SelectedValue == DBNull.Value)
            {
                LoadDanhSachPhong(null); // Load tất cả
            }
            else
            {
                LoadDanhSachPhong(comNHA.SelectedValue.ToString());
            }
        }

        private void LoadDanhSachPhong(string maNha)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        P.MA_PHONG,
                                        P.MANHA,
                                        N.GIOITINH,
                                        N.LOAIPHONG,
                                        N.GIAPHONG,
                                        N.TOIDA
                                     FROM PHONG P
                                     INNER JOIN NHA N ON P.MANHA = N.MANHA";

                    if (!string.IsNullOrEmpty(maNha))
                    {
                        query += " WHERE P.MANHA = @MANHA";
                    }

                    query += " ORDER BY P.MANHA, P.MA_PHONG";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(maNha))
                        {
                            cmd.Parameters.AddWithValue("@MANHA", maNha);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvDM_PHONG.Rows.Clear();

                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            int index = dgvDM_PHONG.Rows.Add();

                            dgvDM_PHONG.Rows[index].Cells["STT_2"].Value = stt++;
                            dgvDM_PHONG.Rows[index].Cells["MA_PHONG"].Value = row["MA_PHONG"].ToString();
                            dgvDM_PHONG.Rows[index].Cells["MANHA"].Value = row["MANHA"].ToString();
                            dgvDM_PHONG.Rows[index].Cells["GIOITINH"].Value = row["GIOITINH"].ToString();
                            dgvDM_PHONG.Rows[index].Cells["LOAIPHONG_2"].Value = row["LOAIPHONG"].ToString();
                            dgvDM_PHONG.Rows[index].Cells["GIAPHONG_2"].Value = Convert.ToDecimal(row["GIAPHONG"]).ToString("N0");
                            dgvDM_PHONG.Rows[index].Cells["TOIDA"].Value = row["TOIDA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm event cho nút btnAdd (btnadd_PHONG)
        private void btnadd_PHONG_Click(object sender, EventArgs e)
        {
            frm_DM_PHONG form = new frm_DM_PHONG();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachPhong(comNHA.SelectedValue?.ToString());
            }
        }

        // Thêm event cho nút guna2CircleButton5 (btnedit_PHONG)
        private void btnedit_PHONG_Click(object sender, EventArgs e)
        {
            if (dgvDM_PHONG.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_PHONG.SelectedRows[0];

            string maPhong = row.Cells["MA_PHONG"].Value.ToString();

            // Lấy mã nhà từ database
            string maNha = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MANHA FROM PHONG WHERE MA_PHONG = @MA_PHONG";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            maNha = result.ToString();
                    }
                }
            }
            catch { }

            frm_DM_PHONG form = new frm_DM_PHONG(maPhong, maNha);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachPhong(comNHA.SelectedValue?.ToString());
            }
        }

        // Thêm event cho nút guna2CircleButton6 (btndelete_PHONG)
        private void btndelete_PHONG_Click(object sender, EventArgs e)
        {
            if (dgvDM_PHONG.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDM_PHONG.SelectedRows[0];
            string maPhong = row.Cells["MA_PHONG"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra có hợp đồng nào không
                    string checkQuery = "SELECT COUNT(*) FROM HOPDONG WHERE MA_PHONG = @MA_PHONG";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                        int countHD = (int)checkCmd.ExecuteScalar();

                        if (countHD > 0)
                        {
                            MessageBox.Show($"Không thể xóa phòng {maPhong} vì đã có hợp đồng!\n" +
                                          "Vui lòng xóa các hợp đồng trước khi xóa phòng.",
                                          "Thông báo",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa phòng {maPhong}?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM PHONG WHERE MA_PHONG = @MA_PHONG";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                        {
                            deleteCmd.Parameters.AddWithValue("@MA_PHONG", maPhong);
                            int affectedRows = deleteCmd.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Xóa phòng thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDanhSachPhong(comNHA.SelectedValue?.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

       
    }
}