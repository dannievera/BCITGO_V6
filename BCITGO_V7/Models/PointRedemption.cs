using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class PointRedemption
    {
        [Key]
        public int RedemptionId { get; set; }



        public int PointsRedeemed { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        // Navigation
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
