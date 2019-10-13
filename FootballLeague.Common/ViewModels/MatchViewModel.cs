using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague.Common.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }
        public TeamViewModel HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public TeamViewModel AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }
}
