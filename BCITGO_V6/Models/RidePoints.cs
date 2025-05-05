using System;

namespace BCITGO_V6.Models
{
    public class RidePoints
    {
        public Guid RidePointsId { get; set; }
        public Guid UserId { get; set; }
        public Guid? RideId { get; set; } // Nullable if not from ride
        public string Source { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public Ride? Ride { get; set; }
    }
}
