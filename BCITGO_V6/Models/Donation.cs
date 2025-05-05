using System;

namespace BCITGO_V6.Models
{
    public class Donation
    {
        public Guid DonationId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; }
    }
}
