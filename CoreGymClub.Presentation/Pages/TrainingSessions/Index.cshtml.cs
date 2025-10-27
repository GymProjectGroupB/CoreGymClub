using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreGymClub.Presentation.Pages.TrainingSessions
{
    public class IndexModel : PageModel
    {
        public IReadOnlyList<TrainingSessionViewModel> Sessions { get; private set; } = [];

        public void OnGet()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Sample data for demonstration purposes
            Sessions = new List<TrainingSessionViewModel>
            {
                 new(1, "Spin 45", today.AddDays(1), new(6,30), new(7,15), "Studio A", "Maja"),
                 new(2, "Yoga Flow", today.AddDays(1), new(9,0), new(10,0), "Studio B", "Jonas"),
                 new(3, "HIIT Express", today.AddDays(2), new(7,0), new(7,30), "Studio A", "Sara"),
            }
            .ToList();
        }
    }
}
