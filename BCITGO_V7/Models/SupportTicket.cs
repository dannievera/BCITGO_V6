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

        // Navigation
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public ICollection<SupportComment>? Comments { get; set; }
    }
}
