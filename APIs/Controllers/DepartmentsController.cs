using APIs.Models;
using APIs.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentsController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            //try
            //{
            var departments = _departmentRepository.GetAllDepartments();
            if (departments == null || !departments.Any())
            {
                return Ok(new { status = HttpStatusCode.OK, message = "No departments found", data = new List<Department>() });
            }
            return Ok(new { HttpStatusCode.OK, message = "Departments successfully retrieved", data = departments });
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception for debugging purposes
            //    Console.WriteLine($"An error occurred while retrieving departments: {ex}");

            //    return StatusCode(500, new { status = "error", message = "Internal server error", code = "500" });
            //}
        }

        [HttpGet("{deptId}")]
        public IActionResult GetDepartmentById(string deptId)
        {
            var department = _departmentRepository.GetDepartmentById(deptId);

            if (department == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "No department found with the specified id" });
            }
            return Ok(new { status = HttpStatusCode.OK, message = "Department found", data = department });
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Dept_Initial) || string.IsNullOrEmpty(department.Dept_Name))
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Invalid request. Department initial and name are required." });
            }

            int result = _departmentRepository.AddDepartment(department);

            if (result > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Department added successfully", data = result });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Failed to add department" });
        }


        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Dept_Initial) || string.IsNullOrEmpty(department.Dept_Name))
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Invalid request. Department initial and name are required." });
            }

            int result = _departmentRepository.UpdateDepartment(department);

            if (result > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Department updated successfully", data = result });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { status = HttpStatusCode.InternalServerError, message = "Failed to update department"});
        }

        [HttpDelete("{deptId}")]
        public IActionResult DeleteDepartment(string deptId)
        {
            if (string.IsNullOrEmpty(deptId))
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Invalid request. Department id is required."});
            }

            int result = _departmentRepository.DeleteDepartment(deptId);

            if (result > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Department deleted successfully", data = result });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { status = HttpStatusCode.InternalServerError, message = "Failed to delete department"});
        }

        //[HttpGet]
        //public IActionResult GetAllDepartments()
        //{
        //    try 
        //    {

        //        var departments = _departmentRepository.GetAllDepartments();
        //        if (departments.Count() != 0)
        //        {
        //            return Ok();
        //        }
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception for debugging purposes
        //        _logger.LogError($"Error retrieving departments: {ex}");

        //        // Return a 500 Internal Server Error with a generic error message
        //        return StatusCode(500, new { Error = "An unexpected error occurred while processing the request." });
        //    };
        //}

    }
}
