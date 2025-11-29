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
    public partial class UC_ThuePhong : UserControl
    {

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql , constr;
        DataTable comdt = new DataTable();
        public UC_ThuePhong()
        {
            InitializeComponent();
        }
        
        private void UC_ThuePhong_Load(object sender, EventArgs e)
        {
            //Khong sua, header no bi loi stytle
            dgvThuePhong.ColumnHeadersDefaultCellStyle.Font = new Font(dgvThuePhong.Font, FontStyle.Bold);

            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";

            conn.ConnectionString = constr;
            conn.Open();
            
            //sql = "Select MAHD, TENHD, sv.MASV,TENSV, NGAYKY,nv.MANV, nv. TENNV, NGAYKTTT, sv. GIOITINH, " +
            //    "hd. MA_PHONG, n. MANHA, LOAIPHONG, hd. DONGIA, TONGTIEN, TUNGAY, DENNGAY, THOIHAN " +
            //    " from SINHVIEN sv " +
            //    " LEFT JOIN HOPDONG hd ON sv.MASV = hd. MASV" +
            //    " JOIN PHONG p on hd.MA_PHONG = p.MA_PHONG" +
            //    " JOIN NHA n on p.MANHA = n.MANHA " +
            //    " JOIN NHANVIEN nv on hd. MANV = nv. MANV" +
            //    " ORDER BY MAHD";
            //da = new SqlDataAdapter(sql, conn);
            //dt.Clear();
            //da.Fill(dt);
            //dgvThuePhong.DataSource = dt;
            //dgvThuePhong.Refresh();
            LoadData();
          

        }

        private void btn_AddHopDong_Click(object sender, EventArgs e)
        {

            frm_addHopDong frm = new frm_addHopDong();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Refresh lại danh sách sau khi thêm thành công
                LoadData();
            }

        }

        private void dgvThuePhong_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Vẽ số thứ tự ở cột đầu tiên
            dgvThuePhong.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void LoadData()
        {
            try
            {
                sql = 
                    "Select MAHD ,TENHD,sv.MASV,TENSV, sv.GIOITINH , n.MANHA, hd.MA_PHONG , n.LOAIPHONG, hd.TUNGAY , hd.DENNGAY , hd.NGAYKTTT, hd.THOIHAN , hd. DONGIA, hd. TONGTIEN, hd.MANV, nv.TENNV, hd.NGAYKY"+
                    " from SINHVIEN sv " +
                    " LEFT JOIN HOPDONG hd ON sv.MASV = hd.MASV" +
                    " JOIN PHONG p on hd.MA_PHONG = p.MA_PHONG" +
                    " JOIN NHA n on p.MANHA = n.MANHA " +
                    " LEFT JOIN NHANVIEN nv on hd.MANV = nv.MANV" +
                    " ORDER BY MAHD";

                da = new SqlDataAdapter(sql, conn);
                dt.Clear();
                da.Fill(dt);
                dgvThuePhong.DataSource = dt;
                dgvThuePhong.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
