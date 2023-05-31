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
    public class TVEpisodesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public TVEpisodesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<TVEpisodesResponse>> GetTVEpisodes(string term)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/search?term={term}&entity=tvEpisode");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tvEpisodesResponse = JsonConvert.DeserializeObject<TVEpisodesResponse>(json);

                // Generate the details URL for each TV episode
                foreach (var episode in tvEpisodesResponse.Results)
                {
                    episode.ViewDetailsUrl = Url.Action("Details", new { id = episode.episodeId });
                }

                return Ok(tvEpisodesResponse);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpGet("{id}")]
        /*public IActionResult Details(int id)
        {
            // Fetch the details of the TV episode with the given id
            // Example code:
            var episode = GetTVEpisodeDetailsFromDatabase(id);

            if (episode == null)
            {
                return NotFound();
            }

         //   return View(episode);
        }*/

        private TVEpisodes GetTVEpisodeDetailsFromDatabase(int id)
        {
            // Retrieve the TV episode details from the database using the provided id
            // Example code:
            // var episode = _database.GetTVEpisode(id);
            // return episode;

            // In this example, assume a TVEpisode class is defined for holding the TV episode details
            return new TVEpisodes { episodeId = id, trackName = "Episode Title", artistName = "Artist", collectionName = "Album", artworkUrl100 = "TV-Episode", trackPrice = (decimal?) 9.99};
        }
    }
}
