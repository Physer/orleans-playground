using Microsoft.AspNetCore.Mvc;
using UrlShortenerWithoutOrleans;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IStateManager, StateManager>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/shorten", static ([FromServices] IStateManager stateManager, [FromQuery] string url) =>
{
    var segment = Guid.NewGuid().GetHashCode().ToString("X");
    stateManager.SetUrl(segment, url);
    return Results.Ok(new
    {
        Url = $"/link/{segment}"
    });
});

app.MapGet("/link/{routeSegment}", static ([FromServices] IStateManager stateManager, [FromRoute] string routeSegment) =>
{
    var url = stateManager.GetUrl(routeSegment);
    if (string.IsNullOrWhiteSpace(url))
        return Results.BadRequest();

    return Results.Redirect(url);
});

app.Run();
