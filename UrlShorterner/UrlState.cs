namespace UrlShorterner;

[GenerateSerializer, Alias(nameof(UrlState))]
public sealed record UrlState
{
    [Id(0)]
    public string? ShortenedUrl { get; set; }

    [Id(1)]
    public string? FullUrl { get; set; }
}
