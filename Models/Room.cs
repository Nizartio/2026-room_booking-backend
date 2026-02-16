using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 500)]
        public int Capacity { get; set; }

        public string? Description { get; set; }

        // Status bisnis
        public bool IsActive { get; set; } = true;

        // Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation
        public List<RoomBooking> RoomBookings { get; set; } = new();
    }
}
