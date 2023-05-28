using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTunes_WebApp_API.Models
{
    public class Albums
    {
        [Key]
        public int AlbumId { get; set; }

        public string CoverImage { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }
       
        public int ReleaseYear { get; set; }

        public double Price { get; set; }

        //Relationships
        public List<Songs> Songs { get; set; } // Relationship: Album can have multiple songs
    }
}
