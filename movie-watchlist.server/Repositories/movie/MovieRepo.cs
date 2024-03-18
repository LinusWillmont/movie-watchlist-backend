using Microsoft.EntityFrameworkCore;
using movie_watchlist.server.Data;
using movie_watchlist.server.Models;

namespace movie_watchlist.server.Repositories.movie
{
    public class MovieRepo : IMovieRepo
    {
        private WatchlistsContext _db;

        public MovieRepo(WatchlistsContext db)
        {
            _db = db;
        }

        public async Task<Movie?> AddMovieAsync(int movieId, string movieName)
        {
            var foundMovie = await GetMovieByIdAsync(movieId);
            if (foundMovie != null)
            {
                return null;
            }

            var movie = await _db.Movies.AddAsync(new Movie { Id = movieId, MovieName = movieName });
            await _db.SaveChangesAsync();
            return movie.Entity;
        }

        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(movie => movie.Id.Equals(movieId));
            return movie == null ? null : movie;
        }
    }
}
