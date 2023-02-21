using MetroFramework;
using System;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmConfig : MetroFramework.Forms.MetroForm
    {
        private string _rtspCameraIn = "rtsp://admin:123abc@@@192.168.1.250/live";
        private string _rtspCameraOut = "rtsp://admin:123abc@@@192.168.1.252:554/live";
        private string _cardReaderIn = "10620057";
        private string _cardReaderOut = "95748765";
        private double _timeWaiting = 0;

        public FrmConfig()
        {
            InitializeComponent();
            Helper.GetConfig(ref _rtspCameraIn, ref _rtspCameraOut, ref _cardReaderIn, ref _cardReaderOut, ref _timeWaiting);
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            txtRtspIn.Text = _rtspCameraIn;
            txtRtspOut.Text = _rtspCameraOut;
            txtCardReaderIn.Text = _cardReaderIn;
            txtCardReaderOut.Text = _cardReaderOut;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Helper.SetConfig(txtRtspIn.Text.Trim(), txtRtspOut.Text.Trim(), txtCardReaderIn.Text.Trim(), txtCardReaderOut.Text.Trim(), _timeWaiting.ToString());

            MetroMessageBox.Show(this, "Cấu hình thành công !", "Thông báo");

            this.DialogResult = DialogResult.OK;
        }

        private void txtTimerUpload_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
