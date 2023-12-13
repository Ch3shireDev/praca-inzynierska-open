using Microsoft.EntityFrameworkCore;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;

namespace WorldFacts.Library.Services;

public class QuestionResultService : IQuestionResultService
{
    private readonly AppDbContext _context;

    public QuestionResultService(AppDbContext context)
    {
        _context = context;
    }

    public void Create(QuestionResult result)
    {
        _context.QuestionResults.Add(result);
        _context.SaveChanges();
    }

    public QuestionsResultsStatistics GetStatistics()
    {
        var answersInfo = GetAnswersInfo();

        var statistics = new QuestionsResultsStatistics
        {
            Questions = _context.Questions.Include(q => q.Answers).ToList(),
            QuestionsAnswersInfo = answersInfo.ToArray()
        };


        return statistics;
    }

    private IEnumerable<QuestionsAnswersInfo> GetAnswersInfo()
    {
        foreach (var answer in _context.Answers.ToArray())
        {
            var answerId = answer.AnswerId;
            var questionId = answer.QuestionId;
            var answerCount = _context.QuestionResults.Count(qr => qr.AnswerId == answerId);
            var questionCount = _context.QuestionResults.Count(qr => qr.QuestionId == questionId);

            yield return new QuestionsAnswersInfo
            {
                QuestionId = questionId,
                AnswerId = answerId,
                AnswerCount = answerCount,
                QuestionCount = questionCount
            };
        }
    }
}