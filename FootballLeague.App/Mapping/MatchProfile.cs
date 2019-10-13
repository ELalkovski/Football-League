using AutoMapper;
using FootballLeague.Common.Models;
using FootballLeague.Common.ViewModels;

namespace FootballLeague.App.Mapping
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            this.CreateMap<Match, MatchViewModel>()
                .ReverseMap();
        }
    }
}
