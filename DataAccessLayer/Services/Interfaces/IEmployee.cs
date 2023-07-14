
using DataAccessLayer.Models;

namespace DataAccessLayer.Services.Interfaces
{
    public interface IEmployee
    {
        Employee GetSingleEmployee(int id);
        List<Employee> GetEmployees();
        Employee AddOrUpdateEmployee(int id, EmployeeDto employeeData);
        Employee RemoveEmployee(int id);
        List<Department> GetDepartments();
        Department GetDepartment(int id);
    }
}
