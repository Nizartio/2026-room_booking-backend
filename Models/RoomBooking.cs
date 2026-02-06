using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room name is required")]
        [StringLength(100)]
        public string RoomName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Borrower name is required")]
        [StringLength(100)]
        public string BorrowerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; }

        [Required]
        [RegularExpression("Pending|Approved|Rejected",
            ErrorMessage = "Status must be Pending, Approved, or Rejected")]
        public string Status { get; set; } = "Pending";
    }
}
