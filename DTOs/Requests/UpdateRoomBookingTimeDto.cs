using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class UpdateRoomBookingTimeDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
