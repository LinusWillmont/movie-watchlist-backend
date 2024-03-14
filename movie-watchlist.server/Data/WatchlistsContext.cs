using Microsoft.EntityFrameworkCore;
using movie_watchlist.server.Models;

namespace movie_watchlist.server.Data
{
    public class WatchlistsContext : DbContext
    {
        public WatchlistsContext(DbContextOptions<WatchlistsContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add model relations
            // this.Database.EnsureCreated();
        }

        // Add DbSets when models are created
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
