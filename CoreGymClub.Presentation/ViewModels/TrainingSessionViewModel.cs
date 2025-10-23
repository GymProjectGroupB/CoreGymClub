namespace CoreGymClub.Presentation.ViewModels;

public sealed record TrainingSessionViewModel(
    int Id,
    string SessionName,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Location,
    string Instructor
);
