using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.MembershipTypes
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MembershipType MembershipType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _context.MembershipTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
                return NotFound();

            MembershipType = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var item = await _context.MembershipTypes.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.MembershipTypes.Remove(item);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Medlemskapstypen har tagits bort.";
            return RedirectToPage("Index");
        }
    }
}