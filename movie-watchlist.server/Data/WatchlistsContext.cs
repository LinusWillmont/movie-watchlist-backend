using Microsoft.EntityFrameworkCore;

namespace movie_watchlist.server.Data
{
    public class WatchlistsContext : DbContext
    {
        public WatchlistsContext(DbContextOptions<WatchlistsContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add model relations
        }

        // Add DbSets when models are created
    }
}
