using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("movies")]
    public class Movie
    {
        //ids
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        // References to other tables
        public List<UserMovie> UserMovie { get; } = [];
        // public List<User> Users { get; } = [];

        public List<WatchlistMovie> WatchlistMovie { get; } = [];
        //public List<Watchlist> Watchlist { get; } = [];

        //Properties
        [Required]
        [Column("movie_name")]
        public string MovieName { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
