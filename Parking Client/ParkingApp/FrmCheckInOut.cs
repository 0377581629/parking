using Emgu.CV;
using Emgu.CV.CvEnum;
using ParkingLib;
using RawInput_dll;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Capture = Emgu.CV.Capture;

namespace ParkingApp
{
    public partial class FrmCheckInOut : MetroFramework.Forms.MetroForm
    {
        #region Variable initialization and declaration

        private readonly Helper _helper = new Helper();

        private readonly string _rtspCameraIn;
        private readonly string _rtspCameraOut;
        private readonly double _timeWaiting;
        private readonly string _bariePortName;

        private Capture _captureIn;
        private readonly Mat _frameIn = new Mat();
        private Mat _frameInCopy = new Mat();
        private bool _captureInProgress;
        private bool _takeSnapshotIn;
        private string _pathCaptureIn = string.Empty;
        private string _imageUrl = string.Empty;
        private string _currentImgFileName = string.Empty;
        private Capture _captureOut;
        private readonly Mat _frameOut = new Mat();
        private Mat _frameOutCopy = new Mat();
        private bool _captureOutProgress;
        private bool _takeSnapshotOut;
        private string _pathCaptureOut = string.Empty;

        private string _cardNumberNow = string.Empty;
        private bool _isIn;

        private StudentData _studentSelected = new StudentData();

        private EnumCheckInOut _xuLy;

        private readonly RawInput _rawInput;
        private const bool CAPTURE_ONLY_IN_FOREGROUND = true;
        private readonly string _cardReaderIn;
        private readonly string _cardReaderOut;

        #endregion

