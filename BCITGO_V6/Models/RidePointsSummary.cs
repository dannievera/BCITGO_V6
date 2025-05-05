using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BCITGO_V6.Models
{
    public class RidePointsSummary
    {
        [Key]
        public Guid RidePointsSummaryId { get; set; }

        // Foreign Key to User
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = default!;

        // Total points earned
        [Required]
        public int TotalPointsEarned { get; set; }

        // Total points claimed
        [Required]
        public int TotalPointsClaimed { get; set; }

        // Current balance (earned - claimed)
        [Required]
        public int BalancePoints { get; set; }

        // Last updated date
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
