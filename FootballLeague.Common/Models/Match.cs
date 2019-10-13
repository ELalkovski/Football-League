using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Common.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }
}
