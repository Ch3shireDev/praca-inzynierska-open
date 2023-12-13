using System.ComponentModel;

namespace WorldFacts.Library.Models;

public enum AnswerType
{
    [Description("Poprawna")] CORRECT,
    [Description("Niepoprawna")] INCORRECT,
    [Description("Bardzo niepoprawna")] VERY_INCORRECT
}