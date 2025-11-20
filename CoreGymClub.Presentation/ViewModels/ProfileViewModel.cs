namespace CoreGymClub.Presentation.ViewModels;

public class ProfileViewModel
{
    public string Email { get; set; } = string.Empty; //Identity
    public string PhoneNumber { get; set; } = string.Empty; //Identity

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    public string? MembershipName { get; set; }
    public DateTime? MembershipStart { get; set; }
    public DateTime? MembershipEnd { get; set; }
    public List<UpcomingBookingItem> UpcomingBookings { get; set; } = new();
}
