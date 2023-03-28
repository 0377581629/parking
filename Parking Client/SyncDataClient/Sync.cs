using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using SyncDataModels;

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

        public static async Task<SyncClientDto> GetSyncClientData()
        {
            var accessToken = GetAccessTokenAsync();
            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);
                var response = client.GetAsync($"{GlobalConfig.TargetDomain}{GlobalConst.AvailableDomitoryDataUrl}")
                    .Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    var res = JsonConvert.DeserializeObject<SyncClientDto>(content);
                    return res;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static async Task<bool> SendSyncClientData(SyncClientDto syncData)
        {
            var accessToken = GetAccessTokenAsync();
            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);
                var json = JsonConvert.SerializeObject(syncData);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response =
                    await client.PostAsync($"{GlobalConfig.TargetDomain}/{GlobalConst.SendDomitoryDataInfoUrl}", data);

                return response.IsSuccessStatusCode;
            }
        }
    }
}