using EMS.Shared.DTOs.Employee;

namespace EMS.Shared.DTOs.Department
{
    public class BaseDepartmentDto : BaseDto
    {
        public string Name { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }

    public class DepartmentDto : BaseDto
    {
        public string Name { get; set; }
    }
}
