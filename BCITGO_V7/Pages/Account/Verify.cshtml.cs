using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.Account
{
    public class VerifyModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;

        public VerifyModel(UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Message { get; set; }

        public bool CanResend { get; set; }

        public void OnGet()
        {
            var lastSent = HttpContext.Session.GetString("LastVerificationEmailSent");
            if (lastSent == null)
            {
                CanResend = true;
            }
            else
            {
                var lastSentTime = DateTime.Parse(lastSent);
                CanResend = (DateTime.Now - lastSentTime).TotalSeconds >= 5; //change expiry here for testing -dvb
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var lastSent = HttpContext.Session.GetString("LastVerificationEmailSent");

            if (lastSent != null)
            {
                var lastSentTime = DateTime.Parse(lastSent);
                if ((DateTime.Now - lastSentTime).TotalSeconds < 5) //change expiry here for testing -dvb
                {
                    CanResend = false;
                    Message = "Please wait before resending.";
                    return Page();
                }
            }

            // "Send" verification email (Simulation)

            //----- ORIGINAL BELOW START
            //Message = "Verification email sent successfully (simulation only).";
            //HttpContext.Session.SetString("LastVerificationEmailSent", DateTime.Now.ToString());
            //CanResend = false;

            //return Page();
            //----- ORIGINAL BELOW END

            var userId = HttpContext.Session.GetString("VerificationUserId");

            if (userId == null)
            {
                Message = "User session expired. Please login again.";
                return Page();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Message = "User not found.";
                return Page();
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var verificationLink = Url.Page(
                "/Account/VerifyEmail",
                pageHandler: null,
                values: new { userId = user.Id, token = token },
                protocol: Request.Scheme);

            // Send email
            var emailSender = new Helpers.EmailSender();
            await emailSender.SendEmailAsync(user.Email, "Verify your BCITGO Account", $"Please verify your account by clicking here: {verificationLink}");

            Message = "Verification email sent successfully!";
            HttpContext.Session.SetString("LastVerificationEmailSent", DateTime.Now.ToString());
            CanResend = false;
            return Page();


        }
    }
}


