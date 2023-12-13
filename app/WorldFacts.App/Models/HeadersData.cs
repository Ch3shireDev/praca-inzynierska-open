using WorldFacts.Library.Entities;

namespace WorldFacts.App.Models;

public class HeadersData
{
    public IList<Narrative> Headers { get; set; } = new List<Narrative>();
}