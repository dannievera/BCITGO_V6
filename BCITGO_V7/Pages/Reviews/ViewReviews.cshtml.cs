using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BCITGO_V6.Data;
using BCITGO_V6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BCITGO_V6.Pages.Reviews
{
    public class ViewReviewsModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewReviewsModel(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Review> GivenReviews { get; set; } = new();
        public List<Review> ReceivedReviews { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string ViewMode { get; set; } = "received"; // default

        public void OnGet()
        {
            LoadUnreadCount();

            var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.User.FirstOrDefault(u => u.IdentityUserId == identityId);
            if (user == null) return;

            if (ViewMode == "given")
            {
                GivenReviews = _context.Review
                    .Include(r => r.Ride)
                    .Include(r => r.Reviewee)
                    .Where(r => r.ReviewerId == user.UserId)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();
            }
            else
            {
                ReceivedReviews = _context.Review
                    .Include(r => r.Ride)
                    .Include(r => r.Reviewer)
                    .Where(r => r.RevieweeId == user.UserId)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();
            }
        }
    }
}
