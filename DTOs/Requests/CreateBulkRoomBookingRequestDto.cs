using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class CreateBulkRoomBookingRequestDto
    {
        [Required]
        public List<CreateRoomBookingRequestDto> Bookings { get; set; } = new();
    }
}
