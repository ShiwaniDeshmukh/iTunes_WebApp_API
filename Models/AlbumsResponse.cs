using Newtonsoft.Json;

namespace iTunes_WebApp_API.Models
{
    public class AlbumsResponse
    {
        [JsonProperty("results")]
        public List<Albums> Results { get; set; }
    }
}
