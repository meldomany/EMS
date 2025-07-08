using AutoMapper;
using EMS.DataAccess.Entities;
using EMS.DataAccess.Interfaces;
using EMS.Services.IServices;
using EMS.Shared.DTOs.Department;

namespace EMS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentCreateDto departmentDto)
        {
            var department = mapper.Map<Department>(departmentDto);
            await unitOfWork.Departments.AddAsync(department);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return mapper.Map<DepartmentDto>(department);
            }
            return new DepartmentDto();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await unitOfWork.Departments.GetByIdAsync(id);
            if (department != null)
            {
                unitOfWork.Departments.Delete(department);
                return await unitOfWork.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await unitOfWork.Departments.GetAllAsync();
            return mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            var department = await unitOfWork.Departments.GetByIdAsync(id);
            if (department != null)
            {
                return mapper.Map<DepartmentDto>(department);
            }
            return new DepartmentDto();
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(DepartmentUpdateDto departmentDto)
        {
            var department = mapper.Map<Department>(departmentDto);
            unitOfWork.Departments.Update(department);
            if (await unitOfWork.SaveChangesAsync() > 0)
            {
                return mapper.Map<DepartmentDto>(department);
            }
            return new DepartmentDto();
        }
    }
}
