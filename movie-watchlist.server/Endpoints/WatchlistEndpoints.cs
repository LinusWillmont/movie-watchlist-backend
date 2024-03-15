using Microsoft.AspNetCore.Mvc;
using movie_watchlist.server.DTOs;
using movie_watchlist.server.Models.Payloads;
using movie_watchlist.server.Repositories.watchlist;

namespace movie_watchlist.server.Endpoints
{
    static public class WatchlistEndpoints
    {
        static public void ConfigureWatchlistEndpoints(this WebApplication app)
        {
            var watchlists = app.MapGroup("/watchlists");

            watchlists.MapGet("", GetWatchlists);
            watchlists.MapPost("", CreateWatchlist);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static private async Task<IResult> GetWatchlists(IWatchlistRepo repository)
        {
            var watchlists = await repository.GetWatchlistsAsync();

            if (watchlists.Count == 0)
            {
                return TypedResults.NotFound("No watchlists where found");
            }

            var watchlistDTOList = WatchlistDTO.ArrayOfWatchlistDTOs(watchlists);

            return TypedResults.Ok(watchlistDTOList);
        }

        static private async Task<IResult> CreateWatchlist(IWatchlistRepo repository, WatchlistPayload payload)
        {
            string payloadCheckResponse = payload.CheckPayload();
            if (payloadCheckResponse != string.Empty)
            {
                return TypedResults.BadRequest(payloadCheckResponse);
            }

            var newWatchlist = await repository.CreateWatchlistAsync(name: payload.Name, description: payload.Description);
            return TypedResults.Created($"/watchlists/{newWatchlist.Id}", new WatchlistDTO(newWatchlist));
        }
    }
}
