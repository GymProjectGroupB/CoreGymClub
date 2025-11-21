using System.ComponentModel.DataAnnotations;

namespace CoreGymClub.Presentation.ViewModels
{
    public class EditMemberViewModel
    {
        // IdentityUser
        public string Id { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        // Member (profil)
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        // Membership
        public int? MembershipTypeId { get; set; }
        public DateTime? MembershipStart { get; set; }
        public DateTime? MembershipEnd { get; set; }
    }
}