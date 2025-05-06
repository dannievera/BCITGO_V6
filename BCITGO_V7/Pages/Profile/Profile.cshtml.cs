using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace BCITGO_V6.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActiveAt { get; set; }
        public string ProfilePicture { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var appUser = _context.User.FirstOrDefault(u => u.IdentityUserId == identityUser.Id);
            if (appUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            FullName = appUser.FullName;
            Email = appUser.Email;
            Role = appUser.Role;
            Status = appUser.Status;
            CreatedAt = appUser.CreatedAt;
            LastActiveAt = appUser.LastActiveAt;

            return Page();
        }
    }
}
