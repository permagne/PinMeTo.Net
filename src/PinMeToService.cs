using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace PinMeTo.Net
{
    public class PinMeToService : IPinMeToService
    {
        private readonly string _appUrl;
        private readonly string _baseUrl;
        private readonly string _token;

        public PinMeToService(string appId, string appSecret, string appUrl, string baseUrl)
        {
            _baseUrl = baseUrl;
            _appUrl = string.Format("v1/{0}/locations/", appUrl);
            _token = GetToken(appId, appSecret).Result;
        }

        public PinMeToService()
        {
            _baseUrl = ConfigurationManager.AppSettings["PinMeToApiUrl"]; ;
            var appSecret = ConfigurationManager.AppSettings["PinMeToAppSecret"];
            var appId = ConfigurationManager.AppSettings["PinMeToAppId"];
            _appUrl = string.Format("v1/{0}/locations/", ConfigurationManager.AppSettings["PinMeToAppUrl"]);
            _token = GetToken(appId, appSecret).Result;
        }

        public async Task<string> GetToken(string appId, string appSecret)
        {
            string authUrl = string.Format("{0}{1}", _baseUrl, "oauth/token");
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, authUrl);
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", appId},
                    {"client_secret", appSecret},
                    {"grant_type", "client_credentials"}
                });
                var response = client.SendAsync(request);
                var result = await response.Result.Content.ReadAsStringAsync();
                var bearerToken = JObject.Parse(result)["access_token"].ToString();
                return bearerToken;
            }
        }

        public async Task<ILocationData> GetLocation<T>(string locationId) where T : ILocationData
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var request = new HttpRequestMessage(HttpMethod.Get, _appUrl + locationId + "?access_token=" + _token);
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;
                var locationText = await response.Content.ReadAsStringAsync();
                var location = JObject.Parse(locationText).SelectToken("data").ToObject<T>(); ;
                return location;
            }
        }

        public async Task<string> AddLocation(ILocationData location)
        {
            return await DoRequest(location, HttpMethod.Post, _appUrl);
        }

        public async Task<string> UpdateLocation(ILocationData location, string locationId)
        {
            location.StoreId = null;
            return await DoRequest(location, HttpMethod.Put, _appUrl + locationId);
        }

        private async Task<string> DoRequest(ILocationData location, 
            HttpMethod httpMethod, string requestUri)
        {
            var json = JsonConvert.SerializeObject(location, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(httpMethod, requestUri);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}