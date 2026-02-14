using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class CreateRoomBookingRequestDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public string? Description { get; set; }
    }
}