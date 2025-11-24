using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Formadd.QLPHONG_FORM
{
    public partial class frm_DM_NHA : Form
    {
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
        private bool isEditMode = false;
        private string oldMANHA = "";

        public frm_DM_NHA()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        // Constructor cho chế độ sửa
        public frm_DM_NHA(string maNha, string loaiPhong, string gioiTinh, decimal giaPhong, int toiDa)
        {
            InitializeComponent();
            LoadComboBoxData();

            isEditMode = true;
            oldMANHA = maNha;

            txtMANHA.Text = maNha;
            txtMANHA.ReadOnly = true; // Không cho sửa mã nhà
            comLOAIPHONG.Text = loaiPhong;
            comGIOITINH.Text = gioiTinh;
            txtGIAPHONG.Text = giaPhong.ToString();
            txtTOIDA.Text = toiDa.ToString();

            this.Text = "Cập nhật thông tin nhà";
        }

        private void frm_DM_NHA_Load(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                GenerateMANHA();
            }
        }

        private void LoadComboBoxData()
        {
            // Load combobox Giới tính
            comGIOITINH.Items.Clear();
            comGIOITINH.Items.Add("Nam");
            comGIOITINH.Items.Add("Nữ");

            // Load combobox Loại phòng
            comLOAIPHONG.Items.Clear();
            comLOAIPHONG.Items.Add("TC"); // Tiêu chuẩn
            comLOAIPHONG.Items.Add("CLC"); // Chất lượng cao
        }

        private void GenerateMANHA()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 MANHA FROM NHA WHERE MANHA LIKE 'N%' ORDER BY MANHA DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string lastMANHA = result.ToString();
                            // Lấy số từ mã nhà cuối cùng (vd: N01 -> 01)
                            string numberPart = lastMANHA.Substring(1);
                            int nextNumber = int.Parse(numberPart) + 1;

                            // Tạo mã mới với format N + số 1 chữ số
                            txtMANHA.Text = "N" + nextNumber.ToString("D1");
                        }
                        else
                        {
                            // Nếu chưa có nhà nào, bắt đầu từ N01
                            txtMANHA.Text = "N1";
                        }
                    }
                }

                txtMANHA.ReadOnly = true; // Không cho phép sửa mã nhà
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã nhà: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(txtMANHA.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhà!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMANHA.Focus();
                return;
            }

            if (comLOAIPHONG.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comLOAIPHONG.Focus();
                return;
            }

            if (comGIOITINH.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comGIOITINH.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGIAPHONG.Text))
            {
                MessageBox.Show("Vui lòng nhập giá phòng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGIAPHONG.Focus();
                return;
            }

            if (!decimal.TryParse(txtGIAPHONG.Text, out decimal giaPhong) || giaPhong <= 0)
            {
                MessageBox.Show("Giá phòng phải là số dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGIAPHONG.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTOIDA.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng tối đa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTOIDA.Focus();
                return;
            }

            if (!int.TryParse(txtTOIDA.Text, out int toiDa) || toiDa <= 0)
            {
                MessageBox.Show("Số lượng tối đa phải là số nguyên dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTOIDA.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (isEditMode)
                    {
                        // Cập nhật
                        string query = @"UPDATE NHA 
                                       SET LOAIPHONG = @LOAIPHONG,
                                           GIOITINH = @GIOITINH,
                                           GIAPHONG = @GIAPHONG,
                                           TOIDA = @TOIDA,
                                           TRANGTHAI = @TRANGTHAI
                                       WHERE MANHA = @MANHA";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MANHA", txtMANHA.Text.Trim());
                            cmd.Parameters.AddWithValue("@LOAIPHONG", comLOAIPHONG.Text);
                            cmd.Parameters.AddWithValue("@GIOITINH", comGIOITINH.Text);
                            cmd.Parameters.AddWithValue("@GIAPHONG", giaPhong);
                            cmd.Parameters.AddWithValue("@TOIDA", toiDa);
                            cmd.Parameters.AddWithValue("@TRANGTHAI", "Đang hoạt động");

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Cập nhật thông tin nhà thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        // Kiểm tra trùng mã
                        string checkQuery = "SELECT COUNT(*) FROM NHA WHERE MANHA = @MANHA";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@MANHA", txtMANHA.Text.Trim());
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Mã nhà đã tồn tại!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Thêm mới
                        string query = @"INSERT INTO NHA (MANHA, LOAIPHONG, GIOITINH, GIAPHONG, TOIDA)
                                       VALUES (@MANHA, @LOAIPHONG, @GIOITINH, @GIAPHONG, @TOIDA)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MANHA", txtMANHA.Text.Trim());
                            cmd.Parameters.AddWithValue("@LOAIPHONG", comLOAIPHONG.Text);
                            cmd.Parameters.AddWithValue("@GIOITINH", comGIOITINH.Text);
                            cmd.Parameters.AddWithValue("@GIAPHONG", giaPhong);
                            cmd.Parameters.AddWithValue("@TOIDA", toiDa);
                         //   cmd.Parameters.AddWithValue("@TRANGTHAI", "Đang hoạt động");

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Thêm nhà mới thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
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