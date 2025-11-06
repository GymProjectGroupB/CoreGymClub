using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreGymClub.Presentation.Pages.Admin.TrainingSessions
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
        public TrainingSession TrainingSession { get; set; } = new();

        public void OnGet()
        {
            TrainingSession = new TrainingSession
            {
                DateTimeStart = DateTime.Now.AddHours(9),
                DateTimeEnd = DateTime.Now.AddHours(10)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (TrainingSession.DateTimeEnd <= TrainingSession.DateTimeStart)
            {
                ModelState.AddModelError("TrainingSession.DateTimeEnd", "Sluttiden måste vara efter starttiden.");
                return Page();
            }

            _context.TrainingSessions.Add(TrainingSession);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Träningspasset har skapats!";
            return RedirectToPage("Index");
        }
    }
}