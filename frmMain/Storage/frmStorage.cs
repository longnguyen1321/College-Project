using frmMain.HRM;
using frmMain.Product;
using frmMain.Storage;
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

namespace frmMain
{
    public partial class frmStorage : Form
    {
        //Khai báo
        DataTable tblDGV;
        DataTable tblProducts;
        SqlDataReader reader;
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản

        //frmLogin _frmProducts;

        public frmStorage()
        {
            InitializeComponent();
        }
        
        public frmStorage(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmStorage_Load(object sender, EventArgs e)
        {
            mnuStorage.Checked = true;
            DefineTblDGV();
            ShowDataDGV("select MaMH, TenMH, TonKho, SLTonKhoToiThieu from MatHang except select MaMH, TenMH, TonKho, SLTonKhoToiThieu from MatHang where MaMH = 'MH_0000'");
            txtTotalProducts.Text = tblDGV.Rows.Count.ToString();
        }

        private void frmStorage_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn muốn đóng phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        //=========Định nghĩa các hàm xử lý==========
        //Hàm hiển thị dữ liệu lên DataGridView
        private void ShowDataDGV(String queryForTblProducts)
        {
            //Mở kết nối
            Class.DbConnection.OpenConnection();
            Class.GeneralFunctions func = new Class.GeneralFunctions();
            
            int presentMonth = DateTime.Now.Month;//Lấy tháng hiện tại
           
            tblProducts = Class.DbConnection.GetDataToTable(queryForTblProducts);            

            //Nếu có mặt hàng trong tblProducts mới thực hiện
            if(tblProducts.Rows.Count > 0)
            {
                DataRow product, presentProduct;
                String totalImportThisMonth, totalExportThisMonth;

                tblDGV.Rows.Clear(); //Do thiết lập khóa chính bảng tblDGV là mã mặt hàng, cần xóa mặt cũ đi khi thêm lại mặt hàng đó vào bảng do bị trùng 2 khóa chính

                //Truy vấn tổng nhập và xuất trong tháng cho từng mặt hàng 
                for (int i = 0; i < tblProducts.Rows.Count; i++)
                {
                    //Dòng DataRow lấy kết quả từ truy vấn để hiển thị lên DGV
                    product = tblDGV.NewRow();

                    //Mặt hàng đang được truy vấn hiện tại
                    presentProduct = tblProducts.Rows[i];

                    //Tính tổng số lượng nhập trong tháng của mặt hàng
                    //Lọc tổng số lượng nhập của mặt hàng trong tháng                
                    reader = Class.DbConnection._ExecuteReader("select Sum(SoLuongNhap) from ChiTietPN inner join PhieuNhap on ChiTietPN.MaPN = PhieuNhap.MaPN where MaMH = '" + presentProduct["MaMH"].ToString().Trim() + "' and Month(NgayNhap) = " + presentMonth + " and TinhTrangPN = N'Đã nhập!'");
                    reader.Read();
                    //Kiểm tra nếu trong CSDL không tồn tại dữ liệu mặt hàng nhập trong tháng để reader đọc
                    totalImportThisMonth = reader.IsDBNull(0) ? null : reader.GetInt32(0).ToString();

                    //Nếu không tồn tại dữ liệu nhập tháng này để truy vấn thì mặc định tổng nhập tháng bằng 0
                    if (totalImportThisMonth == null)
                    {
                        totalImportThisMonth = "0";
                    }

                    reader.Close();

                    //Tính tổng số lượng xuất của tháng
                    reader = Class.DbConnection._ExecuteReader("select Sum(SoLuongXuat) from ChiTietPX inner join PhieuXuat on ChiTietPX.MaPX = PhieuXuat.MaPX where Month(NgayXuat) = " + presentMonth + " and MaMH = '" + presentProduct["MaMH"].ToString().Trim() + "' and TinhTrangPX = N'Đã xuất!'");
                    reader.Read();

                    totalExportThisMonth = reader.IsDBNull(0) ? null : reader.GetInt32(0).ToString();

                    if (totalExportThisMonth == null)
                    {
                        totalExportThisMonth = "0";

                    }
                    reader.Close();


                    String presentProductStatus = "Nhiều hàng";
                    //Nếu tồn kho ít hơn số lượng tồn kho tối thiểu thì hàng cần bổ sung
                    if (func.stringToInt(presentProduct["TonKho"].ToString()) < func.stringToInt(presentProduct["SLTonKhoToiThieu"].ToString()))
                    {
                        presentProductStatus = "Ít hàng";
                    }

                    product["MaMH"] = presentProduct["MaMH"].ToString();
                    product["TenMH"] = presentProduct["TenMH"].ToString();
                    product["TonKho"] = func.stringToInt(presentProduct["TonKho"].ToString());
                    product["TinhTrang"] = presentProductStatus;
                    product["TongSLN"] = func.stringToInt(totalImportThisMonth);
                    product["TongSLX"] = func.stringToInt(totalExportThisMonth);
                    tblDGV.Rows.Add(product);
                }
                dgvStorage.DataSource = tblDGV;
            }
            
        }

        //Hàm định nghĩa bảng tblDGV
        private void DefineTblDGV()
        {
            tblDGV = new DataTable();

            //Định nghĩa cột cho DataTable
            DataColumn codeCol = new DataColumn("MaMH", System.Type.GetType("System.String"));
            DataColumn nameCol = new DataColumn("TenMH", System.Type.GetType("System.String"));
            DataColumn quantityCol = new DataColumn("TonKho", System.Type.GetType("System.Int32"));
            DataColumn statusCol = new DataColumn("TinhTrang", System.Type.GetType("System.String"));
            DataColumn importTotalQuanttityCol = new DataColumn("TongSLN", System.Type.GetType("System.Int32"));
            DataColumn exportTotalQuanttityCol = new DataColumn("TongSLX", System.Type.GetType("System.Int32"));

            //Thêm cột vào DataTable
            tblDGV.Columns.Add(codeCol);
            tblDGV.Columns.Add(nameCol);
            tblDGV.Columns.Add(quantityCol);
            tblDGV.Columns.Add(statusCol);
            tblDGV.Columns.Add(importTotalQuanttityCol);
            tblDGV.Columns.Add(exportTotalQuanttityCol);

            tblDGV.PrimaryKey = new DataColumn[] { codeCol };
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            ShowDataDGV("select MaMH, TenMH, TonKho, SLTonKhoToiThieu from MatHang except select MaMH, TenMH, TonKho, SLTonKhoToiThieu from MatHang where MaMH = 'MH_0000'");
            txtSearchInfo.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtSearchInfo.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập mặt hàng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ShowDataDGV("select MaMH, TenMH, TonKho, SLTonKhoToiThieu from MatHang where MaMH = '" + txtSearchInfo.Text.Trim() + "' or TenMH like N'%" + txtSearchInfo.Text.Trim() + "%' except select MaMH, TenMH, TonKho, SLTonKhoToiThieu from MatHang where MaMH = 'MH_0000'");
                if (tblProducts.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            
        }

        private void btnOpenFrmIventory_Click(object sender, EventArgs e)
        {
            frmStockIventory frmStockIventory = new frmStockIventory(_frmLogin, _staffCode, _staffAuthority);
            frmStockIventory.Show();
            this.Dispose(true);
        }
        //====Xử lý sự kiện Click MenuStrip=====
        private void mnuSelectProducts_Click(object sender, EventArgs e)
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

        private void mnuStockIventoryManage_Click(object sender, EventArgs e)
        {
            frmIventoryManage frmIventoryManage = new frmIventoryManage(_frmLogin, _staffCode, _staffAuthority);
            frmIventoryManage.Show();
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
