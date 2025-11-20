using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreGymClub.Presentation.Pages.Admin.Members
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public EditModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public EditMemberViewModel Member { get; set; } = new EditMemberViewModel();

        public List<MembershipType> MembershipTypes { get; set; } = new();

        // ?? OBS: INGEN IActionResult – annars får du 404
        public async Task OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("", "Id saknas.");
                return;
            }

            // Identity-user
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "Kunde inte hitta användaren.");
                return;
            }

            // Hämta eller skapa Member-profil
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (member == null)
            {
                // Skapa en tom profil (för att undvika 404)
                member = new Member
                {
                    UserId = id,
                    FirstName = "",
                    LastName = "",
                    Street = "",
                    City = "",
                    PostalCode = ""
                };

                _context.Members.Add(member);
                await _context.SaveChangesAsync();
            }

            // Ladda medlemskapstyper
            MembershipTypes = await _context.MembershipTypes
                .Where(m => m.IsActive)
                .OrderBy(m => m.Name)
                .ToListAsync();

            // Mappa till ViewModel
            Member = new EditMemberViewModel
            {
                Id = user.Id,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",

                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Street = member.Street,
                City = member.City,
                PostalCode = member.PostalCode,

                MembershipTypeId = member.MembershipTypeId,
                MembershipStart = member.MembershipStart,
                MembershipEnd = member.MembershipEnd
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Ladda dropdown ALLTID
            MembershipTypes = await _context.MembershipTypes
                .Where(mt => mt.IsActive)
                .OrderBy(mt => mt.Name)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByIdAsync(Member.Id);
            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == Member.Id);

            member.MembershipTypeId = Member.MembershipTypeId;
            member.MembershipStart = Member.MembershipStart;
            member.MembershipEnd = Member.MembershipEnd;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}



