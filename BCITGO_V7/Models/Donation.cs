using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}
