using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Linq;

namespace BCITGO_V6.Pages.Book
{
    public class ViewDriverRidePostingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewDriverRidePostingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ride Ride { get; set; }

        public string DriverProfilePictureUrl { get; set; } = "/images/default-profile.png"; // Default picture

        public void OnGet(int id)
        {
            // Load ride with driver info and bookings
            Ride = _context.Ride
                .Include(r => r.User)
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RideId == id && r.Status == "Active");

            if (Ride == null)
            {
                // Ride not found or not active → redirect or show not found
                RedirectToPage("/Rides/AvailableRides");
                return;
            }

            // Calculate booked seats (exclude cancelled bookings)
            Ride.BookedSeats = Ride.Bookings.Where(b => b.Status != "Cancelled").Sum(b => b.SeatsBooked);

            // Optional → Load Driver profile picture (for now use default)
            // If in future you have User.ProfilePicture → you can use that
            if (!string.IsNullOrEmpty(Ride.User.ProfilePicture))
            {
                DriverProfilePictureUrl = Ride.User.ProfilePicture;
            }
            else
            {
                DriverProfilePictureUrl = "/images/default-profile.png";
            }

        }
    }
}
