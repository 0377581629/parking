using Emgu.CV;
using Emgu.CV.CvEnum;
using ParkingLib;
using RawInput_dll;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using Capture = Emgu.CV.Capture;

namespace ParkingApp
{
    public partial class FrmCheckInOut : MetroFramework.Forms.MetroForm
    {
        private readonly string _rtspCameraIn;
        private readonly string _rtspCameraOut;
        private readonly double _timeWaiting;
        private string _ipBarie;
        private int _portBarie;

        //
        private Capture _captureIn;
        private readonly Mat _frameIn = new Mat();
        private Mat _frameInCopy = new Mat();
        private bool _captureInProgress;
        private bool _takeSnapshotIn;
        private string _pathCaptureIn = string.Empty;
        private string _imageInUrl = string.Empty;
        private string _currentImgFileName = string.Empty;

        private Capture _captureOut;
        private readonly Mat _frameOut = new Mat();
        private Mat _frameOutCopy = new Mat();
        private bool _captureOutProgress;
        private bool _takeSnapshotOut;

        private string _pathCaptureOut = string.Empty;

        // Now
        private string _cardNumberNow = string.Empty;
        private bool _isIn;

        private StudentData _studentSelected = new StudentData();

        //
        private EnumCheckInOut _xuLy;

        private readonly Helper _helper = new Helper();

        //
        private readonly RawInput _rawInput;
        private const bool CAPTURE_ONLY_IN_FOREGROUND = true;
        private readonly string _cardReaderIn;
        private readonly string _cardReaderOut;

        public FrmCheckInOut()
        {
            InitializeComponent();
            Helper.GetConfig(ref _rtspCameraIn, ref _rtspCameraOut, ref _cardReaderIn, ref _cardReaderOut,
                ref _timeWaiting);
            //
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _rawInput = new RawInput(Handle, CAPTURE_ONLY_IN_FOREGROUND);
            _rawInput.AddMessageFilter(); // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit(); // Writes a file DeviceAudit.txt to the current directory
            _rawInput.KeyPressed += OnKeyPressed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const string strTitle = "Đang kết nối thiết bị !";
            _xuLy = EnumCheckInOut.CONNECT_DEVICE;
            WaitWindow.WaitWindow.Show(WaitingSyncData, strTitle);
        }

        #region Proccess Camera

