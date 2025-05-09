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

        public string DriverName { get; set; }  // Added
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public decimal PricePerSeat { get; set; }
        public int TotalSeats { get; set; } 
        public string? Notes { get; set; }
        public bool LuggageAllowed { get; set; }
        public bool PetsAllowed { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public int BookedSeats { get; set; }

        [NotMapped]
        public int PendingRequests { get; set; }

        [NotMapped]
        public int ConfirmedSeats { get; set; }  // For AvailableRides

        [NotMapped]
        public int PendingSeats { get; set; }    // For AvailableRides

        [NotMapped]
        public int AvailableSeats => TotalSeats - BookedSeats;  //added as not mapped then I deleted on back end due to not mapped >> included for logic purposes

        // Navigation
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }



        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
