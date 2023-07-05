using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmMain.HRM
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Class.DbConnection.OpenConnection();
        }

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void ckPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUser.Text;
                string pass = txtPass.Text;

                string sql = "select * from TaiKhoanCH where TaiKhoan = '" + username + "' and MatKhau = '" + pass + "'";

                DataTable dt = new DataTable();
                dt = DataProvider.Instance.ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    // gan thuoc tinh trong bang voi cac bien tuong ung trong doi tuong Test
                    frmProductSelect frmProductSelect = new frmProductSelect(this, dt.Rows[0][0].ToString().Trim(), dt.Rows[0][3].ToString().Trim());
                    frmProductSelect.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Thông tin tài khoản không hợp lệ!", "Sai thông tin tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to leave ??", "Notice", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
    }
}
