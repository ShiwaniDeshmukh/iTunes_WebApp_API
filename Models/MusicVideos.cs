using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTunes_WebApp_API.Models
{
    public class MusicVideos
    {
        [Key]
        public int MusicVideoId { get; set; }


        public string CoverImage { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public int Duration { get; set; }

        public double Price { get; set; }

        //Albums
        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public Albums Albums { get; set; } // Relationship: Song can have only one Album
    }
}
