using ParkingLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParkingApp
{
     class Helper
    {
        /// <summary>
        /// Lấy thông tin cấu hình trong file config
        /// </summary>
        public static void GetConfig(ref string rtspCameraIn, ref string rtspCameraOut, ref string cardReaderIn, ref string cardReaderOut, ref double timeWaiting)
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
        public static void SetConfig(string rtspCameraIn, string rtspCameraOut, string cardReaderIn, string cardReaderOut, string timeWaiting)
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
                if (lstStudents.Any())
                {
                    student = lstStudents.FirstOrDefault(x => x.CardNumber.Contains(cardNumber.Trim()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return student;
        }

        public SecurityData GetLogInLasted(string cardNumber)
        {
            try
            {
                var lstLogs = new SecurityData() { CardNumber = cardNumber }.GetByCardNumbers();
                if(lstLogs.Any())
                {
                    var lstLogIns = lstLogs.Where(x => x.Status == (int)Helper.SecurityDataStatus.In).OrderByDescending(x => x.SecurityDate).ToList();
                    var lstLogOuts = lstLogs.Where(x => x.Status == (int)Helper.SecurityDataStatus.Out).OrderByDescending(x => x.SecurityDate).ToList();

                    if(lstLogOuts.Count > lstLogIns.Count)
                    {
                        return lstLogIns[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new SecurityData();
        }

        public int AddLogSecurity(SecurityData data, ref string mes)
        {
            try
            {
                var log = new SecurityData
                {
                    Id = data.PersonId,
                    StudentId = data.StudentId,
                    PersonId = data.PersonId,
                    CamCode = data.CamCode,
                    CardNumber = data.CardNumber,
                    ParentId = data.ParentId,
                    PhotoUrl = data.PhotoUrl,
                    SecurityDateStr = data.SecurityDateStr,
                    Status = 0
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

        /// <summary>
        /// Lưu dữ liệu lấy từ server về DB local
        /// </summary>
        /// <param name="educateData"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public bool SyncDownData(SyncDataModels.SyncClientDto syncData, ref string mes)
        {
            try
            {
                if (syncData == null || syncData.ListStudentData == null || syncData.ListSecurityData == null)
                {
                    mes = "Không có dữ liệu !";
                    return false;
                }

                // Sync Candidate
                if (syncData.ListStudentData.Any())
                {
                    foreach (var itm in syncData.ListStudentData)
                    {
                        var candidate = new StudentData()
                        {
                            Id = (int)itm.Id,
                            Code = itm.Code,
                            PersonId = (int)itm.PersonId,
                            FirstName = itm.FirstName,
                            LastName = itm.LastName,
                            PeopleId = itm.PeopleId,
                            Avatar = itm.Avatar,
                            AvatarBase64 = itm.AvatarBase64,
                            Male = itm.Male ? 1 : 0,
                            DoBStr = itm.DoBStr,

                            CardDateStr = itm.CardDateStr,
                            CardNumber = itm.CardNumber,

                            ClassName = itm.ClassName
                        };

                        candidate.Add();
                    }
                }

                // Sync FingerData
                if (syncData.ListSecurityData != null && syncData.ListSecurityData.Any())
                {
                    foreach (var itm in syncData.ListSecurityData)
                    {
                        var security = new SecurityData()
                        {
                            Id = (int)itm.Id,
                            StudentId = itm.StudentId,
                            CardNumber = itm.CardNumber,
                            PersonId = (int)itm.PersonId,
                            CamCode = itm.CamCode,
                            PhotoUrl = itm.PhotoUrl,
                            PhotoBase64 = itm.PhotoBase64,
                            SecurityDateStr = itm.SecurityDateStr
                        };

                        security.Add();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return false;
                throw;
            }
        }


        /// <summary>
        /// Push dữ liệu từ local lên server
        /// </summary>
        /// <param name="syncData"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public bool SyncUpData(ref SyncDataModels.SyncClientDto syncData, ref string mes)
        {
            try
            {
                if (syncData == null) syncData = new SyncDataModels.SyncClientDto();

                var lstSecurityData = new SecurityData().Gets();

                if (lstSecurityData.Any())
                {

                    var lstSecurityDataLocal = lstSecurityData.Where(x => x.Status == 0);

                    syncData.ListSecurityData = new List<SyncDataModels.SecurityData>();
                    foreach (var itm in lstSecurityDataLocal)
                    {
                        var securityDataServer = new SyncDataModels.SecurityData()
                        {
                            Id = itm.Id,
                            StudentId = itm.StudentId,
                            
                            ParentId = itm.ParentId,
                            PersonId = itm.PersonId,
                            CamCode = itm.CamCode,
                            SecurityDate = DateTime.ParseExact(itm.SecurityDateStr, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                            SecurityDateStr = itm.SecurityDateStr,
                            CardNumber = itm.CardNumber,
                            PhotoUrl = itm.PhotoUrl,
                            PhotoBase64 = itm.PhotoBase64
                        };
                        //

                        if(!string.IsNullOrEmpty(securityDataServer.PhotoUrl) && File.Exists(securityDataServer.PhotoUrl))
                        {
                            securityDataServer.PhotoBase64 = PathImageToBase64(securityDataServer.PhotoUrl);
                        }
                        syncData.ListSecurityData.Add(securityDataServer);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return false;
                throw;
            }
        }


        public bool LoadSecurityData(DateTime fromDate, DateTime toDate, ref List<SecurityData> lstSecurityDatas, bool fakeNew = true)
        {
            try
            {
                lstSecurityDatas.Clear();
                if (!fakeNew)
                {
                    var lstSecurityData = new SecurityData().Gets();
                    var lstStudents = new StudentData().Gets();
                    if(lstSecurityData!= null && lstSecurityData.Any())
                    {
                        foreach (var itm in lstSecurityData)
                        {
                            itm.StudentInfo = lstStudents.FirstOrDefault(o => o.Id == itm.StudentId);
                            itm.SecurityDate = DateTime.ParseExact(itm.SecurityDateStr, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        }

                        foreach (var itm in lstSecurityData)
                        {
                            if(fromDate <= itm.SecurityDate && toDate >= itm.SecurityDate)
                            {
                                lstSecurityDatas.Add(itm);
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

        public enum SecurityDataStatus
        {
            In = 1,
            Out = 2
        }
    }
}
