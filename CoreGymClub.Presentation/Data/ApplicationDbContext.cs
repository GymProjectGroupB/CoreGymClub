using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TrainingSession> TrainingSessions { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
