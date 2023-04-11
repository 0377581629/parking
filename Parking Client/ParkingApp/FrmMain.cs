using System;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmMain : MetroFramework.Forms.MetroForm
    {
        private readonly Timer _aTimer;

        public FrmMain()
        {
            InitializeComponent();

            _aTimer = new Timer();
            _aTimer.Tick += Timer_Tick;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            _aTimer.Enabled = true;
            _aTimer.Start();
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
            }
            lbDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbDayOfWeek.Text = strDayOfWeek;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ShowDateTime();
        }
        
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            var frmHistory = new FrmHistory();
            var result = frmHistory.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmHistory.Close();
            }
        }

        private void BtnSupervise_Click(object sender, EventArgs e)
        {
            var frmCheckInOut = new FrmCheckInOut();
            var result = frmCheckInOut.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmCheckInOut.Close();
            }
        }

        private void BtnConfig_Click(object sender, EventArgs e)
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

        private void BtnTestModelAI_click(object sender, EventArgs e)
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
