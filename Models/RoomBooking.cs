using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        // Navigation property
        public Room Room { get; set; } = null!;

        [Required, StringLength(100)]
        public string BorrowerName { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [RegularExpression("Pending|Approved|Rejected")]
        public string Status { get; set; } = "Pending";

        // 🔽 Soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
