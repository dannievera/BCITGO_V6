using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Security.Claims;

namespace BCITGO_V6.Pages.Reviews
{
    public class LeaveReviewModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public LeaveReviewModel(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        [BindProperty]
        public int RideId { get; set; }

        [BindProperty]
        public int RevieweeId { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public string? ReviewText { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(int rideId, int revieweeId)
        {
            RideId = rideId;
            RevieweeId = revieweeId;
            LoadUnreadCount();
        }

        public IActionResult OnPost()
        {
            LoadUnreadCount();

            if (Rating < 1 || Rating > 5)
            {
                ErrorMessage = "Please select a valid star rating.";
                return Page();
            }

            var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var reviewer = _context.User.FirstOrDefault(u => u.IdentityUserId == identityId);

            if (reviewer == null)
            {
                ErrorMessage = "User not found.";
                return Page();
            }

            var newReview = new Review
            {
                RideId = RideId,
                ReviewerId = reviewer.UserId,
                RevieweeId = RevieweeId,
                Rating = Rating,
                ReviewText = string.IsNullOrWhiteSpace(ReviewText) ? null : ReviewText.Trim(),
                CreatedAt = DateTime.Now
            };

            _context.Review.Add(newReview);
            _context.SaveChanges();

            SuccessMessage = "Thank you! Your review has been submitted.";
            return Page();
        }
    }
}
