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
    public class SongsController : ControllerBase
    {
        private readonly DataContext _context;

        public SongsController(DataContext context)
        {
            _context = context;
        }

        // GET api/songs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Songs> songs = await _context.Songs.ToListAsync();
            return Ok(songs);
        }

        // GET api/songs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Songs song = await _context.Songs.FindAsync(id);
            if (song == null)
                return NotFound();

            return Ok(song);
        }

        // GET api/songs/search/{keyword}
        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            List<Songs> searchResults = await _context.Songs
                .Where(song => song.Title.Contains(keyword) || song.Artist.Contains(keyword))
                .ToListAsync();

            return Ok(searchResults);
        }

        // POST api/songs
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Songs song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = song.SongId }, song);
        }

        // PUT api/songs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Songs updatedSong)
        {
            Songs song = await _context.Songs.FindAsync(id);
            if (song == null)
                return NotFound();

            song.CoverImage = updatedSong.CoverImage;
            song.Title = updatedSong.Title;
            song.Artist = updatedSong.Artist;
            song.Duration = updatedSong.Duration;
            song.Price = updatedSong.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/songs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Songs song = await _context.Songs.FindAsync(id);
            if (song == null)
                return NotFound();

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
