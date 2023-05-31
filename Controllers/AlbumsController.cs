/*using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace iTunes_WebApp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public AlbumsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<AlbumsResponse>> GetAlbums(string term)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/search?term={term}&entity=album");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var albumsResponse = JsonConvert.DeserializeObject<AlbumsResponse>(json);
                return Ok(albumsResponse);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDetails>> Details(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/lookup?id={id}&entity=song");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var songsResponse = JsonConvert.DeserializeObject<SongsResponse>(json);

                if (songsResponse.Results.Count > 0)
                {
                    // Create an AlbumDetails object and populate it with the album information
                    var albumDetails = new AlbumDetails
                    {
                        album = songsResponse.Results[0].collectionName,
                        artist = songsResponse.Results[0].artistName,
                        artworkUrl100 = songsResponse.Results[0].artworkUrl100,
                        Songs = new List<Songs>()
                    };

                    // Iterate over the songs and add them to the album's list of songs
                    foreach (var song in songsResponse.Results)
                    {
                        albumDetails.Songs.Add(new Songs
                        {
                            trackName = song.trackName,
                            trackPrice = song.trackPrice,
                            releaseDate = song.releaseDate
                        });
                    }

                    return Ok(albumDetails);
                }
                else
                {
                    return NotFound("No songs found for the specified album.");
                }
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}*/

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
    public class AlbumsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public AlbumsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<Albums>>> GetAlbums(string term)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/search?term={term}&entity=album");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var albumsResponse = JsonConvert.DeserializeObject<AlbumsResponse>(json);
                return Ok(albumsResponse.Results);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

    }
}

