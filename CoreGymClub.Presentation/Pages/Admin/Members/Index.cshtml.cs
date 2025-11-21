using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.ViewModels;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.Members
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Listan vi skickar till vyn
        public List<AdminMemberListItemViewModel> Members { get; set; } = new();

        public async Task OnGet()
        {
            var users = _userManager.Users.OrderBy(u => u.Email).ToList();

            var members = await _context.Members
                .Include(m => m.MembershipType)
                .ToListAsync();

            foreach (var user in users)
            {
                var memberProfile = members.FirstOrDefault(m => m.UserId == user.Id);

                Members.Add(new AdminMemberListItemViewModel
                {
                    UserId = user.Id,
                    Email = user.Email ?? "",
                    PhoneNumber = user.PhoneNumber,

                    FirstName = memberProfile?.FirstName ?? "-",
                    LastName = memberProfile?.LastName ?? "-",

                    MembershipType = memberProfile?.MembershipType?.Name ?? "Inget",
                    MembershipStart = memberProfile?.MembershipStart,
                    MembershipEnd = memberProfile?.MembershipEnd
                });
            }
        }
    }
}