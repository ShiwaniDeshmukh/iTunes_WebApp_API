using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using iTunes_WebApp_API.Models;

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
            // Implement the action method for displaying the iTunes store page here
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("Index");
            }

            // Implement the action method for handling search requests and retrieving search results

            // Construct the search API URL by appending the keyword to the base URL
            string apiUrl = "https://itunes.apple.com/search?term=" + keyword;

            // Use HttpClient to send an HTTP GET request to the search API
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Parse the API response and deserialize it into a SearchResult object
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonResponse);

                // Pass the search result to the view for display
                return View("SearchResult", searchResult);
            }
            else
            {
                // Handle the case when the API request fails
                return View("Error");
            }
        }
    }
}
