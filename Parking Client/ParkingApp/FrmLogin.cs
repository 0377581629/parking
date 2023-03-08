using DataAccess;
using MetroFramework;
using SyncDataClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        private SQLiteConnection _conn = DBExecute.OpenConnection();

        public FrmLogin()
        {
            InitializeComponent();

            var connectionString = ConfigurationManager.AppSettings["connectionString"];
            if (connectionString.Equals("Data Source=DataBase/parking.db"))
            {
                var newConnectionSting = "Data Source=" + Application.StartupPath + "/" +
                                         connectionString.Replace("Data Source=", "");
                var cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cf.AppSettings.Settings.Remove("connectionString");
                cf.AppSettings.Settings.Add("connectionString", newConnectionSting);
                cf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                txtUser.Text = Properties.Settings.Default.UserName;
                txtPass.Text = Properties.Settings.Default.Password;
            }
        }
        
        private static DataTable LookupUser(string UserName)
        {
            var connStr = ConfigurationManager.AppSettings["connectionString"];

            const string query = "SELECT Password FROM Parking_User WHERE UserName = @UserName"; // shouldn't use, should throw it into a stored procedure to authenticate
            
            var result = new DataTable();
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.Add("@UserName", DbType.String).Value = UserName;
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
                    var appPassword = Convert.ToString(txtPass.Text); //store the password as encrypted in the DB

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