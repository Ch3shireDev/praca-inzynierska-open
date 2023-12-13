using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using WorldFacts.Library;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Helpers;
using WorldFacts.Library.Models;
using WorldFacts.Library.Services;

namespace WorldFacts.Tests;

public class PollServiceTests
{
    private AppDbContext dbContext = null!;
    private Mock<AppDbContext> dbContextMock = null!;
    private QuestionService service = null!;

    [SetUp]
    public void Setup()
    {
        dbContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        service = new QuestionService(dbContextMock.Object);
    }

    [Test]
    public void GetPollTest()
    {
        var questions = GetQuestions(3, 3);

        dbContextMock.Setup(m => m.Questions).Returns(DbHelpers.CreateDbSetMock(questions));

        var pollQuestions = service.GetQuestions();

        Assert.That(pollQuestions, Is.Not.Null);
        Assert.That(pollQuestions.Count, Is.EqualTo(3));
        Assert.That(pollQuestions[0].Answers.Count, Is.EqualTo(3));
        Assert.That(pollQuestions[1].Answers.Count, Is.EqualTo(3));
        Assert.That(pollQuestions[2].Answers.Count, Is.EqualTo(3));
    }

    [Test]
    public void GetQuestionTest()
    {
        var questions = GetQuestions(3, 3);

        dbContextMock.Setup(m => m.Questions).Returns(DbHelpers.CreateDbSetMock(questions));

        var question = service.GetQuestion(2);

        Assert.That(question, Is.Not.Null);
        Assert.That(question.QuestionId, Is.EqualTo(2));
        Assert.That(question.Content, Is.EqualTo("Question 2"));
        Assert.That(question.Answers, Is.Not.Null);
        Assert.That(question.Answers.Count, Is.EqualTo(3));
        Assert.That(question.Answers[0].AnswerId, Is.EqualTo(4));
        Assert.That(question.Answers[0].Content, Is.EqualTo("Answer 4 for question 2"));
        Assert.That(question.Answers[1].AnswerId, Is.EqualTo(5));
        Assert.That(question.Answers[1].Content, Is.EqualTo("Answer 5 for question 2"));
        Assert.That(question.Answers[2].AnswerId, Is.EqualTo(6));
        Assert.That(question.Answers[2].Content, Is.EqualTo("Answer 6 for question 2"));
    }

    [Test]
    public void UpdateQuestionTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("UpdateQuestionTest")
            .Options;

        dbContext = new AppDbContext(options);

