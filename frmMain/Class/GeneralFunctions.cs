using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.Class
{
    internal class GeneralFunctions
    {
        //Hàm tạo 1 chuỗi random l kí tự, ngăn cách giữa Split kí tự
        public String RandomizeChar(int l, int split)
        {
            String result = "";

            try
            {
                //Tạo mảng sâu có độ dài l
                String[] rdCharArray = new string[l];
                
                //Bắt đầu random
                Random rd = new Random();
                for (int i = 0; i < l; i++)
                {
                    rdCharArray[i] = Convert.ToString((char)rd.Next(65, 90)); //Chữ in hoa
                    result += rdCharArray[i].ToString();

                    //Nếu là kí tự cuối hoặc tham số split <= 0 thì thì không thêm ngăn cách
                    if ((split > 0) && ((i+1) % split) == 0 && ((i+1) % l) != 0)
                    {
                        result += "-";                      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        //Hàm chuyển DL kiểu String sang Decimal
        public Decimal stringToDecimal(String str)
        {

            Decimal result = -1m;

            try
            {
                result = Decimal.Parse(str.Trim());
                String test = String.Format("{0:0,0}", result).Replace(",", ".");
            }
            catch 
            {
                MessageBox.Show("Sâu số Decimal không hợp lệ!");
            }

            return result;
        }

        //Hàm chuyển DL kiểu String sang int
        public int stringToInt(String num)
        {
            int kq = -1;

            try
            {
                kq = int.Parse(num.Trim());                
            }
            catch
            {
                MessageBox.Show("Sâu số int32 không hợp lệ");
            }

            return kq;
        }

        //Hàm kiểm tra 

        //Hàm kiểm tra SDT được nhập có hợp lệ không
        public bool CheckIfContactValid(String num)
        {
            bool result = false;
            long phoneNum;
            try
            {
                phoneNum = long.Parse(num);
                if (phoneNum <= 0)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                }
                else if (phoneNum < 1000000000)
                {
                    MessageBox.Show("Số điện thoại quá ngắn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                }
                else if (phoneNum > 100000000000)
                {
                    MessageBox.Show("Số điện thoại quá dài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = false;
                }
                else
                    result = true;
            }
            catch
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        //Hàm đọc từ dạng String sang Image 
        public byte[] ImageToBytes(String imagePath)
        {
            FileStream fs;
            byte[] picBytes = null;

            try
            {
                fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

                picBytes = new byte[fs.Length];

                fs.Read(picBytes, 0, System.Convert.ToInt32(fs.Length));

                fs.Close();
            }
            catch
            {
                MessageBox.Show("Ảnh nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return picBytes;
        }
    }
}
