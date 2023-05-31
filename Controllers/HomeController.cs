using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iTunes_WebApp_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/GoToiTunesStore
        public IActionResult GoToiTunesStore()
        {
            return RedirectToAction("Index", "iTunesStore");
        }



        // GET: Home/SignIn
        public IActionResult SignIn()
        {
            return RedirectToAction("SignIn", "Authentication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
