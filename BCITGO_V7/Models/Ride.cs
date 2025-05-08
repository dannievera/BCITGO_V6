using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }


        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public decimal PricePerSeat { get; set; }
        public int AvailableSeats { get; set; }
        public string? Notes { get; set; }
        public bool LuggageAllowed { get; set; }
        public bool PetsAllowed { get; set; }

        [NotMapped]
        public int BookedSeats { get; set; }

        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
