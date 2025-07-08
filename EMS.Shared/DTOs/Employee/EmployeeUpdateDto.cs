using EMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.DTOs.Employee
{
    public class EmployeeUpdateDto : BaseDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name can't exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required]
        [EnumDataType(typeof(EmployeeStatusEnum))]
        public EmployeeStatusEnum Status { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DepartmentId must be a valid positive integer.")]
        public int DepartmentId { get; set; }
    }
}
