using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pohon.Config;

namespace Pohon.External.OAuth
{
    public record GithubAccessTokenRequest
    {
        [JsonProperty("client_id")] public string ClientId { get; init; }
        [JsonProperty("client_secret")] public string ClientSecret { get; init; }
        [JsonProperty("code")] public string Code { get; init; }
        [JsonProperty("redirect_uri")] public string RedirectUri { get; init; }
    }

    public record GithubAccessTokenResponse
    {
        [JsonProperty("token_type")] public string TokenType { get; init; }
        [JsonProperty("scope")] public string Scope { get; init; }
        [JsonProperty("access_token")] public string AccessToken { get; init; }
    }
    
    public static class GithubOAuthClient
    {
        private const string GithubAccessTokenEndpoint = "https://github.com/login/oauth/access_token";
        
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
            Console.WriteLine(stringPayload);
            requestMessage.Content = httpContent;
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = await client.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            return JsonConvert.DeserializeObject<GithubAccessTokenResponse>(responseString);
        }
    }
}