using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Pages.Account
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new RegisterInputModel();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Dummy Save logic for now > ready for future DB save
            TempData["Success"] = "Account registered successfully (simulation).";

            return RedirectToPage("/Index");
        }

        public class RegisterInputModel
        {
            [Required]
            [StringLength(50)]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            [RegularExpression(@"^[^@\s]+@my\.bcit\.ca$", ErrorMessage = "Must be a @my.bcit.ca email.")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }
    }
}
