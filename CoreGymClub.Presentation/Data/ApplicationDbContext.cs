using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TrainingSession> TrainingSessions { get; set; } = default!;
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Member> Members { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
               .HasIndex(b => new { b.UserId, b.TrainingSessionId })
               .IsUnique();
        }
    }
}
