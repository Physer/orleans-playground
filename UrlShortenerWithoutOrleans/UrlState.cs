namespace UrlShortenerWithoutOrleans;

public record UrlState
{
    public string? FullUrl { get; set; }
    public string? Segment { get; set; }
}
