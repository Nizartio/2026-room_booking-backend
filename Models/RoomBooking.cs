using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string RoomName { get; set; } = string.Empty;

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
