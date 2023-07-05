using frmMain.HRM;
using frmMain.Product;
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

namespace frmMain.Storage
{
    public partial class frmTransactionManage : Form
    {
        //Khai báo các biến toàn cục
        DataTable tblDGVImportDetails;
        DataTable tblDGVExportDetails;
        String selectedAction;
        SqlDataReader reader;

        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản

        public frmTransactionManage()
        {
            InitializeComponent();
        }

        public frmTransactionManage(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmTransactionManage_Load(object sender, EventArgs e)
        {
            mnuTransactionsManage.Checked = true;

            Class.DbConnection.OpenConnection();

            ShowDataLVi("select MaPN, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap order by NgayNhap desc");

            selectedAction = "PhieuNhap";

            DefineTblDGVImportDetails();
            DefineTblDGVExportDetails();
        }

        private void frmTransactionManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn thực sự muốn đóng ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(selectedAction == "PhieuNhap")
            {
                ShowDataLVi("select MaPN, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap where MaPN = '" + txtSearchCode.Text.Trim() + "'");
                if(lviTransactions.Items.Count == 0)
                {
                    MessageBox.Show("Phiếu nhập không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowDataLVi("select MaPN, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap order by NgayNhap desc");
                }
            }
            else if(selectedAction == "PhieuXuat")
            {
                ShowDataLVi("select MaPX, NgayXuat, TongTienXuat, TinhTrangPX from PhieuXuat where MaPX = '" + txtSearchCode.Text.Trim() + "'");
                if(lviTransactions.Items.Count == 0)
                {
                    MessageBox.Show("Phiếu xuất không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowDataLVi("select MaPX, NgayXuat, TongTienXuat, TinhTrangPX from PhieuXuat order by NgayXuat desc");
                }
            }
        }

        //=====Các hàm xử lý dữ liệu=====
        private void ShowDataLVi(String query)
        {
            //Xóa các Items trong ListView
            lviTransactions.Items.Clear();

            //Thực thi truy vấn và trả về đầu đọc
            reader = Class.DbConnection._ExecuteReader(query);

            String code;
            String datetime;
            String money;
            String status;
            //Show các dữ liệu truy xuất được lên ListView
            while (reader != null && reader.Read())
            {
                code = reader.GetString(0);
                datetime = String.Format("{0:dd/MM/yyyy hh:mm}",reader.GetDateTime(1));
                money = String.Format("{0:0,0.00}",reader.GetDecimal(2));
                status = reader.GetString(3);
                 
                ListViewItem lvi = new ListViewItem(code);
                lvi.SubItems.Add(datetime);
                lvi.SubItems.Add(money);
                lvi.SubItems.Add(status);

                lviTransactions.Items.Add(lvi);
            }

            if(reader != null)
            {
                reader.Close();
            }
        }

        private void DefineTblDGVImportDetails()
        {
            tblDGVImportDetails = new DataTable();

            DataColumn pdCode = new DataColumn("Mã mặt hàng", System.Type.GetType("System.String"));
            DataColumn pdName = new DataColumn("Tên mặt hàng", System.Type.GetType("System.String"));
            DataColumn importQuantity = new DataColumn("Số lượng nhập", System.Type.GetType("System.Int32"));
            DataColumn importPrice = new DataColumn("Giá nhập", System.Type.GetType("System.String"));
            DataColumn importUnitTotal = new DataColumn("Thành tiền nhập", System.Type.GetType("System.String"));

            tblDGVImportDetails.Columns.Add(pdCode);
            tblDGVImportDetails.Columns.Add(pdName);
            tblDGVImportDetails.Columns.Add(importPrice);
            tblDGVImportDetails.Columns.Add(importQuantity);
            tblDGVImportDetails.Columns.Add(importUnitTotal);
        }

        private void DefineTblDGVExportDetails()
        {
            tblDGVExportDetails = new DataTable();

            DataColumn pdCode = new DataColumn("Mã mặt hàng", System.Type.GetType("System.String"));
            DataColumn pdName = new DataColumn("Tên mặt hàng", System.Type.GetType("System.String"));
            DataColumn exportQuantity = new DataColumn("Số lượng xuất", System.Type.GetType("System.Int32"));
            DataColumn exportPrice = new DataColumn("Giá xuất", System.Type.GetType("System.String"));
            DataColumn exportUnitTotal = new DataColumn("Thành tiền xuất", System.Type.GetType("System.String"));

            tblDGVExportDetails.Columns.Add(pdCode);
            tblDGVExportDetails.Columns.Add(pdName);
            tblDGVExportDetails.Columns.Add(exportQuantity);
            tblDGVExportDetails.Columns.Add(exportPrice);
            tblDGVExportDetails.Columns.Add(exportUnitTotal);
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            if(selectedAction == "PhieuNhap")
            {
                ShowDataLVi("select MaPN, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap order by NgayNhap desc");
            }
            else 
            {
                ShowDataLVi("select MaPX, NgayXuat, TongTienXuat, TinhTrangPX from PhieuXuat order by NgayXuat desc");
            }
        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(selectedAction == "PhieuNhap")
            {
                String query = "select MaPN, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap ";

                if(cbTime.Text.Trim() == "Trong ngày")
                {
                    query += "where DATEDIFF(Day,'" + DateTime.Now + "', NgayNhap) = 0"; //Khoảng cách giữa DateTime.Now và NgayNhap bằng 0
                }

                else if(cbTime.Text.Trim() == "Trong tuần")
                {
                    query += "where DATEDIFF(Day,'" + DateTime.Now + "', NgayNhap) > (-7)";
                }

                else if(cbTime.Text.Trim() == "Trong tháng")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayNhap) = 0";
                }

                else if(cbTime.Text.Trim() == "3 tháng gần nhất")
                {              
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayNhap) > (-3)";
                }

                else if (cbTime.Text.Trim() == "6 tháng gần nhất")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayNhap) > (-6)";
                }

