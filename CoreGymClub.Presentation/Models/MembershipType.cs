namespace CoreGymClub.Presentation.Models
{
    public class MembershipType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;   // t.ex. Ordinarie, Student
        public string Description { get; set; } = string.Empty;
        public decimal PricePerMonth { get; set; }         // pris per månad
        public bool IsActive { get; set; } = true;         // för att kunna “inaktivera” utan att radera
    }
}
