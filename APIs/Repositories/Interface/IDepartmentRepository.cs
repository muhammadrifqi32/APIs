using APIs.Models;

namespace APIs.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        // Retrieve all departments
        IEnumerable<Department> GetAllDepartments();

        // Retrieve a specific department by ID
        Department GetDepartmentById(string deptId);

        // Add a new department
        int AddDepartment(Department department);

        // Update an existing department
        int UpdateDepartment(Department department);

        // Delete a department by ID
        int DeleteDepartment(string deptId);
    }
}
