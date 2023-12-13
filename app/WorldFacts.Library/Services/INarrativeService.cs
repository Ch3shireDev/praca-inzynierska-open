using WorldFacts.Library.Entities;

namespace WorldFacts.Library.Services;

public interface INarrativeService
{
    IList<Narrative> GetNarratives();
    Narrative GetNarrative(int narrativeId);
    void CreateOrUpdateNarrative(Narrative narrative);
    void DeleteNarrative(int narrativeId);
}