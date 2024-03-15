using Microsoft.AspNetCore.Mvc;
using movie_watchlist.server.DTOs;
using movie_watchlist.server.Repositories.watchlist;

namespace movie_watchlist.server.Endpoints
{
    static public class WatchlistEndpoints
    {
        static public void ConfigureWatchlistEndpoints(this WebApplication app)
        {
            var watchlists = app.MapGroup("/watchlists");

            watchlists.MapGet("", GetWatchlists);
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
    }
}
