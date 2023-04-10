using Emgu.CV;
using Emgu.CV.CvEnum;
using ParkingLib;
using RawInput_dll;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Capture = Emgu.CV.Capture;

namespace ParkingApp
{
    public partial class FrmCheckInOut : MetroFramework.Forms.MetroForm
    {
        private string _rtspCameraIn = "rtsp://admin:123abc@@@192.168.1.250/live";
        private string _rtspCameraOut = "rtsp://admin:123abc@@@192.168.1.252:554/live";
        private double _timeWaiting = 0;
        private string _ipBarie = "192.168.1.201";
        private int _portBarie = 4370;
        //
        private Capture _captureIn = null;
        Mat frameIn = new Mat();
        Mat frameIn_copy = new Mat();
        private bool _captureInProgress;
        private bool takeSnapshotIn = false;
        private string pathCaptureIn = string.Empty;

        private Capture _captureOut = null;
        Mat frameOut = new Mat();
        Mat frameOut_copy = new Mat();
        private bool _captureOutProgress;
        private bool takeSnapshotOut = false;
        private string pathCaptureOut = string.Empty;
        // Now
        private string cardNumberNow = string.Empty;
        private bool isIn = false;
        StudentData studentSelected = new StudentData();
        //
        private enumCheckInOut _xuLy;
        readonly Helper _helper = new Helper();
        //
        private readonly RawInput _rawinput;
        const bool CaptureOnlyInForeground = true;
        private string _cardReaderIn = "10620057";
        private string _cardReaderOut = "95748765";

        public FrmCheckInOut()
        {
            InitializeComponent();
            Helper.GetConfig(ref _rtspCameraIn, ref _rtspCameraOut, ref _cardReaderIn, ref _cardReaderOut, ref _timeWaiting);
            //
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _rawinput = new RawInput(Handle, CaptureOnlyInForeground);
            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit();            // Writes a file DeviceAudit.txt to the current directory
            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //KeyPreview = true;
            var strTitle = "Đang kết nối thiết bị !";
            _xuLy = enumCheckInOut.ConnectDevice;
            WaitWindow.WaitWindow.Show(WaitingSyncData, strTitle);
        }

        #region Proccess Camera
        private void StartStopCamera()
        {

            {
                if (_captureIn != null)
                {
                    if (_captureInProgress)
                    {  //stop the capture
                        Console.WriteLine("Start Capture Camera In");
                        _captureIn.Pause();
                    }
                    else
                    {
                        //start the capture
                        Console.WriteLine("Stop Camera In");
                        _captureIn.Start();
                    }

                    _captureInProgress = !_captureInProgress;
                }
            }

            {
                if (_captureOut != null)
                {
                    if (_captureOutProgress)
                    {  //stop the capture
                        Console.WriteLine("Start Capture Camera Out");
                        _captureOut.Pause();
                    }
                    else
                    {
                        //start the capture
                        Console.WriteLine("Stop Camera Out");
                        _captureOut.Start();
                    }

                    _captureOutProgress = !_captureOutProgress;
                }
            }
        }

