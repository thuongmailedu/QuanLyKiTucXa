using QuanLyKiTucXa.Formadd.QLHD_FORM;
using QuanLyKiTucXa.Formadd.QLPHONG_FORM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Main_UC.QLPHONG
{
    public partial class UC_DANHMUCPHONG : UserControl
    {
        public UC_DANHMUCPHONG()
        {
            InitializeComponent();
            ThemChuGiaiMau();
        }

        private string connectionString = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";


        private void UC_DANHMUCPHONG_Load(object sender, EventArgs e)
        {
            dgvDMPhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDMPhong.Font, FontStyle.Bold);
            dgvTinhTrangPhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvTinhTrangPhong.Font, FontStyle.Bold);

            // Thiết lập cho phép chọn nhiều ô
            dgvTinhTrangPhong.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvTinhTrangPhong.MultiSelect = true;

            numericUpDownNam.Minimum = 1900;
            numericUpDownNam.Maximum = 2100;
            numericUpDownNam.Value = DateTime.Now.Year;

            numericUpDownNam.ValueChanged += numericUpDownNam_ValueChanged;
            comboBoxNHA.SelectedIndexChanged += comboBoxNHA_SelectedIndexChanged;

            // Đăng ký event cho CellFormatting để tô màu
            dgvTinhTrangPhong.CellFormatting += dgvTinhTrangPhong_CellFormatting;

            LoadComboBoxNHA();
            LoadDuLieuPhong();

        }

        private void btn_addPhong_Click(object sender, EventArgs e)
        {
            frm_DM_PHONG form = new frm_DM_PHONG(); // Tạo instance
            form.ShowDialog();
        }
        private void LoadComboBoxNHA()
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

                    // Thêm dòng "Tất cả" vào đầu
                    DataRow newRow = dt.NewRow();
                    newRow["MANHA"] = DBNull.Value;
                    newRow["DISPLAY_TEXT"] = "-- Tất cả nhà --";
                    dt.Rows.InsertAt(newRow, 0);

                    comboBoxNHA.DataSource = dt;
                    comboBoxNHA.DisplayMember = "DISPLAY_TEXT";
                    comboBoxNHA.ValueMember = "MANHA";
                    comboBoxNHA.SelectedIndex = 0;
                }

            }
        }
            private void LoadDuLieuPhong()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ThongKeSinhVienTheoPhongVaThang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm parameters
                        cmd.Parameters.AddWithValue("@Nam", (int)numericUpDownNam.Value);

                        // Kiểm tra nếu chọn "Tất cả" thì truyền NULL
                        if (comboBoxNHA.SelectedValue == DBNull.Value || comboBoxNHA.SelectedValue == null)
                        {
                            cmd.Parameters.AddWithValue("@MaNha", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MaNha", comboBoxNHA.SelectedValue.ToString());
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvTinhTrangPhong.DataSource = dt;

                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void comboBoxNHA_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuPhong();
        }
        private void numericUpDownNam_ValueChanged(object sender, EventArgs e)
        {
            LoadDuLieuPhong();
        }

        

private void dgvTinhTrangPhong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        try
        {
            string columnName = dgvTinhTrangPhong.Columns[e.ColumnIndex].Name.Trim();

            // Regex: chỉ match T theo sau bởi 1 hoặc 2 chữ số (1-12)
            Regex monthColumnRegex = new Regex(@"^T([1-9]|1[0-2])$", RegexOptions.IgnoreCase);

            if (monthColumnRegex.IsMatch(columnName))
            {
                int thang = int.Parse(columnName.Substring(1));

                int namLoc = (int)numericUpDownNam.Value;
                int thangHienTai = DateTime.Now.Month;
                int namHienTai = DateTime.Now.Year;

                int soLuongSV = 0;
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    int.TryParse(e.Value.ToString(), out soLuongSV);
                }

                int toiDa = 0;
                if (dgvTinhTrangPhong.Columns.Contains("TOIDA") &&
                    dgvTinhTrangPhong.Rows[e.RowIndex].Cells["TOIDA"].Value != null)
                {
                    int.TryParse(dgvTinhTrangPhong.Rows[e.RowIndex].Cells["TOIDA"].Value.ToString(), out toiDa);
                }

                bool isDaQua = (namLoc < namHienTai) || (namLoc == namHienTai && thang < thangHienTai);

                if (isDaQua)
                {
                    e.CellStyle.BackColor = Color.LightGray;
                    e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionBackColor = Color.White;
                        e.CellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    if (soLuongSV == toiDa && toiDa > 0)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.SelectionBackColor = Color.White;
                        e.CellStyle.SelectionForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvTinhTrangPhong.Font, FontStyle.Bold);
                    }
                    else if (soLuongSV >= 0 && soLuongSV < toiDa)
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionBackColor = Color.White;
                        e.CellStyle.SelectionForeColor = Color.DarkGreen;
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in CellFormatting: " + ex.Message);
        }
    }
    private void ThemChuGiaiMau()
        {
            // Tạo panel chú giải
            Panel legendPanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Bottom,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Xám - Đã qua
            Label lblGray = new Label { Text = "■ Đã qua", ForeColor = Color.Gray, AutoSize = true, Location = new Point(10, 7) };

            // Đỏ - Full
            Label lblRed = new Label { Text = "■ Đầy", ForeColor = Color.Red, AutoSize = true, Location = new Point(100, 7) };

            // Xanh - Còn chỗ
            Label lblGreen = new Label { Text = "■ Còn chỗ", ForeColor = Color.Green, AutoSize = true, Location = new Point(180, 7) };

            legendPanel.Controls.AddRange(new Control[] { lblGray, lblRed, lblGreen });
            this.Controls.Add(legendPanel);
        }

        private void btn_addHD_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có ô nào được chọn không
                if (dgvTinhTrangPhong.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn các tháng để tạo hợp đồng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy danh sách các cell được chọn
                var selectedCells = dgvTinhTrangPhong.SelectedCells.Cast<DataGridViewCell>().ToList();

                // 1. Kiểm tra tất cả các ô phải cùng 1 hàng
                int rowIndex = selectedCells[0].RowIndex;
                if (selectedCells.Any(cell => cell.RowIndex != rowIndex))
                {
                    MessageBox.Show("Vui lòng chỉ chọn các ô trên cùng một hàng!", "Lỗi thao tác",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Kiểm tra các ô được chọn phải thuộc cột T1-T12
                string[] validMonthColumns = { "T1", "T2", "T3", "T4", "T5", "T6",
                                       "T7", "T8", "T9", "T10", "T11", "T12" };

                var selectedMonthCells = selectedCells
                    .Where(cell => validMonthColumns.Contains(dgvTinhTrangPhong.Columns[cell.ColumnIndex].Name))
                    .ToList();

                if (selectedMonthCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn các tháng (T1-T12) để tạo hợp đồng!", "Lỗi thao tác",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3. Lấy danh sách các tháng được chọn
                List<int> selectedMonths = new List<int>();
                foreach (var cell in selectedMonthCells)
                {
                    string columnName = dgvTinhTrangPhong.Columns[cell.ColumnIndex].Name;
                    int month = int.Parse(columnName.Substring(1));
                    selectedMonths.Add(month);
                }
                selectedMonths = selectedMonths.OrderBy(m => m).ToList();

                // 4. Kiểm tra các tháng phải liên tiếp
                for (int i = 0; i < selectedMonths.Count - 1; i++)
                {
                    if (selectedMonths[i + 1] - selectedMonths[i] != 1)
                    {
                        MessageBox.Show("Vui lòng chọn các tháng liên tiếp nhau!", "Lỗi thao tác",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 5. Kiểm tra các tháng phải là hiện tại hoặc tương lai
                int namLoc = (int)numericUpDownNam.Value;
                int thangHienTai = DateTime.Now.Month;
                int namHienTai = DateTime.Now.Year;

                int thangNhoNhat = selectedMonths.Min();

                if (namLoc < namHienTai || (namLoc == namHienTai && thangNhoNhat < thangHienTai))
                {
                    MessageBox.Show("Không thể tạo hợp đồng cho các tháng đã qua!", "Lỗi thao tác",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 6. Kiểm tra tình trạng phòng - không được chọn phòng đã đầy
                DataGridViewRow selectedRow = dgvTinhTrangPhong.Rows[rowIndex];
                int toiDa = Convert.ToInt32(selectedRow.Cells["TOIDA"].Value);

                foreach (int month in selectedMonths)
                {
                    string colName = "T" + month;
                    var cellValue = selectedRow.Cells[colName].Value;
                    int soLuongSV = (cellValue != null && cellValue != DBNull.Value)
                        ? Convert.ToInt32(cellValue) : 0;

                    if (soLuongSV >= toiDa)
                    {
                        MessageBox.Show($"Phòng đã đạt tối đa vào tháng {month}. Vui lòng chọn phòng khác!",
                            "Phòng đã đầy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 7. Lấy thông tin phòng từ hàng được chọn
                string maPhong = selectedRow.Cells["MA_PHONG"].Value.ToString();
                string maNha = selectedRow.Cells["MANHA"].Value.ToString();

                // Lấy thông tin chi tiết từ database (LOAIPHONG, GIAPHONG, GIOITINH)
                var thongTinPhong = LayThongTinPhong(maNha);
                if (thongTinPhong == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 8. Tính toán thời gian hợp đồng
                int thangLonNhat = selectedMonths.Max();
                DateTime tuNgay = new DateTime(namLoc, thangNhoNhat, 1);
                DateTime denNgay = new DateTime(namLoc, thangLonNhat, DateTime.DaysInMonth(namLoc, thangLonNhat));
                int thoiHan = selectedMonths.Count; // Số tháng
                decimal tongTien = thongTinPhong.GiaPhong * thoiHan;

                // 9. Mở form thêm hợp đồng và truyền dữ liệu
                frm_addHopDong frmAdd = new frm_addHopDong();
                frmAdd.SetThongTinPhong(maPhong, maNha, thongTinPhong.LoaiPhong,
                    thongTinPhong.GiaPhong, tuNgay, denNgay, thoiHan, tongTien);

                if (frmAdd.ShowDialog() == DialogResult.OK)
                {
                    // Refresh lại dữ liệu sau khi thêm hợp đồng thành công
                    LoadDuLieuPhong();
                    MessageBox.Show("Thêm hợp đồng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Class để lưu thông tin phòng
        public class ThongTinPhong
        {
            public string LoaiPhong { get; set; }
            public string GioiTinh { get; set; }
            public decimal GiaPhong { get; set; }
            public int ToiDa { get; set; }
        }

        // Hàm lấy thông tin chi tiết phòng từ database
        private ThongTinPhong LayThongTinPhong(string maNha)
        {
            ThongTinPhong thongTin = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT LOAIPHONG, GIOITINH, GIAPHONG, TOIDA 
                           FROM NHA 
                           WHERE MANHA = @MANHA";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MANHA", maNha);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                thongTin = new ThongTinPhong
                                {
                                    LoaiPhong = reader["LOAIPHONG"].ToString(),
                                    GioiTinh = reader["GIOITINH"].ToString(),
                                    GiaPhong = Convert.ToDecimal(reader["GIAPHONG"]),
                                    ToiDa = Convert.ToInt32(reader["TOIDA"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin phòng: " + ex.Message);
            }
            return thongTin;
        }
    }
}
