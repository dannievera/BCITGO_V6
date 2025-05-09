using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BCITGO_V6.Pages.Profile
{
    public class NotificationsModel : BasePageModel
    {
        public NotificationsModel(ApplicationDbContext context) : base(context) { }

        public List<Notification> UserNotifications { get; set; } = new List<Notification>();

        public void OnGet()
        {
            LoadUnreadCount(); //Load badge count (will be 0 after marking as read)

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            if (user != null)
            {
                UserNotifications = _context.Notification
                    .Where(n => n.UserId == user.UserId)
                    .OrderByDescending(n => n.CreatedAt)
                    .ToList();

                // Mark all unread notifications as read
                foreach (var note in UserNotifications.Where(n => !n.IsRead))
                {
                    note.IsRead = true;
                }

                _context.SaveChanges(); //  Persist the "read" status
                ViewData["UnreadCount"] = 0; //  Hide red badge after open
            }
        }
    }
}
