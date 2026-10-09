using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Provides local fallback connection string for migration creation
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KooliProjektDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Tee automaatselt ootel olevad migratsioonid (tekitab andmebaasi)
                context.Database.Migrate();

                // Kui andmebaasis on juba turniire olemas, ei tehta midagi
                if (context.Set<Tournament>().Any())
                {
                    return;
                }

                // 1. Kasutajad (User) - kasutame enumit Role
                var users = new List<User>();
                for (int i = 1; i <= 35; i++)
                {
                    users.Add(new User
                    {
                        Username = $"kasutaja{i}",
                        Email = $"kasutaja{i}@kooliprojekt.ee"
                        // Kui User klassil on Role väli, saab määrata: Role = Role.USER
                    });
                }
                context.Set<User>().AddRange(users);
                context.SaveChanges();

                // 2. Turniirid (Tournament)
                var tournaments = new List<Tournament>();
                for (int i = 1; i <= 35; i++)
                {
                    tournaments.Add(new Tournament
                    {
                        Name = $"Turniir {i}",
                        StartDate = DateTime.Now.AddDays(-i),
                        EndDate = DateTime.Now.AddDays(i)
                    });
                }
                context.Set<Tournament>().AddRange(tournaments);
                context.SaveChanges();

                // 3. Meeskonnad (Team)
                var teams = new List<Team>();
                for (int i = 1; i <= 35; i++)
                {
                    teams.Add(new Team { Name = $"Meeskond {i}" });
                }
                context.Set<Team>().AddRange(teams);
                context.SaveChanges();

                // 4. Mängud (Game) - kui Game klassil on Round väli, saab kasutada: Round = Round.FINAL
                var games = new List<Game>();
                for (int i = 1; i <= 35; i++)
                {
                    games.Add(new Game());
                }
                context.Set<Game>().AddRange(games);
                context.SaveChanges();

                // 5. Ennustused (Prediction)
                var predictions = new List<Prediction>();
                for (int i = 1; i <= 35; i++)
                {
                    predictions.Add(new Prediction());
                }
                context.Set<Prediction>().AddRange(predictions);
                context.SaveChanges();

                // 6. Edetabel (Scoreboard)
                var scoreboards = new List<Scoreboard>();
                for (int i = 1; i <= 35; i++)
                {
                    scoreboards.Add(new Scoreboard());
                }
                context.Set<Scoreboard>().AddRange(scoreboards);
                context.SaveChanges();
            }
        }
    }
}