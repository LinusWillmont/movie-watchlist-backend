using api_cinema_challenge.Data;
using Microsoft.EntityFrameworkCore;
using movie_watchlist.server.Data;
using movie_watchlist.server.Endpoints;
using movie_watchlist.server.Repositories.watchlist;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

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

app.UseCors();

app.ConfigureWatchlistEndpoints();

app.ApplyProjectMigrations();

app.Run();