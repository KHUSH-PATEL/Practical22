using DataAccessLayer.Models;
using DataAccessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiInstuctions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;        
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;            
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployee()
        {
            IEnumerable<Employee> employee = _employee.GetEmployees();
            if (employee == null)
            {
                return NotFound();
            }
            return employee.ToList();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var result = _employee.GetSingleEmployee(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employee)
        {
            try
            {
                var dept = _employee.GetDepartment(employee.DepartmentId);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }                
                else if(dept is null)
                {
                    return BadRequest("Department Id Does not exists!");
                }
                var result = _employee.AddOrUpdateEmployee(employee.Id, employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _employee.AddOrUpdateEmployee(id, employee);
                if (result is null)
                {
                    return NotFound();
                }                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _employee.GetSingleEmployee(id);
                if(data is null)
                {
                    return NotFound();
                }
                var result = _employee.RemoveEmployee(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
