using AutoMapper;
using EmployeeApi.DTOs;
using EmployeeApi.Model.Entities;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _svc;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetEmployeeByID{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await _svc.GetAsync(id);
            if (emp == null) return NotFound();
            return Ok(_mapper.Map<EmployeeReadDto>(emp));
        }

        [HttpGet("SearchEmployee")]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int size = 10,
                                              [FromQuery] string? search = null, [FromQuery] string? department = null)
        {
            var result = await _svc.QueryAsync(page, size, search, department);
            return Ok(result);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _svc.EmailExistsAsync(dto.Email)) return Conflict(new { message = "Email already exists" });

            var emp = _mapper.Map<Employee>(dto);
            var created = await _svc.CreateAsync(emp);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, _mapper.Map<EmployeeReadDto>(created));
        }

        [HttpPut("UpdateEmployee{ID}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeUpdateDto dto)
        {
            var emp = await _svc.GetAsync(id);
            if (emp == null) return NotFound();
            if (await _svc.EmailExistsAsync(dto.Email, excludeId: id)) return Conflict(new { message = "Email already exists" });

            _mapper.Map(dto, emp);
            await _svc.UpdateAsync(emp);
            return NoContent();
        }

        [HttpDelete("DeleteEmployee{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _svc.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
