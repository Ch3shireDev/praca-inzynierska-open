using Microsoft.EntityFrameworkCore;
using Moq;
using WorldFacts.Library;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Services;

namespace WorldFacts.Tests;

public class NarrativeServiceTests
{
    private AppDbContext dbContext = null!;
    private DbContextOptions<AppDbContext> options = null!;
    private NarrativeService service = null!;

    [SetUp]
    public void SetUp()
    {
        options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("test")
            .Options;

        dbContext = new AppDbContext(options);

        service = new NarrativeService(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Database.EnsureDeleted();
    }

    [Test]
    public void GetHeaders()
    {
        var questions = GetNarratives(9);

        var dbContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        dbContextMock.Setup(m => m.Narratives).Returns(DbHelpers.CreateDbSetMock(questions));

        var mockDbContext = dbContextMock.Object;
        service = new NarrativeService(mockDbContext);

        var headers = service.GetNarratives();

        Assert.That(headers, Is.Not.Null);
        Assert.That(headers.Count, Is.EqualTo(9));
    }

    [Test]
    public void DeleteNarrativeTest()
    {
        dbContext.Narratives.Add(new()
        {
            NarrativeId = 1,
            ReportPageId = "aaa",
            Title = "Report nr 1",
            Description = "Description nr 1"
        });

        dbContext.Narratives.Add(new()
        {
            NarrativeId = 2,
            ReportPageId = "bbb",
            Title = "Report nr 2",
            Description = "Description nr 2"
        });

        dbContext.SaveChanges();

        service.DeleteNarrative(1);

        Assert.That(dbContext.Narratives.Count(), Is.EqualTo(1));
        var narrative = dbContext.Narratives.First();
        Assert.That(narrative.NarrativeId, Is.EqualTo(2));
    }

    [Test]
    public void CreateNarrativeTest()
    {
        service.CreateOrUpdateNarrative(new()
        {
            ReportPageId = "aaa",
            Title = "Report nr 1",
            Description = "Description nr 1"
        });

        Assert.That(dbContext.Narratives.Count(), Is.EqualTo(1));
        var narrative = dbContext.Narratives.First();
        Assert.That(narrative.NarrativeId, Is.EqualTo(1));
        Assert.That(narrative.ReportPageId, Is.EqualTo("aaa"));
        Assert.That(narrative.Title, Is.EqualTo("Report nr 1"));
        Assert.That(narrative.Description, Is.EqualTo("Description nr 1"));
    }

    [Test]
    public void UpdateNarrativeTest()
    {
        dbContext.Narratives.Add(new()
        {
            NarrativeId = 1,
            ReportPageId = "aaa",
            Title = "Report nr 1",
            Description = "Description nr 1"
        });
        dbContext.SaveChanges();

        service.CreateOrUpdateNarrative(new()
        {
            NarrativeId = 1,
            ReportPageId = "bbb",
            Title = "Report nr 2",
            Description = "Description nr 2"
        });

        Assert.That(dbContext.Narratives.Count(), Is.EqualTo(1));
        var narrative = dbContext.Narratives.First();
        Assert.That(narrative.NarrativeId, Is.EqualTo(1));
        Assert.That(narrative.ReportPageId, Is.EqualTo("bbb"));
        Assert.That(narrative.Title, Is.EqualTo("Report nr 2"));
        Assert.That(narrative.Description, Is.EqualTo("Description nr 2"));
    }

    private static List<Narrative> GetNarratives(int numNarratives)
    {
        var questions = Enumerable
            .Range(1, numNarratives)
            .Select(narrativeId => new Narrative
            {
                NarrativeId = narrativeId,
                ReportPageId = $"report_page_{narrativeId}",
                Title = $"Report Page {narrativeId}",
                Description = $"Description {narrativeId}"
            }).ToList();
        return questions;
    }

}