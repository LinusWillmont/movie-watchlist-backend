using api_cinema_challenge.Data;
using Microsoft.EntityFrameworkCore;
using movie_watchlist.server.Data;
using movie_watchlist.server.Endpoints;
using movie_watchlist.server.Repositories.movie;
using movie_watchlist.server.Repositories.watchlist;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWatchlistRepo, WatchlistRepo>();
builder.Services.AddScoped<IMovieRepo, MovieRepo>();

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

app.ApplyProjectMigrations();

app.Run();