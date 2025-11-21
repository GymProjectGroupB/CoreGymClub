using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ProfileViewModel ViewModel { get; set; } = new();

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return;

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == user.Id);

            if (member == null)
            {
                Response.Redirect("/Identity/Account/CompleteProfile");
                return;
            }

            ViewModel.FirstName = member.FirstName;
            ViewModel.LastName = member.LastName;
            ViewModel.BirthDate = member.BirthDate;
            ViewModel.Street = member.Street;
            ViewModel.City = member.City;
            ViewModel.PostalCode = member.PostalCode;

            ViewModel.Email = user.Email ?? "";
            ViewModel.PhoneNumber = user.PhoneNumber ?? "";

            ViewModel.UpcomingBookings = await _context.Bookings
                .Where(b => b.UserId == user.Id)
                .Include(b => b.TrainingSession)
                .Where(b => b.TrainingSession.DateTimeStart > DateTime.Now)
                .OrderBy(b => b.TrainingSession.DateTimeStart)
                .Select(b => new UpcomingBookingItem
                {
                    TrainingSessionId = b.TrainingSessionId,
                    Title = b.TrainingSession.Title,
                    Start = b.TrainingSession.DateTimeStart,
                    Location = b.TrainingSession.Location
                })
                .ToListAsync();
        }
    }
}
