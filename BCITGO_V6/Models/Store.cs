using System;
using System.Collections.Generic;

namespace BCITGO_V6.Models
{
    public class Store
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<PointClaim> PointClaims { get; set; } = new List<PointClaim>();
    }
}
