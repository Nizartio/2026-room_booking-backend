using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Requests
{
    public class UpdateCustomerRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(30)]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}
