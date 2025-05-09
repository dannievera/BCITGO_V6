using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string? ReviewText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int RideId { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }

        // Navigation Properties
        [ForeignKey("RideId")]
        public Ride Ride { get; set; }

        [ForeignKey("ReviewerId")]
        public User Reviewer { get; set; }

        [ForeignKey("RevieweeId")]
        public User Reviewee { get; set; }
    }
}