                else if (cbTime.Text.Trim() == "12 tháng gần nhất")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayNhap) > (-12)";
                }

                ShowDataLVi(query + " order by NgayNhap");
            }
            else
            {
                String query = "select MaPX, NgayXuat, TongTienXuat, TinhTrangPX from PhieuXuat ";

                if(cbTime.Text.Trim() == "Trong ngày")
                {
                    query += "where DATEDIFF(Day,'" + DateTime.Now + "', NgayXuat) = 0";
                }

                if (cbTime.Text.Trim() == "Trong tuần")
                {
                    query += "where DATEDIFF(Day,'" + DateTime.Now + "', NgayXuat) > (-7)";
                }

                if (cbTime.Text.Trim() == "Trong tháng")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayXuat) = 0";
                }

                if (cbTime.Text.Trim() == "3 tháng gần nhất")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayXuat) > (-3)";
                }

                if (cbTime.Text.Trim() == "6 tháng gần nhất")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayXuat) > (-6)";
                }

                if (cbTime.Text.Trim() == "12 tháng gần nhất")
                {
                    query += "where DATEDIFF(Month,'" + DateTime.Now + "', NgayXuat) > (-12)";
                }

                ShowDataLVi(query + " order by NgayXuat");
            }
        }

        private void lviTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lviTransactions.Items.Count == 0)
            {
                return;
            }
            else
            {
                String selectedPdCode;
                foreach (ListViewItem items in lviTransactions.SelectedItems)
                {
                    selectedPdCode = items.SubItems[0].Text.Trim();

                    if (selectedAction == "PhieuNhap")
                    {
                        reader = Class.DbConnection._ExecuteReader("select MaPN, TenNCC, Ma, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap inner join NhaCungCap on PhieuNhap.MaNCC = NhaCungCap.MaNCC where MaPN = '" + selectedPdCode + "'");

                        if (reader != null)
                        {
                            reader.Read();

                            txtType.Text = "Phiếu nhập";
                            txtCode.Text = reader.GetString(0);
                            txtImportSupplier.Text = reader.GetString(1);
                            txtCreatedBy.Text = reader.GetString(2);
                            txtDate.Text = String.Format("{0:dd/MM/yyyy hh:MM}", reader.GetDateTime(3));
                            txtTransactionTotal.Text = String.Format("{0:0,0.00 VNĐ}", reader.GetDecimal(4));
                            txtStatus.Text = reader.GetString(5);
                            txtExportReason.Text = "Không có lý do xuất!";

                            reader.Close();
                        }
                    }
                    else
                    {
                        reader = Class.DbConnection._ExecuteReader("select MaPX, Ma, NgayXuat, TongTienXuat, LyDoXuat, TinhTrangPX from PhieuXuat where MaPX = '" + selectedPdCode + "'");

                        if (reader != null)
                        {
                            reader.Read();

                            txtType.Text = "Phiếu xuất";
                            txtCode.Text = reader.GetString(0);
                            txtImportSupplier.Text = "Không có nhà cung cấp!";
                            txtCreatedBy.Text = reader.GetString(1);
                            txtDate.Text = String.Format("{0:dd/MM/yyyy hh:MM}", reader.GetDateTime(2));
                            txtTransactionTotal.Text = String.Format("{0:0,0.00 VNĐ}", reader.GetDecimal(3));
                            txtExportReason.Text = reader.GetString(4);
                            txtStatus.Text = reader.GetString(5);

                            reader.Close();
                        }
                    }
                }               
            }
            
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            if(txtCode.Text.Trim() != "")
            {
                if (selectedAction == "PhieuNhap")
                {
                    reader = Class.DbConnection._ExecuteReader("select MatHang.MaMH, TenMH, SoLuongNhap, GiaNhap, ThanhTienNhap from ChiTietPN inner join MatHang on ChiTietPN.MaMH = MatHang.MaMH where MaPN = '" + txtCode.Text + "'");

                    if (reader != null)
                    {
                        tblDGVImportDetails.Rows.Clear();
                        DataRow importDetailRow;
                        while (reader.Read())
                        {
                            importDetailRow = tblDGVImportDetails.NewRow();
                            importDetailRow["Mã mặt hàng"] = reader.GetString(0);
                            importDetailRow["Tên mặt hàng"] = reader.GetString(1);
                            importDetailRow["Số lượng nhập"] = reader.GetInt32(2);
                            importDetailRow["Giá nhập"] = String.Format("{0: 0,0.00}", reader.GetDecimal(3));
                            importDetailRow["Thành tiền nhập"] = String.Format("{0: 0,0.00}", reader.GetDecimal(4));
                            tblDGVImportDetails.Rows.Add(importDetailRow);
                        }

                        reader.Close();

                        dgvDetails.DataSource = tblDGVImportDetails;
                        //Chỉnh độ rộng các cột của DGV
                        dgvDetails.Columns[0].Width = 80;
                        dgvDetails.Columns[1].Width = 150;
                    }
                }
                else
                {
                    reader = Class.DbConnection._ExecuteReader("select MatHang.MaMH, TenMH, SoLuongXuat, GiaXuat, ThanhTienXuat from ChiTietPX inner join MatHang on ChiTietPX.MaMH = MatHang.MaMH where MaPX = '" + txtCode.Text + "'");

                    if (reader != null)
                    {
                        tblDGVExportDetails.Rows.Clear();
                        DataRow exportDetailRow;
                        while (reader.Read())
                        {
                            exportDetailRow = tblDGVExportDetails.NewRow();
                            exportDetailRow["Mã mặt hàng"] = reader.GetString(0);
                            exportDetailRow["Tên mặt hàng"] = reader.GetString(1);
                            exportDetailRow["Số lượng xuất"] = reader.GetInt32(2);
                            exportDetailRow["Giá xuất"] = String.Format("{0: 0,0.00}", reader.GetDecimal(3));
                            exportDetailRow["Thành tiền xuất"] = String.Format("{0: 0,0.00}", reader.GetDecimal(4));
                            tblDGVExportDetails.Rows.Add(exportDetailRow);
                        }

                        reader.Close();

                        dgvDetails.DataSource = tblDGVExportDetails;
                        //Chỉnh độ rộng các cột của DGV
                        dgvDetails.Columns[0].Width = 80;
                        dgvDetails.Columns[1].Width = 150;
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn phiếu cần hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (selectedAction == "PhieuNhap")
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn hủy phiếu nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Class.GeneralFunctions func = new Class.GeneralFunctions();

                        //Tìm phiếu nhập
                        DataTable tblDeleteDBImport;
                        tblDeleteDBImport = Class.DbConnection.GetDataToTable("select MaPN, TinhTrangPN from PhieuNhap where MaPN = '" + txtCode.Text.Trim() + "'");
                        if (tblDeleteDBImport.Rows.Count == 0) //Nếu không tìm thấy
                        {
                            MessageBox.Show("Phiếu nhập không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //Cập nhập tình trạng phiếu nhập thành đã hủy
                            DataRow cancelImportRow = tblDeleteDBImport.Rows[0];

                            //Nếu phiếu nhập đã bị hủy từ trước
                            if (cancelImportRow["TinhTrangPN"].ToString().Trim() == "Đã hủy!")
                            {
                                MessageBox.Show("Tình trạng phiếu nhập đã bị hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                cancelImportRow.BeginEdit();
                                cancelImportRow["TinhTrangPN"] = "Đã hủy!";
                                cancelImportRow.EndEdit();

                                //Cập nhập tồn kho của các mặt hàng có trong ChiTietPN của phiếu nhập bị hủy
                                DataTable tblImportDetailsUpdate = Class.DbConnection.GetDataToTable("select MatHang.MaMH, TonKho, SoLuongNhap from MatHang inner join ChiTietPN on MatHang.MaMH = ChiTietPN.MaMH where MaPN ='" + txtCode.Text.Trim() + "'");
                                String errorPdList = ""; //Chứa danh sách các mặt hàng số lượng tồn kho không hợp lệ
                                foreach (DataRow dataRow in tblImportDetailsUpdate.Rows)
                                {
                                    if (func.stringToInt(dataRow["SoLuongNhap"].ToString().Trim()) > func.stringToInt(dataRow["TonKho"].ToString().Trim()))
                                    {
                                        if (errorPdList != "") //Nếu là mặt hàng đầu tiền không hợp lệ
                                        {
                                            errorPdList += ", '" + dataRow["MaMH"].ToString().Trim() + "'";
                                        }
                                        else
                                        {
                                            errorPdList += "'" + dataRow["MaMH"].ToString().Trim() + "'";
                                        }
                                        errorPdList += dataRow["MaMH"].ToString().Trim() + ", ";
                                    }
                                    else
                                    {
                                        dataRow.BeginEdit();
                                        dataRow["TonKho"] = func.stringToInt(dataRow["TonKho"].ToString().Trim()) - func.stringToInt(dataRow["SoLuongNhap"].ToString().Trim());
                                        dataRow.EndEdit();
                                    }
                                }

                                //Nếu không có mặt hàng có số lượng tồn kho không hợp lệ, tiến hành cập nhập CSDL
                                if (errorPdList == "")
                                {
                                    int checkUpdateImportDetails = Class.DbConnection.UpdateDBThroughDTable(tblImportDetailsUpdate, "select MaMH, TonKho from MatHang");
                                    int checkImport = Class.DbConnection.UpdateDBThroughDTable(tblDeleteDBImport, "select MaPN, TinhTrangPN from PhieuNhap");

                                    if (checkImport > 0 && checkUpdateImportDetails > 0)
                                    {
                                        MessageBox.Show("Hủy phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Các mặt hàng " + errorPdList + " không đủ số lượng trong kho!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                        }
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn hủy phiếu xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Class.GeneralFunctions func = new Class.GeneralFunctions();

                        //Tìm phiếu xuất
                        DataTable tblDBCancelExport;
                        tblDBCancelExport = Class.DbConnection.GetDataToTable("select MaPX, TinhTrangPX from PhieuXuat where MaPX = '" + txtCode.Text.Trim() + "'");
                        if (tblDBCancelExport.Rows.Count == 0) //Nếu không tìm thấy
                        {
                            MessageBox.Show("Phiếu xuất không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //Cập nhập tình trạng phiếu nhập thành đã hủy
                            DataRow cancelExportRow = tblDBCancelExport.Rows[0];

                            //Nếu phiếu xuất đã bị hủy từ trước
                            if (cancelExportRow["TinhTrangPX"].ToString().Trim() == "Đã hủy!")
                            {
                                MessageBox.Show("Không thể hủy! \n Tình trạng phiếu xuất đã bị hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                cancelExportRow.BeginEdit();
                                cancelExportRow["TinhTrangPX"] = "Đã hủy!";
                                cancelExportRow.EndEdit();

                                //Cập nhập tồn kho của các mặt hàng có trong ChiTietPX của phiếu xuất bị hủy
                                DataTable tblExportDetailsUpdate = Class.DbConnection.GetDataToTable("select MatHang.MaMH, TonKho, SoLuongXuat from MatHang inner join ChiTietPX on MatHang.MaMH = ChiTietPX.MaMH where MaPX ='" + txtCode.Text.Trim() + "'");
                                foreach (DataRow dataRow in tblExportDetailsUpdate.Rows)
                                {
                                    dataRow.BeginEdit();
                                    dataRow["TonKho"] = func.stringToInt(dataRow["TonKho"].ToString().Trim()) + func.stringToInt(dataRow["SoLuongXuat"].ToString().Trim());
                                    dataRow.EndEdit();
                                }

                                int checkUpdateImportDetails = Class.DbConnection.UpdateDBThroughDTable(tblExportDetailsUpdate, "select MaMH, TonKho from MatHang");
                                int checkImport = Class.DbConnection.UpdateDBThroughDTable(tblDBCancelExport, "select MaPX, TinhTrangPX from PhieuXuat");

                                if (checkImport > 0 && checkUpdateImportDetails > 0)
                                {
                                    MessageBox.Show("Hủy phiếu xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }                          
                        }
                    }
                }
            }
        }

        private void radioImport_CheckedChanged(object sender, EventArgs e)
        {
            if(selectedAction == "PhieuNhap") //Nếu radio đã được chọn từ trước thì return
            {
                return;
            }
            else
            {
                selectedAction = "PhieuNhap"; //Hành động được chọn hiện tại
                ShowDataLVi("select MaPN, NgayNhap, TongTienNhap, TinhTrangPN from PhieuNhap order by NgayNhap desc");
            }
            
        }

        private void radioExport_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedAction == "PhieuXuat") //Nếu radio đã được chọn từ trước thì return
            {
                return;
            }
            else
            {
                selectedAction = "PhieuXuat"; //Hành động được chọn hiện tại
                ShowDataLVi("select MaPX, NgayXuat, TongTienXuat, TinhTrangPX from PhieuXuat order by NgayXuat desc");
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
