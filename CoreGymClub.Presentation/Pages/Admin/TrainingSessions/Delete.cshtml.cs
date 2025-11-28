using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreGymClub.Presentation.Pages.Admin.TrainingSessions
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrainingSession TrainingSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TrainingSession = await _context.TrainingSessions.FindAsync(id);

            if (TrainingSession == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var session = await _context.TrainingSessions.FindAsync(TrainingSession.Id);

            if (session == null)
                return NotFound();

            _context.TrainingSessions.Remove(session);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Träningspasset har tagits bort.";
            return RedirectToPage("/Admin/TrainingSessions/Index");
        }
    }
}
