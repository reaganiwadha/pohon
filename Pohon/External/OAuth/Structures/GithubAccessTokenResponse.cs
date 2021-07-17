using Newtonsoft.Json;

namespace Pohon.External.OAuth.Structures
{
    public record GithubAccessTokenResponse
    {
        [JsonProperty("token_type")] public string TokenType { get; init; }
        [JsonProperty("scope")] public string Scope { get; init; }
        [JsonProperty("access_token")] public string AccessToken { get; init; }
    }
}