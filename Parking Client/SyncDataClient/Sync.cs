using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace SyncDataClient
{
    public static class Sync
    {
        private static string GetAccessTokenAsync()
        {
            var client = new HttpClient();

            var disco = client.GetDiscoveryDocumentAsync(GlobalConfig.TargetDomain).Result;

            if (disco.IsError)
            {
                return null;
            }

            client.DefaultRequestHeaders.Add("Abp.TenantId", GlobalConfig.TenantId);
            var tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = GlobalConfig.ClientId,
                ClientSecret = GlobalConfig.ClientSecret,
                Scope = GlobalConfig.ClientScope,

                UserName = GlobalConfig.UserName,
                Password = GlobalConfig.Password
            }).Result;

            return tokenResponse.IsError ? null : tokenResponse.AccessToken;
        }

        public static async Task<bool> UploadImageToServer(string imgPath, string fileName)
        {
            var accessToken = GetAccessTokenAsync();
            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);
                using (var content = new MultipartFormDataContent())
                {
                    var stringContent = new StringContent(GlobalConfig.HistoryImageFolderName);
                    content.Add(stringContent, "path");

                    var imageBytes = File.ReadAllBytes(imgPath);
                    var imageContent = new ByteArrayContent(imageBytes);
                    content.Add(imageContent, "file", fileName);

                    var response =
                        await client.PostAsync($"{GlobalConfig.TargetDomain}{GlobalConfig.UploadImageToServerUrl}",
                            content);

                    return response.IsSuccessStatusCode;
                }
            }
        }

        public static async Task<string> RecognitionLicensePlate(byte[] imageBytes, string fileName)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    var imageContent = new ByteArrayContent(imageBytes);

                    content.Add(imageContent, "image", fileName);

                    // License plate detection and recognition
                    var response = await client.PostAsync(GlobalConfig.PythonUploadImageUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
            }
        }
    }
}