using System.ComponentModel.DataAnnotations;
using backend.Models;
namespace backend.DTOs.Requests
{
    public class UpdateRoomBookingStatusDto
    {
        [Required]
        public BookingStatus Status { get; set; }
    }
}
