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

namespace BaiTapLon_LapTrinhDotNet.Product
{
    public partial class frmProductManage : Form
    {
        //==========Khai báo các biến toàn cục============
        DataTable tblProductsInfo; //Bảng để làm DataSource cho DGV
        DataTable tblProducts; //Bảng để cập nhập bảng MatHang trong CSDL
        int dgvSelectedIndex = -1; //Chứa index đang được chọn của DGV
        //frmMain1 _frmProducts;
        String dgvSelectedPdCode; //Chứa mã mặt hàng được chọn từ DGV, Phục vụ cho chức năng sửa thông tin

        public frmProductManage()
        {
            InitializeComponent();
        }
        /*
        public frmProductManage(frmMain1 frmProducts)
        {
            _frmProducts = frmProducts;
        }
        */
        private void ProductManage_Load(object sender, EventArgs e)
        {
            MessageBox.Show("hello world dsadasdasdasda");
            mnuPdManage.Checked = true;
            DefineTable(); //Định nghĩa bảng DataTable dùng để cập nhập CSDL

            ShowDataDGV("select MaMH, TenMH, LoaiMH.TenL, GiaMH, SizeMH, ChatLieuMH.TenCL, SoLuongMH, SLTonKhoToiThieu, AnhMH, MoTaMH from MatHang inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL");
            GetCBoxData("select TenL from LoaiMH except select TenL from LoaiMH where MaL = 'L_0000'", "TenL", cbCategory);
            GetCBoxData("select TenCL from ChatLieuMH except select TenCL from ChatLieuMH where MaCL = 'MCL_0000'", "TenCL", cbMaterial);
            cbMaterial.Text = ""; //Gía trị ban đầu của combobox là rỗng
            cbCategory.Text = ""; //Gía trị ban đầu của combobox là rỗng
        }

        private void mnuPdManage_Click(object sender, EventArgs e)
        {
            frmProductManage frmProductManage = new frmProductManage();
            frmProductManage.Show();

        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSelectedIndex = e.RowIndex;
            txtCode.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim();
            cbCategory.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[2].Value.ToString().Trim();
            txtPrice.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[3].Value.ToString().Trim();
            cbSize.SelectedItem = dgvProducts.Rows[dgvSelectedIndex].Cells[4].Value.ToString().Trim();
            cbMaterial.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[5].Value.ToString().Trim();
            txtQuantity.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[6].Value.ToString().Trim();
            txtMinAmount.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[7].Value.ToString().Trim();
            txtDescription.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[9].Value.ToString().Trim();

            String size = dgvProducts.Rows[dgvSelectedIndex].Cells[4].Value.ToString().Trim();
            if (size == "S" || size == "M" || size == "L") //Nếu kích cỡ có 1 chữ
            {
                txtName.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim().Substring(0,
                    dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim().Length - 4); //Bỏ đuôi kích cỡ cuối tên mặt hàng
            }
            else if (size == "XXL")
            {
                txtName.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim().Substring(0,
                    dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim().Length - 6); //Bỏ đuôi kích cỡ cuối tên mặt hàng
            }
            else
            {
                txtName.Text = dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim().Substring(0,
                    dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim().Length - 5); //Bỏ đuôi kích cỡ cuối tên mặt hàng
            }


            //Lấy mã mặt hàng đang được chọn trong DGV
            dgvSelectedPdCode = dgvProducts.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim();
        }
        //==========Các hàm xử lý=============
        private void ShowDataDGV(String query)
        {
            //Mở kết nối
            Class.DbConnection.OpenConnection();

            //Thực hiện truy vấn
            tblProductsInfo = Class.DbConnection.GetDataToTable(query); //Bảng DataSource cho DGV

            if (tblProductsInfo.Rows.Count < 1)
            {
                MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //Chọn nguồn dữ liệu cho bảng DGV
                dgvProducts.DataSource = tblProductsInfo;
            }
        }

        //Hàm lấy dữ liệu từ cột được chọn trong DataTable truy vấn được cho ComboBox được truyền vào
        private void GetCBoxData(String query, String disPlayMember, ComboBox ComboBName)
        {
            DataTable dt = Class.DbConnection.GetDataToTable(query);

            ComboBName.DataSource = dt;
            ComboBName.DisplayMember = disPlayMember;
        }

