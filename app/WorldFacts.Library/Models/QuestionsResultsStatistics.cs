using WorldFacts.Library.Entities;

namespace WorldFacts.Library.Models;

public class QuestionsResultsStatistics
{
    public IList<Question> Questions { get; set; } = Array.Empty<Question>();
    public IList<QuestionsAnswersInfo> QuestionsAnswersInfo { get; set; } = Array.Empty<QuestionsAnswersInfo>();
}