using Microsoft.AspNetCore.Identity;

namespace CoreGymClub.Presentation.Models;

public class Booking
{
    public int Id { get; set; }

    public int TrainingSessionId { get; set; }
    public TrainingSession TrainingSession { get; set; } = default!;

    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = default!;
}
