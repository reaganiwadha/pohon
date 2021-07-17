using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pohon.External.OAuth;
using Pohon.External.OAuth.Structures;

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
        public async Task AuthorizeGithubOAuth(string code)
        {
            var exchangeCodeResponse = await GithubOAuthClient.ExchangeCode(_githubOAuthOptions, code, _httpClient);
            var getUserResponse = await GithubOAuthClient.GetUser(exchangeCodeResponse.AccessToken, _httpClient);

            // Add a repository to check the user store etc etc
            
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
            };

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, getUserResponse.Login),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
            
            // return Ok(result);
        }
        
    }
}