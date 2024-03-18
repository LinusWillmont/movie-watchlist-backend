using movie_watchlist.server.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("users")]
    public class User
    {
        //ids
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        // References to other tables
        public List<Movie> Movies { get; } = [];
        public List<UserMovie> UserMovie { get; } = [];

        public List<Watchlist> Watchlists { get; } = [];
        public List<UserWatchlist> UserWatchlist { get; } = [];

        // Properties
        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("is_enabled")]
        public bool IsEnabled { get; set; } = true;

        [Required]
        [Column("role")]
        public Enums.Role Role { get; set; } = Enums.Role.User;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
