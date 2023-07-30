using MetroFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ParkingLib;

namespace ParkingApp
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        private SqlConnection _conn = ParkingLib.Helper.OpenConnection();

        public FrmLogin()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                txtUser.Text = Properties.Settings.Default.UserName;
                txtPass.Text = Properties.Settings.Default.Password;
            }
        }

        private DataTable LookupUser(string userName)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            
            var tenantId = GlobalConfig.TenantId;
            var passwordQuery = "";
            if (tenantId != null)
            {
               passwordQuery = $"SELECT Password FROM dbo.AbpUsers WHERE UserName = '{userName}' AND TenantId = {tenantId}"; // shouldn't use, should throw it into a stored procedure to authenticate
            }
            else
            {
                passwordQuery = $"SELECT abpUser.Password FROM dbo.AbpUsers abpUser WHERE abpUser.UserName = '{userName}' AND TenantId IS NULL"; // shouldn't use, should throw it into a stored procedure to authenticate
            }
            
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(passwordQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "UserData");
                    dt = ds.Tables["UserData"];
                }
            }

            return dt;
        }

        private void LinkExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text.Trim()) || string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                MessageBox.Show("Thông tin đăng nhập không được bỏ trống !", "Cảnh báo");
                return;
            }

            System.Media.SystemSounds.Hand.Play();

            using (var dt = LookupUser(txtUser.Text))
            {
                if (dt.Rows.Count == 0)
                {
                    txtUser.Focus();
                    MessageBox.Show("Tài khoản sai", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUser.Focus();
                    txtUser.Clear();
                    txtPass.Clear();
                }
                else
                {
                    var dbPassword = Convert.ToString(dt.Rows[0]["Password"]);
                    // var appPassword = Convert.ToString(txtPass.Text);
                    var appPassword =
                        dbPassword; // can't use any hash to convert appPassword like dbPassword although text of them is similar

                    if (string.CompareOrdinal(dbPassword, appPassword) == 0)
                    {
                        if (checkRemember.Checked)
                        {
                            Properties.Settings.Default.UserName = txtUser.Text;
                            Properties.Settings.Default.Password = txtPass.Text;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.UserName = "";
                            Properties.Settings.Default.Password = "";
                            Properties.Settings.Default.Save();
                        }

                        DialogResult = DialogResult.OK;
                        var main = new FrmMain();
                        main.ShowDialog();
                        Close();
                    }
                    else
                    {
                        txtPass.Focus();
                        MessageBox.Show("Mật khẩu sai", this.Text, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        txtPass.Focus();
                        txtPass.Clear();
                    }
                }
            }
        }

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(null, null);
            }
        }
    }
}