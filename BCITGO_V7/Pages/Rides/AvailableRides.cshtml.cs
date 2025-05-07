using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Collections.Generic;
using System.Linq;

namespace BCITGO_V6.Pages.Rides
{
    public class AvailableRidesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AvailableRidesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ride> Rides { get; set; }

        public void OnGet()
        {
            // Get all active rides, ordered by soonest
            Rides = _context.Ride
                .Where(r => r.Status != "Deleted" && r.DepartureDate >= System.DateTime.Today)
                .ToList() // ✅ Fetch first before ordering by TimeSpan (DepartureTime)
                .OrderBy(r => r.DepartureDate)
                .ThenBy(r => r.DepartureTime)
                .ToList();
        }
    }
}
