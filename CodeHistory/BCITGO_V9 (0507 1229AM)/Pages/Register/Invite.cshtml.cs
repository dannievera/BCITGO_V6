using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Pages.Register
{
    public class InviteModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string Message { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (!Input.Email.EndsWith("@my.bcit.ca"))
            {
                ModelState.AddModelError("Input.Email", "Only BCIT emails are allowed.");
                return Page();
            }

            Message = "Invite sent successfully (simulation only).";
            return Page();
        }
    }

}