using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTunes_WebApp_API.Models
{
    public class TVEpisodes
    {
        public string WrapperType { get; set; }
        public string Kind { get; set; }
        public string ArtistName { get; set; }
        public string CollectionName { get; set; }
        public string TrackName { get; set; }
        public string ArtworkUrl100 { get; set; }
        public decimal? TrackPrice { get; set; }
        public DateTime? ReleaseDate { get; set; }

    }
}
