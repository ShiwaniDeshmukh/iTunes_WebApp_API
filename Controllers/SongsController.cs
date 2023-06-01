using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using iTunes_WebApp_API.Utilities;

namespace iTunes_WebApp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SongsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [HttpPost]
        public IActionResult TrackClick(int trackId)
        {
            ClickCountTracker.IncrementClickCount(trackId);
            return Ok();
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
                    var song = songsResponse.Results.FirstOrDefault();
                    var songDetailsViewModel = new SongDetailsViewModel
                    {
                        trackId = song.trackId,
                        wrapperType = song.wrapperType,
                        kind = song.kind,
                        artistName = song.artistName,
                        collectionName = song.collectionName,
                        trackName = song.trackName,
                        artworkUrl100 = song.artworkUrl100,
                        trackPrice = song.trackPrice,
                        releaseDate = song.releaseDate,
                        ViewDetailsUrl = song.ViewDetailsUrl,
                        ClickCount = ClickCountTracker.GetClickCount(id)
                    };

                    // Increment click count
                    IncrementClickCount(id);

                    return View("Details", songDetailsViewModel);

                }
            }

            return NotFound();
        }

        private void IncrementClickCount(int id)
        {
            ClickCountTracker.IncrementClickCount(id);
        }
    }
}