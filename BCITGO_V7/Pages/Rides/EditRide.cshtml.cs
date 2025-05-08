using BCITGO_V6.Data;
using BCITGO_V6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.Rides
{
    public class EditRideModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditRideModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ride Ride { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnGet(int id)
        {
            Ride = _context.Ride.FirstOrDefault(r => r.RideId == id);

            if (Ride == null)
            {
                return RedirectToPage("/Rides/PostRide");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            var ride = _context.Ride.FirstOrDefault(r => r.RideId == Ride.RideId);

            if (ride == null)
            {
                ErrorMessage = "Ride not found.";
                return Page();
            }

            if (action == "delete")
            {
                ride.Status = "Deleted";
                await _context.SaveChangesAsync();
                SuccessMessage = "Posting has been deleted! You can now manage your ride in 'My Rides.'";
                return RedirectToPage("/Rides/PostRide");
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
