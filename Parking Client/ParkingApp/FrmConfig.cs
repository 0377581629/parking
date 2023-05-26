using MetroFramework;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using ParkingLib;

namespace ParkingApp
{
    public partial class FrmConfig : MetroFramework.Forms.MetroForm
    {
        private readonly string _rtspCameraIn;
        private readonly string _rtspCameraOut;
        private readonly string _cardReaderIn;
        private readonly string _cardReaderOut;
        private readonly double _timeWaiting;

        public FrmConfig()
        {
            InitializeComponent();
            Helper.GetConfig(ref _rtspCameraIn, ref _rtspCameraOut, ref _cardReaderIn, ref _cardReaderOut,
                ref _timeWaiting);
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            txtRtspIn.Text = _rtspCameraIn;
            txtRtspOut.Text = _rtspCameraOut;
            txtCardReaderIn.Text = _cardReaderIn;
            txtCardReaderOut.Text = _cardReaderOut;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            Helper.SetConfig(txtRtspIn.Text.Trim(), txtRtspOut.Text.Trim(), txtCardReaderIn.Text.Trim(),
                txtCardReaderOut.Text.Trim(), _timeWaiting.ToString(CultureInfo.InvariantCulture));

            MetroMessageBox.Show(this, "Cấu hình thành công !", "Thông báo");

            DialogResult = DialogResult.OK;
        }

        private void TxtTimerUpload_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}