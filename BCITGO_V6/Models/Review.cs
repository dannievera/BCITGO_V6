using System;

namespace BCITGO_V6.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid RideId { get; set; }
        public Guid ReviewerId { get; set; }
        public Guid ReviewedUserId { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Ride Ride { get; set; }
        public User Reviewer { get; set; }
        public User ReviewedUser { get; set; }
    }
}
