using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pohon.External.OAuth.Structures;

namespace Pohon.Controllers
{
    public class IndexController : Controller
    {
        private readonly GithubOAuthOptions _githubOAuthOptions;
        
        public IndexController(IOptions<GithubOAuthOptions> githubOAuthOptions)
        {
            _githubOAuthOptions = githubOAuthOptions.Value;
        }
        
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["Github.AuthorizeUri"] = _githubOAuthOptions.AuthorizeUri;
            return View();
        }
    }
}