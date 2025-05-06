using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int RideId { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Ride? Ride { get; set; }
        public User? Reviewer { get; set; }
        public User? Reviewee { get; set; }
    }
}
