using System.Collections;
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
    public class MusicVideosController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public MusicVideosController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<MusicVideosResponse>> GetMusicVideos(string term)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/search?term={term}&entity=musicVideo");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var musicVideosResponse = JsonConvert.DeserializeObject<MusicVideosResponse>(json);

                // Generate the details URL for each music video
                foreach (var musicVideo in musicVideosResponse.Results)
                {
                    musicVideo.ViewDetailsUrl = Url.Action("Details", new { id = musicVideo.videoId });
                }

                return Ok(musicVideosResponse);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpGet("{id}")]
        /*public IActionResult Details(int id)
        {
            // Fetch the details of the music video with the given id
            // Example code:
            var musicVideo = GetMusicVideoDetailsFromDatabase(id);

            if (musicVideo == null)
            {
                return NotFound();
            }

            //return View(musicVideo);
        }*/

        private MusicVideos GetMusicVideoDetailsFromDatabase(int id)
        {
            // Retrieve the music video details from the database using the provided id
            // Example code:
            // var musicVideo = _database.GetMusicVideo(id);
            // return musicVideo;

            // In this example, assume a MusicVideo class is defined for holding the music video details
            return new MusicVideos { videoId = id, trackName = "Music Video Title", collectionName = "Album", artistName = "Artist", artworkUrl100 = "Music Video", trackPrice = (decimal?) 9.99 };
    }
    }
}
