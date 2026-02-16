using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class BookingGroupDate
    {
        public int Id { get; set; }

        [Required]
        public int BookingGroupId { get; set; }

        public BookingGroup BookingGroup { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }
    }
}
