using FootballLeague.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Services.Interfaces
{
    public interface IMatchService
    {
        Task Create(Match model);
        Task<Match> Get(int id);        
        Task Update(Match model);
    }
}
