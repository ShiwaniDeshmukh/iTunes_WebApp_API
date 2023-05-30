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
                return Ok(tvEpisodesResponse);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}