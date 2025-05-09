using BCITGO_V6.Data;
using BCITGO_V6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.Rides
{
    public class EditRideModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public EditRideModel(ApplicationDbContext context)
            : base(context) // Call the base constructor
        {
            _context = context;
        }

        [BindProperty]
        public Ride Ride { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsEditBlocked => TempData["EditBlocked"] != null;


        public IActionResult OnGet(int id)
        {
            Ride = _context.Ride.FirstOrDefault(r => r.RideId == id);

            if (Ride == null)
            {
                return RedirectToPage("/Rides/PostRide");
            }

            
            int totalBooked = _context.Booking
                .Where(b => b.RideId == Ride.RideId && (b.Status == "Confirmed" || b.Status == "Pending"))
                .Sum(b => (int?)b.SeatsBooked) ?? 0;

            if (totalBooked > 0)
            {
                TempData["EditBlocked"] = "This ride already has booked passengers. Editing is disabled.";
            }

            LoadUnreadCount(); // under onget added

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string action)
        {
            var ride = _context.Ride.FirstOrDefault(r => r.RideId == Ride.RideId);

            int totalBooked = _context.Booking
                .Where(b => b.RideId == Ride.RideId && (b.Status == "Confirmed" || b.Status == "Pending"))
                .Sum(b => (int?)b.SeatsBooked) ?? 0;

            if (totalBooked > 0)
            {
                TempData["EditBlocked"] = "This ride already has booked passengers. Editing is disabled.";
            }


            if (ride == null)
            {
                ErrorMessage = "Ride not found.";
                return Page();
            }

            if (action == "delete")
            {
                var hasBookings = _context.Booking
                    .Any(b => b.RideId == ride.RideId && b.Status == "Confirmed");

                if (hasBookings)
                {
                    TempData["DeleteWarning"] = "Warning: This ride has confirmed passengers. Deleting will cancel all bookings.";
                }

                // Mark as deleted
                ride.Status = "Deleted";

                // Cancel bookings
                var bookings = _context.Booking.Where(b => b.RideId == ride.RideId && b.Status != "Cancelled");
                foreach (var booking in bookings)
                {
                    booking.Status = "Cancelled";
                }

                await _context.SaveChangesAsync();

                SuccessMessage = "Ride deleted successfully. Affected passengers have been notified.";
                return RedirectToPage("/Rides/MyRides");
            }


            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please complete all required fields.";
                return Page();
            }

            ride.StartLocation = Ride.StartLocation;
            ride.EndLocation = Ride.EndLocation;
            ride.DepartureDate = Ride.DepartureDate;
            ride.DepartureTime = Ride.DepartureTime;
            ride.PricePerSeat = Ride.PricePerSeat;
            ride.TotalSeats = Ride.TotalSeats;
            ride.Notes = Ride.Notes;
            ride.LuggageAllowed = Ride.LuggageAllowed;
            ride.PetsAllowed = Ride.PetsAllowed;

            await _context.SaveChangesAsync();


            SuccessMessage = "Update Successful! You can now manage your ride in 'My Rides.'";
            return Page();
        }
    }
}