        private void StartStopCamera()
        {
            {
                if (_captureIn != null)
                {
                    if (_captureInProgress)
                    {
                        //stop the capture
                        _captureIn.Pause();
                    }
                    else
                    {
                        //start the capture
                        _captureIn.Start();
                    }

                    _captureInProgress = !_captureInProgress;
                }
            }

            {
                if (_captureOut != null)
                {
                    if (_captureOutProgress)
                    {
                        //stop the capture
                        _captureOut.Pause();
                    }
                    else
                    {
                        //start the capture
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

                    _captureIn.ImageGrabbed += ProcessFrameIn;
                }

                {
                    _captureOut = new Capture(_rtspCameraOut);

                    _captureOut.SetCaptureProperty(CapProp.FrameWidth, 1280);
                    _captureOut.SetCaptureProperty(CapProp.FrameHeight, 720);

                    _captureOut.ImageGrabbed += ProcessFrameOut;
                }
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ProcessFrameIn(object sender, EventArgs arg)
        {
            _captureIn.Retrieve(_frameIn);
            _frameInCopy = _frameIn;
            var bitmap = (Bitmap) _frameInCopy.Bitmap.Clone();
            picCaptureIn.Image = bitmap;

            if (_takeSnapshotIn)
            {
                var pathCache = Application.StartupPath + "\\Cache";
                if (!Directory.Exists(pathCache))
                {
                    Directory.CreateDirectory(pathCache);
                }

                _currentImgFileName = $"{Guid.NewGuid()}.jpg";
                var path = $"{pathCache}\\{_currentImgFileName}";
                // Save the image
                _frameInCopy.Save(path);
                _pathCaptureIn = path;
                _imageInUrl = $"{GlobalConfig.ParkingServerHost}{GlobalConfig.ImageStorageFolder}{_currentImgFileName}";
                // Set the bool to false again to make sure we only take one snapshot
                _takeSnapshotIn = !_takeSnapshotIn;
            }
        }

        private readonly object _lockObject = new object();

        private void ProcessFrameOut(object sender, EventArgs arg)
        {
            _captureOut.Retrieve(_frameOut);
            _frameOutCopy = _frameOut;
            var bitmap = (Bitmap) _frameOutCopy.Bitmap.Clone();
            lock (_lockObject) // (bitmap)
                picCaptureOut.Image = bitmap;

            if (_takeSnapshotOut)
            {
                var pathCache = Application.StartupPath + "\\Cache";
                if (!Directory.Exists(pathCache))
                {
                    Directory.CreateDirectory(pathCache);
                }

                var path = pathCache + "\\" + Guid.NewGuid() + ".jpg";
                // Save the image
                _frameOutCopy.Save(path);

                _pathCaptureOut = path;
                // Set the bool to false again to make sure we only take one snapshot
                _takeSnapshotOut = !_takeSnapshotOut;
            }
        }

        #endregion

        #region Proccess Card Reader

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (null == ex) return;
            MessageBox.Show(ex.Message);
        }

        private string _cardNumber = string.Empty;
        private string _handleCardReader = string.Empty;

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            _handleCardReader = e.KeyPressEvent.Source.ToString();
            _cardNumber += e.KeyPressEvent.VKeyName;

            if (e.KeyPressEvent.VKeyName == "ENTER")
            {
                var currentCardNumber = FormatCardNumber(_cardNumber);
                txtMaThe.Text = currentCardNumber;
                // _cardNumber = string.Empty;
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
                        strOut += strIn[i];
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
                case EnumCheckInOut.CONNECT_DEVICE:
                    CaptureCamera();
                    StartStopCamera();
                    break;
                case EnumCheckInOut.SET_HISTORY:
                {
                    var lstCards = new CardData().Gets();
                    var card = lstCards.FirstOrDefault(o => o.CardNumber == _cardNumberNow);

                    if (card != null)
                    {
                        var historyData = new HistoryData
                        {
                            CardId = card.Id,
                            CardNumber = _cardNumberNow,
                            LicensePlate = card.LicensePlate,
                            Time = DateTime.Now,
                            Type = _isIn ? (int) Helper.HistoryDataStatus.IN : (int) Helper.HistoryDataStatus.OUT,
                            Photo = _imageInUrl,
                            CardTypeName = card.CardType,
                            VehicleTypeName = card.VehicleType,
                            StudentData = _studentSelected,
                        };
                        historyData.Price = historyData.Type == (int) Helper.HistoryDataStatus.IN ? 0 : 1;

                        // Save history
                        historyData.Add();
                        var historyId = historyData.Id;
                        _cardNumberNow = string.Empty;
                        if (historyId > 0 && !string.IsNullOrEmpty(historyData.CardNumber))
                        {
                            // OpenBarie();
                        }
                    }
                }
                    break;
            }
        }

        private void FrmCheckInOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartStopCamera();
            _rawInput.KeyPressed -= OnKeyPressed;
        }

