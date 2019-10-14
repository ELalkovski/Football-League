using FootballLeague.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Data
{
    public class FootballLeagueContext : IdentityDbContext<User>
    {
        public FootballLeagueContext(DbContextOptions<FootballLeagueContext> options)
            :base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Match>(entity =>
            {
                entity.HasOne(m => m.HomeTeam)
                    .WithMany(t => t.HomeMatchesPlayed)
                    .HasForeignKey(m => m.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.AwayTeam)
                    .WithMany(t => t.AwayMatchesPlayed)
                    .HasForeignKey(m => m.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
