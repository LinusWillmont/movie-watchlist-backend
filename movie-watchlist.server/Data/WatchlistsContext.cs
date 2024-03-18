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
            modelBuilder.Entity<User>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Users)
                .UsingEntity<UserMovie>();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Watchlists)
                .WithMany(e => e.Users)
                .UsingEntity<UserWatchlist>();

            modelBuilder.Entity<Watchlist>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Watchlists)
                .UsingEntity<WatchlistMovie>();

            // this.Database.EnsureCreated();
        }

        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
