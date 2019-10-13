using FootballLeague.Common.Models;
using FootballLeague.Data;
using FootballLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services
{
    public class TeamService : ITeamService
    {
        private const int WinPoints = 3;
        private const int DrawPoints = 1;

        private readonly FootballLeagueContext _db;

        public TeamService(FootballLeagueContext db)
        {
            this._db = db;
        }

        public async Task Create(Team model)
        {
            this._db.Teams.Add(model);
            await this._db.SaveChangesAsync();
        }

        public async Task Update(Team model)
        {
            this._db.Teams.Update(model);
            await this._db.SaveChangesAsync();
        }

        public async Task<Team> Get(int id)
        {
            var team = await this._db.Teams
                .FirstOrDefaultAsync(x => x.Id == id);
            return team;
        }

        public async Task<IList<Team>> List()
        {
            var teams = await this._db.Teams
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            return teams;
        }

        public async Task<IList<Match>> GetTeamHomeMatches(int homeTeamId)
        {
            var matches = await this._db.Matches
                .Where(x => x.HomeTeamId == homeTeamId)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .ToListAsync();

            return matches;
        }

        public async Task<IList<Match>> GetTeamAwayMatches(int awayTeamId)
        {
            var matches = await this._db.Matches
                .Where(x => x.AwayTeamId == awayTeamId)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .ToListAsync();

            return matches;
        }

        public async Task UpdatePoints(Team homeTeam, Team awayTeam, int homeGoals, int awayGoals)
        {
            if (homeGoals > awayGoals)
            {
                homeTeam.Points += WinPoints;                
            }
            else if (homeGoals < awayGoals)
            {
                awayTeam.Points += WinPoints;
            }
            else
            {
                homeTeam.Points += DrawPoints;
                awayTeam.Points += DrawPoints;
            }

            homeTeam.GoalsScored += homeGoals;
            homeTeam.GoalsReceived += awayGoals;
            awayTeam.GoalsScored += awayGoals;
            awayTeam.GoalsReceived += homeGoals;

            this._db.Teams.Update(homeTeam);
            this._db.Teams.Update(awayTeam);
            await this._db.SaveChangesAsync();
        }
    }
}
