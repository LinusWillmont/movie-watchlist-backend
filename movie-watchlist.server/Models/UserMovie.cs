using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("users_movies")]
    public class UserMovie
    {
        //ids
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

        [ForeignKey(nameof(Movie))]
        [Required]
        [Column("movie_id")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        // Properties
        [Required]
        [Column("seen")]
        public bool Seen { get; set; } = false;

        [Column("rating")]
        public float? Rating { get; set; } = null;

        [Column("comments")]
        public string? Comment { get; set; } = null;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



    }
}
