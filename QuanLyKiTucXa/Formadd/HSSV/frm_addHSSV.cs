using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.HSSV
{
    public partial class frm_addHSSV : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string editingMASV = "";

        public frm_addHSSV()
        {
            InitializeComponent();
            LoadComboBoxLop();

            // Disable các textbox không được nhập
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtTINHTRANG_CUTRU.ReadOnly = true;

            txtMA_PHONG.BackColor = SystemColors.Control;
            txtMANHA.BackColor = SystemColors.Control;
            txtTINHTRANG_CUTRU.BackColor = SystemColors.Control;
        }

        // Constructor cho chế độ sửa
        public frm_addHSSV(string maSV, string maPhong, string maNha, string tinhTrangCuTru)
        {
            InitializeComponent();
            LoadComboBoxLop();

            isEditMode = true;
            editingMASV = maSV;

            // Disable các textbox
            txtMA_PHONG.ReadOnly = true;
            txtMANHA.ReadOnly = true;
            txtTINHTRANG_CUTRU.ReadOnly = true;

            txtMA_PHONG.BackColor = SystemColors.Control;
            txtMANHA.BackColor = SystemColors.Control;
            txtTINHTRANG_CUTRU.BackColor = SystemColors.Control;

            // Hiển thị thông tin
            txtMA_PHONG.Text = maPhong;
            txtMANHA.Text = maNha;
            txtTINHTRANG_CUTRU.Text = tinhTrangCuTru;

            LoadSinhVien(maSV);

            this.Text = "Cập nhật thông tin sinh viên";
        }

        private void frm_addHSSV_Load(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                // Đặt giá trị mặc định
                txtTINHTRANG_CUTRU.Text = "Chưa có hợp đồng";
            }
        }

        private void LoadComboBoxLop()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT L.MALOP, L.TENLOP + ' - ' + K.TENKHOA AS DISPLAY_TEXT
                                   FROM LOP L
                                   INNER JOIN KHOA K ON L.MAKHOA = K.MAKHOA
                                   ORDER BY L.MALOP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comTENLOP.DataSource = dt;
                        comTENLOP.DisplayMember = "DISPLAY_TEXT";
                        comTENLOP.ValueMember = "MALOP";
                        comTENLOP.SelectedIndex = -1;
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT SV.MASV, SV.TENSV, SV.NGAYSINH, SV.GIOITINH, 
                                          SV.CCCD, SV.SDT, SV.MALOP, SV.DKTOTNGHIEP,
                                          TN.TEN_THANNHAN, TN.SDT AS SDT_THANNHAN, 
                                          TN.MOIQUANHE, TN.DIACHI
                                   FROM SINHVIEN SV
                                   LEFT JOIN THANNHAN TN ON SV.MASV = TN.MASV
                                   WHERE SV.MASV = @MASV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MASV", maSV);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtMASV.Text = reader["MASV"].ToString();
                                txtMASV.ReadOnly = true; // Không cho sửa mã SV

                                txtTENSV.Text = reader["TENSV"].ToString();

                                if (reader["NGAYSINH"] != DBNull.Value)
                                    dtpNGAYSINH.Value = Convert.ToDateTime(reader["NGAYSINH"]);

                                comGIOITINH.Text = reader["GIOITINH"] != DBNull.Value ? reader["GIOITINH"].ToString() : "";
                                txtCCCD.Text = reader["CCCD"] != DBNull.Value ? reader["CCCD"].ToString() : "";
                                txtSDT.Text = reader["SDT"] != DBNull.Value ? reader["SDT"].ToString() : "";

                                comTENLOP.SelectedValue = reader["MALOP"].ToString();

                                //if (reader["DKTOTNGHIEP"] != DBNull.Value)
                                //    dtpDKTOTNGHIEP.Value = Convert.ToDateTime(reader["DKTOTNGHIEP"]);

                                // Thông tin thân nhân
                                txtTEN_THANNHAN.Text = reader["TEN_THANNHAN"] != DBNull.Value ? reader["TEN_THANNHAN"].ToString() : "";
                                txtSDT_THANNHAN.Text = reader["SDT_THANNHAN"] != DBNull.Value ? reader["SDT_THANNHAN"].ToString() : "";
                                txtMOIQUANHE.Text = reader["MOIQUANHE"] != DBNull.Value ? reader["MOIQUANHE"].ToString() : "";
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
                            // Cập nhật sinh viên
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
                               // cmd.Parameters.AddWithValue("@DKTOTNGHIEP", dtpDKTOTNGHIEP.Value);
                                cmd.ExecuteNonQuery();
                            }

                            // Cập nhật hoặc thêm thân nhân
                            if (!string.IsNullOrWhiteSpace(txtTEN_THANNHAN.Text))
                            {
                                // Kiểm tra đã có thân nhân chưa
                                string checkTN = "SELECT COUNT(*) FROM THANNHAN WHERE MASV = @MASV";
                                using (SqlCommand checkCmd = new SqlCommand(checkTN, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                    int countTN = (int)checkCmd.ExecuteScalar();

                                    if (countTN > 0)
                                    {
                                        // Update
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
                                            cmdTN.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(txtMOIQUANHE.Text) ? (object)DBNull.Value : txtMOIQUANHE.Text.Trim());
                                            cmdTN.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(txtDIACHI.Text) ? (object)DBNull.Value : txtDIACHI.Text.Trim());
                                            cmdTN.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        // Insert
                                        string queryInsertTN = @"INSERT INTO THANNHAN (MASV, TEN_THANNHAN, SDT, MOIQUANHE, DIACHI)
                                                               VALUES (@MASV, @TEN_THANNHAN, @SDT, @MOIQUANHE, @DIACHI)";
                                        using (SqlCommand cmdTN = new SqlCommand(queryInsertTN, conn, transaction))
                                        {
                                            cmdTN.Parameters.AddWithValue("@MASV", txtMASV.Text.Trim());
                                            cmdTN.Parameters.AddWithValue("@TEN_THANNHAN", txtTEN_THANNHAN.Text.Trim());
                                            cmdTN.Parameters.AddWithValue("@SDT", txtSDT_THANNHAN.Text.Trim());
                                            cmdTN.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(txtMOIQUANHE.Text) ? (object)DBNull.Value : txtMOIQUANHE.Text.Trim());
                                            cmdTN.Parameters.AddWithValue("@DIACHI", string.IsNullOrEmpty(txtDIACHI.Text) ? (object)DBNull.Value : txtDIACHI.Text.Trim());
                                            cmdTN.ExecuteNonQuery();
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
                               // cmd.Parameters.AddWithValue("@DKTOTNGHIEP", dtpDKTOTNGHIEP.Value);
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
                                    cmdTN.Parameters.AddWithValue("@MOIQUANHE", string.IsNullOrEmpty(txtMOIQUANHE.Text) ? (object)DBNull.Value : txtMOIQUANHE.Text.Trim());
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