using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class RidePoint
    {
        [Key]
        public int RidePointsId { get; set; }


        public int Points { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
