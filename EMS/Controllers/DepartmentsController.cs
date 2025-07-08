using EMS.Services.IServices;
using EMS.Shared.DTOs.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet]
        [Route("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            return Ok(department);
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentCreateDto department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (department == null)
            {
                return BadRequest("Department data is null.");
            }
            var createdDepartment = await departmentService.CreateDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpPut]
        [Route("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateDto department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (department == null || id != department.Id)
            {
                return BadRequest("Department data is invalid.");
            }
            var updatedDepartment = await departmentService.UpdateDepartmentAsync(department);
            if (updatedDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            return Ok(updatedDepartment);
        }

        [HttpDelete]
        [Route("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await departmentService.DeleteDepartmentAsync(id);
            if (!result)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            return NoContent(); // 204 No Content
        }
    }
}
