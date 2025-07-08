using EMS.Shared.Enums;

namespace EMS.Shared.DTOs.Employee
{
    public class EmployeeFilterDto : PaginationDto
    {
        public string? Name { get; set; }
        public int? DepartmentId { get; set; }
        public EmployeeStatusEnum? Status { get; set; }
        public DateTime? HireDateFrom { get; set; }
        public DateTime? HireDateTo { get; set; }
        public string SortBy { get; set; } = "Name";
        public string SortDirection { get; set; } = "asc";
    }
}
