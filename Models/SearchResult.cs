using System.Collections.Generic;

namespace iTunes_WebApp_API.Models
{
    public class SearchResult
    {
        public List<Albums> Albums { get; set; }
        public List<Songs> Songs { get; set; }
        public List<MusicVideos> MusicVideos { get; set; }
    }
}
