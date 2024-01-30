namespace UrlShortenerWithoutOrleans;

public static class StateManager
{
    private static readonly HashSet<UrlState> _state = [];

    public static void SetUrl(string segment, string url)
    {
        _state.Add(new UrlState
        {
            FullUrl = url,
            Segment = segment,
        });
    }

    public static string? GetUrl(string segment) => _state.FirstOrDefault(state => state.Segment?.Equals(segment) == true)?.FullUrl;
}
