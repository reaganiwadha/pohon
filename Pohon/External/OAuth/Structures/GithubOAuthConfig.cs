namespace Pohon.External.OAuth.Structures
{
    public record GithubOAuthOptions
    {
        private const string GithubBaseAuthorizeUri = "https://github.com/login/oauth/authorize";

        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public string RedirectUri { get; init; }

        public string AuthorizeUri => $"{GithubBaseAuthorizeUri}/?client_id={ClientId}&redirect_uri={RedirectUri}";
    }
}