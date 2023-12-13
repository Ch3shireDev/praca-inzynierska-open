using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldFacts.App.Models;
using WorldFacts.Library.Services;

namespace WorldFacts.App.Controllers;

[AllowAnonymous]
public class ReportController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INarrativeService _narrativeService;
    private readonly IPowerBiService _powerBiService;

    public ReportController(ILogger<HomeController> logger, IPowerBiService powerBiService, INarrativeService narrativeService)
    {
        _logger = logger;
        _powerBiService = powerBiService;
        _narrativeService = narrativeService;
    }

    public IActionResult Index()
    {
        var headers = new HeadersData
        {
            Headers = _narrativeService.GetNarratives()
        };
        return (View(headers));
    }

    [HttpGet("/report/{narrativeId}")]
    public async Task<IActionResult> Report(int narrativeId)
    {
        try
        {
            var config = await _powerBiService.GetEmbedConfig();
            var details = _narrativeService.GetNarrative(narrativeId);
            config.ReportPageId = details.ReportPageId;
            var reportData = new ReportData
            {
                Config = config,
                ReportName = details.ReportPageId,
                Details = details
            };
            return View(reportData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting report");
            return View("Error");
        }
    }
}