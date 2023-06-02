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
                    song.ViewDetailsUrl = Url.Action("Details", new { id = song.TrackId });
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
                    ClickCountTracker.IncrementClickCount(id);
                    var songDetailsViewModel = new SongDetailsViewModel
                    {
                        TrackId = song.TrackId,
                        WrapperType = song.WrapperType,
                        Kind = song.Kind,
                        ArtistName = song.ArtistName,
                        CollectionName = song.CollectionName,
                        TrackName = song.TrackName,
                        ArtworkUrl100 = song.ArtworkUrl100,
                        TrackPrice = song.TrackPrice,
                        ReleaseDate = song.ReleaseDate,
                        ViewDetailsUrl = song.ViewDetailsUrl,
                        ClickCount = ClickCountTracker.GetClickCount(id)
                    };

                    return View(songDetailsViewModel);
                }
            }

            return NotFound();
        }
    }
}
