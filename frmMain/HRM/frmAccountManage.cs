using frmMain.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.HRM
{
    public partial class frmAccountManage : Form
    {
        frmLogin _frmLogin; //Để chứa form gốc đang chạy ban đầu là form login
        String _staffCode; //Chứa mã nhân viên đang đăng nhập
        String _staffAuthority; //Chứa quyền của tài khoản
        public frmAccountManage()
        {
            InitializeComponent();
        }

        public frmAccountManage(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmAccountManage_Load(object sender, EventArgs e)
        {
            mnuAccountManage.Checked = true;
            Load_TaiKhoan();
            reset_data();
        }

        private void frmAccountManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn thực sự muốn đóng ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        //load du lieu len form tai khoan
        private void Load_TaiKhoan()
        {
            string sql = "select * from TaiKhoan";
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery(sql);
            dtTaiKhoan.DataSource = dt;
            txtTenTK.Focus();

        }
        private void reset_data()
        {
            txtMaNV.Text = txtMatKhau.Text = txtTenTK.Text = txtQuyen.Text = " ";

        }

        private void dtTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dtTaiKhoan.CurrentRow.Index;
            txtTenTK.Text = dtTaiKhoan.Rows[i].Cells[0].Value.ToString();

            txtMatKhau.Text = dtTaiKhoan.Rows[i].Cells[1].Value.ToString();
            txtMaNV.Text = dtTaiKhoan.Rows[i].Cells[2].Value.ToString();
            txtQuyen.Text = dtTaiKhoan.Rows[i].Cells[3].Value.ToString().Trim();
            txtMaNV.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "INSERT INTO TaiKhoan(TenTaiKhoan,MatKhau,MaNV, Quyen) VALUES (N'" + txtTenTK.Text.ToString().Trim() + "','"
                    + txtMatKhau.Text.ToString().Trim() + "','" + txtMaNV.Text.ToString().Trim() + "','"
                    + txtQuyen.Text.ToString().Trim() + "')";


                DataProvider.Instance.ExecuteNonQuery(sql);
                MessageBox.Show("da them thanh cong");
                Load_TaiKhoan();
                reset_data();

            }
            catch
            {
                MessageBox.Show("chua chon Ma Nhan vien :");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNV.Text;
            string sql = $"Delete TaiKhoan where MaNV = N'{maNhanVien}'";

            try
            {
                if (txtMaNV.Text == " ")
                {
                    MessageBox.Show("chua chon MaNV de xoa");

                    txtMaNV.Focus();
                }
                else
                {
                    DataProvider.Instance.ExecuteNonQuery(sql);
                    MessageBox.Show("Da xoa thanh cong");

                    Load_TaiKhoan();
                    reset_data();
                }
            }
            catch (Exception)
            {
                if (maNhanVien == null)
                    MessageBox.Show("Loi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE TaiKhoan SET TenTaiKhoan = '" + txtTenTK.Text.Trim().ToString() + "' ,MatKhau=N'" + txtMatKhau.Text.Trim().ToString() +
               "', Quyen='" + txtQuyen.Text.ToString().Trim() + "' where MaNV='"
               + txtMaNV.Text.ToString().Trim() + "'";

                if (txtTenTK.Text == " ")
                {
                    MessageBox.Show("Ban chua dien  Ten Tai Khoan :");
                    txtTenTK.Focus();
                }
                if (txtQuyen.Text == " ")
                {
                    MessageBox.Show("Ban chua phan Quyen :");
                    txtQuyen.Focus();
                }
                DataProvider.Instance.ExecuteNonQuery(sql);
                MessageBox.Show("Sua thanh cong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);


                reset_data();
                Load_TaiKhoan();

            }
            catch (Exception)
            {

                MessageBox.Show("Loi", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            reset_data();
            txtMaNV.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn thực sự muốn đóng phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
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
                Product.frmProductManage frmProductManage = new Product.frmProductManage(_frmLogin, _staffCode, _staffAuthority);
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
                Storage.frmTransactionManage frmTransactionManage = new Storage.frmTransactionManage(_frmLogin, _staffCode, _staffAuthority);
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
                Storage.frmSuppliers frmSuppliers = new Storage.frmSuppliers(_frmLogin, _staffCode, _staffAuthority);
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
    }
}
