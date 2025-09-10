namespace EmployeeApi.DTOs
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Department { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }  

    }
}
