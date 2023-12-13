using AutoMapper;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;

namespace WorldFacts.Library.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Question, QuestionData>();
        CreateMap<QuestionData, Question>();
        CreateMap<Answer, AnswerData>();
        CreateMap<AnswerData, Answer>();
        CreateMap<Narrative, Narrative>();
        CreateMap<Narrative, NarrativeData>();
        CreateMap<NarrativeData, Narrative>();
    }
}