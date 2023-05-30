using System.Collections.Generic;
using Newtonsoft.Json;

namespace iTunes_WebApp_API.Models
{
    public class TVEpisodesResponse
    {
        [JsonProperty("results")]
        public List<TVEpisodes> Results { get; set; }
    }
}
