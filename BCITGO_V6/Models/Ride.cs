using System;
using System.Collections.Generic;

namespace BCITGO_V6.Models
{
    public class Ride
    {
        public Guid RideId { get; set; }
        public Guid DriverId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public decimal PricePerSeat { get; set; }
        public int AvailableSeats { get; set; }
        public string? Notes { get; set; }
        public string? LuggageAllowed { get; set; }
        public bool PetsAllowed { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User Driver { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<RidePoints> RidePoints { get; set; } = new List<RidePoints>();
    }
}