        dbContext.Questions.Add(new Question
        {
            QuestionId = 1,
            Content = "Question 1",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Content = "Answer 1",
                    Comment = "Comment 1",
                    AnswerType = AnswerType.CORRECT
                },
                new Answer
                {
                    Content = "Answer 2",
                    Comment = "Comment 2",
                    AnswerType = AnswerType.INCORRECT
                },
                new Answer
                {
                    Content = "Answer 3",
                    Comment = "Comment 3",
                    AnswerType = AnswerType.VERY_INCORRECT
                }
            }
        });

        dbContext.SaveChanges();

        service = new QuestionService(dbContext);

        var questionData = service.GetQuestion(1);

        var map = new MapperConfiguration(conf => conf.AddProfile<AutoMapperProfile>()).CreateMapper();

        var question = map.Map<Question>(questionData);

        question.Answers[0].Comment = "Updated comment 1";
        question.Answers[0].AnswerType = AnswerType.INCORRECT;
        question.Answers[1].Comment = "Updated comment 2";
        question.Answers[1].AnswerType = AnswerType.VERY_INCORRECT;
        question.Answers[2].Comment = "Updated comment 3";
        question.Answers[2].AnswerType = AnswerType.CORRECT;

        service.CreateOrUpdateQuestion(question);

        var answers = dbContext.Answers.ToArray();

        Assert.That(dbContext.Questions.Count(), Is.EqualTo(1));
        Assert.That(dbContext.Answers.Count(), Is.EqualTo(3));
        Assert.That(answers[0].Comment, Is.EqualTo("Updated comment 1"));
        Assert.That(answers[0].AnswerType, Is.EqualTo(AnswerType.INCORRECT));
        Assert.That(answers[1].Comment, Is.EqualTo("Updated comment 2"));
        Assert.That(answers[1].AnswerType, Is.EqualTo(AnswerType.VERY_INCORRECT));
        Assert.That(answers[2].Comment, Is.EqualTo("Updated comment 3"));
        Assert.That(answers[2].AnswerType, Is.EqualTo(AnswerType.CORRECT));
    }

    [Test]
    public void GetMaxIdWhenThereAreNoQuestions()
    {
        dbContextMock
            .Setup(m => m.Questions)
            .Returns(DbHelpers.CreateDbSetMock(Array.Empty<Question>()));

        var maxId = service.GetQuestionMaxId();

        Assert.That(maxId, Is.EqualTo(0));
    }

    [Test]
    public void GetQuestionsMaxId()
    {
        dbContextMock
            .Setup(m => m.Questions)
            .Returns(DbHelpers.CreateDbSetMock(new[]
            {
                new Question { QuestionId = 7 },
                new Question { QuestionId = 5 }
            }));

        var maxId = service.GetQuestionMaxId();

        Assert.That(maxId, Is.EqualTo(7));
    }

    [Test]
    public void CreateOrUpdateShouldCreateNewQuestionWithNewAnswersWhenQuestionIdIsZero()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        var dbContext = new AppDbContext(options);

        dbContext.Questions.Add(new Question
        {
            QuestionId = 5,
            Content = "Question 5",
            Answers = new List<Answer>
            {
                new Answer
                {
                    AnswerId = 3,
                    Content = "Answer 1",
                    Comment = "Comment 1",
                    AnswerType = AnswerType.CORRECT
                },
                new Answer
                {
                    AnswerId = 5,
                    Content = "Answer 2",
                    Comment = "Comment 2",
                    AnswerType = AnswerType.INCORRECT
                },
                new Answer
                {
                    AnswerId = 7,
                    Content = "Answer 3",
                    Comment = "Comment 3",
                    AnswerType = AnswerType.VERY_INCORRECT
                }
            }
        });

        dbContext.SaveChanges();

        service = new QuestionService(dbContext);

        var newQuestion = new Question
        {
            Content = "new question",
            Answers = new List<Answer>
            {
                new Answer
                {
                    Content = "new answer 1",
                    Comment = "new comment 1",
                    AnswerType = AnswerType.CORRECT
                },
                new Answer
                {
                    Content = "new answer 2",
                    Comment = "new comment 2",
                    AnswerType = AnswerType.INCORRECT
                },
                new Answer
                {
                    Content = "new answer 3",
                    Comment = "new comment 3",
                    AnswerType = AnswerType.VERY_INCORRECT
                }
            }
        };

        service.CreateOrUpdateQuestion(newQuestion);

        var questions = dbContext.Questions.ToArray();
        var lastQuestion = questions.Last();
        var answers = dbContext.Answers.OrderBy(a => a.AnswerId).ToArray();

        Assert.That(questions.Length, Is.EqualTo(2));
        Assert.That(lastQuestion.QuestionId, Is.EqualTo(6));
        Assert.That(lastQuestion.Content, Is.EqualTo("new question"));
        Assert.That(answers.Length, Is.EqualTo(6));
        Assert.That(answers[3].AnswerId, Is.EqualTo(8));
        Assert.That(answers[3].QuestionId, Is.EqualTo(6));
        Assert.That(answers[3].Content, Is.EqualTo("new answer 1"));
        Assert.That(answers[3].Comment, Is.EqualTo("new comment 1"));
        Assert.That(answers[3].AnswerType, Is.EqualTo(AnswerType.CORRECT));
        Assert.That(answers[4].AnswerId, Is.EqualTo(9));
        Assert.That(answers[4].QuestionId, Is.EqualTo(6));
        Assert.That(answers[4].Content, Is.EqualTo("new answer 2"));
        Assert.That(answers[4].Comment, Is.EqualTo("new comment 2"));
        Assert.That(answers[4].AnswerType, Is.EqualTo(AnswerType.INCORRECT));
        Assert.That(answers[5].AnswerId, Is.EqualTo(10));
        Assert.That(answers[5].QuestionId, Is.EqualTo(6));
        Assert.That(answers[5].Content, Is.EqualTo("new answer 3"));
        Assert.That(answers[5].Comment, Is.EqualTo("new comment 3"));
    }

    [Test]
    public void NextQuestionIdShouldBeShown()
    {
        dbContextMock.Setup(m => m.Questions).Returns(DbHelpers.CreateDbSetMock(new[]
        {
            new Question { QuestionId = 5 },
            new Question { QuestionId = 7 },
            new Question { QuestionId = 12 }
        }));

        var question1 = service.GetQuestion(5);
        var question2 = service.GetQuestion(7);
        var question3 = service.GetQuestion(12);

        Assert.That(question1.NextQuestionId, Is.EqualTo(7));
        Assert.That(question2.NextQuestionId, Is.EqualTo(12));
        Assert.That(question3.NextQuestionId, Is.EqualTo(0));
    }

    private static List<Question> GetQuestions(int numQuestions, int numAnswersPerQuestion)
    {
        var questions = Enumerable.Range(1, numQuestions)
            .Select(questionId => new Question
            {
                QuestionId = questionId,
                Content = $"Question {questionId}",
                Answers = Enumerable.Range((questionId - 1) * numQuestions + 1, numAnswersPerQuestion)
                    .Select(answerId => new Answer
                    {
                        AnswerId = answerId,
                        QuestionId = questionId,
                        Content = $"Answer {answerId} for question {questionId}"
                    })
                    .ToList()
            }).ToList();
        return questions;
    }
}