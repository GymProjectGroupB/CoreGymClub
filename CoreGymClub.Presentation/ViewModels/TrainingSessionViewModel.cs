namespace CoreGymClub.Presentation.ViewModels;

public sealed record TrainingSessionViewModel(
    int Id,
    string Title,
    DateTime DateTimeStart,
    DateTime DateTimeEnd,
    string Location,
    string Instructor,
    int Capacity,
    int RemainingPlaces,
    bool IsFull,
    bool IsUserBooked
);
