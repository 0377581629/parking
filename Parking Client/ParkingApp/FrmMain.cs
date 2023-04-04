using MetroFramework;
using ParkingLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SyncDataModels;

namespace ParkingApp
{
    public partial class FrmMain : MetroFramework.Forms.MetroForm
    {
        private System.Windows.Forms.Timer aTimer;

        public FrmMain()
        {
            InitializeComponent();

            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(timer_Tick);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            aTimer.Enabled = true;
            aTimer.Start();
            ShowDateTime();
        }

        private void ShowDateTime()
        {
            var strDayOfWeek = string.Empty;
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    strDayOfWeek = "Chủ nhật";
                    break;
                case DayOfWeek.Monday:
                    strDayOfWeek = "Thứ hai";
                    break;
                case DayOfWeek.Tuesday:
                    strDayOfWeek = "Thứ ba";
                    break;
                case DayOfWeek.Wednesday:
                    strDayOfWeek = "Thứ tư";
                    break;
                case DayOfWeek.Thursday:
                    strDayOfWeek = "Thứ năm";
                    break;
                case DayOfWeek.Friday:
                    strDayOfWeek = "Thứ sáu";
                    break;
                case DayOfWeek.Saturday:
                    strDayOfWeek = "Thứ bảy";
                    break;
                default:
                    break;
            }
            lbDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbDayOfWeek.Text = strDayOfWeek;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowDateTime();
        }
        
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            var frmHistory = new FrmHistory();
            var result = frmHistory.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmHistory.Close();
            }
        }

        private void btnGiamSat_Click(object sender, EventArgs e)
        {
            var frmCheckInOut = new FrmCheckInOut();
            var result = frmCheckInOut.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmCheckInOut.Close();
            }
        }

        private void btnCauHinh_Click(object sender, EventArgs e)
        {
            var config = new FrmConfig();
            var result = config.ShowDialog();

            if (result == DialogResult.OK)
            {
                config.Close();
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnTestModelAI_click(object sender, EventArgs e)
        {
            var frmTestModelAI = new FrmTestModelAI();
            var result = frmTestModelAI.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmTestModelAI.Close();
            }
        }
    }
}
