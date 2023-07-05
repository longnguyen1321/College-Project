using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace frmMain
{
    public partial class frmMain1 : Form
    {
        //Khai báo 
        DataTable tblProduct = null;//Bảng chứa dữ liệu truy vấn mặt hàng
        int lvSelectedIndex = -1; //Chứa index đang được chọn của ListView
        //frmMain1 _frmLogin;

        public frmMain1()
        {
            InitializeComponent();
        }

        private void frmProductSelect_Load(object sender, EventArgs e)
        {
            mnuSelectProducts.Checked = true;

            GetCBoxData("select * from LoaiMH except select * from LoaiMH where MaL = 'L_0000'", "TenL", cbType);
            cbType.SelectedItem = null;

            GetCBoxData("select * from ChatLieuMH except select * from ChatLieuMH where MaCL = 'MCL_0000'", "TenCL", cbMaterial);
            cbMaterial.SelectedItem = null;
            ShowDataDGV("select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang " +
                "inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL");
        }

        //===========Các hàm xử lý===============
        //Hàm hiển thị dữ liệu bảng mặt hàng
        private void ShowDataDGV(String query)
        {
            //Mở kết nối
            Class.DbConnection.OpenConnection();

            //Thực hiện truy vấn
            tblProduct = Class.DbConnection.GetDataToTable(query);

            //Chọn nguồn dữ liệu cho bảng DGV
            dgvProduct.DataSource = tblProduct;
        }

        //Hàm điều khiển trạng thái các buttons
        private void ButtonsStatus(bool status)
        {
            btnDelete.Enabled = status;
            btnDeleteAll.Enabled = status;
            btnUpdate.Enabled = status;
            btnDeleteAll.Enabled = status;
            btnCreateBill.Enabled = status;
        }

        //Hàm kiểm tra các ô TextBox trong bảng thao tác cần nhập
        private bool ProductTextBIsValid()
        {
            bool kq = true;

            String pdCode = txtCode.Text;
            String pdName = txtName.Text;
            String pdQuantity = txtQuantity.Text.Trim();
            String pdSize = txtSize.Text.Trim();

            if (pdCode == "")
            {
                MessageBox.Show("Chưa chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kq = false;
            }
            else if (pdQuantity == "" && pdSize == "")
            {
                MessageBox.Show("Thông tin mặt hàng chưa được nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kq = false;
            }
            else if (pdSize == "")
            {
                MessageBox.Show("Chưa nhập kích cỡ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kq = false;
            }
            else if (pdQuantity == "")
            {
                MessageBox.Show("Chưa nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kq = false;
            }
            return kq;
        }

        //Hàm lấy dữ liệu cho ComboBox từ DataSource, truyền vào câu truy vấn, tên trường được show lên CB, tên CB được dùng
        private void GetCBoxData(String query, String disPlayMember, ComboBox ComboBName)
        {
            DataTable dt = Class.DbConnection.GetDataToTable(query);

            ComboBName.DataSource = dt;
            ComboBName.DisplayMember = disPlayMember;
        }

        //=================Xử lý sự kiện click trên các Controls hiển thị==================
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy Index của dòng được chọn 
            int numRow;
            numRow = e.RowIndex;

            //Gán giá trị dòng numrow, cột mã và tên vào ô Text 
            txtCode.Text = dgvProduct.Rows[numRow].Cells[0].Value.ToString().Trim();
            txtName.Text = dgvProduct.Rows[numRow].Cells[1].Value.ToString().Trim();
            txtPrice.Text = dgvProduct.Rows[numRow].Cells[2].Value.ToString().Trim();
            txtSize.Text = dgvProduct.Rows[numRow].Cells[6].Value.ToString().Trim();
        }

        private void lvSelectedProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Lấy index Item đang được chọn
            lvSelectedIndex = lvSelectedProducts.FocusedItem.Index;

            //Hiển thị dữ liệu lên các ô TextBox
            ListViewItem item = lvSelectedProducts.FocusedItem;

            txtCode.Text = item.SubItems[1].Text;
            txtName.Text = item.SubItems[2].Text;
            txtQuantity.Text = item.SubItems[3].Text;
            txtSize.Text = item.SubItems[4].Text;
            txtPrice.Text = item.SubItems[5].Text;
        }

        //=================Xử lý sự kiện các ComboBox==================
        private void cbPrice_SelectedValueChanged(object sender, EventArgs e)
        {
            //Lấy về Item được chọn trong danh sách Item
            String productPrice = cbPrice.Text.Trim();

            //Câu lệnh truy vấn ban đầu
            String rootQuery = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH " +
                "on MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL ";

            if (cbType.Text.Trim() != "")
            {
                rootQuery += "where TenL = N'" + cbType.Text.Trim() + "' ";
            }

            if (cbMaterial.Text.Trim() != "")
            {
                String checkKeyWord = "";
                if (cbType.Text.Trim() == "")
                {
                    checkKeyWord = "where ";
                }
                else
                {
                    checkKeyWord = "and ";
                }
                rootQuery += checkKeyWord + "ChatLieuMH.TenCL = N'" + cbMaterial.Text.Trim() + "' ";
            }

            if (cbSize.Text.Trim() != "")
            {
                String checkKeyWord = "";
                if (cbType.Text.Trim() == "" || cbMaterial.Text.Trim() == "")
                {
                    checkKeyWord = "where ";
                }
                else
                {
                    checkKeyWord = "and ";
                }

                rootQuery += checkKeyWord + "SizeMH = '" + cbSize.Text.Trim() + "' ";
            }

            //Thực hiện lọc theo giá để cuối do cú pháp order by
            if (productPrice == "Tăng dần")
                rootQuery += "order by GiaMH asc";
            else
                rootQuery += " order by GiaMH desc";

            //Thực hiện truy vấn và hiển thị lên DGV
            ShowDataDGV(rootQuery);
        }

        private void cbType_SelectedValueChanged(object sender, EventArgs e)
        {
            String query;

            //Mở kết nối
            Class.DbConnection.OpenConnection();

            //Lấy về Item được chọn trong danh sách Item
            String productType = cbType.Text.Trim();

            //Thực hiện truy vấn và hiển thị lên DGV
            query = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH on " +
                "MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where LoaiMH.TenL = N'" + productType + "' ";

            //Thực hiện truy vấn kết hợp với điều kiện lọc ở các ComboBox khác
            if (cbMaterial.Text.Trim() != "")
            {
                query += "and ChatLieuMH.TenCL = N'" + cbMaterial.Text.Trim() + "' ";
            }

            if (cbSize.Text.Trim() != "")
            {
                query += "and SizeMH = '" + cbSize.Text.Trim() + "' ";
            }

            if (cbPrice.Text.Trim() != "")
            {
                if (cbPrice.Text.Trim() == "Tăng dần")
                {
                    query += "order by GiaMH asc";
                }
                else
                {
                    query += "order by GiaMH desc";
                }
            }
            ShowDataDGV(query);
        }

        private void cbMaterial_SelectedValueChanged(object sender, EventArgs e)
        {
            //Lấy về Item được chọn trong danh sách Item
            String productMaterial = cbMaterial.Text.Trim();

            //Thực hiện truy vấn và trả về bảng kết quả
            String query = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where TenCL = N'" + productMaterial + "'";

            if (cbType.Text.Trim() != "")
            {
                query += "and TenL = N'" + cbType.Text.Trim() + "' ";
            }

            if (cbSize.Text.Trim() != "")
            {
                query += "and SizeMH = '" + cbSize.Text.Trim() + "' ";
            }

            if (cbPrice.Text.Trim() != "")
            {
                if (cbPrice.Text.Trim() == "Tăng dần")
                {
                    query += "order by GiaMH asc";
                }
                else
                {
                    query += "order by GiaMH desc";
                }
            }

            ShowDataDGV(query);
        }

        private void cbSize_SelectedValueChanged(object sender, EventArgs e)
        {
            //Lấy về Item được chọn trong danh sách Item
            String productSize = cbSize.Text.Trim();

            //Thực hiện truy vấn và trả về bảng kết quả
            String query = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where SizeMH = '" + productSize + "' ";

            if (cbType.Text.Trim() != "")
            {
                query += "and TenL = N'" + cbType.Text.Trim() + "' ";
            }

            if (cbMaterial.Text.Trim() != "")
            {
                query += "and ChatLieuMH.TenCL = N'" + cbMaterial.Text.Trim() + "' ";
            }


            if (cbPrice.Text.Trim() == "Tăng dần")
            {
                query += "order by GiaMH asc";
            }
            else
            {
                query += "order by GiaMH desc";
            }

            ShowDataDGV(query);
        }

        //===============Xử lý các Buttons===================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Lấy về dữ liệu được nhập
            String code = txtTypedCode.Text.Trim();
            String name = txtTypedName.Text.Trim();

            //Nếu người dùng chưa nhập thông tin 
            if (code == "" && name == "")
            {
                MessageBox.Show("Nhập thông tin cần tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (code != "")
                {
                    String query1 = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL " +
                        "inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where MaMH = '" + code + "'";
                    ShowDataDGV(query1);
                }
                else if (name != "")
                {
                    String query2 = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL " +
                        "inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where TenMH like N'%" + name + "%'";
                    ShowDataDGV(query2);
                }
                else if (code != "" && name != "")
                {
                    String query3 = "select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL " +
                        "inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where MaMH = '" + code + "' and TenMH like N'" + name + "'";
                    ShowDataDGV(query3);
                }

                if (tblProduct.Rows.Count < 1)
                {
                    MessageBox.Show("Không tìm thấy mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnResetDGV_Click(object sender, EventArgs e)
        {
            ShowDataDGV("select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang inner join " +
                "ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL");

            cbType.SelectedItem = null;
            cbMaterial.SelectedItem = null;
            cbSize.SelectedItem = null;
            cbPrice.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Kiểm tra các thông tin cần nhập
            if (ProductTextBIsValid())
            //Nếu đầy đủ thông tin cần nhập
            {
                int lvSTT = 0;

                //Lấy dữ liệu từ form
                String pdCode = txtCode.Text;
                String pdName = txtName.Text;
                String pdQuantity = txtQuantity.Text.Trim();
                String pdSize = txtSize.Text.Trim();
                String pdPrice = txtPrice.Text.Trim();

                Class.GeneralFunctions func = new Class.GeneralFunctions();

                DataTable checkInstoreQuantity = Class.DbConnection.GetDataToTable("select SoLuongMH from MatHang where MaMH = '" + pdCode + "'");
                DataRow dataRow = checkInstoreQuantity.Rows[0];
                //Kiểm tra dữ liệu có hợp lệ không.
                if (func.stringToInt(pdQuantity) == 0)
                {
                    return;
                }
                //Kiểm tra số lượng hàng hiện tại có đủ bán
                else if (func.stringToInt(pdQuantity) > func.stringToInt(dataRow["SoLuongMH"].ToString()))
                {
                    MessageBox.Show("Mặt hàng hiện tại không đủ số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //Đếm số sản phầm trong ListView
                    if (lvSelectedProducts.Items.Count == 0)
                    {
                        lvSTT = 1;
                    }
                    else
                    {
                        lvSTT = lvSelectedProducts.Items.Count + 1;
                    }

                    //Khởi tạo đối tượng ListViewItem
                    ListViewItem lvi = new ListViewItem(lvSTT.ToString());
                    lvi.SubItems.Add(pdCode);
                    lvi.SubItems.Add(pdName);
                    lvi.SubItems.Add(pdQuantity);
                    lvi.SubItems.Add(pdSize);
                    lvi.SubItems.Add(pdPrice);

                    //Thêm Item vào ListView
                    lvSelectedProducts.Items.Add(lvi);

                    //Kích hoạt các nút nếu có dữ liệu trong ListView
                    if (lvSelectedProducts.Items.Count == 1)
                    {
                        ButtonsStatus(true);
                    }
                }
            }
            else
            //Nếu không đầy đủ thông tin cần nhập.
            {
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvSelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn mặt hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                int num = lvSelectedIndex;
                int numRows = lvSelectedProducts.Items.Count;

                lvSelectedProducts.Items.RemoveAt(num);

                //Chỉnh lại Index các cột trong ListView
                //Index của các Item trong ListView bắt đầu từ 0
                //Nếu mặt hàng được xóa không phải ở cuối danh sách thì tiến hành sửa số thứ tự
                if ((num < (numRows - 1)))
                {
                    ListViewItem order;
                    for (int i = num; i < (numRows - 1); i++) //Sau khi xóa, bớt đi 1 item, index lớn nhất có thể là tổng số item ban đầu trừ đi 2
                    {
                        order = lvSelectedProducts.Items[i];
                        int j = i + 1;
                        order.SubItems[0].Text = j.ToString();
                    }
                }

                if (lvSelectedProducts.Items.Count == 0)
                {
                    ButtonsStatus(false);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lvSelectedIndex < 0)
            {
                MessageBox.Show("Chưa chọn mặt hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ListViewItem item = lvSelectedProducts.Items[lvSelectedIndex];

                item.SubItems[3].Text = txtQuantity.Text;
            }
        }

        private void btnCreateBill_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Thực sự muốn xóa mọi mặt hàng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                lvSelectedProducts.Items.Clear();
                txtCode.Text = "";
                txtName.Text = "";
                txtQuantity.Text = "";
                txtSize.Text = "";
                txtPrice.Text = "";
                txtTypedCode.Text = "";
                txtTypedName.Text = "";
                lvSelectedIndex = -1;

                cbType.SelectedValue = null;
                cbMaterial.SelectedValue = null;

                ShowDataDGV("select MaMH, TenMH, GiaMH, SoLuongMH, LoaiMH.TenL, SizeMH, TenCL, MoTaMH, AnhMH from MatHang " +
                    "inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL");
            }
        }

        private void mnuPdManage_Click(object sender, EventArgs e)
        {
            Product.frmProductManage frmProductManage = new Product.frmProductManage();
            frmProductManage.Show();
        }

        private void mnuStorage_Click(object sender, EventArgs e)
        {
            frmStorage frmStorage = new frmStorage();
            frmStorage.Show();
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            //frmImport frmImport = new frmImport(_frmLogin, "import");
            //frmImport.Show();
        }

        private void mnuExport_Click(object sender, EventArgs e)
        {
           // frmImport frmImport = new frmImport(_frmLogin, "export");
           // frmImport.Show();
        }

        private void quanrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.frmTransactionManage frmTransactionManage = new Storage.frmTransactionManage();
            frmTransactionManage.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.frmSuppliers frmSuppliers = new Storage.frmSuppliers();
            frmSuppliers.Show();
        }

        private void phiếuKiểmKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.frmStockIventory frmStockIventory = new Storage.frmStockIventory();
            frmStockIventory.ShowDialog();
        }

        private void thốngKêPhiếuKiểmKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage.frmIventoryManage frmIventoryManage = new Storage.frmIventoryManage();
            frmIventoryManage.Show();
        }
    }
}
