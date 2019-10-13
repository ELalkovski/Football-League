using AutoMapper;
using FootballLeague.Common.Models;
using FootballLeague.Common.ViewModels;

namespace FootballLeague.App.Mapping
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            this.CreateMap<Team, TeamViewModel>()
                .ReverseMap();
        }
    }
}
