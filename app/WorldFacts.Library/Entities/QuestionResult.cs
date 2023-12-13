namespace WorldFacts.Library.Entities;

public class QuestionResult
{
    public int QuestionResultId { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
    public string IpAddress { get; set; } = null!;
}