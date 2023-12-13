using WorldFacts.Library.Models;

namespace WorldFacts.App.Models;

public class PostAnswerData
{
    public QuestionData? Question { get; set; }
    public AnswerData? Answer { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}
