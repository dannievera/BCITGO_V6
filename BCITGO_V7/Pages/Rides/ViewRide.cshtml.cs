using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Linq;

namespace BCITGO_V6.Pages.Rides
{
    public class ViewRideModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewRideModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ride Ride { get; set; }

        public IActionResult OnGet(int id)
        {
            Ride = _context.Ride.FirstOrDefault(r => r.RideId == id && r.Status != "Deleted");

            if (Ride == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
