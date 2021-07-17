using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pohon.External.OAuth.Structures;

namespace Pohon.External.OAuth
{
    public static class GithubOAuthClient
    {
        private const string GithubAccessTokenEndpoint = "https://github.com/login/oauth/access_token";
        private const string GithubRequestUserEndpoint = "https://api.github.com/user";
        
        public static async Task<GithubAccessTokenResponse> ExchangeCode(GithubOAuthOptions options, string code, HttpClient client)
        {
            var payload = new GithubAccessTokenRequest
            {
                ClientId = options.ClientId,
                ClientSecret = options.ClientSecret,
                Code = code,
                RedirectUri = options.RedirectUri
            };
            var stringPayload = JsonConvert.SerializeObject(payload);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, GithubAccessTokenEndpoint);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            requestMessage.Content = httpContent;
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = await client.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GithubAccessTokenResponse>(responseString);
        }

        public static async Task<GithubGetUserResponse> GetUser(string token, HttpClient client)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, GithubRequestUserEndpoint);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            requestMessage.Headers.UserAgent.Add(new ProductInfoHeaderValue("pohon", "1.0"));
            var response = await client.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GithubGetUserResponse>(responseString);
        }
    }
}