using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon_LapTrinhDotNet.HRM
{
    public partial class frmLogin : Form
    {
        private void frmLogin_Load(object sender, EventArgs e)
        {
            Class.DbConnection.OpenConnection(); 
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                string username = txtUser.Text;
                string pass = txtPass.Text;

                string sql = "select * from TaiKhoan where TenTaiKhoan = '" + username + "' and MatKhau = '" + pass + "'";

                DataTable dt = new DataTable();
                dt = DataProvider.Instance.ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("suscces");
                    // gan thuoc tinh trong bang voi cac bien tuong ung trong doi tuong Test
                    Test te = new(dt.Rows[0][0].ToString(),
                        dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString());
                    te.ShowDialog();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("role");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi");
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
    }
}
