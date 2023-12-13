using WorldFacts.Library.Entities;

namespace WorldFacts.App.Models;

public class AdminNarrativesModel
{
    public IList<Narrative> Narratives { get; set; } = null!;
}