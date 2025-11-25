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

namespace QuanLyKiTucXa.Formadd
{
    public partial class testadddata : Form
    {
        public testadddata()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;
        DataTable comdt = new DataTable();



        private void testadddata_Load(object sender, EventArgs e)
        {
            constr = "Data Source=LAPTOP-MGOO2M8J\\SQLEXPRESS07;Initial Catalog=KL_KTX;Integrated Security=True";
            conn.ConnectionString = constr;
            conn.Open();
            sql = "Select MA_PHONG, MANHA,TRANGTHAI From PHONG ";
            da = new SqlDataAdapter(sql, conn);
            dt.Clear();
            da.Fill(dt);
            //grdData.DataSource = dt;
            //grdData.Refresh();
            
        }
    }
}
