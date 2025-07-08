using EMS.Services;
using EMS.Services.IServices;
using EMS.Shared.DTOs.Employee;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            if (!employee.Success)
                return BadRequest(employee);
            return Ok(employee);
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (employee == null)
            {
                return BadRequest("Employee data is null.");
            }

            var createdEmployee = await employeeService.CreateEmployeeAsync(employee);
            if(createdEmployee.Success)
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Data.Id }, createdEmployee);
            return BadRequest(createdEmployee);
        }

        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (employee == null || id != employee.Id)
            {
                return BadRequest("Employee data is invalid.");
            }
            var updatedEmployee = await employeeService.UpdateEmployeeAsync(employee);
            if (!updatedEmployee.Success)
                return BadRequest(updatedEmployee);
            return Ok(updatedEmployee);
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await employeeService.DeleteEmployeeAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterDto filter)
        {
            var result = await employeeService.GetEmployeesAsync(filter);
            return Ok(result);
        }

    }
}
