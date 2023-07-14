

using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Services.Interfaces;
using DataAccessLayer.Singleton;

namespace DataAccessLayer.Services.Repository
{
    public class DepartmentRepository : IDepartment
    {
        private readonly AppDbContext _context = SingletonClass.GetInstance();
        
        public Department AddOrUpdateDepartment(int id, Department departmentData)
        {
            var data = GetSingleDepartment(id);
            if (data != null)
            {
                data.Name = departmentData.Name;
                data.IsDeleted = departmentData.IsDeleted;
                _context.SaveChanges();
                return data;
            }
            else
            {
                _context.Departments.Add(departmentData);
                _context.SaveChanges();
                return departmentData;
            }
        }

        public List<Department> GetDepartments()
        {
            var data = _context.Departments.Where(x => x.IsDeleted == Convert.ToBoolean(0)).ToList();
            return data;
        }

        public Department GetSingleDepartment(int id)
        {
            var data = _context.Departments.Find(id);
            return data;
        }

        public Department RemoveDepartment(int id)
        {
            var data = GetSingleDepartment(id);
            data.IsDeleted = true;
            _context.SaveChanges();
            return data;
        }
    }
}
