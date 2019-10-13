using FootballLeague.Common.Models;
using FootballLeague.Data;
using FootballLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Match> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Match model)
        {
            throw new NotImplementedException();
        }
    }
}
