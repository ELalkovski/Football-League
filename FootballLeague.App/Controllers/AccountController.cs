using System.Threading.Tasks;
using FootballLeague.Common.ViewModels;
using FootballLeague.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

            var model = new LoginViewModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var signInSucceeded = await this._accountService.SignIn(model);

            if (!signInSucceeded)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.View(model);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }

            var model = new RegisterViewModel();

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var emailExists = this._accountService.UserExists(model.Email);

            if (emailExists)
            {
                this.ModelState.AddModelError(string.Empty, "This email already exists!");
                return this.View(model);
            }

            await this._accountService.Register(model);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}