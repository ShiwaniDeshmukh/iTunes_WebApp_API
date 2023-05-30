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
        public async Task<ActionResult<AlbumDetails>> GetAlbumDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/lookup?id={id}&entity=song");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var songsResponse = JsonConvert.DeserializeObject<SongsResponse>(json);

                // Create an AlbumDetails object and populate it with the album information
                var albumDetails = new AlbumDetails
                {
                    Album = songsResponse.Results[0].CollectionName,
                    Artist = songsResponse.Results[0].ArtistName,
                    ArtworkUrl = songsResponse.Results[0].ArtworkUrl100,
                    Songs = new List<Song>()
                };

                // Iterate over the songs and add them to the album's list of songs
                foreach (var song in songsResponse.Results)
                {
                    albumDetails.Songs.Add(new Song
                    {
                        TrackName = song.TrackName,
                        TrackPrice = song.TrackPrice,
                        ReleaseDate = song.ReleaseDate
                    });
                }

                return Ok(albumDetails);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}