using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class Invite
    {
        [Key]
        public int InviteId { get; set; }

        public int UserId { get; set; }
        public int InviteeId { get; set; }
        public string InviteeEmail { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User? User { get; set; }
        public User? Invitee { get; set; }
    }
}
