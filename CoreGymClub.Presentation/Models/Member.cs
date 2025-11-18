using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoreGymClub.Presentation.Data
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(50)] 
        public string City { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }
    }
}