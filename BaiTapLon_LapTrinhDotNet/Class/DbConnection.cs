using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon_LapTrinhDotNet.Class
{
    internal class DbConnection
    {
        //Khai báo các biến toàn cục
        //Chuỗi kết nối đến CSDL
        static readonly String strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\LT.NET\BTL\BaiTapLon_LapTrinhDotNet\QuanLyBanQuanAoNam.mdf;Integrated Security=True";

        //Đối tượng kết nối
        static SqlConnection sqlCon = null;

        public static void OpenConnection()
        {
            //Nếu rỗng thì tạo đối tượng kết nối và truyền chuỗi kết nối
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection(strCon);
            }
            //Nếu đóng thì mở
            if (sqlCon.State != ConnectionState.Open)
            {
                sqlCon.Open();
            }
        }

        //Hàm đóng kết nối
        public static void CloseConnection()
        {
            //Nếu chuỗi kết nối khác rỗng và đang mở thì đóng
            if (sqlCon.State == ConnectionState.Open && sqlCon != null)
            {
                sqlCon.Close();//Đóng kết nối
                sqlCon.Dispose();//Giải phóng tài nguyên
                sqlCon = null;
            }
        }

        //Hàm lấy dữ liệu vào bảng và trả về bảng
        public static DataTable GetDataToTable(String query)
        {
            //Kết nối đến CSDL
            DbConnection.OpenConnection();

            //Khơi tạo DataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Khởi tạo bảng dữ liệu
            DataTable dtTable = new DataTable();

            //Khởi tạo đối tượng thuộc chứa câu lênh truy, truyền câu lệnh truy vấn và chuỗi kết nối
            adapter.SelectCommand = new SqlCommand(query, sqlCon);

            //Gửi dữ liệu truy vấn được vào bảng dữ liệu
            adapter.Fill(dtTable);

            //Trả về bảng dữ liệu
            return dtTable;
        }

        //Hàm update CSDL qua DataTable
        public static int UpdateDBThroughDTable(DataTable tbl, String query)
        {
            int affectedRows = 0;

            DbConnection.OpenConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);

            //Khởi tạo đối tượng thực hiện các lệnh Insert, Delete, Update được thực hiện trên DataTable lên CSDL
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            //Kiểm tra số dòng được Update trong CSDL
            affectedRows = adapter.Update(tbl);

            return affectedRows;
        }

        //Hàm kiểm tra khóa có bị trùng
        public static bool CheckKey(String query)
        {
            bool check = false;
            DataTable tbl;

            OpenConnection();

            tbl = GetDataToTable(query);

            if (tbl.Rows.Count > 0)
                check = true;

            return check;
        }

        //Hàm thực thi truy vấn không có dữ liệu trả về
        public static void ExecuteSql(String sql)
        {
            //Mở kết nối
            OpenConnection();

            //Khởi tạo đối tượng thực thi truy vấn, truyền vào câu truy vấn và chuỗi kết nối
            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);

            //Thử thực thi truy vấn
            try
            {
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //Giải phóng bộ nhớ
            sqlCmd.Dispose();
            sqlCmd = null;
        }

        //Hàm thực thi truy vấn có dữ liệu trả về
        public static SqlDataReader _ExecuteReader(String sql)
        {
            OpenConnection();

            SqlDataReader reader = null;

            //Khởi tạo đối tượng thực thi truy vấn, truyền vào câu truy vấn và chuỗi kết nối
            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);

            //Thử thực thi truy vấn
            try
            {
                reader = sqlCmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show("Truy vấn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(e.Message);
            }

            sqlCmd.Dispose();

            return reader;
        }
    }
}
