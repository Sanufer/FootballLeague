namespace FootballLeague.Domain.Entities
{
    public class Teams
    {
        public int TeamId { get; set; }  // Unique identifier for the team
        public string Name { get; set; }  = string.Empty; // Name of the team (e.g., "Manchester United")
        public string Coach { get; set; } = string.Empty; // Name of the team's coach
        public string Stadium { get; set; } = string.Empty; // Home stadium of the team
        public int FoundedYear { get; set; } // Year the team was founded
        public string City { get; set; } = string.Empty; //City the Team is based on 
        public int LeagueId { get; set; } //Foreign key to the League
        public Leagues Leagues { get; set; } // Navigation property to League
    }
}
