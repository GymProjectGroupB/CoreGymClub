using CoreGymClub.Presentation.Models;

namespace CoreGymClub.Presentation.Services;
public interface ITrainingSessionService
{
    Task<List<TrainingSession>> UpcomingAsync(CancellationToken ct = default);
}