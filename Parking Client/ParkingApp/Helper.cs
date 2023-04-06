using ParkingLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SyncDataModels;

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

        public StudentData GetStudentByCode(string cardNumber)
        {
            StudentData student = null;
            try
            {
                var lstStudents = new StudentData().Gets();
                var lstCards = new CardData().Gets();
                var lstStudentCard = new StudentCardData().Gets();

                var card = lstCards.FirstOrDefault(o => o.CardNumber.Contains(cardNumber.Trim()));
                var studentCard = lstStudentCard.FirstOrDefault(o => o.CardId == card.Id);
                student = lstStudents.FirstOrDefault(o => o.Id == studentCard.StudentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return student;
        }

        public HistoryData GetLogInLasted(string cardNumber)
        {
            try
            {
                var lstHistories = new HistoryData().Gets().Where(o => o.CardNumber == cardNumber);
                if (lstHistories.Any())
                {
                    var lstLogIns = lstHistories.Where(x => x.Type == (int) Helper.HistoryDataStatus.In)
                        .OrderByDescending(x => x.Time).ToList();
                    var lstLogOuts = lstHistories.Where(x => x.Type == (int) Helper.HistoryDataStatus.Out)
                        .OrderByDescending(x => x.Time).ToList();

                    if (lstLogOuts.Count > lstLogIns.Count)
                    {
                        return lstLogIns[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new HistoryData();
        }

        public int AddLogHistory(HistoryData data, ref string mes)
        {
            try
            {
                var log = new HistoryData
                {
                    Id = data.Id,
                    CardId = data.CardId,
                    CardCode = data.CardCode,
                    CardNumber = data.CardNumber,
                    LicensePlate = data.LicensePlate,
                    Price = data.Price,
                    Time = data.Time,
                    Type = data.Type,
                    Photo = data.Photo,
                    CardTypeName = data.CardTypeName,
                    VehicleTypeName = data.VehicleTypeName
                };
                log.Add();
                return log.Id;
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return 0;
                throw;
            }
        }

        /// <summary>
        /// chuyển đổi file ảnh -> string base 64
        /// </summary>
        /// <param name="path">Đường dẫn lưu file ảnh</param>
        /// <returns></returns>
        public static string PathImageToBase64(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return "";
            }
            else
            {
                System.Threading.Thread.Sleep(5000);
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            var base64String = Convert.ToBase64String(imageBytes);
                            return base64String;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Chuyển đổi đối tượng Bitmap (ảnh) --> string base 64
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ImageToBase64(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var base64String = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                return base64String;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    return image;
                }
            }

            return null;
        }

        /// <summary>
        /// Chuẩn hóa chuỗi sang tiếng việt không dấu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertNoUnicode(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public bool LoadHistoryData(DateTime fromDate, DateTime toDate, ref List<HistoryData> lstHistoryDatas,
            bool fakeNew = true)
        {
            try
            {
                lstHistoryDatas.Clear();
                if (!fakeNew)
                {
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
                            // history.HistoryDate = DateTime.ParseExact(history.HistoryDateStr, "dd/MM/yyyy HH:mm:ss",
                            //     System.Globalization.CultureInfo.InvariantCulture);
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
                else
                {
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public enum HistoryDataStatus
        {
            In = 1,
            Out = 2
        }
    }
}