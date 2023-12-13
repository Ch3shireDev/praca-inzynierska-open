using WorldFacts.Library.Models;

namespace WorldFacts.Library.Services;

public interface IPowerBiService
{
    Task<PowerBIEmbedConfig> GetEmbedConfig();
}