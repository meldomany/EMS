using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Employee;

namespace EMS.Services.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto employee);
        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeUpdateDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<PagedResult<EmployeeDto>> GetEmployeesAsync(EmployeeFilterDto filter);
    }
}
