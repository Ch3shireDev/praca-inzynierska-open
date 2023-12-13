using WorldFacts.Library.Models;

namespace WorldFacts.App.Models;

public class AdminQuestionsModel
{
    public IList<QuestionData> Questions { get; set; } = null!;
}