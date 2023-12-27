using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
