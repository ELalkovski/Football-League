using FootballLeague.Common.Models;
using FootballLeague.Data;
using FootballLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services
{
    public class MatchService : IMatchService
    {
        private readonly FootballLeagueContext _db;

        public MatchService(FootballLeagueContext db)
        {
            this._db = db;
        }

        public async Task Create(Match model)
        {
            this._db.Matches.Add(model);
            await this._db.SaveChangesAsync();
        }

        public async Task Update(Match model)
        {
            this._db.Matches.Update(model);
            await this._db.SaveChangesAsync();
        }

        public async Task<Match> Get(int id)
        {
            var match = await this._db.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .FirstOrDefaultAsync(x => x.Id == id);

            return match;    
        }

        public async Task<IList<Match>> List()
        {
            var matches = await this._db.Matches
                .Where(x => !x.IsDeleted)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .ToListAsync();

            return matches;
        }
    }
}
