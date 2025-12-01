using CoreGymClub.Presentation.Data;
using CoreGymClub.Presentation.Models;
using CoreGymClub.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CoreGymClub.Presentation.Pages;

[BindProperties]
[Authorize(Roles = "Member")]
public class JoinMembershipModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public JoinMembershipModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public List<SelectListItem> MembershipTypes { get; set; }
    public int MembershipTypeId { get; set; }
    public DateTime MembershipStart { get; set; }
    public DateTime MembershipEnd { get; set; }
    public class CreditCardModel
    {
        [Required]
        [Display(Name = "Card Number")]
        [CreditCard] // Built-in validation for credit card format (requires jQuery validation)
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Month/Year")]
        public string Expiration { get; set; } // e.g., "MM/YY"

        [Required]
        [Display(Name = "CVV")]
        [StringLength(4, MinimumLength = 3)] // Usually 3 or 4 digits
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }
    }

    public CreditCardModel CreditCard { get; set; }

    public async Task OnGetAsync()
    {
        await FillMembershipTypeList();

        CreditCard = new CreditCardModel(); // Initialize for GET

        ClaimsPrincipal currentUser = this.User;
        var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Identity-user
        var user = await _userManager.FindByIdAsync(currentUserID);
        if (user == null)
        {
            ModelState.AddModelError("", "Kunde inte hitta användaren.");
            return;
        }
    }

    private async Task FillMembershipTypeList()
    {
        MembershipTypes = new List<SelectListItem>();

        MembershipTypes.Add(new SelectListItem
        {
            Text = "Välj",
            Value = "0"
        });

        var membershipTypesList =  await _context.MembershipTypes
            .Where(mt => mt.IsActive)
            .OrderBy(mt => mt.Name)
            .Select(mt => new SelectListItem
            {
                Text = $"{mt.Name} - {mt.PricePerMonth:C} per månad",
                Value = mt.Id.ToString()
            })
            .ToListAsync();

        foreach (var mt in membershipTypesList)
        {
            MembershipTypes.Add(mt);
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await FillMembershipTypeList();

        if (MembershipTypeId == 0)
        {
            ModelState.AddModelError("MembershipTypeId", "Vänligen välj en medlemskapstyp.");
            return Page();
        }
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CreditCard = new CreditCardModel(); // Initialize for GET

        ClaimsPrincipal currentUser = this.User;
        var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Identity-user
        var user = await _userManager.FindByIdAsync(currentUserID);
        if (user == null)
        {
            ModelState.AddModelError("", "Kunde inte hitta användaren.");
            return Page();
        }

        // Hämta Member-profil
        var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == currentUserID);
        if (member == null)
        {
            ModelState.AddModelError("", "Medlemsprofilen kunde inte hittas.");
            return Page();
        }

        // Uppdatera medlemskap
        member.MembershipTypeId = MembershipTypeId;
        member.MembershipStart = MembershipStart;
        member.MembershipEnd = MembershipEnd;

        await _context.SaveChangesAsync();

        TempData["Success"] = "Grattis! Nu är du medlem!";
        return RedirectToPage("Index");
    }
}
