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


        [ForeignKey(nameof(User))]
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
