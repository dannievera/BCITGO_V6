using System;

namespace BCITGO_V6.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public Guid RideId { get; set; }
        public Guid PassengerId { get; set; }
        public int SeatsBooked { get; set; }
        public string? BookingMessage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Ride Ride { get; set; }
        public User Passenger { get; set; }
    }
}
