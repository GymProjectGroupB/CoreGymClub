namespace CoreGymClub.Presentation.Models
{
    public class TrainingClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Location { get; set; }
        public string Instructor { get; set; }
    }
}
