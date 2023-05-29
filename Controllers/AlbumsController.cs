using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTunes_WebApp_API.Data;
using iTunes_WebApp_API.Models;

namespace iTunes_WebApp_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly DataContext _context;

        public AlbumsController(DataContext context)
        {
            _context = context;
        }

        // GET 
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            List<Albums> albums = await _context.Albums.ToListAsync();
            return Ok(albums);
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Albums album = await _context.Albums.FindAsync(id);
            if (album == null)
                return NotFound();

            return Ok(album);
        }

        // GET 
        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            List<Albums> searchResults = await _context.Albums
                .Where(album => album.Title.Contains(keyword) || album.Artist.Contains(keyword))
                .ToListAsync();

            return Ok(searchResults);
        }

        // POST 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Albums album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = album.AlbumId }, album);
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Albums updatedAlbum)
        {
            Albums album = await _context.Albums.FindAsync(id);
            if (album == null)
                return NotFound();

            album.CoverImage = updatedAlbum.CoverImage;
            album.Title = updatedAlbum.Title;
            album.Artist = updatedAlbum.Artist;
            album.ReleaseYear = updatedAlbum.ReleaseYear;
            album.Price = updatedAlbum.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Albums album = await _context.Albums.FindAsync(id);
            if (album == null)
                return NotFound();

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
