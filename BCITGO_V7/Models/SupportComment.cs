using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class SupportComment
    {
        [Key]
        public int CommentId { get; set; }

        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public SupportTicket? Ticket { get; set; }
        public User? User { get; set; }
    }
}
