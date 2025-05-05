using System;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class SupportTicket
    {
        [Key] // ✅ ADD THIS → Marks as Primary Key

        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public string? AdminNotes { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public User User { get; set; }
    }
}
