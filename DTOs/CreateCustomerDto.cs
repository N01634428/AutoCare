using System.ComponentModel.DataAnnotations;

namespace AutoCareAPI.DTOs
{
    public class CreateCustomerDto
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
