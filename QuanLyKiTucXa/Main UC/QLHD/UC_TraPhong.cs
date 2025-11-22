using QuanLyKiTucXa.Formadd.QLHD_FORM;
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

namespace QuanLyKiTucXa.Main_UC.QLHD
{
    public partial class UC_TraPhong : UserControl
    {
        public UC_TraPhong()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        DataTable comdt = new DataTable();


        private void UC_TraPhong_Load(object sender, EventArgs e)
        {
            //Khong sua, header no bi loi stytle
            dgvTraPhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvTraPhong.Font, FontStyle.Bold);


            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

            conn.ConnectionString = constr;
            conn.Open();
            
            sql = "select hd. MAHD, TENHD, sv.MASV, TENSV, sv.GIOITINH, p. MA_PHONG, hd. MANV, n. MANHA, n.LOAIPHONG, hd.TUNGAY, NGAYKTTT, NGAYKY, TENNV\r\n" +
                " FROM SINHVIEN sv \r\n" +
                " LEFT JOIN HOPDONG hd on sv. MASV = hd. MASV\r\n" +
                " JOIN PHONG p on hd. MA_PHONG = p. MA_PHONG\r\n" +
                " JOIN NHA n on p. MANHA = n. MANHA\r\n" +
                " JOIN NHANVIEN nv on nv. MANV = hd. MANV " +
                "WHERE NGAYKTTT IS NOT NULL";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            dgvTraPhong.DataSource = dt;
            dgvTraPhong.Refresh();


        }

        private void btn_Traphong_Click(object sender, EventArgs e)
        {
            frm_Traphong form = new frm_Traphong(); // Tạo instance
            form.ShowDialog();
        }
    }
}
