namespace WorldFacts.Library.Models;

public class PollData
{
    public List<QuestionData> Questions { get; set; } = new()
    {
        new QuestionData(),
        new QuestionData(),
        new QuestionData(),
        new QuestionData()
    };
}