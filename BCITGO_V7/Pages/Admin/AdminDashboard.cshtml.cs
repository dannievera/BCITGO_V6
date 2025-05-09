using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using BCITGO_V6.Data;
using Microsoft.EntityFrameworkCore;

namespace BCITGO_V6.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalUsers { get; set; }
        public int TotalRides { get; set; }
        public int TotalSeats { get; set; }
        public int OpenReports { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Optional double check
            if (!User.IsInRole("Admin"))
                return RedirectToPage("/Register/Login");

            TotalUsers = await _context.Users.CountAsync();
            TotalRides = await _context.Ride.CountAsync();
            TotalSeats = await _context.Booking.SumAsync(b => b.SeatsBooked);
            OpenReports = await _context.SupportTicket.CountAsync(t => t.Status == "Open"); 


            return Page();
        }
    }
}
