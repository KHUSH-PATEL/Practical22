
using DataAccessLayer.Models;

namespace DataAccessLayer.Services.Interfaces
{
    public interface IDepartment
    {
        Department GetSingleDepartment(int id);
        List<Department> GetDepartments();
        Department AddOrUpdateDepartment(int id, Department departmentData);
        Department RemoveDepartment(int id);
    }
}
