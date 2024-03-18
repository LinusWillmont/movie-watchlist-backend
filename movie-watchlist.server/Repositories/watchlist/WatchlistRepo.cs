using Microsoft.EntityFrameworkCore;
using movie_watchlist.server.Data;
using movie_watchlist.server.Models;

namespace movie_watchlist.server.Repositories.watchlist
{
    public class WatchlistRepo : IWatchlistRepo
    {
        private WatchlistsContext _db;

        public WatchlistRepo(WatchlistsContext db)
        {
            _db = db;
        }

        public async Task<Watchlist> CreateWatchlistAsync(string name, string description)
        {
            var watchlist = await _db.Watchlists.AddAsync(new Watchlist { Name = name, Description = description });
            await _db.SaveChangesAsync();
            return watchlist.Entity;
        }

        public async Task<Watchlist?> DeleteWatchlistAsync(int watchlistId)
        {
            var watchlist = await GetWatchlistByIdAsync(watchlistId);
            if (watchlist == null)
            {
                return null;
            }

            _db.Watchlists.Remove(watchlist);

            return watchlist;
        }

        public async Task<Watchlist?> GetWatchlistByIdAsync(int watchlistId)
        {
            var watchlist = await _db.Watchlists.Include(watchlist => watchlist.Movies).FirstOrDefaultAsync(watchlist => watchlist.Id.Equals(watchlistId));
            return watchlist;
        }

        public async Task<List<Watchlist>> GetWatchlistsAsync()
        {
            var watchlists = await _db.Watchlists.Include(watchlist => watchlist.Movies).ToListAsync();
            return watchlists;
        }

        public async Task<Watchlist?> UpdateWatchlistAsync(int watchlistId, string name, string description, List<int> movieIDs)
        {
            var watchlist = await GetWatchlistByIdAsync(watchlistId);

            if (watchlist == null)
            {
                return null;
            }

            watchlist.Name = name;
            watchlist.Description = description;
            watchlist.UpdatedAt = DateTime.UtcNow;
            await updateMovies();

            await _db.SaveChangesAsync();

            return watchlist;

            async Task updateMovies()
            {
                watchlist.Movies.Clear();
                foreach (var movieID in movieIDs)
                {
                    var movie = await _db.Movies.FirstOrDefaultAsync(movie => movie.Id.Equals(movieID));
                    if (movie != null)
                    {
                        watchlist.Movies.Add(movie);
                    }
                }
            }
        }
    }
}
