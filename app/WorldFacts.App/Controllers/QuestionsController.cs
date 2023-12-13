using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldFacts.App.Models;
using WorldFacts.Library.Models;
using WorldFacts.Library.Services;

namespace WorldFacts.App.Controllers;

[AllowAnonymous]
[Route("questions")]
public class QuestionsController : Controller
{
    private readonly ILogger<QuestionsController> _logger;
    private readonly IQuestionResultService _questionResultService;
    private readonly IQuestionService _questionService;

    public QuestionsController(ILogger<QuestionsController> logger, IQuestionService questionService, IQuestionResultService questionResultService)
    {
        _logger = logger;
        _questionService = questionService;
        _questionResultService = questionResultService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            return View();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Błąd podczas pobierania strony głównej.");
            return View("Error");
        }
    }

    [HttpGet("first")]
    public IActionResult FirstQuestion()
    {
        try
        {
            var questions = _questionService.GetQuestions();
            var question = questions.FirstOrDefault();
            if (question == null) throw new Exception("Błąd pobierania pytań.");
            if (question.Answers.Count == 0) throw new Exception("Brak pytań w bazie.");
            var questionId = question.QuestionId;
            return RedirectToAction("Show", new { questionId });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Błąd podczas pobierania pytań.");
            return View("Error");
        }
    }

    [HttpGet("{questionId}")]
    public IActionResult Show(int questionId)
    {
        try
        {
            if (questionId == 0) throw new Exception("Wysłany identyfikator jest nieprawidłowy.");
            var question = _questionService.GetQuestion(questionId);
            if (question == null) throw new Exception($"Brak zapytania o QuestionId: {questionId} w bazie.");
            return View(question);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Błąd pobierania pytań.");
            return View("Error");
        }
    }


    [HttpGet("end")]
    public IActionResult End()
    {
        var cookies = Request.Cookies.ToImmutableDictionary();

        var questions = _questionService.GetQuestions();

        var answerData = questions.Select(question =>
        {
            var answerId = GetAnswerId(cookies, question);
            var answer = question.Answers.FirstOrDefault(a => a.AnswerId == answerId);

            return new PostAnswerData
            {
                Question = question,
                QuestionId = question.QuestionId,
                AnswerId = answerId,
                Answer = answer
            };
        }).ToList();

        var statistics = _questionResultService.GetStatistics();

        var model = new AnswerDataModel
        {
            AnswerData = answerData,
            Statistics = statistics,
            TotalQuestions = questions.Count,
            CorrectAnswers = answerData.Count(a => a.Answer?.AnswerType == AnswerType.CORRECT)
        };

        try
        {
            return View(model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting end");
            return View("Error");
        }
    }

    private static int GetAnswerId(ImmutableDictionary<string, string> cookies, QuestionData q)
    {
        var key = $"question-{q.QuestionId}";
        if (!cookies.ContainsKey(key)) return 0;
        var answerStr = cookies[key];
        return int.TryParse(answerStr, out var answerId) ? answerId : 0;
    }
}