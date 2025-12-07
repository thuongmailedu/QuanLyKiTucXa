using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.HSSV
{
    public partial class frm_addHSSV : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMASV = "";
        private string editingMAKHOA = "";
        private string editingMALOP = "";
        private bool isLoadingData = false;

        public frm_addHSSV()
        {
            InitializeComponent();
        }

        // Constructor cho chế độ sửa
        public frm_addHSSV(string maSV, string maPhong, string maNha, string tinhTrangCuTru)
        {
            InitializeComponent();

            isEditMode = true;
            editingMASV = maSV;

            // Hiển thị thông tin
            txtMA_PHONG.Text = maPhong;
            txtMANHA.Text = maNha;
            txtTINHTRANG_CUTRU.Text = tinhTrangCuTru;

            this.Text = "Cập nhật thông tin sinh viên";
        }

        private void frm_addHSSV_Load(object sender, EventArgs e)
        {
            // Disable các textbox không được nhập
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtTINHTRANG_CUTRU.ReadOnly = true;

            txtMA_PHONG.BackColor = System.Drawing.SystemColors.Control;
            txtMANHA.BackColor = System.Drawing.SystemColors.Control;
            txtTINHTRANG_CUTRU.BackColor = System.Drawing.SystemColors.Control;

            // Load combo Khoa trước
            LoadComboBoxKhoa();

            // Đăng ký sự kiện cho comTENKHOA SAU KHI đã load xong
           // comTENKHOA.SelectedIndexChanged += comTENKHOA_SelectedIndexChanged;
            // ✅ THÊM: Đăng ký sự kiện FormClosing để xử lý khi nhấn nút X
            this.FormClosing += frm_addHSSV_FormClosing;

            if (!isEditMode)
            {
                txtTINHTRANG_CUTRU.Text = "Chưa có hợp đồng";
            }
            else
            {
                // Load dữ liệu sinh viên SAU KHI đã load combo và đăng ký event
                LoadSinhVien(editingMASV);
            }
        }

      // ✅ THÊM METHOD MỚI: Xử lý khi đóng form
        private void frm_addHSSV_FormClosing(object sender, FormClosingEventArgs e)
                {
                    // Nếu người dùng nhấn nút X (đóng form) mà chưa set DialogResult
                    // thì tự động set thành Cancel để tránh lỗi
                    if (this.DialogResult == DialogResult.None)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }

        private void LoadComboBoxKhoa()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MAKHOA, TENKHOA FROM KHOA ORDER BY MAKHOA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comTENKHOA.DataSource = dt;
                        comTENKHOA.DisplayMember = "TENKHOA";
                        comTENKHOA.ValueMember = "MAKHOA";
                        comTENKHOA.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách khoa: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comTENKHOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            // THÊM KIỂM TRA FLAG - Không xử lý nếu đang load dữ liệu
            if (isLoadingData) return;

            if (comTENKHOA.SelectedIndex != -1 && comTENKHOA.SelectedValue != null)
            {
                string maKhoa = comTENKHOA.SelectedValue.ToString();
                LoadComboBoxLop(maKhoa);
            }
            else
            {
                comTENLOP.DataSource = null;
            }
        }

        private void LoadComboBoxLop(string maKhoa)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MALOP, TENLOP FROM LOP WHERE MAKHOA = @MAKHOA ORDER BY MALOP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MAKHOA", maKhoa);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comTENLOP.DataSource = dt;
                        comTENLOP.DisplayMember = "TENLOP";
                        comTENLOP.ValueMember = "MALOP";

                        // Nếu đang ở chế độ edit và có mã lớp được lưu trước đó
                        if (isEditMode && !string.IsNullOrEmpty(editingMALOP))
                        {
                            comTENLOP.SelectedValue = editingMALOP;
                        }
                        else
                        {
                            comTENLOP.SelectedIndex = -1;
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

        private void LoadSinhVien(string maSV)
        {
            try
            {
                // BẬT FLAG TRƯỚC KHI BẮT ĐẦU LOAD
                isLoadingData = true;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ LỖI 2: Kiểm tra sinh viên có hợp đồng chưa để quyết định cho sửa mã
                    string checkHDQuery = "SELECT COUNT(*) FROM HOPDONG WHERE MASV = @MASV";
                    bool coHopDong = false;

                    using (SqlCommand checkCmd = new SqlCommand(checkHDQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MASV", maSV);
                        int countHD = (int)checkCmd.ExecuteScalar();
                        coHopDong = countHD > 0;
                    }

                    string query = @"SELECT SV.MASV, SV.TENSV, SV. NGAYSINH, SV.GIOITINH, 
                                          SV.CCCD, SV.SDT, SV. MALOP, L.MAKHOA,
                                          TN.TEN_THANNHAN, TN.SDT AS SDT_THANNHAN, 
                                          TN. MOIQUANHE, TN. DIACHI
                                   FROM SINHVIEN SV
                                   INNER JOIN LOP L ON SV.MALOP = L. MALOP
                                   LEFT JOIN THANNHAN TN ON SV.MASV = TN.MASV
                                   WHERE SV. MASV = @MASV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MASV", maSV);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMASV.Text = reader["MASV"].ToString();

                                // ✅ LỖI 2: Cho phép sửa MASV nếu chưa có hợp đồng
                                if (coHopDong)
                                {
                                    txtMASV.ReadOnly = true;
                                    txtMASV.BackColor = System.Drawing.SystemColors.Control;
                                }
                                else
                                {
                                    txtMASV.ReadOnly = false;
                                    txtMASV.BackColor = System.Drawing.SystemColors.Window;
                                }

                                txtTENSV.Text = reader["TENSV"].ToString();

                                if (reader["NGAYSINH"] != DBNull.Value)
                                    dtpNGAYSINH.Value = Convert.ToDateTime(reader["NGAYSINH"]);

                                comGIOITINH.Text = reader["GIOITINH"] != DBNull.Value ? reader["GIOITINH"].ToString() : "";
                                txtCCCD.Text = reader["CCCD"] != DBNull.Value ? reader["CCCD"].ToString() : "";
                                txtSDT.Text = reader["SDT"] != DBNull.Value ? reader["SDT"].ToString() : "";

                                // Lưu mã khoa và mã lớp
                                editingMAKHOA = reader["MAKHOA"].ToString();
                                editingMALOP = reader["MALOP"].ToString();

                                // Set khoa (event sẽ không chạy vì flag đang bật)
                                comTENKHOA.SelectedValue = editingMAKHOA;

                                // TẮT FLAG TRƯỚC KHI LOAD LỚP (để có thể set đúng giá trị lớp)
                                isLoadingData = false;

                                // Load lớp thủ công
                                LoadComboBoxLop(editingMAKHOA);

                                // RESET editingMALOP SAU KHI ĐÃ LOAD XONG
                                editingMALOP = "";

                                // Thông tin thân nhân
                                txtTEN_THANNHAN.Text = reader["TEN_THANNHAN"] != DBNull.Value ? reader["TEN_THANNHAN"].ToString() : "";
                                txtSDT_THANNHAN.Text = reader["SDT_THANNHAN"] != DBNull.Value ? reader["SDT_THANNHAN"].ToString() : "";
                                comMOIQUANHE.Text = reader["MOIQUANHE"] != DBNull.Value ? reader["MOIQUANHE"].ToString() : "";
                                txtDIACHI.Text = reader["DIACHI"] != DBNull.Value ? reader["DIACHI"].ToString() : "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin sinh viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ĐẢM BẢO TẮT FLAG SAU KHI LOAD XONG
                isLoadingData = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtMASV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMASV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTENSV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sinh viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENSV.Focus();
                return;
            }

            if (comTENLOP.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn lớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comTENLOP.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        if (isEditMode)
                        {
                            // ✅ LỖI 2: Nếu mã SV thay đổi, cần kiểm tra và cập nhật
                            string maSVMoi = txtMASV.Text.Trim();

                            // Kiểm tra nếu mã SV thay đổi
                            if (maSVMoi != editingMASV)
                            {
                                // Kiểm tra mã SV mới có tồn tại chưa
                                string checkQuery = "SELECT COUNT(*) FROM SINHVIEN WHERE MASV = @MASV";
                                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@MASV", maSVMoi);
                                    int count = (int)checkCmd.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("Mã sinh viên mới đã tồn tại!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                // Kiểm tra sinh viên cũ có hợp đồng không
                                string checkHDQuery = "SELECT COUNT(*) FROM HOPDONG WHERE MASV = @MASV";
                                using (SqlCommand checkHDCmd = new SqlCommand(checkHDQuery, conn, transaction))
                                {
                                    checkHDCmd.Parameters.AddWithValue("@MASV", editingMASV);
                                    int countHD = (int)checkHDCmd.ExecuteScalar();

                                    if (countHD > 0)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("Không thể sửa mã sinh viên vì đã có hợp đồng!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                // Xóa thân nhân cũ (nếu có)
                                string deleteTNQuery = "DELETE FROM THANNHAN WHERE MASV = @MASV_OLD";
                                using (SqlCommand deleteTNCmd = new SqlCommand(deleteTNQuery, conn, transaction))
                                {
                                    deleteTNCmd.Parameters.AddWithValue("@MASV_OLD", editingMASV);
                                    deleteTNCmd.ExecuteNonQuery();
                                }

                                // Xóa sinh viên cũ
                                string deleteSVQuery = "DELETE FROM SINHVIEN WHERE MASV = @MASV_OLD";
                                using (SqlCommand deleteSVCmd = new SqlCommand(deleteSVQuery, conn, transaction))
                                {
                                    deleteSVCmd.Parameters.AddWithValue("@MASV_OLD", editingMASV);
                                    deleteSVCmd.ExecuteNonQuery();
                                }

                                // Thêm sinh viên mới
                                string insertSVQuery = @"INSERT INTO SINHVIEN (MASV, TENSV, NGAYSINH, GIOITINH, CCCD, SDT, MALOP)
                                                        VALUES (@MASV, @TENSV, @NGAYSINH, @GIOITINH, @CCCD, @SDT, @MALOP)";
                                using (SqlCommand insertSVCmd = new SqlCommand(insertSVQuery, conn, transaction))
                                {
                                    insertSVCmd.Parameters.AddWithValue("@MASV", maSVMoi);
                                    insertSVCmd.Parameters.AddWithValue("@TENSV", txtTENSV.Text.Trim());
                                    insertSVCmd.Parameters.AddWithValue("@NGAYSINH", dtpNGAYSINH.Value);
                                    insertSVCmd.Parameters.AddWithValue("@GIOITINH", string.IsNullOrEmpty(comGIOITINH.Text) ? (object)DBNull.Value : comGIOITINH.Text);
                                    insertSVCmd.Parameters.AddWithValue("@CCCD", string.IsNullOrEmpty(txtCCCD.Text) ? (object)DBNull.Value : txtCCCD.Text.Trim());
                                    insertSVCmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim());
                                    insertSVCmd.Parameters.AddWithValue("@MALOP", comTENLOP.SelectedValue.ToString());
                                    insertSVCmd.ExecuteNonQuery();
                                }

                                // Thêm thân nhân mới (nếu có)
                                if (!string.IsNullOrWhiteSpace(txtTEN_THANNHAN.Text))
                                {
                                    string insertTNQuery = @"INSERT INTO THANNHAN (MASV, TEN_THANNHAN, SDT, MOIQUANHE, DIACHI)
                                                            VALUES (@MASV, @TEN_THANNHAN, @SDT, @MOIQUANHE, @DIACHI)";
                                    using (SqlCommand insertTNCmd = new SqlCommand(insertTNQuery, conn, transaction))
                                    {
                                        insertTNCmd.Parameters.AddWithValue("@MASV", maSVMoi);
                                        insertTNCmd.Parameters.AddWithValue("@TEN_THANNHAN", txtTEN_THANNHAN.Text.Trim());
                                        insertTNCmd.Parameters.AddWithValue("@SDT", txtSDT_THANNHAN.Text.Trim());
                                        insertTNCmd.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(comMOIQUANHE.Text) ? (object)DBNull.Value : comMOIQUANHE.Text);
                                        insertTNCmd.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(txtDIACHI.Text) ? (object)DBNull.Value : txtDIACHI.Text.Trim());
                                        insertTNCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                // Mã SV không đổi - cập nhật bình thường
                                string queryUpdateSV = @"UPDATE SINHVIEN 
                                                       SET TENSV = @TENSV,
                                                           NGAYSINH = @NGAYSINH,
                                                           GIOITINH = @GIOITINH,
                                                           CCCD = @CCCD,
                                                           SDT = @SDT,
                                                           MALOP = @MALOP
                                                       WHERE MASV = @MASV";

                                using (SqlCommand cmd = new SqlCommand(queryUpdateSV, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                    cmd.Parameters.AddWithValue("@TENSV", txtTENSV.Text.Trim());
                                    cmd.Parameters.AddWithValue("@NGAYSINH", dtpNGAYSINH.Value);
                                    cmd.Parameters.AddWithValue("@GIOITINH", string.IsNullOrEmpty(comGIOITINH.Text) ? (object)DBNull.Value : comGIOITINH.Text);
                                    cmd.Parameters.AddWithValue("@CCCD", string.IsNullOrEmpty(txtCCCD.Text) ? (object)DBNull.Value : txtCCCD.Text.Trim());
                                    cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim());
                                    cmd.Parameters.AddWithValue("@MALOP", comTENLOP.SelectedValue.ToString());
                                    cmd.ExecuteNonQuery();
                                }

                                // Cập nhật hoặc thêm thân nhân
                                if (!string.IsNullOrWhiteSpace(txtTEN_THANNHAN.Text))
                                {
                                    string checkTN = "SELECT COUNT(*) FROM THANNHAN WHERE MASV = @MASV";
                                    using (SqlCommand checkCmd = new SqlCommand(checkTN, conn, transaction))
                                    {
                                        checkCmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                        int countTN = (int)checkCmd.ExecuteScalar();

                                        if (countTN > 0)
                                        {
                                            string queryUpdateTN = @"UPDATE THANNHAN 
                                                                   SET TEN_THANNHAN = @TEN_THANNHAN,
                                                                       SDT = @SDT,
                                                                       MOIQUANHE = @MOIQUANHE,
                                                                       DIACHI = @DIACHI
                                                                   WHERE MASV = @MASV";
                                            using (SqlCommand cmdTN = new SqlCommand(queryUpdateTN, conn, transaction))
                                            {
                                                cmdTN.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                                cmdTN.Parameters.AddWithValue("@TEN_THANNHAN", txtTEN_THANNHAN.Text.Trim());
                                                cmdTN.Parameters.AddWithValue("@SDT", txtSDT_THANNHAN.Text.Trim());
                                                cmdTN.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(comMOIQUANHE.Text) ? (object)DBNull.Value : comMOIQUANHE.Text);
                                                cmdTN.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(txtDIACHI.Text) ? (object)DBNull.Value : txtDIACHI.Text.Trim());
                                                cmdTN.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            string queryInsertTN = @"INSERT INTO THANNHAN (MASV, TEN_THANNHAN, SDT, MOIQUANHE, DIACHI)
                                                                   VALUES (@MASV, @TEN_THANNHAN, @SDT, @MOIQUANHE, @DIACHI)";
                                            using (SqlCommand cmdTN = new SqlCommand(queryInsertTN, conn, transaction))
                                            {
                                                cmdTN.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                                cmdTN.Parameters.AddWithValue("@TEN_THANNHAN", txtTEN_THANNHAN.Text.Trim());
                                                cmdTN.Parameters.AddWithValue("@SDT", txtSDT_THANNHAN.Text.Trim());
                                                cmdTN.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(comMOIQUANHE.Text) ? (object)DBNull.Value : comMOIQUANHE.Text);
                                                cmdTN.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(txtDIACHI.Text) ? (object)DBNull.Value : txtDIACHI.Text.Trim());
                                                cmdTN.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Kiểm tra trùng mã
                            string checkQuery = "SELECT COUNT(*) FROM SINHVIEN WHERE MASV = @MASV";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                            {
                                checkCmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Mã sinh viên đã tồn tại!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Thêm mới sinh viên
                            string queryInsertSV = @"INSERT INTO SINHVIEN (MASV, TENSV, NGAYSINH, GIOITINH, CCCD, SDT, MALOP)
                                                   VALUES (@MASV, @TENSV, @NGAYSINH, @GIOITINH, @CCCD, @SDT, @MALOP)";

                            using (SqlCommand cmd = new SqlCommand(queryInsertSV, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                cmd.Parameters.AddWithValue("@TENSV", txtTENSV.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGAYSINH", dtpNGAYSINH.Value);
                                cmd.Parameters.AddWithValue("@GIOITINH", string.IsNullOrEmpty(comGIOITINH.Text) ? (object)DBNull.Value : comGIOITINH.Text);
                                cmd.Parameters.AddWithValue("@CCCD", string.IsNullOrEmpty(txtCCCD.Text) ? (object)DBNull.Value : txtCCCD.Text.Trim());
                                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim());
                                cmd.Parameters.AddWithValue("@MALOP", comTENLOP.SelectedValue.ToString());
                                cmd.ExecuteNonQuery();
                            }

                            // Thêm thân nhân nếu có
                            if (!string.IsNullOrWhiteSpace(txtTEN_THANNHAN.Text))
                            {
                                string queryInsertTN = @"INSERT INTO THANNHAN (MASV, TEN_THANNHAN, SDT, MOIQUANHE, DIACHI)
                                                       VALUES (@MASV, @TEN_THANNHAN, @SDT, @MOIQUANHE, @DIACHI)";

                                using (SqlCommand cmdTN = new SqlCommand(queryInsertTN, conn, transaction))
                                {
                                    cmdTN.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                    cmdTN.Parameters.AddWithValue("@TEN_THANNHAN", txtTEN_THANNHAN.Text.Trim());
                                    cmdTN.Parameters.AddWithValue("@SDT", txtSDT_THANNHAN.Text.Trim());
                                    cmdTN.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(comMOIQUANHE.Text) ? (object)DBNull.Value : comMOIQUANHE.Text);
                                    cmdTN.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(txtDIACHI.Text) ? (object)DBNull.Value : txtDIACHI.Text.Trim());
                                    cmdTN.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Thêm sinh viên mới thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}