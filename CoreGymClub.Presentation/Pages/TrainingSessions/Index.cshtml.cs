using CoreGymClub.Presentation.Services;
using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CoreGymClub.Presentation.Pages.TrainingSessions
{
    public class IndexModel : PageModel
    {
        private readonly ITrainingSessionService _trainingService;
        private readonly IBookingService _bookingService;

        public IndexModel(ITrainingSessionService trainingService, IBookingService bookingService)
        {
            _trainingService = trainingService;
            _bookingService = bookingService;
        }

        public IReadOnlyList<TrainingSessionViewModel> Sessions { get; private set; } = [];

        public IReadOnlyList<TrainingSessionViewModel> MyBookedSessions { get; private set; } = [];

        public async Task OnGetAsync()
        {
            var sessions = await _trainingService.UpcomingAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MyBookedSessions = sessions
                .Where(s => s.Bookings.Any(b => b.UserId == userId))
                .Select(s => new TrainingSessionViewModel(
                    s.Id,
                    s.Title,
                    s.DateTimeStart,
                    s.DateTimeEnd,
                    s.Location,
                    s.Instructor,
                    s.Capacity,
                    s.Capacity - s.Bookings.Count,
                    s.Bookings.Count >= s.Capacity,
                    true
                ))
                .ToList();

            Sessions = sessions
                .Select(s => new TrainingSessionViewModel(
                    s.Id,
                    s.Title,
                    s.DateTimeStart,
                    s.DateTimeEnd,
                    s.Location,
                    s.Instructor,
                    s.Capacity,
                    s.Capacity - s.Bookings.Count,
                    s.Bookings.Count >= s.Capacity,
                    s.Bookings.Any(b => b.UserId == userId)
                ))
                .ToList();
        }

        public async Task<IActionResult> OnPostBookAsync(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;

            var (ok, msg) = await _bookingService.BookAsync(userId, id);

            if (!ok) TempData["Error"] = msg;
            else TempData["Success"] = msg;

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnbookAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (ok, msg) = await _bookingService.UnbookAsync(userId!, id);

            TempData[ok ? "Success" : "Error"] = msg;
            return RedirectToPage();
        }

    }
}
