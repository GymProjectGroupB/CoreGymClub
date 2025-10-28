namespace CoreGymClub.Presentation.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
    }
}
