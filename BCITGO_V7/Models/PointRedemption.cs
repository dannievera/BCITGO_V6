using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class PointRedemption
    {
        [Key]
        public int RedemptionId { get; set; }

        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int PointsRedeemed { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Store? Store { get; set; }
        public User? User { get; set; }
    }
}
