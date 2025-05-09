using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class SupportTicket
    {
        [Key]
        public int TicketId { get; set; }

        public string Details { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Foreign key to the User model
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        // Navigation property to link back to User
        public User User { get; set; }  // This is the missing part: defining navigation to User

        // Navigation property to link comments related to the ticket
        public ICollection<SupportComment>? Comments { get; set; }
    }
}
