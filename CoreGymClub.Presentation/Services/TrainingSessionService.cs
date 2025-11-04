using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Services;

public class TrainingSessionService : ITrainingSessionService
{
    private readonly ApplicationDbContext _db;

    public TrainingSessionService(ApplicationDbContext db) => _db = db;

    public async Task<List<TrainingSession>> UpcomingAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        return await _db.TrainingSessions
            .Include(s => s.Bookings)
            .Where(s => s.DateTimeEnd > now)
            .OrderBy(s => s.DateTimeStart)
            .ToListAsync(ct);
    }
}
