using System.ComponentModel;
using WorldFacts.Library.Models;

namespace WorldFacts.Library.Helpers;

public static class EnumHelpers
{
    public static string GetDescription(this AnswerType answerType)
    {
        var fieldInfo = answerType.GetType().GetField(answerType.ToString());
        if (fieldInfo == null) return answerType.ToString();
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : answerType.ToString();
    }
}