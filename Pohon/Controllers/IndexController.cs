using Microsoft.AspNetCore.Mvc;

namespace Pohon.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}