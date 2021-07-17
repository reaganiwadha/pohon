using Microsoft.AspNetCore.Mvc;

namespace Pohon.Controllers
{
    public class DashboardController : Controller
    {
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}