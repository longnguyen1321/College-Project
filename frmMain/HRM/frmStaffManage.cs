using frmMain.Product;
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
    public partial class frmStaffManage : Form
    {
        frmLogin _frmLogin;
        String _staffCode;
        String _staffAuthority;

        public frmStaffManage()
        {
            InitializeComponent();
        }

        public frmStaffManage(frmLogin frmLogin, String staffCode, String staffAuthority)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
            _staffCode = staffCode;
            _staffAuthority = staffAuthority;
        }

        private void frmStaffManage_Load(object sender, EventArgs e)
        {
            Class.DbConnection.OpenConnection();

            txtMaNV.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            dtgNhanVien_load();
            Delete_Data();
        }

        //ham hien thi danh sach nhan vien
        private void dtgNhanVien_load()
        {
            string sql = "select * from NhanVien";
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery(sql);
            dataGridView1.DataSource = dt;

        }

        private void Delete_Data()
        {
            txtDiaChi.Text = txtMaNV.Text = txtTenNV.Text = "";
            mstSDT.Text = "";
            txtTimKiem.Text = " ";
            dtNgaySinh.Value = DateTime.Now;
        }
        void LamMoi()
        {
            dtgNhanVien_load();
            Delete_Data();
            btnSkip.Enabled = true;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = false;
            txtMaNV.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            txtMaNV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();

            string gt = dataGridView1.Rows[i].Cells[1].Value.ToString();
            if (gt.ToString() == "nam")
            {
                ckbGioi.Checked = true;
            }

            txtTenNV.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            mstSDT.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtDiaChi.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            dataGridView1.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
            dtNgaySinh.Value = (DateTime)dataGridView1.Rows[i].Cells[5].Value;



            btnDelete.Enabled = true;
            btnSkip.Enabled = true;
            btnUpdate.Enabled = true;
            txtMaNV.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnAdd.Enabled = false;

            Delete_Data();
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
            btnSkip.Enabled = true;
            btnSkip.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNV.Text;
            string sql = $"Delete NhanVien where MaNV = N'{maNhanVien}'";
            try
            {
                DataProvider.Instance.ExecuteNonQuery(sql);
                MessageBox.Show("Da xoa thanh cong");
            }
            catch (Exception)
            {
                MessageBox.Show("Chua chon nhan vien can xoa");
            }

            LamMoi();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mstSDT.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mstSDT.Focus();
                return;
            }

            if (ckbGioi.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            sql = "UPDATE NhanVien SET  TenNV=N'" + txtTenNV.Text.Trim().ToString() +
                    "',DiaChiNV=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',LienLacNV='" + mstSDT.Text.ToString() + "',GioiTinh=N'" + gt +
                    "',NgaySinhNV='" + dtNgaySinh.Value +
                    "' WHERE MaNV=N'" + txtMaNV.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(sql);
            LamMoi();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string gt;
            string sql;
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã Nhân Viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên Nhân Viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (mstSDT.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập Số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mstSDT.Focus();
                return;

            }


            if (ckbGioi.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";


            sql = "INSERT INTO NhanVien(MaNV,TenNV,GioiTinh, DiaChiNV,LienLacNV, NgaySinhNV) VALUES (N'" + txtMaNV.Text.Trim() + "',N'" + txtTenNV.Text.Trim() + "',N'" + gt + "',N'" + txtDiaChi.Text.Trim() + "','" + mstSDT.Text + "','" + dtNgaySinh.Value + "')";

            DataProvider.Instance.ExecuteNonQuery(sql);

            LamMoi();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult closeAllForm = MessageBox.Show("Bạn thực sự muốn đóng phần mềm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (closeAllForm == DialogResult.Yes)
            {
                _frmLogin.Close(); //Đóng form gốc đang chạy là Form frmLogin
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maNV = txtTimKiem.Text.Trim();
            if (txtTimKiem.Text == " ")
            {
                MessageBox.Show("Bạn phải nhập Ma Nhan Vien", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTimKiem.Focus();
                return;

            }
            DataTable dt = DataProvider.Instance.ExecuteQuery($"Select * from NhanVien where MaNV = N'{maNV}'");

            foreach (DataRow item in dt.Rows)
            {
                txtMaNV.Text = item["MaNV"].ToString();
                txtTenNV.Text = item["TenNV"].ToString();
                dtNgaySinh.Value = (DateTime)item["NgaySinhNV"];
                txtDiaChi.Text = item["DiaChiNV"].ToString();
                mstSDT.Text = item["LienLacNV"].ToString();


                if (item["GioiTinh"].ToString().Equals("Nam"))
                {
                    ckbGioi.Checked = true;
                }

                btnSkip.Enabled = true;
            }
            dataGridView1.DataSource = dt;
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
                frmTransactionManage frmTransactionManage = new Storage.frmTransactionManage(_frmLogin, _staffCode, _staffAuthority);
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
                frmSuppliers frmSuppliers = new Storage.frmSuppliers(_frmLogin, _staffCode, _staffAuthority);
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
