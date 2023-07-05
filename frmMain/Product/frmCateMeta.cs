using frmMain.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.Product
{
    public partial class frmCateMeta : Form
    {
        //Khai báo
        DataTable tblObject;
        DataTable tblUpdateDBPd;
        String action;
        frmProductManage _frmProductManage; //Chứa form quản lý mặt hàng hiện tại
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản
        String pdCode, pdName, pdType, pdMaterial, pdSize, pdQuantity, pdMinQuantity, pdPrice; //Chứa thông tin mặt hàng đang tương tác trong Form frmProductManage
        public frmCateMeta()
        {
            InitializeComponent();
        }

        public frmCateMeta(String selectedAction, frmProductManage frmProductManage, frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();

            action = selectedAction;
            if (selectedAction == "ChatLieu")
            {
                radioBtnMaterial.Checked = true;
                GetCBoxData("select * from ChatLieuMH except select * FROM ChatLieuMH WHERE MaCL = 'MCL_0000'", "TenCL", cbDeleteObject);
            }
            else if (selectedAction == "Loai")
            {
                radioBtnCategory.Checked = true;
                GetCBoxData("select * from LoaiMH except select * FROM LoaiMH WHERE MaL = 'L_0000'", "TenL", cbDeleteObject);
            }

            _frmProductManage = frmProductManage;
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority; 
        }

        //Constructor để lấy hành động người dùng
        public frmCateMeta(String selectedAction) : this()
        {
            action = selectedAction;
            if(selectedAction == "ChatLieu")
            {
                radioBtnMaterial.Checked = true;
                GetCBoxData("select * from ChatLieuMH except select * FROM ChatLieuMH WHERE MaCL = 'MCL_0000'", "TenCL", cbDeleteObject);
            }
            else if(selectedAction == "Loai")
            {
                radioBtnCategory.Checked = true;
                GetCBoxData("select * from LoaiMH except select * FROM LoaiMH WHERE MaL = 'L_0000'", "TenL", cbDeleteObject);
            } 
        }

        private void frmMaterial_Load(object sender, EventArgs e)
        {
            //GetCBoxData("select * from ChatLieuMH", "TenCL", cbDeleteObject);
            cbDeleteObject.Text = "";
        }

        private void frmCateMeta_FormClosed(object sender, FormClosedEventArgs e)
        { 
            //_frmProductManage = new frmProductManage(_frmLogin, _staffCode, _staffAuthority); 
            //_frmProductManage.Show(); //Hiển thị Form quản lý mặt hàng với danh sách chất liệu hoặc mặt hàng mới
        }

        private void GetCBoxData(String query, String disPlayMember, ComboBox ComboBName)
        {
            tblObject = Class.DbConnection.GetDataToTable(query);

            ComboBName.DataSource = tblObject;
            ComboBName.DisplayMember = disPlayMember;
        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            if (radioBtnMaterial.Checked)
            {
                String materialName = txtAddObject.Text;

                if (materialName == "")
                {
                    MessageBox.Show("Chưa nhập chất liệu cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn thêm chất liệu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        bool check = Class.DbConnection.CheckKey("select TenCL from ChatLieuMH where TenCL = '" + materialName + "'");
                        if (check)
                        {
                            MessageBox.Show("Chất liệu đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            Class.GeneralFunctions func = new Class.GeneralFunctions();
                            String randomizedCode = func.RandomizeChar(5, 0);

                            DataRow addedRow = tblObject.NewRow();
                            addedRow["MaCL"] = randomizedCode;
                            addedRow["TenCL"] = materialName;

                            tblObject.Rows.Add(addedRow);

                            int numRow = Class.DbConnection.UpdateDBThroughDTable(tblObject, "select * from ChatLieuMH");

                            if (numRow < 1)
                            {
                                MessageBox.Show("Thêm chất liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tblObject = Class.DbConnection.GetDataToTable("select * from ChatLieuMH");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Thêm chất liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                }
            }
            else //Nếu radio button được chọn là Loại
            {
                String categoryName = txtAddObject.Text;
                if (categoryName == "")
                {
                    MessageBox.Show("Chưa nhập loại mặt hàng cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn thêm loại mặt hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        bool check = Class.DbConnection.CheckKey("select TenL from LoaiMH where TenL = '" + categoryName + "'");
                        if (check)
                        {
                            MessageBox.Show("Loại mặt hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            Class.GeneralFunctions func = new Class.GeneralFunctions();
                            String randomizedCode = func.RandomizeChar(5, 0);

                            DataRow addedRow = tblObject.NewRow();
                            addedRow["MaL"] = randomizedCode;
                            addedRow["TenL"] = categoryName;

                            tblObject.Rows.Add(addedRow);

                            int numRow = Class.DbConnection.UpdateDBThroughDTable(tblObject, "select * from LoaiMH");

                            if (numRow < 1)
                            {
                                MessageBox.Show("Thêm loại mặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tblObject = Class.DbConnection.GetDataToTable("select * from ChatLieuMH");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Thêm loại mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //Không cần truy vấn lại bảng tblObject vì DataSource của ComboBox vẫn là đối tượng DataTable cũ
                                return;
                            }
                        }
                    }

                }
            }
            
        }

        private void btnDeleteObject_Click(object sender, EventArgs e)
        {
            if (radioBtnMaterial.Checked)
            {
                String selectedMaterial = cbDeleteObject.Text;

                if (selectedMaterial == "")
                {
                    MessageBox.Show("Chưa chọn chất liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn xóa chất liệu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        //Kiểm tra có tồn tại chất liệu để xóa không
                        tblObject = Class.DbConnection.GetDataToTable("select * from ChatLieuMH where TenCL = N'" + selectedMaterial + "'");

                        //Kiểm tra có mặt hàng nào sử dụng chất liệu không
                        DataTable checkProductMaterial = Class.DbConnection.GetDataToTable("select MaMH, MatHang.ChatLieuMH from MatHang inner join ChatLieuMH on MatHang.ChatLieuMH = ChatLieuMH.MaCL where ChatLieuMH.TenCL = N'" + selectedMaterial + "'");

                        if (tblObject.Rows.Count < 1)
                        {
                            MessageBox.Show("Chất liệu không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            //Nếu có mặt hàng sử dụng đối tượng chất liệu này
                            if (checkProductMaterial.Rows.Count > 0)
                            {
                                //Xác nhận
                                DialogResult dialogResult1;
                                if (checkProductMaterial.Rows.Count == 1)
                                {
                                    DataRow dataRow = checkProductMaterial.Rows[0];
                                    dialogResult1 = MessageBox.Show("Mặt hàng " + dataRow["MaMH"] + "đang sử dụng chất liệu này! \n Bạn muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                }
                                else
                                {
                                    dialogResult1 = MessageBox.Show("Tồn tại các mặt hàng đang sử dụng chất liệu này! \n Bạn muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                }

                                //Thay đổi chất liệu của từng mặt hàng sử dụng chất liệu bị xóa thành chất liệu rỗng
                                if (dialogResult1 == DialogResult.Yes)
                                {
                                    foreach (DataRow dataRow in checkProductMaterial.Rows)
                                    {
                                        dataRow.BeginEdit();
                                        dataRow["ChatLieuMH"] = "MCL_0000"; //Chất liệu rỗng
                                        dataRow.EndEdit();
                                    }
                                    int checkPdIfChanged = Class.DbConnection.UpdateDBThroughDTable(checkProductMaterial, "select MaMH, ChatLieuMH from MatHang");
                                }
                            }
                            
                                    
                            //Xóa dòng đầu tiên của bảng vừa truy vấn tìm theo tên
                            tblObject.Rows[0].Delete();

                            int numRow = Class.DbConnection.UpdateDBThroughDTable(tblObject, "select * from ChatLieuMH");

                            if (numRow > 0)
                            {
                                MessageBox.Show("Xóa chất liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GetCBoxData("select * from ChatLieuMH", "TenCL", cbDeleteObject);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Xóa chất liệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                GetCBoxData("select * from ChatLieuMH", "TenCL", cbDeleteObject);
                                return;
                            }
                                    
                                
                            
                        }
                    }
                }  
            }
            else //=====Nếu radio button được chọn là Loại======//
            {
                String selectedCategory = cbDeleteObject.Text.Trim();

                if (selectedCategory == "")
                {
                    MessageBox.Show("Chưa chọn loại mặt hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Thực sự muốn xóa loại mặt hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        //Kiểm tra có tồn tại chất liệu để xóa không
                        tblObject = Class.DbConnection.GetDataToTable("select * from LoaiMH where TenL = N'" + selectedCategory + "'");

                        //Kiểm tra có mặt hàng nào sử dụng chất liệu không
                        DataTable checkProductCategory = Class.DbConnection.GetDataToTable("select MaMH, MatHang.LoaiMH from MatHang inner join LoaiMH on MatHang.LoaiMH = LoaiMH.MaL where LoaiMH.TenL = N'" + selectedCategory + "'");

                        if (tblObject.Rows.Count < 1)
                        {
                            MessageBox.Show("Loại mặt hàng không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            //Nếu có mặt hàng sử dụng đối tượng loại mặt hàng này
                            if (checkProductCategory.Rows.Count > 0)
                            {
                                //Xác nhận
                                DialogResult dialogResult1;
                                if (checkProductCategory.Rows.Count == 1)
                                {
                                    DataRow dataRow = checkProductCategory.Rows[0];
                                    dialogResult1 = MessageBox.Show("Mặt hàng " + dataRow["MaMH"] + "đang được xếp vào loại này! \n Bạn muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                }
                                else
                                {
                                    dialogResult1 = MessageBox.Show("Tồn tại các mặt hàng đang được xếp vào loại này! \n Bạn muốn tiếp tục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                }

                                if (dialogResult1 == DialogResult.Yes)
                                {
                                    //Thay đổi loại mặt hàng của từng mặt hàng sử dụng loại mặt hàng bị xóa thành loại mặt hàng rỗng
                                    foreach (DataRow dataRow in checkProductCategory.Rows)
                                    {
                                        dataRow.BeginEdit();
                                        dataRow["LoaiMH"] = "L_0000"; //Loại mặt hàng rỗng
                                        dataRow.EndEdit();
                                    }

                                    int checkPdCateChanged = Class.DbConnection.UpdateDBThroughDTable(checkProductCategory, "select MaMH, LoaiMH from MatHang");
                                }
                            }

                            //Xóa dòng đầu tiên của bảng vừa truy vấn tìm theo tên
                            tblObject.Rows[0].Delete();

                            int numRow = Class.DbConnection.UpdateDBThroughDTable(tblObject, "select * from LoaiMH");

                            if (numRow > 0)
                            {
                                MessageBox.Show("Xóa loại mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GetCBoxData("select * from LoaiMH", "TenL", cbDeleteObject);

                            }
                            else
                            {
                                MessageBox.Show("Xóa loại mặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                GetCBoxData("select * from LoaiMH", "TenL", cbDeleteObject);

                            }
                        }
                    }
                }
            }

        }

        private void radioBtnMaterial_CheckedChanged(object sender, EventArgs e)
        {
            GetCBoxData("select * from ChatLieuMH except select * FROM ChatLieuMH WHERE MaCL = 'MCL_0000'", "TenCL", cbDeleteObject);
            cbDeleteObject.Text = "";           
        }

        private void radioBtnCategory_CheckedChanged(object sender, EventArgs e)
        {   
            GetCBoxData("select * from LoaiMH except select * FROM LoaiMH WHERE MaL = 'L_0000'", "TenL", cbDeleteObject);
            cbDeleteObject.Text = "";
        }
    
    }
}
