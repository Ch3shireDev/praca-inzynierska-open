namespace WorldFacts.Library.Models;

public class PowerBISettings
{
    public Guid ApplicationId { get; set; }
    public string ApplicationSecret { get; set; } = null!;
    public Guid ReportId { get; set; }
    public Guid WorkspaceId { get; set; }
    public string AuthorityUrl { get; set; } = null!;
    public string ResourceUrl { get; set; } = null!;
    public string ApiUrl { get; set; } = null!;
    public string EmbedUrlBase { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}