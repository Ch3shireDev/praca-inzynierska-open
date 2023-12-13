using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldFacts.App.Models;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;
using WorldFacts.Library.Services;

namespace WorldFacts.App.Controllers;

[Authorize]
public class QuestionsAdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IQuestionService _questionService;

    public QuestionsAdminController(ILogger<AdminController> logger, IQuestionService questionService)
    {
        _logger = logger;
        _questionService = questionService;
    }

    public IActionResult ShowQuestions()
    {
        try
        {
            var questions = _questionService.GetQuestions();

            var model = new AdminQuestionsModel
            {
                Questions = questions
            };

            return View(model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting questions");
            return View("Error");
        }
    }


    [HttpPost]
    public IActionResult UpdateQuestion(Question question)
    {
        try
        {
            _questionService.CreateOrUpdateQuestion(question);
            return RedirectToAction(nameof(ShowQuestions));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating question");
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult CreateQuestion()
    {
        var newQuestion = new QuestionData
        {
            Content = "Przykładowe pytanie",
            Answers = new List<AnswerData>
            {
                new AnswerData
                {
                    Content = "Przykładowa odpowiedź 1",
                    AnswerType = AnswerType.CORRECT,
                    Comment = "Przykładowy komentarz 1"
                },
                new AnswerData
                {
                    Content = "Przykładowa odpowiedź 2",
                    AnswerType = AnswerType.INCORRECT,
                    Comment = "Przykładowy komentarz 2"
                },
                new AnswerData
                {
                    Content = "Przykładowa odpowiedź 3",
                    AnswerType = AnswerType.VERY_INCORRECT,
                    Comment = "Przykładowy komentarz 3"
                }
            }
        };
        return View(newQuestion);
    }
}