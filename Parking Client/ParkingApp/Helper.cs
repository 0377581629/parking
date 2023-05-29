using ParkingLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ParkingApp
{
    class Helper
    {
        /// <summary>
        /// Lấy thông tin cấu hình trong file config
        /// </summary>
        public static void GetConfig(ref string rtspCameraIn, ref string rtspCameraOut, ref string cardReaderIn,
            ref string cardReaderOut, ref double timeWaiting)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("RtspCameraIn"))
            {
                // Key exists
                rtspCameraIn = ConfigurationManager.AppSettings["RtspCameraIn"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RtspCameraOut"))
            {
                // Key exists
                rtspCameraOut = ConfigurationManager.AppSettings["RtspCameraOut"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("CardReaderIn"))
            {
                // Key exists
                cardReaderIn = ConfigurationManager.AppSettings["CardReaderIn"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("CardReaderOut"))
            {
                // Key exists
                cardReaderOut = ConfigurationManager.AppSettings["CardReaderOut"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("TimeWaiting"))
            {
                // Key exists
                timeWaiting = double.Parse(ConfigurationManager.AppSettings["TimeWaiting"]);
            }
        }

        /// <summary>
        /// Lưu thông tin cấu hình trong file config
        /// </summary>
        public static void SetConfig(string rtspCameraIn, string rtspCameraOut, string cardReaderIn,
            string cardReaderOut, string timeWaiting)
        {
            var cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            cf.AppSettings.Settings.Remove("RtspCameraOut");
            cf.AppSettings.Settings.Add("RtspCameraOut", rtspCameraOut);

            cf.AppSettings.Settings.Remove("RtspCameraIn");
            cf.AppSettings.Settings.Add("RtspCameraIn", rtspCameraIn);

            cf.AppSettings.Settings.Remove("CardReaderIn");
            cf.AppSettings.Settings.Add("CardReaderIn", cardReaderIn);

            cf.AppSettings.Settings.Remove("CardReaderOut");
            cf.AppSettings.Settings.Add("CardReaderOut", cardReaderOut);

            cf.AppSettings.Settings.Remove("TimeWaiting");
            cf.AppSettings.Settings.Add("TimeWaiting", timeWaiting);

            cf.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public HistoryData GetHistoryInLasted(string cardNumber)
        {
            try
            {
                var lstHistories = new HistoryData().Gets().Where(o => o.CardNumber == cardNumber);
                if (lstHistories.Any())
                {
                    var lstHistoryIn = lstHistories.Where(x => x.Type == (int)Helper.HistoryDataStatus.IN)
                        .OrderByDescending(x => x.Time).ToList();
                    var lstLogOuts = lstHistories.Where(x => x.Type == (int)Helper.HistoryDataStatus.OUT)
                        .OrderByDescending(x => x.Time).ToList();

                    if (lstLogOuts.Count > lstHistoryIn.Count)
                    {
                        return lstHistoryIn[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new HistoryData();
        }

        /// <summary>
        /// Chuẩn hóa chuỗi sang tiếng việt không dấu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertNoUnicode(string s)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public void LoadHistoryData(DateTime fromDate, DateTime toDate, ref List<HistoryData> lstHistoryDatas)
        {
            try
            {
                lstHistoryDatas.Clear();

                var lstHistoryData = new HistoryData().Gets();
                var lstStudents = new StudentData().Gets();
                var lstStudentCards = new StudentCardData().Gets();
                if (lstHistoryData != null && lstHistoryData.Any())
                {
                    foreach (var history in lstHistoryData)
                    {
                        var studentCard = lstStudentCards.FirstOrDefault(o => o.CardId == history.CardId);
                        history.StudentData = lstStudents.FirstOrDefault(o =>
                            studentCard != null && o.Id == studentCard.StudentId);
                    }

                    foreach (var itm in lstHistoryData)
                    {
                        if (fromDate <= itm.Time && toDate >= itm.Time)
                        {
                            lstHistoryDatas.Add(itm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Image LoadImageFromUrl(string imageUrl, int pictureBoxWith, int pictureBoxHeight)
        {
            try
            {
                var request = WebRequest.Create(imageUrl);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var image = Image.FromStream(stream);
                    var resizedImage = ResizeImage(image, pictureBoxWith, pictureBoxHeight);
                    return resizedImage;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Không thể tải ảnh: " + ex.Message);
                return null;
            }
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            var resizedImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        public enum HistoryDataStatus
        {
            IN = 1,
            OUT = 2
        }
    }
}