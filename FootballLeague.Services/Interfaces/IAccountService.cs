using FootballLeague.Common.ViewModels;
using System.Threading.Tasks;

namespace FootballLeague.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> SignIn(LoginViewModel model);
        Task SignOut();
        Task Register(RegisterViewModel model);
        bool UserExists(string email);
    }
}
