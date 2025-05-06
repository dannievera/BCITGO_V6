using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Pages.Account
{
    public class RegisterModel : PageModel
    {

        // UserManager handles user creation (ASP.NET Identity) > for saving new user in AspNetUsers table - dvb
        private readonly UserManager<IdentityUser> _userManager;

        // Addedd for constructor injection to pass usermanager service to the page model - dvb 0505
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        [BindProperty]
        public RegisterInputModel Input { get; set; } = new RegisterInputModel();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()

        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser
            {
                UserName = Input.Email,
                Email = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // Generate email verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var verificationLink = Url.Page(
                    "/Account/VerifyEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, token = token },
                    protocol: Request.Scheme);

                // Send email
                var emailSender = new Helpers.EmailSender();
                await emailSender.SendEmailAsync(user.Email, "Verify your BCITGO Account", $"Please verify your account by clicking here: {verificationLink}");

                HttpContext.Session.SetString("VerificationUserId", user.Id);



                TempData["Success"] = "Account registered successfully! Please check your email to verify your account.";
                return RedirectToPage("/Account/Verify");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

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
