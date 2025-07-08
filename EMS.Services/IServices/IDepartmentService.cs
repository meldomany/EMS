using EMS.Shared.DTOs.Department;

namespace EMS.Services.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(DepartmentCreateDto employee);
        Task<DepartmentDto> UpdateDepartmentAsync(DepartmentUpdateDto employee);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
