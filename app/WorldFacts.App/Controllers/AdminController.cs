using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorldFacts.App.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
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
            _logger.LogError(e, "Error while getting questions");
            return View("Error");
        }
    }
}