namespace CoreGymClub.Presentation.ViewModels;

public class UpcomingBookingItem
{
    public int TrainingSessionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public string Location { get; set; } = string.Empty;
}
