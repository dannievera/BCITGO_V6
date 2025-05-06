using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }



        public int SeatsBooked { get; set; }
        public string? BookingMessage { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("RideId")]
        public int RideId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
