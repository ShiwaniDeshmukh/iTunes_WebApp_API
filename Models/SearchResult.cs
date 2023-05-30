using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTunes_WebApp_API.Models
{
    public class SearchResults
    {
        public List<Albums> Albums { get; set; }
        public List<Songs> Songs { get; set; }
        public List<MusicVideos> MusicVideos { get; set; }
        public List<TVEpisodes> TVEpisodes { get; set; }

        // Constructor
        public SearchResults()
        {
            Albums = new List<Albums>();
            Songs = new List<Songs>();
            MusicVideos = new List<MusicVideos>();
            TVEpisodes = new List<TVEpisodes>();
        }
    }
}
