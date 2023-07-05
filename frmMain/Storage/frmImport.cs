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
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;
using System.Data.SqlTypes;
using frmMain.Storage;
using System.Reflection.Emit;
using frmMain.HRM;
using frmMain.Product;

namespace frmMain
{
    public partial class frmImport : Form
    {
        //Khai báo
        SqlDataReader reader;
        DataTable tblImports; //Để làm DataSource cho DGV
        DataTable tblDBImports; //Để cập nhập cơ sở dữ liệu bảng phiếu nhập
        DataTable tblDBExport; //Để cập nhập cơ sở dữ liệu bảng phiếu xuất
        DataTable tblDBImportsDetail; //Để cập nhập cơ sở dữ liệu bảng ChiTietPN
        DataTable tblDBExportDetail; //Để cập nhập cơ sở dữ liệu bảng ChiTietPX
        DataTable tblDBProduct; //Để cập nhập số lượng tồn kho của các mặt hàng được nhập
        
        String checkSelectedAction; //Chứa hành động là hiện form phiếu nhập hoặc phiếu xuất     
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản


        int dgvSelectedIndex = -1; //Chứa Index dòng đang được chọn cùa DGV
        public frmImport()
        {
            InitializeComponent();
        }

        public frmImport(String selectedAction, frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            checkSelectedAction = selectedAction;
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            if(checkSelectedAction == "import")
            {
                mnuImport.Checked = true;
            }
            else
            {
                mnuExport.Checked = true;
            }

            ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");

            ChangeIntoExportForm(checkSelectedAction);
            
            GetCBoxData();

            DefineTblImport();

            DefineTblDBProduct();

            cbSupplier.Text = "";
        }

