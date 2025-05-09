using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;

namespace BCITGO_V6.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> Users { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        // Handle POST requests for Suspend, Ban, and Delete actions
        public async Task<IActionResult> OnPostAsync(string action, string id)
        {
            // Parse the id to an integer
            if (!int.TryParse(id, out var userId))
            {
                return NotFound();  // If id is not a valid integer, return NotFound
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return NotFound();

            // Suspend User
            if (action.StartsWith("suspend_"))
            {
                user.Status = "Suspended";
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            // Ban User
            else if (action.StartsWith("ban_"))
            {
                user.Status = "Banned";
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            // Delete User (Soft-Delete)
            else if (action.StartsWith("delete_"))
            {
                user.Status = "Deleted";
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            // Reload the users list
            return RedirectToPage("./Users");
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var query = _context.User.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u =>
                    u.FullName.Contains(SearchTerm) ||
                    u.Email.Contains(SearchTerm));
            }

            Users = await query.OrderBy(u => u.UserId).ToListAsync();
            return Page();
        }
    }
}
