using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace BCITGO_V6.Pages.Rides
{
    public class MyRideDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MyRideDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ride Ride { get; set; }
        public List<Booking> Bookings { get; set; }

        public IActionResult OnGet(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            Ride = _context.Ride
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RideId == id && r.UserId == user.UserId);

            if (Ride == null)
            {
                return RedirectToPage("/Rides/MyRides");
            }

            // Load bookings with user info
            Bookings = _context.Booking
                .Include(b => b.User)
                .Where(b => b.RideId == id)
                .OrderBy(b => b.CreatedAt)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            var booking = _context.Booking.FirstOrDefault(b => b.BookingId == id);

            if (booking != null && booking.Status == "Pending")
            {
                booking.Status = "Confirmed";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = booking.RideId });
        }

        public async Task<IActionResult> OnPostDeclineAsync(int id)
        {
            var booking = _context.Booking
                .Include(b => b.Ride)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking != null && booking.Status == "Pending")
            {
                booking.Status = "Declined"; // Just Decline, no need to adjust seats anymore
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = booking.RideId });
        }



    }
}