        private async void txtMaThe_TextChanged(object sender, EventArgs e)
        {
            var txt = (RichTextBox) sender;
            if (txt.Text.Length > 0)
            {
                //Do what you have to do
                if (_handleCardReader == _cardReaderIn)
                {
                    _takeSnapshotIn = !_takeSnapshotIn;
                }
                else if (_handleCardReader == _cardReaderOut)
                {
                    _takeSnapshotOut = !_takeSnapshotOut;
                }

                Thread.Sleep(500);
                // Có ảnh & Mã code đúng định dạng
                if (txtMaThe.Text.Length < 10) return;
                if (_handleCardReader == _cardReaderIn)
                {
                    picIn.Image = Image.FromFile(_pathCaptureIn);
                }
                else if (_handleCardReader == _cardReaderOut)
                {
                    picOut.Image = Image.FromFile(_pathCaptureOut);
                }

                if (_handleCardReader == _cardReaderIn)
                {
                    txtThoiGianVao.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    _isIn = true;
                }
                else if (_handleCardReader == _cardReaderOut)
                {
                    txtThoiGianRa.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    _isIn = false;
                }

                //
                var lstStudents = new StudentData().Gets();
                var lstCards = new CardData().Gets();
                var lstStudentCard = new StudentCardData().Gets();

                var card = lstCards.FirstOrDefault(o => o.CardNumber.Contains(txtMaThe.Text.Trim()));
                var studentCard = lstStudentCard.FirstOrDefault(o => card != null && o.CardId == card.Id);
                var studentInfo = lstStudents.FirstOrDefault(o => studentCard != null && o.Id == studentCard.StudentId);

                if (studentInfo != null)
                {
                    // var avatar = Image.FromFile($"{GlobalConfig.ParkingServerHost}{studentInfo.Avatar}");
                    var avatar = Image.FromFile(_pathCaptureIn);
                    var fullName = studentInfo.Name;
                    var gender = studentInfo.Gender ? "Nam" : "Nữ";
                    var mes = " Sinh viên: " + fullName + " - " + studentInfo.Code +
                              "\n Ngày sinh: " + studentInfo.Dob.ToString(CultureInfo.InvariantCulture) +
                              " - Giới tính: " + gender;

                    picRegistry.Image = avatar;
                    richNoiDungCanhBao.Text = mes;
                    richCardLicensePlate.Text = card != null ? card.LicensePlate : "";
                    richRecognitionLicensePlate.Text = "";

                    // License plate detection and recognition
                    // byte[] imageBytes = { };
                    // if (_handleCardReader == _cardReaderIn)
                    // {
                    //     imageBytes = File.ReadAllBytes(_pathCaptureIn);
                    // }
                    // else if (_handleCardReader == _cardReaderOut)
                    // {
                    //     imageBytes = File.ReadAllBytes(_pathCaptureOut);
                    // }
                    // var responseString = await SyncDataClient.Sync.RecognitionLicensePlate(imageBytes, _currentImgFileName);
                    // richRecognitionLicensePlate.Text = responseString;
                    
                    // Add image to web app
                    await SyncDataClient.Sync.UploadImageToServer(_pathCaptureIn, _currentImgFileName);

                    _studentSelected = (StudentData) studentInfo.Clone();
                }
                else
                {
                    richNoiDungCanhBao.Text = "Khách";

                    _studentSelected = new StudentData();
                }

                // --------------------
                if (!_isIn)
                {
                    #region Get Last In

                    var historyInLasted = _helper.GetHistoryInLasted(txtMaThe.Text.Trim());
                    if (!string.IsNullOrEmpty(historyInLasted.Photo) && File.Exists(historyInLasted.Photo))
                    {
                        picIn.Image = (Bitmap) Image.FromFile(historyInLasted.Photo);
                        txtThoiGianVao.Text = historyInLasted.Time.ToString(CultureInfo.InvariantCulture);
                    }

                    #endregion
                }

                // --------------------
                _cardNumberNow = txtMaThe.Text.Trim();
                const string strTitle = "Đang tiến hành tải dữ liệu !";
                _xuLy = EnumCheckInOut.SET_HISTORY;
                WaitWindow.WaitWindow.Show(WaitingSyncData, strTitle);
            }
        }

        private void picIn_DoubleClick(object sender, EventArgs e)
        {
            var obj = (PictureBox) sender;
            if (obj != null)
            {
                var img = new Bitmap(obj.Image);
                var frm = new FrmZoom(img);
                frm.ShowDialog();
            }
        }

        private void picOut_DoubleClick(object sender, EventArgs e)
        {
            var obj = (PictureBox) sender;
            if (obj != null)
            {
                var img = new Bitmap(obj.Image);
                var frm = new FrmZoom(img);
                frm.ShowDialog();
            }
        }

        private void FrmCheckInOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.F1)
            {
                txtMaThe.Focus();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                //MessageBox.Show("Mở !");
                btnOpen.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.F6)
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

    internal enum EnumCheckInOut
    {
        SET_HISTORY,
        CONNECT_DEVICE,
    }
}