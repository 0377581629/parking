
using MetroFramework;
using SyncDataClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        public FrmLogin()
        {
            InitializeComponent();
            var db = ConfigurationManager.AppSettings["connectionString"];
            if (db == "Data Source=Data/ParkingDB.db")
            {
                var db1 = "Data Source=" + Application.StartupPath + "/" + db.Replace("Data Source=", "");
                var cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cf.AppSettings.Settings.Remove("connectionString");
                cf.AppSettings.Settings.Add("connectionString", db1);
                cf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            // ---

        }

        private void linkExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtUser.Text.Trim()) || string.IsNullOrEmpty(txtPass.Text.Trim()))
            //{
            //    MetroMessageBox.Show(this, "Thông tin đăng nhập không được bỏ trống !", "Cảnh báo");
            //    return;
            //}

            //System.Media.SystemSounds.Hand.Play();
            //var domainName = string.Empty; // "https://colab.368up.com";
            //if (ConfigurationManager.AppSettings.AllKeys.Contains("DomainName"))
            //{
            //    domainName = ConfigurationManager.AppSettings["DomainName"];
            //}
            //SyncDataClient.Init.SetKey("dataSyncData", "319D1E17-856F-4999-A8A2-11A10BF61B07");
            //SyncDataClient.Init.SetInfo(domainName, "", txtUser.Text.Trim(), txtPass.Text.Trim());
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //var educateData = AsyncHelper.RunSync(() => SyncDataClient.Sync.GetSyncClientData());
            //if (educateData != null)
            //{
                var main = new FrmMain();
                this.Hide();
                main.ShowDialog();
            //}
            //else
            //{
            //    MetroMessageBox.Show(this, "Đăng nhập thật bại !", "Cảnh báo");
            //    return;
            //}

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
