using FootballLeague.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FootballLeague.App.Helpers
{
    public static class AdminAccountSeeder
    {
        public static void SeedAdminAccount(UserManager<User> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                var adminEmail = "admin@a.com";
                var adminPassword = "admin12";

                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };

                var result = userManager.CreateAsync(adminUser, adminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
        }
    }
}
