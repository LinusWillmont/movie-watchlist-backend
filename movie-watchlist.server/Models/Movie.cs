using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie_watchlist.server.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("movie_name")]
        public string MovieName { get; set; }
    }
}
