using BCITGO_V6.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.AccountHome
{
    public class UserHomeModel : BasePageModel //added
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserHomeModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
            : base(context) //added
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

            var identityId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (appUser != null)
            {
                var unreadCount = _context.Notification
                    .Where(n => n.UserId == appUser.UserId && !n.IsRead)
                    .Count();

                ViewData["UnreadCount"] = unreadCount;
            }

            LoadUnreadCount(); // under onget added
            return Page();
        }
    }
}

