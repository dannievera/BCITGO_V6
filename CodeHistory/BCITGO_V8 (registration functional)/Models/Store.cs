using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        public string StoreName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<PointRedemption>? PointRedemptions { get; set; }
    }
}
