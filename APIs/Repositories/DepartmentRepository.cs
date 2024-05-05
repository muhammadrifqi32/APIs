using APIs.Context;
using APIs.Models;
using APIs.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace APIs.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext _myContext;

        public DepartmentRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public int AddDepartment(Department department)
        {
            _myContext.Departments.Add(department);
            return _myContext.SaveChanges();
        }

        public int DeleteDepartment(string deptId)
        {
            var departmentToDelete = _myContext.Departments.Find(deptId);
            if (departmentToDelete != null)
            {
                _myContext.Departments.Remove(departmentToDelete);
                return _myContext.SaveChanges();
            }
            return 0; // Department not found or other error
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var get =  _myContext.Departments.ToList();
            return get;
        }

        public Department GetDepartmentById(string deptId)
        {
            return _myContext.Departments.Find(deptId);
        }

        public int UpdateDepartment(Department department)
        {
            _myContext.Entry(department).State = EntityState.Modified;
            return _myContext.SaveChanges();
        }
    }
}
