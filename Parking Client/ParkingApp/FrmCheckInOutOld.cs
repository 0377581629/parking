using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Capture = Emgu.CV.Capture;

namespace ParkingApp
{
    public partial class FrmCheckInOutOld : MetroFramework.Forms.MetroForm
    {
        private string _rtspCamera = "rtsp://admin:1qaz1qaz@192.168.0.107:554/live";
        //
        private Capture _capture = null;
        Mat frame = new Mat();
        Mat frame_copy = new Mat();
        private bool _captureInProgress;
        private bool takeSnapshot = false;
        //
        private enumCheckInOutOld _xuLy;

        public FrmCheckInOutOld()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var strTitle = "Đang kết nối thiết bị !";
            _xuLy = enumCheckInOutOld.ConnectDevice;
            var result = WaitWindow.WaitWindow.Show(WartingSyncData, strTitle);
        }

        #region Proccess Camera
        private void StartStopCamera()
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {  //stop the capture
                    Console.WriteLine("Start Capture");
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    Console.WriteLine("Stop");
                    _capture.Start();
                }

                _captureInProgress = !_captureInProgress;
            }
        }

        private void CaptureCamera()
        {
            CvInvoke.UseOpenCL = false;
            try
            {
                _capture = new Capture(_rtspCamera);

                _capture.SetCaptureProperty(CapProp.FrameWidth, 1280);
                _capture.SetCaptureProperty(CapProp.FrameHeight, 720);
                //_capture.SetCaptureProperty(CapProp.Fps, 3);

                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            _capture.Retrieve(frame);
            frame_copy = frame;
            picInOne.Image = frame_copy.Bitmap;

            if (takeSnapshot)
            {
                string pathCache = Application.StartupPath + "\\Cache";
                if (!Directory.Exists(pathCache))
                {
                    Directory.CreateDirectory(pathCache);
                }
                string path = pathCache + "\\" + new Guid().ToString() + ".jpg";
                // Save the image
                frame_copy.Save(path);

                // Set the bool to false again to make sure we only take one snapshot
                takeSnapshot = !takeSnapshot;
            }
        }
        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    txtBienSo.Focus();
                    return true;
                case (Keys.Control | Keys.F1):
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void WartingSyncData(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var stt = "Quá trình xử lý dữ liệu hoàn tất !";
            var thread = new Thread(Doing);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (stt == "")
                stt = "Quá trình xử lý dữ liệu hoàn tất !";
            e.Result = e.Arguments.Count > 0 ? e.Arguments[0].ToString() : stt;
        }

        private void Doing()
        {
            switch (_xuLy)
            {
                case enumCheckInOutOld.ConnectDevice:
                    CaptureCamera();
                    StartStopCamera();
                    break;
                default:
                    break;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            takeSnapshot = !takeSnapshot;
        }

        private void FrmCheckInOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartStopCamera();
        }

        private void richNoiDungCanhBao_TextChanged(object sender, EventArgs e)
        {

        }
    }

    enum enumCheckInOutOld
    {
        LoadData,
        CaptureAvatar,
        ConnectDevice,
        Default
    }
}
