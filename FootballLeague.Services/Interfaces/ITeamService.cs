using FootballLeague.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.Services.Interfaces
{
    public interface ITeamService
    {
        Task Create(Team model);
        Task<Team> Get(int id);
        Task<IList<Team>> List();
        Task Update(Team model);
        Task UpdatePoints(Team homeTeam, Team awayTeam, int homeGoals, int awayGoals);
        Task<IList<Match>> GetTeamHomeMatches(int homeTeamId);
        Task<IList<Match>> GetTeamAwayMatches(int awayTeamId);
    }
}
