using QuanLyKiTucXa.Formadd.HSSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKiTucXa.Ribbons
{
    public partial class UC_HOSOSINHVIEN_Ribbon : UserControl
    {
        public UC_HOSOSINHVIEN_Ribbon()
        {
            InitializeComponent();
        }


        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        DataTable comdt = new DataTable();



        private void UC_HOSOSINHVIEN_Ribbon_Load(object sender, EventArgs e)
        {
            //Khong sua, header no bi loi stytle
            grdData.ColumnHeadersDefaultCellStyle.Font = new Font(grdData.Font, FontStyle.Bold);
            grdData.AutoGenerateColumns = false;

            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
            //conn.ConnectionString = constr;
            //conn.Open();
            //sql = "Select MASV, TENSV, MA_PHONG, MANHA , TTCUTRU FROM SINHVIEN  "
            //      + " JOIN PHONG ON SINHVIEN.MA_PHONG = PHONG.MA_PHONG "
            //      + " JOIN NHA ON PHONG.MANHA = NHA.MANHA ";
            //da = new SqlDataAdapter(sql, conn);
            //dt.Clear();
            //da.Fill(dt);
            //grdData.DataSource = dt;
            //grdData.Refresh();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Tạo SqlCommand gọi thủ tục
                SqlCommand cmd = new SqlCommand("[dbo].[sp_DanhSachHoSoSinhVien]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm tham số đầu vào


                // Tạo DataAdapter để đổ dữ liệu vào DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Gán nguồn dữ liệu cho DataGridView
                grdData.DataSource = dt;
            }
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grbThongTinSinhVien_Enter(object sender, EventArgs e)
        {

        }

        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Vẽ số thứ tự ở cột đầu tiên
            grdData.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void btn_addHSSV_Click(object sender, EventArgs e)
        {
            frm_addHSSV form = new frm_addHSSV(); // Tạo instance
            form.ShowDialog();
        }
    }
}
