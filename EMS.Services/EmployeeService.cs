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

        public async Task<ResultDto<EmployeeDto>> CreateEmployeeAsync(EmployeeCreateDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);

            if(await unitOfWork.Departments.ExistsAsync(e => e.Id == employeeDto.DepartmentId))
            { 
                await unitOfWork.Employees.AddAsync(employee);
                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return ResultDto<EmployeeDto>.Ok(mapper.Map<EmployeeDto>(employee), "Employee created successfully.");
                }
                return ResultDto<EmployeeDto>.Fail("Failed to create a new employee");
            }
            return ResultDto<EmployeeDto>.Fail("Invalid department id");
        }

        public async Task<ResultDto<EmployeeDto>> DeleteEmployeeAsync(int id)
        {
            var employee = await unitOfWork.Employees.GetByIdAsync(id);
            if(employee != null)
            {
                unitOfWork.Employees.Delete(employee);
                if(await unitOfWork.SaveChangesAsync() > 0)
                   return ResultDto<EmployeeDto>.Ok(mapper.Map<EmployeeDto>(employee), "Employee deleted successfully");
                return ResultDto<EmployeeDto>.Fail("Failed to delete the employee");
            }
            return ResultDto<EmployeeDto>.Fail("Invalid employee id");
        }

        public async Task<IEnumerable<BaseEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await unitOfWork.Employees.GetAllAsync(e => e.Department);
            return mapper.Map<IEnumerable<BaseEmployeeDto>>(employees);
        }

        public async Task<ResultDto<BaseEmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            var employee = await unitOfWork.Employees.GetByIdAsync(id, e => e.Department);
            if (employee != null)
                return ResultDto<BaseEmployeeDto>.Ok(mapper.Map<BaseEmployeeDto>(employee));
            return ResultDto<BaseEmployeeDto>.Fail("Invalid employee id");
        }

        public async Task<ResultDto<EmployeeDto>> UpdateEmployeeAsync(EmployeeUpdateDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);

            if(await unitOfWork.Employees.ExistsAsync(e => e.Id == employeeDto.Id)
                && await unitOfWork.Departments.ExistsAsync(e => e.Id == employeeDto.DepartmentId))
            {
                unitOfWork.Employees.Update(employee);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return ResultDto<EmployeeDto>.Ok(mapper.Map<EmployeeDto>(employee), "Employee updated successfully");
                return ResultDto<EmployeeDto>.Fail("Failed to update the employee");
            }
            return ResultDto<EmployeeDto>.Fail("Invalid employee or department id");
        }

        public async Task<PagedResult<BaseEmployeeDto>> GetEmployeesAsync(EmployeeFilterDto filter)
        {
            var result = await unitOfWork.Employees.GetFilteredEmployeesAsync(filter);

            return new PagedResult<BaseEmployeeDto>
            {
                Items = mapper.Map<IEnumerable<BaseEmployeeDto>>(result.Items),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

    }
}
