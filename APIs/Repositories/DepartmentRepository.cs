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
            // Get the count of existing departments
            int departmentCount = _myContext.Departments.Count();

            // Generate the next department ID based on the count
            string newDepartmentId = $"D{departmentCount + 1:D3}";

            // Set the new department ID for the department being added
            department.Dept_ID = newDepartmentId;

            // Add the department to the database and save changes
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
