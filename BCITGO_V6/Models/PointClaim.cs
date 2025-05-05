using System;

namespace BCITGO_V6.Models
{
    public class PointClaim
    {
        public Guid PointClaimId { get; set; }
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public int AmountClaimed { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public Store Store { get; set; }
    }
}
