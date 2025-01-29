using System.Diagnostics;
using GhostBot.DataContext;
using GhostBot.EntityModels;
using GhostBot.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GhostBot.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GhostBotContext _db;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(
            ILogger<HomeController> logger,
            GhostBotContext db,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _db = db;
            _clientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person p)
        {
            HttpClient client = _clientFactory.CreateClient(
            name: "GhostBot.WebApi");

            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri: "api/person", value: p);

            // Optionally, get the created customer back as JSON
            // so the user can see the assigned ID, for example.
            Person? model = await response.Content.ReadFromJsonAsync<Person>();

            if (response.IsSuccessStatusCode)
            {
                TempData["success-message"] = "Person successfully added.";
            }
            else
            {
                TempData["error-message"] = "Person was NOT added.";
            }

            // Show the full Persons list to see if it was added.
            return RedirectToAction("Person");
        }

        public async Task<IActionResult> Person(string city)
        {
            string uri;
            if (string.IsNullOrEmpty(city))
            {
                ViewData["Title"] = "All Customers WorldWide";
                uri = "api/person";
            } 
            else
            {
                ViewData["Title"] = $"Customers in {city}";
                uri = $"api/person/?city={city}";
            }

            HttpClient client = _clientFactory.CreateClient(
                name: "GhostBot.WebApi");
            
            HttpRequestMessage request = new (
                method: HttpMethod.Get, requestUri: uri);

            HttpResponseMessage response = await client.SendAsync(request);

            IEnumerable<Person>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<Person>>();

            return View(model);
        }

        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            // return View();
            _logger.LogInformation("Index method return comments and categories.");

            HomeIndexViewModel model = new
            (
                Comments: await _db.Comments!.ToListAsync(),
                Categories: await _db.Categories!.ToListAsync()
            );
            return View(model); // Pass the model to the view.
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}