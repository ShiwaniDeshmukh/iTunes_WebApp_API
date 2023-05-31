using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTunes_WebApp_API.Models
{
    public class Albums
    {
        public int albumId { get; set; }
        public string wrapperType { get; set; }
        public string kind { get; set; }
        public string artistName { get; set; }
        public string collectionName { get; set; }
        public string artworkUrl100 { get; set; }
        public decimal? collectionPrice { get; set; }
        public DateTime? releaseDate { get; set; }
        public string ViewDetailsUrl { get; set; }
    }
}
