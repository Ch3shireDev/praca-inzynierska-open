using WorldFacts.Library.Entities;
using WorldFacts.Library.Models;

namespace WorldFacts.App.Models;

public class ReportData
{
    public string ReportName { get; set; } = "";
    public PowerBIEmbedConfig Config { get; set; } = null!;
    public Narrative Details { get; set; } = null!;
}