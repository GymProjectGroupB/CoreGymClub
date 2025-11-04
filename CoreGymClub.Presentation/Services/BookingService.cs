using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Services;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _db;

    public BookingService(ApplicationDbContext db) => _db = db;

    public async Task<(bool ok, string message)> BookAsync(string userId, int sessionId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return (false, "You must be signed in.");

        var session = await _db.TrainingSessions
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == sessionId, ct);

        if (session is null)
            return (false, "Session not found.");

        // Already booked by this user?
        if (session.Bookings.Any(b => b.UserId == userId))
            return (false, "You are already booked for this session.");

        // Capacity check
        if (session.Bookings.Count >= session.Capacity)
            return (false, "Session is full.");

        _db.Bookings.Add(new Booking
        {
            TrainingSessionId = session.Id,
            UserId = userId
        });

        try
        {
            await _db.SaveChangesAsync(ct);
            return (true, "Booked!");
        }
        catch (DbUpdateException)
        {
            // Covers the unique index race (UserId, TrainingSessionId)
            return (false, "Could not complete booking. Try again.");
        }
    }

    public async Task<(bool ok, string message)> UnbookAsync(string userId, int sessionId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return (false, "You must be signed in.");

        var booking = await _db.Bookings
            .FirstOrDefaultAsync(b => b.TrainingSessionId == sessionId && b.UserId == userId, ct);

        if (booking is null)
            return (false, "No booking found for this session.");

        _db.Bookings.Remove(booking);

        try
        {
            await _db.SaveChangesAsync(ct);
            return (true, "Unbooked!");
        }
        catch (DbUpdateException ex)
        {
            return (false, $"DB error: {ex.GetBaseException().Message}");
        }
    }
}