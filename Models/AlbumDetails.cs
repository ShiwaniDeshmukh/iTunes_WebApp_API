using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTunes_WebApp_API.Models
{
    public class AlbumDetails
    {
        public string album { get; set; }
        public string artist { get; set; }
        public string artworkUrl100 { get; set; }
        public List<Songs> Songs { get; set; }
    }
}
