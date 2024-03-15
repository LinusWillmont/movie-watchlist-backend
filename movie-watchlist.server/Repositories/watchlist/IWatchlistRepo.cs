using movie_watchlist.server.Models;

namespace movie_watchlist.server.Repositories.watchlist
{
    public interface IWatchlistRepo
    {
        public Task<Watchlist?> CreateWatchlistAsync(string name, string description);
        public Task<List<Watchlist>> GetWatchlistsAsync();
        public Task<Watchlist?> GetWatchlistByIdAsync(int watchlistId);
        public Task<Watchlist?> UpdateWatchlistAsync(int watchlistId, string name, string description, List<int> movieIDs);
        public Task<Watchlist?> DeleteWatchlistAsync(int watchlistId);
    }
}
