using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BCITGO_V6.Pages.Rides
{
    public class MyRidesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MyRidesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ride> UserRides { get; set; }

        public void OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            if (user != null)
            {
                UserRides = _context.Ride
                    .Where(r => r.UserId == user.UserId && r.Status != "Deleted")
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();

                var now = DateTime.Now;
                bool hasExpired = false;

                foreach (var ride in UserRides)
                {
                    var rideDateTime = ride.DepartureDate.Date + ride.DepartureTime;

                    // Expire ride if past
                    if (rideDateTime <= now && ride.Status == "Active")
                    {
                        ride.Status = "Expired";
                        _context.Update(ride);
                        hasExpired = true;
                    }

                    // Count confirmed seats only
                    ride.BookedSeats = _context.Booking
                        .Where(b => b.RideId == ride.RideId && b.Status == "Confirmed")
                        .Sum(b => (int?)b.SeatsBooked) ?? 0;

                    // Count pending requests
                    ride.PendingRequests = _context.Booking
                        .Where(b => b.RideId == ride.RideId && b.Status == "Pending")
                        .Count();
                }

                // Only save if there was any expired ride
                if (hasExpired)
                {
                    _context.SaveChanges();
                }
            }
            else
            {
                UserRides = new List<Ride>();
            }
        }




        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var ride = _context.Ride
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RideId == id);

            if (ride == null)
            {
                return NotFound();
            }

            // Mark ride as deleted
            ride.Status = "Deleted";

            // Cancel all bookings linked to this ride
            foreach (var booking in ride.Bookings.Where(b => b.Status != "Cancelled"))
            {
                booking.Status = "Cancelled";
            }

            await _context.SaveChangesAsync();

            return RedirectToPage(); // refresh MyRides page after delete
        }


    }
}

