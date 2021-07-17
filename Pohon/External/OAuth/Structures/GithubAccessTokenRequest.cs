using Newtonsoft.Json;

namespace Pohon.External.OAuth.Structures
{
    public record GithubAccessTokenRequest
    {
        [JsonProperty("client_id")] public string ClientId { get; init; }
        [JsonProperty("client_secret")] public string ClientSecret { get; init; }
        [JsonProperty("code")] public string Code { get; init; }
        [JsonProperty("redirect_uri")] public string RedirectUri { get; init; }
    }
}