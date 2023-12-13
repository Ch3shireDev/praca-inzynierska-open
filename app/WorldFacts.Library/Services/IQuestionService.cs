using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;

namespace WorldFacts.Library.Services;

public interface IQuestionService
{
    IList<QuestionData> GetQuestions();
    QuestionData GetQuestion(int questionId);
    void CreateOrUpdateQuestion(Question question);
}