using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class CreateBulkBookingGroupRequestDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<CreateBookingGroupItemDto> Groups { get; set; } = new();
    }

    public class CreateBookingGroupItemDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public List<int> RoomIds { get; set; } = new();

        public string? Description { get; set; }
    }
}
