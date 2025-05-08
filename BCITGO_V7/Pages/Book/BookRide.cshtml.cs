using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BCITGO_V6.Pages.Book
{
    public class BookRideModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookRideModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ride Ride { get; set; }

        [BindProperty]
        public int SeatsToBook { get; set; }

        [BindProperty]
        public string BookingMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            Ride = _context.Ride
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RideId == id && r.Status == "Active");

            if (Ride == null)
                return RedirectToPage("/Rides/AvailableRides");

            // Calculate Confirmed seats
            Ride.BookedSeats = Ride.Bookings.Where(b => b.Status == "Confirmed").Sum(b => b.SeatsBooked);

            // Calculate Pending requests
            Ride.PendingRequests = Ride.Bookings.Where(b => b.Status == "Pending").Sum(b => b.SeatsBooked);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ride = _context.Ride
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RideId == id && r.Status == "Active");

            if (ride == null)
                return RedirectToPage("/Rides/AvailableRides");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == userId);

            // Validate seats
            var confirmedSeats = ride.Bookings.Where(b => b.Status == "Confirmed").Sum(b => b.SeatsBooked);
            var pendingSeats = ride.Bookings.Where(b => b.Status == "Pending").Sum(b => b.SeatsBooked);
            var totalReservedSeats = confirmedSeats + pendingSeats;

            if (SeatsToBook < 1 || SeatsToBook > (ride.TotalSeats - totalReservedSeats))

            {
                ModelState.AddModelError("", "Invalid number of seats selected.");
                Ride = ride;
                return Page();
            }

            // Create booking
            var booking = new Booking
            {
                RideId = id,
                UserId = user.UserId,
                SeatsBooked = SeatsToBook,
                BookingMessage = BookingMessage,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            // Redirect to MyBookings with success message
            return RedirectToPage("/Book/MyBookings", new { success = "You have now successfully booked this ride." });
        }
    }
}
