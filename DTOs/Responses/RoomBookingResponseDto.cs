namespace backend.DTOs.Responses
{
    public class RoomBookingResponseDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
