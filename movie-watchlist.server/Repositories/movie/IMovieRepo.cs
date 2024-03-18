using movie_watchlist.server.Models;

namespace movie_watchlist.server.Repositories.movie
{
    public interface IMovieRepo
    {
        public Task<Movie?> AddMovieAsync(int movieId, string moviename);
        public Task<Movie?> GetMovieByIdAsync(int movieId);
    }
}
