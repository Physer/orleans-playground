using Microsoft.AspNetCore.Mvc;
using UrlShorterner;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOrleans(options =>
{
    options.UseLocalhostClustering();
    options.AddMemoryGrainStorage(Constants.StorageName);
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGet("/shorten", static async (IGrainFactory grainFactory, [FromQuery] string url) =>
{
    var shortenedSegment = Guid.NewGuid().GetHashCode().ToString("X");
    var grain = grainFactory.GetGrain<IUrlShortenerGrain>(shortenedSegment);
    await grain.SetUrlAsync(url);
    return Results.Ok(new
    {
        Url = $"/link/{shortenedSegment}"
    });
});

app.MapGet("/link/{routeSegment}", static async (IGrainFactory grainFactory, [FromRoute] string routeSegment) =>
{
    var grain = grainFactory.GetGrain<IUrlShortenerGrain>(routeSegment);
    var url = await grain.GetUrlAsync();
    if (string.IsNullOrWhiteSpace(url))
        return Results.BadRequest();

    return Results.Redirect(url);
});

app.Run();
