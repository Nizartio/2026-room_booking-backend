namespace backend.DTOs.Responses
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? Address { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
