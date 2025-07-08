using AutoMapper;
using EMS.DataAccess.Entities;
using EMS.DataAccess.Interfaces;
using EMS.Services.IServices;
using EMS.Shared.DTOs;
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

        public async Task<ResultDto<BaseDepartmentDto>> CreateDepartmentAsync(DepartmentCreateDto departmentDto)
        {
            var department = mapper.Map<Department>(departmentDto);
            await unitOfWork.Departments.AddAsync(department);
            if (await unitOfWork.SaveChangesAsync() > 0)
                return ResultDto<BaseDepartmentDto>.Ok(mapper.Map<BaseDepartmentDto>(department));
            return ResultDto<BaseDepartmentDto>.Fail("Failed to create the department");
        }

        public async Task<ResultDto<DepartmentDto>> DeleteDepartmentAsync(int id)
        {
            var department = await unitOfWork.Departments.GetByIdAsync(id);
            if (department != null)
            {
                unitOfWork.Departments.Delete(department);
                if(await unitOfWork.SaveChangesAsync() > 0)
                    return ResultDto<DepartmentDto>.Ok(mapper.Map<DepartmentDto>(department), "Department deleted successfully");
                return ResultDto<DepartmentDto>.Fail("Failed to delete the department");
            }
            return ResultDto<DepartmentDto>.Fail("Invalid department id");
        }

        public async Task<IEnumerable<BaseDepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await unitOfWork.Departments.GetAllAsync(e => e.Employees);
            return mapper.Map<IEnumerable<BaseDepartmentDto>>(departments);
        }

        public async Task<ResultDto<BaseDepartmentDto>> GetDepartmentByIdAsync(int id)
        {
            var department = await unitOfWork.Departments.GetByIdAsync(id, e => e.Employees);
            if (department != null)
                return ResultDto<BaseDepartmentDto>.Ok(mapper.Map<BaseDepartmentDto>(department));
            return ResultDto<BaseDepartmentDto>.Fail("Invalid department id");
        }

        public async Task<ResultDto<BaseDepartmentDto>> UpdateDepartmentAsync(DepartmentUpdateDto departmentDto)
        {
            var department = mapper.Map<Department>(departmentDto);

            if (await unitOfWork.Departments.ExistsAsync(e => e.Id == departmentDto.Id))
            {
                unitOfWork.Departments.Update(department);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return ResultDto<BaseDepartmentDto>.Ok(mapper.Map<BaseDepartmentDto>(department), "Department updated successfully");
                return ResultDto<BaseDepartmentDto>.Fail("Failed to update the department");
            }

            return ResultDto<BaseDepartmentDto>.Fail("Invalid department id");

        }
    }
}
