namespace backend.DTOs.Responses
{
    public class BulkRoomBookingResponseDto
    {
        public int TotalRequested { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }

        public List<BulkBookingResultDto> Results { get; set; } = new();
    }

    public class BulkBookingResultDto
    {
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
