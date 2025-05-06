using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime LastActiveAt { get; set; }

        // Navigation
        public ICollection<Ride>? Rides { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? ReviewsWritten { get; set; }
        public ICollection<Review>? ReviewsReceived { get; set; }
        public ICollection<RidePoint>? RidePoints { get; set; }
        public ICollection<PointRedemption>? PointRedemptions { get; set; }
        public ICollection<Invite>? InvitesSent { get; set; }
        public ICollection<SupportTicket>? SupportTickets { get; set; }
        public ICollection<SupportComment>? SupportComments { get; set; }
        public ICollection<Donation>? Donations { get; set; }
    }
}

