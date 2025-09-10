using AutoMapper;
using EmployeeApi.DTOs;
using EmployeeApi.Model.Entities;

namespace EmployeeApi.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeReadDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => src.DateOfJoining ?? DateTime.UtcNow));
            CreateMap<EmployeeUpdateDto, Employee>()
                .ForMember(dest => dest.DateOfJoining, opt => opt.Condition(src => src.DateOfJoining.HasValue))
                .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => src.DateOfJoining ?? DateTime.UtcNow));
        }
    }
}
