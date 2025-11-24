using beFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace beFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<ExerciseEntry> ExerciseEntries { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }

    }

}
