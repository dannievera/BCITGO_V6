using System;

namespace BCITGO_V6.Models
{
    public class Invite
    {
        public Guid InviteId { get; set; }
        public Guid InviterUserId { get; set; }
        public string InviteEmail { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User InviterUser { get; set; }
    }
}
