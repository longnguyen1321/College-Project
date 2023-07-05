using frmMain.HRM;
using frmMain.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace frmMain.Storage
{
    public partial class frmIventoryManage : Form
    {
        //Khai báo các biến toàn cục
        SqlDataReader reader;
        DataTable tblDGV;
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản
        public frmIventoryManage()
        {
            InitializeComponent();
        }

        public frmIventoryManage(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmIventoryManage_Load(object sender, EventArgs e)
        {
            mnuStockIventoryManage.Checked = true;
            Class.DbConnection.OpenConnection();
            ShowDataLVi("Select MaPKK, NgayTaoPKK, SoTienLech from PhieuKiemKho order by NgayTaoPKK");
            DefineTblDGV();
        }

        private void frmIventoryManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn muốn đóng phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        //========Cấc hàm xử lý=======
        private void ShowDataLVi(String query)
        {
            lvIventoryList.Items.Clear();

            //Thực thi truy vấn và trả về đầu đọc
            reader = Class.DbConnection._ExecuteReader(query);

            String code, date, money;
            while(reader != null && reader.Read())
            {
                code = reader.GetString(0);
                date = String.Format("{0:dd/MM/yyyy hh:MM}", reader.GetDateTime(1));
                if(reader.GetDecimal(2) > 0) //Nếu số tiền nhỏ hơn 0, thêm dấu + đằng trước để thể hiện là tiền bị thừa
                {
                    money = String.Format("{0:+0,0.00}", reader.GetDecimal(2));
                }
                else
                {
                    money = String.Format("{0:0,0.00}", reader.GetDecimal(2));
                }

                ListViewItem lvi = new ListViewItem(code);
                lvi.SubItems.Add(date);
                lvi.SubItems.Add(money);

                lvIventoryList.Items.Add(lvi);
            }

            if(reader != null)
            {
                reader.Close();
            }
        }

        private void DefineTblDGV()
        {
            tblDGV = new DataTable();

            DataColumn pdCodeCol = new DataColumn("MaMH", System.Type.GetType("System.String"));
            DataColumn pdNameCol = new DataColumn("TenMH", System.Type.GetType("System.String"));
            DataColumn pdPriceCol = new DataColumn("GiaMHLech", System.Type.GetType("System.String")); 
            DataColumn pdAnticipatedQuantity = new DataColumn("SLTinh", System.Type.GetType("System.Int32"));
            DataColumn pdCountedQuantity = new DataColumn("SLThucTe", System.Type.GetType("System.Int32"));
            DataColumn pdQuantityDifferent = new DataColumn("SLLech", System.Type.GetType("System.Int32"));
            DataColumn pdValueDifferent = new DataColumn("SoTienMHLech", System.Type.GetType("System.String"));

            tblDGV.Columns.Add(pdCodeCol);
            tblDGV.Columns.Add(pdNameCol);
            tblDGV.Columns.Add(pdPriceCol);
            tblDGV.Columns.Add(pdAnticipatedQuantity);
            tblDGV.Columns.Add(pdCountedQuantity);
            tblDGV.Columns.Add(pdQuantityDifferent);
            tblDGV.Columns.Add(pdValueDifferent);

            dgvIventoryDetail.DataSource = tblDGV;
        }

        //=======Xử lý sự kiện bấm trên các Controls hiển thị dữ liệu=====
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvIventoryList.Items.Count == 0)
            {
                return;
            }
            else
            {
                foreach (ListViewItem items in lvIventoryList.SelectedItems)
                {
                    reader = Class.DbConnection._ExecuteReader("select Ma from PhieuKiemKho where MaPKK = '" + items.SubItems[0].Text.Trim() + "'");
                    reader.Read();

                    txtCode.Text = items.SubItems[0].Text.Trim();
                    txtCreatedBy.Text = reader.GetString(0);
                    txtDate.Text = items.SubItems[1].Text.Trim();
                    txtDifferentAmount.Text = items.SubItems[2].Text.Trim();

                    reader.Close();
                }
                    
            }
        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "Select MaPKK, NgayTaoPKK, SoTienLech from PhieuKiemKho ";

            if (cbTime.Text.Trim() == "Trong ngày")
            {
                query += "where DATEDIFF(Day,'" + DateTime.Now + "', NgayTaoPKK) = 0"; //Khoảng cách giữa DateTime.Now và NgayNhap bằng 0
            }

            else if (cbTime.Text.Trim() == "Trong tuần")
            {
                query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayTaoPKK) > (-7)";
            }

            else if (cbTime.Text.Trim() == "Trong tháng")
            {
                query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayTaoPKK) = 0";
            }

            else if (cbTime.Text.Trim() == "3 tháng gần nhất")
            {
                query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayTaoPKK) > (-3)";
            }

            else if (cbTime.Text.Trim() == "6 tháng gần nhất")
            {
                query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayTaoPKK) > (-6)";
            }

            else if (cbTime.Text.Trim() == "12 tháng gần nhất")
            {
                query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayTaoPKK) > (-12)";
            }

            ShowDataLVi(query + " order by NgayTaoPKK");
        }

        //=====Xử lý sự kiện bấm các buttons=====
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowDataLVi("Select MaPKK, NgayTaoPKK, SoTienLech from PhieuKiemKho where MaPKK = '" + txtSearchCode.Text.Trim() + "' order by NgayTaoPKK");
            
            if(lvIventoryList.Items.Count == 0)
            {
                MessageBox.Show("Phiếu kiểm kho không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowDataLVi("Select MaPKK, NgayTaoPKK, SoTienLech from PhieuKiemKho order by NgayTaoPKK");
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ShowDataLVi("Select MaPKK, NgayTaoPKK, SoTienLech from PhieuKiemKho order by NgayTaoPKK");
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            if(txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn phiếu kiểm kho cần hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                reader = Class.DbConnection._ExecuteReader("select ChiTietPKK.MaMH, TenMH, GiaMHLech, SLTinh, SLThucTe, SLLech, SoTienMHLech from ChiTietPKK inner join MatHang on ChiTietPKK.MaMH = MatHang.MaMH where MaPKK = '" + txtCode.Text.Trim() + "'");

                tblDGV.Rows.Clear();

                DataRow iventoryRow;
                while (reader.Read())
                {
                    iventoryRow = tblDGV.NewRow();
                    iventoryRow["MaMH"] = reader.GetString(0);
                    iventoryRow["TenMH"] = reader.GetString(1);
                    iventoryRow["GiaMHLech"] = String.Format("{0:+0,0.00}", reader.GetDecimal(2));
                    iventoryRow["SLTinh"] = reader.GetInt32(3);
                    iventoryRow["SLThucTe"] = reader.GetInt32(4);
                    iventoryRow["SLLech"] = reader.GetInt32(5);

                    if (reader.GetDecimal(6) > 0) //Nếu số tiền nhỏ hơn 0, thêm dấu + đằng trước để thể hiện là tiền bị thừa
                    {
                        iventoryRow["SoTienMHLech"]  = String.Format("{0:+0,0.00}", reader.GetDecimal(6));
                    }
                    else
                    {
                        iventoryRow["SoTienMHLech"] = String.Format("{0:0,0.00}", reader.GetDecimal(6));
                    }

                    tblDGV.Rows.Add(iventoryRow);
                }

                reader.Close();
            }
        }

        //====Xử lý sự kiện Click MenuStrip=====
        private void mnuSelectProducts_Click_1(object sender, EventArgs e)
        {
            frmProductSelect frmProductSelect = new frmProductSelect(_frmLogin, _staffCode, _staffAuthority);
            frmProductSelect.Show();
            this.Dispose(true);
        }

        private void mnuPdManage_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmProductManage frmProductManage = new frmProductManage(_frmLogin, _staffCode, _staffAuthority);
                frmProductManage.Show();
                this.Dispose(true); //Giải phóng tài nguyên dùng bởi Form
            }
        }

        private void mnuStorage_Click(object sender, EventArgs e)
        {
            frmStorage frmStorage = new frmStorage(_frmLogin, _staffCode, _staffAuthority);
            frmStorage.Show();
            this.Dispose(true);
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmImport frmImport = new frmImport("import", _frmLogin, _staffCode, _staffAuthority);
                frmImport.Show();
                this.Dispose(true);
            }
        }

        private void mnuExport_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmImport frmImport = new frmImport("export", _frmLogin, _staffCode, _staffAuthority);
                frmImport.Show();
                this.Dispose(true);
            }
        }

        private void mnuTransactionsManage_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmTransactionManage frmTransactionManage = new frmTransactionManage(_frmLogin, _staffCode, _staffAuthority);
                frmTransactionManage.Show();
                this.Dispose(true);
            }
        }

        private void mnuSuppliers_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmSuppliers frmSuppliers = new frmSuppliers(_frmLogin, _staffCode, _staffAuthority);
                frmSuppliers.Show();
                this.Dispose(true);
            }
        }

        private void mnuStockIventory_Click(object sender, EventArgs e)
        {
            frmStockIventory frmStockIventory = new frmStockIventory(_frmLogin, _staffCode, _staffAuthority);
            frmStockIventory.Show();
            this.Dispose(true);
        }

        private void mnuStaffManage_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmStaffManage frmStaffManage = new frmStaffManage(_frmLogin, _staffCode, _staffAuthority);
                frmStaffManage.Show();
                this.Dispose(true);
            }
        }

        private void mnuAccountManage_Click(object sender, EventArgs e)
        {
            if (_staffAuthority == "NhanVien")
            {
                MessageBox.Show("Bạn không có quyền sử dụng chức năng này!", "Không thể truy cập", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                frmAccountManage frmAccountManage = new frmAccountManage(_frmLogin, _staffCode, _staffAuthority);
                frmAccountManage.Show();
                this.Dispose(true);
            }
        }
    }
}
