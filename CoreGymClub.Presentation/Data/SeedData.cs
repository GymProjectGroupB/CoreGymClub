using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Skapa roller
            string[] roleNames = { "Admin", "Member", "Trainer" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            var adminEmail = "admin@hotmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            await SeedTrainingSessionsAsync(serviceProvider);
        }

        private static async Task SeedTrainingSessionsAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (await db.TrainingSessions.AnyAsync())
                return;

            var now = DateTime.UtcNow.Date.AddHours(17);

            var sessions = new[]
            {
        new TrainingSession
        {
            Title = "Strength & Conditioning",
            DateTimeStart = now.AddDays(1),
            DateTimeEnd = now.AddDays(1).AddHours(1),
            Location = "Main Hall A",
            Instructor = "Coach Lisa",
            Capacity = 0

        },
        new TrainingSession
        {
            Title = "Morning Yoga Flow",
            DateTimeStart = now.AddDays(2).AddHours(-10),
            DateTimeEnd = now.AddDays(2).AddHours(-9),
            Location = "Studio 2",
            Instructor = "Anna Lee",
            Capacity = 15

        },
        new TrainingSession
        {
            Title = "HIIT Express",
            DateTimeStart = now.AddDays(3),
            DateTimeEnd = now.AddDays(3).AddHours(1),
            Location = "Gym Floor",
            Instructor = "Mark Johnson",
            Capacity = 12


        },
        new TrainingSession
        {
            Title = "Spin Class 45",
            DateTimeStart = now.AddDays(4),
            DateTimeEnd = now.AddDays(4).AddMinutes(45),
            Location = "Spin Room",
            Instructor = "Sarah Thompson",
            Capacity = 5
        },
        new TrainingSession
        {
            Title = "Pilates Core",
            DateTimeStart = now.AddDays(5).AddHours(-9),
            DateTimeEnd = now.AddDays(5).AddHours(-8),
            Location = "Studio 1",
            Instructor = "Emily Carter",
            Capacity = 8
        }
    };

            db.TrainingSessions.AddRange(sessions);
            await db.SaveChangesAsync();
        }

    }
}