
namespace UrlShorterner;

public interface IUrlShortenerGrain : IGrainWithStringKey
{
    Task<string?> GetUrlAsync();
    Task SetUrlAsync(string url);
}
