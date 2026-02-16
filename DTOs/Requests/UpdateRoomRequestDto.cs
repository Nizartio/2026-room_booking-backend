using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class UpdateRoomRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 500)]
        public int Capacity { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
