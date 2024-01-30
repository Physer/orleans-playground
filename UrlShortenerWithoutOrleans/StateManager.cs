using System.Collections.Concurrent;

namespace UrlShortenerWithoutOrleans;

public class StateManager : IStateManager
{
    private readonly ConcurrentBag<UrlState> _state = [];

    public void SetUrl(string segment, string url) => _state.Add(new UrlState
    {
        FullUrl = url,
        Segment = segment,
    });

    public string? GetUrl(string segment) => _state.FirstOrDefault(state => state.Segment?.Equals(segment) == true)?.FullUrl;
}

public interface IStateManager
{
    string? GetUrl(string segment);
    void SetUrl(string segment, string url);
}