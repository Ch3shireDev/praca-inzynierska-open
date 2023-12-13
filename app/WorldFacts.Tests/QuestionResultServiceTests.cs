using Microsoft.EntityFrameworkCore;
using WorldFacts.Library;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Services;

namespace WorldFacts.Tests;

public class QuestionResultServiceTests
{
    private AppDbContext _context = null!;
    private QuestionResultService _service = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        _context = new AppDbContext(options);

        _service = new QuestionResultService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
    }

    [Test]
    public void CreateTest()
    {
        var result = new QuestionResult
        {
            QuestionId = 3,
            AnswerId = 5,
            IpAddress = "127.0.0.1"
        };

        _service.Create(result);

        var result2 = _context.QuestionResults.FirstOrDefault();

        Assert.That(result2, Is.Not.Null);
        Assert.That(result2.QuestionId, Is.EqualTo(3));
        Assert.That(result2.AnswerId, Is.EqualTo(5));
        Assert.That(result2.IpAddress, Is.EqualTo("127.0.0.1"));
    }

    [Test]
    public void GetStatisticsTest()
    {
        _context.Questions.Add(new Question
        {
            QuestionId = 1,
            Content = "pytanie 1",
            Answers = new List<Answer>
            {
                new Answer
                {
                    AnswerId = 1,
                    Content = "odpowiedź 1"
                },
                new Answer
                {
                    AnswerId = 2,
                    Content = "odpowiedź 2"
                },
                new Answer
                {
                    AnswerId = 3,
                    Content = "odpowiedź 3"
                }
            }
        });

        _context.Questions.Add(new Question
        {
            QuestionId = 2,
            Content = "pytanie 2",
            Answers = new List<Answer>
            {
                new Answer
                {
                    AnswerId = 4,
                    Content = "odpowiedź 4"
                },
                new Answer
                {
                    AnswerId = 5,
                    Content = "odpowiedź 5"
                },
                new Answer
                {
                    AnswerId = 6,
                    Content = "odpowiedź 6"
                }
            }
        });

        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 1, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 1, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 1, IpAddress = "" });

        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 2, IpAddress = "" });

        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 3, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 3, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 3, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 1, AnswerId = 3, IpAddress = "" });

        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 4, IpAddress = "" });

        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });
        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 5, IpAddress = "" });

        _context.QuestionResults.Add(new QuestionResult { QuestionId = 2, AnswerId = 6, IpAddress = "" });

        _context.SaveChanges();

        var statistics = _service.GetStatistics();

        Assert.That(statistics, Is.Not.Null);
        Assert.That(statistics.Questions, Is.Not.Null);
        Assert.That(statistics.Questions.Count, Is.EqualTo(2));

        Assert.That(statistics.Questions[0].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.Questions[0].Content, Is.EqualTo("pytanie 1"));
        Assert.That(statistics.Questions[0].Answers.Count, Is.EqualTo(3));
        Assert.That(statistics.Questions[0].Answers[0].AnswerId, Is.EqualTo(1));
        Assert.That(statistics.Questions[0].Answers[0].Content, Is.EqualTo("odpowiedź 1"));
        Assert.That(statistics.Questions[0].Answers[1].AnswerId, Is.EqualTo(2));
        Assert.That(statistics.Questions[0].Answers[1].Content, Is.EqualTo("odpowiedź 2"));
        Assert.That(statistics.Questions[0].Answers[2].AnswerId, Is.EqualTo(3));
        Assert.That(statistics.Questions[0].Answers[2].Content, Is.EqualTo("odpowiedź 3"));

        Assert.That(statistics.Questions[1].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.Questions[1].Content, Is.EqualTo("pytanie 2"));
        Assert.That(statistics.Questions[1].Answers.Count, Is.EqualTo(3));
        Assert.That(statistics.Questions[1].Answers[0].AnswerId, Is.EqualTo(4));
        Assert.That(statistics.Questions[1].Answers[0].Content, Is.EqualTo("odpowiedź 4"));
        Assert.That(statistics.Questions[1].Answers[1].AnswerId, Is.EqualTo(5));
        Assert.That(statistics.Questions[1].Answers[1].Content, Is.EqualTo("odpowiedź 5"));
        Assert.That(statistics.Questions[1].Answers[2].AnswerId, Is.EqualTo(6));
        Assert.That(statistics.Questions[1].Answers[2].Content, Is.EqualTo("odpowiedź 6"));

        Assert.That(statistics.QuestionsAnswersInfo, Is.Not.Null);
        Assert.That(statistics.QuestionsAnswersInfo.Count, Is.EqualTo(6));

        Assert.That(statistics.QuestionsAnswersInfo[0].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[0].AnswerId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[0].AnswerCount, Is.EqualTo(3));
        Assert.That(statistics.QuestionsAnswersInfo[0].QuestionCount, Is.EqualTo(17));

        Assert.That(statistics.QuestionsAnswersInfo[1].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[1].AnswerId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[1].AnswerCount, Is.EqualTo(10));
        Assert.That(statistics.QuestionsAnswersInfo[1].QuestionCount, Is.EqualTo(17));

        Assert.That(statistics.QuestionsAnswersInfo[2].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[2].AnswerId, Is.EqualTo(3));
        Assert.That(statistics.QuestionsAnswersInfo[2].AnswerCount, Is.EqualTo(4));
        Assert.That(statistics.QuestionsAnswersInfo[2].QuestionCount, Is.EqualTo(17));

        Assert.That(statistics.QuestionsAnswersInfo[3].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[3].AnswerId, Is.EqualTo(4));
        Assert.That(statistics.QuestionsAnswersInfo[3].AnswerCount, Is.EqualTo(12));
        Assert.That(statistics.QuestionsAnswersInfo[3].QuestionCount, Is.EqualTo(22));

        Assert.That(statistics.QuestionsAnswersInfo[4].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[4].AnswerId, Is.EqualTo(5));
        Assert.That(statistics.QuestionsAnswersInfo[4].AnswerCount, Is.EqualTo(9));
        Assert.That(statistics.QuestionsAnswersInfo[4].QuestionCount, Is.EqualTo(22));

        Assert.That(statistics.QuestionsAnswersInfo[5].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[5].AnswerId, Is.EqualTo(6));
        Assert.That(statistics.QuestionsAnswersInfo[5].AnswerCount, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[5].QuestionCount, Is.EqualTo(22));
    }

    [Test]
    public void StatisticsShouldBeCreatedEvenIfNoElementsInDatabase()
    {
        _context.Questions.Add(new Question
        {
            QuestionId = 1,
            Content = "pytanie 1",
            Answers = new List<Answer>
            {
                new Answer
                {
                    AnswerId = 1,
                    Content = "odpowiedź 1"
                },
                new Answer
                {
                    AnswerId = 2,
                    Content = "odpowiedź 2"
                },
                new Answer
                {
                    AnswerId = 3,
                    Content = "odpowiedź 3"
                }
            }
        });

        _context.Questions.Add(new Question
        {
            QuestionId = 2,
            Content = "pytanie 2",
            Answers = new List<Answer>
            {
                new Answer
                {
                    AnswerId = 4,
                    Content = "odpowiedź 4"
                },
                new Answer
                {
                    AnswerId = 5,
                    Content = "odpowiedź 5"
                },
                new Answer
                {
                    AnswerId = 6,
                    Content = "odpowiedź 6"
                }
            }
        });

        _context.SaveChanges();

        var statistics = _service.GetStatistics();

        Assert.That(statistics, Is.Not.Null);
        Assert.That(statistics.Questions, Is.Not.Null);
        Assert.That(statistics.Questions.Count, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo.Count, Is.EqualTo(6));

        Assert.That(statistics.QuestionsAnswersInfo[0].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[0].AnswerId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[0].AnswerCount, Is.EqualTo(0));
        Assert.That(statistics.QuestionsAnswersInfo[0].QuestionCount, Is.EqualTo(0));

        Assert.That(statistics.QuestionsAnswersInfo[1].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[1].AnswerId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[1].AnswerCount, Is.EqualTo(0));
        Assert.That(statistics.QuestionsAnswersInfo[1].QuestionCount, Is.EqualTo(0));

        Assert.That(statistics.QuestionsAnswersInfo[2].QuestionId, Is.EqualTo(1));
        Assert.That(statistics.QuestionsAnswersInfo[2].AnswerId, Is.EqualTo(3));
        Assert.That(statistics.QuestionsAnswersInfo[2].AnswerCount, Is.EqualTo(0));
        Assert.That(statistics.QuestionsAnswersInfo[2].QuestionCount, Is.EqualTo(0));

        Assert.That(statistics.QuestionsAnswersInfo[3].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[3].AnswerId, Is.EqualTo(4));
        Assert.That(statistics.QuestionsAnswersInfo[3].AnswerCount, Is.EqualTo(0));
        Assert.That(statistics.QuestionsAnswersInfo[3].QuestionCount, Is.EqualTo(0));

        Assert.That(statistics.QuestionsAnswersInfo[4].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[4].AnswerId, Is.EqualTo(5));
        Assert.That(statistics.QuestionsAnswersInfo[4].AnswerCount, Is.EqualTo(0));
        Assert.That(statistics.QuestionsAnswersInfo[4].QuestionCount, Is.EqualTo(0));

        Assert.That(statistics.QuestionsAnswersInfo[5].QuestionId, Is.EqualTo(2));
        Assert.That(statistics.QuestionsAnswersInfo[5].AnswerId, Is.EqualTo(6));
        Assert.That(statistics.QuestionsAnswersInfo[5].AnswerCount, Is.EqualTo(0));
        Assert.That(statistics.QuestionsAnswersInfo[5].QuestionCount, Is.EqualTo(0));
    }
}