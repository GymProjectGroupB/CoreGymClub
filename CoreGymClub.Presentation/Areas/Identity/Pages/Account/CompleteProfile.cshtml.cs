using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreGymClub.Presentation.Models;

namespace CoreGymClub.Presentation.Areas.Identity.Pages.Account;

[Authorize]
public class CompleteProfileModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public CompleteProfileModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [BindProperty]
    public CompleteProfileViewModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login");

        var member = _context.Members.FirstOrDefault(m => m.UserId == user.Id);
        if (member != null)
            return RedirectToPage("/Account/Profile");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToPage("/Account/Login");

        var member = new Member
        {
            UserId = user.Id,
            FirstName = Input.FirstName,
            LastName = Input.LastName,
            BirthDate = Input.BirthDate,
            Street = Input.Street,
            City = Input.City,
            PostalCode = Input.PostalCode
        };

        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Account/Profile");
    }
}
