using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.TrainingSessions
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TrainingSession> Sessions { get; set; } = new List<TrainingSession>();

        public async Task OnGetAsync()
        {
            Sessions = await _context.TrainingSessions
                .OrderBy(s => s.DateTimeStart)
                .ToListAsync();
        }
    }
}
