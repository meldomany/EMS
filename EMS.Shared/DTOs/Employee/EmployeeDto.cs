using EMS.Shared.DTOs.Department;
using EMS.Shared.Enums;

namespace EMS.Shared.DTOs.Employee
{

    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeStatusEnum Status { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
