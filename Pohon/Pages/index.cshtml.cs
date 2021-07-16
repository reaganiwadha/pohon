using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pohon.Pages
{
    public class Index : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CheckQuery { get; set; }
        public IActionResult OnGet()
        {
            Console.WriteLine(CheckQuery);
            return Page();
        }
    }
}