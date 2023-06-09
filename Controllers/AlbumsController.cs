﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace iTunes_WebApp_API.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class AlbumsController : Controller
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
        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://itunes.apple.com/lookup?id={id}&entity=album");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var albumResponse = JsonConvert.DeserializeObject<AlbumsResponse>(json);

                if (albumResponse?.Results?.Count > 0)
                {
                    var album = albumResponse.Results[0];
                    return View(album);
                }
            }

            return NotFound();
        }
    }
}

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
}*/
