using FootballLeague.Common.Models;
using FootballLeague.Common.ViewModels;
using FootballLeague.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services
{    
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public async Task<bool> SignIn(LoginViewModel model)
        {
            var result = await this._signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            return result.Succeeded;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterViewModel model)
        {
            var user = new User 
            { 
                UserName = model.Email,
                Email = model.Email 
            };

            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
        }

        public bool UserExists(string email)
        {
            return _userManager.Users.Any(x => x.Email == email);
        }
    }
}
