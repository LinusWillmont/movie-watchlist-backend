using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("watchlists")]
    public class Watchlist
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        // References to other tables
        public List<WatchlistMovie> WatchlistMovie { get; } = [];
        //public List<Movie> Movies { get; } = [];

        [ForeignKey(nameof(User))]
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
