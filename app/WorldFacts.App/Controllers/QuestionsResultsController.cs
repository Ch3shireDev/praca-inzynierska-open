using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldFacts.App.Models;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Services;

namespace WorldFacts.App.Controllers;

[Route("questions-results")]
[AllowAnonymous]
public class QuestionsResultsController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IQuestionResultService _questionResultService;

    public QuestionsResultsController(ILogger<AdminController> logger, IQuestionResultService questionResultService)
    {
        _logger = logger;
        _questionResultService = questionResultService;
    }

    public IActionResult Index()
    {
        var statistics = _questionResultService.GetStatistics();
        return View(statistics);
    }

    [HttpPost]
    public IActionResult PostAnswer(PostAnswerData answerData2)
    {
        var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<PostAnswerData, QuestionResult>(); });

        var mapper = configuration.CreateMapper();

        var resultInfo = mapper.Map<QuestionResult>(answerData2);

        resultInfo.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";

        _questionResultService.Create(resultInfo);

        return Ok();
    }
}