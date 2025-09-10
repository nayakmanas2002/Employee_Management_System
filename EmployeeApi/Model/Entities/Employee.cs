using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApi.Model.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(200), EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
             ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [MaxLength(100)]
        public string? Department { get; set; }

        public DateTime DateOfJoining { get; set; } = DateTime.Now;

        [DefaultValue(true)]

        public bool IsActive { get; set; } = true;
    }
}
