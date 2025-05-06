using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int RideId { get; set; }
        public int UserId { get; set; }
        public int SeatsBooked { get; set; }
        public string? BookingMessage { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Ride? Ride { get; set; }
        public User? User { get; set; }
    }
}
