using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Pohon.External.OAuth.Structures
{
    public record GithubGetUserResponse
    {
        [JsonProperty("login")] public string Login { get; init; }
        [JsonProperty("id")] public int Id { get; init; }
        [JsonProperty("node_id")] public string NodeId { get; init; }
    }
}