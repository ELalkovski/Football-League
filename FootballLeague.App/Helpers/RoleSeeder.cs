using FootballLeague.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FootballLeague.App.Helpers
{
    public static class RoleSeeder
    {
        public static void SeedPrimaryRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.Roles.Count() == 0)
            {
                IdentityRole[] roles =
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("User"),
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role);
                }
            }
        }
    }
}
