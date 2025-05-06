using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        public decimal Amount { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
