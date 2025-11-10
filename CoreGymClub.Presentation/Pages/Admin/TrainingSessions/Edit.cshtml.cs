using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.TrainingSessions
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
        public TrainingSession TrainingSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TrainingSession = await _context.TrainingSessions.FindAsync(id);

            if (TrainingSession == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(TrainingSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Träningspasset har uppdaterats!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TrainingSessions.Any(e => e.Id == TrainingSession.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("/Admin/TrainingSessions/Index");
        }
    }
}
