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
    public class MusicVideosController : ControllerBase
    {
        private readonly DataContext _context;

        public MusicVideosController(DataContext context)
        {
            _context = context;
        }

        // GET api/musicvideos/all
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            List<MusicVideos> musicVideos = await _context.MusicVideos.ToListAsync();
            return Ok(musicVideos);
        }

        // GET api/musicvideos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MusicVideos musicVideo = await _context.MusicVideos.FindAsync(id);
            if (musicVideo == null)
                return NotFound();

            return Ok(musicVideo);
        }

        // GET api/musicvideos/search/{keyword}
        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> Search(string keyword)
        {
            List<MusicVideos> searchResults = await _context.MusicVideos
                .Where(mv => mv.Title.Contains(keyword) || mv.Artist.Contains(keyword))
                .ToListAsync();

            return Ok(searchResults);
        }

        // POST api/musicvideos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MusicVideos musicVideo)
        {
            _context.MusicVideos.Add(musicVideo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = musicVideo.MusicVideoId }, musicVideo);
        }

        // PUT api/musicvideos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MusicVideos updatedMusicVideo)
        {
            MusicVideos musicVideo = await _context.MusicVideos.FindAsync(id);
            if (musicVideo == null)
                return NotFound();

            musicVideo.CoverImage = updatedMusicVideo.CoverImage;
            musicVideo.Title = updatedMusicVideo.Title;
            musicVideo.Artist = updatedMusicVideo.Artist;
            musicVideo.Duration = updatedMusicVideo.Duration;
            musicVideo.Price = updatedMusicVideo.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/musicvideos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            MusicVideos musicVideo = await _context.MusicVideos.FindAsync(id);
            if (musicVideo == null)
                return NotFound();

            _context.MusicVideos.Remove(musicVideo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
