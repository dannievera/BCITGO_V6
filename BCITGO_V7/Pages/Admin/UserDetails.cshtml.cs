using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;

namespace BCITGO_V6.Pages.Admin
{
    public class UserDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get; set; } = new();
        public List<Ride> UserRides { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            // Convert id to integer if UserId is an int
            var userId = int.Parse(id);  // Assuming id is an int, convert it

            // Fetch user by IdentityUserId (linked to AspNetUsers)
            User = await _context.User
                .FirstOrDefaultAsync(u => u.UserId == userId);  // Correct comparison with int

            if (User == null)
                return NotFound();

            // Fetch the user's rides using their UserId
            UserRides = await _context.Ride
                .Where(r => r.UserId == int.Parse(id))  // Convert id to int to match UserId type
                .ToListAsync();


            return Page();
        }


    }
}
