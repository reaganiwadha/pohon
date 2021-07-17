using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pohon.Constants;
using Pohon.Data;
using Pohon.External.OAuth;
using Pohon.External.OAuth.Structures;
using Pohon.Models;

namespace Pohon.Controllers
{
    public class AuthController : Controller
    {
        private readonly GithubOAuthOptions _githubOAuthOptions;
        private readonly HttpClient _httpClient;
        private readonly PohonDbContext _dbContext;
        
        public AuthController(IOptions<GithubOAuthOptions> githubOAuthOptions, HttpClient httpClient, PohonDbContext dbContext)
        {
            _githubOAuthOptions = githubOAuthOptions.Value;
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/authorize/github")]
        public async Task<IActionResult> AuthorizeGithubOAuth(string code)
        {
            var exchangeCodeResponse = await GithubOAuthClient.ExchangeCode(_githubOAuthOptions, code, _httpClient);
            var getUserResponse = await GithubOAuthClient.GetUser(exchangeCodeResponse.AccessToken, _httpClient);

            var oAuthDetail = await _dbContext.GithubOAuthDetails
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.GithubId == getUserResponse.Id);
            
            User user;
            
            if (oAuthDetail == null)
            {
                user = new User
                {
                    Username = getUserResponse.Login,
                    GithubOAuthDetails = new List<GithubOAuthDetail>
                    {
                        new()
                        {
                            NodeId = getUserResponse.NodeId,
                            Login = getUserResponse.Login,
                            GithubId = getUserResponse.Id,
                        }
                    }
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                user = oAuthDetail.User;
            }

            var claimsIdentity = PohonAuthConstants.GenerateCookieClaimsFromUser(user);
         
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), PohonAuthConstants.DefaultAuthProperties());

            return new LocalRedirectResult("/dashboard");
        }
        
    }
}