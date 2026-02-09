namespace backend.DTOs.Responses
{
    public class RoomBookingDetailResponseDto
    {
        public int Id { get; set; }

        // Room
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public int RoomCapacity { get; set; }

        // Customer
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }

        // Booking
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
