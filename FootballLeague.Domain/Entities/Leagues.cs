using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Domain.Entities
{
    public class Leagues
    {
        [Key]
        public int LeagueId { get; set; } // Unique identifier for the league

        [Required]
        public string Name { get; set; } = string.Empty; // Name of the league (e.g., "Premier League")

        [Required]
        public string Country { get; set; } = string.Empty; // Country where the league is based (e.g., "England")

        [Required]
        public int FoundedYear { get; set; } // The year the league was founded (e.g., 1992)

        public List<Teams> Teams { get; set; }
    }
}
