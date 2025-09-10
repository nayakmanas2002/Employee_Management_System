using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.DTOs
{
    public class EmployeeCreateDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = null!;

        [MaxLength(100)]
        public string? Department { get; set; }

        public DateTime? DateOfJoining { get; set; }
    }
}
