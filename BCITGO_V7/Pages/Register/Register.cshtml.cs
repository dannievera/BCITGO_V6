using BCITGO_V6.Data;
using BCITGO_V6.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Pages.Register
{
    public class RegisterModel : PageModel
    {

        // UserManager handles user creation (ASP.NET Identity) > for saving new user in AspNetUsers table - dvb
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        // Addedd for constructor injection to pass usermanager service to the page model - dvb 0505
        public RegisterModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context; 
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

            // Create Identity User
            var user = new IdentityUser
            {
                UserName = Input.Email,
                Email = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {

                //Create app user (USERS table)
                var appUser = new User
                {
                    IdentityUserId = user.Id, // Link to AspNetUsers.Id
                    FullName = Input.FullName,
                    Email = Input.Email,
                    Role = "User", // Default role
                    Status = "Inactive", // Not yet verified
                    CreatedAt = DateTime.Now,
                    LastActiveAt = DateTime.Now
                };

                _context.User.Add(appUser);
                await _context.SaveChangesAsync();

                // Generate email verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var verificationLink = Url.Page(
                    "/Register/VerifyEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, token = token },
                    protocol: Request.Scheme);

                // Send verification email
                var emailSender = new Helpers.EmailSender();
                await emailSender.SendEmailAsync(user.Email, "Verify your BCITGO Account", $"Please verify your account by clicking here: {verificationLink}");

                // Save VerificationUserId to session
                HttpContext.Session.SetString("VerificationUserId", user.Id);

                // Redirect to Verify page with success message
                TempData["Success"] = "Account registered successfully! Please check your email to verify your account.";
                return RedirectToPage("/Register/Verify");
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

        //for database mapping below -dvb
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
