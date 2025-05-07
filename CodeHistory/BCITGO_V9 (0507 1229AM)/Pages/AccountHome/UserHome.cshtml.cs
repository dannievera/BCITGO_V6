using BCITGO_V6.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.AccountHome
{
    public class UserHomeModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserHomeModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string FullName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var appUser = _context.User.FirstOrDefault(u => u.IdentityUserId == identityUser.Id);
            if (appUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            FullName = appUser.FullName;

            return Page();
        }
    }
}

