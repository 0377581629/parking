using System;
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

        public static async Task<bool> UploadImageToServer(MultipartFormDataContent content)
        {
            var accessToken = GetAccessTokenAsync();
            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);
                var json = JsonConvert.SerializeObject(content);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{GlobalConfig.TargetDomain}{GlobalConfig.UploadImageToServerUrl}", data);

                return response.IsSuccessStatusCode;
            }
        }
    }
}