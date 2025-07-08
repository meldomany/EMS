using AutoMapper;
using EMS.DataAccess.Entities;
using EMS.DataAccess.Interfaces;
using EMS.Services.IServices;
using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Employee;

namespace EMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            await unitOfWork.Employees.AddAsync(employee);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return mapper.Map<EmployeeDto>(employee);
            }
            return new EmployeeDto();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await unitOfWork.Employees.GetByIdAsync(id);
            if(employee != null)
            {
                unitOfWork.Employees.Delete(employee);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await unitOfWork.Employees.GetAllAsync();
            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await unitOfWork.Employees.GetByIdAsync(id);
            if (employee != null)
            {
                return mapper.Map<EmployeeDto>(employee);
            }
            return new EmployeeDto();
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeUpdateDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            unitOfWork.Employees.Update(employee);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return mapper.Map<EmployeeDto>(employee);
            }
            return new EmployeeDto();
        }

        public async Task<PagedResult<EmployeeDto>> GetEmployeesAsync(EmployeeFilterDto filter)
        {
            var result = await unitOfWork.Employees.GetFilteredEmployeesAsync(filter);

            return new PagedResult<EmployeeDto>
            {
                Items = mapper.Map<IEnumerable<EmployeeDto>>(result.Items),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

    }
}
