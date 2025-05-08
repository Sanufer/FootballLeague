using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Domain.Entities
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }  // Unique identifier

        [Required]
        public string Name { get; set; }  = string.Empty;  // Player name

        [Required]
        public string Position { get; set; }  =  string.Empty; // e.g., Goalkeeper, Defender, Midfielder, Forward

        [Required]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }
    }
}