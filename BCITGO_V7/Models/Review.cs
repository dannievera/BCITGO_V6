using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }




        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("RideId")]
        public int RideId { get; set; }

        [ForeignKey("ReviewerId")]
        public int ReviewerId { get; set; }

        [ForeignKey("RevieweeId")]
        public int RevieweeId { get; set; }
    }
}