        private void CaptureCamera()
        {
            CvInvoke.UseOpenCL = false;
            try
            {
                {
                    _captureIn = new Capture(_rtspCameraIn);

                    _captureIn.SetCaptureProperty(CapProp.FrameWidth, 1280);
                    _captureIn.SetCaptureProperty(CapProp.FrameHeight, 720);
                    //_captureIn.SetCaptureProperty(CapProp.Fps, 3);

                    _captureIn.ImageGrabbed += ProcessFrameIn;
                }

                {
                    _captureOut = new Capture(_rtspCameraOut);

                    _captureOut.SetCaptureProperty(CapProp.FrameWidth, 1280);
                    _captureOut.SetCaptureProperty(CapProp.FrameHeight, 720);
                    //_captureOut.SetCaptureProperty(CapProp.Fps, 3);

                    _captureOut.ImageGrabbed += ProcessFrameOut;
                }
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void ProcessFrameIn(object sender, EventArgs arg)
        {
            _captureIn.Retrieve(frameIn);
            frameIn_copy = frameIn;
            var bitmap = (Bitmap)frameIn_copy.Bitmap.Clone();
            picCaptureIn.Image = bitmap;

            if (takeSnapshotIn)
            {
                string pathCache = Application.StartupPath + "\\Cache";
                if (!Directory.Exists(pathCache))
                {
                    Directory.CreateDirectory(pathCache);
                }
                string path = pathCache + "\\" + Guid.NewGuid().ToString() + ".jpg";
                // Save the image
                frameIn_copy.Save(path);

                pathCaptureIn = path;
                // Set the bool to false again to make sure we only take one snapshot
                takeSnapshotIn = !takeSnapshotIn;
            }
        }

        private object lockObject = new object();
        private void ProcessFrameOut(object sender, EventArgs arg)
        {
            _captureOut.Retrieve(frameOut);
            frameOut_copy = frameOut;
            var bitmap = (Bitmap)frameOut_copy.Bitmap.Clone();
            lock (lockObject) // (bitmap)
                picCaptureOut.Image = bitmap;

            if (takeSnapshotOut)
            {
                string pathCache = Application.StartupPath + "\\Cache";
                if (!Directory.Exists(pathCache))
                {
                    Directory.CreateDirectory(pathCache);
                }
                string path = pathCache + "\\" + Guid.NewGuid().ToString() + ".jpg";
                // Save the image
                frameOut_copy.Save(path);

                pathCaptureOut = path;
                // Set the bool to false again to make sure we only take one snapshot
                takeSnapshotOut = !takeSnapshotOut;
            }
        }

        #endregion

        #region Proccess Card Reader
        private static void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (null == ex) return;
            MessageBox.Show(ex.Message);
        }

        private string cardNumber = string.Empty;
        private string handleCardReader = string.Empty;
        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            handleCardReader = e.KeyPressEvent.Source.ToString();
            cardNumber += e.KeyPressEvent.VKeyName;

            if (e.KeyPressEvent.VKeyName == "ENTER")
            {
                txtMaThe.Text = FormatCardNumber(cardNumber);
                cardNumber = string.Empty;
            }
        }
        private string FormatCardNumber(string strIn)
        {
            var strOut = string.Empty;
            if (!string.IsNullOrEmpty(strIn))
            {
                strIn = strIn.Replace("ENTER", "");
                strIn = strIn.Replace("D", "");
                for (int i = 0; i < strIn.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        strOut = strOut + strIn[i];
                    }
                }
            }
            return strOut;
        }
        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F))
            {
                // Alt+F pressed
                MessageBox.Show("Đóng !");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void WaitingSyncData(object sender, WaitWindow.WaitWindowEventArgs e)
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
                case enumCheckInOut.ConnectDevice:
                    CaptureCamera();
                    StartStopCamera();
                    break;
                case enumCheckInOut.SetLogHistory:
                    {
                        var mes = string.Empty;
                        var log = new ParkingLib.HistoryData
                        {
                            CardNumber = cardNumberNow,
                            Type = isIn ? (int)Helper.HistoryDataStatus.In : (int)Helper.HistoryDataStatus.Out,
                            StudentData = studentSelected,
                            Photo = pathCaptureIn,
                            Time = DateTime.Now
                        };
                        // Save log
                        var logId = _helper.AddLogHistory(log, ref mes);
                        cardNumberNow = string.Empty;
                        if (logId > 0 && !string.IsNullOrEmpty(log.CardNumber))
                        {
                            // OpenBarie();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void FrmCheckInOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartStopCamera();
            //
            _rawinput.KeyPressed -= OnKeyPressed;
        }

        private void txtMaThe_TextChanged(object sender, EventArgs e)
        {
            var txt = (RichTextBox)sender;
            if (txt.Text.Length > 0)
            {
                //Do what you have to do
                if (handleCardReader == _cardReaderIn)
                {
                    takeSnapshotIn = !takeSnapshotIn;
                }
                else if (handleCardReader == _cardReaderOut)
                {
                    takeSnapshotOut = !takeSnapshotOut;
                }
                Thread.Sleep(500);
                // Có ảnh & Mã code đúng định dạng
                if (txtMaThe.Text.Length >= 10)
                {
                    if (handleCardReader == _cardReaderIn)
                    {
                        picIn.Image = Image.FromFile(pathCaptureIn);
                    }
                    else if (handleCardReader == _cardReaderOut)
                    {
                        picOut.Image = Image.FromFile(pathCaptureOut);
                    }

                    if (handleCardReader == _cardReaderIn)
                    {
                        txtThoiGianVao.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        isIn = true;
                    }
                    else if (handleCardReader == _cardReaderOut)
                    {
                        txtThoiGianRa.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        isIn = false;
                    }

                    //
                    var lstStudents = new StudentData().Gets();
                    var lstCards = new CardData().Gets();
                    var lstStudentCard = new StudentCardData().Gets();

                    var card = lstCards.FirstOrDefault(o => o.CardNumber.Contains(txtMaThe.Text.Trim()));
                    var studentCard = lstStudentCard.FirstOrDefault(o => card != null && o.CardId == card.Id);
                    var studentInfo = lstStudents.FirstOrDefault(o => studentCard != null && o.Id == studentCard.StudentId);

                    var mes = string.Empty;

                    if (studentInfo != null)
                    {
                        var avatar = Image.FromFile("../../Resources/ic_person.png");
                        var fullName = studentInfo.Name;
                        var gender = studentInfo.Gender ? "Nam" : "Nữ";
                        mes = " Sinh viên: " + fullName + " - " + studentInfo.Code +
                              "\n Ngày sinh: " + studentInfo.Dob.ToString(CultureInfo.InvariantCulture) + " - Giới tính: " + gender;

                        picRegistry.Image = avatar;
                        richNoiDungCanhBao.Text = mes;
                        richCardLicensePlate.Text = card != null ? card.LicensePlate : "";
                        richRecognitionLicensePlate.Text = "";
                        studentSelected = (StudentData)studentInfo.Clone();
                    }
                    else
                    {
                        richNoiDungCanhBao.Text = "Khách";

                        studentSelected = new StudentData();
                    }
                    // --------------------
                    if(!isIn)
                    {
                        #region Get thông tin vào gần nhất ra
                        var logHistoryInLasted = new HistoryData();
                        logHistoryInLasted = _helper.GetLogInLasted(txtMaThe.Text.Trim());
                        if (!string.IsNullOrEmpty(logHistoryInLasted.Photo) && File.Exists(logHistoryInLasted.Photo))
                        {
                            picIn.Image = (Bitmap)Image.FromFile(logHistoryInLasted.Photo);
                            txtThoiGianVao.Text = logHistoryInLasted.Time.ToString(CultureInfo.InvariantCulture);
                        }
                        #endregion
                    }
                    // --------------------
                    cardNumberNow = txtMaThe.Text.Trim();
                    var strTitle = "Đang tiến hành tải dữ liệu !";
                    _xuLy = enumCheckInOut.SetLogHistory;
                    var result = WaitWindow.WaitWindow.Show(WaitingSyncData, strTitle);
                }
            }
        }

        private void picIn_DoubleClick(object sender, EventArgs e)
        {
            var obj = (PictureBox)sender;
            if (obj != null)
            {
                var img = new Bitmap(obj.Image);
                var frm = new FrmZoom(img);
                frm.ShowDialog();
            }
        }

        private void picOut_DoubleClick(object sender, EventArgs e)
        {
            var obj = (PictureBox)sender;
            if (obj != null)
            {
                var img = new Bitmap(obj.Image);
                var frm = new FrmZoom(img);
                frm.ShowDialog();
            }
        }

        private void FrmCheckInOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.F1)
            {
                txtMaThe.Focus();
            }
            else if((e.Control == true && e.KeyCode == Keys.F2))
            {
                //MessageBox.Show("Mở !");
                btnOpen.PerformClick();
            }
            else if ((e.Control == true && e.KeyCode == Keys.F6))
            {
                //MessageBox.Show("Đóng !");
                btnClose.PerformClick();
            }
        }

        private void FrmCheckInOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Đóng !");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }

    enum enumCheckInOut
    {
        SetLogHistory,
        ConnectDevice,
    }
}
