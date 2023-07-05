using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.SqlServer.Management.Dmf.ExpressionNodeFunction;

namespace frmMain.Class
{
    class DbConnection
    {
        //Khai báo các biến toàn cục
        //Chuỗi kết nối đến CSDL
        static readonly String strCon = Properties.Settings.Default.DBString;
        
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

            if(tbl.Rows.Count > 0)
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
            catch(Exception ex)
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
            catch(Exception e)
            {
                MessageBox.Show("Truy vấn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(e.Message);
            }

            sqlCmd.Dispose();

            return reader;
        }

        public static void FillCombo(string sql, ComboBox cb, string ma, string ten)
        {
            OpenConnection();
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand(sql);
            dap.SelectCommand.Connection = sqlCon;
            DataTable table = new DataTable();

            dap.Fill(table);
            cb.DataSource = table;
            cb.ValueMember = ten; //Trường giá trị
            cb.DisplayMember = ma; //Trường hiển thị
            CloseConnection();
        }
        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            //Xóa các dấu "," nếu có
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // trừ 1 vì thứ tự đi từ 0
            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) // Chữ số cuối cùng không cần xét tiếp break; 
                    switch ((mLen - i) % 9)
                    {
                        case 0:
                            mTemp = mTemp + " tỷ";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 6:
                            mTemp = mTemp + " triệu";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 3:
                            mTemp = mTemp + " nghìn";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        default:
                            switch ((mLen - i) % 3)
                            {
                                case 2:
                                    mTemp = mTemp + " trăm";
                                    break;
                                case 1:
                                    mTemp = mTemp + " mươi";
                                    break;
                            }
                            break;
                    }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", ""); //Loại bỏ trường hợp 00x 
            mTemp = mTemp.Replace("không mươi ", "linh "); //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";
            return mTemp;
        }

        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }
    }
}
