using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }

        [Required]
        public string RoomName { get; set; } = string.Empty;

        [Required]
        public string BorrowerName { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";
    }
}
