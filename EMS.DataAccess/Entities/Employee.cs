using EMS.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.DataAccess.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeStatusEnum Status { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
    }
}
