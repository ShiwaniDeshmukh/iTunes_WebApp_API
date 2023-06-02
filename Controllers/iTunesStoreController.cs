using System;
using System.Net.Http;
using System.Threading.Tasks;
using iTunes_WebApp_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace iTunes_WebApp_API.Controllers
{
    public class iTunesStoreController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public iTunesStoreController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View(); // Remove the parameter "Index" to use the default view
        }

        public IActionResult ClickCount()
        {
            int totalClicks = ClickCountTracker.GetTotalClicks();
            return View("~/Views/Songs/ClickCount.cshtml", totalClicks);
        }

        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrEmpty(term))
                return View("Index");

            // Encode the search term for the URL
            string encodedTerm = Uri.EscapeDataString(term);

            // Create the HTTP client
            HttpClient client = _httpClientFactory.CreateClient();

            // Build the API URL
            string apiUrl = $"https://itunes.apple.com/search?term={encodedTerm}";

            try
            {
                // Send the API request and retrieve the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // Read the response content
                string json = await response.Content.ReadAsStringAsync();

                // Validate the JSON structure
                if (!SearchResult.ValidateJsonStructure(json))
                {
                    ViewBag.Error = "Invalid JSON structure returned from the iTunes API.";
                    return View("Index");
                }

                // Deserialize the JSON response into a SearchResult object
                SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(json);

                searchResult.Results = searchResult.Results ?? new List<SearchItem>();

                // Generate the details URL for each search item
                foreach (var item in searchResult.Results)
                {
                    switch (item.Kind)
                    {
                        case "song":
                            item.ViewDetailsUrl = Url.Action("Details", "Songs", new { id = item.TrackId });
                            item.ClickCount = ClickCountTracker.GetClickCount(item.TrackId);
                            break;
                    }
                }

                return View("Search", searchResult);
            }
            catch (Exception ex)
            {
                // Handle any errors that occurred during the API request
                ViewBag.Error = "An error occurred while retrieving the search results.";
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }
    }
}
