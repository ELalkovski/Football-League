using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FootballLeague.Common.Models;
using FootballLeague.Common.ViewModels;
using FootballLeague.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MatchController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMatchService _matchService;
        private readonly ITeamService _teamService;

        public MatchController(IMapper mapper, IMatchService matchService, ITeamService teamService)
        {
            this._mapper = mapper;
            this._matchService = matchService;
            this._teamService = teamService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            return this.View(new MatchViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(MatchViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var match = this._mapper.Map<Match>(model);
            var homeTeam = await this._teamService.Get(match.HomeTeamId);
            var awayTeam = await this._teamService.Get(match.AwayTeamId);

            await this._matchService.Create(match);
            await this._teamService.UpdatePoints(homeTeam, awayTeam, match.HomeGoals, match.AwayGoals);

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var team = await this._teamService.Get(id);
        //    var model = this._mapper.Map<TeamViewModel>(team);
        //    return this.View(model);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(TeamViewModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    var team = this._mapper.Map<Team>(model);
        //    await this._teamService.Update(team);

        //    return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        //}
    }
}