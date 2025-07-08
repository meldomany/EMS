using AutoMapper;
using EMS.DataAccess.Entities;
using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Department;
using EMS.Shared.DTOs.Employee;

namespace EMS.Services.MappingProfile
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDto>().ReverseMap();
            
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();

            CreateMap<LogHistory, LogHistoryDto>().ReverseMap();
        }
    }
}
