using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.Account
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
                Message = "Your email has been verified successfully!";
            }
            else
            {
                Message = "Email verification failed.";
            }

            return Page();
        }
    }
}
