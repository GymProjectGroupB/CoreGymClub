using System.ComponentModel.DataAnnotations;

namespace CoreGymClub.Presentation.ViewModels;

public class CompleteProfileViewModel
{
    [Required(ErrorMessage = "Förnamn är obligatoriskt.")]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efternamn är obligatoriskt.")]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Födelsedatum är obligatoriskt.")]
    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "Gatuadress är obligatoriskt.")]
    public string? Street { get; set; }

    [Required(ErrorMessage = "Stad är obligatoriskt.")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Postnummer är obligatoriskt.")]
    [MaxLength(5)]
    public string? PostalCode { get; set; }
}
