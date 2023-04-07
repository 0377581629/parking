using MetroFramework;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        private readonly string _connectionString = ConfigurationManager.AppSettings["connectionStringSqlServer"];

        public FrmLogin()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                txtUser.Text = Properties.Settings.Default.UserName;
                txtPass.Text = Properties.Settings.Default.Password;
            }
        }

        private DataTable LookupUser(string UserName, int TenantId)
        {
            const string
                query =
                    "SELECT Password FROM dbo.AbpUsers WHERE UserName = @UserName AND TenantId = @TenantId"; // shouldn't use, should throw it into a stored procedure to authenticate

            var result = new DataTable();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@UserName", DbType.String).Value = UserName;
                    cmd.Parameters.Add("@TenantId", DbType.Int32).Value = TenantId;
                    using (var dr = cmd.ExecuteReader())
                    {
                        result.Load(dr);
                    }
                }
            }

            return result;
        }

        private void linkExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text.Trim()) || string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                MetroMessageBox.Show(this, "Thông tin đăng nhập không được bỏ trống !", "Cảnh báo");
                return;
            }

            System.Media.SystemSounds.Hand.Play();

            using (var dt = LookupUser(txtUser.Text, 1))
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

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}