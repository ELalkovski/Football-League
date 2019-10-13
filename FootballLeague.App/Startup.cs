using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FootballLeague.Common.Models;
using FootballLeague.Data;
using FootballLeague.Services.Interfaces;
using FootballLeague.Services;
using FootballLeague.App.Helpers;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FootballLeague.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6,
                    RequireDigit = true,
                    RequiredUniqueChars = 0,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };
            });

            services.AddDbContext<FootballLeagueContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<FootballLeagueContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMatchService, MatchService>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/404");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");

                // ** Enable bellow rows for dev exceptions ** //

                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/500");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            RoleSeeder.SeedPrimaryRoles(roleManager);
            AdminAccountSeeder.SeedAdminAccount(userManager);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
