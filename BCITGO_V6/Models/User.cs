using System;
using System.Collections.Generic;

namespace BCITGO_V6.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string Role { get; set; }
        public bool EmailVerified { get; set; }
        public string AccountStatus { get; set; }
        public string? BanReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public ICollection<Ride> Rides { get; set; } = new List<Ride>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> ReviewsWritten { get; set; } = new List<Review>();
        public ICollection<Review> ReviewsReceived { get; set; } = new List<Review>();
        public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<RidePoints> RidePoints { get; set; } = new List<RidePoints>();
        public ICollection<RidePointsSummary> RidePointsSummary { get; set; } = new List<RidePointsSummary>();

        public ICollection<Invite> InvitesSent { get; set; } = new List<Invite>();
        public ICollection<PointClaim> PointClaims { get; set; } = new List<PointClaim>();
    }
}
