using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FootballLeague.Domain.Entities
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }  // Unique identifier for the team
        public string Name { get; set; }  = string.Empty; // Name of the team (e.g., "Manchester United")
        public string Coach { get; set; } = string.Empty; // Name of the team's coach
        public string Stadium { get; set; } = string.Empty; // Home stadium of the team
        public int FoundedYear { get; set; } // Year the team was founded
        public string City { get; set; } = string.Empty; //City the Team is based on 
        
        public int LeagueId { get; set; } // Foreign key to the League

        [JsonIgnore]
        public League? League { get; set; } // Navigation property to League
    }
}