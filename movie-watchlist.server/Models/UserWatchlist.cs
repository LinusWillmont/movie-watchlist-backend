using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("users_watchlists")]
    public class UserWatchlist
    {
        // ids
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        // References to other tables
        [ForeignKey(nameof(User))]
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Watchlist))]
        [Required]
        [Column("watchlist_id")]
        public int WatchlistId { get; set; }
        public Watchlist Watchlist { get; set; } = null!;

        //Properties
        [Required]
        [Column("archived")]
        public bool Archived { get; set; } = false;

        [Column("rating")]
        public float? Rating { get; set; } = null;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



    }
}
