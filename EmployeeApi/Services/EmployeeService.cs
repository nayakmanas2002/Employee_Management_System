using AutoMapper;
using EmployeeApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Services
{
    public class EmployeeService  : IEmployeeService    
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public EmployeeService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Employee?> GetAsync(int id) => await _db.Employees.FindAsync(id);

        public async Task<PagedResult<EmployeeReadDto>> QueryAsync(int page, int size, string? search, string? department)
        {
            var query = _db.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                var s = search.ToLower();
                query = query.Where(e => (e.FirstName + " " + e.LastName).ToLower().Contains(s) || e.Email.ToLower().Contains(s));
            }

            if (!string.IsNullOrEmpty(department))
                query = query.Where(e => e.Department == department);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();

            return new PagedResult<EmployeeReadDto>
            {
                Items = items.Select(e => _mapper.Map<EmployeeReadDto>(e)),
                TotalItems = total,
                Page = page,
                Size = size
            };
        }

        public async Task<Employee> CreateAsync(Employee e)
        {
            _db.Employees.Add(e);
            await _db.SaveChangesAsync();
            return e;
        }

        public async Task<bool> UpdateAsync(Employee e)
        {
            var exists = await _db.Employees.AnyAsync(x => x.Id == e.Id);
            if (!exists) return false;

            _db.Employees.Update(e);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e == null) return false;

            _db.Employees.Remove(e);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var q = _db.Employees.Where(e => e.Email == email);
            if (excludeId.HasValue)
                q = q.Where(e => e.Id != excludeId.Value);

            return await q.AnyAsync();
        }
    }
}
