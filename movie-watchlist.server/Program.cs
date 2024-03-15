using Microsoft.EntityFrameworkCore;
using movie_watchlist.server.Data;
using movie_watchlist.server.Endpoints;
using movie_watchlist.server.Repositories.watchlist;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWatchlistRepo, WatchlistRepo>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WatchlistsContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ElephantConnectionSting"));

    options.EnableDetailedErrors();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseHttpsRedirection();

app.ConfigureWatchlistEndpoints();

app.Run();


/*
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
*/

/*
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/
