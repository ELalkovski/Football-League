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
            await this._teamService.UpdatePointsNewMatch(homeTeam, awayTeam, match.HomeGoals, match.AwayGoals);

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }

        [HttpGet]
        [Route("/Match/List")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> List()
        {
            var matches = await this._matchService.List();
            var models = this._mapper.Map<IList<Match>, IList<MatchViewModel>>(matches)
                .OrderByDescending(x => x.Date)
                .ToList();

            return this.View(models);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var match = await this._matchService.Get(id);
            var model = this._mapper.Map<MatchViewModel>(match);
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(MatchViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var matchPastState = await this._matchService.Get(model.Id);
            var pastHomeGoals = matchPastState.HomeGoals;
            var pastAwayGoals = matchPastState.AwayGoals;
            var matchNewState = matchPastState;
            var homeTeam = await this._teamService.Get(model.HomeTeamId);
            var awayTeam = await this._teamService.Get(model.AwayTeamId);

            matchNewState.HomeGoals = model.HomeGoals;
            matchNewState.AwayGoals = model.AwayGoals;
            matchNewState.Date = model.Date;

            await this._matchService.Update(matchNewState);
            await this._teamService
                .UpdatePointsRemovedMatch(homeTeam, awayTeam, pastHomeGoals, pastAwayGoals);
            await this._teamService
                .UpdatePointsNewMatch(homeTeam, awayTeam, model.HomeGoals, model.AwayGoals);
            

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(MatchViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var match = this._mapper.Map<MatchViewModel, Match>(model);
            var homeTeam = await this._teamService.Get(model.HomeTeamId);
            var awayTeam = await this._teamService.Get(model.AwayTeamId);

            match.IsDeleted = true;

            await this._teamService
                .UpdatePointsRemovedMatch(homeTeam, awayTeam, model.HomeGoals, model.AwayGoals);
            await this._matchService.Update(match);

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }
    }
}