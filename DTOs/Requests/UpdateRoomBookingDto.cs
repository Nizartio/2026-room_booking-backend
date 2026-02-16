using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class UpdateRoomBookingTimeDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
