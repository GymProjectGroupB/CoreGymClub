using CoreGymClub.Presentation.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;
using System.ComponentModel.DataAnnotations;

namespace CoreGymClub.Presentation.Pages;

[Authorize(Roles = "Member")]
public class JoinMembershipModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public JoinMembershipModel(ApplicationDbContext context)
    {
        _context = context;
    }



    [Required]
    public string Membership { get; set; }

    public class CreditCardModel
    {
        [Required]
        [Display(Name = "Card Number")]
        [CreditCard] // Built-in validation for credit card format (requires jQuery validation)
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Month/Year")]
        public string Expiration { get; set; } // e.g., "MM/YY"

        [Required]
        [Display(Name = "CVV")]
        [StringLength(4, MinimumLength = 3)] // Usually 3 or 4 digits
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }
    }

    [BindProperty]
    public CreditCardModel CreditCard { get; set; }

    public void OnGet()
    {
        CreditCard = new CreditCardModel(); // Initialize for GET
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }





        await _context.SaveChangesAsync();

        TempData["Success"] = "Medlemskapstypen har ändrats.";
        return RedirectToPage("Index");
    }
}
