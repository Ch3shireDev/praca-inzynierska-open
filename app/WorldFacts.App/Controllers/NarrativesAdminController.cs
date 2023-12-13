using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldFacts.App.Models;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;
using WorldFacts.Library.Services;

namespace WorldFacts.App.Controllers;

[Authorize]
public class NarrativesAdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly INarrativeService _narrativeService;
    private readonly IPowerBiService _powerBiService;

    public NarrativesAdminController(
        ILogger<AdminController> logger,
        INarrativeService narrativeService,
        IPowerBiService powerBiService)
    {
        _logger = logger;
        _narrativeService = narrativeService;
        _powerBiService = powerBiService;
    }

    public IActionResult ShowNarratives()
    {
        try
        {
            var narratives = _narrativeService.GetNarratives();

            var model = new AdminNarrativesModel
            {
                Narratives = narratives
            };

            return View(model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting narratives");
            return View("Error");
        }
    }

    [HttpGet]
    [Route("/admin/edit-narrative/{narrativeId}")]
    public async Task<IActionResult> ShowUpdateNarrative(int narrativeId)
    {
        try
        {
            var narrative = _narrativeService.GetNarrative(narrativeId);
            var config = await _powerBiService.GetEmbedConfig();
            config.ReportPageId = narrative.ReportPageId;

            var narrativeData = new NarrativeData
            {
                Narrative = narrative,
                Config = config
            };
            return View(narrativeData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting narrative");
            return View("Error");
        }
    }

    [HttpGet]
    [Route("/admin/narratives/create-narrative")]
    public async Task<IActionResult> CreateNarrative()
    {
        var config = await _powerBiService.GetEmbedConfig();
        return View(new NarrativeData
        {
            Narrative = new Narrative(),
            Config = config
        });
    }

    [HttpPost]
    public IActionResult UpdateNarrative(Narrative narrative)
    {
        _narrativeService.CreateOrUpdateNarrative(narrative);

        return RedirectToAction(nameof(ShowNarratives));
    }

    [HttpDelete]
    [Route("/admin/delete-narrative/{narrativeId}")]
    public IActionResult DeleteNarrative(int narrativeId)
    {
        _narrativeService.DeleteNarrative(narrativeId);
        return RedirectToAction(nameof(ShowNarratives));
    }
}