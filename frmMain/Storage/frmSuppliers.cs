using frmMain.HRM;
using frmMain.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace frmMain.Storage
{
    public partial class frmSuppliers : Form
    {
        //Khai báo
        int dgvSelectedIndex = -1;
        DataTable tblSuppliers;
        DataTable tblUpdateDBFKeyTable;
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản

        public frmSuppliers()
        {
            InitializeComponent();
        }
        
        public frmSuppliers(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }
        
        private void frmSuppliers_Load(object sender, EventArgs e)
        {
            //Mở kết nối
            Class.DbConnection.OpenConnection();

            mnuSuppliers.Checked = true;
            
            ShowDataDGV("select * from NhaCungCap except select * from NhaCungCap where MaNCC = 'NCC_0000'");
        }
        private void frmSuppliers_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn muốn đóng ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        //========Các hàm xử lý===========
        //Hàm hiển thị các nhà cung cấp lên DGV
        private bool ShowDataDGV(String query)
        {
            bool result = false;

            //Thực hiện truy vấn
            tblSuppliers = Class.DbConnection.GetDataToTable(query);

            //Nếu có dữ liệu trong bảng DataTable 
            if(tblSuppliers.Rows.Count > 0)
            {
                //Chọn nguồn dữ liệu cho bảng DGV
                dgvSuppliers.DataSource = tblSuppliers;
                result = true;
            }            
            else
                MessageBox.Show("Không tồn tại!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);

            return result;
        }

        //Hàm kiểm tra các ô TextBox cần nhập đã được nhập
        private bool CheckTextBFilled()
        {
            bool check;
            if (txtSupCode.Text == "")
            {
                MessageBox.Show("Chưa điền mã nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                check = false;
            }
            else if (txtSupName.Text == "")
            {
                MessageBox.Show("Chưa điền tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                check = false;
            }
            else if (txtSupAddress.Text == "")
            {
                MessageBox.Show("Chưa điền địa chỉ nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                check = false;
            }
            else if (txtSupContact.Text == "")
            {
                MessageBox.Show("Chưa điền số điện thoại nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                check = false;
            }
            else if (txtSupCode.Text != "" && txtSupName.Text != "" && txtSupAddress.Text != "" && txtSupContact.Text != "")
            {
                check = true;
            }
            else
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                check = false;
            }    

            return check;
        }

        //=======Xử lý các sự kiện click trên các controls hiển thị=============
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSelectedIndex = e.RowIndex; //Lấy Index dòng đang được chọn
            txtSupCode.Text = dgvSuppliers.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim();
            txtSupName.Text = dgvSuppliers.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim();
            txtSupAddress.Text = dgvSuppliers.Rows[dgvSelectedIndex].Cells[2].Value.ToString().Trim();
            txtSupContact.Text = dgvSuppliers.Rows[dgvSelectedIndex].Cells[3].Value.ToString().Trim();
        }

        //============Xử lý các buttons===========
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ form về
            String code = txtSupCode.Text.Trim();
            String name = txtSupName.Text.Trim();
            String addr = txtSupAddress.Text.Trim();
            String phone = txtSupContact.Text.Trim();

            //Kiểm tra dữ liệu đã được điền
            bool textB = CheckTextBFilled();
            if(textB)
            {
                Class.GeneralFunctions func = new Class.GeneralFunctions();
                bool checkPhone;

                //Kiểm tra Mã có bị trùng và Formar sdt có hợp lệ
                checkPhone = func.CheckIfContactValid(phone);

                if(code == "")
                {
                    code = func.RandomizeChar(6, 0);
                }

                //Kiểm tra tên và mã nhà cung cấp có bị trùng
                DataTable tblCheckReplicates = Class.DbConnection.GetDataToTable("select * from NhaCungCap where MaNCC = '" + code + "' or TenNCC like N'%" + name + "%'");
                DataRow row1 = null;
                DataRow row2 = null;
                int tblCheckReplicatesNumRow = tblCheckReplicates.Rows.Count;

                //Chỉ có thể có tối đa 2 nhà cung cấp bị trùng, 1 là trùng mã nhà cung cấp, 2 là trùng tên là khóa unique
                if (tblCheckReplicatesNumRow == 1)
                {
                    row1 = tblCheckReplicates.Rows[0];
                }
                else if(tblCheckReplicatesNumRow == 2)
                {
                    row2 = tblCheckReplicates.Rows[1];
                }
                
                if ((row1 != null && code == row1["MaNCC"].ToString().Trim()) ||(row2 != null && code == row2["MaNCC"].ToString().Trim()))
                {
                    MessageBox.Show("Mã nhà cung cấp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if ((row1 != null && name == row1["TenNCC"].ToString().Trim()) || (row2 != null && name == row2["TenNCC"].ToString().Trim()))
                {
                    MessageBox.Show("Tên nhà cung cấp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                else if (checkPhone == true)
                {
                    DataRow addedRow = tblSuppliers.NewRow();
                    addedRow["MaNCC"] = code;
                    addedRow["TenNCC"] = name;
                    addedRow["DiaChiNCC"] = addr;
                    addedRow["LienLacNCC"] = phone;

                    tblSuppliers.Rows.Add(addedRow);

                    int u = Class.DbConnection.UpdateDBThroughDTable(tblSuppliers, "select * from NhaCungCap");
                    if (u > 0)
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvSelectedIndex = -1; //Không có nhà cung cấp nào được chọn
                    }
                    else
                        MessageBox.Show("Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    return;
            }          
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSelectedIndex == -1 || dgvSelectedIndex > (tblSuppliers.Rows.Count - 1))
                MessageBox.Show("Chưa chọn đối tượng xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult answer = MessageBox.Show("Thực sự muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(answer == DialogResult.Yes)
                {
                    //Xóa dòng được chọn trên DGV trong DataTable
                    DataRow delRow = tblSuppliers.Rows[dgvSelectedIndex];

                    int numRows; //Biến chứa kết quả update bảng CSDL NhaCungCap

                    //Cập nhập các phiếu nhập có nhà cung cấp đang bị xóa
                    tblUpdateDBFKeyTable = Class.DbConnection.GetDataToTable("select MaPN, MaNCC from PhieuNhap where MaNCC = '" + dgvSuppliers.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim() + "'");
                    if(tblUpdateDBFKeyTable.Rows.Count > 0) //Nếu tồn tại PhieuNhap có mã nhà cung cấp đang được xóa
                    {
                        DialogResult answer2 = MessageBox.Show("Tồn tại phiếu nhập đang sử dụng nhà cung cấp này! \nBạn muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if(answer2 == DialogResult.Yes)
                        {
                            foreach (DataRow dataRow in tblUpdateDBFKeyTable.Rows)
                            {
                                dataRow["MaNCC"] = "NCC_0000"; //Là mã loại mặt hàng rỗng
                            }

                            int importSubRow = Class.DbConnection.UpdateDBThroughDTable(tblUpdateDBFKeyTable, "select MaPN, MaNCC from PhieuNhap where MaPN = '" + dgvSuppliers.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim() + "'");

                            //Nếu cập nhập bảng PhieuNhap thành công thì tiến hành cập nhập bảng NhaCungCap
                            if (importSubRow > 0)
                            {
                                delRow.Delete();

                                //Cập nhập CSDL qua DataTable tblSuppliers
                                //Cập nhập bảng chứa khóa ngoại trước                    
                                numRows = Class.DbConnection.UpdateDBThroughDTable(tblSuppliers, "select * from NhaCungCap");

                                //Kiểm tra hành động cập nhập có thành công không
                                if (numRows > 0)
                                {
                                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgvSelectedIndex = -1;
                                }
                                else
                                    MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        
                    }
                    //Nếu không tồn tại mặt hàng sử dụng đối tượng nhà cung cấp này
                    else
                    {
                        delRow.Delete();

                        //Cập nhập CSDL qua DataTable tblSuppliers                   
                        numRows = Class.DbConnection.UpdateDBThroughDTable(tblSuppliers, "select * from NhaCungCap");

                        //Kiểm tra hành động cập nhập có thành công không
                        if (numRows > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSelectedIndex == -1 || dgvSelectedIndex > (tblSuppliers.Rows.Count - 1))
            {
                MessageBox.Show("Chưa chọn đối tượng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Thực sự muốn thay đổi thông tin nhà cung cấp?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
                if (dialogResult == DialogResult.Yes)
                {
                    Class.GeneralFunctions func = new Class.GeneralFunctions();
                    int n;
                    bool checkPhone;

                    //Lấy dữ liệu ban đầu trên form
                    String code = txtSupCode.Text.Trim();
                    String name = txtSupName.Text.Trim();
                    String addr = txtSupAddress.Text.Trim();
                    String phone = txtSupContact.Text.Trim();

                    //Nếu người dùng bỏ trống mã nhà cung cấp thì hệ thống tự tạo
                    if (code == "")
                    {
                        code = func.RandomizeChar(6, 0);
                    }

                    //Kiểm tra Mã có bị trùng và Formar sdt có hợp lệ
                    checkPhone = func.CheckIfContactValid(phone);
                    //Kiểm tra tên và mã nhà cung cấp có bị trùng
                    DataTable tblCheckReplicates = Class.DbConnection.GetDataToTable("select * from NhaCungCap where MaNCC = '" + code + "' or TenNCC = N'" + name + "'");
                    DataRow row1 = null;
                    DataRow row2 = null;
                    int tblCheckReplicatesNumRow = tblCheckReplicates.Rows.Count;

                    //Chỉ có thể có tối đa 2 nhà cung cấp bị trùng, 1 là trùng mã nhà cung cấp, 2 là trùng tên là khóa unique là tên
                    if (tblCheckReplicatesNumRow == 1)
                    {
                        row1 = tblCheckReplicates.Rows[0];
                    }
                    if (tblCheckReplicatesNumRow == 2) ///Nếu tồn tại dòng 2 của DataTable
                    {
                        row2 = tblCheckReplicates.Rows[1];
                    }

                    String dgvCode = dgvSuppliers.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim();
                    String dgvName = dgvSuppliers.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim();

                    //Nếu người dùng thay đổi mã nhà cung cấp và mã nhà cung cấp bị trùng
                    if (code != dgvCode || name != dgvName) //Nếu khác giá trị trên DGV(Gía trị ban đầu của mã và tên)
                    {
                        if(code != dgvCode) //Nếu mã bị thay đổi, kiểm tra trùng
                        {
                            if (row1 != null && code == row1["MaNCC"].ToString().Trim() || row2 != null && code == row2["MaNCC"].ToString().Trim())
                            {
                                MessageBox.Show("Mã nhà cung cấp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }                       
                        
                        if(name != dgvName) //Nếu tên bị thay đổi, kiểm tra trùng
                        {
                            if (row1 != null && code == row1["TenNCC"].ToString().Trim() || row2 != null && code == row2["TenNCC"].ToString().Trim())
                            {
                                MessageBox.Show("Tên nhà cung cấp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }                      
                       
                        if(checkPhone == true) //Nếu mã và tên nhà cung cấp mới không bị trùng và số điện thoại hợp lệ
                        {
                            //Cập nhập các phiếu nhập sử dụng đối tượng là nhà cung cấp
                            tblUpdateDBFKeyTable = Class.DbConnection.GetDataToTable("select MaNCC, MaPN from PhieuNhap where MaNCC = '" + dgvCode + "'");

                            if (tblUpdateDBFKeyTable.Rows.Count > 0)
                            {
                                //Cho mã nhà cung cấp trong các phiếu nhập thành NCC_0000 = Nhà cung cấp rỗng
                                foreach (DataRow dataRow in tblUpdateDBFKeyTable.Rows)
                                {
                                    dataRow["MaNCC"] = "NCC_0000";
                                }

                                int checkImportSubUpdate = Class.DbConnection.UpdateDBThroughDTable(tblUpdateDBFKeyTable, "select MaNCC, MaPN from PhieuNhap where MaNCC = '" + dgvCode + "'");

                                if (checkImportSubUpdate > 0)
                                {
                                    DataRow updateRow = tblSuppliers.Rows[dgvSelectedIndex];
                                    updateRow.BeginEdit();
                                    updateRow["MaNCC"] = code;
                                    updateRow["TenNCC"] = name;
                                    updateRow["DiaChiNCC"] = addr;
                                    updateRow["LienLacNCC"] = phone;
                                    updateRow.EndEdit();

                                    //Cập nhập CSDL qua DataTable
                                    n = Class.DbConnection.UpdateDBThroughDTable(tblSuppliers, "select * from NhaCungCap");

                                    //Cập nhập mã nhà cung cấp mới của phiếu nhập
                                    //Cho những giá trị 
                                    foreach (DataRow dataRow in tblUpdateDBFKeyTable.Rows)
                                    {
                                        dataRow["MaNCC"] = code;
                                    }

                                    int checkFinalImportSubUpdate = Class.DbConnection.UpdateDBThroughDTable(tblUpdateDBFKeyTable, "select MaNCC, MaPN from PhieuNhap where MaNCC = '" + dgvCode + "'");

                                    //Kiểm tra hành động cập nhập có thành công không                                                              
                                    if (n > 0 && checkFinalImportSubUpdate > 0)
                                    {
                                        MessageBox.Show("Cập nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dgvSelectedIndex = -1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cập nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        tblSuppliers = Class.DbConnection.GetDataToTable("select * from NhaCungCap"); 
                                    }

                                }
                                else //Nếu cập nhập bảng chưa khoán ngoại thất bại
                                {
                                    MessageBox.Show("Cập nhập thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    tblSuppliers = Class.DbConnection.GetDataToTable("select * from NhaCungCap");
                                }

                            }

                            //Nếu không tồn tại phiếu nhập sử sử dụng nhà cung cấp
                            else
                            {
                                DataRow updateRow = tblSuppliers.Rows[dgvSelectedIndex];
                                updateRow.BeginEdit();
                                updateRow["MaNCC"] = code;
                                updateRow["TenNCC"] = name;
                                updateRow["DiaChiNCC"] = addr;
                                updateRow["LienLacNCC"] = phone;
                                updateRow.EndEdit();

                                //Cập nhập CSDL qua DataTable
                                n = Class.DbConnection.UpdateDBThroughDTable(tblSuppliers, "select * from NhaCungCap");

                                //Kiểm tra hành động cập nhập có thành công không
                                if (n > 0)
                                {
                                    MessageBox.Show("Cập nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Cập nhập không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    
                    //Nếu mã và tên nhà cung cấp không bị thay đổi so với ban đầu
                    else if(code == dgvCode && name == dgvName && addr == dgvSuppliers.Rows[dgvSelectedIndex].Cells[2].Value.ToString().Trim() && phone == dgvSuppliers.Rows[dgvSelectedIndex].Cells[3].Value.ToString().Trim())
                    {
                        MessageBox.Show("Nhà cung cấp không có thông tin bị thay đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Lấy về dữ liệu được nhập
            String supInfo = txtSearchInfo.Text.Trim();

            //Nếu người dùng chưa nhập thông tin 
            if (supInfo == null)
            {
                MessageBox.Show("Chưa nhập thông tin cần tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                
                String query = "select * from NhaCungCap where MaNCC = '" + supInfo + "' or TenNCC like N'%" + supInfo + "%' except select * from NhaCungCap where MaNCC = 'NCC_0000'";
                ShowDataDGV(query);               

                if (tblSuppliers.Rows.Count < 1)
                {
                    MessageBox.Show("Nhà cung cấp không tồn tại!");

                }
            }
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            ShowDataDGV("select * from NhaCungCap except select * from NhaCungCap where MaNCC = 'NCC_0000'");
            dgvSelectedIndex = -1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtSearchInfo.Text = "";
            txtSupAddress.Text = "";
            txtSupCode.Text = "";
            txtSupContact.Text = "";
            txtSupName.Text = "";

            ShowDataDGV("select * from NhaCungCap except select * from NhaCungCap where MaNCC = 'NCC_0000'");
            dgvSelectedIndex = -1;
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
