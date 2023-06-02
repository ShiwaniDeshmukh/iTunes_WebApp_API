using Newtonsoft.Json;
using System;

namespace iTunes_WebApp_API.Models
{
    public class TVEpisodeDetailsViewModel
    {
        [JsonProperty("trackId")]
        public int episodeId { get; set; }
        public string wrapperType { get; set; }
        public string kind { get; set; }
        public string artistName { get; set; }
        public string collectionName { get; set; }
        public string trackName { get; set; }
        public string artworkUrl100 { get; set; }
        public decimal? trackPrice { get; set; }
        public DateTime? releaseDate { get; set; }
        public string ViewDetailsUrl { get; set; }
        public int ClickCount { get; set; }
    }
}
