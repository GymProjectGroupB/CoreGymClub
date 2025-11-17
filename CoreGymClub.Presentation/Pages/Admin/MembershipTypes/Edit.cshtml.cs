using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.MembershipTypes
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MembershipType MembershipType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var membership = await _context.MembershipTypes.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }

            MembershipType = membership;
            return Page();
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

            _context.Attach(MembershipType).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Medlemskapstypen har uppdaterats.";
            return RedirectToPage("Index");
        }
    }
}