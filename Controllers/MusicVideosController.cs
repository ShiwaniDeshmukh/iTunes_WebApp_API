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
                return Ok(musicVideosResponse);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}