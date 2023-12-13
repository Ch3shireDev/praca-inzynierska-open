namespace WorldFacts.Library.Entities;

public class Question
{
    public int QuestionId { get; set; }
    public string Content { get; set; } = "";
    public List<Answer> Answers { get; set; } = new();
}