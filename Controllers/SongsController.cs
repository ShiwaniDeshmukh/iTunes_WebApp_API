using iTunesWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iTunesWebApp.Controllers
{
    public class SongsController : Controller
    {
        private readonly AppDbContext _context;

        public SongsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dataSongs = await _context.Songs.ToListAsync();
            return View(dataSongs);
        }
    }
}
