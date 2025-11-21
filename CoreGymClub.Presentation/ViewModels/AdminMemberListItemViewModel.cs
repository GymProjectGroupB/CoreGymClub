namespace CoreGymClub.Presentation.ViewModels
{
    public class AdminMemberListItemViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        public string FirstName { get; set; } = "-";
        public string LastName { get; set; } = "-";

        public string MembershipType { get; set; } = "Inget";
        public DateTime? MembershipStart { get; set; }
        public DateTime? MembershipEnd { get; set; }
    }
}
