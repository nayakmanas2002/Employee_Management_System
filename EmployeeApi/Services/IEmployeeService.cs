using EmployeeApi.DTOs;
using EmployeeApi.Model.Entities;

namespace EmployeeApi.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetAsync(int id);
        Task<PagedResult<EmployeeReadDto>> QueryAsync(int page, int size, string? search, string? department);
        Task<Employee> CreateAsync(Employee e);
        Task<bool> UpdateAsync(Employee e);
        Task<bool> DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}
