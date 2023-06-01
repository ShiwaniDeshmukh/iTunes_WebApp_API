using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

// Add this using directive to reference the DataContext class
using iTunes_WebApp_API.Data;

namespace iTunes_WebApp_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context; // Add a private field for DataContext

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context; // Initialize the DataContext field
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

        [HttpPost]
        public IActionResult IncrementClickCount(int trackId)
        {
            // Use the initialized DataContext field instead of creating a new instance
            // using (var context = new DataContext())
            // {
            // Find the song and its click count
            var song = _context.Songs.FirstOrDefault(s => s.Id == trackId);
            var clickCount = _context.ClickCounts.FirstOrDefault(c => c.TrackId == trackId);

            if (song != null && clickCount != null)
            {
                // Increment the click count
                clickCount.Count++;
                _context.SaveChanges();
            }
            // }

            // Return a success response
            return Ok();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//Error after clicking on track name link - but deatils is opening,
//we need a back button from there to get returned on the search resutls page but the click count did increase
//UI for details page
//Implement the track thing for all albums, musicvideos, tv episodes
//mobile optimized
//Account Controller
//Authentication Controller
//Purchase Controller