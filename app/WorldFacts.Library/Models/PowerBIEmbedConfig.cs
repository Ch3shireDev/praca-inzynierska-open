using Microsoft.PowerBI.Api.Models;

namespace WorldFacts.Library.Models;

public class PowerBIEmbedConfig
{
    public Guid Id { get; set; }
    public string EmbedUrl { get; set; } = null!;
    public EmbedToken EmbedToken { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string ReportPageId { get; set; } = null!;
}