        //Hàm kiểm tra các textbox cần nhập đã được nhập
        private bool CheckIfTextBoxsIsFilled()
        {
            bool result = true;
            String name = txtName.Text.Trim();
            String category = cbCategory.Text.Trim();
            String size = cbSize.Text.Trim();
            String material = cbMaterial.Text.Trim();
            String quantity = txtQuantity.Text.Trim();
            String minQuantity = txtMinAmount.Text.Trim();
            String price = txtPrice.Text.Trim();
            String imagePath = txtImage.Text.Trim();

            if (name == "")
            {
                MessageBox.Show("Chưa nhập tên mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (category == "")
            {
                MessageBox.Show("Chưa nhập loại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (material == "")
            {
                MessageBox.Show("Chưa nhập chất liệu mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (size == "")
            {
                MessageBox.Show("Chưa nhập kích cỡ mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (quantity == "")
            {
                MessageBox.Show("Chưa nhập số lượng mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (minQuantity == "")
            {
                MessageBox.Show("Chưa nhập số lượng tồn kho tối thiểu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (price == "")
            {
                MessageBox.Show("Chưa nhập giá mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }
            else if (imagePath == "")
            {
                MessageBox.Show("Chưa nhập ảnh mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
            }

            return result;
        }

        //Hàm kiểm tra dữ liệu được nhập có hợp lệ
        private bool CheckIfInputValid()
        {
            bool result = true;
            String quantity = txtQuantity.Text.Trim();
            String minQuantity = txtMinAmount.Text.Trim();
            String price = txtPrice.Text.Trim();
            String imagePath = txtImage.Text.Trim();

            Class.GeneralFunctions func = new Class.GeneralFunctions();
            int quantityNum = func.stringToInt(quantity);
            int minQuantityNum = func.stringToInt(minQuantity);
            decimal priceNum = func.stringToDecimal(price);
            byte[] image = func.ImageToBytes(imagePath);
            if (quantityNum == -1 || quantityNum < 0)
            {
                MessageBox.Show("Số lượng mặt hàng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (minQuantityNum == -1 || quantityNum < 0)
            {
                MessageBox.Show("Số lượng tồn kho tối thiếu không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (priceNum == -1m || priceNum < 0)
            {
                MessageBox.Show("Giá bán nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            else if (image == null)
            {
                result = false;
            }

            return result;
        }

        private void DefineTable()
        {
            tblProducts = null;
            tblProducts = Class.DbConnection.GetDataToTable("select * from MatHang");

            //Thiết lập khóa chính cho bảng tblProducts
            DataColumn c = tblProducts.Columns["MaMH"];
            tblProducts.PrimaryKey = new DataColumn[] { c };
        }

        //==========Xử lý buttons=============
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            //Set các type ảnh có thể chọn
            ofd.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.Title = "Chọn ảnh minh họa cho mặt hàng!";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Hiển thị lên ImageBox
                picBProduct.Image = Image.FromFile(ofd.FileName);
                txtImage.Text = ofd.FileName;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String code = txtCode.Text.Trim();
            String name = txtName.Text.Trim();
            String category = cbCategory.Text.Trim();
            String size = cbSize.Text.Trim();
            String material = cbMaterial.Text.Trim();
            String quantity = txtQuantity.Text.Trim();
            String minQuantity = txtMinAmount.Text.Trim();
            String price = txtPrice.Text.Trim();
            String imagePath = txtImage.Text.Trim();
            String description = txtDescription.Text.Trim();

            if (CheckIfInputValid() != true || CheckIfTextBoxsIsFilled() != true)
                return;
            else
            {
                DialogResult dialogResult = MessageBox.Show("Thực sự muốn thêm mặt hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Class.GeneralFunctions func = new Class.GeneralFunctions();
                    SqlDataReader reader;

                    //Lấy mã chất liệu và mã loại mặt hàng dựa vào tên tương ứng được nhập trong TextBox 
                    reader = Class.DbConnection._ExecuteReader("select MaCL from ChatLieuMH where TenCL = '" + material + "'");
                    reader.Read();
                    String materialName = reader.GetString(0);
                    reader.Close();

                    reader = Class.DbConnection._ExecuteReader("select MaL from LoaiMH where TenL = '" + category + "'");
                    reader.Read();
                    String categoryName = reader.GetString(0);
                    reader.Close();

                    //Nếu người dùng không nhập mã thì hệ thống tự tạo mã
                    if (code == "")
                    {
                        code = func.RandomizeChar(6, 0);
                    }

                    //Định nghĩa 1 DataRow mới với các dữ liệu được nhập và thêm vào bảng tblProducts
                    DataRow dataRow = tblProducts.NewRow();
                    dataRow["MaMH"] = code;
                    dataRow["TenMH"] = name + " - " + size;
                    dataRow["LoaiMH"] = categoryName;
                    dataRow["SizeMH"] = size;
                    dataRow["ChatLieuMH"] = materialName;
                    dataRow["SoLuongMH"] = func.stringToInt(quantity);
                    dataRow["TonKho"] = 0;
                    dataRow["SLTonKhoToiThieu"] = func.stringToInt(minQuantity);
                    dataRow["GiaMH"] = func.stringToDecimal(price);
                    dataRow["AnhMH"] = func.ImageToBytes(imagePath);
                    dataRow["MoTaMH"] = description;

                    tblProducts.Rows.Add(dataRow);

                    //Cập nhập CSDL thông qua DataTable tblProducts
                    int addedRow = Class.DbConnection.UpdateDBThroughDTable(tblProducts, "select * from MatHang");

                    //Kiểm tra kết quả thêm
                    if (addedRow != 0)
                    {
                        MessageBox.Show("Thêm mới mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Hiển thị lại dữ liệu DGV
                        ShowDataDGV("select MaMH, TenMH, LoaiMH.TenL, GiaMH, SizeMH, ChatLieuMH.TenCL, SoLuongMH, SLTonKhoToiThieu, AnhMH, MoTaMH from MatHang inner join " +
                            "LoaiMH on MatHang.LoaiMH = LoaiMH.MaL inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL");

                        dgvSelectedIndex = -1; //Không có mặt hàng nào được chọn
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới mặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //Nếu thêm mặt hàng thất bại và DataTable đã bị chỉnh sửa, ta truy vấn lại bảng dữ liệu cho DataTable dùng để cập nhập CSDL
                        DefineTable();
                        return;
                    }
                }
                else
                    return;

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Kiểm tra người dùng đã chọn mặt hàng cần xóa trên DGV
            if (dgvSelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn mặt hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                String code = txtCode.Text.Trim();
                String name = txtName.Text.Trim();
                String category = cbCategory.Text.Trim();
                String size = cbSize.Text.Trim();
                String material = cbMaterial.Text.Trim();
                String quantity = txtQuantity.Text.Trim();
                String minQuantity = txtMinAmount.Text.Trim();
                String price = txtPrice.Text.Trim();
                String imagePath = txtImage.Text.Trim();
                String description = txtDescription.Text.Trim();

                if (CheckIfInputValid() != true || CheckIfTextBoxsIsFilled() != true)
                    return;
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự thay đổi thông tin mặt hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Class.GeneralFunctions func = new Class.GeneralFunctions();
                        SqlDataReader reader;

                        //Lấy mã chất liệu và mã loại mặt hàng dựa vào tên tương ứng được nhập trong TextBox 
                        reader = Class.DbConnection._ExecuteReader("select MaCL from ChatLieuMH where TenCL = N'" + material + "'");
                        reader.Read();
                        String materialCode = reader.GetString(0);
                        reader.Close();

                        reader = Class.DbConnection._ExecuteReader("select MaL from LoaiMH where TenL = N'" + category + "'");
                        reader.Read();
                        String categoryCode = reader.GetString(0);
                        reader.Close();

                        //Nếu mã để trống thì hệ thống tự tạo
                        if (code == "")
                        {
                            code = func.RandomizeChar(6, 0);
                        }

                        String pdDGVSize = dgvProducts.Rows[dgvSelectedIndex].Cells[4].Value.ToString().Trim();
                        String pdDGVName = dgvProducts.Rows[dgvSelectedIndex].Cells[1].Value.ToString().Trim();
                        String pdDGVCode = dgvProducts.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim();
                        //Sửa lại kích cỡ sau tên nếu tên bị sửa
                        if (code != pdDGVCode || name != pdDGVName || size != pdDGVSize)
                        {
                            if (size != pdDGVSize)
                            {
                                if (pdDGVSize == "XS" || pdDGVSize == "XL" || pdDGVSize == "XL")
                                {
                                    name = pdDGVName.Substring(0, pdDGVName.Length - 5) + " - " + size;
                                }
                                else if (pdDGVSize == "S" || pdDGVSize == "M" || pdDGVSize == "L")
                                {
                                    name = pdDGVName.Substring(0, pdDGVName.Length - 4) + " - " + size;
                                }
                                else //Kích cỡ là XXL
                                {
                                    name = pdDGVName.Substring(0, pdDGVName.Length - 6) + " - " + size;
                                }
                            }



                            //Kiểm tra mã và tên mặt hàng có bị trùng
                            DataTable checkReplicate = Class.DbConnection.GetDataToTable("select MaMH, TenMH from MatHang where MaMH = '" + code + "' or TenMH = N'" + name + "'");

                            DataRow dataRow1 = null, dataRow2 = null;
                            if (checkReplicate.Rows.Count == 1) //Nếu bảng tồn tại dòng
                            {
                                dataRow1 = checkReplicate.Rows[0];
                            }
                            if (checkReplicate.Rows.Count == 2) //Nếu bảng có 2 dòng
                            {
                                dataRow2 = checkReplicate.Rows[1];
                            }

                            //Nếu là mã bị thay đổi
                            if (code != pdDGVCode)
                            {
                                if (dataRow1 != null && dataRow1["MaMH"].ToString().Trim() == code || dataRow2 != null && dataRow2["MaMH"].ToString().Trim() == code)
                                {
                                    MessageBox.Show("Mã mặt hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            //Nếu là tên bị thay đổi
                            else if (name != pdDGVCode)
                            {
                                if (dataRow1 != null && dataRow1["TenMH"].ToString().Trim() == code || dataRow2 != null && dataRow2["TenMH"].ToString().Trim() == code)
                                {
                                    MessageBox.Show("Tên mặt hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            //Cập nhập mã mặt hàng các bảng có MaMH là khóa ngoại thành mã mặt hàng rỗng 'MH_0000'
                            DataTable tblFKeyImport = Class.DbConnection.GetDataToTable("select MaPN, MaMH from ChiTietPN where MaMH = '" + pdDGVCode + "'");
                            DataTable tblFKeyExport = Class.DbConnection.GetDataToTable("select MaPX, MaMH from ChiTietPX where MaMH = '" + pdDGVCode + "'");
                            DataTable tblFKeyIventory = Class.DbConnection.GetDataToTable("select MaPKK, MaMH from ChiTietPKK where MaMH = '" + pdDGVCode + "'");

                            if (pdDGVCode != code) //Nếu mã bị thay đổi
                            {
                                if (tblFKeyImport.Rows.Count > 0) //Bảng chi tiết phiếu nhập
                                {
                                    foreach (DataRow tblRow in tblFKeyImport.Rows)
                                    {
                                        tblRow.BeginEdit();
                                        tblRow["MaMH"] = "MH_0000";
                                        tblRow.EndEdit();
                                    }
                                    Class.DbConnection.UpdateDBThroughDTable(tblFKeyImport, "select MaPN, MaMH from ChiTietPN");
                                }


                                if (tblFKeyExport.Rows.Count > 0) //Bảng chi tiết phiếu xuất
                                {
                                    foreach (DataRow tblRow in tblFKeyExport.Rows)
                                    {
                                        tblRow.BeginEdit();
                                        tblRow["MaMH"] = "MH_0000";
                                        tblRow.EndEdit();
                                    }
                                    Class.DbConnection.UpdateDBThroughDTable(tblFKeyExport, "select MaPX, MaMH from ChiTietPX");
                                }

                                if (tblFKeyIventory.Rows.Count > 0) //Bảng chi tiết phiếu kiểm kho
                                {
                                    foreach (DataRow tblRow in tblFKeyIventory.Rows)
                                    {
                                        tblRow.BeginEdit();
                                        tblRow["MaMH"] = "MH_0000";
                                        tblRow.EndEdit();
                                    }
                                    Class.DbConnection.UpdateDBThroughDTable(tblFKeyIventory, "select MaPKK, MaMH from ChiTietPKK");
                                }
                            }

                            DataRow updatedProduct = tblProducts.Rows.Find(pdDGVCode);

                            updatedProduct.BeginEdit();
                            updatedProduct["MaMH"] = code;
                            updatedProduct["TenMH"] = name;
                            updatedProduct["LoaiMH"] = categoryCode; //Mã loại được lấy từ tên loại mặt hàng được chọn
                            updatedProduct["SizeMH"] = size;
                            updatedProduct["ChatLieuMH"] = materialCode; //Tên chất liệu được lấy từ tên chất liệu được chọn
                            updatedProduct["SoLuongMH"] = func.stringToInt(quantity);
                            updatedProduct["SLTonKhoToiThieu"] = func.stringToInt(minQuantity);
                            updatedProduct["GiaMH"] = func.stringToDecimal(price);
                            updatedProduct["AnhMH"] = func.ImageToBytes(imagePath); //Chuyển từ dạng ảnh sang dạng nhị phân 
                            updatedProduct["MoTaMH"] = description;
                            updatedProduct.EndEdit();

                            int checkIfPdChanged = Class.DbConnection.UpdateDBThroughDTable(tblProducts, "select * from MatHang");

                            if (checkIfPdChanged > 0)
                            {
                                if (pdDGVCode != code) //Nếu mã bị thay đổi cập nhập thành mã mặt hàng mới
                                {
                                    if (tblFKeyImport.Rows.Count > 0)
                                    {
                                        foreach (DataRow tblRow in tblFKeyImport.Rows)
                                        {
                                            tblRow.BeginEdit();
                                            tblRow["MaMH"] = code;
                                            tblRow.EndEdit();
                                        }
                                        Class.DbConnection.UpdateDBThroughDTable(tblFKeyImport, "select MaPN, MaMH from ChiTietPN");
                                    }

                                    if (tblFKeyExport.Rows.Count > 0)
                                    {
                                        foreach (DataRow tblRow in tblFKeyExport.Rows)
                                        {
                                            tblRow.BeginEdit();
                                            tblRow["MaMH"] = code;
                                            tblRow.EndEdit();
                                        }
                                        Class.DbConnection.UpdateDBThroughDTable(tblFKeyExport, "select MaPX, MaMH from ChiTietPX");
                                    }

                                    if (tblFKeyIventory.Rows.Count > 0)
                                    {
                                        foreach (DataRow tblRow in tblFKeyIventory.Rows)
                                        {
                                            tblRow.BeginEdit();
                                            tblRow["MaMH"] = code;
                                            tblRow.EndEdit();
                                        }
                                        Class.DbConnection.UpdateDBThroughDTable(tblFKeyIventory, "select MaPKK, MaMH from ChiTietPKK");
                                    }
                                }

                                MessageBox.Show("Cập nhập thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //Hiển thi DGV với CSDL mới được cập nhập
                                ShowDataDGV("select MaMH, TenMH, LoaiMH.TenL, GiaMH, SizeMH, ChatLieuMH.TenCL, SoLuongMH, SLTonKhoToiThieu, AnhMH, MoTaMH from MatHang inner join " +
                                "LoaiMH on MatHang.LoaiMH = LoaiMH.MaL inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL");

                                dgvSelectedIndex = -1;
                            }
                            else
                            {
                                MessageBox.Show("Cập nhập thông tin thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                //Nếu SỬA mặt hàng thất bại và DataTable đã bị chỉnh sửa, ta truy vấn lại bảng dữ liệu cho DataTable dùng để cập nhập CSDL
                                DefineTable();
                            }
                        }
                        //Nếu mã/tên/kích cỡ không bị sửa
                        else if (code == pdDGVCode && name == pdDGVName && size == pdDGVSize && category == dgvProducts.Rows[dgvSelectedIndex].Cells[2].Value.ToString().Trim() && material == dgvProducts.Rows[dgvSelectedIndex].Cells[5].Value.ToString().Trim()
                            && quantity == dgvProducts.Rows[dgvSelectedIndex].Cells[6].Value.ToString().Trim() && minQuantity == dgvProducts.Rows[dgvSelectedIndex].Cells[7].Value.ToString().Trim() && price == dgvProducts.Rows[dgvSelectedIndex].Cells[2].Value.ToString().Trim()
                            && description == dgvProducts.Rows[dgvSelectedIndex].Cells[9].Value.ToString().Trim())
                        {
                            MessageBox.Show("Mặt hàng không có thông tin bị thay đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Kiểm tra người dùng đã chọn mặt hàng cần xóa trên DGV
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Chưa chọn mặt hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Thực sự muốn xóa mặt hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    String pdDGVCode = dgvProducts.Rows[dgvSelectedIndex].Cells[0].Value.ToString().Trim();

                    //Tìm dòng trong bảng tblProducts có khóa chính là Mã mặt hàng được chọn 
                    DataRow dataRow = tblProducts.Rows.Find(pdDGVCode);


                    //Cập nhập mã mặt hàng các bảng có MaMH là khóa ngoại thành mã mặt hàng rỗng 'MH_0000'
                    DataTable tblFKeyImport = Class.DbConnection.GetDataToTable("select MaPN, MaMH from ChiTietPN where MaMH = '" + pdDGVCode + "'");
                    DataTable tblFKeyExport = Class.DbConnection.GetDataToTable("select MaPX, MaMH from ChiTietPX where MaMH = '" + pdDGVCode + "'");
                    DataTable tblFKeyIventory = Class.DbConnection.GetDataToTable("select MaPKK, MaMH from ChiTietPKK where MaMH = '" + pdDGVCode + "'");

                    if (tblFKeyImport.Rows.Count > 0) //Bảng chi tiết phiếu nhập
                    {
                        foreach (DataRow tblRow in tblFKeyImport.Rows)
                        {
                            tblRow.BeginEdit();
                            tblRow["MaMH"] = "MH_0000";
                            tblRow.EndEdit();
                        }
                        Class.DbConnection.UpdateDBThroughDTable(tblFKeyImport, "select MaPN, MaMH from ChiTietPN");
                    }


                    if (tblFKeyExport.Rows.Count > 0) //Bảng chi tiết phiếu xuất
                    {
                        foreach (DataRow tblRow in tblFKeyExport.Rows)
                        {
                            tblRow.BeginEdit();
                            tblRow["MaMH"] = "MH_0000";
                            tblRow.EndEdit();
                        }
                        Class.DbConnection.UpdateDBThroughDTable(tblFKeyExport, "select MaPX, MaMH from ChiTietPX");
                    }

                    if (tblFKeyIventory.Rows.Count > 0) //Bảng chi tiết phiếu kiểm kho
                    {
                        foreach (DataRow tblRow in tblFKeyIventory.Rows)
                        {
                            tblRow.BeginEdit();
                            tblRow["MaMH"] = "MH_0000";
                            tblRow.EndEdit();
                        }
                        Class.DbConnection.UpdateDBThroughDTable(tblFKeyIventory, "select MaPKK, MaMH from ChiTietPKK");
                    }

                    dataRow.Delete();

                    //Cập nhập CSDL thông qua DataTable tblProducts
                    int num = Class.DbConnection.UpdateDBThroughDTable(tblProducts, "select * from MatHang");

                    //Kiểm tra kết quả thêm
                    if (num != 0)
                    {
                        MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Hiển thị lại DGV
                        ShowDataDGV("select MaMH, TenMH, LoaiMH.TenL, GiaMH, SizeMH, ChatLieuMH.TenCL, SoLuongMH, SLTonKhoToiThieu, AnhMH, MoTaMH from MatHang inner join " +
                            "LoaiMH on MatHang.LoaiMH = LoaiMH.MaL inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL");

                        dgvSelectedIndex = -1;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Xóa mặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //Nếu xóa mặt hàng thất bại và DataTable đã bị chỉnh sửa, ta truy vấn lại bảng dữ liệu cho DataTable dùng để cập nhập CSDL
                        DefineTable();
                        return;
                    }


                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtName.Text = "";
            cbCategory.Text = "";
            cbSize.Text = "";
            cbMaterial.Text = "";
            txtQuantity.Text = "";
            txtMinAmount.Text = "";
            txtPrice.Text = "";
            txtImage.Text = "";
            txtDescription.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String typedText = txtSearch.Text.Trim();
            if (typedText == "")
            {
                MessageBox.Show("Chưa điền thông tin cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String query = "select MaMH, TenMH, LoaiMH.TenL, GiaMH, SizeMH, ChatLieuMH.TenCL, SoLuongMH, SLTonKhoToiThieu, AnhMH, " +
                    "MoTaMH from MatHang inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL inner join ChatLieuMH on MatHang.ChatLieuMH = " +
                    "ChatLieuMH.MaCL where MaMH = '" + typedText + "' or TenMH like N'%" + typedText + "%'";

                ShowDataDGV(query);
            }
        }



        private void btnaddCategory_Click(object sender, EventArgs e)
        {
            frmCateMeta frmMaterial = new Product.frmCateMeta("Loai");
            frmMaterial.Show();
        }

        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            frmCateMeta frmMaterial = new Product.frmCateMeta("ChatLieu");
            frmMaterial.Show();
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            ShowDataDGV("select MaMH, TenMH, LoaiMH.TenL, GiaMH, SizeMH, ChatLieuMH.TenCL, SoLuongMH, SLTonKhoToiThieu, AnhMH, " +
                "MoTaMH from MatHang inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL");
            dgvSelectedIndex = -1;
        }

        private void frmProductManage_Load(object sender, EventArgs e)
        {

        }
    }
}
