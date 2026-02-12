using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        // Navigation property
        public Room Room { get; set; } = null!;

        [Required]
        public int CustomerId { get; set; }

        // Navigation
        public Customer Customer { get; set; } = null!;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔽 Soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
