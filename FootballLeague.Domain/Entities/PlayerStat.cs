using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Domain.Entities
{
    public class PlayerStat
    {
        [Key]
        public int PlayerStatId { get; set; }  // Primary key

        public int Appearances { get; set; }    // Matches played
        public int Goals { get; set; }          // Goals scored
        public int Assists { get; set; }        // Assists provided
        public int YellowCards { get; set; }    // Yellow cards received
        public int RedCards { get; set; }       // Red cards received
        public int MinutesPlayed { get; set; }  // Total minutes played

        public int PlayerId { get; set; }       // Foreign key to Player
        public Player? Player { get; set; }      // Navigation property
    }
}