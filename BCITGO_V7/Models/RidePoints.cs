using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class RidePoint
    {
        [Key]
        public int RidePointsId { get; set; }

        public int UserId { get; set; }
        public int Points { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}
