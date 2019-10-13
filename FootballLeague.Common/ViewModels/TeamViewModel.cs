using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague.Common.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsReceived { get; set; }

        public int Points { get; set; }

        public bool IsDeleted { get; set; }

        public IList<MatchViewModel> HomeMatchesPlayed { get; set; }

        public IList<MatchViewModel> AwayMatchesPlayed { get; set; }
    }
}
