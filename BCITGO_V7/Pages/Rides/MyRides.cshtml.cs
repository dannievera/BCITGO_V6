using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            }
            else
            {
                UserRides = new List<Ride>();
            }
        }
    }
}
