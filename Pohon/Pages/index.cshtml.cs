using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Pohon.Config;

namespace Pohon.Pages
{
    public class Index : PageModel
    {
        private readonly GithubOAuthConfig _githubOAuthConfig;
        
        public Index(IOptions<GithubOAuthConfig> githubOAuthConfig)
        {
            _githubOAuthConfig = githubOAuthConfig.Value;
        }
        
        [BindProperty(SupportsGet = true)]
        public string CheckQuery { get; set; }
        public IActionResult OnGet()
        {
            ViewData["Github.AuthorizeUri"] = _githubOAuthConfig.AuthorizeUri;
            Console.WriteLine(CheckQuery);
            return Page();
        }
    }

}