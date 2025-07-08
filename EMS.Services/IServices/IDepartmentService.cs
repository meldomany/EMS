using EMS.Shared.DTOs;
using EMS.Shared.DTOs.Department;

namespace EMS.Services.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<BaseDepartmentDto>> GetAllDepartmentsAsync();
        Task<ResultDto<BaseDepartmentDto>> GetDepartmentByIdAsync(int id);
        Task<ResultDto<BaseDepartmentDto>> CreateDepartmentAsync(DepartmentCreateDto employee);
        Task<ResultDto<BaseDepartmentDto>> UpdateDepartmentAsync(DepartmentUpdateDto employee);
        Task<ResultDto<DepartmentDto>> DeleteDepartmentAsync(int id);
    }
}
