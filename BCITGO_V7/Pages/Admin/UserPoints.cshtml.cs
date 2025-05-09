//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using BCITGO_V6.Data;
//using BCITGO_V6.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BCITGO_V6.Pages.Admin
//{
//    public class UserPointsModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;

//        public UserPointsModel(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public List<User> Users { get; set; } = new();

//        public async Task<IActionResult> OnGetAsync()
//        {
//            Users = await _context.User.ToListAsync();
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync(string action, string id)
//        {
//            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId.ToString() == id);
//            if (user == null)
//                return NotFound();

//            if (action.StartsWith("edit_"))
//            {
//                // Logic for editing points
//                // You can show a modal or take the user to another page to edit points
//            }
//            else if (action.StartsWith("delete_"))
//            {
//                // Logic for deleting points (e.g., reset to 0)
//                user.Points = 0;
//                _context.Update(user);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToPage("./UserPoints");
//        }
//    }
//}
