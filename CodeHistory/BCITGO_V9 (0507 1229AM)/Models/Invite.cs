using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class Invite
    {
        [Key]
        public int InviteId { get; set; }



        public string InviteeEmail { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("InviteeId")]
        public int? InviteeId { get; set; }
    }
}
