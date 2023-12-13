using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Helpers;
using WorldFacts.Library.Models;

namespace WorldFacts.Library.Services;

public class QuestionService : IQuestionService
{
    private readonly IMapper _mapper = new MapperConfiguration(
            cfg => cfg.AddProfile<AutoMapperProfile>()
        )
        .CreateMapper();

    private readonly AppDbContext dbContext;

    public QuestionService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public QuestionData GetQuestion(int questionId)
    {
        var question = dbContext
            .Questions
            .Include(q => q.Answers)
            .FirstOrDefault(q => q.QuestionId == questionId);

        var NextQuestionId = dbContext
            .Questions
            .Where(e => e.QuestionId > questionId)
            .Select(e => (int?)e.QuestionId)
            .FirstOrDefault() ?? 0;

        var questionData = _mapper.Map<QuestionData>(question);
        questionData.NextQuestionId = NextQuestionId;

        return questionData ?? throw new Exception($"Question with id {questionId} not found");
    }

    public void CreateOrUpdateQuestion(Question question)
    {
        if (question.QuestionId == 0)
        {
            CreateQuestion(question);
        }
        else
        {
            UpdateQuestion(question);
        }

        dbContext.SaveChanges();
    }

    public IList<QuestionData> GetQuestions()
    {
        var questions = dbContext.Questions.Include(q => q.Answers).ToList();

        return questions.Select(_mapper.Map<QuestionData>).ToList();
    }

    public int GetQuestionMaxId()
    {
        return !dbContext.Questions.Any() ? 0 : dbContext.Questions.Max(q => q.QuestionId);
    }

    private void CreateQuestion(Question question)
    {
        foreach (var answer in question.Answers)
        {
            answer.AnswerId = 0;
        }

        dbContext.Questions.Add(question);
    }

    private void UpdateQuestion(Question question)
    {
        var questionToUpdate = dbContext
            .Questions
            .Include(q => q.Answers)
            .FirstOrDefault(q => q.QuestionId == question.QuestionId);

        questionToUpdate!.Content = question.Content;

        for (var i = 0; i < questionToUpdate.Answers.Count; i++)
        {
            questionToUpdate.Answers[i].Content = question.Answers[i].Content;
            questionToUpdate.Answers[i].Comment = question.Answers[i].Comment;
            questionToUpdate.Answers[i].AnswerType = question.Answers[i].AnswerType;
        }

        dbContext.Questions.Update(questionToUpdate);
    }
}