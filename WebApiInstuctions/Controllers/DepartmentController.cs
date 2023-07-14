using DataAccessLayer.Models;
using DataAccessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiInstuctions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _department;
        public DepartmentController(IDepartment department)
        {
            _department = department;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Department>> GetAllDepartment()
        {
            IEnumerable<Department> dept = _department.GetDepartments();
            if (dept == null)
            {
                return NotFound();
            }
            return dept.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            var result = _department.GetSingleDepartment(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddEmployee(Department department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _department.AddOrUpdateDepartment(department.Id, department);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }        

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, Department department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _department.AddOrUpdateDepartment(id, department);
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = _department.GetSingleDepartment(id);
                if (data is null)
                {
                    return NotFound();
                }
                var result = _department.RemoveDepartment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
