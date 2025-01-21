using System.Diagnostics;
using GhostBot.DataContext;
using GhostBot.EntityModels;
using GhostBot.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace GhostBot.Mvc.Controllers;

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

    public async Task<IActionResult> Persons(string city)
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

    public IActionResult Index()
    {
        return View();
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
