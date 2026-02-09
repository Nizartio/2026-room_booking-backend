using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class UpdateRoomBookingStatusDto
    {
        [Required]
        [RegularExpression("Pending|Approved|Rejected",
            ErrorMessage = "Status must be Pending, Approved, or Rejected.")]
        public string Status { get; set; } = string.Empty;
    }
}
