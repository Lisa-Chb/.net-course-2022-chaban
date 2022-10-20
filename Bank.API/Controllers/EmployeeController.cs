using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Bank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _service { get; set; }
        public EmployeeController()
        {
            _service = new EmployeeService();
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetEmployee(Guid employeeId)
        {
            return Ok(await _service.GetEmployee(employeeId));
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            await _service.AddNewEmployee(employee);
            return Ok("Клиент успешно добавлен");
        }

        [HttpPut]
        public async Task<IActionResult> PutEmployee(Employee employee)
        {
            await _service.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid employeeId)
        {
            await _service.DeleteEmployee(employeeId);
            return Ok();
        }
    }
}
