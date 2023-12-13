using WorldFacts.Library.Entities;

namespace WorldFacts.Library.Models;

public class NarrativeData
{
    public Narrative Narrative { get; set; } = null!;
    public PowerBIEmbedConfig? Config { get; set; }
}