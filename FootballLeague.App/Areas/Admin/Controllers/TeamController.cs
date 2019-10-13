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
    public class TeamController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITeamService _teamService;

        public TeamController(IMapper mapper, ITeamService teamService)
        {
            this._mapper = mapper;
            this._teamService = teamService;
        }

        [HttpGet]
        [Route("/Team/Table")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Table()
        {
            var teams = await this._teamService.List();
            var models = this._mapper.Map<IList<Team>, IList<TeamViewModel>>(teams)
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalsScored)
                .ThenByDescending(x => x.Wins)
                .ToList();

            return this.View(models);
        }

        [HttpGet]
        [Route("/Team/Details")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Details(int id)
        {
            var team = await this._teamService.Get(id);
            var homeMatches = await this._teamService.GetTeamHomeMatches(team.Id);
            var awayMatches = await this._teamService.GetTeamAwayMatches(team.Id);

            team.HomeMatchesPlayed = homeMatches;
            team.AwayMatchesPlayed = awayMatches;

            var model = this._mapper.Map<Team, TeamViewModel>(team);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            return this.View(new TeamViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(TeamViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var team = this._mapper.Map<Team>(model);
            await this._teamService.Create(team);

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await this._teamService.Get(id);
            var model = this._mapper.Map<TeamViewModel>(team);
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(TeamViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var team = this._mapper.Map<Team>(model);
            await this._teamService.Update(team);

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(TeamViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var team = this._mapper.Map<Team>(model);
            team.IsDeleted = true;

            await this._teamService.Update(team);

            return this.RedirectToAction("Table", "Team", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetTeams()
        {
            var teams = await this._teamService.List();
            var models = this._mapper.Map<IList<Team>, IList<TeamViewModel>>(teams);
            return this.Json(models);
        }
    }
}