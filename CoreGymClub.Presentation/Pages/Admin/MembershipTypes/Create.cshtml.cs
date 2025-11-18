using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreGymClub.Presentation.Pages.Admin.MembershipTypes
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MembershipType MembershipType { get; set; } = new MembershipType();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (MembershipType.PricePerMonth < 0)
            {
                ModelState.AddModelError("MembershipType.PricePerMonth", "Pris kan inte vara negativt.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MembershipTypes.Add(MembershipType);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Medlemskapstypen har skapats.";
            return RedirectToPage("Index");
        }
    }
}