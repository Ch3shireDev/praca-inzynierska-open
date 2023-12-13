using WorldFacts.Library.Models;

namespace WorldFacts.App.Models;

public class AnswerDataModel
{
    public IList<PostAnswerData> AnswerData { get; set; } = Array.Empty<PostAnswerData>();
    public QuestionsResultsStatistics Statistics { get; set; } = null!;
    public int CorrectAnswers{ get; set; }
    public int TotalQuestions{ get; set; }
}