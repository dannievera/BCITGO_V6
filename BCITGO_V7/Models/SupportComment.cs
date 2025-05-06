using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCITGO_V6.Models
{
    public class SupportComment
    {
        [Key]
        public int CommentId { get; set; }



        public string CommentText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation
        [ForeignKey("TicketId")]
        public int TicketId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
