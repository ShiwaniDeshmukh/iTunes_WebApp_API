using System.Collections.Generic;
using Newtonsoft.Json;

namespace iTunes_WebApp_API.Models
{
    public class MusicVideosResponse
    {
        [JsonProperty("results")]
        public List<MusicVideos> Results { get; set; }
    }
}
