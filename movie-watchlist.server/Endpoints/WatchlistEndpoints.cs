using Microsoft.AspNetCore.Mvc;
using movie_watchlist.server.DTOs;
using movie_watchlist.server.Models.Payloads.WatchlistPayloads;
using movie_watchlist.server.Repositories.movie;
using movie_watchlist.server.Repositories.watchlist;

namespace movie_watchlist.server.Endpoints
{
    static public class WatchlistEndpoints
    {
        static public void ConfigureWatchlistEndpoints(this WebApplication app)
        {
            var watchlists = app.MapGroup("/watchlists");

            watchlists.MapPost("", CreateWatchlist);
            watchlists.MapGet("", GetWatchlists);
            watchlists.MapGet("/{watchlistId}", GetWatchlistById);
            watchlists.MapPut("/{watchlistId}", UpdateWatchlistMetadata);
            //app.MapGet("/users/{userId}/watchlists", GetUserWatchliststs); stretch goal
            watchlists.MapPut("/{watchlistId}/movies/add", AddMovieToWatchlist);
            watchlists.MapPut("/{watchlistId}/movies/remove", RemoveMovieFromWatchlist);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static private async Task<IResult> GetWatchlistById(IWatchlistRepo repository, int watchlistId)
        {
            var watchlist = await repository.GetWatchlistByIdAsync(watchlistId);

            if (watchlist == null)
            {
                return TypedResults.NotFound($"Watchlist with id:{watchlistId} not found");
            }

            return TypedResults.Ok(new WatchlistDTO(watchlist));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static private async Task<IResult> UpdateWatchlistMetadata(IWatchlistRepo repository, int watchlistId, WatchlistMetadataPayload payload)
        {
            string payloadCheckResponse = payload.CheckPayload();
            if (payloadCheckResponse != string.Empty)
            {
                return TypedResults.BadRequest(payloadCheckResponse);
            }

            var watchlistToUpdate = await repository.GetWatchlistByIdAsync(watchlistId);
            if (watchlistToUpdate == null) { return TypedResults.NotFound($"Watchlist with id:{watchlistId} not found"); }

            var updatedWatchlist = await repository.UpdateWatchlistAsync(
                watchlistId,
                payload.Name,
                payload.Description,
                watchlistToUpdate.Movies.Select(m => m.Id).ToList()
            );

            if (updatedWatchlist == null) { return TypedResults.NotFound($"Watchlist with id:{watchlistId} not found"); }


            return TypedResults.Ok(new WatchlistDTO(updatedWatchlist));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static private async Task<IResult> AddMovieToWatchlist(IWatchlistRepo watchlistRepo, IMovieRepo movieRepo, int watchlistId, WatchlistUpdateMoviePayload payload)
        {
            string payloadCheckResponse = payload.CheckPayload();
            if (payloadCheckResponse != string.Empty)
            {
                return TypedResults.BadRequest(payloadCheckResponse);
            }

            var watchlistToUpdate = await watchlistRepo.GetWatchlistByIdAsync(watchlistId);
            if (watchlistToUpdate == null)
            {
                return TypedResults.NotFound("Watchlist not found");
            }

            await movieRepo.AddMovieAsync(payload.MovieId, payload.MovieName);

            var watchlistMovieIDs = watchlistToUpdate.Movies.Select(m => m.Id).ToList();

            if (watchlistMovieIDs.Contains(payload.MovieId))
            {
                return TypedResults.BadRequest("Movie already exists in watchlist");
            }

            watchlistMovieIDs.Add(payload.MovieId);

            var updatedWatchlist = await watchlistRepo.UpdateWatchlistAsync(
                watchlistId,
                watchlistToUpdate.Name,
                watchlistToUpdate.Description,
                watchlistMovieIDs
            );

            return TypedResults.Ok(new WatchlistDTO(updatedWatchlist!));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        static private async Task<IResult> RemoveMovieFromWatchlist(IWatchlistRepo repository, int watchlistId, WatchlistUpdateMoviePayload payload)
        {
            string payloadCheckResponse = payload.CheckPayload();
            if (payloadCheckResponse != string.Empty)
            {
                return TypedResults.BadRequest(payloadCheckResponse);
            }

            var watchlistToUpdate = await repository.GetWatchlistByIdAsync(watchlistId);
            if (watchlistToUpdate == null)
            {
                return TypedResults.NotFound("Watchlist not found");
            }

            var watchlistMovieIDs = watchlistToUpdate.Movies.Select(m => m.Id).ToList();
            var foundMovieInWatchlist = watchlistMovieIDs.Remove(payload.MovieId);

            if (!foundMovieInWatchlist)
            {
                return TypedResults.BadRequest($"Movie with id:{payload.MovieId} not found in watchlist with id:{watchlistId}");
            }

            var updatedWatchlist = await repository.UpdateWatchlistAsync(
                watchlistId,
                watchlistToUpdate.Name,
                watchlistToUpdate.Description,
                watchlistMovieIDs
            );

            return TypedResults.Ok(new WatchlistDTO(updatedWatchlist!));
        }
    }
}
