using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BCITGO_V6.Pages.Register
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public ResetPasswordInputModel Input { get; set; } = new ResetPasswordInputModel();

        public IActionResult OnGet(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["Error"] = "Invalid password reset link.";
                return RedirectToPage("/Register/Login");
            }

            Input.UserId = userId;
            Input.Token = token;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.UserId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToPage("/Register/Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Token, Input.Password);
            if (result.Succeeded)
            {
                TempData["Success"] = "Your password has been reset successfully! Please login.";
                return RedirectToPage("/Register/Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        public class ResetPasswordInputModel
        {
            [Required]
            public string UserId { get; set; }

            [Required]
            public string Token { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
