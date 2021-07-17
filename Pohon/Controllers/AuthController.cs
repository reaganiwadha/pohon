using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pohon.Config;
using Pohon.External.OAuth;

namespace Pohon.Controllers
{
    public class AuthController : Controller
    {
        private readonly GithubOAuthOptions _githubOAuthOptions;
        private readonly HttpClient _httpClient;
        
        public AuthController(IOptions<GithubOAuthOptions> githubOAuthOptions, HttpClient httpClient)
        {
            _githubOAuthOptions = githubOAuthOptions.Value;
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("/authorize/github")]
        public async Task<IActionResult> AuthorizeGithubOAuth(string code)
        {
            var result = await GithubOAuthClient.ExchangeCode(_githubOAuthOptions, code, _httpClient);
            return Ok(result);
        }
    }
}