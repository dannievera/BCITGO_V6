using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BCITGO_V6.Pages.Book
{
    public class MyBookingsModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public MyBookingsModel(ApplicationDbContext context)
            : base(context) // Call the base constructor
        {
            _context = context;
        }

        public List<Booking> Bookings { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SuccessMessage { get; set; }

        public void OnGet(string success = null)
        {
            if (!string.IsNullOrEmpty(success))
            {
                SuccessMessage = success;
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            Bookings = _context.Booking
                .Include(b => b.Ride).ThenInclude(r => r.User)
                .Where(b => b.UserId == user.UserId)
                .OrderByDescending(b => b.CreatedAt)
                .ToList();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var booking = _context.Booking
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null || booking.Status == "Cancelled" || booking.Status == "Declined")
            {
                return RedirectToPage();
            }

            booking.Status = "Cancelled";

            await _context.SaveChangesAsync();

            LoadUnreadCount(); // under onget added

            return RedirectToPage(new { success = "Your booking has been cancelled." });
        }


    }
}