        private void frmImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn muốn đóng phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        //===========Các hàm xử lý==============
        //Hàm thay đổi form dựa trên hành động được truyền vào từ constructor
        private void ChangeIntoExportForm(String action)
        {
            if(action == "export")
            {
                lbPrice.Text = "Phiếu xuất";
                lbPrice.Text = "Giá xuất";
                btnCreate.Text = "Tạo phiếu xuất";
                btnCancel.Text = "Hủy phiếu xuất";
                gbInfo.Text = "Thông tin phiếu xuất";
                gbPdInfo.Text = "Thông tin xuất";
                gbSelectedProducts.Text = "Mặt hàng xuất";
                labelTitle.Text = "Xuất kho";
                cbSupplier.Enabled = false; //Nếu là xuất hàng thì không cho chọn nhà cung cấp
                btnAddSupp.Enabled = false;
                lbReason.Visible = true; //Hiện textbox nhập lí do xuất
                txtPdReason.Visible = true;
                this.Text = "Xuất kho";
            } 
        }
        //Hàm hiển thị dữ liệu lên bảng ListView
        private bool ShowDataLVi(String query)
        {
            bool result = false;
            int lvItemNum;

            Class.DbConnection.OpenConnection();

            //Xóa các Items trong ListView
            lviImProducts.Items.Clear();

            //Thực thi truy vấn và trả về đầu đọc
            reader = Class.DbConnection._ExecuteReader(query);
            
            //Show các dữ liệu truy xuất được lên ListView
            while (reader.Read())
            {
                String MaMH = reader.GetString(0);
                String TenMH = reader.GetString(1);

                ListViewItem lvi = new ListViewItem(MaMH);
                lvi.SubItems.Add(TenMH);

                lviImProducts.Items.Add(lvi);
            }

            //Kiểm tra số Items có trong ListView
            lvItemNum = lviImProducts.Items.Count;
            if (lvItemNum < 1)
            {
                MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Đóng đầu đọc
            reader.Close();
            return result;
        }

        //Hàm lấy dữ liệu cho ComboBox
        private void GetCBoxData()
        {
            DataTable tblSuppliers = new DataTable();

            String query = "select MaNCC, TenNCC from NhaCungCap except select MaNCC, TenNCC from NhaCungCap where MaNCC = 'NCC_0000'";

            //Định nghĩa các cột cho bảng DataTable
            DataColumn codeCol = new DataColumn("MaNCC", System.Type.GetType("System.String"));
            DataColumn nameCol = new DataColumn("TenNCC", System.Type.GetType("System.String"));

            //Thêm các cột vào bảng
            tblSuppliers.Columns.Add(codeCol);
            tblSuppliers.Columns.Add(nameCol);

            //Thực hiện truy vấn và trả về bảng 
            tblSuppliers = Class.DbConnection.GetDataToTable(query);

            //Gắn DataSource và hiển thị cột tên
            cbSupplier.DataSource = tblSuppliers;
            cbSupplier.DisplayMember = "TenNCC";

            cbSupplier.SelectedIndex = -1;
        }

        //Hàm reset các TextBox nhập
        private void ResetInputTB()
        {
            txtPdCode.Text = "";
            txtPdName.Text = "";
            txtPdPrice.Text = "";
            txtPdQuantity.Text = "";
        }
        
        //Hàm kiểm tra các ô TextBox cần nhập đã được nhấp
        private bool ProductTextBIsValid(String code, String name, String quantity, String price)
        {
            bool result = true;

            if(code == "" || name == "")
            {
                MessageBox.Show("Bạn chưa chọn mặt hàng!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                result = false;
            }
            else if(quantity == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (price == "")
            {
                MessageBox.Show("Bạn chưa nhập giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }

            return result;
        }

        //Hàm tính tổng thành tiền nhập
        private Decimal CalSumIm()
        {
            Class.GeneralFunctions func = new Class.GeneralFunctions();
            Decimal result = 0;

            foreach(DataRow row in tblImports.Rows)
            {
                result += func.stringToDecimal(row["ThanhTienNhap"].ToString());
            }

            return result;
        }

        //Hàm thêm dòng 1 dòng dữ liệu của DataTable của DGV
        private void AddDTRow(String code, String name, String quantity, String price, String total)
        {
            DataRow dataRow;

            //Tính số thứ tự của dòng dữ liệu đang được thêm
            int stt;
            if (tblImports.Rows.Count == 0) //Nếu là mặt hàng đầu tiên được chọn nhập
            {
                stt = 1;
            }
            else //Nếu không phải mặt hàng đầu tiên thì lấy tổng số mặt hàng trước khi thêm mặt hàng + 1
            {
                stt = tblImports.Rows.Count + 1;
            }

            //Thêm dòng dữ liệu mới cho DataTable
            dataRow = tblImports.NewRow();

            Class.GeneralFunctions func = new Class.GeneralFunctions();

            dataRow["STTNhap"] = stt;
            dataRow["MaMH"] = code;
            dataRow["TenMH"] = name;
            dataRow["SoLuongNhap"] = quantity;
            dataRow["GiaNhap"] = func.stringToDecimal(price); //Chuyển từ dạng String sang Decimal
            dataRow["ThanhTienNhap"] = func.stringToDecimal(total);

            tblImports.Rows.Add(dataRow);
        }

        //Hàm định nghĩa bảng dữ liệu tblImport
        private void DefineTblImport()
        {
            tblImports = new DataTable();

            //Định nghĩa cột cho DataTable
            DataColumn orderCol = new DataColumn("STTNhap", System.Type.GetType("System.Int32"));
            DataColumn codeCol = new DataColumn("MaMH", System.Type.GetType("System.String"));
            DataColumn nameCol = new DataColumn("TenMH", System.Type.GetType("System.String"));
            DataColumn quantityCol = new DataColumn("SoLuongNhap", System.Type.GetType("System.Int32"));
            DataColumn priceCol = new DataColumn("GiaNhap", System.Type.GetType("System.Decimal"));
            DataColumn totalCol = new DataColumn("ThanhTienNhap", System.Type.GetType("System.Decimal"));

            //Thêm cột vào DataTable
            tblImports.Columns.Add(orderCol);
            tblImports.Columns.Add(codeCol);
            tblImports.Columns.Add(nameCol);
            tblImports.Columns.Add(quantityCol);
            tblImports.Columns.Add(priceCol);
            tblImports.Columns.Add(totalCol);

            tblImports.PrimaryKey = new DataColumn[] { codeCol };

            dgvImProducts.DataSource = tblImports;
        }

        //Hàm định nghĩa bảng tblDBImportDetail
        private void DefineTblDBProduct()
        {
            tblDBProduct = Class.DbConnection.GetDataToTable("select MaMH, TonKho, GiaMH from MatHang");

            DataColumn productCode = tblDBProduct.Columns[0];

            tblDBProduct.PrimaryKey = new DataColumn[] { productCode };
        }

        //Hàm Enable/Disable các buttons 
        private void ChangeButtonsStatus(bool status)
        {
            btnCancel.Enabled = status;
            btnCreate.Enabled = status;
            btnDelete.Enabled = status;
            btnUpdate.Enabled = status;
        }

        //Hàm kiểm tra các dữ liệu cần nhập có hợp lệ
        private bool CheckIfInputValid(String quantity, String price)
        {
            Class.GeneralFunctions func = new Class.GeneralFunctions();
            bool result = true;

            Decimal _price = func.stringToDecimal(price);
            int _quantity = func.stringToInt(quantity);

            if (_quantity == -1)
            {
                MessageBox.Show("Số lượng nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (_price == -1m)
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            } 
            else if(_price <= 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ! \nGiá nhập tối thiểu là 1!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if(_quantity <= 0 || _quantity > 200){
                MessageBox.Show("Số lượng nhập không hợp lệ! \nSố lượng nhập tối thiểu là 1 và tối đa là 200!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }

        //Hàm reset các Box nhập
        private void ResetInput()
        {
            //Reset các TextBox
            txtImCode.Text = "";
            txtImDate.Text = "";
            cbSupplier.Text = "";
            txtPdCode.Text = "";
            txtPdName.Text = "";
            txtPdPrice.Text = "";
            txtPdQuantity.Text = "";
            txtSearchCode.Text = "";
            txtImTotal.Text = "";
        }

        //Hàm cập nhập tồn kho khi tạo phiếu nhập

        //===============Xử lý các sự kiện chọn dữ liệu từ các controls hiển thị=============== 

        private void dgvImProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSelectedIndex = e.RowIndex;
            txtPdCode.Text = dgvImProducts.Rows[dgvSelectedIndex].Cells["MaMH"].Value.ToString();
            txtPdName.Text = dgvImProducts.Rows[dgvSelectedIndex].Cells["TenMH"].Value.ToString();
            txtPdQuantity.Text = dgvImProducts.Rows[dgvSelectedIndex].Cells["SoLuongNhap"].Value.ToString();
            txtPdPrice.Text = dgvImProducts.Rows[dgvSelectedIndex].Cells["GiaNhap"].Value.ToString();
        }

        private void lviImProducts_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Reset index được chọn của DGV
            dgvSelectedIndex = -1;

            //Xóa dữ liệu trong các TextBox
            txtPdPrice.Text = "";
            txtPdQuantity.Text = "";

            //Lấy các giá trị các Item và gắn vào TextBox
            foreach (ListViewItem items in lviImProducts.SelectedItems)
            {
                txtPdCode.Text = items.SubItems[0].Text;
                txtPdName.Text = items.SubItems[1].Text;
                
                //Nếu là xuất kho, thì giá trị mặc định ban đầu của mặt hàng được chọn là giá bán
                if(checkSelectedAction == "export")
                {
                    DataRow pdPrice = tblDBProduct.Rows.Find(items.SubItems[0].Text);
                    txtPdPrice.Text = pdPrice["GiaMH"].ToString().Trim();
                }
            }
        }

        //===============Xử lý nút===============
        private void btnSearchCode_Click_1(object sender, EventArgs e)
        {
            String TypedCode = txtSearchCode.Text.Trim();

            String query = "select MaMH, TenMH from MatHang where MaMH = '" + TypedCode + "' or TenMH like N'%" + TypedCode + "%' except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'";

            ShowDataLVi(query);
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            //Lấy dữ liệu từ form
            String pdCode = txtPdCode.Text.Trim();
            String pdName = txtPdName.Text.Trim();
            String pdQuantity = txtPdQuantity.Text.Trim();
            String pdPrice = txtPdPrice.Text.Trim();

            //Kiểm tra các TextBox cần nhập
            if ((ProductTextBIsValid(pdCode, pdName, pdQuantity, pdPrice) != true) || (CheckIfInputValid(pdQuantity, pdPrice) != true))
            {
                return;
            }
            else
            {              

                Class.GeneralFunctions func = new Class.GeneralFunctions();

                //Chuyển từ dạng String sang int
                int q = func.stringToInt(pdQuantity);
                decimal p = func.stringToDecimal(pdPrice);               
                                                      
                //Tính thành tiền của 1 sản phẩm
                Decimal total = (Decimal)(q * p);

                //Kiểm tra trong bảng các sản phầm được chọn đã tồn tại mặt hàng đang được thêm không
                DataRow checkReplicatePd = tblImports.Rows.Find(pdCode);
                if(checkReplicatePd == null)
                {
                    //Kiểm tra số lượng tồn kho của mặt hàng có đủ để xuất
                    if(checkSelectedAction == "export")
                    {
                        DataRow checkInStoreQuantity = tblDBProduct.Rows.Find(pdCode);
                        if (func.stringToInt(checkInStoreQuantity["TonKho"].ToString().Trim()) < func.stringToInt(pdQuantity))
                        {
                            MessageBox.Show("Mặt hàng không đủ số lượng tồn kho để xuất!", "Không đủ số lượng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }   
                    
                    //Thêm dòng mới trong DataTable
                    AddDTRow(pdCode, pdName, pdQuantity, pdPrice, total.ToString());

                    //Tính tổng thành tiền nhập các mặt hàng
                    txtImTotal.Text = String.Format("{0:0,0.00 VNĐ}",CalSumIm());

                    //Nếu có dữ liệu trong bàng DGV thì enable các buttons
                    if (dgvImProducts.Rows.Count == 2)
                    {
                        ChangeButtonsStatus(true);
                    }
                }
                else
                {
                    MessageBox.Show("Mặt hàng đã được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }   
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Nếu chưa chọn mặt hàng để xóa
            if(dgvSelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Xóa dòng đang được chọn
                tblImports.Rows.RemoveAt(dgvSelectedIndex);

                //Cập nhập tổng tiền phiếu
                txtImTotal.Text = String.Format("{0:0,0.00 VNĐ}", CalSumIm());

                //Nếu trong DGV không có mặt hàng được chọn thì Disable các buttons
                if (dgvImProducts.RowCount < 2)
                {
                    ChangeButtonsStatus(false);
                }

                //Cập nhập số thứ tự trên DGV
                int startPosition = dgvSelectedIndex; //Vị trí bắt đầu chỉnh lại STT
                //Nếu mặt hàng được xóa không phải mặt hàng cuối cùng trong DGV
                if (dgvSelectedIndex < tblImports.Rows.Count) //tblImports.Rows.Count hiện tại là số lượng dòng trong bảng tblImport sau khi xóa 1 dòng
                {
                    //MessageBox.Show("hello");
                    int j = startPosition;
                    DataRow changeOrder;
                    while(j < tblImports.Rows.Count) //Index lớn nhất của 1 dòng của 1 DataTable là tổng số dòng của DataTable - 1
                    {
                        changeOrder = tblImports.Rows[j];
                        changeOrder.BeginEdit();
                        changeOrder["STTNhap"] = j + 1;
                        changeOrder.EndEdit();

                        j += 1;
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ form về
            String quantity = txtPdQuantity.Text.Trim();
            String imPrice = txtPdPrice.Text.Trim();
            String code = txtPdCode.Text.Trim();
            String name = txtPdName.Text.Trim();
            
            //Nếu chưa chọn mặt hàng để sửa
            if (dgvSelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((ProductTextBIsValid(code, name, quantity, imPrice) != true) || (CheckIfInputValid(quantity, imPrice) != true))
            {
                return;
            }
            else
            {
                Class.GeneralFunctions func = new Class.GeneralFunctions();

                //Kiểm tra số lượng tồn kho của mặt hàng có đủ để xuất
                if (checkSelectedAction == "export")
                {
                    DataRow checkInStoreQuantity = tblDBProduct.Rows.Find(code);
                    if (func.stringToInt(checkInStoreQuantity["TonKho"].ToString().Trim()) < func.stringToInt(quantity))
                    {
                        MessageBox.Show("Mặt hàng không đủ số lượng tồn kho để xuất!", "Không đủ số lượng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //Update dòng đang được chọn trong DGV
                DataRow row = tblImports.Rows[dgvSelectedIndex];
                row.BeginEdit();
                row["SoLuongNhap"] = quantity;
                row["GiaNhap"] = imPrice;
                row["ThanhTienNhap"] = func.stringToInt(quantity) * func.stringToDecimal(imPrice);
                row.EndEdit();

                //Cập nhập tổng tiền phiếu
                txtImTotal.Text = String.Format("{0:0,0.00 VNĐ}", CalSumIm());
            }
        }

        private void btnAddSupp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Thêm nhà cung cấp sẽ làm mất dữ liệu đã nhập! \n Thực sự muốn thực hiện hành động?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if(result == DialogResult.Yes)
            {
                frmSuppliers frmSuppliers = new frmSuppliers(_frmLogin, _staffCode, _staffAuthority);
                frmSuppliers.Show();
                this.Dispose(true);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        { 
            if(checkSelectedAction == "import")
            {
                if (txtImCode.Text != "")
                {
                    MessageBox.Show("Hãy chọn tạo phiếu nhập mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    if (cbSupplier.Text == "")
                    {
                        MessageBox.Show("Chưa chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Thực sự muốn tạo phiếu nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            String suppCode = cbSupplier.Text.Trim();                         

                            Class.DbConnection.OpenConnection();

                            Class.GeneralFunctions func = new Class.GeneralFunctions();                           

                            String randomStr = func.RandomizeChar(6, 3);//Random chuỗi 5 kí tự không có kí hiện phân cách
                            DateTime presentDate = DateTime.Now;

                            //==Phiếu nhập==
                            if(suppCode == "")
                            {
                                MessageBox.Show("Chưa chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //Tìm mã nhà cung cấp dựa theo tên
                            reader = Class.DbConnection._ExecuteReader("select MaNCC from NhaCungCap where TenNCC = N'" + suppCode + "'");
                            reader.Read();
                            String supplierCode = reader.GetString(0);
                            reader.Close();
                            reader = null;

                            tblDBImports = Class.DbConnection.GetDataToTable("select * from PhieuNhap");

                            DataRow row = tblDBImports.NewRow();
                            row["MaPN"] = randomStr;
                            row["MaNCC"] = supplierCode;
                            row["Ma"] = _staffCode; //Mã của quản lý đang đăng nhập
                            row["NgayNhap"] = presentDate;
                            row["TongTienNhap"] = CalSumIm();
                            row["TinhTrangPN"] = "Đã nhập!";
                            tblDBImports.Rows.Add(row);

                            //==Chi tiết phiếu nhập==                           
                            tblDBImportsDetail = tblImports; //Lấy 5 cột mã, tên, số lượng, giá, thành tiền và dữ liệu của bảng chứa mặt hàng đã chọn

                            //Thêm cột MaPN vào bảng tblDBImportsDetail
                            DataColumn importCodeCol = new DataColumn("MaPN", System.Type.GetType("System.String"));
                            tblDBImportsDetail.Columns.Add(importCodeCol);

                            //Cập nhập mã của phiếu nhập vào bảng tblDBImportsDetail đồng thời cập nhập số lượng tồn kho của mặt hàng
                            DataRow dataRow, storeQuantity;
                            for (int i = 0; i < tblImports.Rows.Count; i++)//Lặp qua toàn bộ các mặt hàng có trong tblImports
                            {
                                dataRow = tblDBImportsDetail.Rows[i];

                                dataRow.BeginEdit();
                                dataRow["MaPN"] = randomStr;
                                dataRow.EndEdit();

                                //Tìm mặt hàng trong bảng tblDBProducts
                                storeQuantity = tblDBProduct.Rows.Find(dataRow["MaMH"].ToString());

                                int oldQuantity = func.stringToInt(storeQuantity["TonKho"].ToString());//Tồn kho ban đầu
                                int addedQuantity = func.stringToInt(dataRow["SoLuongNhap"].ToString());//Số lượng nhập

                                storeQuantity.BeginEdit();
                                storeQuantity["TonKho"] = oldQuantity + addedQuantity;//Tồn kho mới
                                storeQuantity.EndEdit();
                            }

                            //Tiến hành cập nhập 2 bảng PhieuNhap và ChiTietPN trong CSDL qua 2 DataTable
                            int checkImport = Class.DbConnection.UpdateDBThroughDTable(tblDBImports, "select * from PhieuNhap");//Tạo phiếu nhập trước
                            int checkImportDetail = Class.DbConnection.UpdateDBThroughDTable(tblDBImportsDetail, "select * from ChiTietPN");//Tạo các phiếu chi tiết 
                            int checkNewPdQuantity = Class.DbConnection.UpdateDBThroughDTable(tblDBProduct, "select MaMH, TonKho from MatHang");//Cập nhập tồn kho các mặt hàng được nhập

                            //Kiểm tra tạo phiếu nhập và chi tiết phiếu nhập thành công
                            if (checkImport > 0 && checkImportDetail > 0 && checkNewPdQuantity > 0)
                            {
                                MessageBox.Show("Tạo phiếu nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //Chuyển thời gian về dạng Ngày/Tháng/Năm giờ:phút
                                String formattedDate = String.Format("{0:dd/MM/yyyy HH:mm}", presentDate);

                                txtImMng.Text = _staffCode;
                                txtImCode.Text = randomStr;
                                txtImDate.Text = formattedDate;
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Tạo phiếu nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }
            else if(checkSelectedAction == "export")
            {
                DialogResult result = MessageBox.Show("Thực sự muốn tạo phiếu xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {                   
                    String exportReason = txtPdReason.Text.Trim();

                    Class.DbConnection.OpenConnection();

                    Class.GeneralFunctions func = new Class.GeneralFunctions();
                    Class.DbConnection dbConnection = new Class.DbConnection();

                    String randomStr = func.RandomizeChar(6, 3);//Random chuỗi 5 kí tự không có kí hiện phân cách
                    DateTime presentDate = DateTime.Now; //Lấy ngày và giờ hiện tại

                    //==Phiếu xuất==
                    tblDBExport = Class.DbConnection.GetDataToTable("select * from PhieuXuat");

                    DataRow row = tblDBExport.NewRow();
                    row["MaPX"] = randomStr;                  
                    row["Ma"] = _staffCode;
                    row["NgayXuat"] = presentDate;
                    row["TongTienXuat"] = CalSumIm();
                    row["TinhTrangPX"] = "Đã xuất!";
                    row["LyDoXuat"] = exportReason;
                    tblDBExport.Rows.Add(row);

                    //==Chi tiết phiếu xuất==
                    tblDBExportDetail = Class.DbConnection.GetDataToTable("select * from ChiTietPX");

                    //Lấy dữ liệu từ DataSource của DGV vào tblDBExportDetail
                    DataRow exportProduct, selectedProduct, storeQuantity;
                    int numRow = tblDBExportDetail.Rows.Count;

                    for(int i = 0; i < tblImports.Rows.Count; i++)
                    {
                        selectedProduct = tblImports.Rows[i]; //Bảng DataSource của DGV
                        exportProduct = tblDBExportDetail.NewRow();
                        exportProduct["MaPX"] = randomStr;
                        exportProduct["MaMH"] = selectedProduct["MaMH"].ToString().Trim();
                        exportProduct["SoLuongXuat"] = func.stringToInt(selectedProduct["SoLuongNhap"].ToString().Trim());
                        exportProduct["GiaXuat"] = func.stringToDecimal(selectedProduct["GiaNhap"].ToString().Trim());
                        exportProduct["ThanhTienXuat"] = func.stringToDecimal(selectedProduct["ThanhTienNhap"].ToString().Trim());
                        tblDBExportDetail.Rows.Add(exportProduct);

                        //Tìm mặt hàng trong bảng tblDBProducts
                        storeQuantity = tblDBProduct.Rows.Find(selectedProduct["MaMH"].ToString().Trim());

                        int oldQuantity = func.stringToInt(storeQuantity["TonKho"].ToString().Trim());//Tồn kho ban đầu
                        int lostQuantity = func.stringToInt(exportProduct["SoLuongXuat"].ToString().Trim());//Số lượng xuất
                       
                        storeQuantity.BeginEdit();
                        storeQuantity["TonKho"] = oldQuantity - lostQuantity;//Tồn kho mới
                        storeQuantity.EndEdit();
                        
                    }

                    
                    int checkExportDB = Class.DbConnection.UpdateDBThroughDTable(tblDBExport, "select * from PhieuXuat");
                    int checkExportDetailDB = Class.DbConnection.UpdateDBThroughDTable(tblDBExportDetail, "select * from ChiTietPX");
                    int checkNewPdQuantity = Class.DbConnection.UpdateDBThroughDTable(tblDBProduct, "select MaMH, TonKho, GiaMH from MatHang");

                    //Kiểm tra tạo phiếu nhập và chi tiết phiếu nhập thành công
                    if (checkExportDB > 0 && checkExportDetailDB > 0 && checkNewPdQuantity > 0)
                    {
                        MessageBox.Show("Tạo phiếu xuất hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Chuyển thời gian về dạng Ngày/Tháng/Năm giờ:phút
                        String formattedDate = String.Format("{0:dd/MM/yyyy HH:mm}", presentDate);

                        txtImMng.Text = _staffCode;
                        txtImCode.Text = randomStr;
                        txtImDate.Text = formattedDate;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Tạo phiếu xuất thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                   
                }
            }
                       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(checkSelectedAction == "import")
            {
                //Phiếu nhập cần phải được tạo trước khi hủy
                if (txtImCode.Text == "")
                {
                    MessageBox.Show("Phiếu nhập chưa được tạo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn hủy phiếu nhập! \n Sau khi đã hủy sẽ không thể thay đổi trạng thái phiếu nhập!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Class.GeneralFunctions func = new Class.GeneralFunctions();

                        //Thiết lập khóa chính cho tblDBImport để tìm phiếu nhập cần hủy                       
                        DataColumn codeCol = tblDBImports.Columns["MaPN"];
                        tblDBImports.PrimaryKey = new DataColumn[] { codeCol };
                        

                        String selectedPdCode = txtImCode.Text;
                        DataRow dataRow = tblDBImports.Rows.Find(selectedPdCode);

                        if (dataRow == null)
                        {
                            MessageBox.Show("Không tìm thấy phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            //Chuyển trạng thái của phiếu nhập sang đã hủy
                            dataRow.BeginEdit();
                            dataRow["TinhTrangPN"] = "Đã hủy";
                            dataRow.EndEdit();

                            //Cập nhập tồn kho
                            //Duyệt qua từng mặt hàng trong bảng DataSource của DGV
                            DataRow tblImportRow, storeQuantity;
                            for (int i = 0; i < tblImports.Rows.Count; i++)
                            {
                                tblImportRow = tblImports.Rows[i];

                                //Tìm mặt hàng trong bảng tblDBProducts
                                storeQuantity = tblDBProduct.Rows.Find(tblImportRow["MaMH"].ToString());

                                int oldQuantity = func.stringToInt(storeQuantity["TonKho"].ToString());//Tồn kho ban đầu
                                int addedQuantity = func.stringToInt(tblImportRow["SoLuongNhap"].ToString());//Số lượng nhập

                                storeQuantity.BeginEdit();
                                storeQuantity["TonKho"] = oldQuantity - addedQuantity;//Tồn kho mới
                                storeQuantity.EndEdit();
                            }

                            int changeImportStatus = Class.DbConnection.UpdateDBThroughDTable(tblDBImports, "select * from PhieuNhap");//Thay đổi trạng thái phiếu nhập
                            int changeProductQuantity = Class.DbConnection.UpdateDBThroughDTable(tblDBProduct, "select MaMH, TonKho from MatHang");//Cập nhập tồn kho

                            if ((changeImportStatus > 0) && (changeProductQuantity > 0))
                            {
                                MessageBox.Show("Hủy phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Xóa các mặt hàng đã chọn trong DGV
                                tblImports.Rows.Clear();

                                //Reset các TextBox
                                ResetInput();

                                ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
                                tblDBImportsDetail = null;
                                tblDBProduct = null;

                                return;
                            }
                            else
                            {
                                MessageBox.Show("Hủy phiếu nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                        return;
                }
            }
            else if(checkSelectedAction == "export")
            {
                if (txtImCode.Text == "")
                {
                    MessageBox.Show("Phiếu xuất chưa được tạo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn hủy phiếu xuất! \n Sau khi đã hủy sẽ không thể thay đổi trạng thái phiếu xuất!", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Class.GeneralFunctions func = new Class.GeneralFunctions();

                        //Thiết lập khóa chính cho tblDBExport để tìm phiếu xuất cần hủy                       
                        DataColumn codeCol = tblDBExport.Columns["MaPX"];
                        tblDBExport.PrimaryKey = new DataColumn[] { codeCol };
                        
                       
                        String canceledExport = txtImCode.Text;
                        DataRow dataRow = tblDBExport.Rows.Find(canceledExport);

                        if (dataRow == null)
                        {
                            MessageBox.Show("Không tìm thấy phiếu xuất!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        else
                        {
                            //Chuyển trạng thái của phiếu xuất sang đã hủy
                            dataRow.BeginEdit();
                            dataRow["TinhTrangPX"] = "Đã hủy";
                            dataRow.EndEdit();

                            //Cập nhập tồn kho
                            //Duyệt qua từng mặt hàng trong bảng DataSource của DGV
                            DataRow tblExportRow, storeQuantity;
                            for (int i = 0; i < tblImports.Rows.Count; i++)
                            {
                                tblExportRow = tblImports.Rows[i];

                                //Tìm mặt hàng trong bảng tblDBProducts
                                storeQuantity = tblDBProduct.Rows.Find(tblExportRow["MaMH"].ToString());

                                int oldQuantity = func.stringToInt(storeQuantity["TonKho"].ToString());//Tồn kho ban đầu
                                int lostQuantity = func.stringToInt(tblExportRow["SoLuongNhap"].ToString());//Số lượng xuất

                                storeQuantity.BeginEdit();
                                storeQuantity["TonKho"] = oldQuantity + lostQuantity;//Tồn kho mới
                                storeQuantity.EndEdit();
                            }

                            int changeExportStatus = Class.DbConnection.UpdateDBThroughDTable(tblDBExport, "select * from PhieuXuat");//Thay đổi trạng thái phiếu xuất
                            int changeProductQuantity = Class.DbConnection.UpdateDBThroughDTable(tblDBProduct, "select MaMH, TonKho, GiaMH from MatHang");//Cập nhập tồn kho

                            if ((changeExportStatus > 0) && (changeProductQuantity > 0))
                            {
                                MessageBox.Show("Hủy phiếu xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Xóa các mặt hàng đã chọn trong DGV
                                tblImports.Rows.Clear();

                                //Reset các TextBox
                                ResetInput();

                                ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
                                tblDBImportsDetail = null;                      

                                return;
                            }
                            else
                            {
                                MessageBox.Show("Hủy phiếu xuất thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }         
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(checkSelectedAction == "import")
            {
                DialogResult result = MessageBox.Show("Thực sự muốn tạo phiếu nhập mới?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DialogResult keep = MessageBox.Show("Bạn có muốn dùng lại thông tin phiếu nhập?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (keep == DialogResult.Yes)
                    {
                        String recentSupplier = cbSupplier.Text;
                        ResetInput();
                        ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
                        tblDBImportsDetail = null;              
                        cbSupplier.Text = recentSupplier;
                    }
                    else
                    {
                        ResetInput();
                        ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
                        tblImports.Rows.Clear();
                        tblDBImportsDetail = null;
                        
                    }

                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Thực sự muốn tạo phiếu xuất mới?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DialogResult keep = MessageBox.Show("Bạn có muốn dùng lại thông tin phiếu xuất?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(keep == DialogResult.Yes)
                    {
                        ResetInput();
                        ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
                    }
                    else
                    {
                        ResetInput();
                        ShowDataLVi("select MaMH, TenMH from MatHang except select MaMH, TenMH from MatHang where MaMH = 'MH_0000'");
                        tblImports = null;
                    }
                }
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
                if (checkSelectedAction == "export")
                {
                    frmImport frmImport = new frmImport("import", _frmLogin, _staffCode, _staffAuthority);
                    frmImport.Show();
                    this.Dispose(true);
                }
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
                if (checkSelectedAction == "import")
                {
                    frmImport frmImport = new frmImport("export", _frmLogin, _staffCode, _staffAuthority);
                    frmImport.Show();
                    this.Dispose(true);
                }
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
