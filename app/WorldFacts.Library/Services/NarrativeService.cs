using AutoMapper;
using WorldFacts.Library.Entities;
using WorldFacts.Library.Helpers;

namespace WorldFacts.Library.Services;

public class NarrativeService : INarrativeService
{
    private readonly AppDbContext _context;

    private readonly IMapper _mapper = new MapperConfiguration(exp => exp.AddProfile<AutoMapperProfile>()).CreateMapper();

    public NarrativeService(AppDbContext context)
    {
        _context = context;
    }

    public IList<Narrative> GetNarratives()
    {
        return _context.Narratives.ToList();
    }

    public Narrative GetNarrative(int narrativeId)
    {
        var narrative = _context.Narratives.FirstOrDefault(x => x.NarrativeId == narrativeId);
        return narrative ?? throw new KeyNotFoundException("Nie znaleziono klucza o id " + narrativeId);
    }

    public void CreateOrUpdateNarrative(Narrative narrative)
    {
        if (narrative.NarrativeId == 0)
        {
            _context.Narratives.Add(narrative);
        }
        else
        {
            var existingNarrative = _context.Narratives.FirstOrDefault(x => x.NarrativeId == narrative.NarrativeId);

            if (existingNarrative == null)
            {
                throw new KeyNotFoundException($"Nie znaleziono klucza o id {narrative.NarrativeId}");
            }

            _mapper.Map(narrative, existingNarrative);

            _context.Narratives.Update(existingNarrative);
        }

        _context.SaveChanges();
    }

    public void DeleteNarrative(int narrativeId)
    {
        var narrative = _context.Narratives.FirstOrDefault(x => x.NarrativeId == narrativeId);

        if (narrative == null)
        {
            throw new KeyNotFoundException($"Nie znaleziono klucza o id {narrativeId}");
        }

        _context.Narratives.Remove(narrative);
        _context.SaveChanges();
    }
}