using WorldFacts.Library.Models;

namespace WorldFacts.Library.Entities;

public class Answer
{
    public int AnswerId { get; set; }
    public int QuestionId { get; set; }
    public string Content { get; set; } = "";
    public AnswerType AnswerType { get; set; }
    public string Comment { get; set; } = "";
}