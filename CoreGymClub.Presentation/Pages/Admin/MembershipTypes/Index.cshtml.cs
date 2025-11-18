using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.MembershipTypes
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MembershipType> Memberships { get; set; } = new List<MembershipType>();

        public async Task OnGetAsync()
        {
            Memberships = await _context.MembershipTypes
                .OrderBy(m => m.Name)
                .ToListAsync();
        }
    }
}