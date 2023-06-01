using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTunes_WebApp_API.Models
{
    public class ClickCount
    {
        [Key]
        public int Id { get; set; }

        public int TrackId { get; set; }

        public int Count { get; set; }

        [ForeignKey("TrackId")]
        public virtual Songs Track { get; set; }
    }

}
