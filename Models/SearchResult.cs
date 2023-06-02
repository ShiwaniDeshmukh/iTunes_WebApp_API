using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace iTunes_WebApp_API.Models
{
    public class SearchResult
    {
        [JsonProperty("resultCount")]
        public int ResultCount { get; set; }

        [JsonProperty("results")]
        public List<SearchItem> Results { get; set; }

        public SearchResult()
        {
            Results = new List<SearchItem>();
        }

        public static bool ValidateJsonStructure(string json)
        {
            try
            {
                // Parse the JSON string into a JObject
                JObject jObject = JObject.Parse(json);

                // Check if the required properties exist in the JSON object
                if (jObject["resultCount"] == null || jObject["results"] == null)
                    return false;

                // Optional: Check for additional properties or nested structures if needed

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class SearchItem
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

        // Additional properties for displaying the search results
        public string Heading { get; set; }  // Heading for grouping results (e.g., Songs, Albums, etc.)

        // Additional methods for formatting and manipulating data
        public string GetFormattedTrackPrice()
        {
            if (TrackPrice.HasValue)
            {
                CultureInfo culture = new CultureInfo("en-US");
                return TrackPrice.Value.ToString("C", culture);  // Format the price as currency in dollars
            }
            else
            {
                return "N/A";
            }
        }
        public string GetFormattedCollectionPrice()
        {
            if (CollectionPrice.HasValue)
            {
                CultureInfo culture = new CultureInfo("en-US");
                return CollectionPrice.Value.ToString("C", culture);  // Format the price as currency in dollars
            }
            else
            {
                return "N/A";
            }
        }
    }
}