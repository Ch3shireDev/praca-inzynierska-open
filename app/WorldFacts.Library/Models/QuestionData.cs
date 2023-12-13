namespace WorldFacts.Library.Models;

public class QuestionData
{
    public int QuestionId { get; set; }
    public string Content { get; set; } = "";
    public List<AnswerData> Answers { get; set; } = new();
    public int NextQuestionId { get; set; }
}