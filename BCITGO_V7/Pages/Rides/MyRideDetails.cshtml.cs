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
    public class MyRideDetailsModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public MyRideDetailsModel(ApplicationDbContext context)
            : base(context) // Call the base constructor
        {
            _context = context;
        }

        public Ride Ride { get; set; }
        public List<Booking> Bookings { get; set; }

        public IActionResult OnGet(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);
                if (user != null)
                {
                    ViewData["UnreadCount"] = _context.Notification
                        .Where(n => n.UserId == user.UserId && !n.IsRead)
                        .Count();

                    Ride = _context.Ride
                        .Include(r => r.Bookings)
                        .FirstOrDefault(r => r.RideId == id && r.UserId == user.UserId);

                    // Automatically update status to Completed or Expired
                    var rideDateTime = Ride.DepartureDate.Date + Ride.DepartureTime;
                    if (rideDateTime <= DateTime.Now && Ride.Status == "Active")
                    {
                        bool wasBooked = _context.Booking
                            .Any(b => b.RideId == Ride.RideId && b.Status == "Confirmed");

                        Ride.Status = wasBooked ? "Completed" : "Expired";
                        _context.Update(Ride);


                        if (wasBooked)
                        {
                            var confirmedBookings = _context.Booking
                                .Where(b => b.RideId == Ride.RideId && b.Status == "Confirmed")
                                .ToList();

                            foreach (var b in confirmedBookings)
                            {
                                b.Status = "Completed";
                            }

                            _context.UpdateRange(confirmedBookings);
                        }

                        
                        _context.SaveChanges();
                    }


                    if (Ride == null)
                    {
                        return RedirectToPage("/Rides/MyRides");
                    }

                    Bookings = _context.Booking
                        .Include(b => b.User)
                        .Where(b => b.RideId == id)
                        .OrderBy(b => b.CreatedAt)
                        .ToList();

                    LoadUnreadCount();
                    return Page();
                }
            }

            return RedirectToPage("/Rides/MyRides"); // fallback if user is null
        }


        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            var booking = _context.Booking
                .Include(b => b.Ride)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking != null && booking.Status == "Pending")
            {
                booking.Status = "Confirmed";

                // ✅ Notify the passenger
                var note = new Notification
                {
                    UserId = booking.UserId,
                    Message = $"Your seat request for the ride from {booking.Ride.StartLocation} to {booking.Ride.EndLocation} has been confirmed.",
                    CreatedAt = DateTime.Now
                };
                _context.Notification.Add(note);

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
                booking.Status = "Declined";

                // ✅ Notify the passenger
                var note = new Notification
                {
                    UserId = booking.UserId,
                    Message = $"Your booking request for the ride from {booking.Ride.StartLocation} to {booking.Ride.EndLocation} was declined.",
                    CreatedAt = DateTime.Now
                };
                _context.Notification.Add(note);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { id = booking.RideId });
        }



    }
}
