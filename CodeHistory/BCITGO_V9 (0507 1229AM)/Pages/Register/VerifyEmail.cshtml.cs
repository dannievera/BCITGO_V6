using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.Register
{
    public class VerifyEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public VerifyEmailModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            if (userId == null || token == null)
            {
                Message = "Invalid verification link.";
                return Page();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Message = "User not found.";
                return Page();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {

                using (var scope = HttpContext.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<BCITGO_V6.Data.ApplicationDbContext>();
                    var appUser = dbContext.User.FirstOrDefault(u => u.Email == user.Email);
                    if (appUser != null)
                    {
                        appUser.Status = "Active";
                        dbContext.SaveChanges();
                    }
                }

                TempData["Success"] = "Your email has been verified successfully! Login to access your account.";

            }
            else
            {
                TempData["Error"] = "Email verification failed.";
            }

            return RedirectToPage("/Register/Login");
        }
    }
}
