using Microsoft.AspNetCore.Mvc;

namespace VideoPlatform.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
