using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace iTunes_WebApp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public SongsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<SongsResponse>> GetSongs(string term)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/search?term={term}&entity=song");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var songsResponse = JsonConvert.DeserializeObject<SongsResponse>(json);

                // Generate the details URL for each search item
                foreach (var song in songsResponse.Results)
                {
                    song.ViewDetailsUrl = Url.Action("Details", new { id = song.trackId });
                }

                return Ok(songsResponse);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/lookup?id={id}&entity=song");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var songsResponse = JsonConvert.DeserializeObject<SongsResponse>(json);

                if (songsResponse?.Results?.Count > 0)
                {
                    var song = songsResponse.Results[0];
                    // return View(song);
                }
            }

            return NotFound();
        }
    }
}