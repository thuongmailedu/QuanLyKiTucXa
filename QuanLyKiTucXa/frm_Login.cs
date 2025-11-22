using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKiTucXa
{
    public partial class frm_Login : Form
    {
        // Thay đổi connection string theo cấu hình của bạn
        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

        public frm_Login()
        {
            InitializeComponent();

            // Thiết lập PasswordChar cho textbox mật khẩu
            txtMATKHAU.PasswordChar = '●';

            // Thêm sự kiện Enter để đăng nhập
            txtMATKHAU.KeyPress += TxtMATKHAU_KeyPress;
        }

        private void TxtMATKHAU_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Nhấn Enter để đăng nhập
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtTENDN.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTENDN.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMATKHAU.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMATKHAU.Focus();
                return;
            }

            // Thực hiện đăng nhập
            DangNhap(txtTENDN.Text.Trim(), txtMATKHAU.Text);
        }

        private void DangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT ID, TENDN, QUYEN 
                                   FROM LOGIN 
                                   WHERE TENDN = @TenDN AND MATKHAU = @MatKhau";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDN", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Đăng nhập thành công
                                string id = reader["ID"].ToString();
                                string tenDN = reader["TENDN"].ToString();
                                string quyen = reader["QUYEN"].ToString();

                                // Lưu thông tin vào session
                                UserSession.Login(id, tenDN, quyen);

                                MessageBox.Show($"Đăng nhập thành công!\nXin chào {tenDN}!",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                // Ẩn form đăng nhập
                                this.Hide();

                                // Mở form chính (thay đổi tên form theo dự án của bạn)
                                frmLayout frmMain = new frmLayout();
                                frmMain.ShowDialog();

                                // Sau khi đóng form chính, đăng xuất và hiện lại form login
                                UserSession.Logout();
                                txtTENDN.Clear();
                                txtMATKHAU.Clear();
                                txtTENDN.Focus();
                                this.Show();
                            }
                            else
                            {
                                // Đăng nhập thất bại
                                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!",
                                    "Lỗi đăng nhập",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                txtMATKHAU.Clear();
                                txtTENDN.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {

        }
    }
}