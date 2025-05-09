// Pages/BasePageModel.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using System.Security.Claims;
using System.Linq;

namespace BCITGO_V6.Pages
{
    public class BasePageModel : PageModel
    {
        protected readonly ApplicationDbContext _context;

        public BasePageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        protected void LoadUnreadCount()
        {
            var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appUser = _context.User.FirstOrDefault(u => u.IdentityUserId == identityId);
            if (appUser != null)
            {
                ViewData["UnreadCount"] = _context.Notification
                    .Count(n => n.UserId == appUser.UserId && !n.IsRead);
            }
        }
    }
}
