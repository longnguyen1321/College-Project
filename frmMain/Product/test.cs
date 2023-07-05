using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.Product
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Mở kết nối
            Class.DbConnection.OpenConnection();

            //Thực hiện truy vấn
            DataTable tblProduct = Class.DbConnection.GetDataToTable("select MaMH, TenMH, GiaMH,SoLuongMH, LoaiMH, SizeMH, ChatLieuMH from MatHang");

            //Chọn nguồn dữ liệu cho bảng DGV
            //dgvProduct.DataSource = tblProduct;
        }
    }
}
