namespace WorldFacts.Library.Models;

public class AnswerData
{
    public int AnswerId { get; set; }
    public int QuestionId { get; set; }
    public string Content { get; set; } = "";
    public AnswerType AnswerType { get; set; }
    public string Comment { get; set; } = "";
}