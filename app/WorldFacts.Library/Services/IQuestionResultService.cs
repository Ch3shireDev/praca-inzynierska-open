using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;

namespace WorldFacts.Library.Services;

public interface IQuestionResultService
{
    void Create(QuestionResult result);
    QuestionsResultsStatistics GetStatistics();
}