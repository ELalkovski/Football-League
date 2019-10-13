using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Common.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Logo { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsReceived { get; set; }

        public int Points { get; set; }

        public bool IsDeleted { get; set; }

        public IList<Match> HomeMatchesPlayed { get; set; }

        public IList<Match> AwayMatchesPlayed { get; set; }
    }
}
