using Microsoft.AspNetCore.Mvc;
using UrlShortenerWithoutOrleans;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/shorten", static ([FromQuery] string url) =>
{
    var segment = Guid.NewGuid().GetHashCode().ToString("X");
    StateManager.SetUrl(segment, url);
    return Results.Ok(new
    {
        Url = $"/link/{segment}"
    });
});

app.MapGet("/link/{routeSegment}", static ([FromRoute] string routeSegment) =>
{
    var url = StateManager.GetUrl(routeSegment);
    if (string.IsNullOrWhiteSpace(url))
        return Results.BadRequest();

    return Results.Redirect(url);
});

app.Run();
