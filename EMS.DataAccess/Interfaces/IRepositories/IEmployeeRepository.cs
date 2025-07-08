using EMS.DataAccess.Entities;
using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Employee;

namespace EMS.DataAccess.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<PagedResult<Employee>> GetFilteredEmployeesAsync(EmployeeFilterDto filter);
    }
}
