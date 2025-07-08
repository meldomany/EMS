using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Employee;

namespace EMS.Services.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<BaseEmployeeDto>> GetAllEmployeesAsync();
        Task<ResultDto<BaseEmployeeDto>> GetEmployeeByIdAsync(int id);
        Task<ResultDto<EmployeeDto>> CreateEmployeeAsync(EmployeeCreateDto employee);
        Task<ResultDto<EmployeeDto>> UpdateEmployeeAsync(EmployeeUpdateDto employee);
        Task<ResultDto<EmployeeDto>> DeleteEmployeeAsync(int id);
        Task<PagedResult<BaseEmployeeDto>> GetEmployeesAsync(EmployeeFilterDto filter);
    }
}
