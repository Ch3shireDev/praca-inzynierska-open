using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldFacts.App.Models;

namespace WorldFacts.App.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            return View();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Błąd podczas wyświetlania strony głównej.");
            return View("Error");
        }
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}