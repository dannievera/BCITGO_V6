using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BCITGO_V6.Pages.Account
{
    public class VerifyModel : PageModel
    {
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
                CanResend = (DateTime.Now - lastSentTime).TotalSeconds >= 60;
            }
        }

        public IActionResult OnPost()
        {
            var lastSent = HttpContext.Session.GetString("LastVerificationEmailSent");

            if (lastSent != null)
            {
                var lastSentTime = DateTime.Parse(lastSent);
                if ((DateTime.Now - lastSentTime).TotalSeconds < 60)
                {
                    CanResend = false;
                    Message = "Please wait before resending.";
                    return Page();
                }
            }

            // "Send" verification email (Simulation)
            Message = "Verification email sent successfully (simulation only).";
            HttpContext.Session.SetString("LastVerificationEmailSent", DateTime.Now.ToString());
            CanResend = false;

            return Page();
        }
    }
}


