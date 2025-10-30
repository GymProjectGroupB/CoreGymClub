using CoreGymClub.Presentation.Services;
using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreGymClub.Presentation.Pages.TrainingSessions
{
    public class IndexModel : PageModel
    {
        private readonly ITrainingSessionService _trainingService;

        public IndexModel(ITrainingSessionService trainingService)
        {
            _trainingService = trainingService;
        }

        public IReadOnlyList<TrainingSessionViewModel> Sessions { get; private set; } = [];

        public async Task OnGetAsync()
        {
            var sessions = await _trainingService.UpcomingAsync();

            Sessions = sessions
                .Select(s => new TrainingSessionViewModel(
                    s.Id,
                    s.Title,
                    s.DateTimeStart,
                    s.DateTimeEnd,
                    s.Location,
                    s.Instructor
                ))
                .ToList();
        }
    }
}
