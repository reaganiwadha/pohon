using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pohon.Config;

namespace Pohon.Controllers
{
    public class IndexController : Controller
    {
        private readonly GithubOAuthOptions _githubOAuthConfig;
        
        public IndexController(IOptions<GithubOAuthOptions> githubOAuthOptions)
        {
            _githubOAuthConfig = githubOAuthOptions.Value;
        }
        
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["Github.AuthorizeUri"] = _githubOAuthConfig.AuthorizeUri;
            return View();
        }
    }
}