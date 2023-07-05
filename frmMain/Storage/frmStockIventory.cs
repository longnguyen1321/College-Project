using frmMain.HRM;
using frmMain.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.Storage
{
    public partial class frmStockIventory : Form
    {
        //Khai báo
        int dgvSelectedIndex = -1;
        SqlDataReader reader;
        DataTable tblDGV;
        DataTable tblPdList;
        DataTable tblDBIventory;
        DataTable tblDBIventoryDetails;
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản

        public frmStockIventory()
        {
            InitializeComponent();
        }

        public frmStockIventory(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmStockIventory_Load(object sender, EventArgs e)
        {
            mnuStockIventory.Checked = true;

            DefineTblPdList();
            DefineTblDGV();
            DefineTblDBIventoryDetails();

            ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");

            dgvIventory.DataSource = tblDGV;
        }

        private void frmStockIventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn muốn đóng ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        //========Các hàm xử lý======
        private bool ShowDataLVi(String query)
        {                      
            bool result = true;
            int lvItemNum;
            String MaMH, TenMH;
            

            Class.DbConnection.OpenConnection();

            //Xóa các Items trong ListView
            lviProducts.Items.Clear();

            //Thực thi truy vấn và trả về đầu đọc
            reader = Class.DbConnection._ExecuteReader(query);

            //Show các dữ liệu truy xuất được lên ListView
            while (reader.Read())
            {
                MaMH = reader.GetString(0);
                TenMH = reader.GetString(1);

                ListViewItem lvi = new ListViewItem(MaMH);
                lvi.SubItems.Add(TenMH);
      
                lviProducts.Items.Add(lvi);
            }

            //Kiểm tra số Items có trong ListView
            lvItemNum = lviProducts.Items.Count;
            if (lvItemNum < 1)
            {
                MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }

            //Đóng đầu đọc
            reader.Close();

            return result;
        }

        //Hàm kiểm tra dữ liệu đã được nhập
        private bool ProductTextBIsFilled(String code, String quantity)
        {
            bool result = true;
            if(code == "")
            {
                MessageBox.Show("Chưa chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (quantity == "")
            {
                MessageBox.Show("Chưa nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }

            return result;
        }

        //Hàm kiểm tra dữ liệu nhập có hợp lệ
        private bool CheckIfInputValid(String quantity)
        {
            bool result = true;
            Class.GeneralFunctions func = new Class.GeneralFunctions();
            
            if(func.stringToInt(quantity) == -1)
            {
                MessageBox.Show("Số lượng nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }

        private void DefineTblDGV()
        {
            tblDGV = new DataTable();

            //Định nghĩa cột cho DataTable
            
            DataColumn codeCol = new DataColumn("MaMH", System.Type.GetType("System.String"));
            DataColumn nameCol = new DataColumn("TenMH", System.Type.GetType("System.String"));
            DataColumn stockCol = new DataColumn("SLTinh", System.Type.GetType("System.Int32"));
            DataColumn countedCol = new DataColumn("SLThucTe", System.Type.GetType("System.Int32"));
            DataColumn differentCol = new DataColumn("SLLech", System.Type.GetType("System.Int32"));
            DataColumn priceCol = new DataColumn("GiaMHLech", System.Type.GetType("System.Decimal"));
            DataColumn differentValueCol = new DataColumn("SoTienMHLech", System.Type.GetType("System.Decimal"));

            //Thêm cột vào DataTable
            tblDGV.Columns.Add(codeCol);
            tblDGV.Columns.Add(nameCol);
            tblDGV.Columns.Add(priceCol);
            tblDGV.Columns.Add(stockCol);
            tblDGV.Columns.Add(countedCol);
            tblDGV.Columns.Add(differentCol);
            tblDGV.Columns.Add(differentValueCol);

            tblDGV.PrimaryKey = new DataColumn[] { codeCol };
        }

        private void DefineTblPdList()
        {
            tblPdList = Class.DbConnection.GetDataToTable("select MaMH, TenMH, GiaMH, TonKho from MatHang");
            DataColumn codeCol = tblPdList.Columns["MaMH"];
            tblPdList.PrimaryKey = new DataColumn[] { codeCol };
        }

        private void DefineTblDBIventoryDetails()
        {
            tblDBIventoryDetails = Class.DbConnection.GetDataToTable("select * from ChiTietPKK");
        }

        private void ChangeButtonsStatus(bool status)
        {
            btnCreate.Enabled = status;
            btnDelete.Enabled = status;
            btnUpdate.Enabled = status;
            btnCreate.Enabled = status;
        }

        private void ResetInput()
        {
            txtPdCode.Text = "";
            txtPdName.Text = "";
            txtPdPrice.Text = "";
            txtPdStock.Text = "";
            txtPdCountedQuantity.Text = "";
        }

        private Decimal CalSumIventory()
        {
            Class.GeneralFunctions func = new Class.GeneralFunctions();

            Decimal decimalNum = 0;

            foreach (DataRow dataRow in tblDGV.Rows)
            {
                decimalNum += func.stringToDecimal(dataRow["SoTienMHLech"].ToString());
            }

            //Nếu bị thừa tiền, ta thêm dấu "+" vào tổng giá trị phiếu kiểm kho
            if (decimalNum > 0)
            {
                txtTotalIventory.Text = "+" + String.Format("{0:0,0.00 VNĐ}", decimalNum);
            }
            else if(decimalNum < 0)
            {
                txtTotalIventory.Text = String.Format("{0:0,0.00 VNĐ}", decimalNum);
            }
            else
            {
                txtTotalIventory.Text = "0";
            }

            return decimalNum;
        }

        //========Xử lý các sự kiện click trên controls hiển thị========

        private void lviProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Xóa dữ liệu trong các TextBox
            txtPdPrice.Text = "";
            txtPdCountedQuantity.Text = "";

            //Lấy các giá trị các Item và gắn vào TextBox
            foreach (ListViewItem items in lviProducts.SelectedItems)
            {
                txtPdCode.Text = items.SubItems[0].Text;
                txtPdName.Text = items.SubItems[1].Text;

                DataRow dataRow = tblPdList.Rows.Find(txtPdCode.Text);
                txtPdPrice.Text = dataRow["GiaMH"].ToString();
                txtPdStock.Text = dataRow["TonKho"].ToString();
            }
        }

        private void dgvIventory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSelectedIndex = e.RowIndex;
            txtPdCode.Text = dgvIventory.Rows[dgvSelectedIndex].Cells["MaMH"].Value.ToString();
            txtPdName.Text = dgvIventory.Rows[dgvSelectedIndex].Cells["TenMH"].Value.ToString();
            txtPdCountedQuantity.Text = dgvIventory.Rows[dgvSelectedIndex].Cells["SLThucTe"].Value.ToString();
            txtPdPrice.Text = dgvIventory.Rows[dgvSelectedIndex].Cells["GiaMHLech"].Value.ToString();
            txtPdStock.Text = dgvIventory.Rows[dgvSelectedIndex].Cells["SLTinh"].Value.ToString();
        }

        private void txtTotalIventory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Không thể thay đổi dữ liệu này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //=========Xử lý sự kiện click các buttons=====
        private void btnSearch_Click(object sender, EventArgs e)
        {
            String TypedCode = txtSearchCode.Text.Trim();

            String query = "select MaMH, TenMH, GiaMH from MatHang where MaMH = '" + TypedCode + "' or TenMH like N'%" + TypedCode + "%'  except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'";

            ShowDataLVi(query);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ form
            String pdCode = txtPdCode.Text.Trim();
            String pdName = txtPdName.Text.Trim();
            String pdQuantity = txtPdCountedQuantity.Text.Trim(); //Số lượng điền
            String pdPrice = txtPdPrice.Text.Trim();
            String pdStock = txtPdStock.Text.Trim();//Số lượng tồn kho trong hệ thống
            
            //Gía trị mặc định khi chưa có dữ liệu của tổng giá trị phiểu kiểm là 0
          

            if (ProductTextBIsFilled(pdCode, pdQuantity) != true || CheckIfInputValid(pdQuantity) != true)
            {
                return;
            }
            else
            {
                Class.GeneralFunctions func = new Class.GeneralFunctions();
                int differents = func.stringToInt(pdQuantity) - func.stringToInt(pdStock); //Tính số lượng bị lệch
                Decimal pdChangedValue = differents * func.stringToInt(pdPrice); //Gía trị bị lệch
                Decimal iventoryTotal = CalSumIventory() + pdChangedValue; //Cập nhập Số tiền lệch của cả phiếu kiểm kho

                DataRow addedPd = tblDGV.Rows.Find(pdCode);

                //Kiểm tra mặt hàng đã được chọn từ trước không
                if(addedPd == null)
                {
                    addedPd = tblDGV.NewRow();
                    addedPd["MaMH"] = pdCode;
                    addedPd["TenMH"] = pdName;
                    addedPd["GiaMHLech"] = func.stringToDecimal(pdPrice);
                    addedPd["SLTinh"] = func.stringToInt(pdStock);
                    addedPd["SLThucTe"] = func.stringToInt(pdQuantity);
                    addedPd["SLLech"] = differents;
                    addedPd["SoTienMHLech"] = pdChangedValue;
                    tblDGV.Rows.Add(addedPd);

                    //Tính tổng tiền phiếu kiểm kho và gán vào TextBox tổng tiền
                    CalSumIventory();

                    //Nếu trong DGV đã có dữ liệu thì ta enable các nút
                    if (tblDGV.Rows.Count == 1)
                    {
                        ChangeButtonsStatus(true);
                    }
                }
                else
                {
                    MessageBox.Show("Mặt hàng đã được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String code = txtPdCode.Text.Trim();
            if (code == "")
            {
                MessageBox.Show("Chưa chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Thực sự muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(dialogResult == DialogResult.Yes)
                {
                    Class.GeneralFunctions func = new Class.GeneralFunctions();
                    
                    //Tìm mặt hàng được xóa trong DataSource của DGV
                    DataRow deletedRow = tblDGV.Rows.Find(code);
                    
                    if(deletedRow != null)
                    {
                        deletedRow.Delete();

                        //Tính tổng tiền phiếu kiểm kho và gán vào TextBox tổng tiền
                        CalSumIventory();

                        //Xóa các dữ liệu trong ô TextBox
                        ResetInput();
                    }                   
                    else
                    {
                        MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String code = txtPdCode.Text.Trim();
            String quantity = txtPdCountedQuantity.Text.Trim();
            String price = txtPdPrice.Text.Trim();
            String stock = txtPdStock.Text.Trim();
            if(ProductTextBIsFilled(code, quantity) != true || CheckIfInputValid(quantity) != true)
            {
                return;
            }
            else
            {
                DataRow updatedRow = tblDGV.Rows.Find(code);

                if(updatedRow == null)
                {
                    MessageBox.Show("Mặt hàng chưa được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    Class.GeneralFunctions func = new Class.GeneralFunctions();

                    //Cập nhập dữ liệu trong DGV
                    int newDifferent = func.stringToInt(quantity) - func.stringToInt(stock);
                    Decimal newChangedValue = newDifferent * func.stringToInt(price); //Gía trị bị lệch

                    updatedRow.BeginEdit();
                    updatedRow["SLThucTe"] = func.stringToInt(quantity);
                    updatedRow["SLLech"] = newDifferent;
                    updatedRow["SoTienMHLech"] = newChangedValue;
                    updatedRow.EndEdit();

                    //Tính tổng tiền phiếu kiểm kho và gán vào TextBox tổng tiền
                    CalSumIventory();
                }
            }
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(tblDGV.Rows.Count < 1)
            {
                MessageBox.Show("Chưa có mặt hàng nào được kiểm tra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Thực sự muộn tạo phiếu kiểm kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialogResult == DialogResult.Yes)
                {
                    DateTime presentDate = DateTime.Now;

                    Class.GeneralFunctions func = new Class.GeneralFunctions();

                    String randomStr = func.RandomizeChar(6, 3);//Random chuỗi 5 kí tự không có kí hiện phân cách

                    //Phiếu kiểm kho
                    tblDBIventory = Class.DbConnection.GetDataToTable("select * from PhieuKiemKho");
                    DataRow iventoryRow = tblDBIventory.NewRow();
                    iventoryRow["MaPKK"] = randomStr;
                    iventoryRow["Ma"] = _staffCode;
                    iventoryRow["NgayTaoPKK"] = presentDate;
                    iventoryRow["SoTienLech"] = CalSumIventory();
                    tblDBIventory.Rows.Add(iventoryRow);

                    //Bảng chi tiết phiếu nhập                   
                    //tblDBIventoryDetails = tblDGV; //Lấy dữ liệu từ bảng DGV
                    foreach(DataRow iventoryDetailRow in tblDGV.Rows) //Sửa mã phiếu kiểm kho của từng chi tiết phiếu kiểm kho
                    {
                        DataRow dataRow = tblDBIventoryDetails.NewRow();
                        dataRow["MaPKK"] = randomStr;
                        dataRow["MaMH"] = iventoryDetailRow["MaMH"].ToString().Trim();
                        dataRow["SLTinh"] = iventoryDetailRow["SLTinh"].ToString().Trim();
                        dataRow["SLThucTe"] = iventoryDetailRow["SLThucTe"].ToString().Trim();
                        dataRow["SLLech"] = iventoryDetailRow["SLLech"].ToString().Trim();
                        dataRow["GiaMHLech"] = iventoryDetailRow["GiaMHLech"].ToString().Trim();
                        dataRow["SoTienMHLech"] = iventoryDetailRow["SoTienMHLech"].ToString().Trim();
                        tblDBIventoryDetails.Rows.Add(dataRow);
                    }

                    int checkIventory = Class.DbConnection.UpdateDBThroughDTable(tblDBIventory, " select * from PhieuKiemKho");
                    int checkIventoryDetails = Class.DbConnection.UpdateDBThroughDTable(tblDBIventoryDetails, "select * from ChiTietPKK");
                    
                    if(checkIventory > 0 && checkIventoryDetails > 0)
                    {
                        MessageBox.Show("Tạo phiếu kiểm kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtIveCode.Text = randomStr;
                        txtIveDate.Text = String.Format("{0:dd/MM/yyyy HH:mm}", presentDate);
                        txtIveMng.Text = _staffCode;
                    }
                    else
                    {
                        MessageBox.Show("Tạo phiếu kiểm kho thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Thực sự muốn tạo phiếu kiểm kho mới?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dialogResult == DialogResult.Yes)
            {
                ResetInput();
                txtIveCode.Text = "";
                txtIveDate.Text = "";
                txtIveMng.Text = "";
                txtTotalIventory.Text = "";

                tblDGV.Rows.Clear();
            }
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
                frmProductManage frmProductManage = new Product.frmProductManage(_frmLogin, _staffCode, _staffAuthority);
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