        public FrmCheckInOut()
        {
            InitializeComponent();
            Helper.GetConfig(ref _rtspCameraIn, ref _rtspCameraOut, ref _cardReaderIn, ref _cardReaderOut,
                ref _timeWaiting, ref _bariePortName);
            //
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _rawInput = new RawInput(Handle, CAPTURE_ONLY_IN_FOREGROUND);
            _rawInput.AddMessageFilter(); // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit(); // Writes a file DeviceAudit.txt to the current directory
            _rawInput.KeyPressed += OnKeyPressed;
            _rawInput.KeyPressed += FrmCheckInOut_KeyPress;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const string connectBarieTitle = "Đang kết nối barie !";
            WaitWindow.WaitWindow.Show(WaitingConnectBarie, connectBarieTitle);

            const string connectCameraTitle = "Đang kết nối camera !";
            _xuLy = EnumCheckInOut.CONNECT_DEVICE;
            WaitWindow.WaitWindow.Show(WaitingConnectCamera, connectCameraTitle);
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
                MessageBox.Show($"{e.Message}", "Không kết nối được với camera", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ProcessFrameIn(object sender, EventArgs arg)
        {
            _captureIn.Retrieve(_frameIn);
            _frameInCopy = _frameIn;
            var bitmap = (Bitmap)_frameInCopy.Bitmap.Clone();
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
                _imageUrl = $"{GlobalConfig.ParkingServerHost}{GlobalConfig.ImageStorageFolder}{_currentImgFileName}";
                // Set the bool to false again to make sure we only take one snapshot
                _takeSnapshotIn = !_takeSnapshotIn;
            }
        }

        private readonly object _lockObject = new object();

        private void ProcessFrameOut(object sender, EventArgs arg)
        {
            _captureOut.Retrieve(_frameOut);
            _frameOutCopy = _frameOut;
            var bitmap = (Bitmap)_frameOutCopy.Bitmap.Clone();
            lock (_lockObject)
            {
                picCaptureOut.Image?.Dispose();
                picCaptureOut.Image = bitmap;
            }

            if (_takeSnapshotOut)
            {
                var pathCache = Application.StartupPath + "\\Cache";
                if (!Directory.Exists(pathCache))
                {
                    Directory.CreateDirectory(pathCache);
                }

                _currentImgFileName = $"{Guid.NewGuid()}.jpg";
                var path = $"{pathCache}\\{_currentImgFileName}";
                // Save the image
                _frameOutCopy.Save(path);
                _pathCaptureOut = path;
                _imageUrl = $"{GlobalConfig.ParkingServerHost}{GlobalConfig.ImageStorageFolder}{_currentImgFileName}";
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
        private int _vKeyNameIsEnterCount = 0;
        private string _handleCardReader = string.Empty;

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            _handleCardReader = e.KeyPressEvent.Source;
            _cardNumber += e.KeyPressEvent.VKeyName;

            if (e.KeyPressEvent.VKeyName == "ENTER")
            {
                if (_vKeyNameIsEnterCount == 0)
                {
                    _vKeyNameIsEnterCount += 1;
                }
                else
                {
                    var currentCardNumber = FormatCardNumber(_cardNumber);
                    txtCardCode.Text = currentCardNumber;
                    _cardNumber = string.Empty;
                    _vKeyNameIsEnterCount = 0;
                }
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

        #region Proccess Barie

        private void ConnectBarie()
        {
            serialPort1.PortName = _bariePortName;

            try
            {
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Không kết nối được với barie", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Connect Device

        private void WaitingConnectCamera(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var stt = "Quá trình kết nối camera hoàn tất !";
            var thread = new Thread(Doing);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (stt == "")
                stt = "Quá trình kết nối camera hoàn tất !";
            e.Result = e.Arguments.Count > 0 ? e.Arguments[0].ToString() : stt;
        }

        private void WaitingConnectBarie(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var stt = "Quá trình kết nối barie hoàn tất !";

            ConnectBarie();

            if (stt == "")
                stt = "Quá trình kết nối barie hoàn tất !";
            e.Result = e.Arguments.Count > 0 ? e.Arguments[0].ToString() : stt;
        }

        #endregion

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
                    if (string.IsNullOrEmpty(_cardNumberNow)) break;

                    var cardData = new CardData();
                    var lstCards = cardData.Gets();
                    var card = lstCards.FirstOrDefault(o => o.CardNumber == _cardNumberNow);

                    if (card != null)
                    {
                        var historyData = new HistoryData
                        {
                            CardId = card.Id,
                            CardNumber = _cardNumberNow,
                            LicensePlate = card.LicensePlate,
                            Time = DateTime.Now,
                            Type = _isIn ? (int)Helper.HistoryDataStatus.IN : (int)Helper.HistoryDataStatus.OUT,
                            Photo = _imageUrl,
                            CardTypeName = card.CardType,
                            VehicleTypeName = card.VehicleType,
                            StudentData = _studentSelected,
                        };

                        if (!_isIn)
                        {
                            var historyInLasted = _helper.GetHistoryInLasted(_cardNumberNow);
                            if (!string.IsNullOrEmpty(historyInLasted.Photo) && File.Exists(historyInLasted.Photo))
                            {
                                MessageBox.Show($"Thẻ này chưa được sử dụng để vào bãi", "Cảnh báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }

                            #region Get Price

                            var listPriceData = new FareData().GetFaresByCardTypeAndVehicleType(card.CardTypeId,
                                card.VehicleTypeId);
                            var dayFareData = listPriceData.FirstOrDefault(o => o.Type == (int)FareType.DAY);
                            var nightFareData = listPriceData.FirstOrDefault(o => o.Type == (int)FareType.NIGHT);
                            if (dayFareData == null || nightFareData == null)
                            {
                                MessageBox.Show($"Chưa cấu hình đủ giá ngày và đêm", "Cảnh báo", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                                break;
                            }

                            historyData.Price = _helper.CalculatePrice(historyInLasted.Time, historyData.Time,
                                dayFareData.Price, nightFareData.Price);

                            txtFare.Text = historyData.Price.ToString();

                            #endregion

                            #region Update Card Balance

                            if (historyData.StudentData.Id != 0)
                            {
                                if (card.Balance > historyData.Price)
                                {
                                    //Update card balance
                                    cardData.UpdateBalance(card.Id, card.Balance - historyData.Price);
                                }
                                else
                                {
                                    MessageBox.Show(
                                        $"Tài khoản của bạn còn {card.Balance}, vui lòng nạp thêm để sử dụng",
                                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }
                            }

                            #endregion
                        }

                        #region Save history and open barie

                        historyData.Add();
                        var historyId = historyData.Id;
                        _cardNumberNow = string.Empty;
                        if (historyId > 0)
                        {
                            if (btnOpenBarie.InvokeRequired)
                            {
                                btnOpenBarie.Invoke((MethodInvoker)delegate { btnOpenBarie.PerformClick(); });
                            }
                        }

                        #endregion
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

        private async void txtCardCode_TextChanged(object sender, EventArgs e)
        {
            var txt = (RichTextBox)sender;
            if (txt.Text.Length > 0)
            {
                if (_handleCardReader == _cardReaderIn)
                {
                    _takeSnapshotIn = !_takeSnapshotIn;
                }
                else if (_handleCardReader == _cardReaderOut)
                {
                    _takeSnapshotOut = !_takeSnapshotOut;
                }

                // Thread.Sleep(500);
                
                if (txtCardCode.Text.Length < 10) return;
                if (_handleCardReader == _cardReaderIn)
                {
                    await SyncDataClient.Sync.UploadImageToServer(_pathCaptureIn, _currentImgFileName);
                    picIn.Image = Image.FromFile(_pathCaptureIn);
                    txtInTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    _isIn = true;
                }
                else if (_handleCardReader == _cardReaderOut)
                {
                    await SyncDataClient.Sync.UploadImageToServer(_pathCaptureOut, _currentImgFileName);
                    picOut.Image = Image.FromFile(_pathCaptureOut);
                    txtOutTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    _isIn = false;
                }

                #region Get Student Info

                var lstStudents = new StudentData().Gets();
                var lstCards = new CardData().Gets();
                var lstStudentCard = new StudentCardData().Gets();

                var card = lstCards.FirstOrDefault(o => o.CardNumber.Contains(txtCardCode.Text.Trim()));
                var studentCard = lstStudentCard.FirstOrDefault(o => card != null && o.CardId == card.Id);
                var studentInfo = lstStudents.FirstOrDefault(o => studentCard != null && o.Id == studentCard.StudentId);

                #endregion
                
                if (studentInfo != null)
                {
                    #region Update UI

                    var avatarUrl = studentInfo.Avatar;
                    var fullName = studentInfo.Name;
                    var gender = studentInfo.Gender ? "Nam" : "Nữ";
                    var mes = " Sinh viên: " + fullName + " - " + studentInfo.Code +
                              "\n Ngày sinh: " + studentInfo.Dob.ToString(CultureInfo.InvariantCulture) +
                              " - Giới tính: " + gender;

                    picRegistry.Image = _helper.LoadImageFromUrl(avatarUrl, picRegistry.Width, picRegistry.Height);
                    checkInOutContent.Text = mes;
                    richCardLicensePlate.Text = card != null ? card.LicensePlate : "";
                    richRecognitionLicensePlate.Text = "";

                    #endregion

                    #region License plate detection and recognition

                    byte[] imageBytes;
                    if (_handleCardReader == _cardReaderIn)
                    {
                        imageBytes = File.ReadAllBytes(_pathCaptureIn);
                        RecognitionLicensePlate(imageBytes);
                    }
                    else if (_handleCardReader == _cardReaderOut)
                    {
                        imageBytes = File.ReadAllBytes(_pathCaptureOut);
                        RecognitionLicensePlate(imageBytes);
                    }

                    #endregion

                    _studentSelected = (StudentData)studentInfo.Clone();
                }
                else
                {
                    checkInOutContent.Text = "Khách";

                    #region License plate detection and recognition

                    byte[] imageBytes;
                    if (_handleCardReader == _cardReaderIn)
                    {
                        imageBytes = File.ReadAllBytes(_pathCaptureIn);
                        RecognitionLicensePlate(imageBytes);
                    }
                    else if (_handleCardReader == _cardReaderOut)
                    {
                        imageBytes = File.ReadAllBytes(_pathCaptureOut);
                        RecognitionLicensePlate(imageBytes);
                    }

                    #endregion

                    _studentSelected = new StudentData();
                }

                if (!_isIn)
                {
                    #region Get Last In

                    var historyInLasted = _helper.GetHistoryInLasted(txtCardCode.Text.Trim());
                    if (!string.IsNullOrEmpty(historyInLasted.Photo))
                    {
                        picIn.Image = _helper.LoadImageFromUrl(historyInLasted.Photo, picCaptureIn.Width,
                            picCaptureIn.Height);
                        txtInTime.Text = historyInLasted.Time.ToString(CultureInfo.InvariantCulture);
                    }

                    #endregion
                }

                _cardNumberNow = txtCardCode.Text.Trim();
                const string strTitle = "Đang tiến hành tải dữ liệu !";
                _xuLy = EnumCheckInOut.SET_HISTORY;
                WaitWindow.WaitWindow.Show(WaitingConnectCamera, strTitle);
            }
        }

        private async void RecognitionLicensePlate(byte[] imageBytes)
        {
            var responseString =
                await SyncDataClient.Sync.RecognitionLicensePlate(imageBytes, _currentImgFileName);
            var responseObject =
                JsonConvert.DeserializeObject<RecognitionLicensePlateResponse>(responseString);
            if (responseObject.LicensePlates.Any())
            {
                richRecognitionLicensePlate.Text = responseObject.LicensePlates[0];
            }
            else
            {
                richRecognitionLicensePlate.Text = "Không xác định được biển số";
            }
        }

        private void PicIn_DoubleClick(object sender, EventArgs e)
        {
            var obj = (PictureBox)sender;
            if (obj != null)
            {
                var img = new Bitmap(obj.Image);
                var frm = new FrmZoom(img);
                frm.ShowDialog();
            }
        }

        private void PicOut_DoubleClick(object sender, EventArgs e)
        {
            var obj = (PictureBox)sender;
            if (obj != null)
            {
                var img = new Bitmap(obj.Image);
                var frm = new FrmZoom(img);
                frm.ShowDialog();
            }
        }

        private void BtnOpenBarieClick(object sender, EventArgs e)
        {
            serialPort1.Write("2");
        }

        private void BtnCloseBarieClick(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }

        private void FrmCheckInOut_KeyPress(object sender, RawInputEventArg e)
        {
            var _handleKeyPress = e.KeyPressEvent.VKeyName;

            if (string.Equals(_handleKeyPress, "ESCAPE", StringComparison.InvariantCultureIgnoreCase))
            {
                Close();
            }
            else if (string.Equals(_handleKeyPress, "F1", StringComparison.InvariantCultureIgnoreCase))
            {
                btnOpenBarie.PerformClick();
            }
            else if (string.Equals(_handleKeyPress, "F2", StringComparison.InvariantCultureIgnoreCase))
            {
                btnCloseBarie.PerformClick();
            }
        }
    }

    public class RecognitionLicensePlateResponse
    {
        public List<string> LicensePlates { get; set; }
    }

    internal enum EnumCheckInOut
    {
        SET_HISTORY,
        CONNECT_DEVICE,
    }

    public enum FareType
    {
        DAY = 1,
        NIGHT = 2,
    }
}