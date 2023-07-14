

using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Interfaces;
using DataAccessLayer.Singleton;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Services.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly AppDbContext _context = SingletonClass.GetInstance();
        private readonly Logger _logger = SingletonClass.GetInstanceLogger();
        private readonly IMapper _mapper;
        public EmployeeRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public List<Employee> GetEmployees()
        {
            var data = _context.Employees.Where(x => x.IsDeleted == Convert.ToBoolean(0)).Include(e => e.Department);
            _logger.Log("Employees Data Fetched");
            return data.ToList();
        }
        public Employee GetSingleEmployee(int id)
        {
            var data = _context.Employees.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
            _logger.Log("Single Employee Data Fetched");
            return data;
        }
        public Employee AddOrUpdateEmployee(int id, EmployeeDto employeeData)
        {
            var data = GetSingleEmployee(id);
            if (data != null)
            {
                data.Name = employeeData.Name;
                data.Email = employeeData.Email;
                data.Salary = employeeData.Salary;
                data.DepartmentId = employeeData.DepartmentId;
                data.Department = GetDepartment(employeeData.DepartmentId);
                _logger.Log($"Employee Data Updated with name:{data.Name}");
                _context.SaveChanges();
                return data;
            }
            else
            {
                var employee = _mapper.Map<Employee>(employeeData);
                employee.Department = GetDepartment(employeeData.DepartmentId);
                _context.Employees.Add(employee);
                _logger.Log("Employee Data Added");
                _context.SaveChanges();
                return employee;
            }
        }
        public Employee RemoveEmployee(int id)
        {
            var data = GetSingleEmployee(id);
            if (data != null)
            {
                data.IsDeleted = true;
                _logger.Log("Employee Data Deleted");
                _context.SaveChanges();
                return data;
            }
            return null;
        }

        public List<Department> GetDepartments()
        {
            var data = _context.Departments.Where(x=>x.IsDeleted == Convert.ToBoolean(0)).ToList();
            _logger.Log("Departments Data Fetched");
            return data;
        }
        public Department GetDepartment(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
            _logger.Log("Single Department Data Fetched");
            return dept;
        }
    }
}
