using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using BCITGO_V6.Data;  // <-- Change namespace to your DB Context

namespace BCITGO_V6.Pages.Profile
{
    public class EditProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IFormFile? ProfilePicture { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Full Name must be between 2 and 50 characters.")]
        public string FullName { get; set; }

        [BindProperty]
        [RegularExpression(@"^(\d{10})?$", ErrorMessage = "Phone number must be 10 digits.")]
        public string? PhoneNumber { get; set; }

        [BindProperty]
        [StringLength(300, ErrorMessage = "About Me can be max 300 characters.")]
        public string? AboutMe { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string ProfilePicturePath { get; set; }

        public void OnGet()
        {
            var appUser = _context.User
                .Where(u => u.IdentityUserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value)
                .FirstOrDefault();

            if (appUser != null)
            {
                FullName = appUser.FullName;
                PhoneNumber = appUser.PhoneNumber;
                AboutMe = appUser.Bio;
                ProfilePicturePath = appUser.ProfilePicture;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please complete all required fields.";
                return Page();
            }

            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            if (user == null)
            {
                ErrorMessage = "User not found.";
                return Page();
            }

            // Update FullName (Required so always update)
            user.FullName = FullName;

            // Update PhoneNumber → If blank keep existing
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                // Keep existing
            }
            else
            {
                user.PhoneNumber = PhoneNumber;
            }

            // Update AboutMe → If blank keep existing
            if (string.IsNullOrWhiteSpace(AboutMe))
            {
                // Keep existing
            }
            else
            {
                user.Bio = AboutMe;
            }

            // Update ProfilePicture → If blank keep existing
            if (ProfilePicture != null)
            {
                var fileName = Path.GetFileName(ProfilePicture.FileName);
                var savePath = Path.Combine("wwwroot/uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePicture = "/uploads/" + fileName;
                ProfilePicturePath = user.ProfilePicture;
            }
            else
            {
                ProfilePicturePath = user.ProfilePicture;
            }

            await _context.SaveChangesAsync();

            SuccessMessage = "Your profile has been updated successfully.";
            return Page();
        }
    }
}
