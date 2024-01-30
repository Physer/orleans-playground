using Orleans.Runtime;

namespace UrlShorterner;

internal sealed class UrlShortenerGrain([PersistentState(Constants.StateName, Constants.StorageName)] IPersistentState<UrlState> state) : Grain, IUrlShortenerGrain
{
    private readonly IPersistentState<UrlState> _state = state;

    public async Task SetUrlAsync(string url)
    {
        _state.State = new()
        {
            ShortenedUrl = this.GetPrimaryKeyString(),
            FullUrl = url
        };
        await _state.WriteStateAsync();
    }

    public Task<string?> GetUrlAsync() => Task.FromResult(_state.State?.FullUrl);
}
