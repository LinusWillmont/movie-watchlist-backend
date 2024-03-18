using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("watchlists_movies")]
    public class WatchlistMovie
    {
        // References to other tables
        [ForeignKey(nameof(Watchlist))]
        [Required]
        [Column("watchlist_id")]
        public int WatchlistId { get; set; }
        public Watchlist Watchlist { get; set; } = null!;

        [ForeignKey(nameof(Movie))]
        [Required]
        [Column("movie_id")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        // Properties
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
