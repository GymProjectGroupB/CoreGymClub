
namespace CoreGymClub.Presentation.Services;

public interface IBookingService
{
    Task<(bool ok, string message)> BookAsync(string userId, int sessionId, CancellationToken ct = default);
    Task<(bool ok, string message)> UnbookAsync(string userId, int sessionId, CancellationToken ct = default);
}