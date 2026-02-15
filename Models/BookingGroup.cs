using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class BookingGroup
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        // Navigation
        public Customer Customer { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public string? Description { get; set; }

        [Required]
        public BookingGroupStatus Status { get; set; } = BookingGroupStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation - rooms in this group
        public ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

        // Navigation - selected dates for this group
        public ICollection<BookingGroupDate> BookingGroupDates { get; set; } = new List<BookingGroupDate>();

        // 🔽 Soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }

    public enum BookingGroupStatus
    {
        Pending,
        PartiallyApproved,
        AllApproved,
        PartiallyRejected,
        AllRejected
    }
}
