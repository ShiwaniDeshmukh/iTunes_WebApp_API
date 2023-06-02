using Newtonsoft.Json;
using System;

namespace iTunes_WebApp_API.Models
{
    public class SongDetailsViewModel
    {
        [JsonProperty("wrapperType")]
        public string WrapperType { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("artistName")]
        public string ArtistName { get; set; }

        [JsonProperty("collectionName")]
        public string CollectionName { get; set; }

        [JsonProperty("collectionPrice")]
        public decimal? CollectionPrice { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }

        [JsonProperty("artworkUrl100")]
        public string ArtworkUrl100 { get; set; }

        [JsonProperty("trackPrice")]
        public decimal? TrackPrice { get; set; }

        [JsonProperty("releaseDate")]
        public DateTime? ReleaseDate { get; set; }

        public string ViewDetailsUrl { get; set; }

        [JsonProperty("clickCount")]
        public int ClickCount { get; set; }

        [JsonProperty("trackId")]
        public int TrackId { get; set; }
    }
}
