using Microsoft.EntityFrameworkCore;
using WorldFacts.Library.Entities;

namespace WorldFacts.Library;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Question> Questions { get; set; } = null!;
    public virtual DbSet<Answer> Answers { get; set; } = null!;
    public virtual DbSet<Narrative> Narratives { get; set; } = null!;
    public virtual DbSet<QuestionResult> QuestionResults { get; set; } = null!;
}